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
using System.Collections.Generic;
using System.Xml;

namespace PluginInterface
{
	public class Settings
	{
		public string SettingFilename = "";
		private XmlDocument xmlDocument = new XmlDocument();

		public Settings()
		{
		}

		public bool Load(string filename)
		{
			string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Plugins");
			try
			{
				SettingFilename = System.IO.Path.Combine(path, filename);
				xmlDocument.Load(SettingFilename);
				return true;
			}
			catch
			{
				xmlDocument.LoadXml("<settings></settings>");
				return false;
			}
		}

		public List<SupportedClient> GetSupportedClientList()
		{
			List<SupportedClient> list = new List<SupportedClient>();

			XmlNodeList nodes = xmlDocument.SelectNodes("/settings/clients/client");
			if (nodes != null)
			{
				foreach (XmlNode node in nodes)
				{
					try
					{
						UInt32 version = UInt32.Parse(node.Attributes["version"].Value);
						string description = node.Attributes["description"].Value;
						UInt32 otbVersion = UInt32.Parse(node.Attributes["otbversion"].Value);
						UInt32 datSignature = (UInt32)System.Int32.Parse(node.Attributes["datsignature"].Value, System.Globalization.NumberStyles.HexNumber);
						UInt32 sprSignature = (UInt32)System.Int32.Parse(node.Attributes["sprsignature"].Value, System.Globalization.NumberStyles.HexNumber);

						SupportedClient client = new SupportedClient(version, description, otbVersion, datSignature, sprSignature);
						list.Add(client);
					}
					catch
					{
						System.Windows.Forms.MessageBox.Show(String.Format("Error loading file {0}", SettingFilename));
					}
				}
			}

			return list;
		}
	}
}
