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
            this.updateSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.createNewItemsCheck = new System.Windows.Forms.CheckBox();
            this.reloadItemAttributesCheck = new System.Windows.Forms.CheckBox();
            this.generateSignatureCheck = new System.Windows.Forms.CheckBox();
            this.reassignUnmatchedSpritesCheck = new System.Windows.Forms.CheckBox();
            this.closeBtn = new System.Windows.Forms.Button();
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
            this.createNewItemsCheck.UseVisualStyleBackColor = true;
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
            this.reloadItemAttributesCheck.UseVisualStyleBackColor = true;
            // 
            // generateSignatureCheck
            // 
            this.generateSignatureCheck.AutoSize = true;
            this.generateSignatureCheck.Location = new System.Drawing.Point(6, 88);
            this.generateSignatureCheck.Name = "generateSignatureCheck";
            this.generateSignatureCheck.Size = new System.Drawing.Size(187, 17);
            this.generateSignatureCheck.TabIndex = 3;
            this.generateSignatureCheck.Text = "Generate Image Signatures (Slow)";
            this.generateSignatureCheck.UseVisualStyleBackColor = true;
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
            this.reassignUnmatchedSpritesCheck.UseVisualStyleBackColor = true;
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeBtn.Location = new System.Drawing.Point(235, 149);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "OK";
            this.closeBtn.UseVisualStyleBackColor = true;
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

        private System.Windows.Forms.GroupBox updateSettingsGroupBox;
        private System.Windows.Forms.Button closeBtn;
        public System.Windows.Forms.CheckBox createNewItemsCheck;
        public System.Windows.Forms.CheckBox reloadItemAttributesCheck;
        public System.Windows.Forms.CheckBox generateSignatureCheck;
        public System.Windows.Forms.CheckBox reassignUnmatchedSpritesCheck;
    }
}
