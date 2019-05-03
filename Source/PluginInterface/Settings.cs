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
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
#endregion

namespace PluginInterface
{
    public class Settings
    {
        #region Private Properties

        private XmlDocument xmlDocument;

        #endregion

        #region Constrctor

        public Settings()
        {
            this.xmlDocument = new XmlDocument();
        }

        #endregion

        #region Public Properties

        public string SettingsFilename { get; private set; }

        #endregion

        #region Public Methods

        public bool Load(string filename)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            try
            {
                this.SettingsFilename = Path.Combine(path, filename);
                this.xmlDocument.Load(this.SettingsFilename);
                return true;
            }
            catch
            {
                this.xmlDocument.LoadXml("<settings></settings>");
                return false;
            }
        }

        public List<SupportedClient> GetSupportedClientList()
        {
            List<SupportedClient> list = new List<SupportedClient>();

            XmlNodeList nodes = this.xmlDocument.SelectNodes("/settings/clients/client");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        uint version = uint.Parse(node.Attributes["version"].Value);
                        string description = node.Attributes["description"].Value;
                        uint otbVersion = uint.Parse(node.Attributes["otbversion"].Value);
                        uint datSignature = uint.Parse(node.Attributes["datsignature"].Value, System.Globalization.NumberStyles.HexNumber);
                        uint sprSignature = uint.Parse(node.Attributes["sprsignature"].Value, System.Globalization.NumberStyles.HexNumber);

                        SupportedClient client = new SupportedClient(version, description, otbVersion, datSignature, sprSignature);
                        list.Add(client);
                    }
                    catch
                    {
                        MessageBox.Show(string.Format("Error loading file {0}", this.SettingsFilename));
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
