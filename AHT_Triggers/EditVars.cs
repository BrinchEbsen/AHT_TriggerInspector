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
    public partial class EditVars : Form
    {
        private ScriptViewer ScriptViewerWnd;
        private GameScriptSaveInfo SaveInfo;

        public EditVars(ScriptViewer parent, GameScriptSaveInfo saveInfo)
        {
            InitializeComponent();
            ScriptViewerWnd = parent;
            this.SaveInfo = saveInfo;
        }

        private void EditVars_Load(object sender, EventArgs e)
        {
            PopulateLists();
        }

        private void PopulateLists()
        {
            DGV_Vars.Rows.Clear();
            DGV_Procs.Rows.Clear();
            DGV_Labels.Rows.Clear();

            DGV_Vars.Columns[0].Width   = DGV_Vars.Width   - 20;
            DGV_Procs.Columns[0].Width  = DGV_Procs.Width  - 20;
            DGV_Labels.Columns[0].Width = DGV_Labels.Width - 20;

            for (int i = 0; i < SaveInfo.NumVars(); i++)
            {
                DGV_Vars.Rows.Add(SaveInfo.GetVar(i));
            }
            for (int i = 0; i < SaveInfo.NumProcs(); i++)
            {
                DGV_Procs.Rows.Add(SaveInfo.GetProc(i));
            }
            foreach (KeyValuePair<int, string> entry in SaveInfo.Labels)
            {
                DGV_Labels.Rows.Add(entry.Value);
            }
        }

        private void SaveScriptInfo()
        {
            //Save line numbers for later
            List<int> lineNrs = new List<int>();
            foreach(KeyValuePair<int, string> entry in SaveInfo.Labels)
            {
                lineNrs.Add(entry.Key);
            }

            SaveInfo.Clear();

            foreach (DataGridViewRow row in DGV_Vars.Rows)
            {
                SaveInfo.AddVar(row.Cells[0].Value.ToString());
            }
            foreach(DataGridViewRow row in DGV_Procs.Rows)
            {
                SaveInfo.AddProc(row.Cells[0].Value.ToString());
            }
            int index = 0;
            foreach (DataGridViewRow row in DGV_Labels.Rows)
            {
                SaveInfo.AddLabel(lineNrs[index], row.Cells[0].Value.ToString());
                index++;
            }

            ScriptViewerWnd.SaveScriptInfo(SaveInfo);
            ScriptViewerWnd.InsertDecompiledCode();
        }

        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            SaveScriptInfo();
        }

        private void DGV_Vars_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SaveInfo.SetVar(e.RowIndex, DGV_Vars.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to reset the names of all variables, procedures and labels?",
                "Resetting saved gamescript names",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (res == DialogResult.Yes)
            {
                SaveInfo = ScriptViewerWnd.InitialiseSaveInfo(SaveInfo);
                ScriptViewerWnd.SaveScriptInfo(SaveInfo);
                ScriptViewerWnd.InsertDecompiledCode();
                PopulateLists();
            }
        }
    }
}
