using AHT_Triggers.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AHT_Triggers.Data
{
    /// <summary>
    /// Information about a local variable, that being its index and what procedure it's used in.
    /// </summary>
    internal struct LocalVar
    {
        public int index;
        public int proc;
    }

    /// <summary>
    /// Result of decoding a gamescript.
    /// </summary>
    public enum DecodeResult
    {
        /// <summary>
        /// No errors occoured during decompilation.
        /// </summary>
        Success = 0b0,
        /// <summary>
        /// The indentation went negative.
        /// </summary>
        NegativeIndent = 0b1,
        /// <summary>
        /// An attempt was made to index a variable out of bounds.
        /// </summary>
        InvalidVar = 0b10,
        /// <summary>
        /// An attempt was made to index a procedure out of bounds.
        /// </summary>
        InvalidProc = 0b100
    }

    /// <summary>
    /// <para>
    /// Contains methods for analyzing and decompiling a <see cref="GameScript"/>.
    /// </para>
    /// <para>
    /// GameScripts are written in a BASIC-like language with simple syntax and rules.
    /// When compiled, the source GameScripts are turned into <i>bytecode</i>, which is the raw data of each line for
    /// the interpreter to process.
    /// Note that the compilation of GameScripts is <i>lossy</i>, so a lot of information is lost from the original source file.
    /// </para>
    /// <para>
    /// The <see cref="DecompileScript(out DecodeResult)"/> method will analyze the given
    /// gamescript and return its best estimate of how the original source file of the GameScript would've looked like.
    /// </para>
    /// <para>
    /// Alternatively, the <see cref="BytecodeToString"/> method simply lists all the raw bytecode data in a list,
    /// along with what each line means.
    /// </para>
    /// <para>
    /// When supplied with a <see cref="GameScriptSaveInfo"/> object, the decompiler can replace the auto-generated names for
    /// variables, procedures and labels with user-defined ones.
    /// </para>
    /// </summary>
    public class ByteCodeDecompiler
    {
        /// <summary>
        /// The script the decompiler will read data from.
        /// </summary>
        public GameScript Script { get; set; }

        /// <summary>
        /// The saved info about the names of elements.
        /// </summary>
        public GameScriptSaveInfo SaveInfo { get; set; }

        /// <summary>
        /// Whether comments showing commands that could not be deciphered should be included in the decompiled text.
        /// </summary>
        public bool ShowUnknown { get; set; }

        /// <summary>
        /// Amount of whitespace to be inserted before the line of code that is to be deciphered.
        /// </summary>
        private int Indentation = 0;

        /// <summary>
        /// How many spaces are added for each indentation level.
        /// </summary>
        private static readonly int INDENT_LEVEL = 4;

        /// <summary>
        /// Describes how a line of code will change the indentation.
        /// </summary>
        private enum IndentState
        {
            /// <summary>
            /// The indentation will not change.
            /// </summary>
            None,
            /// <summary>
            /// The indentation will increase once after this line.
            /// </summary>
            IncAfter,
            /// <summary>
            /// The indentation will decrease once before this line.
            /// </summary>
            DecBefore,
            /// <summary>
            /// The indentation will decrease once before this line, then increase once after this line.
            /// </summary>
            DecBeforeIncAfter,
            /// <summary>
            /// The indentation will increase twice after this line.
            /// </summary>
            IncTwiceAfter,
            /// <summary>
            /// The indentation will decrease twice before this line.
            /// </summary>
            DecTwiceBefore
        }

        /// <summary>
        /// Holds the current <see cref="IndentState"/> during decompilation.
        /// </summary>
        private IndentState CurrentIndentState = IndentState.None;

        /// <summary>
        /// List of local variables that have been used throughout the script code.
        /// Used to deduce what procedures local variables should be declared in, as well as whether any are unused.
        /// The <see cref="LocalVar"></see> object holds the index of the variable and the index of the procedure it's used in.
        /// </summary>
        private List<LocalVar> TrackedLocals;

        /// <summary>
        /// List of global variables that have been used throughout the script code.
        /// Used to deduce what globals are unused.
        /// Each entry is the index of the variable.
        /// </summary>
        private List<int> TrackedGlobals;

        /// <summary>
        /// Whether variables will be tracked using the <see cref="TrackVar(int)"/> method.
        /// </summary>
        private bool TrackVars = false;

        /// <summary>
        /// Dictionary of procedures in the gamescript, where the key is the line number and the value is the name.
        /// </summary>
        private Dictionary<int, string> ProcHeaders = new Dictionary<int, string>();

        /// <summary>
        /// Dictionary of labels in the gamescript, where the key is the line number and the value is the name.
        /// </summary>
        private Dictionary<int, string> LabelHeaders = new Dictionary<int, string>();

        /// <summary>
        /// Holds the index of the current procedure while decompiling. -1 when before the first procedure.
        /// </summary>
        private int CurrentProc;

        /// <summary>
        /// The result of the current decompilation.
        /// </summary>
        private DecodeResult CurrentDecodeResult = DecodeResult.Success;

        /// <summary>
        /// List of internal globals.
        /// These are always present if any variables are used, and are used for things such as return values, messages from other triggers etc.
        /// </summary>
        private static readonly Dictionary<int, string> LVAR_NAMES = new Dictionary<int, string>
        {
          //[0]
          //[1]
          //[2]
          //[3]
          //[4]
            [5]  = "YESNO",
          //[6]
            [7]  = "MESSAGE",
          //[8]
          //[9]
          //[10]
          //[11]
          //[12]
          //[13]
          //[14]
            [15] = "RETURN0",
            [16] = "RETURN1",
            [17] = "RETURN2",
            [18] = "RETURN3",
            [19] = "RETURN4",
            [20] = "RETURN5",
            [21] = "RETURN6",
            [22] = "RETURN7"
          //[23]
        };

        /// <summary>
        /// Instantiates the <see cref="ByteCodeDecompiler"/> with the <see cref="GameScript"/> that it will read data from.
        /// </summary>
        /// <param name="script">Script that the decompiler will read data from</param>
        public ByteCodeDecompiler(GameScript script)
        {
            this.Script = script;
        }
        
        /// <summary>
        /// Instantiates the <see cref="ByteCodeDecompiler"/> with the <see cref="GameScript"/> that it will read data from,
        /// and the <see cref="GameScriptSaveInfo"/> object it will fetch user-defined names from.
        /// </summary>
        /// <param name="script">Script that the decompiler will read data from</param>
        /// <param name="info">Saved info for user-defined names</param>
        public ByteCodeDecompiler(GameScript script, GameScriptSaveInfo info)
        {
            this.Script = script;
            this.SaveInfo = info;
        }

        /// <summary>
        /// Construct list of strings for each procedure listed in the script's VTable.
        /// Each string has the index into the VTable followed by the entry's name (or "(none)" if the reference is empty).
        /// </summary>
        /// <returns>List of strings for each entry in the VTable</returns>
        public List<string> GetVTable()
        {
            List<string> list = new List<string>();

            //Iterate through all entries in the VTable
            int i = 0;
            foreach (byte b in Script.VTable)
            {
                //Show index on the left
                string index = (i + " - ").PadLeft(5, ' ');

                //Add procedure name if it's a valid reference.
                if ((b >= 0) && (b < Script.Procedures.Count))
                {
                    list.Add(index + GetProcName(b));
                } else
                {
                    list.Add(index + "(none)");
                }

                i++;
            }

            return list;
        }

        /// <summary>
        /// Generates a name for the procedure at the given index into the procedure table.
        /// The name will be the same as is stored in the script's data,
        /// except if multiple procedures exist with the same name (due to the 9-character limitation),
        /// in which case its index will be added at the end to make it distinguishable.
        /// Returns "INVALID_PROC" if the index is invalid.
        /// </summary>
        /// <param name="proc">Index into procedure table</param>
        /// <returns>Generated procedure name</returns>
        public string GenerateProcName(int proc)
        {
            if ((proc < 0) || (proc >= Script.Procedures.Count))
            {
                CurrentDecodeResult |= DecodeResult.InvalidProc;
                return "INVALID_PROC";
            }

            string name = Script.Procedures[proc].Name;

            for (int i = 0; i < Script.Procedures.Count; i++)
            {
                //Don't examine the same procedure
                if (i == proc) { continue; }

                //If the name is idential to other procedures, add its index onto the end so it can be identified.
                if (Script.Procedures[i].Name == name)
                {
                    return name + "_" + proc.ToString();
                }
            }

            return name;
        }

        /// <summary>
        /// Get the name of the procedure at the given index into the procedure table.
        /// Will use an automatically generated name if no savedata is defined.
        /// </summary>
        /// <param name="proc">Index into procedure table</param>
        /// <returns>Name of procedure</returns>
        public string GetProcName(int proc)
        {
            if (SaveInfo != null)
            {
                try
                {
                    return SaveInfo.GetProc(proc);
                } catch
                {
                    CurrentDecodeResult |= DecodeResult.InvalidProc;
                    return "INVALID_PROC";
                }
            }

            return GenerateProcName(proc);
        }

        /// <summary>
        /// Auto-generate names for labels in the code. They are formatted as "lbl_[line number]".
        /// Note that labels are not specified in the code, GOTOs instead simply refer to a line number they jump to.
        /// </summary>
        /// <returns>Dictionary of labels - key is line number, value is name</returns>
        public Dictionary<int, string> GenerateLabels()
        {
            Dictionary<int, string> labels = new Dictionary<int, string>();
            //Name labels after the order of when they're found
            int index = 0;

            //Loop through all code
            foreach (CodeLine line in Script.Code)
            {
                //Check for GOTO
                if (line.InstructionID == 0x14)
                {
                    //Get line number referenced by the GOTO statement
                    int lineNum = line.Data4;

                    //If we found a new label, give it a name and add it to the list
                    if (!labels.ContainsKey(lineNum))
                    {
                        labels.Add(lineNum, "lbl_" + index.ToString());
                        index++;
                    }
                }
            }

            return labels;
        }

        /// <summary>
        /// Count the amount of labels in the gamescript.
        /// </summary>
        /// <returns>Amount of labels in the gamescript</returns>
        public int CountLabels()
        {
            //Keep track of labels we've found already
            List<int> found = new List<int>();

            //Loop through all lines of code
            foreach (CodeLine line in Script.Code)
            {
                //Check for GOTO
                if (line.InstructionID == 0x14)
                {
                    //Check for line number in the GOTO statement,
                    //add to list if it's a new label
                    if (!found.Contains(line.Data4))
                    {
                        found.Add(line.Data4);
                    }
                }
            }

            //Return amount of elements in the list of found labels
            return found.Count;
        }

        /// <summary>
        /// Create a compare statement between two variables/values with a given operator ID.
        /// </summary>
        /// <param name="val1">Value/variable on the left of the statement</param>
        /// <param name="val2">Value/variable on the right of the statement</param>
        /// <param name="oper">ID of the operator</param>
        /// <returns>String of the final comparison statement</returns>
        private string CreateCompareStatement(string val1, string val2, byte oper)
        {
            string str = "";

            int o = oper & 0b111;
            switch (o)
            {
                case 1:
                    str = val1 + " == " + val2;
                    break;
                case 2:
                    str = val1 + " > " + val2;
                    break;
                case 3:
                    str = val1 + " >= " + val2;
                    break;
                case 4:
                    str = val1 + " < " + val2;
                    break;
                case 5:
                    str = val1 + " <= " + val2;
                    break;
                case 6:
                    str = val1 + " != " + val2;
                    break;
            }

            if ((oper & 0b1000) != 0)
            {
                str = "NOT " + str;
            }

            return str;
        }

        /// <summary>
        /// Create a math statement between two variables/values with a given operator ID.
        /// </summary>
        /// <param name="val1">Value/variable on the left of the statement</param>
        /// <param name="val2">Value/variable on the right of the statement</param>
        /// <param name="oper">ID of the operator</param>
        /// <returns>String of the final math statement</returns>
        private string CreateMathStatement(string val1, string val2, byte oper)
        {
            string str = "";

            int o = oper & 0b111;
            switch (o)
            {
                case 1:
                    str = val1 + " * " + val2;
                    break;
                case 2:
                    str = val1 + " / " + val2;
                    break;
                case 3:
                    str = val1 + " + " + val2;
                    break;
                case 4:
                    str = val1 + " - " + val2;
                    break;
                case 5:
                    str = val1 + " << " + val2;
                    break;
                case 6:
                    str = val1 + " >> " + val2;
                    break;
                case 7:
                    str = val1 + " & " + val2;
                    break;
                case 8:
                    str = val1 + " | " + val2;
                    break;
                case 9:
                    str = val1 + " ^ " + val2;
                    break;
                case 10:
                    str = val1 + " * -" + val2;
                    break;
                case 11:
                    str = val1 + " / -" + val2;
                    break;
            }

            return str;
        }

        /// <summary>
        /// Add newly used local variables to the <see cref="TrackedLocals"/> list,
        /// and add newly used globale variables to the <see cref="TrackedGlobals"/> list.
        /// Used during decompilation to keep track of if and where variables are used.
        /// </summary>
        /// <param name="i">Index of variable</param>
        private void TrackVar(int i)
        {
            //Check if we're dealing with a local or global
            if (i < Script.NumGlobals)
            {
                //Only add globals that are not language-level variables (first 24 globals)
                if (i > 23)
                {
                    //Add to list if it isn't tracked already
                    if (TrackedGlobals.Contains(i)) { return; }
                    TrackedGlobals.Add(i);
                }
            }
            else
            {
                //Check if this local is tracked already
                foreach (LocalVar v in TrackedLocals)
                {
                    if (v.index == i)
                    {
                        return;
                    }
                }

                //Add to list
                TrackedLocals.Add(
                    new LocalVar
                    {
                        index = i,
                        proc = CurrentProc
                    }
                );
            }
        }

        /// <summary>
        /// Create a name for a variable at the given index.
        /// Local variables are formatted as "loc[index]",
        /// global variables are formatted as "glo[index]",
        /// language variables either use their pre-defined names or a generated name with format "LVAR[index]"
        /// Returns "INVALID_VAR" if outside the index range.
        /// </summary>
        /// <param name="i">Variable index</param>
        /// <returns>Generated name</returns>
        public string GenerateVarName(int i)
        {
            if ((i >= Script.NumVars) || (i < 0))
            {
                CurrentDecodeResult |= DecodeResult.InvalidVar;
                return "INVALID_VAR";
            }

            if (i >= Script.NumGlobals)
            {
                //Local variable
                return "loc" + (i - Script.NumGlobals);
            } else if (i > 23)
            {
                //Global variable (outside procedure scope)
                return "glo" + (i - 24);
            } else
            {
                //Language variable
                //Check if there's a pre-defined name for it, else generate one
                if (LVAR_NAMES.ContainsKey(i))
                {
                    return LVAR_NAMES[i];
                }
                return "LVAR" + i;
            }
        }

        /// <summary>
        /// Get the name of the variable at the given index.
        /// Will use an automatically generated name if no savedata is defined.
        /// </summary>
        /// <param name="i">Variable index</param>
        /// <returns>Name of variable</returns>
        private string GetVarName(int i)
        {
            if (TrackVars) { TrackVar(i); }

            if (SaveInfo != null)
            {
                try
                {
                    return SaveInfo.GetVar(i);
                } catch
                {
                    CurrentDecodeResult |= DecodeResult.InvalidVar;
                    return "INVALID_VAR";
                }
            }

            return GenerateVarName(i);
        }

        /// <summary>
        /// Turn a value into a string representation, or a hashcode if one exists for it.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>String representation of the value, or the hashcode label if one exists for it</returns>
        private string ValToString(int value)
        {
            if (Enum.IsDefined(typeof(EXHashCode), (uint)value) && (value != 0))
            {
                return ((EXHashCode)value).ToString();
            } else if(Enum.IsDefined(typeof(ESHashCode), (uint)value))
            {
                return ((ESHashCode)value).ToString();
            } else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Add whitespace before the given string <paramref name="s"/>
        /// according to the current <see cref="Indentation"/> and <see cref="INDENT_LEVEL"/>.
        /// </summary>
        /// <param name="s">Command to indent</param>
        /// <returns>Indented command</returns>
        private string AddIndentToCommand(string s)
        {
            //Don't add indentation if negative
            if (Indentation < 0)
            {
                return s;
            } else
            {
                return new string(' ', Indentation * INDENT_LEVEL) + s;
            }
        }

        /// <summary>
        /// Add the indentation to a code line <paramref name="instr"/>, and increase/decrease <see cref="Indentation"/> accordingly
        /// </summary>
        /// <param name="instr">String representing the line of code</param>
        /// <param name="becameNegative">Set to true if indentation became negative during the process</param>
        /// <returns>Indented line of code</returns>
        private string AddIndent(string instr, out bool becameNegative)
        {
            string str;

            //Increase/decrease indentation according to the state
            switch (CurrentIndentState)
            {
                case IndentState.None:
                    str = AddIndentToCommand(instr);
                    break;
                case IndentState.IncAfter:
                    str = AddIndentToCommand(instr);
                    Indentation += 1;
                    break;
                case IndentState.DecBefore:
                    Indentation -= 1;
                    str = AddIndentToCommand(instr);
                    break;
                case IndentState.DecBeforeIncAfter:
                    Indentation -= 1;
                    str = AddIndentToCommand(instr);
                    Indentation += 1;
                    break;
                case IndentState.IncTwiceAfter:
                    str = AddIndentToCommand(instr);
                    Indentation += 2;
                    break;
                case IndentState.DecTwiceBefore:
                    Indentation -= 2;
                    str = AddIndentToCommand(instr);
                    break;
                default:
                    //In theory this can't happen
                    str = "INVALID INDENTATION";
                    break;
            }

            //Report if indentation became negative
            if (Indentation < 0)
            {
                Indentation = 0;
                becameNegative = true;
            } else
            {
                becameNegative = false;
            }

            //Reset state
            CurrentIndentState = IndentState.None;

            return str;
        }

        /// <summary>
        /// Returns if the ENDPROC command on the given line is the one that closes out a procedure.
        /// </summary>
        /// <param name="lineNr">Line in the code of the ENDPROC command.</param>
        /// <returns>true if the command closes out the procedure.</returns>
        private bool EndProcIsFinal(int lineNr)
        {
            //False if not an ENDPROC command
            if (Script.Code[lineNr].InstructionID != 0x1e)
            {
                return false;
            }

            //True if a procedure starts right afterwards.
            foreach (Procedure proc in Script.Procedures)
            {
                if (proc.StartLine == lineNr + 1)
                {
                    return true;
                }
            }

            //True if it's the final line of the script
            if (lineNr == Script.NumLines - 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a procedure ever returns a value or variable.
        /// </summary>
        /// <param name="procNr">Index of procedure.</param>
        /// <returns>true if procedure returns variable/value, false if it just returns.</returns>
        private bool ProcReturnsValue(int procNr)
        {
            //Abort if out of bounds
            if ((procNr < 0) || (procNr >= Script.NumProcs))
            {
                CurrentDecodeResult |= DecodeResult.InvalidProc;
                return false;
            }

            //Loop through lines starting from procedure start
            for (int i = Script.Procedures[procNr].StartLine; i < Script.NumLines; i++)
            {
                CodeLine line = Script.Code[i];

                //Check for endproc
                if (line.InstructionID == 0x1e)
                {
                    //If data1 is 1, it returns a variable/value
                    if (line.Data1 != 0)
                    {
                        return true;
                    }
                }

                //If we've reached the end, just return.
                if (EndProcIsFinal(i)) return false;
            }

            return false;
        }

        /// <summary>
        /// Decompile the gamescript and put it in a string.
        /// Decompilation includes deciphering each command to a text representation,
        /// adding intendation for readability, adding variable declaration etc...
        /// </summary>
        /// <param name="res">Contains the <see cref="DecodeResult"/> of the decompilation</param>
        /// <returns>Decompiled gamescript contained in a string</returns>
        public string DecompileScript(out DecodeResult res)
        {
            CurrentDecodeResult = DecodeResult.Success;

            //Start lists of procedures
            ProcHeaders.Clear();

            //Obtain list of labels to add in second pass of decompilation
            LabelHeaders = GenerateLabels();

            //Reset indentation
            Indentation = 0;

            //Reset current procedure
            CurrentProc = -1;

            //Start tracking variables
            TrackVars = true;
            TrackedGlobals = new List<int>();
            TrackedLocals = new List<LocalVar>();

            //Create list of strings in our decompiled output
            List<string> codeStr = new List<string>();

            //With everything set up, let's start decompiling:

            /* 
             * -- FIRST PASS --
             * Add individual deciphered commands and track the usages of
             * variables and the info about procedures.
             */
            
            //Loop through all lines of code
            for (int i = 0; i < Script.NumLines; i++)
            {
                //Check for start of procedure
                for (int j = 0; j < Script.Procedures.Count; j++)
                {
                    Procedure p = Script.Procedures[j];

                    //If start line of procedure matches current line, add to the list
                    if (p.StartLine == i)
                    {
                        string s = GetProcName(j);

                        //If exclusive, mark as such
                        if (p.Exclusive)
                        {
                            s += " exclusive";
                        }

                        //Set indentation to one after declaration
                        Indentation = 1;

                        //Add to list to refer to later
                        ProcHeaders.Add(i, s);

                        //Set current procedure to this one
                        CurrentProc = j;

                        break;
                    }
                }

                //Get decoded command from the bytecode
                string command = DecipherCommand(Script.Code[i], i);

                //Ignore command if it came back empty
                if (command == string.Empty)
                {
                    continue;
                }
                
                //Add proper whitespace before command
                string str = AddIndent(command, out bool becameNegative);

                //Report a negative indentation if it occours
                if (becameNegative)
                {
                    CurrentDecodeResult |= DecodeResult.NegativeIndent;
                }

                //Finally, add it to the list of strings
                codeStr.Add(str);
            }

            //We're not tracking the use of variables anymore
            TrackVars = false;

            /* 
             * -- SECOND PASS --
             * Add procedure declarations, labels, variable declarations, relocate "AND" statements among other things.
             */

            //Keep track of how far we've drifted using a variable, so we don't add a line to the wrong place.
            //This variable is incremented every time a line is inserted that doesn't directly correlate with a code line in the data.
            //(and decremented when we remove a line)
            int offs = 0;

            //Add list of global declarations at the top (declared with INT)
            if (Script.NumGlobals > 0)
            {
                //Start at 24 to only declare variables after language-level vars
                for (int i = 24; i < Script.NumGlobals; i++)
                {
                    string str = "INT " + GetVarName(i);

                    //If we never encountered the global during first pass, then we mark it as unused.
                    if (!TrackedGlobals.Contains(i))
                    {
                        str += " //Unused global";
                    }

                    codeStr.Insert(offs, str);
                    offs++;
                }
            }

            //Add info about unused locals, if any exist, by checking if the tracked amount is less than the actual amount
            int numLocals = Script.NumVars - Script.NumGlobals;
            if (numLocals > TrackedLocals.Count)
            {
                //Insert empty line beforehand
                codeStr.Insert(offs, "");
                offs++;
                codeStr.Insert(offs, "//Unused locals:");
                offs++;

                //Loop through all local variables
                for (int i = Script.NumVars - numLocals; i < Script.NumVars; i++)
                {
                    //Check if we've tracked this local
                    bool exist = false;
                    foreach (LocalVar v in TrackedLocals)
                    {
                        if (v.index == i)
                        {
                            exist = true;
                            break;
                        }
                    }

                    if (!exist)
                    {
                        codeStr.Insert(offs, "//  "+GetVarName(i));
                        offs++;
                    }
                }

                codeStr.Insert(offs, "");
                offs++;
            }

            //Keep track of which procedures we've added declarations for
            CurrentProc = -1;

            //Loop through all lines of code again
            for (int i = 0; i < Script.NumLines; i++)
            {
                //Check if a list contains a value at a given line of code (+ the drift), and insert the name if so.

                if (ProcHeaders.ContainsKey(i))
                {
                    CurrentProc++;

                    //Add empty line before procedure declaration if one doesn't exist already
                    if ((i+offs) > 0)
                    {
                        //Check if previous line is empty
                        if (codeStr[i+offs-1] != "")
                        {
                            codeStr.Insert(i + offs, "");
                            offs++;
                        }
                    }

                    //Add declaration
                    codeStr.Insert(i + offs, "DEFPROC " + ProcHeaders[i]);
                    offs++;

                    //Add local declarations for the current procedure at the top
                    if (TrackedLocals.Count > 0)
                    {
                        //We only add an empty line afterwards if we actually put any locals there
                        bool addNewLine = false;

                        //Loop through tracked locals and add declaration if it's part of this procedure
                        foreach (LocalVar v in TrackedLocals)
                        {
                            if (v.proc == CurrentProc)
                            {
                                codeStr.Insert(i + offs, new string(' ', INDENT_LEVEL) + "INT " + GetVarName(v.index));
                                offs++;

                                addNewLine = true;
                            }
                        }

                        if (addNewLine)
                        {
                            codeStr.Insert(i + offs, "");
                            offs++;
                        }
                    }
                }

                //If the line number has an associated label, add it
                //If save data is defined, grab the name from that, otherwise just grab a generated name
                string lblName = null;
                if (SaveInfo != null)
                {
                    if (SaveInfo.Labels.ContainsKey(i))
                    {
                        lblName = SaveInfo.Labels[i];
                    }
                } else if (LabelHeaders.ContainsKey(i))
                {
                    lblName = LabelHeaders[i];
                }

                if (lblName != null)
                {
                    codeStr.Insert(i + offs, new string(' ', INDENT_LEVEL) + "LABEL " + lblName);
                    offs++;
                }

                //Take any line with "AND" and stick it onto the line before, then delete the original line
                if (i < codeStr.Count)
                {
                    if (codeStr[i].IndexOf(" AND") >= 0)
                    {
                        codeStr[i - 1] += " " + codeStr[i].Trim();
                        codeStr.RemoveAt(i);
                        offs--; //Since we've removed a line, not added one
                    }
                }
            }

            /* 
             * -- THIRD PASS --
             * Right now any reference to a label just refers to its line number with the tag: "#L[line number]".
             * In this pass we replace these tags with the name of the label.
             */

            for (int i = 0; i < codeStr.Count; i++)
            {
                string s;

                //Find the index of the tag
                int n = codeStr[i].IndexOf("#L");
                if (n < 0) { continue; } //If one doesn't exist, continue

                //Remove everything up to the number
                s = codeStr[i].Remove(0, n + 2);

                //Check if anything after the number exists, remove if there is

                //Get the index of the nearest space or line break
                int iNextSpace = s.IndexOf(' ');
                int iNextLineBr = s.IndexOf("\n");

                if (iNextLineBr < 0)
                {
                    n = iNextSpace;
                }
                else if (iNextSpace < 0)
                {
                    n = iNextLineBr;
                }
                else
                {
                    n = Math.Min(iNextSpace, iNextLineBr);
                }

                //If any exists, remove from here
                if (n >= 0)
                {
                    s = s.Remove(n);
                }

                //Try to parse a number from the resulting string
                int lineNum;
                try
                {
                    lineNum = int.Parse(s);
                } catch
                {
                    Console.WriteLine("Invalid number to parse: " + codeStr[i]);
                    continue;
                }

                //Get the name of the label. If no save info is present, just use the auto-generated ones.
                string lblName;
                if (SaveInfo != null)
                {
                    //Check if a label exists at this line number
                    if (!SaveInfo.Labels.ContainsKey(lineNum))
                    {
                        Console.WriteLine("Line number does not correspond to any label: " + lineNum);
                        continue;
                    }

                    lblName = SaveInfo.Labels[lineNum];
                } else
                {
                    //Check if a label exists at this line number
                    if (!LabelHeaders.ContainsKey(lineNum))
                    {
                        Console.WriteLine("Line number does not correspond to any label: " + lineNum);
                        continue;
                    }

                    lblName = LabelHeaders[lineNum];
                }

                //Replace the tag with the label name
                codeStr[i] = codeStr[i].Replace("#L" + lineNum, lblName);
            }

            //Build string and return it
            StringBuilder sb = new StringBuilder();
            foreach (string s in codeStr)
            {
                sb.AppendLine(s);
            }

            res = CurrentDecodeResult;
            return sb.ToString();
        }

        /// <summary>
        /// Create a list of instructions from the bytecode.
        /// </summary>
        /// <returns>String containing the list of instructions</returns>
        public string BytecodeToString()
        {
            StringBuilder sb = new StringBuilder();

            //Get how wide the line number column needs to be
            int lineNrCol = (Script.NumLines - 1).ToString().Length + 1;

            for (int i = 0; i < Script.NumLines; i++)
            {
                CodeLine line = Script.Code[i];

                //Add a little label when a procedure starts on the next line
                foreach (Procedure proc in Script.Procedures)
                {
                    if (proc.StartLine == i)
                        sb.AppendLine("  DEFPROC " + proc.Name);
                }

                //This is a mess but it's just padding the strings to make it look nice
                //  [line number] | instr: [instruction ID] | data: [[data1]] [[data2]] [[data3]] [[data4]] | [decompiled command]
                string str =
                    i.ToString().PadRight(lineNrCol, ' ') +
                    string.Format("| instr: 0x{0:X}", line.InstructionID).PadRight(14, ' ') + "| data: [0x" +
                    string.Format("{0:X}",            line.Data1).PadLeft(2, '0')           + "] [0x" +
                    string.Format("{0:X}",            line.Data2).PadLeft(2, '0')           + "] [0x" +
                    string.Format("{0:X}",            line.Data3).PadLeft(2, '0')           + "] [0x" +
                    string.Format("{0:X}",            line.Data4).PadLeft(8, '0')           + "] | " + 
                    DecipherCommand(line, i);

                sb.AppendLine(str);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Turn an instruction into a string representation of the line of code.
        /// </summary>
        /// <param name="line">Line of code to decipher</param>
        /// <param name="lineNr">Line number</param>
        /// <returns>Deciphered line of code</returns>
        public string DecipherCommand(CodeLine line, int lineNr)
        {
            //Reset indent state
            CurrentIndentState = IndentState.None;

            string str;
            string val;

            //Format string depending on the instruction ID of the line
            switch(line.InstructionID)
            {
                /*--------------------*/
                /*- GENERIC COMMANDS -*/
                /*--------------------*/

                case 0x2: // IF <var1> <operator> <var2>
                    CurrentIndentState = IndentState.IncAfter;
                    str = "IF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            GetVarName(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x3: // IF <var> <operator> <value>
                    CurrentIndentState = IndentState.IncAfter;
                    str = "IF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x4: // AND <var1> <operator> <var2>
                    CurrentIndentState = IndentState.DecBeforeIncAfter;
                    str = " AND " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            GetVarName(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x5: // AND <var> <operator> <value>
                    CurrentIndentState = IndentState.DecBeforeIncAfter;
                    str = " AND " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x6: // ELSE
                    CurrentIndentState = IndentState.DecBeforeIncAfter;
                    str = "ELSE";

                    break;
                case 0x7: // ELSEIF <var1> <operator> <var2>
                    CurrentIndentState = IndentState.DecBeforeIncAfter;
                    str = "ELSEIF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            GetVarName(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x8: // ELSEIF <var> <operator> <value>
                    CurrentIndentState = IndentState.DecBeforeIncAfter;
                    str = "ELSEIF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x9: // ENDIF
                    CurrentIndentState = IndentState.DecBefore;
                    str = "ENDIF";

                    break;
                case 0xa: // REPEAT <var/value>
                    if (line.Data3 == 0)
                    {
                        val = GetVarName(line.Data2);
                    } else
                    {
                        val = ValToString(line.Data4);
                    }

                    CurrentIndentState = IndentState.IncAfter;
                    str = "REPEAT " + val;

                    break;
                case 0xc: // ENDREPEAT
                    CurrentIndentState = IndentState.DecBefore;
                    str = "ENDREPEAT";

                    break;
                case 0xe: // <var> = <var/value>
                    if (line.Data3 == 0)
                    {
                        if (line.Data4 == 0)
                        {
                            str = GetVarName(line.Data1) + " = "+GetVarName(line.Data2);
                        } else
                        {
                            str = GetVarName(line.Data1) + " = -" + GetVarName(line.Data2);
                        }
                    } else
                    {
                        str = GetVarName(line.Data1) + " = "+ValToString(line.Data4);
                    }

                    break;
                case 0x14: // GOTO <line>
                    str = "GOTO #L" + line.Data4;

                    break;
                case 0x15: // <var1> = <var2/value> <operator> <var3>
                    if ((line.Data3 & 0b1000000) != 0)
                    {
                        str = GetVarName(line.Data1) + " = " + CreateMathStatement(
                                ValToString(line.Data4),
                                GetVarName(line.Data2),
                                line.Data3
                            );
                    } else
                    {
                        str = GetVarName(line.Data1) + " = " + CreateMathStatement(
                                GetVarName(line.Data4),
                                GetVarName(line.Data2),
                                line.Data3
                            );
                    }

                    break;
                case 0x16: // <var1> = <var2> <operator> <value>
                    str = GetVarName(line.Data1) + " = " + CreateMathStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x17: // <var1> = GETBIT <var2> <value>
                    if (line.Data4 == 0)
                    {
                        val = ValToString(line.Data2);
                    } else
                    {
                        val = GetVarName(line.Data2);
                    }

                    str = string.Format("{0} = GETBIT {1} {2}",
                            GetVarName(line.Data3),
                            GetVarName(line.Data1),
                            val
                        );

                    break;
                case 0x18: // SETBIT <var> <value> <value>
                    if (line.Data4 == 0)
                    {
                        val = ValToString(line.Data2);
                    }
                    else
                    {
                        val = GetVarName(line.Data2);
                    }

                    str = string.Format("SETBIT {0} {1} {2}",
                            GetVarName(line.Data1),
                            val,
                            GetVarName(line.Data3)
                        );

                    break;
                case 0x1a: // WHILE <var> <operator> <value>
                    CurrentIndentState = IndentState.IncAfter;
                    str = "WHILE " + CreateCompareStatement(GetVarName(line.Data2), ValToString(line.Data4), line.Data3);

                    break;
                case 0x1b: // ENDWHILE
                    CurrentIndentState = IndentState.DecBefore;
                    str = "ENDWHILE";

                    break;
                case 0x1e: // ENDPROC <optional var/value>
                    //Decrease indentation if this command closes out a procedure
                    if (EndProcIsFinal(lineNr))
                    {
                        CurrentIndentState = IndentState.DecBefore;
                    }

                    if (line.Data1 == 0)
                    {
                        str = "ENDPROC";
                    } else if (line.Data1 == 1)
                    {
                        str = "ENDPROC " + ValToString(line.Data4);
                    } else
                    {
                        str = "ENDPROC " + GetVarName(line.Data4);
                    }

                    break;
                case 0x20: // <var> = CALLPROC <procedure>
                    str = "CALLPROC " + GetProcName(line.Data1);

                    //Only add variable assignment if procedure returns a value
                    if (ProcReturnsValue(line.Data1))
                    {
                        str = GetVarName(line.Data3) + " = " + str;
                    }

                    break;
                case 0x21: // <var> = INSTANCE <procedure>
                    str = "INSTANCE " + GetProcName(line.Data1);

                    //Only add variable assignment if procedure returns a value
                    if (ProcReturnsValue(line.Data1))
                    {
                        str = GetVarName(line.Data3) + " = " + str;
                    }

                    break;
                case 0x22: // BREAK
                    str = "BREAK";

                    break;
                case 0x24: // CASE <value/hash>
                    CurrentIndentState = IndentState.DecBeforeIncAfter;
                    str = "CASE "+ValToString(line.Data4);

                    break;
                case 0x25: // SWITCH <var>
                    CurrentIndentState = IndentState.IncTwiceAfter;
                    str = "SWITCH "+GetVarName(line.Data2);

                    break;
                case 0x27: // ENDSWITCH
                    CurrentIndentState = IndentState.DecTwiceBefore;
                    str = "ENDSWITCH";

                    break;
                case 0x28: // <var1> = RAND <var2/value>
                    if (line.Data3 == 0)
                    {
                        val = GetVarName(line.Data2);
                    } else
                    {
                        val = ValToString(line.Data4);
                    }

                    str = GetVarName(line.Data1) + " = RAND " + val;

                    break;
                case 0x2a: // WAIT <var/value>
                    if (line.Data3 == 0)
                    {
                        val = GetVarName(line.Data4);
                    }
                    else
                    {
                        val = ValToString(line.Data4);
                    }

                    str = "WAIT " + val;

                    break;
                case 0x2b: // WAIT <value>
                    str = "WAIT " + ValToString(line.Data4);

                    break;
                case 0x2c: // <var> = GETFRAMECOUNT <scope>
                    str = GetVarName(line.Data4) + " = GETFRAMECOUNT";

                    if (line.Data2 != 0)
                    {
                        str += " local";
                    }

                    break;
                case 0x2d: // SETFRAMECOUNT <var/value> <scope>
                    if (line.Data3 == 0)
                    {
                        val = GetVarName(line.Data1);
                    } else
                    {
                        val = ValToString(line.Data4);
                    }

                    str = "SETFRAMECOUNT " + val;

                    if (line.Data2 != 0)
                    {
                        str += " local";
                    }

                    break;
                case 0x30: // SUSPEND <procedure>
                    str = "SUSPEND ";

                    if (line.Data1 == 0xFE)
                    {
                        str += "ALL";
                    } else
                    {
                        str += GetProcName(line.Data1);
                    }

                    break;
                case 0x31: // RESUME <procedure>
                    str = "RESUME ";

                    if (line.Data1 == 0xFE)
                    {
                        str += "ALL";
                    }
                    else
                    {
                        str += GetProcName(line.Data1);
                    }

                    break;
                case 0x32: // RESET <procedure>
                    str = "RESET ";

                    if (line.Data1 == 0xFE)
                    {
                        str += "ALL";
                    }
                    else
                    {
                        str += GetProcName(line.Data1);
                    }

                    break;
                case 0x33: // KILL <procedure>
                    str = "KILL ";

                    if (line.Data1 == 0xFE)
                    {
                        str += "ALL";
                    }
                    else
                    {
                        str += GetProcName(line.Data1);
                    }

                    break;
                case 0x34: // <var> = ISRUNNING <procedure>
                    str = GetVarName(line.Data3) + " = ISRUNNING " + GetProcName(line.Data1);

                    break;
       /*TODO*/ case 0x38: // DEFARRAY <array>
                    str = "DEFARRAY temp_arrname (size = "+line.Data4+")";

                    break;
       /*TODO*/ case 0x3a: // <var> = GETARRAY <array> <index>
                    if ((line.Data4 & 0x10000) == 1)
                    {
                        val = GetVarName(line.Data4);
                    } else
                    {
                        val = "temp_arrname";
                    }

                    str = GetVarName(line.Data3) + " = GETARRAY " + val + " ";

                    if (line.Data2 == 0)
                    {
                        str += GetVarName(line.Data1);
                    } else
                    {
                        str += ValToString(line.Data1);
                    }

                    break;
       /*TODO*/ case 0x3b: // SETARRAY <array> <index> <value>
                    if ((line.Data4 & 0x10000) == 1)
                    {
                        val = GetVarName(line.Data4);
                    }
                    else
                    {
                        val = "temp_arrname";
                    }

                    str = "SETARRAY " + val + " ";

                    if ((line.Data3 & 1) == 0)
                    {
                        str += GetVarName(line.Data1) + " ";
                    } else
                    {
                        str += ValToString(line.Data1) + " ";
                    }

                    if ((line.Data3 & 2) == 0)
                    {
                        str += GetVarName(line.Data2);
                    } else
                    {
                        str += ValToString(line.Data2);
                    }

                    break;
                case 0x3c: // EXCLUSIVE <value>
                    str = "EXCLUSIVE "+ValToString(line.Data1);

                    break;

                /*--------------------------*/
                /*- GAME-SPECIFIC COMMANDS -*/
                /*--------------------------*/

                case 0x3d: // <var> = GETOBJECTIVE <hash>
                    str = GetVarName(line.Data3) + " = GETOBJECTIVE " + ValToString(line.Data4);

                    break;
                case 0x3e: // SETOBJECTIVE <hash>
                    str = "SETOBJECTIVE " + ValToString(line.Data4);

                    break;
                case 0x40: // SETANIM <hash>
                    if (line.Data4 < 0xFF)
                    {
                        val = GetVarName(line.Data4);
                    } else
                    {
                        val = ValToString(line.Data4);
                    }

                    str = "SETANIM " + val;

                    break;
                case 0x41: // NEXTANIM <hash>
                    if (line.Data4 < 0xFF)
                    {
                        val = GetVarName(line.Data4);
                    }
                    else
                    {
                        val = ValToString(line.Data4);
                    }

                    str = "NEXTANIM " + val;

                    break;
                case 0x44: // <var> = GETDISTANCETOPLAYER
                    str = GetVarName(line.Data3) + " = GETDISTANCETOPLAYER";

                    break;
                case 0x45: // TURNANDFACEPLAYER
                    str = "TURNANDFACEPLAYER";

                    break;
                case 0x46: // <var> = ISFACINGPLAYER
                    str = GetVarName(line.Data3) + " = ISFACINGPLAYER";

                    break;
                case 0x47: // <var> = ISFACINGPLAYERSTRICT
                    str = GetVarName(line.Data3) + " = ISFACINGPLAYERSTRICT";

                    break;
                case 0x48: // STOPMOVING
                    str = "STOPMOVING";

                    break;
                case 0x49: // PRINTMESSAGE <hash>
                    str = "PRINTMESSAGE " + ValToString(line.Data4);

                    break;
                case 0x4a: // KILLMESSAGE <hash>
                    str = "KILLMESSAGE " + ValToString(line.Data4);

                    break;
                case 0x4b: // TALKTOPLAYER <value>
                    str = "TALKTOPLAYER " + ValToString(line.Data4);

                    break;
                case 0x4c: // FORCETALK
                    str = "FORCETALK";

                    break;
                case 0x4d: // WALKTOPOINT <var>
                    str = "WALKTOPOINT " + GetVarName(line.Data1);

                    break;
                case 0x4e: // WALKTOPOINT <value>
                    str = "WALKTOPOINT " + ValToString(line.Data1);

                    break;
                case 0x4f: // <var1> = GETDISTANCETOPATHNODE <var2>
                    str = GetVarName(line.Data3) + " = GETDISTANCETOPATHNODE " + GetVarName(line.Data1);

                    break;
                case 0x50: // <var> = GETDISTANCETOPATHNODE <value>
                    str = GetVarName(line.Data3) + " = GETDISTANCETOPATHNODE " + ValToString(line.Data1);

                    break;
                case 0x51: // <var> = GETNUMBEROFPATHNODES
                    str = GetVarName(line.Data3) + " = GETNUMBEROFPATHNODES";

                    break;
                case 0x52: // TURNTOPATHNODE <var>
                    str = "TURNTOPATHNODE " + GetVarName(line.Data1);

                    break;
                case 0x53: // TURNTOPATHNODE <value>
                    str = "TURNTOPATHNODE " + ValToString(line.Data1);

                    break;
                //case 0x54: // Unused
                //    str = "UNUSED";
                //
                //    break;
                case 0x55: // TALKINDICATOR <value>
                    str = "TALKINDICATOR " + ValToString(line.Data4);

                    break;
                case 0x56: // CUTSEQUENCE <var/hash>
                    if ((line.Data4 & 0xFF000000) != 0)
                    {
                        val = ValToString(line.Data4);
                    } else
                    {
                        val = GetVarName(line.Data4);
                    }

                    str = "CUTSEQUENCE " + val;

                    break;
                case 0x57: // CAMERASTORE
                    str = "CAMERASTORE";

                    break;
                case 0x58: // CAMERARESTORE
                    str = "CAMERARESTORE";

                    break;
                case 0x59: // CAMERAMODESCRIPTPATH <hash>
                    str = "CAMERAMODESCRIPTPATH " + ValToString(line.Data4);

                    break;
                case 0x5a: // LOCKCONTROLS <value>
                    str = "LOCKCONTROLS " + ValToString(line.Data4);

                    break;
                case 0x5b: // ACTIVATETASK <hash>
                    str = "ACTIVATETASK " + ValToString(line.Data4);

                    break;
                case 0x5c: // ACHIEVETASK <hash>
                    str = "ACHIEVETASK " + ValToString(line.Data4);

                    break;
                case 0x5e: // <var> = ISRESTARTPOINTSET <hash>
                    str = GetVarName(line.Data3) + " = ISRESTARTPOINTSET " + ValToString(line.Data4);

                    break;
                case 0x5f: // <var> = ISRESTARTPOINTVISITED <hash>
                    str = GetVarName(line.Data3) + " = ISRESTARTPOINTVISITED " + ValToString(line.Data4);

                    break;
                case 0x60: // SETRESTARTPOINT <hash>
                    str = "SETRESTARTPOINT " + ValToString(line.Data4);

                    break;
                case 0x61: // WAITFORCANCHANGEANIM
                    str = "WAITFORCANCHANGEANIM";

                    break;
                case 0x62: // REACT <value>
                    str = "REACT " + ValToString(line.Data4);

                    break;
                case 0x63: // SENDMESSAGE <ref> <value>
                    str = "SENDMESSAGE " + ValToString(line.Data4) + " " + ValToString(line.Data1);

                    break;
                case 0x64: // SENDTOALL <value>
                    str = "SENDTOALL " + ValToString(line.Data4);

                    break;
                case 0x65: // HEADTRACKING <value>
                    str = "HEADTRACKING " + ValToString(line.Data4);

                    break;
                case 0x66: // TALKCAMERA <value>
                    str = "TALKCAMERA " + ValToString(line.Data4);

                    break;
                case 0x67: // HEROTOTALKDIST <value>
                    str = "HEROTOTALKDIST " + ValToString(line.Data4);

                    break;
                case 0x68: // STOP
                    str = "STOP";

                    break;
                case 0x69: // STARTSOUND <hash>
                    str = "STARTSOUND " + ValToString(line.Data4);

                    break;
                case 0x6a: // PLAYSCRIPT
                    str = "PLAYSCRIPT";

                    break;
                case 0x6b: // PAUSESCRIPT
                    str = "PAUSESCRIPT";

                    break;
                case 0x6c: // RESUMESCRIPT
                    str = "RESUMESCRIPT";

                    break;
                case 0x6d: // KILLSCRIPT
                    str = "KILLSCRIPT";

                    break;
                case 0x6e: // SETFILEHASH <hash>
                    str = "SETFILEHASH " + ValToString(line.Data4);

                    break;
                case 0x6f: // YESNOBOX <var/hash>
                    if ((line.Data4 & 0xFF000000) == 0)
                    {
                        val = GetVarName(line.Data4);
                    } else
                    {
                        val = ValToString(line.Data4);
                    }

                    str = "YESNOBOX " + val;

                    break;
                case 0x70: // HEROTOLINKEDPOINT <ref>
                    str = "HEROTOLINKEDPOINT " + ValToString(line.Data4);

                    break;
                case 0x71: // SETBLENDDEPTH <value>
                    str = "SETBLENDDEPTH " + ValToString(line.Data4);

                    break;
                case 0x72: // HEROTOPATH <hash>
                    str = "HEROTOPATH " + ValToString(line.Data4);

                    break;
                case 0x73: // ENABLEENTITY <hash>
                    str = "ENABLEENTITY " + ValToString(line.Data4);

                    break;
                case 0x74: // DISABLEENTITY <hash>
                    str = "DISABLEENTITY " + ValToString(line.Data4);

                    break;
                case 0x75: // CHANGEHERO <value>
                    str = "CHANGEHERO " + ValToString(line.Data4);

                    break;
                case 0x76: // VISIBILITY <value>
                    str = "VISIBILITY " + ValToString(line.Data4);

                    break;
                case 0x77: // PUTHEROHERE
                    str = "PUTHEROHERE";

                    break;
                case 0x78: // CLEAROBJECTIVE <hash>
                    str = "CLEAROBJECTIVE " + ValToString(line.Data4);

                    break;
                case 0x79: // GIVEGEMSTOHERO <value>
                    str = "GIVEGEMSTOHERO " + ValToString(line.Data4);

                    break;
                case 0x7a: // TAKEGEMSFROMHERO <value>
                    str = "TAKEGEMSFROMHERO " + ValToString(line.Data4);

                    break;
                case 0x7b: // <var> = HOWMANYGEMS
                    str = GetVarName(line.Data3) + " = HOWMANYGEMS";

                    break;
                case 0x7c: // GIVEEGGTOHERO <value>
                    str = "GIVEEGGTOHERO " + ValToString(line.Data4);

                    break;
                case 0x7d: // TAKEEGGFROMHERO <value>
                    str = "TAKEEGGFROMHERO " + ValToString(line.Data4);

                    break;
                case 0x7e: // <var> = HOWMANYEGGS
                    str = GetVarName(line.Data3) + " = HOWMANYEGGS";

                    break;
                case 0x7f: // GIVELIGHTGEMSTOHERO <value>
                    str = "GIVELIGHTGEMSTOHERO " + ValToString(line.Data4);

                    break;
                case 0x80: // TAKELIGHTGEMSFROMHERO <value>
                    str = "TAKELIGHTGEMSFROMHERO " + ValToString(line.Data4);

                    break;
                case 0x81: // <var> = HOWMANYLIGHTGEMS
                    str = GetVarName(line.Data3) + " = HOWMANYLIGHTGEMS";

                    break;
                case 0x82: // VISUALLYGIVEDRAGONEGG <hash>
                    str = "VISUALLYGIVEDRAGONEGG " + ValToString(line.Data4);

                    break;
                case 0x83: // VISUALLYGIVELIGHTGEM
                    str = "VISUALLYGIVELIGHTGEM";

                    break;
                case 0x84: // ZOOPOO <value/hash>
                    str = "ZOOPOO " + ValToString(line.Data4);

                    break;
                case 0x85: // SETPANELCLOCKTIME <value>
                    str = "SETPANELCLOCKTIME " + ValToString(line.Data4);

                    break;
                case 0x86: // PANELCLOCK <value>
                    str = "PANELCLOCK " + ValToString(line.Data4);

                    break;
                case 0x87: // ADDPANELCLOCKTIME <value>
                    str = "ADDPANELCLOCKTIME " + ValToString(line.Data4);

                    break;
                case 0x88: // SUBPANELCLOCKTIME <value>
                    str = "SUBPANELCLOCKTIME " + ValToString(line.Data4);

                    break;
                case 0x89: // MAPPLACEMENTON <hash>
                    str = "MAPPLACEMENTON " + ValToString(line.Data4);

                    break;
                case 0x8a: // MAPPLACEMENTOFF <hash>
                    str = "MAPPLACEMENTOFF " + ValToString(line.Data4);

                    break;
                case 0x8b: // LOADHERO <hash>
                    str = "LOADHERO " + ValToString(line.Data4);

                    break;
                case 0x8c: // UNLOADHERO <hash>
                    str = "UNLOADHERO " + ValToString(line.Data4);

                    break;
                case 0x8d: // <var> = ISHEROLOADED <hash>
                    str = GetVarName(line.Data3) + " = ISHEROLOADED " + ValToString(line.Data4);

                    break;
                case 0x8e: // LOADFILE <hash>
                    str = "LOADFILE " + ValToString(line.Data4);

                    break;
                case 0x8f: // UNLOADFILE <hash>
                    str = "UNLOADFILE " + ValToString(line.Data4);

                    break;
                case 0x90: // <var> = ISFILELOADED <hash>
                    str = GetVarName(line.Data3) + " = ISFILELOADED " + ValToString(line.Data4);

                    break;
                case 0x91: // GAMETIMEOUT <value> (this does nothing)
                    str = "GAMETIMEOUT " + ValToString(line.Data4);

                    break;
                case 0x92: // HEROVISIBLE <value>
                    str = "HEROVISIBLE " + ValToString(line.Data4);

                    break;
                case 0x93: // SPARXVISIBLE <value>
                    str = "SPARXVISIBLE " + ValToString(line.Data4);

                    break;
                case 0x94: // GOTOMAP <hash>
                    str = "GOTOMAP " + ValToString(line.Data4);

                    break;
                case 0x95: // SPARXCHAT <hash>
                    str = "SPARXCHAT " + ValToString(line.Data4);

                    break;
                case 0x96: // MOVESPARXALONGPATH <hash>
                    str = "MOVESPARXALONGPATH " + ValToString(line.Data4);

                    break;
                case 0x97: // LETSGOSHOPPING
                    str = "LETSGOSHOPPING";

                    break;
                case 0x98: // MAPFADEPAUSE <value>
                    str = "MAPFADEPAUSE " + ValToString(line.Data4);

                    break;
                case 0x99: // LOADCUTSEQUENCEFILES <var/hash>
                    if ((line.Data4 & 0xFF000000) != 0)
                    {
                        val = ValToString(line.Data4);
                    }
                    else
                    {
                        val = GetVarName(line.Data4);
                    }

                    str = "LOADCUTSEQUENCEFILES " + val;

                    break;
                case 0x9a: // UNLOADCUTSEQUENCEFILES <var/hash>
                    if ((line.Data4 & 0xFF000000) != 0)
                    {
                        val = ValToString(line.Data4);
                    }
                    else
                    {
                        val = GetVarName(line.Data4);
                    }

                    str = "UNLOADCUTSEQUENCEFILES " + val;

                    break;
                case 0x9b: // <var1> = ARECUTSEQUENCEFILESLOADED <var2/hash>
                    if ((line.Data4 & 0xFF000000) != 0)
                    {
                        val = ValToString(line.Data4);
                    }
                    else
                    {
                        val = GetVarName(line.Data4);
                    }

                    str = GetVarName(line.Data3) + " = ARECUTSEQUENCEFILESLOADED " + val;

                    break;
                case 0x9c: // RESTARTMAP
                    str = "RESTARTMAP";

                    break;
                case 0x9d: // <var> = GETMAPSTATE
                    str = GetVarName(line.Data3) + " = GETMAPSTATE";

                    break;
                case 0x9e: // <var> = ISSAFETOTALK
                    str = GetVarName(line.Data3) + " = ISSAFETOTALK";

                    break;
                case 0x9f: // PROGRESSCOUNTER <value>
                    str = "PROGRESSCOUNTER " + ValToString(line.Data4);

                    break;
                case 0xa0: // SETPROGRESS <value>
                    str = "SETPROGRESS " + ValToString(line.Data4);

                    break;
                case 0xa1: // SETMAXPROGRESS <value>
                    str = "SETMAXPROGRESS " + ValToString(line.Data4);

                    break;
                case 0xa2: // INCPROGRESS
                    str = "INCPROGRESS";

                    break;
                case 0xa3: // DECPROGRESS
                    str = "DECPROGRESS";

                    break;
                case 0xa4: // <var> = GETPROGRESS
                    str = GetVarName(line.Data3) + " = GETPROGRESS";

                    break;
                case 0xa5: // GIVELOCKPICKSTOHERO <value>
                    str = "GIVELOCKPICKSTOHERO " + ValToString(line.Data4);

                    break;
                case 0xa6: // TAKELOCKPICKSFROMHERO <value>
                    str = "TAKELOCKPICKSFROMHERO " + ValToString(line.Data4);

                    break;
                case 0xa7: // <var> = HOWMANYLOCKPICKS
                    str = GetVarName(line.Data3) + " = HOWMANYLOCKPICKS";

                    break;
                case 0xa8: // <var> = ISTHEREANEWSHOPITEM
                    str = GetVarName(line.Data3) + " = ISTHEREANEWSHOPITEM";

                    break;
                case 0xa9: // CUTSEQUENCEWITHTRIGGERS <hash>
                    str = "CUTSEQUENCEWITHTRIGGERS " + ValToString(line.Data4);

                    break;
                case 0xaa: // RESETTIMER
                    str = "RESETTIMER";

                    break;
                case 0xab: // <var> = GETELAPSEDTIME
                    str = GetVarName(line.Data3) + " = GETELAPSEDTIME";

                    break;
                case 0xac: // <var> = HEROCHARACTER
                    str = GetVarName(line.Data3) + " = HEROCHARACTER";

                    break;
                case 0xad: // LOADMAP = <hash>
                    str = "LOADMAP " + ValToString(line.Data4);

                    break;
                case 0xae: // CLOSEMAP = <hash>
                    str = "CLOSEMAP " + ValToString(line.Data4);

                    break;
                case 0xaf: // <var> = ISMAPLOADED <hash>
                    str = GetVarName(line.Data3) + " = ISMAPLOADED " + ValToString(line.Data4);

                    break;
                case 0xb0: // FADEDOWN <value>
                    str = "FADEDOWN " + ValToString(line.Data4);

                    break;
                case 0xb1: // FADEUP <value>
                    str = "FADEUP " + ValToString(line.Data4);

                    break;
                case 0xb2: // PREVENTTALK <value>
                    str = "PREVENTTALK " + ValToString(line.Data4);

                    break;
                case 0xb3: // STALL (Does nothing)
                    str = "STALL";

                    break;
                case 0xb4: // <var> = HEROSTATUS
                    str = GetVarName(line.Data3) + " = HEROSTATUS";

                    break;
                default:
                    if (ShowUnknown)
                    {
                        str = string.Format("// Unknown Instruction: 0x{0:X}", line.InstructionID);
                    } else
                    {
                        str = string.Empty;
                    }
                    
                    break;
            }

            return str;
        }
    }
}
