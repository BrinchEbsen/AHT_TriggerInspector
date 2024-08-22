using AHT_Triggers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHT_Triggers
{
    public partial class EditVars : Form
    {
        private readonly ScriptViewer ScriptViewerWnd;
        private readonly string viewingFile;
        private readonly int selectedMap;
        private readonly Trigger trigger;

        //Lists to keep track of the last value in any given cell of the gridviews, so it can be reverted
        private readonly List<string> savedVarNames   = new List<string>();
        private readonly List<string> savedProcNames  = new List<string>();
        private readonly List<string> savedLabelNames = new List<string>();

        public EditVars(ScriptViewer parent, string viewingFile, int selectedMap, Trigger trigger)
        {
            InitializeComponent();
            ScriptViewerWnd = parent;
            this.viewingFile = viewingFile;
            this.selectedMap = selectedMap;
            this.trigger = trigger;
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

            GameScriptSaveInfo Info = ScriptSaveInfoHandler.ActiveInfo;

            for (int i = 0; i < Info.NumVars(); i++)
            {
                int cellNr = DGV_Vars.Rows.Add(Info.GetVar(i));
                var cell = DGV_Vars.Rows[cellNr].Cells[0];

                if (i < 24)
                {
                    cell.Style.BackColor = Color.LightGray;
                }

                savedVarNames.Add(Info.GetVar(i));
            }
            for (int i = 0; i < Info.NumProcs(); i++)
            {
                DGV_Procs.Rows.Add(Info.GetProc(i));

                savedProcNames.Add(Info.GetProc(i));
            }
            foreach (KeyValuePair<int, string> entry in Info.Labels)
            {
                DGV_Labels.Rows.Add(entry.Value);

                savedLabelNames.Add(entry.Value);
            }
        }

        private void SaveScriptInfo()
        {
            GameScriptSaveInfo Info = ScriptSaveInfoHandler.ActiveInfo;

            //Save line numbers for later
            List<int> lineNrs = new List<int>();
            foreach(KeyValuePair<int, string> entry in Info.Labels)
            {
                lineNrs.Add(entry.Key);
            }

            Info.Clear();

            foreach (DataGridViewRow row in DGV_Vars.Rows)
            {
                Info.AddVar(row.Cells[0].Value.ToString());
            }
            foreach(DataGridViewRow row in DGV_Procs.Rows)
            {
                Info.AddProc(row.Cells[0].Value.ToString());
            }
            int index = 0;
            foreach (DataGridViewRow row in DGV_Labels.Rows)
            {
                Info.AddLabel(lineNrs[index], row.Cells[0].Value.ToString());
                index++;
            }

            try
            {
                ScriptSaveInfoHandler.SaveInfoToFile(viewingFile, selectedMap, trigger);
            } catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error saving names to file",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            ScriptViewerWnd.InsertDecompiledCode();
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
                "Are you sure you want to reset the names of all variables, procedures and labels to their auto-generated versions?",
                "Resetting saved gamescript names",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (res == DialogResult.Yes)
            {
                ScriptSaveInfoHandler.InitialiseSaveInfo(trigger.Script);

                try
                {
                    ScriptSaveInfoHandler.SaveInfoToFile(viewingFile, selectedMap, trigger);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error saving names to file",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ScriptViewerWnd.InsertDecompiledCode();
                PopulateLists();
            }
        }

        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            SaveScriptInfo();
        }

        private void DGV_Vars_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewCell cell = DGV_Vars.Rows[e.RowIndex].Cells[0];

            //Check if using reserved syntax
            foreach (string s in Syntax.SYNTAX_KEYWORDS)
            {
                if (((string)cell.Value).IndexOf(s) >= 0)
                {
                    MessageBox.Show("A variable cannot contain reserved keyword " + s + ".", "Invalid name",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cell.Value = savedVarNames[e.RowIndex];
                    return;
                }
            }
            //Check if using hashcode naming
            if (((string)cell.Value).IndexOf("HT_") >= 0)
            {
                MessageBox.Show("A variable cannot contain \"HT_\", as this marks the start of a hashcode.", "Invalid name",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                cell.Value = savedVarNames[e.RowIndex];
                return;
            }

            //Else overwrite our saved value
            savedVarNames[e.RowIndex] = (string)cell.Value;
        }

        private void DGV_Procs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewCell cell = DGV_Procs.Rows[e.RowIndex].Cells[0];

            //Check if using reserved syntax
            foreach (string s in Syntax.SYNTAX_KEYWORDS)
            {
                if (((string)cell.Value).IndexOf(s) >= 0)
                {
                    MessageBox.Show("A procedure cannot contain reserved keyword " + s + ".", "Invalid name",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cell.Value = savedProcNames[e.RowIndex];
                    return;
                }
            }
            //Check if using hashcode naming
            if (((string)cell.Value).IndexOf("HT_") >= 0)
            {
                MessageBox.Show("A procedure cannot contain \"HT_\", as this marks the start of a hashcode.", "Invalid name",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                cell.Value = savedProcNames[e.RowIndex];
                return;
            }

            //Else overwrite our saved value
            savedProcNames[e.RowIndex] = (string)cell.Value;
        }

        private void DGV_Labels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewCell cell = DGV_Labels.Rows[e.RowIndex].Cells[0];

            //Check if using reserved syntax
            foreach (string s in Syntax.SYNTAX_KEYWORDS)
            {
                if (((string)cell.Value).IndexOf(s) >= 0)
                {
                    MessageBox.Show("A label cannot contain reserved keyword " + s + ".", "Invalid name",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cell.Value = savedLabelNames[e.RowIndex];
                    return;
                }
            }
            //Check if using hashcode naming
            if (((string)cell.Value).IndexOf("HT_") >= 0)
            {
                MessageBox.Show("A label cannot contain \"HT_\", as this marks the start of a hashcode.", "Invalid name",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                cell.Value = savedLabelNames[e.RowIndex];
                return;
            }

            //Else overwrite our saved value
            savedLabelNames[e.RowIndex] = (string)cell.Value;
        }
    }
}
