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

using ItemEditor;
using System;
using System.Collections.Generic;

namespace PluginInterface
{
    public class ClientItems : Dictionary<UInt16, ClientItem>
    {
        public bool signatureCalculated = false;
    }

    public class SupportedClient
    {
        #region Private Properties

        private UInt32 _version;
        private string _description;
        private UInt32 _otbVersion;
        private UInt32 _datSignature;
        private UInt32 _sprSignature;

        #endregion

        #region Constructor

        public SupportedClient(
            UInt32 version,
            string description,
            UInt32 otbVersion,
            UInt32 datSignature,
            UInt32 sprSignature)
        {
            this._version = version;
            this._description = description;
            this._otbVersion = otbVersion;
            this._datSignature = datSignature;
            this._sprSignature = sprSignature;
        }

        #endregion

        #region Public Properties

        public UInt32 Version
        {
            get { return _version; }
        }

        public string Description
        {
            get { return _description; }
        }

        public UInt32 OtbVersion
        {
            get { return _otbVersion; }
        }

        public UInt32 DatSignature
        {
            get { return _datSignature; }
        }

        public UInt32 SprSignature
        {
            get { return _sprSignature; }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this._description;
        }

        #endregion
    }

    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        ClientItems Items { get; }
        List<SupportedClient> SupportedClients { get; }
        bool LoadClient(SupportedClient client, bool extended, bool transparency, string datFullPath, string sprFullPath);

        void Initialize();
        void Dispose();
    }

    public interface IPluginHost
    {
    }
}
