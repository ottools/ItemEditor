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

                        item.isUnpassable = ((flags & ServerItemFlag.BLOCK_SOLID) == ServerItemFlag.BLOCK_SOLID);
                        item.blockMissiles = ((flags & ServerItemFlag.BLOCK_MISSILE) == ServerItemFlag.BLOCK_MISSILE);
                        item.blockPathfinder = ((flags & ServerItemFlag.BLOCK_PATHFINDER) == ServerItemFlag.BLOCK_PATHFINDER);
                        item.isPickupable = ((flags & ServerItemFlag.PICKUPABLE) == ServerItemFlag.PICKUPABLE);
                        item.isMoveable = ((flags & ServerItemFlag.MOVEABLE) == ServerItemFlag.MOVEABLE);
                        item.isStackable = ((flags & ServerItemFlag.STACKABLE) == ServerItemFlag.STACKABLE);
                        item.alwaysOnTop = ((flags & ServerItemFlag.ALWAYS_ON_TOP) == ServerItemFlag.ALWAYS_ON_TOP);
                        item.isVertical = ((flags & ServerItemFlag.VERTICAL_WALL) == ServerItemFlag.VERTICAL_WALL);
                        item.isHorizontal = ((flags & ServerItemFlag.HORIZONTAL_WALL) == ServerItemFlag.HORIZONTAL_WALL);
                        item.isHangable = ((flags & ServerItemFlag.HANGABLE) == ServerItemFlag.HANGABLE);
                        item.isRotatable = ((flags & ServerItemFlag.ROTABLE) == ServerItemFlag.ROTABLE);
                        item.isReadable = ((flags & ServerItemFlag.READABLE) == ServerItemFlag.READABLE);
                        item.multiUse = ((flags & ServerItemFlag.USEABLE) == ServerItemFlag.USEABLE);
                        item.hasElevation = ((flags & ServerItemFlag.HAS_ELEVATION) == ServerItemFlag.HAS_ELEVATION);
                        item.ignoreLook = ((flags & ServerItemFlag.IGNORE_LOOK) == ServerItemFlag.IGNORE_LOOK);
                        item.allowDistRead = ((flags & ServerItemFlag.ALLOW_DISTANCE_READ) == ServerItemFlag.ALLOW_DISTANCE_READ);
                        item.isAnimation = ((flags & ServerItemFlag.ANIMATION) == ServerItemFlag.ANIMATION);
                        item.fullGround = ((flags & ServerItemFlag.FULL_GROUND) == ServerItemFlag.FULL_GROUND);

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
