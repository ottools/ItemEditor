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
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ItemEditor.Settings
{
	[Serializable]
	public class Preferences
	{
		public String ClientDirectory { get; set; }
		public UInt32 DatSignature { get; set; }
		public UInt32 SprSignature { get; set; }
		public Boolean Extended { get; set; }
		public Boolean Transparency { get; set; }

		public void Load()
		{
			string fileName = FileNameHelper.SettingData;
			if (File.Exists(fileName))
			{
				XmlSerializer deserializer = new XmlSerializer(typeof(Preferences));
				TextReader reader = new StreamReader(fileName);
				object obj = deserializer.Deserialize(reader);
				Program.preferences = (Preferences)obj;
				reader.Close();
			}
		}

		public void Save()
		{
			try
			{
				string fileName = FileNameHelper.SettingData;
				if (!File.Exists(fileName))
				{
					String folder = Path.GetDirectoryName(fileName);
					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}

					XmlSerializer serializer = new XmlSerializer(typeof(Preferences));
					using (TextWriter writer = new StreamWriter(fileName))
					{
						serializer.Serialize(writer, this);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
	}
}
