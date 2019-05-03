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
using ImageSimilarity;
using OTLib.Server.Items;
using OTLib.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
#endregion

namespace ItemEditor
{
    public class Item
    {
        #region Private Properties

        protected byte[] spriteHash = null;

        #endregion

        #region Constructor

        public Item()
        {
            this.Type = ServerItemType.None;
            this.StackOrder = TileStackOrder.None;
            this.Movable = true;
            this.Name = string.Empty;
        }

        #endregion

        #region Public Properties

        public ushort ID { get; set; }

        public ServerItemType Type { get; set; }

        public bool HasStackOrder { get; set; }

        public TileStackOrder StackOrder { get; set; }

        public bool Unpassable { get; set; }

        public bool BlockMissiles { get; set; }

        public bool BlockPathfinder { get; set; }

        public bool HasElevation { get; set; }

        public bool ForceUse { get; set; }

        public bool MultiUse { get; set; }

        public bool Pickupable { get; set; }

        public bool Movable { get; set; }

        public bool Stackable { get; set; }

        public bool Readable { get; set; }

        public bool Rotatable { get; set; }

        public bool Hangable { get; set; }

        public bool HookSouth { get; set; }

        public bool HookEast { get; set; }

        public bool HasCharges { get; set; }

        public bool IgnoreLook { get; set; }

        public bool FullGround { get; set; }

        public bool AllowDistanceRead { get; set; }

        public bool IsAnimation { get; set; }

        public ushort GroundSpeed { get; set; }

        public ushort LightLevel { get; set; }

        public ushort LightColor { get; set; }

        public ushort MaxReadChars { get; set; }

        public ushort MaxReadWriteChars { get; set; }

        public ushort MinimapColor { get; set; }

        public ushort TradeAs { get; set; }

        public string Name { get; set; }

        // used to find sprites during updates
        public virtual byte[] SpriteHash
        {
            get
            {
                return this.spriteHash;
            }

            set
            {
                this.spriteHash = value;
            }
        }

        #endregion

        #region Public Methods

        public bool Equals(Item item)
        {
            if (this.Type != item.Type ||
                this.StackOrder != item.StackOrder ||
                this.Unpassable != item.Unpassable ||
                this.BlockMissiles != item.BlockMissiles ||
                this.BlockPathfinder != item.BlockPathfinder ||
                this.HasElevation != item.HasElevation ||
                this.ForceUse != item.ForceUse ||
                this.MultiUse != item.MultiUse ||
                this.Pickupable != item.Pickupable ||
                this.Movable != item.Movable ||
                this.Stackable != item.Stackable ||
                this.Readable != item.Readable ||
                this.Rotatable != item.Rotatable ||
                this.Hangable != item.Hangable ||
                this.HookSouth != item.HookSouth ||
                this.HookEast != item.HookEast ||
                this.IgnoreLook != item.IgnoreLook ||
                this.FullGround != item.FullGround ||
                this.IsAnimation != item.IsAnimation ||
                this.GroundSpeed != item.GroundSpeed ||
                this.LightLevel != item.LightLevel ||
                this.LightColor != item.LightColor ||
                this.MaxReadChars != item.MaxReadChars ||
                this.MaxReadWriteChars != item.MaxReadWriteChars ||
                this.MinimapColor != item.MinimapColor ||
                this.TradeAs != item.TradeAs)
            {
                return false;
            }

            if (this.Name.CompareTo(item.Name) != 0)
            {
                return false;
            }

            return true;
        }

        public bool HasProperties(ServerItemFlag properties)
        {
            if (properties == ServerItemFlag.None) return false;
            if (properties.HasFlag(ServerItemFlag.Unpassable) && !this.Unpassable) return false;
            if (properties.HasFlag(ServerItemFlag.BlockMissiles) && !this.BlockMissiles) return false;
            if (properties.HasFlag(ServerItemFlag.BlockPathfinder) && !this.BlockPathfinder) return false;
            if (properties.HasFlag(ServerItemFlag.HasElevation) && !this.HasElevation) return false;
            if (properties.HasFlag(ServerItemFlag.ForceUse) && !this.ForceUse) return false;
            if (properties.HasFlag(ServerItemFlag.MultiUse) && !this.MultiUse) return false;
            if (properties.HasFlag(ServerItemFlag.Pickupable) && !this.Pickupable) return false;
            if (properties.HasFlag(ServerItemFlag.Movable) && !this.Movable) return false;
            if (properties.HasFlag(ServerItemFlag.Stackable) && !this.Stackable) return false;
            if (properties.HasFlag(ServerItemFlag.Readable) && !this.Readable) return false;
            if (properties.HasFlag(ServerItemFlag.Rotatable) && !this.Rotatable) return false;
            if (properties.HasFlag(ServerItemFlag.Hangable) && !this.Hangable) return false;
            if (properties.HasFlag(ServerItemFlag.HookSouth) && !this.HookSouth) return false;
            if (properties.HasFlag(ServerItemFlag.HookEast) && !this.HookEast) return false;
            if (properties.HasFlag(ServerItemFlag.AllowDistanceRead) && !this.AllowDistanceRead) return false;
            if (properties.HasFlag(ServerItemFlag.IgnoreLook) && !this.IgnoreLook) return false;
            if (properties.HasFlag(ServerItemFlag.FullGround) && !this.FullGround) return false;
            if (properties.HasFlag(ServerItemFlag.IsAnimation) && !this.IsAnimation) return false;
            return true;
        }

        public Item CopyPropertiesFrom(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Type type = this.GetType();

            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                if (property.Name == "SpriteHash")
                {
                    continue;
                }

                PropertyInfo targetProperty = type.GetProperty(property.Name);
                if (targetProperty == null)
                {
                    continue;
                }

                if (!targetProperty.CanWrite)
                {
                    continue;
                }

                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }

                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }

                if (!targetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                {
                    continue;
                }

                targetProperty.SetValue(this, property.GetValue(item, null), null);
            }

            return this;
        }

        #endregion
    }

    public class ClientItem : Item
    {
        #region Private Properties

        private static Rectangle Rect = new Rectangle();

        #endregion

        #region Constructor

        public ClientItem()
        {
            this.SpriteList = new List<Sprite>();
        }

        #endregion

        #region Public Properties

        public byte Width { get; set; }

        public byte Height { get; set; }

        public byte Layers { get; set; }

        public byte PatternX { get; set; }

        public byte PatternY { get; set; }

        public byte PatternZ { get; set; }

        public byte Frames { get; set; }

        public uint NumSprites { get; set; }

        public List<Sprite> SpriteList { get; private set; }

        public override byte[] SpriteHash
        {
            get
            {
                if (this.spriteHash == null)
                {
                    MD5 md5 = MD5.Create();
                    int spriteBase = 0;
                    MemoryStream stream = new MemoryStream();
                    byte[] rgbaData = new byte[Sprite.ARGBPixelsDataSize];

                    for (byte l = 0; l < this.Layers; l++)
                    {
                        for (byte h = 0; h < this.Height; h++)
                        {
                            for (byte w = 0; w < this.Width; w++)
                            {
                                int index = spriteBase + w + h * this.Width + l * this.Width * this.Height;
                                Sprite sprite = this.SpriteList[index];
                                byte[] rgbData = sprite.GetRGBData();

                                // reverse rgb
                                for (int y = 0; y < Sprite.DefaultSize; ++y)
                                {
                                    for (int x = 0; x < Sprite.DefaultSize; ++x)
                                    {
                                        rgbaData[128 * y + x * 4 + 0] = rgbData[(32 - y - 1) * 96 + x * 3 + 2]; // blue
                                        rgbaData[128 * y + x * 4 + 1] = rgbData[(32 - y - 1) * 96 + x * 3 + 1]; // green
                                        rgbaData[128 * y + x * 4 + 2] = rgbData[(32 - y - 1) * 96 + x * 3 + 0]; // red
                                        rgbaData[128 * y + x * 4 + 3] = 0;
                                    }
                                }

                                stream.Write(rgbaData, 0, Sprite.ARGBPixelsDataSize);
                            }
                        }
                    }

                    stream.Position = 0;
                    this.spriteHash = md5.ComputeHash(stream);
                }

                return this.spriteHash;
            }

            set
            {
                this.spriteHash = value;
            }
        }

        // contains sprite signatures using Euclidean distance (4x4 blocks) on a ff2d generated image of the sprite
        public double[,] SpriteSignature { get; set; }

        #endregion

        #region Public Methods

        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(this.Width * Sprite.DefaultSize, this.Height * Sprite.DefaultSize, PixelFormat.Format32bppArgb);

            try
            {
                using (BitmapLocker locker = new BitmapLocker(bitmap))
                {
                    locker.LockBits();

                    for (byte l = 0; l < this.Layers; l++)
                    {
                        for (byte w = 0; w < this.Width; w++)
                        {
                            for (byte h = 0; h < this.Height; h++)
                            {
                                int index = w + h * this.Width + l * this.Width * this.Height;
                                int px = (this.Width - w - 1) * Sprite.DefaultSize;
                                int py = (this.Height - h - 1) * Sprite.DefaultSize;

                                locker.CopyPixels(this.SpriteList[index].GetBitmap(), px, py);
                            }
                        }
                    }

                    locker.UnlockBits();
                    bitmap.MakeTransparent(Color.FromArgb(0x11, 0x11, 0x11));
                }
            }
            catch
            {
                Trace.WriteLine(string.Format("Failed to get image for client id {0}. Check the transparency option.", this.ID));
                return null;
            }

            return bitmap;
        }

        public void GenerateSignature()
        {
            int width = Sprite.DefaultSize;
            int height = Sprite.DefaultSize;

            if (this.Width > 1 || this.Height > 1)
            {
                width = Sprite.DefaultSize * 2;
                height = Sprite.DefaultSize * 2;
            }

            Bitmap canvas = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(canvas);

            // draw sprite
            for (int l = 0; l < this.Layers; l++)
            {
                for (int h = 0; h < this.Height; ++h)
                {
                    for (int w = 0; w < this.Width; ++w)
                    {
                        int index = w + h * this.Width + l * this.Width * this.Height;
                        Bitmap bitmap = ImageUtils.GetBitmap(this.SpriteList[index].GetRGBData(), PixelFormat.Format24bppRgb, Sprite.DefaultSize, Sprite.DefaultSize);

                        if (canvas.Width == Sprite.DefaultSize)
                        {
                            Rect.X = 0;
                            Rect.Y = 0;
                            Rect.Width = bitmap.Width;
                            Rect.Height = bitmap.Height;
                        }
                        else
                        {
                            Rect.X = Math.Max(Sprite.DefaultSize - w * Sprite.DefaultSize, 0);
                            Rect.Y = Math.Max(Sprite.DefaultSize - h * Sprite.DefaultSize, 0);
                            Rect.Width = bitmap.Width;
                            Rect.Height = bitmap.Height;
                        }

                        g.DrawImage(bitmap, Rect);
                    }
                }
            }

            g.Save();

            Bitmap ff2dBmp = Fourier.fft2dRGB(canvas, false);
            this.SpriteSignature = ImageUtils.CalculateEuclideanDistance(ff2dBmp, 1);
        }

        #endregion
    }
}
