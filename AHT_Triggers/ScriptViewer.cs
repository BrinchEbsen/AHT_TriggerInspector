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

        private Color defTextCol  = Color.FromArgb(101, 123, 131);
        private Color keywordCol  = Color.FromArgb(38, 139, 210);
        private Color hashcodeCol = Color.FromArgb(255, 0, 255);

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

            Highlight();
        }

        //https://learn.microsoft.com/en-us/answers/questions/530055/how-to-color-a-specific-string-s-in-richtextbox-te
        void Highlight()
        {
            const int WM_SETREDRAW = 0x000B;

            SendMessage(Txt_ScriptCode.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            var sel_start = Txt_ScriptCode.SelectionStart;
            var sel_len = Txt_ScriptCode.SelectionLength;

            Txt_ScriptCode.SelectAll();
            Txt_ScriptCode.SelectionColor = defTextCol;

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
                string s = Txt_ScriptCode.Text.Remove(0, p);

                //Get index of nearest space or newline, whichever is first
                int n = Math.Min(s.IndexOf(' '), s.IndexOf('\n'));
                if (n < 0) { //Not sure how this could happen
                    p += 1;
                    continue;
                }

                s = s.Remove(n);

                Txt_ScriptCode.SelectionStart = p;
                Txt_ScriptCode.SelectionLength = s.Length;
                Txt_ScriptCode.SelectionColor = hashcodeCol;

                p += n;
            }

            Txt_ScriptCode.SelectionStart = sel_start;
            Txt_ScriptCode.SelectionLength = sel_len;

            SendMessage(Txt_ScriptCode.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            Txt_ScriptCode.Invalidate();
        }

        [DllImport("user32")]
        private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}
