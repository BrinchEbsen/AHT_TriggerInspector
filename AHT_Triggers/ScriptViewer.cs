using AHT_Triggers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHT_Triggers
{
    public partial class ScriptViewer : Form
    {
        private Trigger trigger;
        private ByteCodeDecompiler decomp;

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
        }
    }
}
