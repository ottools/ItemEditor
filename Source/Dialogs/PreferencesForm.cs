#region Licence
/**
* Copyright © 2014-2019 OTTools <https://github.com/ottools/ItemEditor/>
*
* This program is free software; you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation; either version 2 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License along
* with this program; if not, write to the Free Software Foundation, Inc.,
* 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
#endregion

#region Using Statements
using DarkUI.Forms;
using ItemEditor.Host;
using PluginInterface;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Dialogs
{
    public partial class PreferencesForm : DarkForm
    {
        #region Private Properties

        private uint datSignature;
        private uint sprSignature;

        #endregion

        #region Contructor

        public PreferencesForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        public Plugin Plugin { get; private set; }

        public SupportedClient Client { get; private set; }

        #endregion

        #region Private Methods

        private void OnSelectFiles(string directory)
        {
            this.alertLabel.Text = string.Empty;

            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                this.alertLabel.Text = "Directory not found";
                return;
            }

            string datPath = Utils.FindClientFile(directory, ".dat");
            string sprPath = Utils.FindClientFile(directory, ".spr");

            if (!File.Exists(datPath) || !File.Exists(sprPath))
            {
                this.alertLabel.Text = "Client files not found";
                return;
            }

            uint datSignature = this.GetSignature(datPath);
            uint sprSignature = this.GetSignature(sprPath);

            this.Plugin = Program.plugins.AvailablePlugins.Find(datSignature, sprSignature);
            if (this.Plugin == null)
            {
                alertLabel.Text = string.Format("Unsupported version\nDat Signature: {0:X}\nSpr Signature: {1:X}", datSignature, sprSignature);
                return;
            }

            this.Client = this.Plugin.Instance.GetClientBySignatures(datSignature, sprSignature);
            this.extendedCheckBox.Checked = this.extendedCheckBox.Checked || this.Client.Version >= 960;
            this.extendedCheckBox.Enabled = this.Client.Version < 960;
            this.frameDurationsCheckBox.Checked = this.frameDurationsCheckBox.Checked || this.Client.Version >= 1050;
            this.frameDurationsCheckBox.Enabled = this.Client.Version < 1050;
            this.datSignature = datSignature;
            this.sprSignature = sprSignature;
            this.directoryPathTextBox.Text = directory;
        }

        private uint GetSignature(string fileName)
        {
            uint signature = 0;
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    signature = reader.ReadUInt32();
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }

            return signature;
        }

        private void Clear()
        {
            this.directoryPathTextBox.Text = string.Empty;
            this.extendedCheckBox.Checked = false;
            this.frameDurationsCheckBox.Checked = false;
            this.transparencyCheckBox.Checked = false;
            this.datSignature = 0;
            this.sprSignature = 0;
        }

        #endregion

        #region Event Handlers

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            this.directoryPathTextBox.Text = (string)Properties.Settings.Default["ClientDirectory"];
            this.extendedCheckBox.Checked = (bool)Properties.Settings.Default["Extended"];
            this.frameDurationsCheckBox.Checked = (bool)Properties.Settings.Default["FrameDurations"];
            this.transparencyCheckBox.Checked = (bool)Properties.Settings.Default["Transparency"];
            this.datSignature = (uint)Properties.Settings.Default["DatSignature"];
            this.sprSignature = (uint)Properties.Settings.Default["SprSignature"];

            this.OnSelectFiles(this.directoryPathTextBox.Text);
        }

        private void DirectoryPathTextBox_TextChanged(object sender, System.EventArgs e)
        {
            this.OnSelectFiles(this.directoryPathTextBox.Text);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.OnSelectFiles(dialog.SelectedPath);
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["ClientDirectory"] = this.directoryPathTextBox.Text;
            Properties.Settings.Default["Extended"] = this.extendedCheckBox.Checked;
            Properties.Settings.Default["FrameDurations"] = this.frameDurationsCheckBox.Checked;
            Properties.Settings.Default["Transparency"] = this.transparencyCheckBox.Checked;
            Properties.Settings.Default["DatSignature"] = this.datSignature;
            Properties.Settings.Default["SprSignature"] = this.sprSignature;
            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
