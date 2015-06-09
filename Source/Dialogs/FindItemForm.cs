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

#region Using Statements
using System;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Dialogs
{
    public partial class FindItemForm : Form
    {
        #region Private Properties

        private MainForm mainForm = null;

        #endregion

        #region Constructor

        public FindItemForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        public MainForm MainForm
        {
            get
            {
                return this.mainForm;
            }

            set
            {
                this.mainForm = value;

                if (this.mainForm != null)
                {
                    this.findItemNumericUpDown.Minimum = this.mainForm.MinItemId;
                    this.findItemNumericUpDown.Maximum = this.mainForm.MaxItemId;
                }
            }
        }

        #endregion

        #region Private Methods

        private void OnFind(ushort sid)
        {
            if (this.mainForm != null)
            {
                if (!this.mainForm.SelectItem(sid))
                {
                    MessageBox.Show(string.Format("Item id {0} not found.", sid), "Find Item");
                }
            }
        }

        #endregion

        #region Event Handlers

        private void FindItemButton_Click(object sender, EventArgs e)
        {
            this.OnFind((ushort)this.findItemNumericUpDown.Value);
        }

        private void FindItemNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnFind((ushort)this.findItemNumericUpDown.Value);
            }
        }

        #endregion
    }
}
