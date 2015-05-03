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

using System;
using System.Windows.Forms;

namespace ItemEditor.Dialogs
{
    public partial class FindItemForm : Form
    {
        #region Constructor

        public FindItemForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Properties

        private MainForm mainForm = null;

        #endregion

        #region Public Properties

        public MainForm MainForm
        {
            get { return mainForm; }
            set
            {
                mainForm = value;
                if (mainForm != null)
                {
                    findItemNumericUpDown.Minimum = mainForm.MinItemId;
                    findItemNumericUpDown.Maximum = mainForm.MaxItemId;
                }
            }
        }

        #endregion

        #region General Methods

        private void OnFind(UInt16 sid)
        {
            if (mainForm != null)
            {
                if (!mainForm.SelectItem(sid))
                {
                    MessageBox.Show(String.Format("Item id {0} not found.", sid), "Find Item");
                }
            }
        }

        #endregion

        #region Event Handlers

        private void findItemButton_Click(object sender, EventArgs e)
        {
            this.OnFind((UInt16)findItemNumericUpDown.Value);
        }

        private void findItemNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnFind((UInt16)findItemNumericUpDown.Value);
            }
        }

        #endregion
    }
}
