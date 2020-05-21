namespace ItemEditor.Dialogs
{
    partial class PreferencesForm
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
            this.cancelButton = new DarkUI.Controls.DarkButton();
            this.confirmButton = new DarkUI.Controls.DarkButton();
            this.groupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.directoryPathTextBox = new DarkUI.Controls.DarkTextBox();
            this.browseButton = new DarkUI.Controls.DarkButton();
            this.transparencyCheckBox = new DarkUI.Controls.DarkCheckBox();
            this.extendedCheckBox = new DarkUI.Controls.DarkCheckBox();
            this.alertLabel = new DarkUI.Controls.DarkLabel();
            this.frameDurationsCheckBox = new DarkUI.Controls.DarkCheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(347, 183);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(266, 182);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.frameDurationsCheckBox);
            this.groupBox1.Controls.Add(this.directoryPathTextBox);
            this.groupBox1.Controls.Add(this.browseButton);
            this.groupBox1.Controls.Add(this.transparencyCheckBox);
            this.groupBox1.Controls.Add(this.extendedCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 131);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default Client";
            // 
            // directoryPathTextBox
            // 
            this.directoryPathTextBox.Location = new System.Drawing.Point(12, 96);
            this.directoryPathTextBox.Name = "directoryPathTextBox";
            this.directoryPathTextBox.Size = new System.Drawing.Size(303, 20);
            this.directoryPathTextBox.TabIndex = 3;
            this.directoryPathTextBox.TextChanged += new System.EventHandler(this.DirectoryPathTextBox_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(325, 94);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // transparencyCheckBox
            // 
            this.transparencyCheckBox.AutoSize = true;
            this.transparencyCheckBox.Location = new System.Drawing.Point(12, 70);
            this.transparencyCheckBox.Name = "transparencyCheckBox";
            this.transparencyCheckBox.Size = new System.Drawing.Size(91, 17);
            this.transparencyCheckBox.ForeColor = DarkUI.Config.Colors.LightText;
            this.transparencyCheckBox.TabIndex = 1;
            this.transparencyCheckBox.Text = "Transparency";
            // 
            // extendedCheckBox
            // 
            this.extendedCheckBox.AutoSize = true;
            this.extendedCheckBox.Location = new System.Drawing.Point(12, 24);
            this.extendedCheckBox.Name = "extendedCheckBox";
            this.extendedCheckBox.Size = new System.Drawing.Size(71, 17);
            this.extendedCheckBox.ForeColor = DarkUI.Config.Colors.LightText;
            this.extendedCheckBox.TabIndex = 0;
            this.extendedCheckBox.Text = "Extended";
            // 
            // alertLabel
            // 
            this.alertLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alertLabel.ForeColor = System.Drawing.Color.Red;
            this.alertLabel.Location = new System.Drawing.Point(13, 152);
            this.alertLabel.Name = "alertLabel";
            this.alertLabel.Size = new System.Drawing.Size(247, 53);
            this.alertLabel.TabIndex = 3;
            // 
            // frameDurationsCheckBox
            // 
            this.frameDurationsCheckBox.AutoSize = true;
            this.frameDurationsCheckBox.Location = new System.Drawing.Point(12, 47);
            this.frameDurationsCheckBox.Name = "frameDurationsCheckBox";
            this.frameDurationsCheckBox.Size = new System.Drawing.Size(103, 17);
            this.frameDurationsCheckBox.ForeColor = DarkUI.Config.Colors.LightText;
            this.frameDurationsCheckBox.TabIndex = 4;
            this.frameDurationsCheckBox.Text = "Frame Durations";
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 221);
            this.Controls.Add(this.alertLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkButton cancelButton;
        private DarkUI.Controls.DarkButton confirmButton;
        private DarkUI.Controls.DarkGroupBox groupBox1;
        private DarkUI.Controls.DarkCheckBox extendedCheckBox;
        private DarkUI.Controls.DarkTextBox directoryPathTextBox;
        private DarkUI.Controls.DarkButton browseButton;
        private DarkUI.Controls.DarkCheckBox transparencyCheckBox;
        private DarkUI.Controls.DarkLabel alertLabel;
        private DarkUI.Controls.DarkCheckBox frameDurationsCheckBox;
    }
}
