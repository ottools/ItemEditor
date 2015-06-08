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
using OTLib.Server.Items;
using System;
using System.Collections.Generic;
using System.IO;
#endregion

namespace ItemEditor
{
    public class ItemImpl : ICloneable
    {
        public object Clone()
        {
            ItemImpl clone = (ItemImpl)this.MemberwiseClone();
            return clone;
        }

        public ushort id;
        public ServerItemType type;
        public bool HasStackOrder;
        public TileStackOrder StackOrder;
        public ushort groundSpeed;
        public bool multiUse;
        public ushort maxReadChars;
        public ushort maxReadWriteChars;
        public bool hasElevation;
        public ushort minimapColor;
        public bool ignoreLook;
        public ushort lightLevel;
        public ushort lightColor;
        public bool isStackable;
        public bool isReadable;
        public bool isMoveable;
        public bool isPickupable;
        public bool isHangable;
        public bool isHorizontal;
        public bool isVertical;
        public bool isRotatable;
        public bool isUnpassable;
        public bool blockMissiles;
        public bool blockPathfinder;
        public bool allowDistanceRead;
        public bool isAnimation;
        public bool fullGround;
        public ushort tradeAs;
        public string name;
    }

    public class Item : IServerItemType
    {
        public ItemImpl itemImpl = new ItemImpl();

        public Item()
        {
            this.Name = "";
            this.Movable = true;
        }

        public virtual bool IsEqual(Item item)
        {
            if (Type != item.Type) { return false; }

            if (Name.CompareTo(item.Name) != 0) { return false; }
            if (TradeAs != item.TradeAs) { return false; }
            if (FullGround != item.FullGround) { return false; }
            if (IsAnimation != item.IsAnimation) { return false; }
            if (StackOrder != item.StackOrder) { return false; }
            if (Unpassable != item.Unpassable) { return false; }
            if (BlockPathfinder != item.BlockPathfinder) { return false; }
            if (BlockMissiles != item.BlockMissiles) { return false; }
            if (GroundSpeed != item.GroundSpeed) { return false; }
            if (HasElevation != item.HasElevation) { return false; }
            if (MultiUse != item.MultiUse) { return false; }
            if (Hangable != item.Hangable) { return false; }
            if (HookEast != item.HookEast) { return false; }
            if (HookSouth != item.HookSouth) { return false; }
            if (Movable != item.Movable) { return false; }
            if (Pickupable != item.Pickupable) { return false; }
            if (Readable != item.Readable) { return false; }
            if (Rotatable != item.Rotatable) { return false; }
            if (Stackable != item.Stackable) { return false; }
            if (LightColor != item.LightColor) { return false; }
            if (LightLevel != item.LightLevel) { return false; }
            if (IgnoreLook != item.IgnoreLook) { return false; }
            if (MaxReadChars != item.MaxReadChars) { return false; }
            if (MaxReadWriteChars != item.MaxReadWriteChars) { return false; }
            if (MinimapColor != item.MinimapColor) { return false; }
            return true;
        }

        public ushort ID { get { return itemImpl.id; } set { itemImpl.id = value; } }
        public ServerItemType Type { get { return itemImpl.type; } set { itemImpl.type = value; } }
        public bool HasStackOrder { get { return itemImpl.HasStackOrder; } set { itemImpl.HasStackOrder = value; } }
        public TileStackOrder StackOrder { get { return itemImpl.StackOrder; } set { itemImpl.StackOrder = value; } }
        public bool Unpassable { get { return itemImpl.isUnpassable; } set { itemImpl.isUnpassable = value; } }
        public bool BlockMissiles { get { return itemImpl.blockMissiles; } set { itemImpl.blockMissiles = value; } }
        public bool BlockPathfinder { get { return itemImpl.blockPathfinder; } set { itemImpl.blockPathfinder = value; } }
        public bool HasElevation { get { return itemImpl.hasElevation; } set { itemImpl.hasElevation = value; } }
        public bool MultiUse { get { return itemImpl.multiUse; } set { itemImpl.multiUse = value; } }
        public bool Pickupable { get { return itemImpl.isPickupable; } set { itemImpl.isPickupable = value; } }
        public bool Movable { get { return itemImpl.isMoveable; } set { itemImpl.isMoveable = value; } }
        public bool Stackable { get { return itemImpl.isStackable; } set { itemImpl.isStackable = value; } }
        public bool Readable { get { return itemImpl.isReadable; } set { itemImpl.isReadable = value; } }
        public bool Rotatable { get { return itemImpl.isRotatable; } set { itemImpl.isRotatable = value; } }
        public bool Hangable { get { return itemImpl.isHangable; } set { itemImpl.isHangable = value; } }
        public bool HookSouth { get { return itemImpl.isVertical; } set { itemImpl.isVertical = value; } }
        public bool HookEast { get { return itemImpl.isHorizontal; } set { itemImpl.isHorizontal = value; } }
        public bool IgnoreLook { get { return itemImpl.ignoreLook; } set { itemImpl.ignoreLook = value; } }
        public bool FullGround { get { return itemImpl.fullGround; } set { itemImpl.fullGround = value; } }
        public ushort GroundSpeed { get { return itemImpl.groundSpeed; } set { itemImpl.groundSpeed = value; } }
        public ushort LightLevel { get { return itemImpl.lightLevel; } set { itemImpl.lightLevel = value; } }
        public ushort LightColor { get { return itemImpl.lightColor; } set { itemImpl.lightColor = value; } }
        public ushort MaxReadChars { get { return itemImpl.maxReadChars; } set { itemImpl.maxReadChars = value; } }
        public ushort MaxReadWriteChars { get { return itemImpl.maxReadWriteChars; } set { itemImpl.maxReadWriteChars = value; } }
        public ushort MinimapColor { get { return itemImpl.minimapColor; } set { itemImpl.minimapColor = value; } }
        public ushort TradeAs { get { return itemImpl.tradeAs; } set { itemImpl.tradeAs = value; } }
        public string Name { get { return itemImpl.name; } set { itemImpl.name = value; } }
        public bool AllowDistanceRead { get { return itemImpl.allowDistanceRead; } set { itemImpl.allowDistanceRead = value; } }
        public bool IsAnimation { get { return itemImpl.isAnimation; } set { itemImpl.isAnimation = value; } }

        // used to find sprites during updates
        protected byte[] _spriteHash = null;
        public virtual byte[] SpriteHash
        {
            get { return _spriteHash; }
            set { _spriteHash = value; }
        }
    }

    public class ClientItem : Item
    {
        // sprite meta-data
        public byte width;
        public byte height;
        public byte layers;
        public byte patternX;
        public byte patternY;
        public byte patternZ;
        public byte frames;
        public uint numSprites;
        public List<Sprite> spriteList = new List<Sprite>();

        public override byte[] SpriteHash
        {
            get
            {
                if (_spriteHash == null)
                {
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    Int32 spriteBase = 0;
                    MemoryStream stream = new MemoryStream();

                    for (Int32 l = 0; l < layers; l++)
                    {
                        for (Int32 h = 0; h < height; h++)
                        {
                            for (Int32 w = 0; w < width; w++)
                            {
                                Int32 frameIndex = spriteBase + w + h * width + l * width * height;
                                Sprite sprite = spriteList[frameIndex];
                                if (sprite != null)
                                {
                                    stream.Write(sprite.GetRGBAData(), 0, 32 * 32 * 4);
                                }
                            }
                        }
                    }

                    stream.Position = 0;
                    _spriteHash = md5.ComputeHash(stream);
                }

                return _spriteHash;
            }

            set
            {
                _spriteHash = value;
            }
        }

        // contains sprite signatures using Euclidean distance (4x4 blocks) on a ff2d generated image of the sprite
        private double[,] _spriteSignature = null;
        public double[,] SpriteSignature
        {
            get { return _spriteSignature; }
            set { _spriteSignature = value; }
        }

        // used to calculate fourier transformation
        public byte[] GetRGBData()
        {
            return spriteList[0].GetRGBData();
        }

        public byte[] GetRGBData(int frameIndex)
        {
            return spriteList[frameIndex].GetRGBData();
        }

        // used for drawing and calculating MD5
        public byte[] GetRGBAData()
        {
            return spriteList[0].GetRGBAData();
        }

        public byte[] GetRGBAData(int frameIndex)
        {
            return spriteList[frameIndex].GetRGBAData();
        }
    }
}
