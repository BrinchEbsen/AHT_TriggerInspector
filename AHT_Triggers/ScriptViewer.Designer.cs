﻿namespace AHT_Triggers
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
            this.components = new System.ComponentModel.Container();
            this.Box_ScriptInfo = new System.Windows.Forms.GroupBox();
            this.Check_SyntaxHighlight = new System.Windows.Forms.CheckBox();
            this.Lbl_IndentationLevel = new System.Windows.Forms.Label();
            this.Btn_ApplyChanges = new System.Windows.Forms.Button();
            this.NUD_IndentationLevel = new System.Windows.Forms.NumericUpDown();
            this.Lbl_VTable = new System.Windows.Forms.Label();
            this.List_VTable = new System.Windows.Forms.ListView();
            this.Functions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Btn_EditVarNames = new System.Windows.Forms.Button();
            this.Check_ShowUnknown = new System.Windows.Forms.CheckBox();
            this.Lbl_Index = new System.Windows.Forms.Label();
            this.Lbl_IndexDesc = new System.Windows.Forms.Label();
            this.Lbl_Procs = new System.Windows.Forms.Label();
            this.Lbl_Locals = new System.Windows.Forms.Label();
            this.Lbl_Globals = new System.Windows.Forms.Label();
            this.Lbl_Lines = new System.Windows.Forms.Label();
            this.Lbl_ProcsDesc = new System.Windows.Forms.Label();
            this.Lbl_LocalsDesc = new System.Windows.Forms.Label();
            this.Lbl_GlobalsDesc = new System.Windows.Forms.Label();
            this.Lbl_LinesDesc = new System.Windows.Forms.Label();
            this.Txt_ScriptCode = new System.Windows.Forms.RichTextBox();
            this.TabCtrl_Script = new System.Windows.Forms.TabControl();
            this.Tab_Decompile = new System.Windows.Forms.TabPage();
            this.Tab_RawBytecode = new System.Windows.Forms.TabPage();
            this.Txt_ByteCode = new System.Windows.Forms.RichTextBox();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.Box_ScriptInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_IndentationLevel)).BeginInit();
            this.TabCtrl_Script.SuspendLayout();
            this.Tab_Decompile.SuspendLayout();
            this.Tab_RawBytecode.SuspendLayout();
            this.SuspendLayout();
            // 
            // Box_ScriptInfo
            // 
            this.Box_ScriptInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Box_ScriptInfo.Controls.Add(this.Check_SyntaxHighlight);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_IndentationLevel);
            this.Box_ScriptInfo.Controls.Add(this.Btn_ApplyChanges);
            this.Box_ScriptInfo.Controls.Add(this.NUD_IndentationLevel);
            this.Box_ScriptInfo.Controls.Add(this.Lbl_VTable);
            this.Box_ScriptInfo.Controls.Add(this.List_VTable);
            this.Box_ScriptInfo.Controls.Add(this.Btn_EditVarNames);
            this.Box_ScriptInfo.Controls.Add(this.Check_ShowUnknown);
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
            this.Box_ScriptInfo.Size = new System.Drawing.Size(913, 138);
            this.Box_ScriptInfo.TabIndex = 0;
            this.Box_ScriptInfo.TabStop = false;
            this.Box_ScriptInfo.Text = "GameScript Information";
            // 
            // Check_SyntaxHighlight
            // 
            this.Check_SyntaxHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Check_SyntaxHighlight.AutoSize = true;
            this.Check_SyntaxHighlight.Checked = true;
            this.Check_SyntaxHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Check_SyntaxHighlight.Location = new System.Drawing.Point(805, 9);
            this.Check_SyntaxHighlight.Name = "Check_SyntaxHighlight";
            this.Check_SyntaxHighlight.Size = new System.Drawing.Size(102, 17);
            this.Check_SyntaxHighlight.TabIndex = 17;
            this.Check_SyntaxHighlight.Text = "Highlight Syntax";
            this.ToolTips.SetToolTip(this.Check_SyntaxHighlight, "Highlight syntax for better readability.\r\nCan be slow for very large gamescripts." +
        "");
            this.Check_SyntaxHighlight.UseVisualStyleBackColor = true;
            this.Check_SyntaxHighlight.CheckedChanged += new System.EventHandler(this.Check_SyntaxHighlight_CheckedChanged);
            // 
            // Lbl_IndentationLevel
            // 
            this.Lbl_IndentationLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_IndentationLevel.AutoSize = true;
            this.Lbl_IndentationLevel.Location = new System.Drawing.Point(706, 56);
            this.Lbl_IndentationLevel.Name = "Lbl_IndentationLevel";
            this.Lbl_IndentationLevel.Size = new System.Drawing.Size(102, 13);
            this.Lbl_IndentationLevel.TabIndex = 16;
            this.Lbl_IndentationLevel.Text = "Indentation Amount:";
            // 
            // Btn_ApplyChanges
            // 
            this.Btn_ApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ApplyChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Btn_ApplyChanges.Location = new System.Drawing.Point(814, 109);
            this.Btn_ApplyChanges.Name = "Btn_ApplyChanges";
            this.Btn_ApplyChanges.Size = new System.Drawing.Size(89, 23);
            this.Btn_ApplyChanges.TabIndex = 15;
            this.Btn_ApplyChanges.Text = "Apply Changes";
            this.ToolTips.SetToolTip(this.Btn_ApplyChanges, "Re-trigger the decompile with the changes made to the settings.");
            this.Btn_ApplyChanges.UseVisualStyleBackColor = false;
            this.Btn_ApplyChanges.Click += new System.EventHandler(this.Btn_ApplyChanges_Click);
            // 
            // NUD_IndentationLevel
            // 
            this.NUD_IndentationLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NUD_IndentationLevel.Location = new System.Drawing.Point(814, 54);
            this.NUD_IndentationLevel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.NUD_IndentationLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_IndentationLevel.Name = "NUD_IndentationLevel";
            this.NUD_IndentationLevel.Size = new System.Drawing.Size(89, 20);
            this.NUD_IndentationLevel.TabIndex = 14;
            this.ToolTips.SetToolTip(this.NUD_IndentationLevel, "How many spaces are added for each indentation level in the decompiled text.");
            this.NUD_IndentationLevel.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.NUD_IndentationLevel.ValueChanged += new System.EventHandler(this.NUD_IndentationLevel_ValueChanged);
            // 
            // Lbl_VTable
            // 
            this.Lbl_VTable.AutoSize = true;
            this.Lbl_VTable.Location = new System.Drawing.Point(162, 10);
            this.Lbl_VTable.Name = "Lbl_VTable";
            this.Lbl_VTable.Size = new System.Drawing.Size(41, 13);
            this.Lbl_VTable.TabIndex = 13;
            this.Lbl_VTable.Text = "VTable";
            // 
            // List_VTable
            // 
            this.List_VTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Functions});
            this.List_VTable.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.List_VTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.List_VTable.HideSelection = false;
            this.List_VTable.Location = new System.Drawing.Point(165, 26);
            this.List_VTable.Name = "List_VTable";
            this.List_VTable.Size = new System.Drawing.Size(184, 106);
            this.List_VTable.TabIndex = 12;
            this.List_VTable.UseCompatibleStateImageBehavior = false;
            this.List_VTable.View = System.Windows.Forms.View.Details;
            // 
            // Btn_EditVarNames
            // 
            this.Btn_EditVarNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_EditVarNames.Location = new System.Drawing.Point(814, 80);
            this.Btn_EditVarNames.Name = "Btn_EditVarNames";
            this.Btn_EditVarNames.Size = new System.Drawing.Size(89, 23);
            this.Btn_EditVarNames.TabIndex = 11;
            this.Btn_EditVarNames.Text = "Edit Names";
            this.ToolTips.SetToolTip(this.Btn_EditVarNames, "Edit the names of the auto-generated variables, procedures and labels in the deco" +
        "mpiled text.");
            this.Btn_EditVarNames.UseVisualStyleBackColor = true;
            this.Btn_EditVarNames.Click += new System.EventHandler(this.Btn_EditVarNames_Click);
            // 
            // Check_ShowUnknown
            // 
            this.Check_ShowUnknown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Check_ShowUnknown.AutoSize = true;
            this.Check_ShowUnknown.Checked = true;
            this.Check_ShowUnknown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Check_ShowUnknown.Location = new System.Drawing.Point(748, 31);
            this.Check_ShowUnknown.Name = "Check_ShowUnknown";
            this.Check_ShowUnknown.Size = new System.Drawing.Size(159, 17);
            this.Check_ShowUnknown.TabIndex = 10;
            this.Check_ShowUnknown.Text = "Show Unknown Instructions";
            this.ToolTips.SetToolTip(this.Check_ShowUnknown, "Denote unknown instructions with a comment in the decompiled text.");
            this.Check_ShowUnknown.UseVisualStyleBackColor = true;
            this.Check_ShowUnknown.CheckedChanged += new System.EventHandler(this.Check_ShowUnknown_CheckedChanged);
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
            // Txt_ScriptCode
            // 
            this.Txt_ScriptCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_ScriptCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(246)))), ((int)(((byte)(227)))));
            this.Txt_ScriptCode.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_ScriptCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.Txt_ScriptCode.Location = new System.Drawing.Point(7, 6);
            this.Txt_ScriptCode.Name = "Txt_ScriptCode";
            this.Txt_ScriptCode.ReadOnly = true;
            this.Txt_ScriptCode.Size = new System.Drawing.Size(892, 749);
            this.Txt_ScriptCode.TabIndex = 1;
            this.Txt_ScriptCode.Text = "";
            // 
            // TabCtrl_Script
            // 
            this.TabCtrl_Script.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabCtrl_Script.Controls.Add(this.Tab_Decompile);
            this.TabCtrl_Script.Controls.Add(this.Tab_RawBytecode);
            this.TabCtrl_Script.Location = new System.Drawing.Point(4, 148);
            this.TabCtrl_Script.Name = "TabCtrl_Script";
            this.TabCtrl_Script.SelectedIndex = 0;
            this.TabCtrl_Script.Size = new System.Drawing.Size(913, 787);
            this.TabCtrl_Script.TabIndex = 2;
            // 
            // Tab_Decompile
            // 
            this.Tab_Decompile.Controls.Add(this.Txt_ScriptCode);
            this.Tab_Decompile.Location = new System.Drawing.Point(4, 22);
            this.Tab_Decompile.Name = "Tab_Decompile";
            this.Tab_Decompile.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Decompile.Size = new System.Drawing.Size(905, 761);
            this.Tab_Decompile.TabIndex = 0;
            this.Tab_Decompile.Text = "Decompile";
            this.Tab_Decompile.UseVisualStyleBackColor = true;
            // 
            // Tab_RawBytecode
            // 
            this.Tab_RawBytecode.Controls.Add(this.Txt_ByteCode);
            this.Tab_RawBytecode.Location = new System.Drawing.Point(4, 22);
            this.Tab_RawBytecode.Name = "Tab_RawBytecode";
            this.Tab_RawBytecode.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_RawBytecode.Size = new System.Drawing.Size(905, 761);
            this.Tab_RawBytecode.TabIndex = 1;
            this.Tab_RawBytecode.Text = "Raw Bytecode";
            this.Tab_RawBytecode.UseVisualStyleBackColor = true;
            // 
            // Txt_ByteCode
            // 
            this.Txt_ByteCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_ByteCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(246)))), ((int)(((byte)(227)))));
            this.Txt_ByteCode.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.Txt_ByteCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.Txt_ByteCode.Location = new System.Drawing.Point(7, 6);
            this.Txt_ByteCode.Name = "Txt_ByteCode";
            this.Txt_ByteCode.Size = new System.Drawing.Size(895, 709);
            this.Txt_ByteCode.TabIndex = 0;
            this.Txt_ByteCode.Text = "";
            this.Txt_ByteCode.WordWrap = false;
            // 
            // ScriptViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 947);
            this.Controls.Add(this.TabCtrl_Script);
            this.Controls.Add(this.Box_ScriptInfo);
            this.Name = "ScriptViewer";
            this.ShowIcon = false;
            this.Text = "Script Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptViewer_FormClosing);
            this.Load += new System.EventHandler(this.ScriptViewer_Load);
            this.Box_ScriptInfo.ResumeLayout(false);
            this.Box_ScriptInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_IndentationLevel)).EndInit();
            this.TabCtrl_Script.ResumeLayout(false);
            this.Tab_Decompile.ResumeLayout(false);
            this.Tab_RawBytecode.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl TabCtrl_Script;
        private System.Windows.Forms.TabPage Tab_Decompile;
        private System.Windows.Forms.TabPage Tab_RawBytecode;
        private System.Windows.Forms.RichTextBox Txt_ByteCode;
        private System.Windows.Forms.CheckBox Check_ShowUnknown;
        private System.Windows.Forms.Button Btn_EditVarNames;
        private System.Windows.Forms.Label Lbl_VTable;
        private System.Windows.Forms.ListView List_VTable;
        private System.Windows.Forms.ColumnHeader Functions;
        private System.Windows.Forms.Button Btn_ApplyChanges;
        private System.Windows.Forms.NumericUpDown NUD_IndentationLevel;
        private System.Windows.Forms.ToolTip ToolTips;
        private System.Windows.Forms.Label Lbl_IndentationLevel;
        private System.Windows.Forms.CheckBox Check_SyntaxHighlight;
    }
}