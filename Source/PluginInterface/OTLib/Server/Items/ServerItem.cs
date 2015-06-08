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
using ItemEditor;
using System;
#endregion

namespace OTLib.Server.Items
{
    public enum ServerItemGroup
    {
        None = 0,
        Ground,
        Container,
        Weapon,
        Ammunition,
        Armor,
        Changes,
        Teleport,
        MagicField,
        Writable,
        Key,
        Splash,
        Fluid,
        Door,
        Deprecated
    }

    public enum ServerItemType
    {
        None,
        Ground,
        Container,
        Fluid,
        Splash,
        Deprecated
    }

    public class ServerItem : Item
    {
        #region Contructors
        
        public ServerItem()
        {
            ////
        }

        public ServerItem(Item item)
        {
            if (item != null)
            {
                this.itemImpl = (ItemImpl)item.itemImpl.Clone();
            }
        }

        #endregion

        #region Public Properties

        public ushort ClientId { get; set; }

        public ushort PreviousClientId { get; set; }

        /// <summary>
        /// Used during an update to indicate if this item has been updated.
        /// </summary>
        public bool SpriteAssigned { get; set; }

        /// <summary>
        /// An custom created item id.
        /// </summary>
        public bool IsCustomCreated { get; set; }

        #endregion

        #region Public Methods
        
        override public string ToString()
        {
            if (!string.IsNullOrEmpty(name))
            {
                return this.id.ToString() + " - " + this.name;
            }

            return this.id.ToString();
        }

        #endregion
    }
}
