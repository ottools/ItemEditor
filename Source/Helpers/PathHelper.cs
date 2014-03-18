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

using System;
using System.IO;
using System.Windows.Forms;

namespace ItemEditor.Helpers
{
	class PathHelper
	{
		/// <summary>
		/// Path to the application directory
		/// </summary>
		public static String ApplicationDirectory
		{
			get
			{
				return Path.GetDirectoryName(Application.ExecutablePath);
			}
		}

		/// <summary>
		/// Path to the appdata folder
		/// </summary>
		public static String ApplicationData
		{
			get
			{
				string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				return Path.Combine(folder, "ItemEditor");
			}
		}

		/// <summary>
		/// Path to the plug-ins directory
		/// </summary>
		public static String Plugins
		{
			get
			{
				return Path.Combine(ApplicationDirectory, "Plug-ins");
			}
		}
	}
}
