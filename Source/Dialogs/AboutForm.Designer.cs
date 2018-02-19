namespace ItemEditor.Dialogs
{
    partial class AboutForm
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.sourceLinkLabel = new System.Windows.Forms.LinkLabel();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.uiIconsLabel = new System.Windows.Forms.Label();
            this.uiIconsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.appIconLabel = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.developerLabel = new System.Windows.Forms.Label();
            this.developerLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            //
            // versionLabel
            //
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.versionLabel.Location = new System.Drawing.Point(223, 137);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(59, 20);
            this.versionLabel.TabIndex = 0;
            this.versionLabel.Text = "version";
            //
            // pictureBox
            //
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(450, 244);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            //
            // sourceLinkLabel
            //
            this.sourceLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(140)))), ((int)(((byte)(44)))));
            this.sourceLinkLabel.AutoSize = true;
            this.sourceLinkLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.sourceLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.sourceLinkLabel.Location = new System.Drawing.Point(58, 221);
            this.sourceLinkLabel.Name = "sourceLinkLabel";
            this.sourceLinkLabel.Size = new System.Drawing.Size(183, 13);
            this.sourceLinkLabel.TabIndex = 3;
            this.sourceLinkLabel.TabStop = true;
            this.sourceLinkLabel.Text = "https://github.com/ottools/ItemEditor//ItemEditor";
            this.sourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelClickedHandler);
            //
            // sourceLabel
            //
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.sourceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.sourceLabel.Location = new System.Drawing.Point(12, 221);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(44, 13);
            this.sourceLabel.TabIndex = 4;
            this.sourceLabel.Text = "Source:";
            //
            // uiIconsLabel
            //
            this.uiIconsLabel.AutoSize = true;
            this.uiIconsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.uiIconsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.uiIconsLabel.Location = new System.Drawing.Point(12, 205);
            this.uiIconsLabel.Name = "uiIconsLabel";
            this.uiIconsLabel.Size = new System.Drawing.Size(110, 13);
            this.uiIconsLabel.TabIndex = 6;
            this.uiIconsLabel.Text = "UI Icons: Mark James";
            //
            // uiIconsLinkLabel
            //
            this.uiIconsLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(140)))), ((int)(((byte)(44)))));
            this.uiIconsLinkLabel.AutoSize = true;
            this.uiIconsLinkLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.uiIconsLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.uiIconsLinkLabel.Location = new System.Drawing.Point(123, 205);
            this.uiIconsLinkLabel.Name = "uiIconsLinkLabel";
            this.uiIconsLinkLabel.Size = new System.Drawing.Size(213, 13);
            this.uiIconsLinkLabel.TabIndex = 5;
            this.uiIconsLinkLabel.TabStop = true;
            this.uiIconsLinkLabel.Text = "http://www.famfamfam.com/lab/icons/silk/";
            this.uiIconsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelClickedHandler);
            //
            // appIconLabel
            //
            this.appIconLabel.AutoSize = true;
            this.appIconLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.appIconLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.appIconLabel.Location = new System.Drawing.Point(12, 189);
            this.appIconLabel.Name = "appIconLabel";
            this.appIconLabel.Size = new System.Drawing.Size(86, 13);
            this.appIconLabel.TabIndex = 8;
            this.appIconLabel.Text = "App Icon: Daniel";
            //
            // linkLabel2
            //
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(140)))), ((int)(((byte)(44)))));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.linkLabel2.Location = new System.Drawing.Point(99, 189);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(203, 13);
            this.linkLabel2.TabIndex = 7;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "https://github.com/DANIELCAMILO2016";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelClickedHandler);
            //
            // developerLabel
            //
            this.developerLabel.AutoSize = true;
            this.developerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.developerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.developerLabel.Location = new System.Drawing.Point(12, 173);
            this.developerLabel.Name = "developerLabel";
            this.developerLabel.Size = new System.Drawing.Size(110, 13);
            this.developerLabel.TabIndex = 10;
            this.developerLabel.Text = "Development: Mignari";
            //
            // developerLinkLabel
            //
            this.developerLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(140)))), ((int)(((byte)(44)))));
            this.developerLinkLabel.AutoSize = true;
            this.developerLinkLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.developerLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.developerLinkLabel.Location = new System.Drawing.Point(124, 173);
            this.developerLinkLabel.Name = "developerLinkLabel";
            this.developerLinkLabel.Size = new System.Drawing.Size(134, 13);
            this.developerLinkLabel.TabIndex = 9;
            this.developerLinkLabel.TabStop = true;
            this.developerLinkLabel.Text = "https://github.com/Mignari";
            this.developerLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelClickedHandler);
            //
            // AboutForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 244);
            this.Controls.Add(this.developerLabel);
            this.Controls.Add(this.developerLinkLabel);
            this.Controls.Add(this.appIconLabel);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.uiIconsLabel);
            this.Controls.Add(this.uiIconsLinkLabel);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.sourceLinkLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About ItemEditor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.LinkLabel sourceLinkLabel;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label uiIconsLabel;
        private System.Windows.Forms.LinkLabel uiIconsLinkLabel;
        private System.Windows.Forms.Label appIconLabel;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label developerLabel;
        private System.Windows.Forms.LinkLabel developerLinkLabel;
    }
}
