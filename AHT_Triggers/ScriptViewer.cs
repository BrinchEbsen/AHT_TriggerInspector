﻿using AHT_Triggers.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AHT_Triggers
{
    public partial class ScriptViewer : Form
    {
        //DLL import for better textbox manipulation
        [DllImport("user32")]
        private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private readonly Trigger trigger;
        private bool doHighLight;
        private bool hasShownErrors = false;
        private readonly string viewingFile;
        private readonly int selectedMap;
        private ByteCodeDecompiler decomp;
        private EditVars VarWnd;

        private static readonly Color COLOR_KEYWORD  = Color.FromArgb(38, 139, 210);
        private static readonly Color COLOR_HASHCODE = Color.FromArgb(255, 0, 255);
        private static readonly Color COLOR_COMMENT  = Color.FromArgb(0x85, 0x99, 0);
        private static readonly Color COLOR_ERROR    = Color.Red;

        public ScriptViewer(Trigger trigger, int selectedMap, string viewingFile, bool doHighLight)
        {
            InitializeComponent();

            this.trigger = trigger;
            this.doHighLight = doHighLight;
            Check_SyntaxHighlight.Checked = doHighLight;
            this.viewingFile = viewingFile;
            this.selectedMap = selectedMap;
        }

        private void ScriptViewer_Load(object sender, EventArgs e)
        {
            decomp = new ByteCodeDecompiler(trigger.Script);

            try
            {
                ScriptSaveInfoHandler.LoadInfoFromFile(viewingFile, selectedMap, trigger);
            } catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error loading edited names from file",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ScriptSaveInfoHandler.InitialiseSaveInfo(trigger.Script);
            }

            decomp.ShowUnknown = Check_ShowUnknown.Checked;
            decomp.SaveInfo = ScriptSaveInfoHandler.ActiveInfo;

            GameScript script = trigger.Script;

            Lbl_Index.Text   = trigger.ScriptIndex.ToString();
            Lbl_Lines.Text   = script.NumLines.ToString();
            Lbl_Globals.Text = script.NumGlobals.ToString();
            Lbl_Locals.Text  = (script.NumVars - script.NumGlobals).ToString();
            Lbl_Procs.Text   = script.NumProcs.ToString();

            InsertDecompiledCode();
        }

        private void CreateDecompErrorPopup(DecodeResult res)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("There were errors during decompilation:");
            sb.AppendLine();

            if ((res & DecodeResult.NegativeIndent) != 0)
            {
                sb.AppendLine(" - Negative indentation encountered.");
            }
            if ((res & DecodeResult.InvalidVar) != 0)
            {
                sb.AppendLine(" - Invalid variables referenced.");
            }
            if ((res & DecodeResult.InvalidProc) != 0)
            {
                sb.AppendLine(" - Invalid procedures referenced.");
            }

            sb.AppendLine();
            sb.AppendLine("There's either a bug in the decompiler or the gamescript's data is corrupted.");

            MessageBox.Show(sb.ToString(), "Decompiler Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void InsertDecompiledCode()
        {
            Txt_ScriptCode.Text = decomp.DecompileScript(out DecodeResult res);

            //If any errors occoured, report it in a pop-up
            if ((res != DecodeResult.Success) && !hasShownErrors)
            {
                CreateDecompErrorPopup(res);
                hasShownErrors = true;
            }

            Txt_ByteCode.Text = decomp.BytecodeToString();

            List_VTable.Columns[0].Width = List_VTable.Width - 30;
            List_VTable.Items.Clear();

            List<string> VTable = decomp.GetVTable();

            foreach(string proc in VTable)
            {
                var lwi = List_VTable.Items.Add(proc);

                if (lwi.Text.IndexOf("(none)") >= 0)
                {
                    lwi.ForeColor = Color.Gray;
                } else
                {
                    lwi.ForeColor = Color.Black;
                }
            }

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
                    Txt_ScriptCode.SelectionColor = COLOR_KEYWORD;
                     
                    p += 1;
                }
            }

            //Color error fields
            foreach (string s in Syntax.SYNTAX_ERRORFIELDS)
            {
                p = 0;
                while ((p = Txt_ScriptCode.Find(s, p, RichTextBoxFinds.MatchCase)) >= 0)
                {
                    Txt_ScriptCode.SelectionStart = p;
                    Txt_ScriptCode.SelectionLength = s.Length;
                    Txt_ScriptCode.SelectionColor = COLOR_ERROR;

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
                Txt_ScriptCode.SelectionColor = COLOR_HASHCODE;

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
                Txt_ScriptCode.SelectionColor = COLOR_COMMENT;

                p += n;
            }

            //Restore selection
            Txt_ScriptCode.SelectionStart = sel_start;
            Txt_ScriptCode.SelectionLength = sel_len;

            SendMessage(Txt_ScriptCode.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            Txt_ScriptCode.Invalidate();
        }

        private void Check_ShowUnknown_CheckedChanged(object sender, EventArgs e)
        {
            decomp.ShowUnknown = Check_ShowUnknown.Checked;
        }

        private void Btn_EditVarNames_Click(object sender, EventArgs e)
        {
            if (VarWnd == null)
            {
                CreateEditVarsWnd();
            } else if (VarWnd.IsDisposed)
            {
                CreateEditVarsWnd();
            } else
            {
                VarWnd.Focus();
            }
        }

        private void CreateEditVarsWnd()
        {
            VarWnd = new EditVars(this, viewingFile, selectedMap, trigger);
            VarWnd.StartPosition = FormStartPosition.CenterParent;
            VarWnd.Show();
        }

        //Close variable edit window if current window is closing
        private void ScriptViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            VarWnd?.Dispose();
        }

        private void Btn_ApplyChanges_Click(object sender, EventArgs e)
        {
            InsertDecompiledCode();
        }

        private void NUD_IndentationLevel_ValueChanged(object sender, EventArgs e)
        {
            decomp.IndentationAmount = (int)NUD_IndentationLevel.Value;
        }

        private void Check_SyntaxHighlight_CheckedChanged(object sender, EventArgs e)
        {
            doHighLight = Check_SyntaxHighlight.Checked;
        }
    }
}
