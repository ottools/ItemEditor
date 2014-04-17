#region Licence
/**
* Copyright (C) 2014 <https://github.com/Mignari/ItemEditor>
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

using Host.Types;
using ItemEditor.Settings;
using System;
using System.IO;
using System.Windows.Forms;

namespace ItemEditor.Dialogs
{
	public partial class PreferencesForm : Form
	{
		#region Private Properties

		private Preferences _preferences;
		private UInt32 _datSignature;
		private UInt32 _sprSignature;

		#endregion

		#region Contructor

		public PreferencesForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void onSelectFiles(string directory)
		{
			alertLabel.Text = String.Empty;

			if (String.IsNullOrEmpty(directory) || !Directory.Exists(directory))
			{
				this.Clear();
				return;
			}

			string datPath = Path.Combine(directory, "Tibia.dat");
			string sprPath = Path.Combine(directory, "Tibia.spr");

			if (!File.Exists(datPath) || !File.Exists(sprPath))
			{
				datPath = Utils.FindClientFile(directory, ".dat");
				sprPath = Utils.FindClientFile(directory, ".spr");

				if (!File.Exists(datPath) || !File.Exists(sprPath))
				{
					this.Clear();
					alertLabel.Text = "Client files not found.";
					return;
				}
			}

			UInt32 datSignature = GetSignature(datPath);
			UInt32 sprSignature = GetSignature(sprPath);

			Plugin plugin = Program.plugins.AvailablePlugins.Find(datSignature, sprSignature);
			if (plugin == null)
			{
				this.Clear();
				alertLabel.Text = "Unsupported version.";
				return;
			}

			_datSignature = datSignature;
			_sprSignature = sprSignature;
			directoryPathTextBox.Text = directory;
		}

		private UInt32 GetSignature(string fileName)
		{
			UInt32 signature = 0;
			FileStream fileStream = new FileStream(fileName, FileMode.Open);

			try
			{
				using (BinaryReader reader = new BinaryReader(fileStream))
				{
					signature = reader.ReadUInt32();
				}
			}
			finally
			{
				fileStream.Close();
			}

			return signature;
		}

		private void Clear()
		{
			directoryPathTextBox.Text = String.Empty;
			extendedCheckBox.Checked = false;
			transparencyCheckBox.Checked = false;
			_datSignature = 0;
			_sprSignature = 0;
		}

		#endregion

		#region Event Handlers

		private void preferencesForm_Load(object sender, EventArgs e)
		{
			_preferences = Program.preferences;
			directoryPathTextBox.Text = _preferences.ClientDirectory;
			extendedCheckBox.Checked = _preferences.Extended;
			transparencyCheckBox.Checked = _preferences.Transparency;

			onSelectFiles(_preferences.ClientDirectory);
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				onSelectFiles(dialog.SelectedPath);
			}
		}

		private void confirmButton_Click(object sender, EventArgs e)
		{
			_preferences.ClientDirectory = directoryPathTextBox.Text;
			_preferences.Extended = extendedCheckBox.Checked;
			_preferences.Transparency = transparencyCheckBox.Checked;
			_preferences.DatSignature = _datSignature;
			_preferences.SprSignature = _sprSignature;
			_preferences.Save();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#endregion
	}
}
