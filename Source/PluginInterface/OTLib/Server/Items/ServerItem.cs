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
using System.Reflection;
#endregion

namespace OTLib.Server.Items
{
    public enum ServerItemGroup : byte
    {
        None        = 0,
        Ground      = 1,
        Container   = 2,
        Weapon      = 3,
        Ammunition  = 4,
        Armor       = 5,
        Changes     = 6,
        Teleport    = 7,
        MagicField  = 8,
        Writable    = 9,
        Key         = 10,
        Splash      = 11,
        Fluid       = 12,
        Door        = 13,
        Deprecated  = 14
    }

    public enum ServerItemType : byte
    {
        None        = 0,
        Ground      = 1,
        Container   = 2,
        Fluid       = 3,
        Splash      = 4,
        Deprecated  = 5
    }

    public enum TileStackOrder : byte
    {
        None    = 0,
        Border  = 1,
        Bottom  = 2,
        Top     = 3
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
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.CopyPropertiesFrom(item);
        }

        #endregion

        #region Public Properties

        public string NameXml { get; set; }
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
            if (!string.IsNullOrEmpty(NameXml))
                return ID.ToString() + " - " + this.NameXml;
            else if (!string.IsNullOrEmpty(Name))
                return ID.ToString() + " - " + this.Name;

            return ID.ToString();
        }

        #endregion
    }
}
