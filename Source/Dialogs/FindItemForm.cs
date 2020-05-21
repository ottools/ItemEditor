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
using ItemEditor.Controls;
using OTLib.Server.Items;
using System;
using System.Diagnostics;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Dialogs
{
    public partial class FindItemForm : DarkForm
    {
        #region Private Properties

        private MainForm m_mainForm;
        private ServerItemFlag m_properties;

        #endregion

        #region Constructor

        public FindItemForm()
        {
            m_properties = ServerItemFlag.None;

            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        public MainForm MainForm
        {
            get
            {
                return m_mainForm;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("MainForm");
                }

                m_mainForm = value;
                m_mainForm.OnCleaned += MainForm_CleanedHandler;
                this.serverItemList.Plugin = m_mainForm.CurrentPlugin.Instance;
                this.UpdateProperties();
            }
        }

        #endregion

        #region Private Methods

        private void UpdateProperties()
        {
            if (findBySidButton.Checked)
            {
                this.itemIdGroupBox.Enabled = true;
                this.itemIdGroupBox.Text = "Server ID";
                this.propertiesGroupBox.Enabled = false;
                this.findIdNumericUpDown.Maximum = m_mainForm.MaxServerItemId;
            }
            else if (findByCidButton.Checked)
            {
                this.itemIdGroupBox.Enabled = true;
                this.itemIdGroupBox.Text = "Client ID";
                this.propertiesGroupBox.Enabled = false;
                this.findIdNumericUpDown.Maximum = m_mainForm.MaxClientItemId;
            }
            else if (findByPropertiesButton.Checked)
            {
                this.itemIdGroupBox.Enabled = false;
                this.propertiesGroupBox.Enabled = true;
            }
        }

        private void StartFind()
        {
            this.serverItemList.Clear();
            this.findItemButton.Enabled = false;

            if (findBySidButton.Checked)
            {
                this.serverItemList.Add(m_mainForm.ServerItems.FindByServerId((ushort)this.findIdNumericUpDown.Value));
            }
            else if (findByCidButton.Checked)
            {
                this.serverItemList.Add(m_mainForm.ServerItems.FindByClientId((ushort)this.findIdNumericUpDown.Value));
            }
            else if (findByPropertiesButton.Checked)
            {
                this.serverItemList.Add(m_mainForm.ServerItems.FindByProperties(m_properties));
            }

            this.findItemButton.Enabled = true;
        }

        #endregion

        #region Event Handlers

        private void FindItemButton_Click(object sender, EventArgs e)
        {
            this.StartFind();
        }

        private void FindIdNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.StartFind();
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateProperties();
        }

        private void PropertyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FlagCheckBox checkBox = (FlagCheckBox)sender;
            if (checkBox.Checked)
            {
                m_properties |= checkBox.ServerItemFlag;
            }
            else
            {
                m_properties &= ~checkBox.ServerItemFlag;
            }
        }

        private void ServerItemList_SelectedIndexChanged(object sender)
        {
            ServerItem item = serverItemList.SelectedItem != null ? serverItemList.SelectedItem : null;
            if (item != null)
            {
                if (item.Type == ServerItemType.Deprecated)
                {
                    MessageBox.Show(string.Format("The id {0} is a deprecated item.", item.ID), "Deprecated Item");
                }
                else
                {
                    m_mainForm.SelectItem(item.ID);
                }
            }
        }

        private void MainForm_CleanedHandler(object sender, EventArgs e)
        {
            this.serverItemList.Clear();
            this.UpdateProperties();
        }

        #endregion
    }
}
