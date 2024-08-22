namespace AHT_Triggers
{
    partial class EditVars
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
            this.components = new System.ComponentModel.Container();
            this.Vars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGV_Vars = new System.Windows.Forms.DataGridView();
            this.Btn_Apply = new System.Windows.Forms.Button();
            this.Btn_Reset = new System.Windows.Forms.Button();
            this.DGV_Procs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGV_Labels = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Vars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Procs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Labels)).BeginInit();
            this.SuspendLayout();
            // 
            // Vars
            // 
            this.Vars.HeaderText = "Vars";
            this.Vars.Name = "Vars";
            // 
            // DGV_Vars
            // 
            this.DGV_Vars.AllowUserToAddRows = false;
            this.DGV_Vars.AllowUserToDeleteRows = false;
            this.DGV_Vars.AllowUserToResizeRows = false;
            this.DGV_Vars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV_Vars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Vars.ColumnHeadersVisible = false;
            this.DGV_Vars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Vars});
            this.DGV_Vars.Location = new System.Drawing.Point(12, 43);
            this.DGV_Vars.Name = "DGV_Vars";
            this.DGV_Vars.RowHeadersVisible = false;
            this.DGV_Vars.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_Vars.Size = new System.Drawing.Size(200, 717);
            this.DGV_Vars.TabIndex = 0;
            this.DGV_Vars.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Vars_CellValueChanged);
            // 
            // Btn_Apply
            // 
            this.Btn_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Apply.Location = new System.Drawing.Point(467, 766);
            this.Btn_Apply.Name = "Btn_Apply";
            this.Btn_Apply.Size = new System.Drawing.Size(75, 23);
            this.Btn_Apply.TabIndex = 1;
            this.Btn_Apply.Text = "Apply";
            this.ToolTips.SetToolTip(this.Btn_Apply, "Save the current names and re-decompile the script to make the changes take effec" +
        "t.");
            this.Btn_Apply.UseVisualStyleBackColor = true;
            this.Btn_Apply.Click += new System.EventHandler(this.Btn_Apply_Click);
            // 
            // Btn_Reset
            // 
            this.Btn_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Reset.Location = new System.Drawing.Point(548, 766);
            this.Btn_Reset.Name = "Btn_Reset";
            this.Btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.Btn_Reset.TabIndex = 2;
            this.Btn_Reset.Text = "Reset";
            this.ToolTips.SetToolTip(this.Btn_Reset, "Reset all variables, procedures and labels to their auto-generated names,\r\nand re" +
        "-decompile the script to make the changes take effect.");
            this.Btn_Reset.UseVisualStyleBackColor = true;
            this.Btn_Reset.Click += new System.EventHandler(this.Btn_Reset_Click);
            // 
            // DGV_Procs
            // 
            this.DGV_Procs.AllowUserToAddRows = false;
            this.DGV_Procs.AllowUserToDeleteRows = false;
            this.DGV_Procs.AllowUserToResizeRows = false;
            this.DGV_Procs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV_Procs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Procs.ColumnHeadersVisible = false;
            this.DGV_Procs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.DGV_Procs.Location = new System.Drawing.Point(218, 43);
            this.DGV_Procs.Name = "DGV_Procs";
            this.DGV_Procs.RowHeadersVisible = false;
            this.DGV_Procs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_Procs.Size = new System.Drawing.Size(200, 717);
            this.DGV_Procs.TabIndex = 3;
            this.DGV_Procs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Procs_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Vars";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // DGV_Labels
            // 
            this.DGV_Labels.AllowUserToAddRows = false;
            this.DGV_Labels.AllowUserToDeleteRows = false;
            this.DGV_Labels.AllowUserToResizeRows = false;
            this.DGV_Labels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV_Labels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Labels.ColumnHeadersVisible = false;
            this.DGV_Labels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.DGV_Labels.Location = new System.Drawing.Point(424, 43);
            this.DGV_Labels.Name = "DGV_Labels";
            this.DGV_Labels.RowHeadersVisible = false;
            this.DGV_Labels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_Labels.Size = new System.Drawing.Size(200, 717);
            this.DGV_Labels.TabIndex = 4;
            this.DGV_Labels.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Labels_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Vars";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Variables";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Procedures";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Labels";
            // 
            // EditVars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 801);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV_Labels);
            this.Controls.Add(this.DGV_Vars);
            this.Controls.Add(this.Btn_Reset);
            this.Controls.Add(this.Btn_Apply);
            this.Controls.Add(this.DGV_Procs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditVars";
            this.ShowIcon = false;
            this.Text = "Edit Names";
            this.Load += new System.EventHandler(this.EditVars_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Vars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Procs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Labels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Vars;
        private System.Windows.Forms.DataGridView DGV_Vars;
        private System.Windows.Forms.Button Btn_Apply;
        private System.Windows.Forms.Button Btn_Reset;
        private System.Windows.Forms.DataGridView DGV_Procs;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridView DGV_Labels;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip ToolTips;
    }
}