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
using OTLib.Collections;
using OTLib.Server.Items;
using OTLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#endregion

namespace OTLib.OTB
{
    public class OtbWriter
    {
        #region Constructor

        public OtbWriter(ServerItemList items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.Items = items;
        }

        #endregion

        #region Public Properties

        public ServerItemList Items { get; private set; }

        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        #endregion

        #region Public Methods

        public bool Write(string path)
        {
            try
            {
                using (BinaryTreeWriter writer = new BinaryTreeWriter(path))
                {
                    writer.WriteUInt32(0, false); // version, always 0

                    writer.CreateNode(0); // root node
                    writer.WriteUInt32(0, true); // flags, unused for root node

                    VersionInfo vi = new VersionInfo();

                    vi.MajorVersion = this.Items.MajorVersion;
                    vi.MinorVersion = this.Items.MinorVersion;
                    vi.BuildNumber = this.Items.BuildNumber;
                    vi.CSDVersion = string.Format("OTB {0}.{1}.{2}-{3}.{4}", vi.MajorVersion, vi.MinorVersion, vi.BuildNumber, this.Items.ClientVersion / 100, this.Items.ClientVersion % 100);

                    MemoryStream ms = new MemoryStream();
                    BinaryWriter property = new BinaryWriter(ms);
                    property.Write(vi.MajorVersion);
                    property.Write(vi.MinorVersion);
                    property.Write(vi.BuildNumber);
                    byte[] CSDVersion = Encoding.ASCII.GetBytes(vi.CSDVersion);
                    Array.Resize(ref CSDVersion, 128);
                    property.Write(CSDVersion);

                    writer.WriteProp(RootAttribute.Version, property);

                    foreach (ServerItem item in this.Items)
                    {
                        List<ServerItemAttribute> saveAttributeList = new List<ServerItemAttribute>();
                        saveAttributeList.Add(ServerItemAttribute.ServerID);

                        if (item.type != ServerItemType.Deprecated)
                        {
                            saveAttributeList.Add(ServerItemAttribute.ClientID);
                            saveAttributeList.Add(ServerItemAttribute.SpriteHash);

                            if (item.minimapColor != 0)
                            {
                                saveAttributeList.Add(ServerItemAttribute.MinimaColor);
                            }

                            if (item.maxReadWriteChars != 0)
                            {
                                saveAttributeList.Add(ServerItemAttribute.MaxReadWriteChars);
                            }

                            if (item.maxReadChars != 0)
                            {
                                saveAttributeList.Add(ServerItemAttribute.MaxReadChars);
                            }

                            if (item.lightLevel != 0 || item.lightColor != 0)
                            {
                                saveAttributeList.Add(ServerItemAttribute.Light);
                            }

                            if (item.type == ServerItemType.Ground)
                            {
                                saveAttributeList.Add(ServerItemAttribute.GroundSpeed);
                            }

                            if (item.alwaysOnTop)
                            {
                                saveAttributeList.Add(ServerItemAttribute.TopOrder);
                            }

                            if (item.tradeAs != 0)
                            {
                                saveAttributeList.Add(ServerItemAttribute.TradeAs);
                            }

                            if (!string.IsNullOrEmpty(item.name))
                            {
                                saveAttributeList.Add(ServerItemAttribute.Name);
                            }
                        }

                        switch (item.type)
                        {
                            case ServerItemType.Container:
                                writer.CreateNode((byte)ServerItemGroup.Container);
                                break;

                            case ServerItemType.Fluid:
                                writer.CreateNode((byte)ServerItemGroup.Fluid);
                                break;
                            case ServerItemType.Ground:
                                writer.CreateNode((byte)ServerItemGroup.Ground);
                                break;

                            case ServerItemType.Splash:
                                writer.CreateNode((byte)ServerItemGroup.Splash);
                                break;

                            case ServerItemType.Deprecated:
                                writer.CreateNode((byte)ServerItemGroup.Deprecated);
                                break;

                            default:
                                writer.CreateNode((byte)ServerItemGroup.None);
                                break;
                        }

                        uint flags = 0;

                        if (item.isUnpassable)
                        {
                            flags |= (uint)ServerItemFlag.Unpassable;
                        }

                        if (item.blockMissiles)
                        {
                            flags |= (uint)ServerItemFlag.BlockMissiles;
                        }

                        if (item.blockPathfinder)
                        {
                            flags |= (uint)ServerItemFlag.BlockPathfinder;
                        }

                        if (item.hasElevation)
                        {
                            flags |= (uint)ServerItemFlag.HasElevation;
                        }

                        if (item.multiUse)
                        {
                            flags |= (uint)ServerItemFlag.Useable;
                        }

                        if (item.isPickupable)
                        {
                            flags |= (uint)ServerItemFlag.Pickupable;
                        }

                        if (item.isMoveable)
                        {
                            flags |= (uint)ServerItemFlag.Movable;
                        }

                        if (item.isStackable)
                        {
                            flags |= (uint)ServerItemFlag.Stackable;
                        }

                        if (item.alwaysOnTop)
                        {
                            flags |= (uint)ServerItemFlag.StackOrder;
                        }

                        if (item.isReadable)
                        {
                            flags |= (uint)ServerItemFlag.Readable;
                        }

                        if (item.isRotatable)
                        {
                            flags |= (uint)ServerItemFlag.Rotatable;
                        }

                        if (item.isHangable)
                        {
                            flags |= (uint)ServerItemFlag.Hangable;
                        }

                        if (item.isVertical)
                        {
                            flags |= (uint)ServerItemFlag.HookSouth;
                        }

                        if (item.isHorizontal)
                        {
                            flags |= (uint)ServerItemFlag.HookEast;
                        }

                        if (item.ignoreLook)
                        {
                            flags |= (uint)ServerItemFlag.IgnoreLook;
                        }

                        if (item.allowDistRead)
                        {
                            flags |= (uint)ServerItemFlag.AllowDistanceRead;
                        }

                        if (item.isAnimation)
                        {
                            flags |= (uint)ServerItemFlag.IsAnimation;
                        }

                        if (item.fullGround)
                        {
                            flags |= (uint)ServerItemFlag.FullGround;
                        }

                        writer.WriteUInt32(flags, true);

                        foreach (ServerItemAttribute attribute in saveAttributeList)
                        {
                            switch (attribute)
                            {
                                case ServerItemAttribute.ServerID:
                                    property.Write((ushort)item.id);
                                    writer.WriteProp(ServerItemAttribute.ServerID, property);
                                    break;

                                case ServerItemAttribute.TradeAs:
                                    property.Write((ushort)item.tradeAs);
                                    writer.WriteProp(ServerItemAttribute.TradeAs, property);
                                    break;

                                case ServerItemAttribute.ClientID:
                                    property.Write((ushort)item.ClientId);
                                    writer.WriteProp(ServerItemAttribute.ClientID, property);
                                    break;

                                case ServerItemAttribute.GroundSpeed:
                                    property.Write((ushort)item.groundSpeed);
                                    writer.WriteProp(ServerItemAttribute.GroundSpeed, property);
                                    break;

                                case ServerItemAttribute.Name:
                                    for (ushort i = 0; i < item.name.Length; ++i)
                                    {
                                        property.Write((char)item.name[i]);
                                    }

                                    writer.WriteProp(ServerItemAttribute.Name, property);
                                    break;

                                case ServerItemAttribute.SpriteHash:
                                    property.Write(item.SpriteHash);
                                    writer.WriteProp(ServerItemAttribute.SpriteHash, property);
                                    break;

                                case ServerItemAttribute.MinimaColor:
                                    property.Write((ushort)item.minimapColor);
                                    writer.WriteProp(ServerItemAttribute.MinimaColor, property);
                                    break;

                                case ServerItemAttribute.MaxReadWriteChars:
                                    property.Write((ushort)item.maxReadWriteChars);
                                    writer.WriteProp(ServerItemAttribute.MaxReadWriteChars, property);
                                    break;

                                case ServerItemAttribute.MaxReadChars:
                                    property.Write((ushort)item.maxReadChars);
                                    writer.WriteProp(ServerItemAttribute.MaxReadChars, property);
                                    break;

                                case ServerItemAttribute.Light:
                                    property.Write((ushort)item.lightLevel);
                                    property.Write((ushort)item.lightColor);
                                    writer.WriteProp(ServerItemAttribute.Light, property);
                                    break;

                                case ServerItemAttribute.TopOrder:
                                    property.Write((byte)item.alwaysOnTopOrder);
                                    writer.WriteProp(ServerItemAttribute.TopOrder, property);
                                    break;
                            }
                        }

                        writer.CloseNode();
                    }

                    writer.CloseNode();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
