using AHT_Triggers.Common;
using AHT_Triggers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHT_Triggers
{
    public partial class MainWnd : Form
    {
        List<MapTriggerData> ViewingData = null;
        int SelectedMap = -1;
        int SelectedTrigger = -1;

        public MainWnd()
        {
            InitializeComponent();
        }

        private void Btn_LoadFile_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "GeoFiles (*.edb)|*.edb";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    try
                    {
                        GamePlatform Platform = FileHandler.CheckPlatform(filePath);
                        ViewingData = FileHandler.ReadTriggerData(filePath, Platform);
                        SelectedMap = -1;
                        SelectedTrigger = -1;

                        PopulateMapList();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Error opening file",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PopulateMapList()
        {
            List_Maps.Items.Clear();
            List_Triggers.Items.Clear();
            Box_TriggerInfo.Visible = false;

            if (ViewingData != null)
            {
                for (int i = 0; i<ViewingData.Count; i++)
                {
                    List_Maps.Items.Add(i.ToString()).SubItems.Add(string.Format("{0:X}", ViewingData[i].MapHash));
                }
            }
        }

        private void List_Maps_DoubleClick(object sender, MouseEventArgs e)
        {
            //Get the first element of the selected items (there can only be one)
            SelectedMap = List_Maps.SelectedItems[0].Index;

            PopulateTriggerList();
        }

        private void PopulateTriggerList()
        {
            List_Triggers.Items.Clear();
            Box_TriggerInfo.Visible = false;

            if (ViewingData != null)
            {
                for (int i = 0; i < ViewingData[SelectedMap].TriggerList.Count; i++)
                {
                    Trigger trig = ViewingData[SelectedMap].TriggerList[i];

                    ListViewItem item = List_Triggers.Items.Add(i.ToString());
                    item.SubItems.Add(trig.Type.ToString().Replace("HT_TriggerType_", ""));
                    item.SubItems.Add(trig.SubType.ToString().Replace("HT_TriggerSubType_", ""));

                    item.SubItems.Add(
                        trig.HasScript() ? trig.ScriptIndex.ToString() : ""
                    );
                }
            }
        }

        private void List_Triggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (List_Triggers.SelectedIndices.Count == 0)
            { return; }

            SelectedTrigger = List_Triggers.SelectedIndices[0];
            PopulateTriggerInfo();
        }

        private void PopulateTriggerInfo()
        {
            Box_TriggerInfo.Visible = true;
            Trigger trig = ViewingData[SelectedMap].TriggerList[SelectedTrigger];

            Lbl_TriggerIndex.Text = "Trigger: " + SelectedTrigger;

            Lbl_TriggerType.Text = trig.Type.ToString();
            Lbl_TriggerSubType.Text = trig.SubType.ToString();

            Lbl_GameFlags.Text = string.Format("0x{0:X}", trig.GameFlags);
            Lbl_TrigFlags.Text = string.Format("0x{0:X}", trig.TrigFlags);

            Lbl_PosX.Text = string.Format("{0:0.00}", trig.Position.x);
            Lbl_PosY.Text = string.Format("{0:0.00}", trig.Position.y);
            Lbl_PosZ.Text = string.Format("{0:0.00}", trig.Position.z);

            Lbl_RotX.Text = string.Format("{0:0.00}", trig.Rotation.x);
            Lbl_RotY.Text = string.Format("{0:0.00}", trig.Rotation.y);
            Lbl_RotZ.Text = string.Format("{0:0.00}", trig.Rotation.z);

            Lbl_ScaleX.Text = string.Format("{0:0.00}", trig.Scale.x);
            Lbl_ScaleY.Text = string.Format("{0:0.00}", trig.Scale.y);
            Lbl_ScaleZ.Text = string.Format("{0:0.00}", trig.Scale.z);

            StringBuilder str = new StringBuilder();
            int j = 0;
            for (int i = 0; i<16; i++)
            {
                if (trig.HasData(i))
                {
                    str.Append(string.Format("0x{0:X}\n", trig.Data[j]));
                    j++;
                } else
                {
                    str.Append("\n");
                }
            }

            Lbl_TriggerData.Text = str.ToString();

            str.Clear();
            j = 0;
            for (int i = 0; i<8; i++)
            {
                if (trig.HasLink(i))
                {
                    str.Append(trig.Links[j].ToString() + "\n");
                    j++;
                } else
                {
                    str.Append("\n");
                }
            }

            Lbl_TriggerLinks.Text = str.ToString();

            if (trig.HasGfxHashRef())
            {
                Lbl_GFXHashDesc.Visible = true;
                Lbl_GFXHash.Visible = true;

                Lbl_GFXHash.Text = string.Format("{0:X}", trig.GfxHashRef);
            } else
            {
                Lbl_GFXHashDesc.Visible = false;
                Lbl_GFXHash.Visible = false;
            }

            if (trig.HasGeoFileHashRef())
            {
                Lbl_GeoHashDesc.Visible = true;
                Lbl_GeoHash.Visible = true;

                Lbl_GeoHash.Text = string.Format("{0:X}", trig.GeoFileHashRef);
            }
            else
            {
                Lbl_GeoHashDesc.Visible = false;
                Lbl_GeoHash.Visible = false;
            }

            if (trig.HasTint())
            {
                Lbl_TintDesc.Visible = true;
                Lbl_Tint.Visible = true;

                Lbl_Tint.Text = string.Format("{0:X} | {1:X} | {2:X} | {3:X}",
                    trig.Tint.r, trig.Tint.g, trig.Tint.b, trig.Tint.a);
            }
            else
            {
                Lbl_TintDesc.Visible = false;
                Lbl_Tint.Visible = false;
            }

            if (trig.HasScript())
            {
                Btn_ViewGameScript.Enabled = true;
            } else
            {
                Btn_ViewGameScript.Enabled = false;
            }
        }

        private void Btn_ViewGameScript_Click(object sender, EventArgs e)
        {
            Trigger trig = ViewingData[SelectedMap].TriggerList[SelectedTrigger];
            ScriptViewer scriptViewer = new ScriptViewer(trig);
            scriptViewer.StartPosition = FormStartPosition.CenterParent;
            scriptViewer.ShowDialog();
        }
    }
}
