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
#endregion

namespace ItemEditor
{
    public class Utils
    {
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
            {
                return false;
            }

            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string FindClientFile(string directory, string extension)
        {
            if (Directory.Exists(directory))
            {
                string path = Path.Combine(directory, "Tibia" + extension);
                if (File.Exists(path))
                {
                    return path;
                }

                foreach (string fileOn in Directory.GetFiles(directory))
                {
                    FileInfo file = new FileInfo(fileOn);
                    if (file.Extension.Equals(extension))
                    {
                        return file.FullName;
                    }
                }
            }

            return string.Empty;
        }
    }
}
