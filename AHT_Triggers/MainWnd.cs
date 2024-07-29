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
                    ListViewItem lwi = List_Maps.Items.Add(i.ToString());

                    lwi.SubItems.Add(string.Format("{0:X}", ViewingData[i].MapHash));
                    lwi.SubItems.Add(ViewingData[i].TriggerList.Count.ToString());
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

                    if (Check_OnlyScripted.Checked & !trig.HasScript())
                    {
                        continue;
                    }

                    ListViewItem item = List_Triggers.Items.Add(i.ToString());
                    item.SubItems.Add(trig.Type.ToString().Replace("HT_TriggerType_", ""));
                    if (trig.SubType != EXHashCode.HT_TriggerSubType_Undefined)
                    {
                        item.SubItems.Add(trig.SubType.ToString().Replace("HT_TriggerSubType_", ""));
                    } else
                    {
                        item.SubItems.Add("");
                    }

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

            string sIndex = List_Triggers.Items[List_Triggers.SelectedIndices[0]].Text;
            SelectedTrigger = int.Parse(sIndex);
            PopulateTriggerInfo();

            Trigger trig = ViewingData[SelectedMap].TriggerList[SelectedTrigger];
            foreach (ListViewItem lwi in List_Triggers.Items)
            {
                int index = int.Parse(lwi.Text);
                bool referenced = false;

                foreach (ushort l in trig.Links)
                {
                    if (index == l)
                    {
                        lwi.BackColor = Color.Beige;

                        referenced = true;
                    }
                }

                if (!referenced)
                {
                    lwi.BackColor = Color.White;
                }
            }
        }

        private void PopulateTriggerInfo()
        {
            Box_TriggerInfo.Visible = true;
            Trigger trig = ViewingData[SelectedMap].TriggerList[SelectedTrigger];

            Lbl_TriggerIndex.Text = "Trigger: " + SelectedTrigger;

            Lbl_TriggerType.Text    = trig.Type.ToString().Replace("HT_TriggerType_", "");
            Lbl_TriggerSubType.Text = trig.SubType.ToString().Replace("HT_TriggerSubType_", "");

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

            //A trigger has 16 slots for data, but they only use ones defined by the TrigFlags
            StringBuilder str = new StringBuilder();
            int j = 0; //index into trigger data list
            for (int i = 0; i<16; i++)
            {
                //Only add data if the trigger has it defined for this slot
                if (trig.HasData(i))
                {
                    uint dat = trig.Data[j];

                    //Test if it's a Hashcode
                    if (Enum.IsDefined(typeof(EXHashCode), dat) && (dat != 0))
                    {
                        str.AppendLine(((EXHashCode)dat).ToString());
                        j++;
                        continue;
                    }

                    //Test if it's a sound Hashcode
                    if (Enum.IsDefined(typeof(ESHashCode), dat) && (dat != 0))
                    {
                        str.AppendLine(((ESHashCode)dat).ToString());
                        j++;
                        continue;
                    }

                    //Test if it's (most likely) a float
                    int dat2 = (int)dat;
                    if ((dat2 < -99999) | (dat2 > 99999))
                    {
                        byte[] bytes = new byte[]{
                            (byte) (dat2 & 0x000000FF),
                            (byte)((dat2 & 0x0000FF00) >> 8 ),
                            (byte)((dat2 & 0x00FF0000) >> 16),
                            (byte)((dat2 & 0xFF000000) >> 24)
                        };

                        str.AppendLine(BitConverter.ToSingle(bytes, 0).ToString());
                        j++;
                        continue;
                    }

                    //Else assume it's an int
                    str.AppendLine(trig.Data[j].ToString());
                    j++;
                } else
                {
                    str.AppendLine();
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

                if (Enum.IsDefined(typeof(EXHashCode), trig.GfxHashRef))
                {
                    Lbl_GFXHash.Text = ((EXHashCode)trig.GfxHashRef).ToString();
                } else
                {
                    Lbl_GFXHash.Text = string.Format("0x{0:X}", trig.GfxHashRef);
                }
            } else
            {
                Lbl_GFXHashDesc.Visible = false;
                Lbl_GFXHash.Visible = false;
            }

            if (trig.HasGeoFileHashRef())
            {
                Lbl_GeoHashDesc.Visible = true;
                Lbl_GeoHash.Visible = true;

                if (Enum.IsDefined(typeof(EXHashCode), trig.GeoFileHashRef))
                {
                    Lbl_GeoHash.Text = ((EXHashCode)trig.GeoFileHashRef).ToString();
                }
                else
                {
                    Lbl_GeoHash.Text = string.Format("0x{0:X}", trig.GeoFileHashRef);
                }
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
                PicBox_TintColour.Visible = true;

                Lbl_Tint.Text = string.Format("{0:X} {1:X} {2:X} {3:X}",
                    trig.Tint.r, trig.Tint.g, trig.Tint.b, trig.Tint.a);

                PicBox_TintColour.BackColor = Color.FromArgb(
                    trig.Tint.r,
                    trig.Tint.g,
                    trig.Tint.b
                );
            }
            else
            {
                Lbl_TintDesc.Visible = false;
                Lbl_Tint.Visible = false;
                PicBox_TintColour.Visible = false;
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

        private void Check_OnlyScripted_CheckedChanged(object sender, EventArgs e)
        {
            PopulateTriggerList();
        }
    }
}
