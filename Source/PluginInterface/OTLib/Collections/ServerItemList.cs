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

#region Using Statements
using OTLib.Server.Items;
using System;
using System.Collections.Generic;
#endregion

namespace OTLib.Collections
{
    public class ServerItemList : List<ServerItem>
    {
        #region Contructor

        public ServerItemList()
        {
            this.MinId = 100;
        }

        #endregion

        #region Public Properties

        public ushort MinId { get; set; }

        public ushort MaxId
        {
            get { return (ushort)(MinId + Count - 1); }
        }

        public uint MajorVersion { get; set; }

        public uint MinorVersion { get; set; }

        public uint BuildNumber { get; set; }

        public uint ClientVersion { get; set; }

        #endregion
    }
}
