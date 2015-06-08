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
using OTLib.Collections;
using OTLib.Server.Items;
using OTLib.Utils;
using System;
using System.Diagnostics;
using System.IO;
#endregion

namespace OTLib.OTB
{
    public class OtbReader
    {
        #region Contructor
        
        public OtbReader()
        {
            this.Items = new ServerItemList();
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

        public bool Read(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                using (BinaryTreeReader reader = new BinaryTreeReader(path))
                {
                    // get root node
                    BinaryReader node = reader.GetRootNode();
                    if (node == null)
                    {
                        return false;
                    }

                    node.ReadByte(); // first byte of otb is 0
                    node.ReadUInt32(); // 4 bytes flags, unused

                    byte attr = node.ReadByte();
                    if ((RootAttribute)attr == RootAttribute.Version)
                    {
                        ushort datalen = node.ReadUInt16();
                        if (datalen != 140) // 4 + 4 + 4 + 1 * 128
                        {
                            Trace.WriteLine(String.Format("Size of version header is invalid, updated .otb version?"));
                            return false;
                        }

                        this.Items.MajorVersion = node.ReadUInt32(); // major, file version
                        this.Items.MinorVersion = node.ReadUInt32(); // minor, client version
                        this.Items.BuildNumber = node.ReadUInt32();  // build number, revision
                        node.BaseStream.Seek(128, SeekOrigin.Current);
                    }

                    node = reader.GetChildNode();
                    if (node == null)
                    {
                        return false;
                    }

                    do
                    {
                        ServerItem item = new ServerItem();

                        ServerItemGroup itemGroup = (ServerItemGroup)node.ReadByte();
                        switch (itemGroup)
                        {
                            case ServerItemGroup.None:
                                item.type = ServerItemType.None;
                                break;

                            case ServerItemGroup.Ground:
                                item.type = ServerItemType.Ground;
                                break;

                            case ServerItemGroup.Splash:
                                item.type = ServerItemType.Splash;
                                break;

                            case ServerItemGroup.Fluid:
                                item.type = ServerItemType.Fluid;
                                break;

                            case ServerItemGroup.Container:
                                item.type = ServerItemType.Container;
                                break;

                            case ServerItemGroup.Deprecated:
                                item.type = ServerItemType.Deprecated;
                                break;
                        }

                        ServerItemFlag flags = (ServerItemFlag)node.ReadUInt32();

                        item.isUnpassable = ((flags & ServerItemFlag.Unpassable) == ServerItemFlag.Unpassable);
                        item.blockMissiles = ((flags & ServerItemFlag.BlockMissiles) == ServerItemFlag.BlockMissiles);
                        item.blockPathfinder = ((flags & ServerItemFlag.BlockPathfinder) == ServerItemFlag.BlockPathfinder);
                        item.isPickupable = ((flags & ServerItemFlag.Pickupable) == ServerItemFlag.Pickupable);
                        item.isMoveable = ((flags & ServerItemFlag.Movable) == ServerItemFlag.Movable);
                        item.isStackable = ((flags & ServerItemFlag.Stackable) == ServerItemFlag.Stackable);
                        item.alwaysOnTop = ((flags & ServerItemFlag.StackOrder) == ServerItemFlag.StackOrder);
                        item.isVertical = ((flags & ServerItemFlag.HookSouth) == ServerItemFlag.HookSouth);
                        item.isHorizontal = ((flags & ServerItemFlag.HookEast) == ServerItemFlag.HookEast);
                        item.isHangable = ((flags & ServerItemFlag.Hangable) == ServerItemFlag.Hangable);
                        item.isRotatable = ((flags & ServerItemFlag.Rotatable) == ServerItemFlag.Rotatable);
                        item.isReadable = ((flags & ServerItemFlag.Readable) == ServerItemFlag.Readable);
                        item.multiUse = ((flags & ServerItemFlag.Useable) == ServerItemFlag.Useable);
                        item.hasElevation = ((flags & ServerItemFlag.HasElevation) == ServerItemFlag.HasElevation);
                        item.ignoreLook = ((flags & ServerItemFlag.IgnoreLook) == ServerItemFlag.IgnoreLook);
                        item.allowDistRead = ((flags & ServerItemFlag.AllowDistanceRead) == ServerItemFlag.AllowDistanceRead);
                        item.isAnimation = ((flags & ServerItemFlag.IsAnimation) == ServerItemFlag.IsAnimation);
                        item.fullGround = ((flags & ServerItemFlag.FullGround) == ServerItemFlag.FullGround);

                        while (node.PeekChar() != -1)
                        {
                            ServerItemAttribute attribute = (ServerItemAttribute)node.ReadByte();
                            ushort datalen = node.ReadUInt16();

                            switch (attribute)
                            {
                                case ServerItemAttribute.ServerID:
                                    item.id = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.ClientID:
                                    item.ClientId = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.GroundSpeed:
                                    item.groundSpeed = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.Name:
                                    item.name = new string(node.ReadChars(datalen));
                                    break;

                                case ServerItemAttribute.SpriteHash:
                                    item.SpriteHash = node.ReadBytes(datalen);
                                    break;

                                case ServerItemAttribute.MinimaColor:
                                    item.minimapColor = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.MaxReadWriteChars:
                                    item.maxReadWriteChars = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.MaxReadChars:
                                    item.maxReadChars = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.Light:
                                    item.lightLevel = node.ReadUInt16();
                                    item.lightColor = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.TopOrder:
                                    item.alwaysOnTopOrder = node.ReadByte();
                                    break;

                                case ServerItemAttribute.TradeAs:
                                    item.tradeAs = node.ReadUInt16();
                                    break;

                                default:
                                    node.BaseStream.Seek(datalen, SeekOrigin.Current);
                                    break;
                            }
                        }

                        if (item.SpriteHash == null && item.type != ServerItemType.Deprecated)
                        {
                            item.SpriteHash = new byte[16];
                        }

                        this.Items.Add(item);
                        node = reader.GetNextNode();

                    } while (node != null);
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
