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
using System;
using System.IO;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Helpers
{
    public class PathHelper
    {
        /// <summary>
        /// Path to the application directory
        /// </summary>
        public static string ApplicationDirectory
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath);
            }
        }

        /// <summary>
        /// Path to the user's application directory
        /// </summary>
        public static string ApplicationData
        {
            get
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(folder, "ItemEditor");
            }
        }

        /// <summary>
        /// Path to the Plugins directory
        /// </summary>
        public static string Plugins
        {
            get
            {
                return Path.Combine(ApplicationDirectory, "Plugins");
            }
        }
    }
}
