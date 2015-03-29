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

using ItemEditor.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ItemEditor.Dialogs
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
			InitializeGraphics();
		}

		#region Methods

		/// <summary>
		/// Attaches the image to the imagebox
		/// </summary>
		private void InitializeGraphics()
		{
			this.pictureBox.Image = ItemEditor.Properties.Resources.about_background;
			this.versionLabel.Text = MainForm.versionString;
			this.copyLabel.Text = "Development: EdMignari and all contributors of the original project.";
			this.linkLabel.Text = "https://github.com/Mignari/ItemEditor";
		}

		#endregion

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/Mignari/ItemEditor");
		}
	}
}
