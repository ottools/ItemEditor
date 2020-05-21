namespace ItemEditor.Dialogs
{
    partial class FindItemForm
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
            this.findIdNumericUpDown = new DarkUI.Controls.DarkNumericUpDown();
            this.findBySidButton = new DarkUI.Controls.DarkRadioButton();
            this.findByPropertiesButton = new DarkUI.Controls.DarkRadioButton();
            this.findByCidButton = new DarkUI.Controls.DarkRadioButton();
            this.groupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.itemIdGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.propertiesGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.ignoreLookCheck = new ItemEditor.Controls.FlagCheckBox();
            this.readableCheck = new ItemEditor.Controls.FlagCheckBox();
            this.hasElevationCheck = new ItemEditor.Controls.FlagCheckBox();
            this.fullGroundCheck = new ItemEditor.Controls.FlagCheckBox();
            this.forceUseCheckBox = new ItemEditor.Controls.FlagCheckBox();
            this.hookEastCheck = new ItemEditor.Controls.FlagCheckBox();
            this.rotatableCheck = new ItemEditor.Controls.FlagCheckBox();
            this.useableCheck = new ItemEditor.Controls.FlagCheckBox();
            this.hangableCheck = new ItemEditor.Controls.FlagCheckBox();
            this.hookSouthCheck = new ItemEditor.Controls.FlagCheckBox();
            this.flagCheckBox1 = new ItemEditor.Controls.FlagCheckBox();
            this.moveableFlagCheckBox = new ItemEditor.Controls.FlagCheckBox();
            this.stackableCheckBox = new ItemEditor.Controls.FlagCheckBox();
            this.blockPathfinderCheckBox = new ItemEditor.Controls.FlagCheckBox();
            this.blockMissilesCheckBox = new ItemEditor.Controls.FlagCheckBox();
            this.unpassableCheckBox = new ItemEditor.Controls.FlagCheckBox();
            this.resultGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.serverItemList = new ItemEditor.Controls.ServerItemListBox();
            this.findItemButton = new DarkUI.Controls.DarkButton();
            ((System.ComponentModel.ISupportInitialize)(this.findIdNumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.itemIdGroupBox.SuspendLayout();
            this.propertiesGroupBox.SuspendLayout();
            this.resultGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // findIdNumericUpDown
            // 
            this.findIdNumericUpDown.Location = new System.Drawing.Point(11, 19);
            this.findIdNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.findIdNumericUpDown.Name = "findIdNumericUpDown";
            this.findIdNumericUpDown.Size = new System.Drawing.Size(126, 20);
            this.findIdNumericUpDown.TabIndex = 0;
            this.findIdNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.findIdNumericUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FindIdNumericUpDown_KeyUp);
            // 
            // findBySidButton
            // 
            this.findBySidButton.AutoSize = true;
            this.findBySidButton.Checked = true;
            this.findBySidButton.Location = new System.Drawing.Point(13, 18);
            this.findBySidButton.Name = "findBySidButton";
            this.findBySidButton.Size = new System.Drawing.Size(108, 17);
            this.findBySidButton.TabIndex = 2;
            this.findBySidButton.TabStop = true;
            this.findBySidButton.Text = "Find By Server ID";
            this.findBySidButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // findByPropertiesButton
            // 
            this.findByPropertiesButton.AutoSize = true;
            this.findByPropertiesButton.Location = new System.Drawing.Point(13, 64);
            this.findByPropertiesButton.Name = "findByPropertiesButton";
            this.findByPropertiesButton.Size = new System.Drawing.Size(110, 17);
            this.findByPropertiesButton.TabIndex = 3;
            this.findByPropertiesButton.Text = "Find By Properties";
            this.findByPropertiesButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // findByCidButton
            // 
            this.findByCidButton.AutoSize = true;
            this.findByCidButton.Location = new System.Drawing.Point(13, 41);
            this.findByCidButton.Name = "findByCidButton";
            this.findByCidButton.Size = new System.Drawing.Size(103, 17);
            this.findByCidButton.TabIndex = 4;
            this.findByCidButton.Text = "Find By Client ID";
            this.findByCidButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.findByPropertiesButton);
            this.groupBox1.Controls.Add(this.findByCidButton);
            this.groupBox1.Controls.Add(this.findBySidButton);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 95);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // itemIdGroupBox
            // 
            this.itemIdGroupBox.Controls.Add(this.findIdNumericUpDown);
            this.itemIdGroupBox.Location = new System.Drawing.Point(10, 99);
            this.itemIdGroupBox.Name = "itemIdGroupBox";
            this.itemIdGroupBox.Size = new System.Drawing.Size(149, 53);
            this.itemIdGroupBox.TabIndex = 6;
            this.itemIdGroupBox.TabStop = false;
            this.itemIdGroupBox.Text = "ID";
            // 
            // propertiesGroupBox
            // 
            this.propertiesGroupBox.Controls.Add(this.ignoreLookCheck);
            this.propertiesGroupBox.Controls.Add(this.readableCheck);
            this.propertiesGroupBox.Controls.Add(this.hasElevationCheck);
            this.propertiesGroupBox.Controls.Add(this.fullGroundCheck);
            this.propertiesGroupBox.Controls.Add(this.forceUseCheckBox);
            this.propertiesGroupBox.Controls.Add(this.hookEastCheck);
            this.propertiesGroupBox.Controls.Add(this.rotatableCheck);
            this.propertiesGroupBox.Controls.Add(this.useableCheck);
            this.propertiesGroupBox.Controls.Add(this.hangableCheck);
            this.propertiesGroupBox.Controls.Add(this.hookSouthCheck);
            this.propertiesGroupBox.Controls.Add(this.flagCheckBox1);
            this.propertiesGroupBox.Controls.Add(this.moveableFlagCheckBox);
            this.propertiesGroupBox.Controls.Add(this.stackableCheckBox);
            this.propertiesGroupBox.Controls.Add(this.blockPathfinderCheckBox);
            this.propertiesGroupBox.Controls.Add(this.blockMissilesCheckBox);
            this.propertiesGroupBox.Controls.Add(this.unpassableCheckBox);
            this.propertiesGroupBox.Enabled = false;
            this.propertiesGroupBox.Location = new System.Drawing.Point(10, 154);
            this.propertiesGroupBox.Name = "propertiesGroupBox";
            this.propertiesGroupBox.Size = new System.Drawing.Size(149, 395);
            this.propertiesGroupBox.TabIndex = 7;
            this.propertiesGroupBox.TabStop = false;
            this.propertiesGroupBox.Text = "Attributes";
            // 
            // ignoreLookCheck
            // 
            this.ignoreLookCheck.AutoSize = true;
            this.ignoreLookCheck.Location = new System.Drawing.Point(10, 311);
            this.ignoreLookCheck.Name = "ignoreLookCheck";
            this.ignoreLookCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.IgnoreLook;
            this.ignoreLookCheck.Size = new System.Drawing.Size(83, 17);
            this.ignoreLookCheck.TabIndex = 61;
            this.ignoreLookCheck.Text = "Ignore Look";
            this.ignoreLookCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // readableCheck
            // 
            this.readableCheck.AutoSize = true;
            this.readableCheck.Location = new System.Drawing.Point(10, 332);
            this.readableCheck.Name = "readableCheck";
            this.readableCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Readable;
            this.readableCheck.Size = new System.Drawing.Size(72, 17);
            this.readableCheck.TabIndex = 62;
            this.readableCheck.Text = "Readable";
            this.readableCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // hasElevationCheck
            // 
            this.hasElevationCheck.AutoSize = true;
            this.hasElevationCheck.Location = new System.Drawing.Point(10, 288);
            this.hasElevationCheck.Name = "hasElevationCheck";
            this.hasElevationCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.HasElevation;
            this.hasElevationCheck.Size = new System.Drawing.Size(92, 17);
            this.hasElevationCheck.TabIndex = 59;
            this.hasElevationCheck.Text = "Has Elevation";
            this.hasElevationCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // fullGroundCheck
            // 
            this.fullGroundCheck.AutoSize = true;
            this.fullGroundCheck.Location = new System.Drawing.Point(10, 353);
            this.fullGroundCheck.Name = "fullGroundCheck";
            this.fullGroundCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.FullGround;
            this.fullGroundCheck.Size = new System.Drawing.Size(80, 17);
            this.fullGroundCheck.TabIndex = 60;
            this.fullGroundCheck.Text = "Full Ground";
            this.fullGroundCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // forceUseCheckBox
            // 
            this.forceUseCheckBox.AutoSize = true;
            this.forceUseCheckBox.Location = new System.Drawing.Point(10, 150);
            this.forceUseCheckBox.Name = "forceUseCheckBox";
            this.forceUseCheckBox.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.ForceUse;
            this.forceUseCheckBox.Size = new System.Drawing.Size(75, 17);
            this.forceUseCheckBox.TabIndex = 58;
            this.forceUseCheckBox.Text = "Force Use";
            this.forceUseCheckBox.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // hookEastCheck
            // 
            this.hookEastCheck.AutoSize = true;
            this.hookEastCheck.Location = new System.Drawing.Point(10, 265);
            this.hookEastCheck.Name = "hookEastCheck";
            this.hookEastCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.HookEast;
            this.hookEastCheck.Size = new System.Drawing.Size(76, 17);
            this.hookEastCheck.TabIndex = 57;
            this.hookEastCheck.Text = "Hook East";
            this.hookEastCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // rotatableCheck
            // 
            this.rotatableCheck.AutoSize = true;
            this.rotatableCheck.Location = new System.Drawing.Point(10, 196);
            this.rotatableCheck.Name = "rotatableCheck";
            this.rotatableCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Rotatable;
            this.rotatableCheck.Size = new System.Drawing.Size(72, 17);
            this.rotatableCheck.TabIndex = 55;
            this.rotatableCheck.Text = "Rotatable";
            this.rotatableCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // useableCheck
            // 
            this.useableCheck.AutoSize = true;
            this.useableCheck.Location = new System.Drawing.Point(10, 173);
            this.useableCheck.Name = "useableCheck";
            this.useableCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.MultiUse;
            this.useableCheck.Size = new System.Drawing.Size(70, 17);
            this.useableCheck.TabIndex = 54;
            this.useableCheck.Text = "Multi Use";
            this.useableCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // hangableCheck
            // 
            this.hangableCheck.AutoSize = true;
            this.hangableCheck.Location = new System.Drawing.Point(10, 219);
            this.hangableCheck.Name = "hangableCheck";
            this.hangableCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Hangable;
            this.hangableCheck.Size = new System.Drawing.Size(72, 17);
            this.hangableCheck.TabIndex = 53;
            this.hangableCheck.Text = "Hangable";
            this.hangableCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // hookSouthCheck
            // 
            this.hookSouthCheck.AutoSize = true;
            this.hookSouthCheck.Location = new System.Drawing.Point(10, 242);
            this.hookSouthCheck.Name = "hookSouthCheck";
            this.hookSouthCheck.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.HookSouth;
            this.hookSouthCheck.Size = new System.Drawing.Size(83, 17);
            this.hookSouthCheck.TabIndex = 56;
            this.hookSouthCheck.Text = "Hook South";
            this.hookSouthCheck.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // flagCheckBox1
            // 
            this.flagCheckBox1.AutoSize = true;
            this.flagCheckBox1.Location = new System.Drawing.Point(10, 106);
            this.flagCheckBox1.Name = "flagCheckBox1";
            this.flagCheckBox1.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Pickupable;
            this.flagCheckBox1.Size = new System.Drawing.Size(79, 17);
            this.flagCheckBox1.TabIndex = 5;
            this.flagCheckBox1.Text = "Pickupable";
            this.flagCheckBox1.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // moveableFlagCheckBox
            // 
            this.moveableFlagCheckBox.AutoSize = true;
            this.moveableFlagCheckBox.Location = new System.Drawing.Point(10, 41);
            this.moveableFlagCheckBox.Name = "moveableFlagCheckBox";
            this.moveableFlagCheckBox.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Movable;
            this.moveableFlagCheckBox.Size = new System.Drawing.Size(73, 17);
            this.moveableFlagCheckBox.TabIndex = 4;
            this.moveableFlagCheckBox.Text = "Moveable";
            this.moveableFlagCheckBox.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // stackableCheckBox
            // 
            this.stackableCheckBox.AutoSize = true;
            this.stackableCheckBox.Location = new System.Drawing.Point(10, 129);
            this.stackableCheckBox.Name = "stackableCheckBox";
            this.stackableCheckBox.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Stackable;
            this.stackableCheckBox.Size = new System.Drawing.Size(74, 17);
            this.stackableCheckBox.TabIndex = 3;
            this.stackableCheckBox.Text = "Stackable";
            this.stackableCheckBox.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // blockPathfinderCheckBox
            // 
            this.blockPathfinderCheckBox.AutoSize = true;
            this.blockPathfinderCheckBox.Location = new System.Drawing.Point(10, 83);
            this.blockPathfinderCheckBox.Name = "blockPathfinderCheckBox";
            this.blockPathfinderCheckBox.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.BlockPathfinder;
            this.blockPathfinderCheckBox.Size = new System.Drawing.Size(104, 17);
            this.blockPathfinderCheckBox.TabIndex = 2;
            this.blockPathfinderCheckBox.Text = "Block Pathfinder";
            this.blockPathfinderCheckBox.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // blockMissilesCheckBox
            // 
            this.blockMissilesCheckBox.AutoSize = true;
            this.blockMissilesCheckBox.Location = new System.Drawing.Point(10, 62);
            this.blockMissilesCheckBox.Name = "blockMissilesCheckBox";
            this.blockMissilesCheckBox.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.BlockMissiles;
            this.blockMissilesCheckBox.Size = new System.Drawing.Size(92, 17);
            this.blockMissilesCheckBox.TabIndex = 1;
            this.blockMissilesCheckBox.Text = "Block Missiles";
            this.blockMissilesCheckBox.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // unpassableCheckBox
            // 
            this.unpassableCheckBox.AutoSize = true;
            this.unpassableCheckBox.Location = new System.Drawing.Point(10, 20);
            this.unpassableCheckBox.Name = "unpassableCheckBox";
            this.unpassableCheckBox.ServerItemFlag = OTLib.Server.Items.ServerItemFlag.Unpassable;
            this.unpassableCheckBox.Size = new System.Drawing.Size(82, 17);
            this.unpassableCheckBox.TabIndex = 0;
            this.unpassableCheckBox.Text = "Unpassable";
            this.unpassableCheckBox.CheckedChanged += new System.EventHandler(this.PropertyCheckBox_CheckedChanged);
            // 
            // resultGroupBox
            // 
            this.resultGroupBox.Controls.Add(this.serverItemList);
            this.resultGroupBox.Location = new System.Drawing.Point(165, 3);
            this.resultGroupBox.Name = "resultGroupBox";
            this.resultGroupBox.Size = new System.Drawing.Size(260, 518);
            this.resultGroupBox.TabIndex = 7;
            this.resultGroupBox.TabStop = false;
            this.resultGroupBox.Text = "Result";
            // 
            // serverItemList
            // 
            this.serverItemList.BackColor = DarkUI.Config.Colors.DarkBackground;
            this.serverItemList.Location = new System.Drawing.Point(7, 19);
            this.serverItemList.Name = "serverItemList";
            this.serverItemList.Plugin = null;
            this.serverItemList.Size = new System.Drawing.Size(244, 486);
            this.serverItemList.TabIndex = 0;
            this.serverItemList.SelectedIndexChanged += ServerItemList_SelectedIndexChanged;
            // 
            // findItemButton
            // 
            this.findItemButton.Location = new System.Drawing.Point(165, 527);
            this.findItemButton.Name = "findItemButton";
            this.findItemButton.Size = new System.Drawing.Size(260, 22);
            this.findItemButton.TabIndex = 8;
            this.findItemButton.Text = "Find";
            this.findItemButton.Click += new System.EventHandler(this.FindItemButton_Click);
            // 
            // FindItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 561);
            this.Controls.Add(this.findItemButton);
            this.Controls.Add(this.resultGroupBox);
            this.Controls.Add(this.propertiesGroupBox);
            this.Controls.Add(this.itemIdGroupBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindItemForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Item";
            ((System.ComponentModel.ISupportInitialize)(this.findIdNumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.itemIdGroupBox.ResumeLayout(false);
            this.propertiesGroupBox.ResumeLayout(false);
            this.propertiesGroupBox.PerformLayout();
            this.resultGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkNumericUpDown findIdNumericUpDown;
        private DarkUI.Controls.DarkRadioButton findBySidButton;
        private DarkUI.Controls.DarkRadioButton findByPropertiesButton;
        private DarkUI.Controls.DarkRadioButton findByCidButton;
        private DarkUI.Controls.DarkGroupBox groupBox1;
        private DarkUI.Controls.DarkGroupBox itemIdGroupBox;
        private DarkUI.Controls.DarkGroupBox propertiesGroupBox;
        private DarkUI.Controls.DarkGroupBox resultGroupBox;
        private Controls.ServerItemListBox serverItemList;
        private Controls.FlagCheckBox unpassableCheckBox;
        private Controls.FlagCheckBox blockPathfinderCheckBox;
        private Controls.FlagCheckBox blockMissilesCheckBox;
        private Controls.FlagCheckBox stackableCheckBox;
        private Controls.FlagCheckBox moveableFlagCheckBox;
        private Controls.FlagCheckBox flagCheckBox1;
        private Controls.FlagCheckBox forceUseCheckBox;
        private Controls.FlagCheckBox hookEastCheck;
        private Controls.FlagCheckBox rotatableCheck;
        private Controls.FlagCheckBox useableCheck;
        private Controls.FlagCheckBox hangableCheck;
        private Controls.FlagCheckBox hookSouthCheck;
        private Controls.FlagCheckBox ignoreLookCheck;
        private Controls.FlagCheckBox readableCheck;
        private Controls.FlagCheckBox hasElevationCheck;
        private Controls.FlagCheckBox fullGroundCheck;
        private DarkUI.Controls.DarkButton findItemButton;
    }
}
