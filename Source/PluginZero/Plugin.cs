﻿#region Licence
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
using OTLib.Server.Items;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
#endregion

namespace PluginZero
{

    internal enum ItemFlag : byte
    {
        Ground = 0x00,
        GroundBorder = 0x01,
        OnBottom = 0x02,
        OnTop = 0x03,
        Container = 0x04,
        Stackable = 0x05,
        ForceUse = 0x06,
        MultiUse = 0x07,
        Writable = 0x08,
        WritableOnce = 0x09,
        FluidContainer = 0x0A,
        Fluid = 0x0B,
        IsUnpassable = 0x0C,
        IsUnmoveable = 0x0D,
        BlockMissiles = 0x0E,
        BlockPathfinder = 0x0F,
        Pickupable = 0x10,
        Hangable = 0x11,
        IsHorizontal = 0x12,
        IsVertical = 0x13,
        Rotatable = 0x14,
        HasLight = 0x15,
        DontHide = 0x16,
        FloorChange = 0x17,
        HasOffset = 0x18,
        HasElevation = 0x19,
        Lying = 0x1A,
        AnimateAlways = 0x1B,
        Minimap = 0x1C,
        LensHelp = 0x1D,
        FullGround = 0x1E,

        LastFlag = 0xFF
    }

    public class Plugin : IPlugin
    {
        #region Private Properties

        private Dictionary<uint, Sprite> sprites;
        private ushort itemCount;

        #endregion

        #region Constructor

        public Plugin()
        {
            this.Settings = new Settings();
            this.sprites = new Dictionary<uint, Sprite>();
            this.Items = new ClientItems();
            this.SupportedClients = new List<SupportedClient>();
        }

        #endregion

        #region Public Properties

        // internal implementation
        public Settings Settings { get; private set; }

        // IPlugin implementation
        public IPluginHost Host { get; set; }

        public List<SupportedClient> SupportedClients { get; private set; }

        public ClientItems Items { get; set; }

        public ushort MinItemId { get { return 100; } }

        public ushort MaxItemId { get { return this.itemCount; } }

        public bool Loaded { get; private set; }

        #endregion

        #region Public Methods

        public bool LoadClient(SupportedClient client, bool extended, bool frameDurations, bool transparency, string datFullPath, string sprFullPath)
        {
            if (this.Loaded)
            {
                this.Dispose();
            }

            if (!LoadDat(datFullPath, client, extended, frameDurations))
            {
                Trace.WriteLine("Failed to load dat.");
                return false;
            }

            if (!LoadSprites(sprFullPath, client, extended, transparency))
            {
                Trace.WriteLine("Failed to load spr.");
                return false;
            }

            this.Loaded = true;
            return true;
        }

        public void Initialize()
        {
            this.Settings.Load("PluginZero.xml");
            this.SupportedClients = Settings.GetSupportedClientList();
        }

        public SupportedClient GetClientBySignatures(uint datSignature, uint sprSignature)
        {
            foreach (SupportedClient client in this.SupportedClients)
            {
                if (client.DatSignature == datSignature && client.SprSignature == sprSignature)
                {
                    return client;
                }
            }

            return null;
        }

        public ClientItem GetClientItem(ushort id)
        {
            if (this.Loaded && id >= 100 && id <= this.itemCount)
            {
                return this.Items[id];
            }

            return null;
        }

        public void Dispose()
        {
            if (this.Loaded)
            {
                this.sprites.Clear();
                this.Items.Clear();
                this.itemCount = 0;
                this.Loaded = false;
            }
        }

        public bool LoadSprites(string filename, SupportedClient client, bool extended, bool transparency)
        {
            return Sprite.LoadSprites(filename, ref sprites, client, extended, transparency);
        }

        public bool LoadDat(string filename, SupportedClient client, bool extended, bool frameDurations)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(fileStream);
                uint datSignature = reader.ReadUInt32();
                if (client.DatSignature != datSignature)
                {
                    string message = "PluginZero: Bad dat signature. Expected signature is {0:X} and loaded signature is {1:X}.";
                    Trace.WriteLine(String.Format(message, client.DatSignature, datSignature));
                    return false;
                }

                // get max id
                this.itemCount = reader.ReadUInt16();
                reader.ReadUInt16(); // skipping outfits count
                reader.ReadUInt16(); // skipping effects count
                reader.ReadUInt16(); // skipping missiles count

                ushort id = 100;
                while (id <= this.itemCount)
                {
                    ClientItem item = new ClientItem();
                    item.ID = id;
                    this.Items[id] = item;

                    // read the options until we find 0xff
                    ItemFlag flag;
                    do
                    {
                        flag = (ItemFlag)reader.ReadByte();

                        switch (flag)
                        {
                            case ItemFlag.Ground:
                                item.GroundSpeed = reader.ReadUInt16();
                                item.Type = ServerItemType.Ground;
                                break;


                            case ItemFlag.GroundBorder:
                                item.HasStackOrder = true;
                                item.StackOrder = TileStackOrder.Border;
                                break;

                            case ItemFlag.OnBottom:
                                item.HasStackOrder = true;
                                item.StackOrder = TileStackOrder.Bottom;
                                break;


                            case ItemFlag.OnTop:
                                item.HasStackOrder = true;
                                item.StackOrder = TileStackOrder.Top;
                                break;

                            case ItemFlag.Container:
                                item.Type = ServerItemType.Container;
                                break;

                            case ItemFlag.Stackable:
                                item.Stackable = true;
                                break;

                            case ItemFlag.ForceUse:
                                break;

                            case ItemFlag.MultiUse:
                                item.MultiUse = true;
                                break;

                            case ItemFlag.Writable:
                                item.Readable = true;
                                item.MaxReadWriteChars = reader.ReadUInt16();
                                break;

                            case ItemFlag.WritableOnce:
                                item.Readable = true;
                                item.MaxReadChars = reader.ReadUInt16();
                                break;

                            case ItemFlag.FluidContainer:
                                item.Type = ServerItemType.Fluid;
                                break;

                            case ItemFlag.Fluid:
                                item.Type = ServerItemType.Splash;
                                break;

                            case ItemFlag.IsUnpassable:
                                item.Unpassable = true;
                                break;

                            case ItemFlag.IsUnmoveable:
                                item.Movable = false;
                                break;

                            case ItemFlag.BlockMissiles:
                                item.BlockMissiles = true;
                                break;

                            case ItemFlag.BlockPathfinder:
                                item.BlockPathfinder = true;
                                break;

                            case ItemFlag.Pickupable:
                                item.Pickupable = true;
                                break;

                            case ItemFlag.Hangable:
                                item.Hangable = true;
                                break;

                            case ItemFlag.IsHorizontal:
                                item.HookEast = true;
                                break;

                            case ItemFlag.IsVertical:
                                item.HookSouth = true;
                                break;

                            case ItemFlag.Rotatable:
                                item.Rotatable = true;
                                break;

                            case ItemFlag.HasLight:
                                item.LightLevel = reader.ReadUInt16();
                                item.LightColor = reader.ReadUInt16();
                                break;

                            case ItemFlag.DontHide:
                                break;

                            case ItemFlag.FloorChange:
                                break;

                            case ItemFlag.HasOffset:
                                reader.ReadUInt16(); // OffsetX
                                reader.ReadUInt16(); // OffsetY
                                break;

                            case ItemFlag.HasElevation:
                                item.HasElevation = true;
                                reader.ReadUInt16(); // Height
                                break;

                            case ItemFlag.Lying:
                                break;

                            case ItemFlag.AnimateAlways:
                                break;

                            case ItemFlag.Minimap:
                                item.MinimapColor = reader.ReadUInt16();
                                break;

                            case ItemFlag.LensHelp:
                                ushort opt = reader.ReadUInt16();
                                if (opt == 1112)
                                {
                                    item.Readable = true;
                                }
                                break;

                            case ItemFlag.FullGround:
                                break;

                            case ItemFlag.LastFlag:
                                break;

                            default:
                                Trace.WriteLine(String.Format("PluginZero: Error while parsing, unknown flag 0x{0:X} at id {1}.", flag, id));
                                return false;

                        }

                    } while (flag != ItemFlag.LastFlag);


                    item.Width = reader.ReadByte();
                    item.Height = reader.ReadByte();

                    if ((item.Width > 1) || (item.Height > 1))
                    {
                        reader.BaseStream.Position++;
                    }

                    item.Layers = reader.ReadByte();
                    item.PatternX = reader.ReadByte();
                    item.PatternY = reader.ReadByte();
                    item.PatternZ = reader.ReadByte();
                    item.Frames = reader.ReadByte();
                    item.IsAnimation = item.Frames > 1;
                    item.NumSprites = (uint)item.Width * item.Height * item.Layers * item.PatternX * item.PatternY * item.PatternZ * item.Frames;

                    // Read the sprite ids
                    for (UInt32 i = 0; i < item.NumSprites; ++i)
                    {
                        UInt16 spriteId = reader.ReadUInt16();
                        Sprite sprite;
                        if (!sprites.TryGetValue(spriteId, out sprite))
                        {
                            sprite = new Sprite();
                            sprite.ID = spriteId;
                            sprites[spriteId] = sprite;
                        }

                        item.SpriteList.Add(sprite);
                    }

                    ++id;


                }
            }

            return true;
        }
        #endregion
    }
}