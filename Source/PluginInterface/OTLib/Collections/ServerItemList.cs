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

using OTLib.Server.Items;
using System.Collections.Generic;
using System.Linq;

namespace OTLib.Collections
{
    public class ServerItemList
    {
        public ServerItemList()
        {
            Items = new List<ServerItem>();
            MinId = 100;
            MaxId = 100;
        }

        public List<ServerItem> Items { get; private set; }
        public ushort MinId { get; private set; }
        public ushort MaxId { get; private set; }
        public int Count => Items.Count;
        public uint MajorVersion { get; set; }
        public uint MinorVersion { get; set; }
        public uint BuildNumber { get; set; }
        public uint ClientVersion { get; set; }

        public void Add(ServerItem item)
        {
            if (Items.Contains(item))
                return;

            Items.Add(item);

            if (MaxId < item.ID)
                MaxId = item.ID;
        }

        public IEnumerator<ServerItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public List<ServerItem> FindByServerId(ushort sid)
        {
            return Items.FindAll(i => i.ID == sid);
        }

        public List<ServerItem> FindByClientId(ushort cid)
        {
            return Items.FindAll(i => i.ClientId == cid);
        }

        public List<ServerItem> FindByProperties(ServerItemFlag properties)
        {
            return Items.FindAll(i => i.HasProperties(properties));
        }

        public bool TryGetValue(ushort sid, out ServerItem item)
        {
            item = Items.FirstOrDefault(i => i.ID == sid);
            return item != null;
        }

        public void Clear()
        {
            Items.Clear();
            MaxId = 100;
        }
    }
}
