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
using OTLib.Collections;
using OTLib.OTB;
using OTLib.Server.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Dialogs
{
    public partial class CompareOtbForm : DarkForm
    {
        #region Contructor

        public CompareOtbForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private bool CompareItems()
        {
            OtbReader reader1 = new OtbReader();
            if (!reader1.Read(this.file1Text.Text))
            {
                MessageBox.Show("Could not open {0}.", this.file1Text.Text);
                return false;
            }

            OtbReader reader2 = new OtbReader();
            if (!reader2.Read(this.file2Text.Text))
            {
                MessageBox.Show("Could not open {0}.", this.file2Text.Text);
                return false;
            }

            IEnumerator<ServerItem> enumerator1 = reader1.Items.GetEnumerator();
            IEnumerator<ServerItem> enumerator2 = reader2.Items.GetEnumerator();

            if (reader1.Count != reader2.Count)
            {
                resultTextBox.AppendText(string.Format("Item count:  [ {0} / {1} ]" + Environment.NewLine, reader1.Count, reader2.Count));
            }

            while (enumerator1.MoveNext())
            {
                if (!enumerator2.MoveNext())
                {
                    return false;
                }

                ServerItem item1 = enumerator1.Current;
                ServerItem item2 = enumerator2.Current;

                if (item1.ClientId != item2.ClientId)
                {
                    resultTextBox.AppendText(string.Format("ID: {0}  -  Sprite changed  -  [ {1} / {2} ]" + Environment.NewLine, item1.ID, item1.ClientId, item2.ClientId));
                    continue;
                }

                if (item1.SpriteHash != null && item2.SpriteHash != null && !Utils.ByteArrayCompare(item1.SpriteHash, item2.SpriteHash))
                {
                    resultTextBox.AppendText(string.Format("ID: {0}  -  Sprite updated." + Environment.NewLine, item1.ID));
                }

                foreach (PropertyInfo property in item1.GetType().GetProperties())
                {
                    if (property.Name != "SpriteHash" && property.Name != "ClientId")
                    {
                        object value1 = property.GetValue(item1, null);
                        object value2 = item2.GetType().GetProperty(property.Name).GetValue(item2, null);

                        if (!value1.Equals(value2))
                        {
                            resultTextBox.AppendText(string.Format("ID: {0}  -  {1}  -  [ {2} / {3} ]{4}", item1.ID, property.Name, value1, value2, Environment.NewLine));
                        }
                    }
                }
            }

            if (resultTextBox.Text.Length == 0)
            {
                MessageBox.Show("No differences found!");
            }

            return true;
        }

        #endregion

        #region Event Handlers

        private void CompareButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();
            CompareItems();
        }

        private void BrowseButton1_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();

            // Now set the file type
            dialog.Filter = "OTB files (*.otb)|*.otb";
            dialog.Title = "Open OTB File";

            if (dialog.ShowDialog() != DialogResult.OK || dialog.FileName.Length == 0)
            {
                return;
            }

            file1Text.Text = dialog.FileName;
        }

        private void BrowseButton2_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();

            // Now set the file type
            dialog.Filter = "OTB files (*.otb)|*.otb";
            dialog.Title = "Open OTB File";

            if (dialog.ShowDialog() != DialogResult.OK || dialog.FileName.Length == 0)
            {
                return;
            }

            file2Text.Text = dialog.FileName;
        }

        private void FileText_TextChanged(object sender, EventArgs e)
        {
            compareButton.Enabled = (file1Text.TextLength > 0 && file2Text.TextLength > 0);
        }

        #endregion
    }
}
