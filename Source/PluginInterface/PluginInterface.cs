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
using ItemEditor;
using System;
using System.Collections.Generic;
#endregion

namespace PluginInterface
{
    public interface IPlugin : IDisposable
    {
        IPluginHost Host { get; set; }

        ClientItems Items { get; }

        ushort MinItemId { get; }

        ushort MaxItemId { get; }

        List<SupportedClient> SupportedClients { get; }

        bool Loaded { get; }

        bool LoadClient(SupportedClient client, bool extended, bool frameDurations, bool transparency, string datFullPath, string sprFullPath);

        void Initialize();

        SupportedClient GetClientBySignatures(uint datSignature, uint sprSignature);

        ClientItem GetClientItem(ushort id);
    }

    public interface IPluginHost
    {
        ////
    }

    public class ClientItems : Dictionary<ushort, ClientItem>
    {
        #region Public Properties

        public bool SignatureCalculated { get; set; }

        #endregion
    }

    public class SupportedClient
    {
        #region Constructor

        public SupportedClient(uint version, string description, uint otbVersion, uint datSignature, uint sprSignature)
        {
            this.Version = version;
            this.Description = description;
            this.OtbVersion = otbVersion;
            this.DatSignature = datSignature;
            this.SprSignature = sprSignature;
        }

        #endregion

        #region Public Properties

        public uint Version { get; private set; }

        public string Description { get; private set; }

        public uint OtbVersion { get; private set; }

        public uint DatSignature { get; private set; }

        public uint SprSignature { get; private set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.Description;
        }

        #endregion
    }
}
