namespace AHT_Triggers
{
    partial class MainWnd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btn_LoadFile = new System.Windows.Forms.ToolStripButton();
            this.Lbl_LoadFile = new System.Windows.Forms.ToolStripLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.List_Maps = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.List_Triggers = new System.Windows.Forms.ListView();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_LoadFile,
            this.Lbl_LoadFile});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1122, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btn_LoadFile
            // 
            this.btn_LoadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_LoadFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_LoadFile.Image")));
            this.btn_LoadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_LoadFile.Name = "btn_LoadFile";
            this.btn_LoadFile.Size = new System.Drawing.Size(23, 22);
            this.btn_LoadFile.Text = "toolStripButton1";
            this.btn_LoadFile.Click += new System.EventHandler(this.btn_LoadFile_Click);
            // 
            // Lbl_LoadFile
            // 
            this.Lbl_LoadFile.Name = "Lbl_LoadFile";
            this.Lbl_LoadFile.Size = new System.Drawing.Size(54, 22);
            this.Lbl_LoadFile.Text = "Load File";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.List_Maps);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 140);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map";
            // 
            // List_Maps
            // 
            this.List_Maps.HideSelection = false;
            this.List_Maps.Location = new System.Drawing.Point(7, 20);
            this.List_Maps.Name = "List_Maps";
            this.List_Maps.Size = new System.Drawing.Size(188, 114);
            this.List_Maps.TabIndex = 0;
            this.List_Maps.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.List_Triggers);
            this.groupBox2.Location = new System.Drawing.Point(12, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 492);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Triggers";
            // 
            // List_Triggers
            // 
            this.List_Triggers.HideSelection = false;
            this.List_Triggers.Location = new System.Drawing.Point(7, 20);
            this.List_Triggers.Name = "List_Triggers";
            this.List_Triggers.Size = new System.Drawing.Size(188, 466);
            this.List_Triggers.TabIndex = 0;
            this.List_Triggers.UseCompatibleStateImageBehavior = false;
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 679);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainWnd";
            this.Text = "Form1";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btn_LoadFile;
        private System.Windows.Forms.ToolStripLabel Lbl_LoadFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView List_Maps;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView List_Triggers;
    }
}

