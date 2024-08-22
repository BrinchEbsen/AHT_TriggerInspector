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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.List_Maps = new System.Windows.Forms.ListView();
            this.MapIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MapHash = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MapNumTriggers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Check_OnlyScripted = new System.Windows.Forms.CheckBox();
            this.List_Triggers = new System.Windows.Forms.ListView();
            this.TriggerIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TriggerType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TriggerSubType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TriggerHasScript = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lbl_TriggerIndex = new System.Windows.Forms.Label();
            this.Lbl_TriggerTypeDesc = new System.Windows.Forms.Label();
            this.Lbl_TriggerSubTypeDesc = new System.Windows.Forms.Label();
            this.Lbl_TriggerType = new System.Windows.Forms.Label();
            this.Lbl_TriggerSubType = new System.Windows.Forms.Label();
            this.Lbl_GameFlagsDesc = new System.Windows.Forms.Label();
            this.Lbl_GameFlags = new System.Windows.Forms.Label();
            this.Box_TriggerInfo = new System.Windows.Forms.GroupBox();
            this.Lbl_Range = new System.Windows.Forms.Label();
            this.Lbl_RangeDesc = new System.Windows.Forms.Label();
            this.Check_ShowDataInHex = new System.Windows.Forms.CheckBox();
            this.Lbl_GameScript = new System.Windows.Forms.Label();
            this.Lbl_GameScriptDesc = new System.Windows.Forms.Label();
            this.PicBox_TintColour = new System.Windows.Forms.PictureBox();
            this.Btn_ViewGameScript = new System.Windows.Forms.Button();
            this.Lbl_GeoHash = new System.Windows.Forms.Label();
            this.Lbl_GeoHashDesc = new System.Windows.Forms.Label();
            this.Lbl_GFXHash = new System.Windows.Forms.Label();
            this.Lbl_GFXHashDesc = new System.Windows.Forms.Label();
            this.Lbl_Tint = new System.Windows.Forms.Label();
            this.Lbl_TintDesc = new System.Windows.Forms.Label();
            this.Box_TriggerLinks = new System.Windows.Forms.GroupBox();
            this.Lbl_TriggerLinks = new System.Windows.Forms.Label();
            this.Box_TriggerData = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Lbl_TriggerData = new System.Windows.Forms.Label();
            this.Lbl_ScaleZ = new System.Windows.Forms.Label();
            this.Lbl_ScaleZDesc = new System.Windows.Forms.Label();
            this.Lbl_ScaleY = new System.Windows.Forms.Label();
            this.Lbl_ScaleYDesc = new System.Windows.Forms.Label();
            this.Lbl_ScaleX = new System.Windows.Forms.Label();
            this.Lbl_ScaleXDesc = new System.Windows.Forms.Label();
            this.Lbl_ScaleDesc = new System.Windows.Forms.Label();
            this.Lbl_RotZ = new System.Windows.Forms.Label();
            this.Lbl_RotZDesc = new System.Windows.Forms.Label();
            this.Lbl_RotY = new System.Windows.Forms.Label();
            this.Lbl_RotYDesc = new System.Windows.Forms.Label();
            this.Lbl_RotX = new System.Windows.Forms.Label();
            this.Lbl_RotXDesc = new System.Windows.Forms.Label();
            this.Lbl_RotDesc = new System.Windows.Forms.Label();
            this.Lbl_PosZ = new System.Windows.Forms.Label();
            this.Lbl_PosZDesc = new System.Windows.Forms.Label();
            this.Lbl_PosY = new System.Windows.Forms.Label();
            this.Lbl_PosYDesc = new System.Windows.Forms.Label();
            this.Lbl_PosX = new System.Windows.Forms.Label();
            this.Lbl_PosXDesc = new System.Windows.Forms.Label();
            this.Lbl_PosDesc = new System.Windows.Forms.Label();
            this.Lbl_TrigFlags = new System.Windows.Forms.Label();
            this.Lbl_TrigFlagsDesc = new System.Windows.Forms.Label();
            this.Btn_LoadFile = new System.Windows.Forms.Button();
            this.Lbl_OpenedFileName = new System.Windows.Forms.Label();
            this.Lbl_MapName = new System.Windows.Forms.Label();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Box_TriggerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_TintColour)).BeginInit();
            this.Box_TriggerLinks.SuspendLayout();
            this.Box_TriggerData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.List_Maps);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 140);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maps";
            // 
            // List_Maps
            // 
            this.List_Maps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MapIndex,
            this.MapHash,
            this.MapNumTriggers});
            this.List_Maps.FullRowSelect = true;
            this.List_Maps.HideSelection = false;
            this.List_Maps.Location = new System.Drawing.Point(7, 20);
            this.List_Maps.MultiSelect = false;
            this.List_Maps.Name = "List_Maps";
            this.List_Maps.Size = new System.Drawing.Size(386, 114);
            this.List_Maps.TabIndex = 0;
            this.List_Maps.UseCompatibleStateImageBehavior = false;
            this.List_Maps.View = System.Windows.Forms.View.Details;
            this.List_Maps.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.List_Maps_DoubleClick);
            // 
            // MapIndex
            // 
            this.MapIndex.Text = "Index";
            this.MapIndex.Width = 44;
            // 
            // MapHash
            // 
            this.MapHash.Text = "HashCode";
            this.MapHash.Width = 100;
            // 
            // MapNumTriggers
            // 
            this.MapNumTriggers.Text = "Trigger Amount";
            this.MapNumTriggers.Width = 103;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.Check_OnlyScripted);
            this.groupBox2.Controls.Add(this.List_Triggers);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 664);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Triggers";
            // 
            // Check_OnlyScripted
            // 
            this.Check_OnlyScripted.AutoSize = true;
            this.Check_OnlyScripted.Location = new System.Drawing.Point(7, 20);
            this.Check_OnlyScripted.Name = "Check_OnlyScripted";
            this.Check_OnlyScripted.Size = new System.Drawing.Size(146, 19);
            this.Check_OnlyScripted.TabIndex = 1;
            this.Check_OnlyScripted.Text = "Only Scripted Triggers";
            this.ToolTips.SetToolTip(this.Check_OnlyScripted, "Only list triggers that have gamescripts tied to them.");
            this.Check_OnlyScripted.UseVisualStyleBackColor = true;
            this.Check_OnlyScripted.CheckedChanged += new System.EventHandler(this.Check_OnlyScripted_CheckedChanged);
            // 
            // List_Triggers
            // 
            this.List_Triggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.List_Triggers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TriggerIndex,
            this.TriggerType,
            this.TriggerSubType,
            this.TriggerHasScript});
            this.List_Triggers.FullRowSelect = true;
            this.List_Triggers.HideSelection = false;
            this.List_Triggers.Location = new System.Drawing.Point(7, 48);
            this.List_Triggers.MultiSelect = false;
            this.List_Triggers.Name = "List_Triggers";
            this.List_Triggers.Size = new System.Drawing.Size(386, 610);
            this.List_Triggers.TabIndex = 0;
            this.List_Triggers.UseCompatibleStateImageBehavior = false;
            this.List_Triggers.View = System.Windows.Forms.View.Details;
            this.List_Triggers.SelectedIndexChanged += new System.EventHandler(this.List_Triggers_SelectedIndexChanged);
            // 
            // TriggerIndex
            // 
            this.TriggerIndex.Text = "Index";
            this.TriggerIndex.Width = 43;
            // 
            // TriggerType
            // 
            this.TriggerType.Text = "Type";
            this.TriggerType.Width = 140;
            // 
            // TriggerSubType
            // 
            this.TriggerSubType.Text = "SubType";
            this.TriggerSubType.Width = 115;
            // 
            // TriggerHasScript
            // 
            this.TriggerHasScript.Text = "GameScript";
            this.TriggerHasScript.Width = 69;
            // 
            // Lbl_TriggerIndex
            // 
            this.Lbl_TriggerIndex.AutoSize = true;
            this.Lbl_TriggerIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TriggerIndex.Location = new System.Drawing.Point(6, 29);
            this.Lbl_TriggerIndex.Name = "Lbl_TriggerIndex";
            this.Lbl_TriggerIndex.Size = new System.Drawing.Size(71, 20);
            this.Lbl_TriggerIndex.TabIndex = 3;
            this.Lbl_TriggerIndex.Text = "Trigger #";
            // 
            // Lbl_TriggerTypeDesc
            // 
            this.Lbl_TriggerTypeDesc.AutoSize = true;
            this.Lbl_TriggerTypeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TriggerTypeDesc.Location = new System.Drawing.Point(6, 62);
            this.Lbl_TriggerTypeDesc.Name = "Lbl_TriggerTypeDesc";
            this.Lbl_TriggerTypeDesc.Size = new System.Drawing.Size(51, 20);
            this.Lbl_TriggerTypeDesc.TabIndex = 4;
            this.Lbl_TriggerTypeDesc.Text = "Type: ";
            // 
            // Lbl_TriggerSubTypeDesc
            // 
            this.Lbl_TriggerSubTypeDesc.AutoSize = true;
            this.Lbl_TriggerSubTypeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TriggerSubTypeDesc.Location = new System.Drawing.Point(6, 82);
            this.Lbl_TriggerSubTypeDesc.Name = "Lbl_TriggerSubTypeDesc";
            this.Lbl_TriggerSubTypeDesc.Size = new System.Drawing.Size(80, 20);
            this.Lbl_TriggerSubTypeDesc.TabIndex = 5;
            this.Lbl_TriggerSubTypeDesc.Text = "SubType: ";
            // 
            // Lbl_TriggerType
            // 
            this.Lbl_TriggerType.AutoSize = true;
            this.Lbl_TriggerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TriggerType.Location = new System.Drawing.Point(108, 62);
            this.Lbl_TriggerType.Name = "Lbl_TriggerType";
            this.Lbl_TriggerType.Size = new System.Drawing.Size(45, 20);
            this.Lbl_TriggerType.TabIndex = 6;
            this.Lbl_TriggerType.Text = "temp";
            // 
            // Lbl_TriggerSubType
            // 
            this.Lbl_TriggerSubType.AutoSize = true;
            this.Lbl_TriggerSubType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TriggerSubType.Location = new System.Drawing.Point(108, 82);
            this.Lbl_TriggerSubType.Name = "Lbl_TriggerSubType";
            this.Lbl_TriggerSubType.Size = new System.Drawing.Size(45, 20);
            this.Lbl_TriggerSubType.TabIndex = 7;
            this.Lbl_TriggerSubType.Text = "temp";
            // 
            // Lbl_GameFlagsDesc
            // 
            this.Lbl_GameFlagsDesc.AutoSize = true;
            this.Lbl_GameFlagsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GameFlagsDesc.Location = new System.Drawing.Point(6, 118);
            this.Lbl_GameFlagsDesc.Name = "Lbl_GameFlagsDesc";
            this.Lbl_GameFlagsDesc.Size = new System.Drawing.Size(100, 20);
            this.Lbl_GameFlagsDesc.TabIndex = 8;
            this.Lbl_GameFlagsDesc.Text = "Game Flags:";
            // 
            // Lbl_GameFlags
            // 
            this.Lbl_GameFlags.AutoSize = true;
            this.Lbl_GameFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GameFlags.Location = new System.Drawing.Point(108, 118);
            this.Lbl_GameFlags.Name = "Lbl_GameFlags";
            this.Lbl_GameFlags.Size = new System.Drawing.Size(45, 20);
            this.Lbl_GameFlags.TabIndex = 9;
            this.Lbl_GameFlags.Text = "temp";
            // 
            // Box_TriggerInfo
            // 
            this.Box_TriggerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Box_TriggerInfo.Controls.Add(this.Lbl_Range);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RangeDesc);
            this.Box_TriggerInfo.Controls.Add(this.Check_ShowDataInHex);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GameScript);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GameScriptDesc);
            this.Box_TriggerInfo.Controls.Add(this.PicBox_TintColour);
            this.Box_TriggerInfo.Controls.Add(this.Btn_ViewGameScript);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GeoHash);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GeoHashDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GFXHash);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GFXHashDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_Tint);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TintDesc);
            this.Box_TriggerInfo.Controls.Add(this.Box_TriggerLinks);
            this.Box_TriggerInfo.Controls.Add(this.Box_TriggerData);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleZ);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleZDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleY);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleYDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleX);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleXDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_ScaleDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotZ);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotZDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotY);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotYDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotX);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotXDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_RotDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosZ);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosZDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosY);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosYDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosX);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosXDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_PosDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TrigFlags);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TrigFlagsDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TriggerIndex);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GameFlags);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TriggerTypeDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_GameFlagsDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TriggerSubTypeDesc);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TriggerSubType);
            this.Box_TriggerInfo.Controls.Add(this.Lbl_TriggerType);
            this.Box_TriggerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Box_TriggerInfo.Location = new System.Drawing.Point(417, 32);
            this.Box_TriggerInfo.Name = "Box_TriggerInfo";
            this.Box_TriggerInfo.Size = new System.Drawing.Size(708, 807);
            this.Box_TriggerInfo.TabIndex = 10;
            this.Box_TriggerInfo.TabStop = false;
            this.Box_TriggerInfo.Text = "Trigger Information";
            this.Box_TriggerInfo.Visible = false;
            // 
            // Lbl_Range
            // 
            this.Lbl_Range.AutoSize = true;
            this.Lbl_Range.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Range.Location = new System.Drawing.Point(108, 172);
            this.Lbl_Range.Name = "Lbl_Range";
            this.Lbl_Range.Size = new System.Drawing.Size(45, 20);
            this.Lbl_Range.TabIndex = 47;
            this.Lbl_Range.Text = "temp";
            this.ToolTips.SetToolTip(this.Lbl_Range, "If the second bit in data slot 10 is set, data slot 11 will be used as this trigg" +
        "er\'s range.\r\nIf not, the map\'s default trigger range is used.");
            // 
            // Lbl_RangeDesc
            // 
            this.Lbl_RangeDesc.AutoSize = true;
            this.Lbl_RangeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RangeDesc.Location = new System.Drawing.Point(6, 172);
            this.Lbl_RangeDesc.Name = "Lbl_RangeDesc";
            this.Lbl_RangeDesc.Size = new System.Drawing.Size(61, 20);
            this.Lbl_RangeDesc.TabIndex = 46;
            this.Lbl_RangeDesc.Text = "Range:";
            // 
            // Check_ShowDataInHex
            // 
            this.Check_ShowDataInHex.AutoSize = true;
            this.Check_ShowDataInHex.Location = new System.Drawing.Point(57, 463);
            this.Check_ShowDataInHex.Name = "Check_ShowDataInHex";
            this.Check_ShowDataInHex.Size = new System.Drawing.Size(53, 22);
            this.Check_ShowDataInHex.TabIndex = 45;
            this.Check_ShowDataInHex.Text = "Hex";
            this.ToolTips.SetToolTip(this.Check_ShowDataInHex, "Display the data in its raw hex format.\r\nWhen unchecked, data is interpreted into" +
        " more readable formats.");
            this.Check_ShowDataInHex.UseVisualStyleBackColor = true;
            this.Check_ShowDataInHex.CheckedChanged += new System.EventHandler(this.Check_ShowDataInHex_CheckedChanged);
            // 
            // Lbl_GameScript
            // 
            this.Lbl_GameScript.AutoSize = true;
            this.Lbl_GameScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GameScript.Location = new System.Drawing.Point(148, 414);
            this.Lbl_GameScript.Name = "Lbl_GameScript";
            this.Lbl_GameScript.Size = new System.Drawing.Size(45, 20);
            this.Lbl_GameScript.TabIndex = 44;
            this.Lbl_GameScript.Text = "temp";
            // 
            // Lbl_GameScriptDesc
            // 
            this.Lbl_GameScriptDesc.AutoSize = true;
            this.Lbl_GameScriptDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GameScriptDesc.Location = new System.Drawing.Point(8, 414);
            this.Lbl_GameScriptDesc.Name = "Lbl_GameScriptDesc";
            this.Lbl_GameScriptDesc.Size = new System.Drawing.Size(141, 20);
            this.Lbl_GameScriptDesc.TabIndex = 43;
            this.Lbl_GameScriptDesc.Text = "GameScript Index:";
            // 
            // PicBox_TintColour
            // 
            this.PicBox_TintColour.BackColor = System.Drawing.Color.Azure;
            this.PicBox_TintColour.Location = new System.Drawing.Point(51, 385);
            this.PicBox_TintColour.Name = "PicBox_TintColour";
            this.PicBox_TintColour.Size = new System.Drawing.Size(60, 20);
            this.PicBox_TintColour.TabIndex = 42;
            this.PicBox_TintColour.TabStop = false;
            // 
            // Btn_ViewGameScript
            // 
            this.Btn_ViewGameScript.Location = new System.Drawing.Point(199, 409);
            this.Btn_ViewGameScript.Name = "Btn_ViewGameScript";
            this.Btn_ViewGameScript.Size = new System.Drawing.Size(145, 30);
            this.Btn_ViewGameScript.TabIndex = 41;
            this.Btn_ViewGameScript.Text = "View GameScript";
            this.ToolTips.SetToolTip(this.Btn_ViewGameScript, "Open the script viewer window to view a decompiled version of the gamescript tied" +
        " to this trigger.");
            this.Btn_ViewGameScript.UseVisualStyleBackColor = true;
            this.Btn_ViewGameScript.Click += new System.EventHandler(this.Btn_ViewGameScript_Click);
            // 
            // Lbl_GeoHash
            // 
            this.Lbl_GeoHash.AutoSize = true;
            this.Lbl_GeoHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GeoHash.Location = new System.Drawing.Point(133, 365);
            this.Lbl_GeoHash.Name = "Lbl_GeoHash";
            this.Lbl_GeoHash.Size = new System.Drawing.Size(45, 20);
            this.Lbl_GeoHash.TabIndex = 40;
            this.Lbl_GeoHash.Text = "temp";
            // 
            // Lbl_GeoHashDesc
            // 
            this.Lbl_GeoHashDesc.AutoSize = true;
            this.Lbl_GeoHashDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GeoHashDesc.Location = new System.Drawing.Point(6, 365);
            this.Lbl_GeoHashDesc.Name = "Lbl_GeoHashDesc";
            this.Lbl_GeoHashDesc.Size = new System.Drawing.Size(121, 20);
            this.Lbl_GeoHashDesc.TabIndex = 39;
            this.Lbl_GeoHashDesc.Text = "Geo Hashcode:";
            // 
            // Lbl_GFXHash
            // 
            this.Lbl_GFXHash.AutoSize = true;
            this.Lbl_GFXHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GFXHash.Location = new System.Drawing.Point(133, 345);
            this.Lbl_GFXHash.Name = "Lbl_GFXHash";
            this.Lbl_GFXHash.Size = new System.Drawing.Size(45, 20);
            this.Lbl_GFXHash.TabIndex = 38;
            this.Lbl_GFXHash.Text = "temp";
            // 
            // Lbl_GFXHashDesc
            // 
            this.Lbl_GFXHashDesc.AutoSize = true;
            this.Lbl_GFXHashDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GFXHashDesc.Location = new System.Drawing.Point(6, 345);
            this.Lbl_GFXHashDesc.Name = "Lbl_GFXHashDesc";
            this.Lbl_GFXHashDesc.Size = new System.Drawing.Size(124, 20);
            this.Lbl_GFXHashDesc.TabIndex = 37;
            this.Lbl_GFXHashDesc.Text = "GFX Hashcode:";
            // 
            // Lbl_Tint
            // 
            this.Lbl_Tint.AutoSize = true;
            this.Lbl_Tint.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Tint.Location = new System.Drawing.Point(133, 385);
            this.Lbl_Tint.Name = "Lbl_Tint";
            this.Lbl_Tint.Size = new System.Drawing.Size(48, 18);
            this.Lbl_Tint.TabIndex = 36;
            this.Lbl_Tint.Text = "temp";
            // 
            // Lbl_TintDesc
            // 
            this.Lbl_TintDesc.AutoSize = true;
            this.Lbl_TintDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TintDesc.Location = new System.Drawing.Point(6, 385);
            this.Lbl_TintDesc.Name = "Lbl_TintDesc";
            this.Lbl_TintDesc.Size = new System.Drawing.Size(39, 20);
            this.Lbl_TintDesc.TabIndex = 35;
            this.Lbl_TintDesc.Text = "Tint:";
            // 
            // Box_TriggerLinks
            // 
            this.Box_TriggerLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Box_TriggerLinks.Controls.Add(this.Lbl_TriggerLinks);
            this.Box_TriggerLinks.Location = new System.Drawing.Point(435, 479);
            this.Box_TriggerLinks.Name = "Box_TriggerLinks";
            this.Box_TriggerLinks.Size = new System.Drawing.Size(267, 322);
            this.Box_TriggerLinks.TabIndex = 34;
            this.Box_TriggerLinks.TabStop = false;
            this.Box_TriggerLinks.Text = "Links";
            // 
            // Lbl_TriggerLinks
            // 
            this.Lbl_TriggerLinks.AutoSize = true;
            this.Lbl_TriggerLinks.Location = new System.Drawing.Point(7, 24);
            this.Lbl_TriggerLinks.Name = "Lbl_TriggerLinks";
            this.Lbl_TriggerLinks.Size = new System.Drawing.Size(41, 144);
            this.Lbl_TriggerLinks.TabIndex = 0;
            this.Lbl_TriggerLinks.Text = "temp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp";
            // 
            // Box_TriggerData
            // 
            this.Box_TriggerData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Box_TriggerData.Controls.Add(this.label1);
            this.Box_TriggerData.Controls.Add(this.Lbl_TriggerData);
            this.Box_TriggerData.Location = new System.Drawing.Point(12, 479);
            this.Box_TriggerData.Name = "Box_TriggerData";
            this.Box_TriggerData.Size = new System.Drawing.Size(417, 322);
            this.Box_TriggerData.TabIndex = 33;
            this.Box_TriggerData.TabStop = false;
            this.Box_TriggerData.Text = "Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 288);
            this.label1.TabIndex = 1;
            this.label1.Text = "0\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15";
            // 
            // Lbl_TriggerData
            // 
            this.Lbl_TriggerData.AutoSize = true;
            this.Lbl_TriggerData.Location = new System.Drawing.Point(45, 24);
            this.Lbl_TriggerData.Name = "Lbl_TriggerData";
            this.Lbl_TriggerData.Size = new System.Drawing.Size(41, 288);
            this.Lbl_TriggerData.TabIndex = 0;
            this.Lbl_TriggerData.Text = "temp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntemp\r\ntem" +
    "p\r\ntemp\r\ntemp";
            // 
            // Lbl_ScaleZ
            // 
            this.Lbl_ScaleZ.AutoSize = true;
            this.Lbl_ScaleZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleZ.Location = new System.Drawing.Point(269, 306);
            this.Lbl_ScaleZ.Name = "Lbl_ScaleZ";
            this.Lbl_ScaleZ.Size = new System.Drawing.Size(45, 20);
            this.Lbl_ScaleZ.TabIndex = 32;
            this.Lbl_ScaleZ.Text = "temp";
            // 
            // Lbl_ScaleZDesc
            // 
            this.Lbl_ScaleZDesc.AutoSize = true;
            this.Lbl_ScaleZDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleZDesc.Location = new System.Drawing.Point(240, 306);
            this.Lbl_ScaleZDesc.Name = "Lbl_ScaleZDesc";
            this.Lbl_ScaleZDesc.Size = new System.Drawing.Size(23, 20);
            this.Lbl_ScaleZDesc.TabIndex = 31;
            this.Lbl_ScaleZDesc.Text = "Z:";
            // 
            // Lbl_ScaleY
            // 
            this.Lbl_ScaleY.AutoSize = true;
            this.Lbl_ScaleY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleY.Location = new System.Drawing.Point(162, 306);
            this.Lbl_ScaleY.Name = "Lbl_ScaleY";
            this.Lbl_ScaleY.Size = new System.Drawing.Size(45, 20);
            this.Lbl_ScaleY.TabIndex = 30;
            this.Lbl_ScaleY.Text = "temp";
            // 
            // Lbl_ScaleYDesc
            // 
            this.Lbl_ScaleYDesc.AutoSize = true;
            this.Lbl_ScaleYDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleYDesc.Location = new System.Drawing.Point(133, 306);
            this.Lbl_ScaleYDesc.Name = "Lbl_ScaleYDesc";
            this.Lbl_ScaleYDesc.Size = new System.Drawing.Size(24, 20);
            this.Lbl_ScaleYDesc.TabIndex = 29;
            this.Lbl_ScaleYDesc.Text = "Y:";
            // 
            // Lbl_ScaleX
            // 
            this.Lbl_ScaleX.AutoSize = true;
            this.Lbl_ScaleX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleX.Location = new System.Drawing.Point(53, 306);
            this.Lbl_ScaleX.Name = "Lbl_ScaleX";
            this.Lbl_ScaleX.Size = new System.Drawing.Size(45, 20);
            this.Lbl_ScaleX.TabIndex = 28;
            this.Lbl_ScaleX.Text = "temp";
            // 
            // Lbl_ScaleXDesc
            // 
            this.Lbl_ScaleXDesc.AutoSize = true;
            this.Lbl_ScaleXDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleXDesc.Location = new System.Drawing.Point(24, 306);
            this.Lbl_ScaleXDesc.Name = "Lbl_ScaleXDesc";
            this.Lbl_ScaleXDesc.Size = new System.Drawing.Size(24, 20);
            this.Lbl_ScaleXDesc.TabIndex = 27;
            this.Lbl_ScaleXDesc.Text = "X:";
            // 
            // Lbl_ScaleDesc
            // 
            this.Lbl_ScaleDesc.AutoSize = true;
            this.Lbl_ScaleDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ScaleDesc.Location = new System.Drawing.Point(8, 286);
            this.Lbl_ScaleDesc.Name = "Lbl_ScaleDesc";
            this.Lbl_ScaleDesc.Size = new System.Drawing.Size(53, 20);
            this.Lbl_ScaleDesc.TabIndex = 26;
            this.Lbl_ScaleDesc.Text = "Scale:";
            // 
            // Lbl_RotZ
            // 
            this.Lbl_RotZ.AutoSize = true;
            this.Lbl_RotZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotZ.Location = new System.Drawing.Point(269, 266);
            this.Lbl_RotZ.Name = "Lbl_RotZ";
            this.Lbl_RotZ.Size = new System.Drawing.Size(45, 20);
            this.Lbl_RotZ.TabIndex = 25;
            this.Lbl_RotZ.Text = "temp";
            // 
            // Lbl_RotZDesc
            // 
            this.Lbl_RotZDesc.AutoSize = true;
            this.Lbl_RotZDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotZDesc.Location = new System.Drawing.Point(240, 266);
            this.Lbl_RotZDesc.Name = "Lbl_RotZDesc";
            this.Lbl_RotZDesc.Size = new System.Drawing.Size(23, 20);
            this.Lbl_RotZDesc.TabIndex = 24;
            this.Lbl_RotZDesc.Text = "Z:";
            // 
            // Lbl_RotY
            // 
            this.Lbl_RotY.AutoSize = true;
            this.Lbl_RotY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotY.Location = new System.Drawing.Point(162, 266);
            this.Lbl_RotY.Name = "Lbl_RotY";
            this.Lbl_RotY.Size = new System.Drawing.Size(45, 20);
            this.Lbl_RotY.TabIndex = 23;
            this.Lbl_RotY.Text = "temp";
            // 
            // Lbl_RotYDesc
            // 
            this.Lbl_RotYDesc.AutoSize = true;
            this.Lbl_RotYDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotYDesc.Location = new System.Drawing.Point(133, 266);
            this.Lbl_RotYDesc.Name = "Lbl_RotYDesc";
            this.Lbl_RotYDesc.Size = new System.Drawing.Size(24, 20);
            this.Lbl_RotYDesc.TabIndex = 22;
            this.Lbl_RotYDesc.Text = "Y:";
            // 
            // Lbl_RotX
            // 
            this.Lbl_RotX.AutoSize = true;
            this.Lbl_RotX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotX.Location = new System.Drawing.Point(53, 266);
            this.Lbl_RotX.Name = "Lbl_RotX";
            this.Lbl_RotX.Size = new System.Drawing.Size(45, 20);
            this.Lbl_RotX.TabIndex = 21;
            this.Lbl_RotX.Text = "temp";
            // 
            // Lbl_RotXDesc
            // 
            this.Lbl_RotXDesc.AutoSize = true;
            this.Lbl_RotXDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotXDesc.Location = new System.Drawing.Point(24, 266);
            this.Lbl_RotXDesc.Name = "Lbl_RotXDesc";
            this.Lbl_RotXDesc.Size = new System.Drawing.Size(24, 20);
            this.Lbl_RotXDesc.TabIndex = 20;
            this.Lbl_RotXDesc.Text = "X:";
            // 
            // Lbl_RotDesc
            // 
            this.Lbl_RotDesc.AutoSize = true;
            this.Lbl_RotDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_RotDesc.Location = new System.Drawing.Point(8, 246);
            this.Lbl_RotDesc.Name = "Lbl_RotDesc";
            this.Lbl_RotDesc.Size = new System.Drawing.Size(74, 20);
            this.Lbl_RotDesc.TabIndex = 19;
            this.Lbl_RotDesc.Text = "Rotation:";
            // 
            // Lbl_PosZ
            // 
            this.Lbl_PosZ.AutoSize = true;
            this.Lbl_PosZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosZ.Location = new System.Drawing.Point(269, 226);
            this.Lbl_PosZ.Name = "Lbl_PosZ";
            this.Lbl_PosZ.Size = new System.Drawing.Size(45, 20);
            this.Lbl_PosZ.TabIndex = 18;
            this.Lbl_PosZ.Text = "temp";
            // 
            // Lbl_PosZDesc
            // 
            this.Lbl_PosZDesc.AutoSize = true;
            this.Lbl_PosZDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosZDesc.Location = new System.Drawing.Point(240, 226);
            this.Lbl_PosZDesc.Name = "Lbl_PosZDesc";
            this.Lbl_PosZDesc.Size = new System.Drawing.Size(23, 20);
            this.Lbl_PosZDesc.TabIndex = 17;
            this.Lbl_PosZDesc.Text = "Z:";
            // 
            // Lbl_PosY
            // 
            this.Lbl_PosY.AutoSize = true;
            this.Lbl_PosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosY.Location = new System.Drawing.Point(162, 226);
            this.Lbl_PosY.Name = "Lbl_PosY";
            this.Lbl_PosY.Size = new System.Drawing.Size(45, 20);
            this.Lbl_PosY.TabIndex = 16;
            this.Lbl_PosY.Text = "temp";
            // 
            // Lbl_PosYDesc
            // 
            this.Lbl_PosYDesc.AutoSize = true;
            this.Lbl_PosYDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosYDesc.Location = new System.Drawing.Point(133, 226);
            this.Lbl_PosYDesc.Name = "Lbl_PosYDesc";
            this.Lbl_PosYDesc.Size = new System.Drawing.Size(24, 20);
            this.Lbl_PosYDesc.TabIndex = 15;
            this.Lbl_PosYDesc.Text = "Y:";
            // 
            // Lbl_PosX
            // 
            this.Lbl_PosX.AutoSize = true;
            this.Lbl_PosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosX.Location = new System.Drawing.Point(53, 226);
            this.Lbl_PosX.Name = "Lbl_PosX";
            this.Lbl_PosX.Size = new System.Drawing.Size(45, 20);
            this.Lbl_PosX.TabIndex = 14;
            this.Lbl_PosX.Text = "temp";
            // 
            // Lbl_PosXDesc
            // 
            this.Lbl_PosXDesc.AutoSize = true;
            this.Lbl_PosXDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosXDesc.Location = new System.Drawing.Point(24, 226);
            this.Lbl_PosXDesc.Name = "Lbl_PosXDesc";
            this.Lbl_PosXDesc.Size = new System.Drawing.Size(24, 20);
            this.Lbl_PosXDesc.TabIndex = 13;
            this.Lbl_PosXDesc.Text = "X:";
            // 
            // Lbl_PosDesc
            // 
            this.Lbl_PosDesc.AutoSize = true;
            this.Lbl_PosDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PosDesc.Location = new System.Drawing.Point(8, 206);
            this.Lbl_PosDesc.Name = "Lbl_PosDesc";
            this.Lbl_PosDesc.Size = new System.Drawing.Size(69, 20);
            this.Lbl_PosDesc.TabIndex = 12;
            this.Lbl_PosDesc.Text = "Position:";
            // 
            // Lbl_TrigFlags
            // 
            this.Lbl_TrigFlags.AutoSize = true;
            this.Lbl_TrigFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TrigFlags.Location = new System.Drawing.Point(108, 138);
            this.Lbl_TrigFlags.Name = "Lbl_TrigFlags";
            this.Lbl_TrigFlags.Size = new System.Drawing.Size(45, 20);
            this.Lbl_TrigFlags.TabIndex = 11;
            this.Lbl_TrigFlags.Text = "temp";
            // 
            // Lbl_TrigFlagsDesc
            // 
            this.Lbl_TrigFlagsDesc.AutoSize = true;
            this.Lbl_TrigFlagsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TrigFlagsDesc.Location = new System.Drawing.Point(6, 138);
            this.Lbl_TrigFlagsDesc.Name = "Lbl_TrigFlagsDesc";
            this.Lbl_TrigFlagsDesc.Size = new System.Drawing.Size(105, 20);
            this.Lbl_TrigFlagsDesc.TabIndex = 10;
            this.Lbl_TrigFlagsDesc.Text = "Trigger Flags:";
            // 
            // Btn_LoadFile
            // 
            this.Btn_LoadFile.Location = new System.Drawing.Point(11, 5);
            this.Btn_LoadFile.Name = "Btn_LoadFile";
            this.Btn_LoadFile.Size = new System.Drawing.Size(109, 28);
            this.Btn_LoadFile.TabIndex = 11;
            this.Btn_LoadFile.Text = "Load File";
            this.ToolTips.SetToolTip(this.Btn_LoadFile, "Load an .edb file to be inspected.");
            this.Btn_LoadFile.UseVisualStyleBackColor = true;
            this.Btn_LoadFile.Click += new System.EventHandler(this.Btn_LoadFile_Click_1);
            // 
            // Lbl_OpenedFileName
            // 
            this.Lbl_OpenedFileName.AutoSize = true;
            this.Lbl_OpenedFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_OpenedFileName.Location = new System.Drawing.Point(126, 12);
            this.Lbl_OpenedFileName.Name = "Lbl_OpenedFileName";
            this.Lbl_OpenedFileName.Size = new System.Drawing.Size(91, 16);
            this.Lbl_OpenedFileName.TabIndex = 12;
            this.Lbl_OpenedFileName.Text = "No file loaded";
            // 
            // Lbl_MapName
            // 
            this.Lbl_MapName.AutoSize = true;
            this.Lbl_MapName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_MapName.Location = new System.Drawing.Point(309, 11);
            this.Lbl_MapName.Name = "Lbl_MapName";
            this.Lbl_MapName.Size = new System.Drawing.Size(37, 16);
            this.Lbl_MapName.TabIndex = 13;
            this.Lbl_MapName.Text = "temp";
            this.Lbl_MapName.Visible = false;
            // 
            // MainWnd
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 851);
            this.Controls.Add(this.Lbl_MapName);
            this.Controls.Add(this.Lbl_OpenedFileName);
            this.Controls.Add(this.Btn_LoadFile);
            this.Controls.Add(this.Box_TriggerInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainWnd";
            this.ShowIcon = false;
            this.Text = "Trigger Inspector";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWnd_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWnd_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Box_TriggerInfo.ResumeLayout(false);
            this.Box_TriggerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_TintColour)).EndInit();
            this.Box_TriggerLinks.ResumeLayout(false);
            this.Box_TriggerLinks.PerformLayout();
            this.Box_TriggerData.ResumeLayout(false);
            this.Box_TriggerData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView List_Maps;
        private System.Windows.Forms.ListView List_Triggers;
        private System.Windows.Forms.ColumnHeader MapIndex;
        private System.Windows.Forms.ColumnHeader MapHash;
        private System.Windows.Forms.ColumnHeader TriggerIndex;
        private System.Windows.Forms.ColumnHeader TriggerType;
        private System.Windows.Forms.ColumnHeader TriggerSubType;
        private System.Windows.Forms.ColumnHeader TriggerHasScript;
        private System.Windows.Forms.Label Lbl_TriggerIndex;
        private System.Windows.Forms.Label Lbl_TriggerTypeDesc;
        private System.Windows.Forms.Label Lbl_TriggerSubTypeDesc;
        private System.Windows.Forms.Label Lbl_TriggerType;
        private System.Windows.Forms.Label Lbl_TriggerSubType;
        private System.Windows.Forms.Label Lbl_GameFlagsDesc;
        private System.Windows.Forms.Label Lbl_GameFlags;
        private System.Windows.Forms.GroupBox Box_TriggerInfo;
        private System.Windows.Forms.Label Lbl_TrigFlags;
        private System.Windows.Forms.Label Lbl_TrigFlagsDesc;
        private System.Windows.Forms.Label Lbl_PosDesc;
        private System.Windows.Forms.Label Lbl_PosZ;
        private System.Windows.Forms.Label Lbl_PosZDesc;
        private System.Windows.Forms.Label Lbl_PosY;
        private System.Windows.Forms.Label Lbl_PosYDesc;
        private System.Windows.Forms.Label Lbl_PosX;
        private System.Windows.Forms.Label Lbl_PosXDesc;
        private System.Windows.Forms.Label Lbl_RotZ;
        private System.Windows.Forms.Label Lbl_RotZDesc;
        private System.Windows.Forms.Label Lbl_RotY;
        private System.Windows.Forms.Label Lbl_RotYDesc;
        private System.Windows.Forms.Label Lbl_RotX;
        private System.Windows.Forms.Label Lbl_RotXDesc;
        private System.Windows.Forms.Label Lbl_RotDesc;
        private System.Windows.Forms.Label Lbl_ScaleZ;
        private System.Windows.Forms.Label Lbl_ScaleZDesc;
        private System.Windows.Forms.Label Lbl_ScaleY;
        private System.Windows.Forms.Label Lbl_ScaleYDesc;
        private System.Windows.Forms.Label Lbl_ScaleX;
        private System.Windows.Forms.Label Lbl_ScaleXDesc;
        private System.Windows.Forms.Label Lbl_ScaleDesc;
        private System.Windows.Forms.GroupBox Box_TriggerData;
        private System.Windows.Forms.Label Lbl_TriggerData;
        private System.Windows.Forms.GroupBox Box_TriggerLinks;
        private System.Windows.Forms.Label Lbl_TriggerLinks;
        private System.Windows.Forms.Label Lbl_Tint;
        private System.Windows.Forms.Label Lbl_TintDesc;
        private System.Windows.Forms.Label Lbl_GeoHash;
        private System.Windows.Forms.Label Lbl_GeoHashDesc;
        private System.Windows.Forms.Label Lbl_GFXHash;
        private System.Windows.Forms.Label Lbl_GFXHashDesc;
        private System.Windows.Forms.Button Btn_ViewGameScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_LoadFile;
        private System.Windows.Forms.PictureBox PicBox_TintColour;
        private System.Windows.Forms.CheckBox Check_OnlyScripted;
        private System.Windows.Forms.ColumnHeader MapNumTriggers;
        private System.Windows.Forms.Label Lbl_OpenedFileName;
        private System.Windows.Forms.Label Lbl_GameScript;
        private System.Windows.Forms.Label Lbl_GameScriptDesc;
        private System.Windows.Forms.CheckBox Check_ShowDataInHex;
        private System.Windows.Forms.Label Lbl_MapName;
        private System.Windows.Forms.Label Lbl_Range;
        private System.Windows.Forms.Label Lbl_RangeDesc;
        private System.Windows.Forms.ToolTip ToolTips;
    }
}

