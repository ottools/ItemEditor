namespace ItemEditor.Dialogs
{
    partial class UpdateForm
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
            this.pluginsListBox = new System.Windows.Forms.ListBox();
            this.button1 = new DarkUI.Controls.DarkButton();
            this.selectBtn = new DarkUI.Controls.DarkButton();
            this.lblDescription = new DarkUI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // pluginsListBox
            // 
            this.pluginsListBox.FormattingEnabled = true;
            this.pluginsListBox.Location = new System.Drawing.Point(12, 45);
            this.pluginsListBox.Name = "pluginsListBox";
            this.pluginsListBox.Size = new System.Drawing.Size(168, 121);
            this.pluginsListBox.TabIndex = 0;
            this.pluginsListBox.SelectedIndexChanged += new System.EventHandler(this.PluginsListBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(186, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Close";
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(186, 45);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(96, 23);
            this.selectBtn.TabIndex = 1;
            this.selectBtn.Text = "&Select";
            this.selectBtn.Click += new System.EventHandler(this.SelectBtn_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(9, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(273, 33);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Select the client you wish to update to then press Select.";
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 178);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.pluginsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update OTB";
            this.Load += new System.EventHandler(this.PluginForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox pluginsListBox;
        private DarkUI.Controls.DarkButton button1;
        private DarkUI.Controls.DarkButton selectBtn;
        public DarkUI.Controls.DarkLabel lblDescription;
    }
}
