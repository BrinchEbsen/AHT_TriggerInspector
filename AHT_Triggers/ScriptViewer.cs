using AHT_Triggers.Common;
using AHT_Triggers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHT_Triggers
{
    public partial class ScriptViewer : Form
    {
        private Trigger trigger;
        private ByteCodeDecompiler decomp;

        private Color keywordCol  = Color.FromArgb(38, 139, 210);
        private Color hashcodeCol = Color.FromArgb(255, 0, 255);
        private Color commentCol  = Color.FromArgb(0x85, 0x99, 0);

        public ScriptViewer(Trigger trigger)
        {
            this.trigger = trigger;
            InitializeComponent();
        }

        private void ScriptViewer_Load(object sender, EventArgs e)
        {
            GameScript script = trigger.Script;
            decomp = new ByteCodeDecompiler(script);

            Lbl_Index.Text   = trigger.ScriptIndex.ToString();
            Lbl_Lines.Text   = script.NumLines.ToString();
            Lbl_Globals.Text = script.NumGlobals.ToString();
            Lbl_Locals.Text  = (script.NumVars - script.NumGlobals).ToString();
            Lbl_Procs.Text   = script.NumProcs.ToString();

            Txt_ScriptCode.Text = decomp.ScriptToString();

            Txt_ByteCode.Text = decomp.BytecodeToString();

            Highlight();
        }

        //https://learn.microsoft.com/en-us/answers/questions/530055/how-to-color-a-specific-string-s-in-richtextbox-te
        void Highlight()
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
    }
}
