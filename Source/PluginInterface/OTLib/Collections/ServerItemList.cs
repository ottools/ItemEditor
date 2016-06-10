#region Licence
/**
* Copyright © 2014-2016 OTTools <https://github.com/ottools>
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
using System.Collections.Generic;
#endregion

namespace OTLib.Collections
{
    public class ServerItemList
    {
        #region Contructor

        public ServerItemList()
        {
            this.Items = new List<ServerItem>();
            this.MinId = 100;
            this.MaxId = 100;
        }

        #endregion

        #region Public Properties

        public List<ServerItem> Items { get; private set; }

        public ushort MinId { get; private set; }

        public ushort MaxId { get; private set; }

        public int Count { get { return this.Items.Count; } }

        public uint MajorVersion { get; set; }

        public uint MinorVersion { get; set; }

        public uint BuildNumber { get; set; }

        public uint ClientVersion { get; set; }

        #endregion

        #region Public Methods

        public void Add(ServerItem item)
        {
            if (this.Items.Contains(item))
            {
                return;
            }

            this.Items.Add(item);

            // increase max id
            if (this.MaxId < item.ID)
            {
                this.MaxId = item.ID;
            }
        }

        public IEnumerator<ServerItem> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        public List<ServerItem> FindByServerId(ushort sid)
        {
            return this.Items.FindAll(i => i.ID == sid);
        }

        public List<ServerItem> FindByClientId(ushort cid)
        {
            return this.Items.FindAll(i => i.ClientId == cid);
        }

        public List<ServerItem> FindByProperties(ServerItemFlag properties)
        {
            return this.Items.FindAll(i => i.HasProperties(properties));
        }

        public void Clear()
        {
            this.Items.Clear();
            this.MaxId = 100;
        }

        #endregion
    }
}
