using AHT_Triggers.Common;
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
    public partial class MainWnd : Form
    {
        List<MapTriggerData> ViewingData = null;
        int SelectedMap = -1;
        int SelectedTrigger = -1;

        public MainWnd()
        {
            InitializeComponent();
        }

        private void btn_LoadFile_Click(object sender, EventArgs e)
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

        }
    }
}
