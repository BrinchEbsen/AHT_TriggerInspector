using AHT_Triggers.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    internal struct LocalVar
    {
        public int index;
        public int proc;
    }
    public enum DecodeResult
    {
        Success,
        NegativeIndent
    }

    internal class ByteCodeDecompiler
    {
        public GameScript Script { get; set; }
        public GameScriptSaveInfo SaveInfo { get; set; }
        public bool ShowUnknown { get; set; }

        //Amount of space before the line of code that is deciphered
        private int Indentation = 0;

        //How many spaces are added for each indentation level
        private static readonly int INDENT_LEVEL = 2;

        //Lists to keep track of information regarding variables during the script decompile
        private List<LocalVar> TrackedLocals;
        private List<int>      TrackedGlobals;
        private bool TrackVars = false;

        //Key is line number, value is name
        private Dictionary<int, string> ProcHeaders  = new Dictionary<int, string>();
        private Dictionary<int, string> LabelHeaders = new Dictionary<int, string>();

        private int CurrentProc;

        //How the indentation changes before and after a line of code
        private enum IndentState
        {
            None,
            IncAfter,
            DecBefore,
            DecBeforeIncAfter,
            IncTwiceAfter,
            DecTwiceBefore
        }

        private IndentState indentState = IndentState.None;

        private static readonly Dictionary<int, string> LVAR_NAMES = new Dictionary<int, string>
        {
            [5]  = "YESNO",
            [7]  = "MESSAGE",
            [15] = "RETURN0"
        };

        public ByteCodeDecompiler(GameScript script)
        {
            this.Script = script;
        }

        public List<string> GetVTable()
        {
            List<string> list = new List<string>();

            int i = 0;
            foreach (byte b in Script.VTable)
            {
                string index = (i + " - ").PadLeft(5, ' ');

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

        //Return the name of the procedure at the given index into the ProcTable.
        //Adds the index to the end of the name, if another procedure with the same name exists.
        public string GenerateProcName(int proc)
        {
            if (Script.Procedures.Count == 0)
            {
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

        public string GetProcName(int proc)
        {
            if (SaveInfo != null)
            {
                return SaveInfo.GetProc(proc);
            }

            return GenerateProcName(proc);
        }

        public Dictionary<int, string> GenerateLabels()
        {
            Dictionary<int, string> labels = new Dictionary<int, string>();
            int index = 0;

            for (int i = 0; i < Script.NumLines; i++)
            {
                if (Script.Code[i].InstructionID == 0x14)
                {
                    int lineNum = Script.Code[i].Data4;

                    if (!labels.ContainsKey(lineNum))
                    {
                        labels.Add(lineNum, "lbl_" + index.ToString());
                        index++;
                    }
                }
            }

            return labels;
        }

        public string GetLabel(int lineNr)
        {
            return SaveInfo.Labels[lineNr];
        }

        //Create a compare statement between two variables/values with a given operator ID
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

        //Create a math statement between two variables/values with a given operator ID
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

        //Store information about used variables for later
        private void TrackVar(int i)
        {
            if (i < Script.NumGlobals)
            {
                if (i > 23) //There are 24 language-defined globals that are always present
                {
                    if (TrackedGlobals.Contains(i)) { return; }
                    TrackedGlobals.Add(i);
                }
            }
            else
            {
                foreach (LocalVar v in TrackedLocals)
                {
                    if (v.index == i)
                    {
                        return;
                    }
                }

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
        /// Create a name for a variable at the given index
        /// </summary>
        /// <param name="i">Variable index</param>
        /// <returns>Name</returns>
        public string GenerateVarName(int i)
        {
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
                if (LVAR_NAMES.ContainsKey(i))
                {
                    return LVAR_NAMES[i];
                }
                return "LVAR" + i;
            }
        }

        private string GetVarName(int i)
        {
            if (TrackVars) { TrackVar(i); }

            if (SaveInfo != null)
            {
                return SaveInfo.GetVar(i);
            }

            return GenerateVarName(i);
        }

        /// <summary>
        /// Turn a value into a string representation, or a hashcode if one exists for it.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>String representation of the value, or the hashcode if one exists for it.</returns>
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

        private string AddIndentToCommand(string s)
        {
            if (Indentation < 0)
            {
                return s;
            } else
            {
                return new string(' ', Indentation * INDENT_LEVEL) + s;
            }
        }

        /// <summary>
        /// Add the indentation to a code line, and increase/decrease accordingly
        /// </summary>
        /// <param name="instr">String representing the line of code</param>
        /// <returns>Indented line of code</returns>
        private string AddIndent(string instr, out bool becameNegative)
        {
            string str;

            switch (indentState)
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
                    str = "INVALID INDENTATION";
                    break;
            }

            if (Indentation < 0)
            {
                Indentation = 0;
                becameNegative = true;
            } else
            {
                becameNegative = false;
            }

            indentState = IndentState.None;

            return str;
        }

        //Take the whole gamescript and turn it into a string representing the interpreted source code

        /// <summary>
        /// Decompile the gamescript.
        /// </summary>
        /// <returns>Decompiled gamescript contained in a string.</returns>
        public string DecompileScript(out DecodeResult res)
        {
            //Default result
            res = DecodeResult.Success;

            //Start lists of procedures and labels
            ProcHeaders.Clear();
            LabelHeaders = GenerateLabels();

            //Reset indentation
            Indentation = 0;

            //Reset current procedure
            CurrentProc = -1;

            //Start tracking variables
            TrackVars = true;
            TrackedGlobals = new List<int>();
            TrackedLocals = new List<LocalVar>();

            //Create list of strings
            List<string> codeStr = new List<string>();

            //First pass:
            //Add a string to the list for each line, keep note of labels and procedures as we encounter them
            for (int i = 0; i < Script.NumLines; i++)
            {
                //Check for start of procedure
                for (int j = 0; j < Script.Procedures.Count; j++)
                {
                    Procedure p = Script.Procedures[j];

                    if (p.StartLine == i)
                    {
                        string s = GetProcName(j);
                        if (p.Exclusive)
                        {
                            s += " exclusive";
                        }

                        //Add indentation after start of procedure
                        Indentation = 1;
                        ProcHeaders.Add(i, s);

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
                    res = DecodeResult.NegativeIndent;
                }

                //Finally, add it to the list of strings
                codeStr.Add(str);
            }

            TrackVars = false;

            //Second pass:
            //Add procedure headers, labels, variable declarations and relocate "AND" statements
            //Keep track of how far we've drifted using a variable, so we don't add a line to the wrong place
            int offs = 0;

            //Add list of global declarations at the top
            if (Script.NumGlobals > 0)
            {
                for (int i = 24; i < Script.NumGlobals; i++)
                {
                    string str = "INT " + GetVarName(i);

                    //If we never encountered the global, then we mark it as unused.
                    if (!TrackedGlobals.Contains(i))
                    {
                        str += " //Unused global";
                    }

                    codeStr.Insert(offs, str);
                    offs++;
                }
            }

            //Add info about unused locals, if any exist
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
            }

            //Keep track of which procedures we've added
            CurrentProc = -1;

            for (int i = 0; i < Script.NumLines; i++)
            {
                //Check if a list contains a value at a given line of code (+ the drift), and insert the name if so.

                if (ProcHeaders.ContainsKey(i))
                {
                    CurrentProc++;

                    codeStr.Insert(i + offs, "");
                    offs++;
                    codeStr.Insert(i + offs, "DEFPROC " + ProcHeaders[i]);
                    offs++;

                    //Add locals for the current procedure at the top
                    if (TrackedLocals.Count > 0)
                    {
                        //We only add an empty line if we actually put any locals there
                        bool addNewLine = false;

                        for (int j = 0;  j < TrackedLocals.Count; j++)
                        {
                            //Only add locals part of the current procedure
                            if (TrackedLocals[j].proc == CurrentProc)
                            {
                                codeStr.Insert(i + offs, new string(' ', INDENT_LEVEL) + "INT " + GetVarName(TrackedLocals[j].index));
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

            //Third pass:
            //Fill in all lines with a label tag (formatted as "#L[line number]") with the label name instead
            for (int i = 0; i < codeStr.Count; i++)
            {
                string s;

                //Find the index of the tag
                int n = codeStr[i].IndexOf("#L");
                if (n < 0) { continue; } //If one doesn't exist, continue

                //Remove everything up to the number
                s = codeStr[i].Remove(0, n + 2);

                //Check if anything after the number exists, remove if there is
                n = s.IndexOf(' ');
                if (n >= 0) {
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

                string lblName;
                if (SaveInfo != null)
                {
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

            return sb.ToString();
        }

        /// <summary>
        /// Create a list of instructions from the bytecode.
        /// </summary>
        /// <returns>String containing the list of instructions</returns>
        public string BytecodeToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Script.NumLines; i++)
            {
                CodeLine line = Script.Code[i];

                foreach (Procedure proc in Script.Procedures)
                {
                    if (proc.StartLine == i)
                        sb.AppendLine("  DEFPROC " + proc.Name);
                }

                string str =
                    i.ToString().PadRight(4, ' ') +
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
        /// Turn an instruction into a string representation of the line of code
        /// </summary>
        /// <param name="line">Line of code to decipher</param>
        /// <returns>Deciphered line of code</returns>
        public string DecipherCommand(CodeLine line, int lineNr)
        {
            indentState = IndentState.None;

            string str;
            string val;

            switch(line.InstructionID)
            {
                //GENERIC
                case 0x2: // IF <var1> <operator> <var2>
                    indentState = IndentState.IncAfter;
                    str = "IF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            GetVarName(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x3: // IF <var> <operator> <value>
                    indentState = IndentState.IncAfter;
                    str = "IF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x4: // AND <var1> <operator> <var2>
                    indentState = IndentState.DecBeforeIncAfter;
                    str = " AND " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            GetVarName(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x5: // AND <var> <operator> <value>
                    indentState = IndentState.DecBeforeIncAfter;
                    str = " AND " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x6: // ELSE
                    indentState = IndentState.DecBeforeIncAfter;
                    str = "ELSE";

                    break;
                case 0x7: // ELSEIF <var1> <operator> <var2>
                    indentState = IndentState.DecBeforeIncAfter;
                    str = "ELSEIF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            GetVarName(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x8: // ELSEIF <var> <operator> <value>
                    indentState = IndentState.DecBeforeIncAfter;
                    str = "ELSEIF " + CreateCompareStatement(
                            GetVarName(line.Data2),
                            ValToString(line.Data4),
                            line.Data3
                        );

                    break;
                case 0x9: // ENDIF
                    indentState = IndentState.DecBefore;
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

                    indentState = IndentState.IncAfter;
                    str = "REPEAT " + val;

                    break;
                case 0xc: // ENDREPEAT
                    indentState = IndentState.DecBefore;
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
                    indentState = IndentState.IncAfter;
                    str = "WHILE " + CreateCompareStatement(GetVarName(line.Data2), ValToString(line.Data4), line.Data3);

                    break;
                case 0x1b: // ENDWHILE
                    indentState = IndentState.DecBefore;
                    str = "ENDWHILE";

                    break;
                case 0x1e: // ENDPROC <optional var/value>
                    indentState = IndentState.None;

                    //Change indentation if another procedure starts right after
                    foreach (Procedure proc in Script.Procedures)
                    {
                        if (proc.StartLine == lineNr+1)
                        {
                            indentState = IndentState.DecBefore;
                            break;
                        }
                    }
                    //Change indentation if its the last command of the gamescript
                    if (lineNr == Script.NumLines-1)
                    {
                        indentState = IndentState.DecBefore;
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
                    str = GetVarName(line.Data3) + " = CALLPROC " + GetProcName(line.Data1);

                    break;
                case 0x21: // <var> = INSTANCE <procedure>
                    str = GetVarName(line.Data3) + " = INSTANCE " + GetProcName(line.Data1);

                    break;
                case 0x22: // BREAK
                    str = "BREAK";

                    break;
                case 0x24: // CASE <value/hash>
                    indentState = IndentState.DecBeforeIncAfter;
                    str = "CASE "+ValToString(line.Data4);

                    break;
                case 0x25: // SWITCH <var>
                    indentState = IndentState.IncTwiceAfter;
                    str = "SWITCH "+GetVarName(line.Data2);

                    break;
                case 0x27: // ENDSWITCH
                    indentState = IndentState.DecTwiceBefore;
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

                //GAME-SPECIFIC
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
                case 0x50: // <var1> = GETDISTANCETOPATHNODE <value>
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
                case 0x56: // CUTSEQUENCE <value/hash>
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
                case 0x59: // CAMERAMOVESCRIPTPATH <hash>
                    str = "CAMERAMOVESCRIPTPATH " + ValToString(line.Data4);

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
                case 0x63: // SENDVALUE <ref> <value>
                    str = "SENDVALUE " + ValToString(line.Data4) + " " + ValToString(line.Data1);

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
                case 0x72: // HEROTOPATH <value/hash?>
                    str = "HEROTOPATH " + ValToString(line.Data4);

                    break;
                case 0x73: // ENABLEENTITY <hash>
                    str = "ENABLEENTITY " + ValToString(line.Data4);

                    break;
                case 0x74: // DISABLEENTITY <hash>
                    str = "DISABLEENTITY " + ValToString(line.Data4);

                    break;
                case 0x75: // CHANGEHERO <value/hash?>
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
                case 0x84: // ZOOPOO <hash>
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
                case 0x99: // LOADCUTSEQUENCEFILES <value/hash>
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
                case 0x9a: // UNLOADCUTSEQUENCEFILES <value/hash>
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
                case 0x9b: // <var> = ARECUTSEQUENCEFILESLOADED <value/hash>
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
                case 0xa2: // INCPROGRESS <value>
                    str = "INCPROGRESS " + ValToString(line.Data4);

                    break;
                case 0xa3: // DECPROGRESS <value>
                    str = "DECPROGRESS " + ValToString(line.Data4);

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
                case 0xa9: // TRIGGERCUTSEQUENCE
                    str = "TRIGGERCUTSEQUENCE";

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
