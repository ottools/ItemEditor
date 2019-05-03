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
using OTLib.Collections;
using OTLib.Server.Items;
using OTLib.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
                                item.Type = ServerItemType.None;
                                break;

                            case ServerItemGroup.Ground:
                                item.Type = ServerItemType.Ground;
                                break;

                            case ServerItemGroup.Container:
                                item.Type = ServerItemType.Container;
                                break;

                            case ServerItemGroup.Splash:
                                item.Type = ServerItemType.Splash;
                                break;

                            case ServerItemGroup.Fluid:
                                item.Type = ServerItemType.Fluid;
                                break;

                            case ServerItemGroup.Deprecated:
                                item.Type = ServerItemType.Deprecated;
                                break;
                        }

                        ServerItemFlag flags = (ServerItemFlag)node.ReadUInt32();

                        item.Unpassable = ((flags & ServerItemFlag.Unpassable) == ServerItemFlag.Unpassable);
                        item.BlockMissiles = ((flags & ServerItemFlag.BlockMissiles) == ServerItemFlag.BlockMissiles);
                        item.BlockPathfinder = ((flags & ServerItemFlag.BlockPathfinder) == ServerItemFlag.BlockPathfinder);
                        item.HasElevation = ((flags & ServerItemFlag.HasElevation) == ServerItemFlag.HasElevation);
                        item.ForceUse = ((flags & ServerItemFlag.ForceUse) == ServerItemFlag.ForceUse);
                        item.MultiUse = ((flags & ServerItemFlag.MultiUse) == ServerItemFlag.MultiUse);
                        item.Pickupable = ((flags & ServerItemFlag.Pickupable) == ServerItemFlag.Pickupable);
                        item.Movable = ((flags & ServerItemFlag.Movable) == ServerItemFlag.Movable);
                        item.Stackable = ((flags & ServerItemFlag.Stackable) == ServerItemFlag.Stackable);
                        item.HasStackOrder = ((flags & ServerItemFlag.StackOrder) == ServerItemFlag.StackOrder);
                        item.Readable = ((flags & ServerItemFlag.Readable) == ServerItemFlag.Readable);
                        item.Rotatable = ((flags & ServerItemFlag.Rotatable) == ServerItemFlag.Rotatable);
                        item.Hangable = ((flags & ServerItemFlag.Hangable) == ServerItemFlag.Hangable);
                        item.HookSouth = ((flags & ServerItemFlag.HookSouth) == ServerItemFlag.HookSouth);
                        item.HookEast = ((flags & ServerItemFlag.HookEast) == ServerItemFlag.HookEast);
                        item.AllowDistanceRead = ((flags & ServerItemFlag.AllowDistanceRead) == ServerItemFlag.AllowDistanceRead);
                        item.HasCharges = ((flags & ServerItemFlag.ClientCharges) == ServerItemFlag.ClientCharges);
                        item.IgnoreLook = ((flags & ServerItemFlag.IgnoreLook) == ServerItemFlag.IgnoreLook);
                        item.FullGround = ((flags & ServerItemFlag.FullGround) == ServerItemFlag.FullGround);
                        item.IsAnimation = ((flags & ServerItemFlag.IsAnimation) == ServerItemFlag.IsAnimation);

                        while (node.PeekChar() != -1)
                        {
                            ServerItemAttribute attribute = (ServerItemAttribute)node.ReadByte();
                            ushort datalen = node.ReadUInt16();

                            switch (attribute)
                            {
                                case ServerItemAttribute.ServerID:
                                    item.ID = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.ClientID:
                                    item.ClientId = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.GroundSpeed:
                                    item.GroundSpeed = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.Name:
                                    byte[] buffer = node.ReadBytes(datalen);
                                    item.Name = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                                    break;

                                case ServerItemAttribute.SpriteHash:
                                    item.SpriteHash = node.ReadBytes(datalen);
                                    break;

                                case ServerItemAttribute.MinimaColor:
                                    item.MinimapColor = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.MaxReadWriteChars:
                                    item.MaxReadWriteChars = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.MaxReadChars:
                                    item.MaxReadChars = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.Light:
                                    item.LightLevel = node.ReadUInt16();
                                    item.LightColor = node.ReadUInt16();
                                    break;

                                case ServerItemAttribute.StackOrder:
                                    item.StackOrder = (TileStackOrder)node.ReadByte();
                                    break;

                                case ServerItemAttribute.TradeAs:
                                    item.TradeAs = node.ReadUInt16();
                                    break;

                                default:
                                    node.BaseStream.Seek(datalen, SeekOrigin.Current);
                                    break;
                            }
                        }

                        if (item.SpriteHash == null && item.Type != ServerItemType.Deprecated)
                        {
                            item.SpriteHash = new byte[16];
                        }

                        this.Items.Add(item);
                        node = reader.GetNextNode();
                    }
                    while (node != null);
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
