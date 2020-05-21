namespace ItemEditor.Dialogs
{
    partial class CompareOtbForm
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
            this.resultTextBox = new DarkUI.Controls.DarkTextBox();
            this.browseButton1 = new DarkUI.Controls.DarkButton();
            this.browseButton2 = new DarkUI.Controls.DarkButton();
            this.file1Text = new DarkUI.Controls.DarkTextBox();
            this.label1 = new DarkUI.Controls.DarkLabel();
            this.file2Text = new DarkUI.Controls.DarkTextBox();
            this.label2 = new DarkUI.Controls.DarkLabel();
            this.compareButton = new DarkUI.Controls.DarkButton();
            this.SuspendLayout();
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(9, 152);
            this.resultTextBox.MaxLength = 0;
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextBox.Size = new System.Drawing.Size(359, 183);
            this.resultTextBox.TabIndex = 0;
            // 
            // browseButton1
            // 
            this.browseButton1.Location = new System.Drawing.Point(293, 24);
            this.browseButton1.Name = "browseButton1";
            this.browseButton1.Size = new System.Drawing.Size(75, 23);
            this.browseButton1.TabIndex = 1;
            this.browseButton1.Text = "Browse...";
            this.browseButton1.Click += new System.EventHandler(this.BrowseButton1_Click);
            // 
            // browseButton2
            // 
            this.browseButton2.Location = new System.Drawing.Point(293, 73);
            this.browseButton2.Name = "browseButton2";
            this.browseButton2.Size = new System.Drawing.Size(75, 23);
            this.browseButton2.TabIndex = 2;
            this.browseButton2.Text = "Browse...";
            this.browseButton2.Click += new System.EventHandler(this.BrowseButton2_Click);
            // 
            // file1Text
            // 
            this.file1Text.Location = new System.Drawing.Point(9, 25);
            this.file1Text.Name = "file1Text";
            this.file1Text.Size = new System.Drawing.Size(278, 20);
            this.file1Text.TabIndex = 3;
            this.file1Text.TextChanged += new System.EventHandler(FileText_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File 1:";
            // 
            // file2Text
            // 
            this.file2Text.Location = new System.Drawing.Point(9, 74);
            this.file2Text.Name = "file2Text";
            this.file2Text.Size = new System.Drawing.Size(278, 20);
            this.file2Text.TabIndex = 5;
            this.file2Text.TextChanged += new System.EventHandler(FileText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "File 2:";
            // 
            // compareButton
            // 
            this.compareButton.Enabled = false;
            this.compareButton.Location = new System.Drawing.Point(151, 111);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(75, 23);
            this.compareButton.TabIndex = 7;
            this.compareButton.Text = "Compare";
            this.compareButton.Click += new System.EventHandler(this.CompareButton_Click);
            // 
            // CompareOtbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 344);
            this.Controls.Add(this.compareButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.file2Text);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.file1Text);
            this.Controls.Add(this.browseButton2);
            this.Controls.Add(this.browseButton1);
            this.Controls.Add(this.resultTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CompareOtbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compare OTB Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkTextBox resultTextBox;
        private DarkUI.Controls.DarkButton browseButton1;
        private DarkUI.Controls.DarkButton browseButton2;
        private DarkUI.Controls.DarkTextBox file1Text;
        private DarkUI.Controls.DarkLabel label1;
        private DarkUI.Controls.DarkTextBox file2Text;
        private DarkUI.Controls.DarkLabel label2;
        private DarkUI.Controls.DarkButton compareButton;
    }
}
