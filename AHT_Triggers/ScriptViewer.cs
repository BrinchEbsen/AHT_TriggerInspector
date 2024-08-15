using AHT_Triggers.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AHT_Triggers
{
    public partial class ScriptViewer : Form
    {
        private MainWnd MainWnd;
        private Trigger trigger;
        private bool doHighLight;
        private string viewingFile;
        private int selectedMap;
        private ByteCodeDecompiler decomp;
        private EditVars VarWnd;

        private GameScriptSaveInfo SaveInfo = new GameScriptSaveInfo();

        private Color keywordCol  = Color.FromArgb(38, 139, 210);
        private Color hashcodeCol = Color.FromArgb(255, 0, 255);
        private Color commentCol  = Color.FromArgb(0x85, 0x99, 0);

        //Constructor needs all the info to know what file to load
        public ScriptViewer(MainWnd parent, Trigger trigger, int selectedMap, string viewingFile, bool doHighLight)
        {
            this.MainWnd = parent;
            this.trigger = trigger;
            this.doHighLight = doHighLight;
            this.viewingFile = viewingFile;
            this.selectedMap = selectedMap;
            InitializeComponent();
        }
        
        private void LoadVarNames()
        {
            string filepath = GetVarNamesFilePath();

            //If the file doesn't exist already, create a new list
            if (!File.Exists(filepath))
            {
                InitialiseSaveInfo(SaveInfo);
                return;
            }

            SaveInfo.Clear();

            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    string line = reader.ReadLine();
                    int varIndex = 0;
                    int procIndex = 0;

                    while (line != null)
                    {
                        //We know we're done
                        //if (varIndex > trigger.Script.NumVars)
                        //{
                        //    break;
                        //}

                        if (line.IndexOf("VAR") == 0)
                        {
                            string n = line.Replace(string.Format("VAR #{0}: ", varIndex), "");
                            SaveInfo.AddVar(n);
                            varIndex++;
                        } else if (line.IndexOf("PROC") == 0)
                        {
                            string n = line.Replace(string.Format("PROC #{0}: ", procIndex), "");
                            SaveInfo.AddProc(n);
                            procIndex++;
                        } else if (line.IndexOf("LABEL") == 0)
                        {
                            int i1 = line.IndexOf("#");
                            int i2 = line.IndexOf(":");

                            string s_lineNr = line.Substring(i1+1, i2 - i1 - 1);
                            int lineNr = int.Parse(s_lineNr);

                            string n = line.Replace(string.Format("LABEL #{0}: ", lineNr), "");
                            SaveInfo.AddLabel(lineNr, n);
                        }

                        line = reader.ReadLine();
                    }
                }
            } catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error loading saved variable names",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public GameScriptSaveInfo InitialiseSaveInfo(GameScriptSaveInfo info)
        {
            info.Clear();

            for (int i = 0; i < trigger.Script.NumVars; i++)
            {
                info.AddVar(decomp.GenerateVarName(i));
            }

            for (int i = 0; i < trigger.Script.NumProcs; i++)
            {
                info.AddProc(decomp.GenerateProcName(i));
            }

            info.Labels = decomp.GenerateLabels();

            return info;
        }

        private string GetVarNamesFilePath()
        {
            //return "C:\\Users\\Ebbers\\Documents\\test.txt";

            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gs_vars");
            Console.WriteLine(folderpath);

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            string filename = string.Format("{0}_{1}_{2}.txt",
                viewingFile.Replace(".edb", ""),
                selectedMap,
                trigger.ScriptIndex);
            return Path.Combine(folderpath, filename);
        }

        private void ScriptViewer_Load(object sender, EventArgs e)
        {
            decomp = new ByteCodeDecompiler(trigger.Script);

            LoadVarNames();

            decomp.ShowUnknown = Check_ShowUnknown.Checked;
            decomp.SaveInfo = SaveInfo;

            GameScript script = trigger.Script;

            Lbl_Index.Text   = trigger.ScriptIndex.ToString();
            Lbl_Lines.Text   = script.NumLines.ToString();
            Lbl_Globals.Text = script.NumGlobals.ToString();
            Lbl_Locals.Text  = (script.NumVars - script.NumGlobals).ToString();
            Lbl_Procs.Text   = script.NumProcs.ToString();

            InsertDecompiledCode();
        }

        public void InsertDecompiledCode()
        {
            Txt_ScriptCode.Text = decomp.DecompileScript(out DecodeResult res);

            if (res == DecodeResult.NegativeIndent)
            {
                MessageBox.Show("The decompiler enountered negative indentation," +
                    " which either means there's a bug in the decompiler, or the gamescript's data is corrupted.",
                    "Decompiler Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Txt_ByteCode.Text = decomp.BytecodeToString();

            if (doHighLight)
                Highlight_ScriptCode();
        }

        //Add syntax highlighting to the scriptcode in the textbox.
        //Searches for key words and hashcodes and colors them appropriately.
        //Colors are chosen to be the same as the colors used in the original Euroland script editor.
        //https://learn.microsoft.com/en-us/answers/questions/530055/how-to-color-a-specific-string-s-in-richtextbox-te
        void Highlight_ScriptCode()
        {
            const int WM_SETREDRAW = 0x000B;

            //Tell the textbox to redraw
            SendMessage(Txt_ScriptCode.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            //Store selection
            var sel_start = Txt_ScriptCode.SelectionStart;
            var sel_len = Txt_ScriptCode.SelectionLength;

            int p;

            //Color keywords
            foreach (string s in Syntax.SYNTAX_KEYWORDS)
            {
                p = 0;
                while ((p = Txt_ScriptCode.Find(s, p, RichTextBoxFinds.MatchCase)) >= 0)
                {
                    Txt_ScriptCode.SelectionStart = p;
                    Txt_ScriptCode.SelectionLength = s.Length;
                    Txt_ScriptCode.SelectionColor = keywordCol;
                     
                    p += 1;
                }
            }

            //Color hashcodes
            p = 0;
            while ((p = Txt_ScriptCode.Find("HT_", p, RichTextBoxFinds.None)) >= 0)
            {
                //Remove everything before the hashcode
                string s = Txt_ScriptCode.Text.Remove(0, p);

                //Get index of nearest space or newline, whichever is first
                //This marks the end of the hashcode
                int iNextSpace  = s.IndexOf(' ');
                int iNextLineBr = s.IndexOf("\n");

                int n;
                if (iNextLineBr < 0)
                {
                    n = iNextSpace;
                } else if (iNextSpace < 0)
                {
                    n = iNextLineBr;
                } else
                {
                    n = Math.Min(iNextSpace, iNextLineBr);
                }

                //This shouldn't happen, but just in case
                if (n < 0) {
                    p += 1;
                    continue;
                }

                Txt_ScriptCode.SelectionStart = p;
                Txt_ScriptCode.SelectionLength = n;
                Txt_ScriptCode.SelectionColor = hashcodeCol;

                p += n;
            }

            //Color comments
            p = 0;
            while ((p = Txt_ScriptCode.Find("//", p, RichTextBoxFinds.None)) >= 0)
            {
                //Remove everything before the comment
                string s = Txt_ScriptCode.Text.Remove(0, p);

                //Get index of nearest newline
                //This marks the end of the comment
                int n = s.IndexOf("\n");
                
                //If none exist, just use the end of the text
                if (n < 0)
                {
                    n = sel_len;
                }

                Txt_ScriptCode.SelectionStart = p;
                Txt_ScriptCode.SelectionLength = n;
                Txt_ScriptCode.SelectionColor = commentCol;

                p += n;
            }

            //Restore selection
            Txt_ScriptCode.SelectionStart = sel_start;
            Txt_ScriptCode.SelectionLength = sel_len;

            SendMessage(Txt_ScriptCode.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            Txt_ScriptCode.Invalidate();
        }

        [DllImport("user32")]
        private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void Check_ShowUnknown_CheckedChanged(object sender, EventArgs e)
        {
            decomp.ShowUnknown = Check_ShowUnknown.Checked;

            InsertDecompiledCode();
        }

        private void Btn_EditVarNames_Click(object sender, EventArgs e)
        {
            if (VarWnd == null)
            {
                CreateEditVarsWnd();
            } else if (VarWnd.IsDisposed)
            {
                CreateEditVarsWnd();
            }
        }

        private void CreateEditVarsWnd()
        {
            VarWnd = new EditVars(this, SaveInfo);
            VarWnd.StartPosition = FormStartPosition.CenterParent;
            VarWnd.Show();
        }

        //Close variable edit window if current window is closing
        private void ScriptViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            VarWnd?.Dispose();
        }

        public void SaveScriptInfo(GameScriptSaveInfo newInfo)
        {
            SaveInfo = newInfo;

            string filepath = GetVarNamesFilePath();

            try
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                {
                    for (int i = 0; i < SaveInfo.NumVars(); i++)
                    {
                        writer.WriteLine(string.Format("VAR #{0}: ", i) + SaveInfo.GetVar(i));
                    }
                    for (int i = 0; i < SaveInfo.NumProcs(); i++)
                    {
                        writer.WriteLine(string.Format("PROC #{0}: ", i) + SaveInfo.GetProc(i));
                    }
                    foreach (KeyValuePair<int, string> entry in SaveInfo.Labels)
                    {
                        writer.WriteLine(string.Format("LABEL #{0}: ", entry.Key) + entry.Value);
                    }

                    writer.Flush();
                }
            } catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error saving variable names",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
