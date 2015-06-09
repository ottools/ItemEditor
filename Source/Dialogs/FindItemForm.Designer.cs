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
            this.findItemNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.findItemButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.findItemNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // findItemNumericUpDown
            // 
            this.findItemNumericUpDown.Location = new System.Drawing.Point(11, 10);
            this.findItemNumericUpDown.Name = "findItemNumericUpDown";
            this.findItemNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.findItemNumericUpDown.TabIndex = 0;
            this.findItemNumericUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(FindItemNumericUpDown_KeyUp);
            // 
            // findItemButton
            // 
            this.findItemButton.Location = new System.Drawing.Point(137, 9);
            this.findItemButton.Name = "findItemButton";
            this.findItemButton.Size = new System.Drawing.Size(75, 22);
            this.findItemButton.TabIndex = 1;
            this.findItemButton.Text = "Find";
            this.findItemButton.UseVisualStyleBackColor = true;
            this.findItemButton.Click += new System.EventHandler(FindItemButton_Click);
            // 
            // findItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 38);
            this.Controls.Add(this.findItemButton);
            this.Controls.Add(this.findItemNumericUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "findItemForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Item";
            ((System.ComponentModel.ISupportInitialize)(this.findItemNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown findItemNumericUpDown;
        private System.Windows.Forms.Button findItemButton;
    }
}
