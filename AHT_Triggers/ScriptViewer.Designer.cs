namespace AHT_Triggers
{
    partial class ScriptViewer
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
            this.Box_ScriptInfo = new System.Windows.Forms.GroupBox();
            this.Txt_ScriptCode = new System.Windows.Forms.RichTextBox();
            this.Lbl_LinesDesc = new System.Windows.Forms.Label();
            this.Lbl_GlobalsDesc = new System.Windows.Forms.Label();
            this.Lbl_LocalsDesc = new System.Windows.Forms.Label();
            this.Lbl_ProcsDesc = new System.Windows.Forms.Label();
            this.Lbl_Procs = new System.Windows.Forms.Label();
            this.Lbl_Locals = new System.Windows.Forms.Label();
            this.Lbl_Globals = new System.Windows.Forms.Label();
            this.Lbl_Lines = new System.Windows.Forms.Label();
            this.Lbl_Index = new System.Windows.Forms.Label();
            this.Lbl_IndexDesc = new System.Windows.Forms.Label();
            this.Box_ScriptInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // Box_ScriptInfo
            // 
            this.Box_ScriptInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Box_ScriptInfo.Controls.Add(this.Lbl_Index);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_IndexDesc);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_Procs);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_Locals);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_Globals);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_Lines);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_ProcsDesc);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_LocalsDesc);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_GlobalsDesc);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_LinesDesc);
            this.Box_ScriptInfo.Location = new System.Drawing.Point(4, 4);
            this.Box_ScriptInfo.Name = "Box_ScriptInfo";
            this.Box_ScriptInfo.Size = new System.Drawing.Size(565, 110);
            this.Box_ScriptInfo.TabIndex = 0;
            this.Box_ScriptInfo.TabStop = false;
            this.Box_ScriptInfo.Text = "GameScript Information";
            // 
            // Txt_ScriptCode
            // 
            this.Txt_ScriptCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_ScriptCode.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_ScriptCode.Location = new System.Drawing.Point(4, 120);
            this.Txt_ScriptCode.Name = "Txt_ScriptCode";
            this.Txt_ScriptCode.ReadOnly = true;
            this.Txt_ScriptCode.Size = new System.Drawing.Size(565, 492);
            this.Txt_ScriptCode.TabIndex = 1;
            this.Txt_ScriptCode.Text = "";
            // 
            // Lbl_LinesDesc
            // 
            this.Lbl_LinesDesc.AutoSize = true;
            this.Lbl_LinesDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_LinesDesc.Location = new System.Drawing.Point(8, 32);
            this.Lbl_LinesDesc.Name = "Lbl_LinesDesc";
            this.Lbl_LinesDesc.Size = new System.Drawing.Size(42, 16);
            this.Lbl_LinesDesc.TabIndex = 0;
            this.Lbl_LinesDesc.Text = "Lines:";
            // 
            // Lbl_GlobalsDesc
            // 
            this.Lbl_GlobalsDesc.AutoSize = true;
            this.Lbl_GlobalsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GlobalsDesc.Location = new System.Drawing.Point(8, 48);
            this.Lbl_GlobalsDesc.Name = "Lbl_GlobalsDesc";
            this.Lbl_GlobalsDesc.Size = new System.Drawing.Size(57, 16);
            this.Lbl_GlobalsDesc.TabIndex = 1;
            this.Lbl_GlobalsDesc.Text = "Globals:";
            // 
            // Lbl_LocalsDesc
            // 
            this.Lbl_LocalsDesc.AutoSize = true;
            this.Lbl_LocalsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_LocalsDesc.Location = new System.Drawing.Point(8, 64);
            this.Lbl_LocalsDesc.Name = "Lbl_LocalsDesc";
            this.Lbl_LocalsDesc.Size = new System.Drawing.Size(50, 16);
            this.Lbl_LocalsDesc.TabIndex = 2;
            this.Lbl_LocalsDesc.Text = "Locals:";
            // 
            // Lbl_ProcsDesc
            // 
            this.Lbl_ProcsDesc.AutoSize = true;
            this.Lbl_ProcsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ProcsDesc.Location = new System.Drawing.Point(8, 80);
            this.Lbl_ProcsDesc.Name = "Lbl_ProcsDesc";
            this.Lbl_ProcsDesc.Size = new System.Drawing.Size(80, 16);
            this.Lbl_ProcsDesc.TabIndex = 3;
            this.Lbl_ProcsDesc.Text = "Procedures:";
            // 
            // Lbl_Procs
            // 
            this.Lbl_Procs.AutoSize = true;
            this.Lbl_Procs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Procs.Location = new System.Drawing.Point(88, 80);
            this.Lbl_Procs.Name = "Lbl_Procs";
            this.Lbl_Procs.Size = new System.Drawing.Size(37, 16);
            this.Lbl_Procs.TabIndex = 7;
            this.Lbl_Procs.Text = "temp";
            // 
            // Lbl_Locals
            // 
            this.Lbl_Locals.AutoSize = true;
            this.Lbl_Locals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Locals.Location = new System.Drawing.Point(88, 64);
            this.Lbl_Locals.Name = "Lbl_Locals";
            this.Lbl_Locals.Size = new System.Drawing.Size(37, 16);
            this.Lbl_Locals.TabIndex = 6;
            this.Lbl_Locals.Text = "temp";
            // 
            // Lbl_Globals
            // 
            this.Lbl_Globals.AutoSize = true;
            this.Lbl_Globals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Globals.Location = new System.Drawing.Point(88, 48);
            this.Lbl_Globals.Name = "Lbl_Globals";
            this.Lbl_Globals.Size = new System.Drawing.Size(37, 16);
            this.Lbl_Globals.TabIndex = 5;
            this.Lbl_Globals.Text = "temp";
            // 
            // Lbl_Lines
            // 
            this.Lbl_Lines.AutoSize = true;
            this.Lbl_Lines.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Lines.Location = new System.Drawing.Point(88, 32);
            this.Lbl_Lines.Name = "Lbl_Lines";
            this.Lbl_Lines.Size = new System.Drawing.Size(37, 16);
            this.Lbl_Lines.TabIndex = 4;
            this.Lbl_Lines.Text = "temp";
            // 
            // Lbl_Index
            // 
            this.Lbl_Index.AutoSize = true;
            this.Lbl_Index.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Index.Location = new System.Drawing.Point(88, 16);
            this.Lbl_Index.Name = "Lbl_Index";
            this.Lbl_Index.Size = new System.Drawing.Size(37, 16);
            this.Lbl_Index.TabIndex = 9;
            this.Lbl_Index.Text = "temp";
            // 
            // Lbl_IndexDesc
            // 
            this.Lbl_IndexDesc.AutoSize = true;
            this.Lbl_IndexDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_IndexDesc.Location = new System.Drawing.Point(8, 16);
            this.Lbl_IndexDesc.Name = "Lbl_IndexDesc";
            this.Lbl_IndexDesc.Size = new System.Drawing.Size(42, 16);
            this.Lbl_IndexDesc.TabIndex = 8;
            this.Lbl_IndexDesc.Text = "Index:";
            // 
            // ScriptViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 616);
            this.Controls.Add(this.Txt_ScriptCode);
            this.Controls.Add(this.Box_ScriptInfo);
            this.Name = "ScriptViewer";
            this.Text = "ScriptViewer";
            this.Load += new System.EventHandler(this.ScriptViewer_Load);
            this.Box_ScriptInfo.ResumeLayout(false);
            this.Box_ScriptInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Box_ScriptInfo;
        private System.Windows.Forms.RichTextBox Txt_ScriptCode;
        private System.Windows.Forms.Label Lbl_GlobalsDesc;
        private System.Windows.Forms.Label Lbl_LinesDesc;
        private System.Windows.Forms.Label Lbl_LocalsDesc;
        private System.Windows.Forms.Label Lbl_ProcsDesc;
        private System.Windows.Forms.Label Lbl_Procs;
        private System.Windows.Forms.Label Lbl_Locals;
        private System.Windows.Forms.Label Lbl_Globals;
        private System.Windows.Forms.Label Lbl_Lines;
        private System.Windows.Forms.Label Lbl_Index;
        private System.Windows.Forms.Label Lbl_IndexDesc;
    }
}