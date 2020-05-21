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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Dialogs
{
    public partial class UpdateForm : DarkForm
    {
        #region Constructor

        public UpdateForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        public MainForm MainForm { get; set; }

        public Plugin SelectedPlugin { get; set; }

        public SupportedClient UpdateClient { get; set; }

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

                this.pluginsListBox.DataSource = list;
                this.pluginsListBox.SelectedIndex = list.Count - 1;
            }
        }

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            if (this.pluginsListBox.SelectedItem != null)
            {
                this.SelectedPlugin = Program.plugins.AvailablePlugins.Find(this.pluginsListBox.SelectedItem.ToString());
                this.UpdateClient = this.pluginsListBox.SelectedItem as SupportedClient;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void PluginsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.pluginsListBox.SelectedItem != null && this.MainForm.CurrentPlugin != null)
            {
                this.selectBtn.Enabled = true;
            }
        }

        #endregion
    }
}
