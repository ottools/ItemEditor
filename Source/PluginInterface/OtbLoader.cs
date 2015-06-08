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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
#endregion

namespace ItemEditor
{
    class OtbLoader : BinaryReader
    {
        enum SpecialChars
        {
            NODE_START = 0xFE,
            NODE_END = 0xFF,
            ESCAPE_CHAR = 0xFD,
        };

        public OtbLoader(Stream input)
            : base(input)
        {
            //
        }

        public BinaryReader getRootNode()
        {
            return getChildNode();
        }

        public BinaryReader getChildNode()
        {
            advance();
            return getNodeData();
        }

        public BinaryReader getNextNode()
        {
            BaseStream.Seek(currentNodePos, SeekOrigin.Begin);

            byte value = ReadByte();
            if ((SpecialChars)value != SpecialChars.NODE_START)
            {
                return null;
            }

            value = ReadByte();

            Int32 level = 1;
            while (true)
            {
                value = ReadByte();
                if ((SpecialChars)value == SpecialChars.NODE_END)
                {
                    --level;
                    if (level == 0)
                    {
                        value = ReadByte();
                        if ((SpecialChars)value == SpecialChars.NODE_END)
                        {
                            return null;
                        }
                        else if ((SpecialChars)value != SpecialChars.NODE_START)
                        {
                            return null;
                        }
                        else
                        {
                            currentNodePos = BaseStream.Position - 1;
                            return getNodeData();
                        }
                    }
                }
                else if ((SpecialChars)value == SpecialChars.NODE_START)
                {
                    ++level;
                }
                else if ((SpecialChars)value == SpecialChars.ESCAPE_CHAR)
                {
                    ReadByte();
                }
            }
        }

        private BinaryReader getNodeData()
        {
            BaseStream.Seek(currentNodePos, SeekOrigin.Begin);

            //read node type
            byte value = ReadByte();

            if ((SpecialChars)value != SpecialChars.NODE_START)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream(200);

            currentNodeSize = 0;
            while (true)
            {
                value = ReadByte();
                if ((SpecialChars)value == SpecialChars.NODE_END || (SpecialChars)value == SpecialChars.NODE_START)
                    break;
                else if ((SpecialChars)value == SpecialChars.ESCAPE_CHAR)
                {
                    value = ReadByte();
                }
                ++currentNodeSize;
                ms.WriteByte(value);
            }

            BaseStream.Seek(currentNodePos, SeekOrigin.Begin);
            ms.Position = 0;
            return new BinaryReader(ms);
        }

        private bool advance()
        {
            try
            {
                Int64 seekPos = 0;
                if (currentNodePos == 0)
                {
                    seekPos = 4;
                }
                else
                {
                    seekPos = currentNodePos;
                }

                BaseStream.Seek(seekPos, SeekOrigin.Begin);

                byte value = ReadByte();
                if ((SpecialChars)value != SpecialChars.NODE_START)
                {
                    return false;
                }

                if (currentNodePos == 0)
                {
                    currentNodePos = BaseStream.Position - 1;
                    return true;
                }
                else
                {
                    value = ReadByte();

                    while (true)
                    {
                        value = ReadByte();
                        if ((SpecialChars)value == SpecialChars.NODE_END)
                        {
                            return false;
                        }
                        else if ((SpecialChars)value == SpecialChars.NODE_START)
                        {
                            currentNodePos = BaseStream.Position - 1;
                            return true;
                        }
                        else if ((SpecialChars)value == SpecialChars.ESCAPE_CHAR)
                        {
                            ReadByte();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CreateNode(byte type)
        {
            WriteByte((byte)SpecialChars.NODE_START, false);
            writeByte(type);
        }

        public void CloseNode()
        {
            WriteByte((byte)SpecialChars.NODE_END, false);
        }

        public void writeByte(byte value)
        {
            byte[] bytes = new byte[1] { value };
            WriteBytes(bytes, true);
        }

        public void WriteByte(byte value, bool unescape)
        {
            byte[] bytes = new byte[1] { value };
            WriteBytes(bytes, unescape);
        }

        public void WriteUInt16(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteBytes(bytes, true);
        }

        public void WriteUInt16(ushort value, bool unescape)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteBytes(bytes, unescape);
        }

        public void WriteUInt32(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteBytes(bytes, true);
        }

        public void WriteUInt32(uint value, bool unescape)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteBytes(bytes, unescape);
        }

        public void WriteProp(Otb.ItemAttribute attr, BinaryWriter writer)
        {
            writer.BaseStream.Position = 0;
            byte[] bytes = new byte[writer.BaseStream.Length];
            writer.BaseStream.Read(bytes, 0, (int)writer.BaseStream.Length);
            writer.BaseStream.Position = 0;
            writer.BaseStream.SetLength(0);

            WriteProp((byte)attr, bytes);
        }

        public void WriteProp(Otb.RootAttribute attr, BinaryWriter writer)
        {
            writer.BaseStream.Position = 0;
            byte[] bytes = new byte[writer.BaseStream.Length];
            writer.BaseStream.Read(bytes, 0, (int)writer.BaseStream.Length);
            writer.BaseStream.Position = 0;
            writer.BaseStream.SetLength(0);

            WriteProp((byte)attr, bytes);
        }

        private void WriteProp(byte attr, byte[] bytes)
        {
            writeByte((byte)attr);
            WriteUInt16((ushort)bytes.Length);
            WriteBytes(bytes, true);
        }

        public void WriteBytes(byte[] bytes, bool unescape)
        {
            foreach (byte b in bytes)
            {
                if (unescape && (b == (byte)SpecialChars.NODE_START || b == (byte)SpecialChars.NODE_END || b == (byte)SpecialChars.ESCAPE_CHAR))
                {
                    BaseStream.WriteByte((byte)SpecialChars.ESCAPE_CHAR);
                }

                BaseStream.WriteByte(b);
            }
        }

        public Int64 currentNodePos = 0;
        public uint currentNodeSize = 0;
    };

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

    public class Otb
    {
        public enum ItemGroup
        {
            NONE = 0,
            GROUND,
            CONTAINER,
            WEAPON,
            AMMUNITION,
            ARMOR,
            CHARGES,
            TELEPORT,
            MAGIC_FIELD,
            WRITEABLE,
            KEY,
            SPLASH,
            FLUID,
            DOOR,
            DEPRECATED,
        };

        public enum ItemAttribute : byte
        {
            SERVER_ID = 0x10,
            CLIENT_ID = 0x11,
            NAME = 0x12,
            GROUND_SPEED = 0x14,
            SPRITE_HASH = 0x20,
            MINIMAP_COLOR = 0x21,
            MAX_READ_WRITE_CHARS = 0x22,
            MAX_READ_CHARS = 0x23,
            LIGHT = 0x2A,
            TOP_ORDER = 0x2B,
            TRADE_AS = 0x2D
        };

        public enum RootAttribute
        {
            ROOT_ATTR_VERSION = 0x01
        };

        public class VersionInfo
        {
            #region Public Properties

            public uint MajorVersion { get; set; }

            public uint MinorVersion { get; set; }

            public uint BuildNumber { get; set; }

            public string CSDVersion { get; set; }

            #endregion
        };

        [FlagsAttribute]
        public enum ItemFlag
        {
            BLOCK_SOLID = 1,
            BLOCK_MISSILE = 2,
            BLOCK_PATHFINDER = 4,
            HAS_ELEVATION = 8,
            USEABLE = 16,
            PICKUPABLE = 32,
            MOVEABLE = 64,
            STACKABLE = 128,
            FLOOR_CHANGE_DOWN = 256,
            FLOOR_CHANGE_NORTH = 512,
            FLOOR_CHANGE_EAST = 1024,
            FLOOR_CHANGE_SOUTH = 2048,
            FLOOR_CHANGE_WEST = 4096,
            ALWAYS_ON_TOP = 8192,
            READABLE = 16384,
            ROTABLE = 32768,
            HANGABLE = 65536,
            VERTICAL_WALL = 131072,
            HORIZONTAL_WALL = 262144,
            ALLOW_DISTANCE_READ = 1048576,
            IGNORE_LOOK = 8388608,
            ANIMATION = 16777216,
            FULL_GROUND = 33554432
        };

        public static bool Open(string filename, ref ServerItemList items)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            try
            {
                using (OtbLoader reader = new OtbLoader(fileStream))
                {
                    //get root node
                    BinaryReader nodeReader = reader.getRootNode();
                    if (nodeReader == null)
                    {
                        return false;
                    }

                    nodeReader.ReadByte(); //first byte of otb is 0
                    nodeReader.ReadUInt32(); //4 bytes flags, unused

                    byte attr = nodeReader.ReadByte();
                    if ((RootAttribute)attr == RootAttribute.ROOT_ATTR_VERSION)
                    {
                        ushort datalen = nodeReader.ReadUInt16();
                        if (datalen != 140) // 4 + 4 + 4 + 1 * 128
                        {
                            Trace.WriteLine(String.Format("Size of version header is invalid, updated .otb version?"));
                            return false;
                        }

                        items.MajorVersion = nodeReader.ReadUInt32(); //major, file version
                        items.MinorVersion = nodeReader.ReadUInt32(); //minor, client version
                        items.BuildNumber = nodeReader.ReadUInt32();  //build number, revision
                        nodeReader.BaseStream.Seek(128, SeekOrigin.Current);
                    }

                    nodeReader = reader.getChildNode();
                    if (nodeReader == null)
                    {
                        return false;
                    }

                    do
                    {
                        ServerItem item = new ServerItem();

                        byte itemGroup = nodeReader.ReadByte();

                        switch ((ItemGroup)itemGroup)
                        {
                            case ItemGroup.NONE: item.type = ItemType.None; break;
                            case ItemGroup.GROUND: item.type = ItemType.Ground; break;
                            case ItemGroup.SPLASH: item.type = ItemType.Splash; break;
                            case ItemGroup.FLUID: item.type = ItemType.Fluid; break;
                            case ItemGroup.CONTAINER: item.type = ItemType.Container; break;
                            case ItemGroup.DEPRECATED: item.type = ItemType.Deprecated; break;
                            default: break;
                        }

                        ItemFlag flags = (ItemFlag)nodeReader.ReadUInt32();

                        item.isUnpassable = ((flags & ItemFlag.BLOCK_SOLID) == ItemFlag.BLOCK_SOLID);
                        item.blockMissiles = ((flags & ItemFlag.BLOCK_MISSILE) == ItemFlag.BLOCK_MISSILE);
                        item.blockPathfinder = ((flags & ItemFlag.BLOCK_PATHFINDER) == ItemFlag.BLOCK_PATHFINDER);
                        item.isPickupable = ((flags & ItemFlag.PICKUPABLE) == ItemFlag.PICKUPABLE);
                        item.isMoveable = ((flags & ItemFlag.MOVEABLE) == ItemFlag.MOVEABLE);
                        item.isStackable = ((flags & ItemFlag.STACKABLE) == ItemFlag.STACKABLE);
                        item.alwaysOnTop = ((flags & ItemFlag.ALWAYS_ON_TOP) == ItemFlag.ALWAYS_ON_TOP);
                        item.isVertical = ((flags & ItemFlag.VERTICAL_WALL) == ItemFlag.VERTICAL_WALL);
                        item.isHorizontal = ((flags & ItemFlag.HORIZONTAL_WALL) == ItemFlag.HORIZONTAL_WALL);
                        item.isHangable = ((flags & ItemFlag.HANGABLE) == ItemFlag.HANGABLE);
                        item.isRotatable = ((flags & ItemFlag.ROTABLE) == ItemFlag.ROTABLE);
                        item.isReadable = ((flags & ItemFlag.READABLE) == ItemFlag.READABLE);
                        item.multiUse = ((flags & ItemFlag.USEABLE) == ItemFlag.USEABLE);
                        item.hasElevation = ((flags & ItemFlag.HAS_ELEVATION) == ItemFlag.HAS_ELEVATION);
                        item.ignoreLook = ((flags & ItemFlag.IGNORE_LOOK) == ItemFlag.IGNORE_LOOK);
                        item.allowDistRead = ((flags & ItemFlag.ALLOW_DISTANCE_READ) == ItemFlag.ALLOW_DISTANCE_READ);
                        item.isAnimation = ((flags & ItemFlag.ANIMATION) == ItemFlag.ANIMATION);
                        item.fullGround = ((flags & ItemFlag.FULL_GROUND) == ItemFlag.FULL_GROUND);

                        while (nodeReader.PeekChar() != -1)
                        {
                            ItemAttribute attribute = (ItemAttribute)nodeReader.ReadByte();
                            ushort datalen = nodeReader.ReadUInt16();

                            switch (attribute)
                            {
                                case ItemAttribute.SERVER_ID:
                                    item.id = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.CLIENT_ID:
                                    item.ClientId = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.GROUND_SPEED:
                                    item.groundSpeed = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.NAME:
                                    item.name = new string(nodeReader.ReadChars(datalen));
                                    break;

                                case ItemAttribute.SPRITE_HASH:
                                    item.SpriteHash = nodeReader.ReadBytes(datalen);
                                    break;

                                case ItemAttribute.MINIMAP_COLOR:
                                    item.minimapColor = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.MAX_READ_WRITE_CHARS:
                                    item.maxReadWriteChars = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.MAX_READ_CHARS:
                                    item.maxReadChars = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.LIGHT:
                                    item.lightLevel = nodeReader.ReadUInt16();
                                    item.lightColor = nodeReader.ReadUInt16();
                                    break;

                                case ItemAttribute.TOP_ORDER:
                                    item.alwaysOnTopOrder = nodeReader.ReadByte();
                                    break;

                                case ItemAttribute.TRADE_AS:
                                    item.tradeAs = nodeReader.ReadUInt16();
                                    break;

                                default:
                                    nodeReader.BaseStream.Seek(datalen, SeekOrigin.Current);
                                    break;
                            }
                        }

                        if (item.SpriteHash == null && item.type != ItemType.Deprecated)
                        {
                            item.SpriteHash = new byte[16];
                        }

                        items.Add(item);
                        nodeReader = reader.getNextNode();

                    } while (nodeReader != null);
                }
            }
            finally
            {
                fileStream.Close();
            }

            return true;
        }

        public static bool Save(string filename, ref ServerItemList items)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Create);
            try
            {
                using (OtbLoader writer = new OtbLoader(fileStream))
                {
                    writer.WriteUInt32(0, false); //version, always 0

                    writer.CreateNode(0); //root node
                    writer.WriteUInt32(0, true); //flags, unused for root node

                    VersionInfo vi = new VersionInfo();

                    vi.MajorVersion = items.MajorVersion;
                    vi.MinorVersion = items.MinorVersion;
                    vi.BuildNumber = items.BuildNumber;
                    vi.CSDVersion = String.Format("OTB {0}.{1}.{2}-{3}.{4}", vi.MajorVersion, vi.MinorVersion, vi.BuildNumber, items.ClientVersion / 100, items.ClientVersion % 100);

                    MemoryStream ms = new MemoryStream();
                    BinaryWriter property = new BinaryWriter(ms);
                    property.Write(vi.MajorVersion);
                    property.Write(vi.MinorVersion);
                    property.Write(vi.BuildNumber);
                    byte[] CSDVersion = System.Text.Encoding.ASCII.GetBytes(vi.CSDVersion);
                    Array.Resize(ref CSDVersion, 128);
                    property.Write(CSDVersion);

                    writer.WriteProp(RootAttribute.ROOT_ATTR_VERSION, property);

                    foreach (ServerItem item in items)
                    {
                        List<ItemAttribute> saveAttributeList = new List<ItemAttribute>();
                        saveAttributeList.Add(ItemAttribute.SERVER_ID);

                        if (item.type == ItemType.Deprecated)
                        {
                            //no other attributes should be saved for this type
                        }
                        else
                        {
                            saveAttributeList.Add(ItemAttribute.CLIENT_ID);
                            saveAttributeList.Add(ItemAttribute.SPRITE_HASH);

                            if (item.minimapColor != 0)
                            {
                                saveAttributeList.Add(ItemAttribute.MINIMAP_COLOR);
                            }

                            if (item.maxReadWriteChars != 0)
                            {
                                saveAttributeList.Add(ItemAttribute.MAX_READ_WRITE_CHARS);
                            }

                            if (item.maxReadChars != 0)
                            {
                                saveAttributeList.Add(ItemAttribute.MAX_READ_CHARS);
                            }

                            if (item.lightLevel != 0 || item.lightColor != 0)
                            {
                                saveAttributeList.Add(ItemAttribute.LIGHT);
                            }

                            if (item.type == ItemType.Ground)
                            {
                                saveAttributeList.Add(ItemAttribute.GROUND_SPEED);
                            }

                            if (item.alwaysOnTop)
                            {
                                saveAttributeList.Add(ItemAttribute.TOP_ORDER);
                            }

                            if (item.tradeAs != 0)
                            {
                                saveAttributeList.Add(ItemAttribute.TRADE_AS);
                            }

                            if (!string.IsNullOrEmpty(item.name))
                            {
                                saveAttributeList.Add(ItemAttribute.NAME);
                            }
                        }

                        switch (item.type)
                        {
                            case ItemType.Container: writer.CreateNode((byte)ItemGroup.CONTAINER); break;
                            case ItemType.Fluid: writer.CreateNode((byte)ItemGroup.FLUID); break;
                            case ItemType.Ground: writer.CreateNode((byte)ItemGroup.GROUND); break;
                            case ItemType.Splash: writer.CreateNode((byte)ItemGroup.SPLASH); break;
                            case ItemType.Deprecated: writer.CreateNode((byte)ItemGroup.DEPRECATED); break;
                            default: writer.CreateNode((byte)ItemGroup.NONE); break;
                        }

                        uint flags = 0;
                        if (item.isUnpassable) { flags |= (uint)ItemFlag.BLOCK_SOLID; }
                        if (item.blockMissiles) { flags |= (uint)ItemFlag.BLOCK_MISSILE; }
                        if (item.blockPathfinder) { flags |= (uint)ItemFlag.BLOCK_PATHFINDER; }
                        if (item.hasElevation) { flags |= (uint)ItemFlag.HAS_ELEVATION; }
                        if (item.multiUse) { flags |= (uint)ItemFlag.USEABLE; }
                        if (item.isPickupable) { flags |= (uint)ItemFlag.PICKUPABLE; }
                        if (item.isMoveable) { flags |= (uint)ItemFlag.MOVEABLE; }
                        if (item.isStackable) { flags |= (uint)ItemFlag.STACKABLE; }
                        if (item.alwaysOnTop) { flags |= (uint)ItemFlag.ALWAYS_ON_TOP; }
                        if (item.isReadable) { flags |= (uint)ItemFlag.READABLE; }
                        if (item.isRotatable) { flags |= (uint)ItemFlag.ROTABLE; }
                        if (item.isHangable) { flags |= (uint)ItemFlag.HANGABLE; }
                        if (item.isVertical) { flags |= (uint)ItemFlag.VERTICAL_WALL; }
                        if (item.isHorizontal) { flags |= (uint)ItemFlag.HORIZONTAL_WALL; }
                        if (item.ignoreLook) { flags |= (uint)ItemFlag.IGNORE_LOOK; }
                        if (item.allowDistRead) { flags |= (uint)ItemFlag.ALLOW_DISTANCE_READ; }
                        if (item.isAnimation) { flags |= (uint)ItemFlag.ANIMATION; }
                        if (item.fullGround) { flags |= (uint)ItemFlag.FULL_GROUND; }

                        writer.WriteUInt32(flags, true);

                        foreach (ItemAttribute attribute in saveAttributeList)
                        {
                            switch (attribute)
                            {
                                case ItemAttribute.SERVER_ID:
                                    property.Write((ushort)item.id);
                                    writer.WriteProp(ItemAttribute.SERVER_ID, property);

                                    break;

                                case ItemAttribute.TRADE_AS:
                                    property.Write((ushort)item.tradeAs);
                                    writer.WriteProp(ItemAttribute.TRADE_AS, property);
                                    break;

                                case ItemAttribute.CLIENT_ID:
                                    property.Write((ushort)item.ClientId);
                                    writer.WriteProp(ItemAttribute.CLIENT_ID, property);
                                    break;

                                case ItemAttribute.GROUND_SPEED:
                                    property.Write((ushort)item.groundSpeed);
                                    writer.WriteProp(ItemAttribute.GROUND_SPEED, property);
                                    break;

                                case ItemAttribute.NAME:
                                    for (ushort i = 0; i < item.name.Length; ++i)
                                    {
                                        property.Write((char)item.name[i]);
                                    }
                                    writer.WriteProp(ItemAttribute.NAME, property);
                                    break;

                                case ItemAttribute.SPRITE_HASH:
                                    property.Write(item.SpriteHash);
                                    writer.WriteProp(ItemAttribute.SPRITE_HASH, property);
                                    break;

                                case ItemAttribute.MINIMAP_COLOR:
                                    property.Write((ushort)item.minimapColor);
                                    writer.WriteProp(ItemAttribute.MINIMAP_COLOR, property);
                                    break;

                                case ItemAttribute.MAX_READ_WRITE_CHARS:
                                    property.Write((ushort)item.maxReadWriteChars);
                                    writer.WriteProp(ItemAttribute.MAX_READ_WRITE_CHARS, property);
                                    break;

                                case ItemAttribute.MAX_READ_CHARS:
                                    property.Write((ushort)item.maxReadChars);
                                    writer.WriteProp(ItemAttribute.MAX_READ_CHARS, property);
                                    break;

                                case ItemAttribute.LIGHT:
                                    property.Write((ushort)item.lightLevel);
                                    property.Write((ushort)item.lightColor);
                                    writer.WriteProp(ItemAttribute.LIGHT, property);
                                    break;

                                case ItemAttribute.TOP_ORDER:
                                    property.Write((byte)item.alwaysOnTopOrder);
                                    writer.WriteProp(ItemAttribute.TOP_ORDER, property);
                                    break;
                            }
                        }

                        writer.CloseNode();
                    }

                    writer.CloseNode();
                }
            }
            finally
            {
                fileStream.Close();
            }

            return true;
        }
    }
}
