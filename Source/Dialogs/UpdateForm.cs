#region Licence
/**
* Copyright (C) 2015 Nailson S. <nailsonnego@gmail.com>
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

using ItemEditor.Host;
using PluginInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ItemEditor.Dialogs
{
	public partial class UpdateForm : Form
	{
		#region Properties

		public MainForm mainForm = null;
		public Plugin selectedPlugin = null;
		public SupportedClient updateClient = null;

		#endregion

		#region Constructor

		public UpdateForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handlers

		private void PluginForm_Load(object sender, EventArgs e)
		{
			List<SupportedClient> list = new List<SupportedClient>();

			foreach (Plugin plugin in Program.plugins.AvailablePlugins)
			{
				foreach (SupportedClient client in plugin.Instance.SupportedClients)
				{
					list.Add(client);
				}
			}

			if (list.Count > 0)
			{
				list = list.OrderBy(i => i.OtbVersion).ToList();

				pluginsListBox.DataSource = list;
				pluginsListBox.SelectedIndex = list.Count - 1;
			}
		}

		private void selectBtn_Click(object sender, EventArgs e)
		{
			if (pluginsListBox.SelectedItem != null)
			{
				selectedPlugin = Program.plugins.AvailablePlugins.Find(pluginsListBox.SelectedItem.ToString());
				updateClient = pluginsListBox.SelectedItem as SupportedClient;

				this.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void pluginsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (pluginsListBox.SelectedItem != null && mainForm.currentPlugin != null)
			{
				selectBtn.Enabled = true;
			}
		}

		#endregion
	}
}
