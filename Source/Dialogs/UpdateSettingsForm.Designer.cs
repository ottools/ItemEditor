namespace ItemEditor.Dialogs
{
    partial class UpdateSettingsForm
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
            this.updateSettingsGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.createNewItemsCheck = new DarkUI.Controls.DarkCheckBox();
            this.reloadItemAttributesCheck = new DarkUI.Controls.DarkCheckBox();
            this.generateSignatureCheck = new DarkUI.Controls.DarkCheckBox();
            this.reassignUnmatchedSpritesCheck = new DarkUI.Controls.DarkCheckBox();
            this.closeBtn = new DarkUI.Controls.DarkButton();
            this.updateSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateSettingsGroupBox
            // 
            this.updateSettingsGroupBox.Controls.Add(this.createNewItemsCheck);
            this.updateSettingsGroupBox.Controls.Add(this.reloadItemAttributesCheck);
            this.updateSettingsGroupBox.Controls.Add(this.generateSignatureCheck);
            this.updateSettingsGroupBox.Controls.Add(this.reassignUnmatchedSpritesCheck);
            this.updateSettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.updateSettingsGroupBox.Name = "updateSettingsGroupBox";
            this.updateSettingsGroupBox.Size = new System.Drawing.Size(298, 131);
            this.updateSettingsGroupBox.TabIndex = 0;
            this.updateSettingsGroupBox.TabStop = false;
            this.updateSettingsGroupBox.Text = "Settings";
            // 
            // createNewItemsCheck
            // 
            this.createNewItemsCheck.AutoSize = true;
            this.createNewItemsCheck.Checked = true;
            this.createNewItemsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createNewItemsCheck.Location = new System.Drawing.Point(6, 65);
            this.createNewItemsCheck.Name = "createNewItemsCheck";
            this.createNewItemsCheck.Size = new System.Drawing.Size(228, 17);
            this.createNewItemsCheck.TabIndex = 2;
            this.createNewItemsCheck.Text = "Create New Item(s) For Unassigned Sprites";
            // 
            // reloadItemAttributesCheck
            // 
            this.reloadItemAttributesCheck.AutoSize = true;
            this.reloadItemAttributesCheck.Checked = true;
            this.reloadItemAttributesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reloadItemAttributesCheck.Location = new System.Drawing.Point(6, 42);
            this.reloadItemAttributesCheck.Name = "reloadItemAttributesCheck";
            this.reloadItemAttributesCheck.Size = new System.Drawing.Size(130, 17);
            this.reloadItemAttributesCheck.TabIndex = 1;
            this.reloadItemAttributesCheck.Text = "Reload Item Attributes";
            // 
            // generateSignatureCheck
            // 
            this.generateSignatureCheck.AutoSize = true;
            this.generateSignatureCheck.Location = new System.Drawing.Point(6, 88);
            this.generateSignatureCheck.Name = "generateSignatureCheck";
            this.generateSignatureCheck.Size = new System.Drawing.Size(187, 17);
            this.generateSignatureCheck.TabIndex = 3;
            this.generateSignatureCheck.Text = "Generate Image Signatures (Slow)";
            // 
            // reassignUnmatchedSpritesCheck
            // 
            this.reassignUnmatchedSpritesCheck.AutoSize = true;
            this.reassignUnmatchedSpritesCheck.Checked = true;
            this.reassignUnmatchedSpritesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reassignUnmatchedSpritesCheck.Location = new System.Drawing.Point(6, 19);
            this.reassignUnmatchedSpritesCheck.Name = "reassignUnmatchedSpritesCheck";
            this.reassignUnmatchedSpritesCheck.Size = new System.Drawing.Size(216, 17);
            this.reassignUnmatchedSpritesCheck.TabIndex = 0;
            this.reassignUnmatchedSpritesCheck.Text = "Reassign Items With Unmatched Sprites";
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeBtn.Location = new System.Drawing.Point(235, 149);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "OK";
            // 
            // UpdateSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 184);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.updateSettingsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.updateSettingsGroupBox.ResumeLayout(false);
            this.updateSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox updateSettingsGroupBox;
        private DarkUI.Controls.DarkButton closeBtn;
        public DarkUI.Controls.DarkCheckBox createNewItemsCheck;
        public DarkUI.Controls.DarkCheckBox reloadItemAttributesCheck;
        public DarkUI.Controls.DarkCheckBox generateSignatureCheck;
        public DarkUI.Controls.DarkCheckBox reassignUnmatchedSpritesCheck;
    }
}
