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

using System;
using System.Collections.Generic;
using System.IO;

namespace ItemEditor
{
    public enum ItemType
    {
        None,
        Ground,
        Container,
        Fluid,
        Splash,
        Deprecated
    };

    public class ItemImpl : ICloneable
    {
        public object Clone()
        {
            ItemImpl clone = (ItemImpl)this.MemberwiseClone();
            return clone;
        }

        public ushort id;
        public ushort groundSpeed;
        public ItemType type;
        public bool alwaysOnTop;
        public ushort alwaysOnTopOrder;
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
        public bool allowDistRead;
        public bool isAnimation;
        public bool fullGround;
        public ushort tradeAs;
        public string name;
    }

    public class Item
    {
        public ItemImpl itemImpl = new ItemImpl();

        public Item()
        {
            this.name = "";
            this.isMoveable = true;
        }

        public virtual bool IsEqual(Item item)
        {
            if (type != item.type) { return false; }

            if (name.CompareTo(item.name) != 0) { return false; }
            if (tradeAs != item.tradeAs) { return false; }
            if (fullGround != item.fullGround) { return false; }
            if (isAnimation != item.isAnimation) { return false; }
            if (alwaysOnTop != item.alwaysOnTop) { return false; }
            if (alwaysOnTopOrder != item.alwaysOnTopOrder) { return false; }
            if (isUnpassable != item.isUnpassable) { return false; }
            if (blockPathfinder != item.blockPathfinder) { return false; }
            if (blockMissiles != item.blockMissiles) { return false; }
            if (groundSpeed != item.groundSpeed) { return false; }
            if (hasElevation != item.hasElevation) { return false; }
            if (multiUse != item.multiUse) { return false; }
            if (isHangable != item.isHangable) { return false; }
            if (isHorizontal != item.isHorizontal) { return false; }
            if (isVertical != item.isVertical) { return false; }
            if (isMoveable != item.isMoveable) { return false; }
            if (isPickupable != item.isPickupable) { return false; }
            if (isReadable != item.isReadable) { return false; }
            if (isRotatable != item.isRotatable) { return false; }
            if (isStackable != item.isStackable) { return false; }
            if (lightColor != item.lightColor) { return false; }
            if (lightLevel != item.lightLevel) { return false; }
            if (ignoreLook != item.ignoreLook) { return false; }
            if (maxReadChars != item.maxReadChars) { return false; }
            if (maxReadWriteChars != item.maxReadWriteChars) { return false; }
            if (minimapColor != item.minimapColor) { return false; }
            return true;
        }

        public ushort id { get { return itemImpl.id; } set { itemImpl.id = value; } }
        public ushort groundSpeed { get { return itemImpl.groundSpeed; } set { itemImpl.groundSpeed = value; } }
        public ItemType type { get { return itemImpl.type; } set { itemImpl.type = value; } }
        public bool alwaysOnTop { get { return itemImpl.alwaysOnTop; } set { itemImpl.alwaysOnTop = value; } }
        public ushort alwaysOnTopOrder { get { return itemImpl.alwaysOnTopOrder; } set { itemImpl.alwaysOnTopOrder = value; } }
        public bool multiUse { get { return itemImpl.multiUse; } set { itemImpl.multiUse = value; } }
        public ushort maxReadChars { get { return itemImpl.maxReadChars; } set { itemImpl.maxReadChars = value; } }
        public ushort maxReadWriteChars { get { return itemImpl.maxReadWriteChars; } set { itemImpl.maxReadWriteChars = value; } }
        public bool hasElevation { get { return itemImpl.hasElevation; } set { itemImpl.hasElevation = value; } }
        public ushort minimapColor { get { return itemImpl.minimapColor; } set { itemImpl.minimapColor = value; } }
        public bool ignoreLook { get { return itemImpl.ignoreLook; } set { itemImpl.ignoreLook = value; } }
        public ushort lightLevel { get { return itemImpl.lightLevel; } set { itemImpl.lightLevel = value; } }
        public ushort lightColor { get { return itemImpl.lightColor; } set { itemImpl.lightColor = value; } }
        public bool isStackable { get { return itemImpl.isStackable; } set { itemImpl.isStackable = value; } }
        public bool isReadable { get { return itemImpl.isReadable; } set { itemImpl.isReadable = value; } }
        public bool isMoveable { get { return itemImpl.isMoveable; } set { itemImpl.isMoveable = value; } }
        public bool isPickupable { get { return itemImpl.isPickupable; } set { itemImpl.isPickupable = value; } }
        public bool isHangable { get { return itemImpl.isHangable; } set { itemImpl.isHangable = value; } }
        public bool isHorizontal { get { return itemImpl.isHorizontal; } set { itemImpl.isHorizontal = value; } }
        public bool isVertical { get { return itemImpl.isVertical; } set { itemImpl.isVertical = value; } }
        public bool isRotatable { get { return itemImpl.isRotatable; } set { itemImpl.isRotatable = value; } }
        public bool isUnpassable { get { return itemImpl.isUnpassable; } set { itemImpl.isUnpassable = value; } }
        public bool blockMissiles { get { return itemImpl.blockMissiles; } set { itemImpl.blockMissiles = value; } }
        public bool blockPathfinder { get { return itemImpl.blockPathfinder; } set { itemImpl.blockPathfinder = value; } }
        public bool allowDistRead { get { return itemImpl.allowDistRead; } set { itemImpl.allowDistRead = value; } }
        public bool isAnimation { get { return itemImpl.isAnimation; } set { itemImpl.isAnimation = value; } }
        public bool fullGround { get { return itemImpl.fullGround; } set { itemImpl.fullGround = value; } }
        public string name { get { return itemImpl.name; } set { itemImpl.name = value; } }
        public ushort tradeAs { get { return itemImpl.tradeAs; } set { itemImpl.tradeAs = value; } }

        //used to find sprites during updates
        protected byte[] _spriteHash = null;
        public virtual byte[] SpriteHash
        {
            get { return _spriteHash; }
            set { _spriteHash = value; }
        }
    }

    public class ClientItem : Item
    {
        //sprite meta-data
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
                    Int32 spriteSize = (Int32)width * (Int32)height * (Int32)frames;
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

        //contains sprite signatures using Euclidean distance (4x4 blocks) on a ff2d generated image of the sprite
        private double[,] _spriteSignature = null;
        public double[,] SpriteSignature
        {
            get { return _spriteSignature; }
            set { _spriteSignature = value; }
        }

        //Used to calculate fourier transformation
        public byte[] GetRGBData()
        {
            return spriteList[0].GetRGBData();
        }

        public byte[] GetRGBData(int frameIndex)
        {
            return spriteList[frameIndex].GetRGBData();
        }

        //used for drawing and calculating MD5
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
