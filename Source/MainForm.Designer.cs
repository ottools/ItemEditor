namespace ItemEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.itemsListBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.appearanceGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.candidatesButton = new DarkUI.Controls.DarkButton();
            this.currentSpriteLabel = new DarkUI.Controls.DarkLabel();
            this.previousLabel = new DarkUI.Controls.DarkLabel();
            this.serverIdLbl = new DarkUI.Controls.DarkLabel();
            this.clientIdUpDown = new DarkUI.Controls.DarkNumericUpDown();
            this.sidLabel = new DarkUI.Controls.DarkLabel();
            this.cidLabel = new DarkUI.Controls.DarkLabel();
            this.typeCombo = new DarkUI.Controls.DarkComboBox();
            this.typeLabel = new DarkUI.Controls.DarkLabel();
            this.optionsGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.stackOrderComboBox = new DarkUI.Controls.DarkComboBox();
            this.stackOrderLabel = new DarkUI.Controls.DarkLabel();
            this.wareIdText = new DarkUI.Controls.DarkTextBox();
            this.wareIdLabel = new DarkUI.Controls.DarkLabel();
            this.nameText = new DarkUI.Controls.DarkTextBox();
            this.nameLabel = new DarkUI.Controls.DarkLabel();
            this.maxReadWriteCharsLabel = new DarkUI.Controls.DarkLabel();
            this.maxReadWriteCharsText = new DarkUI.Controls.DarkTextBox();
            this.maxReadCharsLabel = new DarkUI.Controls.DarkLabel();
            this.maxReadCharsText = new DarkUI.Controls.DarkTextBox();
            this.minimapColorLabel = new DarkUI.Controls.DarkLabel();
            this.minimapColorText = new DarkUI.Controls.DarkTextBox();
            this.lightColorLabel = new DarkUI.Controls.DarkLabel();
            this.lightColorText = new DarkUI.Controls.DarkTextBox();
            this.lightLevelLabel = new DarkUI.Controls.DarkLabel();
            this.lightLevelText = new DarkUI.Controls.DarkTextBox();
            this.groundSpeedLabel = new DarkUI.Controls.DarkLabel();
            this.groundSpeedText = new DarkUI.Controls.DarkTextBox();
            this.ignoreLookCheck = new DarkUI.Controls.DarkCheckBox();
            this.readableCheck = new DarkUI.Controls.DarkCheckBox();
            this.stackableCheck = new DarkUI.Controls.DarkCheckBox();
            this.hookEastCheck = new DarkUI.Controls.DarkCheckBox();
            this.rotatableCheck = new DarkUI.Controls.DarkCheckBox();
            this.useableCheck = new DarkUI.Controls.DarkCheckBox();
            this.hangableCheck = new DarkUI.Controls.DarkCheckBox();
            this.hasElevationCheck = new DarkUI.Controls.DarkCheckBox();
            this.movableCheck = new DarkUI.Controls.DarkCheckBox();
            this.blockPathfinderCheck = new DarkUI.Controls.DarkCheckBox();
            this.hookSouthCheck = new DarkUI.Controls.DarkCheckBox();
            this.fullGroundCheck = new DarkUI.Controls.DarkCheckBox();
            this.pickupableCheck = new DarkUI.Controls.DarkCheckBox();
            this.blockMissilesCheck = new DarkUI.Controls.DarkCheckBox();
            this.unpassableCheck = new DarkUI.Controls.DarkCheckBox();
            this.statusBar = new DarkUI.Controls.DarkStatusStrip();
            this.menuBar = new DarkUI.Controls.DarkMenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.filePreferencesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCreateItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDuplicateItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editReloadItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editFindItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.editCreateMissingItemsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewShowOnlyMismatchedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewShowDecaptedItemsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewUpdateItemsListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsReloadItemAttributesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsCompareOtbFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsUpdateVersionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputTextBox = new DarkUI.Controls.DarkTextBox();
            this.loadingItemsProgressBar = new System.Windows.Forms.ProgressBar();
            this.itemsCountLabel = new DarkUI.Controls.DarkLabel();
            this.separatorLabel1 = new DarkUI.Controls.DarkLabel();
            this.separatorLabel2 = new DarkUI.Controls.DarkLabel();
            this.toolStrip = new DarkUI.Controls.DarkToolStrip();
            this.toolStripNewButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOpenButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSaveAsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCompareButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripFindItemButton = new System.Windows.Forms.ToolStripButton();
            this.candidatesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.candidatesDropDown = new System.Windows.Forms.ToolStripDropDown();
            this.newItemButton = new DarkUI.Controls.DarkButton();
            this.duplicateItemButton = new DarkUI.Controls.DarkButton();
            this.reloadItemButton = new DarkUI.Controls.DarkButton();
            this.findItemButton = new DarkUI.Controls.DarkButton();
            this.label1 = new DarkUI.Controls.DarkLabel();
            this.forceUseCheckBox = new DarkUI.Controls.DarkCheckBox();
            this.previousPictureBox = new ItemEditor.Controls.ClientItemView();
            this.pictureBox = new ItemEditor.Controls.ClientItemView();
            this.serverItemListBox = new ItemEditor.Controls.ServerItemListBox();
            this.candidatesPictureBox1 = new ItemEditor.Controls.ClientItemView();
            this.candidatesPictureBox2 = new ItemEditor.Controls.ClientItemView();
            this.candidatesPictureBox3 = new ItemEditor.Controls.ClientItemView();
            this.candidatesPictureBox4 = new ItemEditor.Controls.ClientItemView();
            this.candidatesPictureBox5 = new ItemEditor.Controls.ClientItemView();
            this.appearanceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientIdUpDown)).BeginInit();
            this.optionsGroupBox.SuspendLayout();
            this.menuBar.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.candidatesTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;
            // 
            // itemsListBoxContextMenu
            // 
            this.itemsListBoxContextMenu.Name = "itemsListBoxContextMenu";
            this.itemsListBoxContextMenu.Size = new System.Drawing.Size(61, 4);
            this.itemsListBoxContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ItemsListBoxContextMenu_Opening);
            this.itemsListBoxContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ItemsListBoxContextMenu_ItemClicked);
            // 
            // appearanceGroupBox
            // 
            this.appearanceGroupBox.Controls.Add(this.candidatesButton);
            this.appearanceGroupBox.Controls.Add(this.currentSpriteLabel);
            this.appearanceGroupBox.Controls.Add(this.previousLabel);
            this.appearanceGroupBox.Controls.Add(this.previousPictureBox);
            this.appearanceGroupBox.Controls.Add(this.pictureBox);
            this.appearanceGroupBox.Controls.Add(this.serverIdLbl);
            this.appearanceGroupBox.Controls.Add(this.clientIdUpDown);
            this.appearanceGroupBox.Controls.Add(this.sidLabel);
            this.appearanceGroupBox.Controls.Add(this.cidLabel);
            this.appearanceGroupBox.Enabled = false;
            this.appearanceGroupBox.Location = new System.Drawing.Point(252, 53);
            this.appearanceGroupBox.Name = "appearanceGroupBox";
            this.appearanceGroupBox.Size = new System.Drawing.Size(89, 309);
            this.appearanceGroupBox.TabIndex = 35;
            this.appearanceGroupBox.TabStop = false;
            this.appearanceGroupBox.Text = "Appearance";
            // 
            // candidatesButton
            // 
            this.candidatesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.candidatesButton.Location = new System.Drawing.Point(11, 277);
            this.candidatesButton.Name = "candidatesButton";
            this.candidatesButton.Size = new System.Drawing.Size(68, 22);
            this.candidatesButton.TabIndex = 17;
            this.candidatesButton.Text = "Candidates";
            this.candidatesButton.Click += new System.EventHandler(this.CandidatesButton_Click);
            // 
            // currentSpriteLabel
            // 
            this.currentSpriteLabel.AutoSize = true;
            this.currentSpriteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.currentSpriteLabel.Location = new System.Drawing.Point(11, 105);
            this.currentSpriteLabel.Name = "currentSpriteLabel";
            this.currentSpriteLabel.Size = new System.Drawing.Size(44, 13);
            this.currentSpriteLabel.TabIndex = 15;
            this.currentSpriteLabel.Text = "Current:";
            // 
            // previousLabel
            // 
            this.previousLabel.AutoSize = true;
            this.previousLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.previousLabel.Location = new System.Drawing.Point(11, 20);
            this.previousLabel.Name = "previousLabel";
            this.previousLabel.Size = new System.Drawing.Size(51, 13);
            this.previousLabel.TabIndex = 14;
            this.previousLabel.Text = "Previous:";
            // 
            // serverIdLbl
            // 
            this.serverIdLbl.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.serverIdLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverIdLbl.Location = new System.Drawing.Point(12, 206);
            this.serverIdLbl.Name = "serverIdLbl";
            this.serverIdLbl.Size = new System.Drawing.Size(66, 20);
            this.serverIdLbl.TabIndex = 9;
            this.serverIdLbl.Text = "0";
            this.serverIdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clientIdUpDown
            // 
            this.clientIdUpDown.Location = new System.Drawing.Point(12, 248);
            this.clientIdUpDown.Name = "clientIdUpDown";
            this.clientIdUpDown.Size = new System.Drawing.Size(66, 20);
            this.clientIdUpDown.TabIndex = 2;
            this.clientIdUpDown.ValueChanged += new System.EventHandler(this.ClientIdUpDown_ValueChanged);
            // 
            // sidLabel
            // 
            this.sidLabel.AutoSize = true;
            this.sidLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.sidLabel.Location = new System.Drawing.Point(10, 190);
            this.sidLabel.Name = "sidLabel";
            this.sidLabel.Size = new System.Drawing.Size(55, 13);
            this.sidLabel.TabIndex = 11;
            this.sidLabel.Text = "Server ID:";
            // 
            // cidLabel
            // 
            this.cidLabel.AutoSize = true;
            this.cidLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.cidLabel.Location = new System.Drawing.Point(11, 232);
            this.cidLabel.Name = "cidLabel";
            this.cidLabel.Size = new System.Drawing.Size(50, 13);
            this.cidLabel.TabIndex = 12;
            this.cidLabel.Text = "Client ID:";
            // 
            // typeCombo
            // 
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(268, 278);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(147, 21);
            this.typeCombo.TabIndex = 3;
            this.typeCombo.SelectedIndexChanged += new System.EventHandler(this.TypeCombo_SelectedIndexChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(228, 282);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(34, 13);
            this.typeLabel.TabIndex = 34;
            this.typeLabel.Text = "Type:";
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.forceUseCheckBox);
            this.optionsGroupBox.Controls.Add(this.stackOrderComboBox);
            this.optionsGroupBox.Controls.Add(this.stackOrderLabel);
            this.optionsGroupBox.Controls.Add(this.wareIdText);
            this.optionsGroupBox.Controls.Add(this.typeCombo);
            this.optionsGroupBox.Controls.Add(this.wareIdLabel);
            this.optionsGroupBox.Controls.Add(this.nameText);
            this.optionsGroupBox.Controls.Add(this.typeLabel);
            this.optionsGroupBox.Controls.Add(this.nameLabel);
            this.optionsGroupBox.Controls.Add(this.maxReadWriteCharsLabel);
            this.optionsGroupBox.Controls.Add(this.maxReadWriteCharsText);
            this.optionsGroupBox.Controls.Add(this.maxReadCharsLabel);
            this.optionsGroupBox.Controls.Add(this.maxReadCharsText);
            this.optionsGroupBox.Controls.Add(this.minimapColorLabel);
            this.optionsGroupBox.Controls.Add(this.minimapColorText);
            this.optionsGroupBox.Controls.Add(this.lightColorLabel);
            this.optionsGroupBox.Controls.Add(this.lightColorText);
            this.optionsGroupBox.Controls.Add(this.lightLevelLabel);
            this.optionsGroupBox.Controls.Add(this.lightLevelText);
            this.optionsGroupBox.Controls.Add(this.groundSpeedLabel);
            this.optionsGroupBox.Controls.Add(this.groundSpeedText);
            this.optionsGroupBox.Controls.Add(this.ignoreLookCheck);
            this.optionsGroupBox.Controls.Add(this.readableCheck);
            this.optionsGroupBox.Controls.Add(this.stackableCheck);
            this.optionsGroupBox.Controls.Add(this.hookEastCheck);
            this.optionsGroupBox.Controls.Add(this.rotatableCheck);
            this.optionsGroupBox.Controls.Add(this.useableCheck);
            this.optionsGroupBox.Controls.Add(this.hangableCheck);
            this.optionsGroupBox.Controls.Add(this.hasElevationCheck);
            this.optionsGroupBox.Controls.Add(this.movableCheck);
            this.optionsGroupBox.Controls.Add(this.blockPathfinderCheck);
            this.optionsGroupBox.Controls.Add(this.hookSouthCheck);
            this.optionsGroupBox.Controls.Add(this.fullGroundCheck);
            this.optionsGroupBox.Controls.Add(this.pickupableCheck);
            this.optionsGroupBox.Controls.Add(this.blockMissilesCheck);
            this.optionsGroupBox.Controls.Add(this.unpassableCheck);
            this.optionsGroupBox.Enabled = false;
            this.optionsGroupBox.Location = new System.Drawing.Point(349, 53);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(425, 309);
            this.optionsGroupBox.TabIndex = 1;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Attributes";
            // 
            // stackOrderComboBox
            // 
            this.stackOrderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stackOrderComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stackOrderComboBox.FormattingEnabled = true;
            this.stackOrderComboBox.Location = new System.Drawing.Point(268, 219);
            this.stackOrderComboBox.Name = "stackOrderComboBox";
            this.stackOrderComboBox.Size = new System.Drawing.Size(147, 21);
            this.stackOrderComboBox.TabIndex = 49;
            this.stackOrderComboBox.SelectedIndexChanged += new System.EventHandler(this.StackOrderComboBox_SelectedIndexChanged);
            // 
            // stackOrderLabel
            // 
            this.stackOrderLabel.AutoSize = true;
            this.stackOrderLabel.Location = new System.Drawing.Point(195, 222);
            this.stackOrderLabel.Name = "stackOrderLabel";
            this.stackOrderLabel.Size = new System.Drawing.Size(67, 13);
            this.stackOrderLabel.TabIndex = 50;
            this.stackOrderLabel.Text = "Stack Order:";
            // 
            // wareIdText
            // 
            this.wareIdText.Location = new System.Drawing.Point(360, 133);
            this.wareIdText.Name = "wareIdText";
            this.wareIdText.Size = new System.Drawing.Size(55, 20);
            this.wareIdText.TabIndex = 48;
            this.wareIdText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // wareIdLabel
            // 
            this.wareIdLabel.AutoSize = true;
            this.wareIdLabel.Location = new System.Drawing.Point(306, 137);
            this.wareIdLabel.Name = "wareIdLabel";
            this.wareIdLabel.Size = new System.Drawing.Size(50, 13);
            this.wareIdLabel.TabIndex = 47;
            this.wareIdLabel.Text = "Ware ID:";
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(268, 249);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(147, 20);
            this.nameText.TabIndex = 46;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(224, 252);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 45;
            this.nameLabel.Text = "Name:";
            // 
            // maxReadWriteCharsLabel
            // 
            this.maxReadWriteCharsLabel.AutoSize = true;
            this.maxReadWriteCharsLabel.Location = new System.Drawing.Point(248, 192);
            this.maxReadWriteCharsLabel.Name = "maxReadWriteCharsLabel";
            this.maxReadWriteCharsLabel.Size = new System.Drawing.Size(108, 13);
            this.maxReadWriteCharsLabel.TabIndex = 44;
            this.maxReadWriteCharsLabel.Text = "Read / Write Length:";
            // 
            // maxReadWriteCharsText
            // 
            this.maxReadWriteCharsText.Location = new System.Drawing.Point(360, 188);
            this.maxReadWriteCharsText.Name = "maxReadWriteCharsText";
            this.maxReadWriteCharsText.Size = new System.Drawing.Size(55, 20);
            this.maxReadWriteCharsText.TabIndex = 42;
            this.maxReadWriteCharsText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // maxReadCharsLabel
            // 
            this.maxReadCharsLabel.AutoSize = true;
            this.maxReadCharsLabel.Location = new System.Drawing.Point(284, 164);
            this.maxReadCharsLabel.Name = "maxReadCharsLabel";
            this.maxReadCharsLabel.Size = new System.Drawing.Size(72, 13);
            this.maxReadCharsLabel.TabIndex = 43;
            this.maxReadCharsLabel.Text = "Read Length:";
            // 
            // maxReadCharsText
            // 
            this.maxReadCharsText.Location = new System.Drawing.Point(360, 160);
            this.maxReadCharsText.Name = "maxReadCharsText";
            this.maxReadCharsText.Size = new System.Drawing.Size(55, 20);
            this.maxReadCharsText.TabIndex = 41;
            this.maxReadCharsText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // minimapColorLabel
            // 
            this.minimapColorLabel.AutoSize = true;
            this.minimapColorLabel.Location = new System.Drawing.Point(280, 57);
            this.minimapColorLabel.Name = "minimapColorLabel";
            this.minimapColorLabel.Size = new System.Drawing.Size(76, 13);
            this.minimapColorLabel.TabIndex = 40;
            this.minimapColorLabel.Text = "Minimap Color:";
            // 
            // minimapColorText
            // 
            this.minimapColorText.Location = new System.Drawing.Point(359, 53);
            this.minimapColorText.Name = "minimapColorText";
            this.minimapColorText.Size = new System.Drawing.Size(56, 20);
            this.minimapColorText.TabIndex = 21;
            this.minimapColorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // lightColorLabel
            // 
            this.lightColorLabel.AutoSize = true;
            this.lightColorLabel.Location = new System.Drawing.Point(296, 111);
            this.lightColorLabel.Name = "lightColorLabel";
            this.lightColorLabel.Size = new System.Drawing.Size(60, 13);
            this.lightColorLabel.TabIndex = 38;
            this.lightColorLabel.Text = "Light Color:";
            // 
            // lightColorText
            // 
            this.lightColorText.Location = new System.Drawing.Point(359, 107);
            this.lightColorText.Name = "lightColorText";
            this.lightColorText.Size = new System.Drawing.Size(56, 20);
            this.lightColorText.TabIndex = 23;
            this.lightColorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // lightLevelLabel
            // 
            this.lightLevelLabel.AutoSize = true;
            this.lightLevelLabel.Location = new System.Drawing.Point(294, 84);
            this.lightLevelLabel.Name = "lightLevelLabel";
            this.lightLevelLabel.Size = new System.Drawing.Size(62, 13);
            this.lightLevelLabel.TabIndex = 36;
            this.lightLevelLabel.Text = "Light Level:";
            // 
            // lightLevelText
            // 
            this.lightLevelText.Location = new System.Drawing.Point(359, 80);
            this.lightLevelText.Name = "lightLevelText";
            this.lightLevelText.Size = new System.Drawing.Size(56, 20);
            this.lightLevelText.TabIndex = 22;
            this.lightLevelText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // groundSpeedLabel
            // 
            this.groundSpeedLabel.AutoSize = true;
            this.groundSpeedLabel.Location = new System.Drawing.Point(277, 30);
            this.groundSpeedLabel.Name = "groundSpeedLabel";
            this.groundSpeedLabel.Size = new System.Drawing.Size(79, 13);
            this.groundSpeedLabel.TabIndex = 28;
            this.groundSpeedLabel.Text = "Ground Speed:";
            // 
            // groundSpeedText
            // 
            this.groundSpeedText.Location = new System.Drawing.Point(359, 26);
            this.groundSpeedText.Name = "groundSpeedText";
            this.groundSpeedText.Size = new System.Drawing.Size(56, 20);
            this.groundSpeedText.TabIndex = 19;
            this.groundSpeedText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // ignoreLookCheck
            // 
            this.ignoreLookCheck.AutoSize = true;
            this.ignoreLookCheck.Location = new System.Drawing.Point(134, 49);
            this.ignoreLookCheck.Name = "ignoreLookCheck";
            this.ignoreLookCheck.Size = new System.Drawing.Size(83, 17);
            this.ignoreLookCheck.TabIndex = 16;
            this.ignoreLookCheck.Text = "Ignore Look";
            // 
            // readableCheck
            // 
            this.readableCheck.AutoSize = true;
            this.readableCheck.Location = new System.Drawing.Point(134, 72);
            this.readableCheck.Name = "readableCheck";
            this.readableCheck.Size = new System.Drawing.Size(72, 17);
            this.readableCheck.TabIndex = 18;
            this.readableCheck.Text = "Readable";
            // 
            // stackableCheck
            // 
            this.stackableCheck.AutoSize = true;
            this.stackableCheck.Location = new System.Drawing.Point(14, 140);
            this.stackableCheck.Name = "stackableCheck";
            this.stackableCheck.Size = new System.Drawing.Size(74, 17);
            this.stackableCheck.TabIndex = 13;
            this.stackableCheck.Text = "Stackable";
            // 
            // hookEastCheck
            // 
            this.hookEastCheck.AutoSize = true;
            this.hookEastCheck.Location = new System.Drawing.Point(14, 279);
            this.hookEastCheck.Name = "hookEastCheck";
            this.hookEastCheck.Size = new System.Drawing.Size(76, 17);
            this.hookEastCheck.TabIndex = 15;
            this.hookEastCheck.Text = "Hook East";
            // 
            // rotatableCheck
            // 
            this.rotatableCheck.AutoSize = true;
            this.rotatableCheck.Location = new System.Drawing.Point(14, 210);
            this.rotatableCheck.Name = "rotatableCheck";
            this.rotatableCheck.Size = new System.Drawing.Size(72, 17);
            this.rotatableCheck.TabIndex = 12;
            this.rotatableCheck.Text = "Rotatable";
            // 
            // useableCheck
            // 
            this.useableCheck.AutoSize = true;
            this.useableCheck.Location = new System.Drawing.Point(14, 187);
            this.useableCheck.Name = "useableCheck";
            this.useableCheck.Size = new System.Drawing.Size(70, 17);
            this.useableCheck.TabIndex = 11;
            this.useableCheck.Text = "Multi Use";
            // 
            // hangableCheck
            // 
            this.hangableCheck.AutoSize = true;
            this.hangableCheck.Location = new System.Drawing.Point(14, 233);
            this.hangableCheck.Name = "hangableCheck";
            this.hangableCheck.Size = new System.Drawing.Size(72, 17);
            this.hangableCheck.TabIndex = 10;
            this.hangableCheck.Text = "Hangable";
            // 
            // hasElevationCheck
            // 
            this.hasElevationCheck.AutoSize = true;
            this.hasElevationCheck.Location = new System.Drawing.Point(134, 26);
            this.hasElevationCheck.Name = "hasElevationCheck";
            this.hasElevationCheck.Size = new System.Drawing.Size(92, 17);
            this.hasElevationCheck.TabIndex = 8;
            this.hasElevationCheck.Text = "Has Elevation";
            // 
            // movableCheck
            // 
            this.movableCheck.AutoSize = true;
            this.movableCheck.Location = new System.Drawing.Point(14, 49);
            this.movableCheck.Name = "movableCheck";
            this.movableCheck.Size = new System.Drawing.Size(67, 17);
            this.movableCheck.TabIndex = 7;
            this.movableCheck.Text = "Movable";
            // 
            // blockPathfinderCheck
            // 
            this.blockPathfinderCheck.AutoSize = true;
            this.blockPathfinderCheck.Location = new System.Drawing.Point(14, 95);
            this.blockPathfinderCheck.Name = "blockPathfinderCheck";
            this.blockPathfinderCheck.Size = new System.Drawing.Size(104, 17);
            this.blockPathfinderCheck.TabIndex = 6;
            this.blockPathfinderCheck.Text = "Block Pathfinder";
            // 
            // hookSouthCheck
            // 
            this.hookSouthCheck.AutoSize = true;
            this.hookSouthCheck.Location = new System.Drawing.Point(14, 256);
            this.hookSouthCheck.Name = "hookSouthCheck";
            this.hookSouthCheck.Size = new System.Drawing.Size(83, 17);
            this.hookSouthCheck.TabIndex = 14;
            this.hookSouthCheck.Text = "Hook South";
            // 
            // fullGroundCheck
            // 
            this.fullGroundCheck.AutoSize = true;
            this.fullGroundCheck.Location = new System.Drawing.Point(134, 95);
            this.fullGroundCheck.Name = "fullGroundCheck";
            this.fullGroundCheck.Size = new System.Drawing.Size(80, 17);
            this.fullGroundCheck.TabIndex = 14;
            this.fullGroundCheck.Text = "Full Ground";
            // 
            // pickupableCheck
            // 
            this.pickupableCheck.AutoSize = true;
            this.pickupableCheck.Location = new System.Drawing.Point(14, 118);
            this.pickupableCheck.Name = "pickupableCheck";
            this.pickupableCheck.Size = new System.Drawing.Size(79, 17);
            this.pickupableCheck.TabIndex = 9;
            this.pickupableCheck.Text = "Pickupable";
            // 
            // blockMissilesCheck
            // 
            this.blockMissilesCheck.AutoSize = true;
            this.blockMissilesCheck.Location = new System.Drawing.Point(14, 72);
            this.blockMissilesCheck.Name = "blockMissilesCheck";
            this.blockMissilesCheck.Size = new System.Drawing.Size(92, 17);
            this.blockMissilesCheck.TabIndex = 5;
            this.blockMissilesCheck.Text = "Block Missiles";
            // 
            // unpassableCheck
            // 
            this.unpassableCheck.AutoSize = true;
            this.unpassableCheck.Location = new System.Drawing.Point(14, 26);
            this.unpassableCheck.Name = "unpassableCheck";
            this.unpassableCheck.Size = new System.Drawing.Size(82, 17);
            this.unpassableCheck.TabIndex = 4;
            this.unpassableCheck.Text = "Unpassable";
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 539);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(784, 22);
            this.statusBar.Stretch = false;
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusBar";
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.viewMenuItem,
            this.toolsMenuItem,
            this.helpMenuItem});
            this.menuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(784, 24);
            this.menuBar.TabIndex = 3;
            this.menuBar.Text = "menuStrip";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenuItem,
            this.fileOpenMenuItem,
            this.fileSaveMenuItem,
            this.fileSaveAsMenuItem,
            this.toolStripSeparator3,
            this.filePreferencesMenuItem,
            this.separatorMenuItem1,
            this.fileExitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // fileNewMenuItem
            // 
            this.fileNewMenuItem.Image = global::ItemEditor.Properties.Resources.NewIcon;
            this.fileNewMenuItem.Name = "fileNewMenuItem";
            this.fileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileNewMenuItem.Size = new System.Drawing.Size(193, 22);
            this.fileNewMenuItem.Text = "&New";
            this.fileNewMenuItem.Click += new System.EventHandler(this.FileNewMenuItem_Click);
            // 
            // fileOpenMenuItem
            // 
            this.fileOpenMenuItem.Image = global::ItemEditor.Properties.Resources.OpenIcon;
            this.fileOpenMenuItem.Name = "fileOpenMenuItem";
            this.fileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpenMenuItem.Size = new System.Drawing.Size(193, 22);
            this.fileOpenMenuItem.Text = "&Open...";
            this.fileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // fileSaveMenuItem
            // 
            this.fileSaveMenuItem.Enabled = false;
            this.fileSaveMenuItem.Image = global::ItemEditor.Properties.Resources.SaveIcon;
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSaveMenuItem.Size = new System.Drawing.Size(193, 22);
            this.fileSaveMenuItem.Text = "&Save";
            this.fileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // fileSaveAsMenuItem
            // 
            this.fileSaveAsMenuItem.Enabled = false;
            this.fileSaveAsMenuItem.Image = global::ItemEditor.Properties.Resources.SaveAsIcon;
            this.fileSaveAsMenuItem.Name = "fileSaveAsMenuItem";
            this.fileSaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.fileSaveAsMenuItem.Size = new System.Drawing.Size(193, 22);
            this.fileSaveAsMenuItem.Text = "S&ave as...";
            this.fileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(190, 6);
            // 
            // filePreferencesMenuItem
            // 
            this.filePreferencesMenuItem.Name = "filePreferencesMenuItem";
            this.filePreferencesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.filePreferencesMenuItem.Size = new System.Drawing.Size(193, 22);
            this.filePreferencesMenuItem.Text = "Preferences";
            this.filePreferencesMenuItem.Click += new System.EventHandler(this.FilePreferencesMenuItem_Click);
            // 
            // separatorMenuItem1
            // 
            this.separatorMenuItem1.Name = "separatorMenuItem1";
            this.separatorMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.Size = new System.Drawing.Size(193, 22);
            this.fileExitMenuItem.Text = "&Exit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCreateItemMenuItem,
            this.editDuplicateItemMenuItem,
            this.editReloadItemMenuItem,
            this.separatorMenuItem4,
            this.editCreateMissingItemsMenuItem,
            this.toolStripSeparator2,
            this.editFindItemMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "&Edit";
            // 
            // editCreateItemMenuItem
            // 
            this.editCreateItemMenuItem.Enabled = false;
            this.editCreateItemMenuItem.Image = global::ItemEditor.Properties.Resources.NewIcon;
            this.editCreateItemMenuItem.Name = "editCreateItemMenuItem";
            this.editCreateItemMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.editCreateItemMenuItem.Size = new System.Drawing.Size(193, 22);
            this.editCreateItemMenuItem.Text = "&Create Item";
            this.editCreateItemMenuItem.Click += new System.EventHandler(this.EditCreateItemMenuItem_Click);
            // 
            // editDuplicateItemMenuItem
            // 
            this.editDuplicateItemMenuItem.Enabled = false;
            this.editDuplicateItemMenuItem.Image = global::ItemEditor.Properties.Resources.DuplicateIcon;
            this.editDuplicateItemMenuItem.Name = "editDuplicateItemMenuItem";
            this.editDuplicateItemMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.editDuplicateItemMenuItem.Size = new System.Drawing.Size(193, 22);
            this.editDuplicateItemMenuItem.Text = "&Duplicate Item";
            this.editDuplicateItemMenuItem.Click += new System.EventHandler(this.EditDuplicateItemMenuItem_Click);
            // 
            // editReloadItemMenuItem
            // 
            this.editReloadItemMenuItem.Enabled = false;
            this.editReloadItemMenuItem.Image = global::ItemEditor.Properties.Resources.ReloadIcon;
            this.editReloadItemMenuItem.Name = "editReloadItemMenuItem";
            this.editReloadItemMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.editReloadItemMenuItem.Size = new System.Drawing.Size(193, 22);
            this.editReloadItemMenuItem.Text = "&Reload Item";
            this.editReloadItemMenuItem.Click += new System.EventHandler(this.EditReloadItemMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // editFindItemMenuItem
            // 
            this.editFindItemMenuItem.Enabled = false;
            this.editFindItemMenuItem.Image = global::ItemEditor.Properties.Resources.FindIcon;
            this.editFindItemMenuItem.Name = "editFindItemMenuItem";
            this.editFindItemMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.editFindItemMenuItem.Size = new System.Drawing.Size(193, 22);
            this.editFindItemMenuItem.Text = "&Find Item";
            this.editFindItemMenuItem.Click += new System.EventHandler(this.FindItemButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.separatorMenuItem4.Name = "separatorMenuItem4";
            this.separatorMenuItem4.Size = new System.Drawing.Size(190, 6);
            // 
            // editCreateMissingItemsMenuItem
            // 
            this.editCreateMissingItemsMenuItem.Enabled = false;
            this.editCreateMissingItemsMenuItem.Name = "editCreateMissingItemsMenuItem";
            this.editCreateMissingItemsMenuItem.Size = new System.Drawing.Size(193, 22);
            this.editCreateMissingItemsMenuItem.Text = "Create Missing Items";
            this.editCreateMissingItemsMenuItem.Click += new System.EventHandler(this.EditCreateMissingItemsMenu_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewShowOnlyMismatchedMenuItem,
            this.viewShowDecaptedItemsMenuItem,
            this.viewUpdateItemsListMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "&View";
            // 
            // viewShowOnlyMismatchedMenuItem
            // 
            this.viewShowOnlyMismatchedMenuItem.CheckOnClick = true;
            this.viewShowOnlyMismatchedMenuItem.Enabled = false;
            this.viewShowOnlyMismatchedMenuItem.Name = "viewShowOnlyMismatchedMenuItem";
            this.viewShowOnlyMismatchedMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewShowOnlyMismatchedMenuItem.Text = "&Show Mismatched Items";
            this.viewShowOnlyMismatchedMenuItem.Click += new System.EventHandler(this.ShowOnlyUnmatchedToolStripMenuItem_Click);
            // 
            // viewShowDecaptedItemsMenuItem
            // 
            this.viewShowDecaptedItemsMenuItem.CheckOnClick = true;
            this.viewShowDecaptedItemsMenuItem.Enabled = false;
            this.viewShowDecaptedItemsMenuItem.Name = "viewShowDecaptedItemsMenuItem";
            this.viewShowDecaptedItemsMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewShowDecaptedItemsMenuItem.Text = "Show Deprecated Items";
            this.viewShowDecaptedItemsMenuItem.Click += new System.EventHandler(this.ViewShowDecaptedItemsMenuItem_Click);
            // 
            // viewUpdateItemsListMenuItem
            // 
            this.viewUpdateItemsListMenuItem.Enabled = false;
            this.viewUpdateItemsListMenuItem.Name = "viewUpdateItemsListMenuItem";
            this.viewUpdateItemsListMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewUpdateItemsListMenuItem.Text = "&Update Items List";
            this.viewUpdateItemsListMenuItem.Click += new System.EventHandler(this.ViewUpdateItemsListMenuItem_Click);
            // 
            // toolsMenuItem
            // 
            this.toolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsReloadItemAttributesMenuItem,
            this.separatorMenuItem2,
            this.toolsCompareOtbFilesMenuItem,
            this.toolsUpdateVersionMenuItem});
            this.toolsMenuItem.Name = "toolsMenuItem";
            this.toolsMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsMenuItem.Text = "&Tools";
            // 
            // toolsReloadItemAttributesMenuItem
            // 
            this.toolsReloadItemAttributesMenuItem.Enabled = false;
            this.toolsReloadItemAttributesMenuItem.Name = "toolsReloadItemAttributesMenuItem";
            this.toolsReloadItemAttributesMenuItem.Size = new System.Drawing.Size(192, 22);
            this.toolsReloadItemAttributesMenuItem.Text = "&Reload Item Attributes";
            this.toolsReloadItemAttributesMenuItem.Click += new System.EventHandler(this.ToolsReloadItemAttributesMenuItem_Click);
            // 
            // separatorMenuItem2
            // 
            this.separatorMenuItem2.Name = "separatorMenuItem2";
            this.separatorMenuItem2.Size = new System.Drawing.Size(189, 6);
            // 
            // toolsCompareOtbFilesMenuItem
            // 
            this.toolsCompareOtbFilesMenuItem.Image = global::ItemEditor.Properties.Resources.FormIcon;
            this.toolsCompareOtbFilesMenuItem.Name = "toolsCompareOtbFilesMenuItem";
            this.toolsCompareOtbFilesMenuItem.Size = new System.Drawing.Size(192, 22);
            this.toolsCompareOtbFilesMenuItem.Text = "&Compare OTB Files";
            this.toolsCompareOtbFilesMenuItem.Click += new System.EventHandler(this.ToolsCompareOtbFilesMenuItem_Click);
            // 
            // toolsUpdateVersionMenuItem
            // 
            this.toolsUpdateVersionMenuItem.Enabled = false;
            this.toolsUpdateVersionMenuItem.Name = "toolsUpdateVersionMenuItem";
            this.toolsUpdateVersionMenuItem.Size = new System.Drawing.Size(192, 22);
            this.toolsUpdateVersionMenuItem.Text = "&Update OTB Version";
            this.toolsUpdateVersionMenuItem.Click += new System.EventHandler(this.ToolsUpdateVersionMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // helpAboutMenuItem
            // 
            this.helpAboutMenuItem.Image = global::ItemEditor.Properties.Resources.InfoIcon;
            this.helpAboutMenuItem.Name = "helpAboutMenuItem";
            this.helpAboutMenuItem.Size = new System.Drawing.Size(165, 22);
            this.helpAboutMenuItem.Text = "&About ItemEditor";
            this.helpAboutMenuItem.Click += new System.EventHandler(this.HelpAboutMenuItem_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(251, 371);
            this.outputTextBox.MaxLength = 20032767;
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(525, 160);
            this.outputTextBox.TabIndex = 24;
            // 
            // loadingItemsProgressBar
            // 
            this.loadingItemsProgressBar.Location = new System.Drawing.Point(96, 543);
            this.loadingItemsProgressBar.Name = "loadingItemsProgressBar";
            this.loadingItemsProgressBar.Size = new System.Drawing.Size(100, 15);
            this.loadingItemsProgressBar.TabIndex = 25;
            this.loadingItemsProgressBar.Visible = false;
            // 
            // itemsCountLabel
            // 
            this.itemsCountLabel.Location = new System.Drawing.Point(6, 544);
            this.itemsCountLabel.Name = "itemsCountLabel";
            this.itemsCountLabel.Size = new System.Drawing.Size(74, 13);
            this.itemsCountLabel.TabIndex = 27;
            this.itemsCountLabel.Text = "0 Items";
            this.itemsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // separatorLabel1
            // 
            this.separatorLabel1.AutoSize = true;
            this.separatorLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLabel1.Location = new System.Drawing.Point(90, 543);
            this.separatorLabel1.Name = "separatorLabel1";
            this.separatorLabel1.Size = new System.Drawing.Size(2, 15);
            this.separatorLabel1.TabIndex = 28;
            // 
            // separatorLabel2
            // 
            this.separatorLabel2.AutoSize = true;
            this.separatorLabel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLabel2.Location = new System.Drawing.Point(201, 543);
            this.separatorLabel2.Name = "separatorLabel2";
            this.separatorLabel2.Size = new System.Drawing.Size(2, 15);
            this.separatorLabel2.TabIndex = 29;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripNewButton,
            this.toolStripOpenButton,
            this.toolStripSaveButton,
            this.toolStripSaveAsButton,
            this.toolStripSeparator1,
            this.toolStripCompareButton,
            this.toolStripFindItemButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(784, 25);
            this.toolStrip.TabIndex = 30;
            // 
            // toolStripNewButton
            // 
            this.toolStripNewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripNewButton.Image = global::ItemEditor.Properties.Resources.NewIcon;
            this.toolStripNewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNewButton.Name = "toolStripNewButton";
            this.toolStripNewButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripNewButton.Text = "New";
            this.toolStripNewButton.Click += new System.EventHandler(this.FileNewMenuItem_Click);
            // 
            // toolStripOpenButton
            // 
            this.toolStripOpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOpenButton.Image = global::ItemEditor.Properties.Resources.OpenIcon;
            this.toolStripOpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOpenButton.Name = "toolStripOpenButton";
            this.toolStripOpenButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripOpenButton.Text = "Open";
            this.toolStripOpenButton.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // toolStripSaveButton
            // 
            this.toolStripSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveButton.Enabled = false;
            this.toolStripSaveButton.Image = global::ItemEditor.Properties.Resources.SaveIcon;
            this.toolStripSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveButton.Name = "toolStripSaveButton";
            this.toolStripSaveButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripSaveButton.Text = "Save";
            this.toolStripSaveButton.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // toolStripSaveAsButton
            // 
            this.toolStripSaveAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveAsButton.Enabled = false;
            this.toolStripSaveAsButton.Image = global::ItemEditor.Properties.Resources.SaveAsIcon;
            this.toolStripSaveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveAsButton.Name = "toolStripSaveAsButton";
            this.toolStripSaveAsButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripSaveAsButton.Text = "Save As";
            this.toolStripSaveAsButton.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripCompareButton
            // 
            this.toolStripCompareButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCompareButton.Image = global::ItemEditor.Properties.Resources.FormIcon;
            this.toolStripCompareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCompareButton.Name = "toolStripCompareButton";
            this.toolStripCompareButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripCompareButton.Text = "Compare OTB Files";
            this.toolStripCompareButton.Click += new System.EventHandler(this.ToolsCompareOtbFilesMenuItem_Click);
            // 
            // toolStripFindItemButton
            // 
            this.toolStripFindItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFindItemButton.Enabled = false;
            this.toolStripFindItemButton.Image = global::ItemEditor.Properties.Resources.FindIcon;
            this.toolStripFindItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFindItemButton.Name = "toolStripFindItemButton";
            this.toolStripFindItemButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripFindItemButton.Text = "Find Item";
            this.toolStripFindItemButton.Click += new System.EventHandler(this.FindItemButton_Click);
            // 
            // candidatesTableLayoutPanel
            // 
            this.candidatesTableLayoutPanel.ColumnCount = 5;
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.candidatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.candidatesTableLayoutPanel.Controls.Add(this.candidatesPictureBox1, 0, 0);
            this.candidatesTableLayoutPanel.Controls.Add(this.candidatesPictureBox2, 1, 0);
            this.candidatesTableLayoutPanel.Controls.Add(this.candidatesPictureBox3, 2, 0);
            this.candidatesTableLayoutPanel.Controls.Add(this.candidatesPictureBox4, 3, 0);
            this.candidatesTableLayoutPanel.Controls.Add(this.candidatesPictureBox5, 4, 0);
            this.candidatesTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.candidatesTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.candidatesTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.candidatesTableLayoutPanel.Name = "candidatesTableLayoutPanel";
            this.candidatesTableLayoutPanel.RowCount = 1;
            this.candidatesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.candidatesTableLayoutPanel.Size = new System.Drawing.Size(320, 64);
            this.candidatesTableLayoutPanel.TabIndex = 0;
            // 
            // candidatesDropDown
            // 
            this.candidatesDropDown.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.candidatesDropDown.Name = "candidatesDropDown";
            this.candidatesDropDown.Size = new System.Drawing.Size(2, 4);
            // 
            // newItemButton
            // 
            this.newItemButton.Enabled = false;
            this.newItemButton.Image = global::ItemEditor.Properties.Resources.NewIcon;
            this.newItemButton.Location = new System.Drawing.Point(10, 506);
            this.newItemButton.Name = "newItemButton";
            this.newItemButton.Size = new System.Drawing.Size(25, 25);
            this.newItemButton.TabIndex = 36;
            this.newItemButton.Click += new System.EventHandler(this.NewItemButton_Click);
            // 
            // duplicateItemButton
            // 
            this.duplicateItemButton.Enabled = false;
            this.duplicateItemButton.Image = global::ItemEditor.Properties.Resources.DuplicateIcon;
            this.duplicateItemButton.Location = new System.Drawing.Point(40, 506);
            this.duplicateItemButton.Name = "duplicateItemButton";
            this.duplicateItemButton.Size = new System.Drawing.Size(25, 25);
            this.duplicateItemButton.TabIndex = 37;
            this.duplicateItemButton.Click += new System.EventHandler(this.DuplicateItemButton_Click);
            // 
            // reloadItemButton
            // 
            this.reloadItemButton.Enabled = false;
            this.reloadItemButton.Image = global::ItemEditor.Properties.Resources.ReloadIcon;
            this.reloadItemButton.Location = new System.Drawing.Point(71, 506);
            this.reloadItemButton.Name = "reloadItemButton";
            this.reloadItemButton.Size = new System.Drawing.Size(25, 25);
            this.reloadItemButton.TabIndex = 38;
            this.reloadItemButton.Click += new System.EventHandler(this.ReloadItemButton_Click);
            // 
            // findItemButton
            // 
            this.findItemButton.Enabled = false;
            this.findItemButton.Image = global::ItemEditor.Properties.Resources.FindIcon;
            this.findItemButton.Location = new System.Drawing.Point(114, 506);
            this.findItemButton.Name = "findItemButton";
            this.findItemButton.Size = new System.Drawing.Size(25, 25);
            this.findItemButton.TabIndex = 40;
            this.findItemButton.Click += new System.EventHandler(this.FindItemButton_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(104, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 20);
            this.label1.TabIndex = 41;
            // 
            // forceUseCheckBox
            // 
            this.forceUseCheckBox.AutoSize = true;
            this.forceUseCheckBox.Location = new System.Drawing.Point(14, 162);
            this.forceUseCheckBox.Name = "forceUseCheckBox";
            this.forceUseCheckBox.Size = new System.Drawing.Size(75, 17);
            this.forceUseCheckBox.TabIndex = 52;
            this.forceUseCheckBox.Text = "Force Use";
            // 
            // previousPictureBox
            // 
            this.previousPictureBox.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.previousPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previousPictureBox.ClientItem = null;
            this.previousPictureBox.Location = new System.Drawing.Point(13, 36);
            this.previousPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.previousPictureBox.Name = "previousPictureBox";
            this.previousPictureBox.Size = new System.Drawing.Size(64, 64);
            this.previousPictureBox.TabIndex = 13;
            this.previousPictureBox.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.ClientItem = null;
            this.pictureBox.Location = new System.Drawing.Point(13, 121);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(64, 64);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // serverItemListBox
            // 
            this.serverItemListBox.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.serverItemListBox.ContextMenuStrip = this.itemsListBoxContextMenu;
            this.serverItemListBox.Enabled = false;
            this.serverItemListBox.Location = new System.Drawing.Point(10, 59);
            this.serverItemListBox.Name = "serverItemListBox";
            this.serverItemListBox.Plugin = null;
            this.serverItemListBox.Size = new System.Drawing.Size(232, 440);
            this.serverItemListBox.TabIndex = 0;
            this.serverItemListBox.SelectedIndexChanged += ItemsListBox_SelectedIndexChanged;
            this.serverItemListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemsListBox_MouseDown);
            // 
            // candidatesPictureBox1
            // 
            this.candidatesPictureBox1.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.candidatesPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidatesPictureBox1.ClientItem = null;
            this.candidatesPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.candidatesPictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.candidatesPictureBox1.Name = "candidatesPictureBox1";
            this.candidatesPictureBox1.Size = new System.Drawing.Size(64, 64);
            this.candidatesPictureBox1.TabIndex = 0;
            this.candidatesPictureBox1.TabStop = false;
            this.candidatesPictureBox1.Tag = "1";
            this.candidatesPictureBox1.Click += new System.EventHandler(this.CandidatePictureBox_Click);
            // 
            // candidatesPictureBox2
            // 
            this.candidatesPictureBox2.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.candidatesPictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidatesPictureBox2.ClientItem = null;
            this.candidatesPictureBox2.Location = new System.Drawing.Point(64, 0);
            this.candidatesPictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.candidatesPictureBox2.Name = "candidatesPictureBox2";
            this.candidatesPictureBox2.Size = new System.Drawing.Size(64, 64);
            this.candidatesPictureBox2.TabIndex = 1;
            this.candidatesPictureBox2.TabStop = false;
            this.candidatesPictureBox2.Tag = "2";
            this.candidatesPictureBox2.Click += new System.EventHandler(this.CandidatePictureBox_Click);
            // 
            // candidatesPictureBox3
            // 
            this.candidatesPictureBox3.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.candidatesPictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidatesPictureBox3.ClientItem = null;
            this.candidatesPictureBox3.Location = new System.Drawing.Point(128, 0);
            this.candidatesPictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.candidatesPictureBox3.Name = "candidatesPictureBox3";
            this.candidatesPictureBox3.Size = new System.Drawing.Size(64, 64);
            this.candidatesPictureBox3.TabIndex = 2;
            this.candidatesPictureBox3.TabStop = false;
            this.candidatesPictureBox3.Tag = "3";
            this.candidatesPictureBox3.Click += new System.EventHandler(this.CandidatePictureBox_Click);
            // 
            // candidatesPictureBox4
            // 
            this.candidatesPictureBox4.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.candidatesPictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidatesPictureBox4.ClientItem = null;
            this.candidatesPictureBox4.Location = new System.Drawing.Point(192, 0);
            this.candidatesPictureBox4.Margin = new System.Windows.Forms.Padding(0);
            this.candidatesPictureBox4.Name = "candidatesPictureBox4";
            this.candidatesPictureBox4.Size = new System.Drawing.Size(64, 64);
            this.candidatesPictureBox4.TabIndex = 3;
            this.candidatesPictureBox4.TabStop = false;
            this.candidatesPictureBox4.Tag = "4";
            this.candidatesPictureBox4.Click += new System.EventHandler(this.CandidatePictureBox_Click);
            // 
            // candidatesPictureBox5
            // 
            this.candidatesPictureBox5.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.candidatesPictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidatesPictureBox5.ClientItem = null;
            this.candidatesPictureBox5.Location = new System.Drawing.Point(256, 0);
            this.candidatesPictureBox5.Margin = new System.Windows.Forms.Padding(0);
            this.candidatesPictureBox5.Name = "candidatesPictureBox5";
            this.candidatesPictureBox5.Size = new System.Drawing.Size(64, 64);
            this.candidatesPictureBox5.TabIndex = 4;
            this.candidatesPictureBox5.TabStop = false;
            this.candidatesPictureBox5.Tag = "5";
            this.candidatesPictureBox5.Click += new System.EventHandler(this.CandidatePictureBox_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.findItemButton);
            this.Controls.Add(this.reloadItemButton);
            this.Controls.Add(this.duplicateItemButton);
            this.Controls.Add(this.newItemButton);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.appearanceGroupBox);
            this.Controls.Add(this.serverItemListBox);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.separatorLabel2);
            this.Controls.Add(this.separatorLabel1);
            this.Controls.Add(this.itemsCountLabel);
            this.Controls.Add(this.loadingItemsProgressBar);
            this.Controls.Add(this.menuBar);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.statusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.appearanceGroupBox.ResumeLayout(false);
            this.appearanceGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientIdUpDown)).EndInit();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.candidatesTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private DarkUI.Controls.DarkStatusStrip statusBar;
        private DarkUI.Controls.DarkMenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator separatorMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsUpdateVersionMenuItem;
        private ItemEditor.Controls.ClientItemView pictureBox;
        private DarkUI.Controls.DarkTextBox outputTextBox;
        private DarkUI.Controls.DarkLabel cidLabel;
        private DarkUI.Controls.DarkLabel sidLabel;
        private DarkUI.Controls.DarkLabel serverIdLbl;
        private DarkUI.Controls.DarkCheckBox readableCheck;
        private DarkUI.Controls.DarkGroupBox optionsGroupBox;
        private DarkUI.Controls.DarkCheckBox ignoreLookCheck;
        private DarkUI.Controls.DarkCheckBox stackableCheck;
        private DarkUI.Controls.DarkCheckBox hookEastCheck;
        private DarkUI.Controls.DarkCheckBox rotatableCheck;
        private DarkUI.Controls.DarkCheckBox useableCheck;
        private DarkUI.Controls.DarkCheckBox hangableCheck;
        private DarkUI.Controls.DarkCheckBox hasElevationCheck;
        private DarkUI.Controls.DarkCheckBox movableCheck;
        private DarkUI.Controls.DarkCheckBox blockPathfinderCheck;
        private DarkUI.Controls.DarkCheckBox hookSouthCheck;
        private DarkUI.Controls.DarkCheckBox fullGroundCheck;
        private DarkUI.Controls.DarkCheckBox pickupableCheck;
        private DarkUI.Controls.DarkCheckBox blockMissilesCheck;
        private DarkUI.Controls.DarkCheckBox unpassableCheck;
        private System.Windows.Forms.ToolStripMenuItem toolsCompareOtbFilesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewShowOnlyMismatchedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsReloadItemAttributesMenuItem;
        private System.Windows.Forms.ToolStripSeparator separatorMenuItem2;
        private System.Windows.Forms.ToolStripSeparator separatorMenuItem4;
        private DarkUI.Controls.DarkLabel minimapColorLabel;
        private DarkUI.Controls.DarkTextBox minimapColorText;
        private DarkUI.Controls.DarkLabel lightColorLabel;
        private DarkUI.Controls.DarkTextBox lightColorText;
        private DarkUI.Controls.DarkLabel lightLevelLabel;
        private DarkUI.Controls.DarkTextBox lightLevelText;
        private DarkUI.Controls.DarkLabel groundSpeedLabel;
        private DarkUI.Controls.DarkTextBox groundSpeedText;
        private DarkUI.Controls.DarkLabel typeLabel;
        private DarkUI.Controls.DarkComboBox typeCombo;
        private DarkUI.Controls.DarkLabel maxReadWriteCharsLabel;
        private DarkUI.Controls.DarkTextBox maxReadWriteCharsText;
        private DarkUI.Controls.DarkLabel maxReadCharsLabel;
        private DarkUI.Controls.DarkTextBox maxReadCharsText;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editDuplicateItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editReloadItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCreateItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editFindItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCreateMissingItemsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewUpdateItemsListMenuItem;
        private DarkUI.Controls.DarkTextBox wareIdText;
        private DarkUI.Controls.DarkLabel wareIdLabel;
        private DarkUI.Controls.DarkTextBox nameText;
        private DarkUI.Controls.DarkLabel nameLabel;
        private System.Windows.Forms.ContextMenuStrip itemsListBoxContextMenu;
        private DarkUI.Controls.DarkGroupBox appearanceGroupBox;
        private System.Windows.Forms.ProgressBar loadingItemsProgressBar;
        private DarkUI.Controls.DarkLabel itemsCountLabel;
        private DarkUI.Controls.DarkLabel separatorLabel1;
        private DarkUI.Controls.DarkLabel separatorLabel2;
        private DarkUI.Controls.DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripNewButton;
        private System.Windows.Forms.ToolStripButton toolStripOpenButton;
        private System.Windows.Forms.ToolStripButton toolStripSaveButton;
        private System.Windows.Forms.ToolStripButton toolStripSaveAsButton;
        private DarkUI.Controls.DarkNumericUpDown clientIdUpDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripCompareButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripFindItemButton;
        private System.Windows.Forms.ToolStripMenuItem viewShowDecaptedItemsMenuItem;
        private DarkUI.Controls.DarkLabel previousLabel;
        private ItemEditor.Controls.ClientItemView previousPictureBox;
        private System.Windows.Forms.Label currentSpriteLabel;
        private System.Windows.Forms.TableLayoutPanel candidatesTableLayoutPanel;
        private System.Windows.Forms.ToolStripDropDown candidatesDropDown;
        private ItemEditor.Controls.ClientItemView candidatesPictureBox1;
        private ItemEditor.Controls.ClientItemView candidatesPictureBox2;
        private ItemEditor.Controls.ClientItemView candidatesPictureBox3;
        private ItemEditor.Controls.ClientItemView candidatesPictureBox4;
        private ItemEditor.Controls.ClientItemView candidatesPictureBox5;
        private DarkUI.Controls.DarkButton candidatesButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem filePreferencesMenuItem;
        private ItemEditor.Controls.ServerItemListBox serverItemListBox;
        private DarkUI.Controls.DarkButton newItemButton;
        private DarkUI.Controls.DarkButton duplicateItemButton;
        private DarkUI.Controls.DarkButton reloadItemButton;
        private DarkUI.Controls.DarkButton findItemButton;
        private DarkUI.Controls.DarkLabel label1;
        private DarkUI.Controls.DarkComboBox stackOrderComboBox;
        private DarkUI.Controls.DarkLabel stackOrderLabel;
        private DarkUI.Controls.DarkCheckBox forceUseCheckBox;
    }
}
