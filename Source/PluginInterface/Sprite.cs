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
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
#endregion

namespace ItemEditor
{
    public class Sprite
    {
        #region Contructor

        public Sprite()
        {
            this.ID = 0;
            this.Size = 0;
            this.CompressedPixels = null;
            this.Transparent = false;
        }

        #endregion

        #region Public Properties

        public const byte DefaultSize = 32;
        public const ushort RGBPixelsDataSize = 3072; // 32*32*3
        public const ushort ARGBPixelsDataSize = 4096; // 32*32*4

        public uint ID { get; set; }
        public uint Size { get; set; }
        public byte[] CompressedPixels { get; set; }
        public bool Transparent { get; set; }

        #endregion

        #region Public Methods

        public byte[] GetRGBData()
        {
            try
            {
                const byte transparentColor = 0x11;
                return this.GetRGBData(transparentColor);
            }
            catch
            {
                Trace.WriteLine(string.Format("Failed to get sprite id {0}", this.ID));
                return BlankRGBSprite;
            }
        }

        private byte[] GetRGBData(byte transparentColor)
        {
            if (this.CompressedPixels == null || this.CompressedPixels.Length != Size)
            {
                return BlankRGBSprite;
            }

            byte[] rgb32x32x3 = new byte[RGBPixelsDataSize];
            uint bytes = 0;
            uint x = 0;
            uint y = 0;
            Int32 chunkSize;
            byte bitPerPixel = (byte)(this.Transparent ? 4 : 3);

            while (bytes < Size)
            {
                chunkSize = this.CompressedPixels[bytes] | this.CompressedPixels[bytes + 1] << 8;
                bytes += 2;

                for (int i = 0; i < chunkSize; ++i)
                {
                    // Transparent pixel
                    rgb32x32x3[96 * y + x * 3 + 0] = transparentColor;
                    rgb32x32x3[96 * y + x * 3 + 1] = transparentColor;
                    rgb32x32x3[96 * y + x * 3 + 2] = transparentColor;
                    x++;
                    if (x >= 32)
                    {
                        x = 0;
                        ++y;
                    }
                }

                // We're done
                if (bytes >= Size)
                {
                    break;
                }

                // Now comes a pixel chunk, read it!
                chunkSize = this.CompressedPixels[bytes] | this.CompressedPixels[bytes + 1] << 8;
                bytes += 2;

                for (int i = 0; i < chunkSize; ++i)
                {
                    byte red = this.CompressedPixels[bytes + 0];
                    byte green = this.CompressedPixels[bytes + 1];
                    byte blue = this.CompressedPixels[bytes + 2];

                    rgb32x32x3[96 * y + x * 3 + 0] = red;
                    rgb32x32x3[96 * y + x * 3 + 1] = green;
                    rgb32x32x3[96 * y + x * 3 + 2] = blue;

                    bytes += bitPerPixel;

                    x++;
                    if (x >= 32)
                    {
                        x = 0;
                        ++y;
                    }
                }
            }

            // Fill up any trailing pixels
            while (y < DefaultSize && x < DefaultSize)
            {
                rgb32x32x3[96 * y + x * 3 + 0] = transparentColor;
                rgb32x32x3[96 * y + x * 3 + 1] = transparentColor;
                rgb32x32x3[96 * y + x * 3 + 2] = transparentColor;
                x++;

                if (x >= DefaultSize)
                {
                    x = 0;
                    ++y;
                }
            }

            return rgb32x32x3;
        }

        public byte[] GetPixels()
        {
            if (this.CompressedPixels == null || this.CompressedPixels.Length != this.Size)
            {
                return BlankARGBSprite;
            }

            int read = 0;
            int write = 0;
            int pos = 0;
            int transparentPixels = 0;
            int coloredPixels = 0;
            int length = this.CompressedPixels.Length;
            byte bitPerPixel = (byte)(this.Transparent ? 4 : 3);
            byte[] pixels = new byte[ARGBPixelsDataSize];

            for (read = 0; read < length; read += 4 + (bitPerPixel * coloredPixels))
            {
                transparentPixels = this.CompressedPixels[pos++] | this.CompressedPixels[pos++] << 8;
                coloredPixels = this.CompressedPixels[pos++] | this.CompressedPixels[pos++] << 8;

                for (int i = 0; i < transparentPixels; i++)
                {
                    pixels[write++] = 0x00; // Blue
                    pixels[write++] = 0x00; // Green
                    pixels[write++] = 0x00; // Red
                    pixels[write++] = 0x00; // Alpha
                }

                for (int i = 0; i < coloredPixels; i++)
                {
                    byte red = this.CompressedPixels[pos++];
                    byte green = this.CompressedPixels[pos++];
                    byte blue = this.CompressedPixels[pos++];
                    byte alpha = this.Transparent ? this.CompressedPixels[pos++] : (byte)0xFF;

                    pixels[write++] = blue;
                    pixels[write++] = green;
                    pixels[write++] = red;
                    pixels[write++] = alpha;
                }
            }

            // Fills the remaining pixels
            while (write < ARGBPixelsDataSize)
            {
                pixels[write++] = 0x00; // Blue
                pixels[write++] = 0x00; // Green
                pixels[write++] = 0x00; // Red
                pixels[write++] = 0x00; // Alpha
            }

            return pixels;
        }

        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(DefaultSize, DefaultSize, PixelFormat.Format32bppArgb);
            byte[] pixels = this.GetPixels();

            if (pixels != null)
            {
                BitmapData bitmapData = bitmap.LockBits(Rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                bitmap.UnlockBits(bitmapData);
            }

            return bitmap;
        }

        #endregion

        #region Static

        private static byte[] BlankRGBSprite = new byte[RGBPixelsDataSize];
        private static byte[] BlankARGBSprite = new byte[ARGBPixelsDataSize];
        private static readonly Rectangle Rect = new Rectangle(0, 0, DefaultSize, DefaultSize);

        public static void CreateBlankSprite()
        {
            for (short i = 0; i < RGBPixelsDataSize; i++)
            {
                BlankRGBSprite[i] = 0x11;
            }

            for (short i = 0; i < ARGBPixelsDataSize; i++)
            {
                BlankARGBSprite[i] = 0x11;
            }
        }

        public static bool LoadSprites(string filename, ref Dictionary<uint, Sprite> sprites, SupportedClient client, bool extended, bool transparency)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            try
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    uint sprSignature = reader.ReadUInt32();
                    if (client.SprSignature != sprSignature)
                    {
                        string message = "Bad spr signature. Expected signature is {0:X} and loaded signature is {1:X}.";
                        Trace.WriteLine(string.Format(message, client.SprSignature, sprSignature));
                        return false;
                    }

                    uint totalPics;
                    if (extended)
                    {
                        totalPics = reader.ReadUInt32();
                    }
                    else
                    {
                        totalPics = reader.ReadUInt16();
                    }

                    List<uint> spriteIndexes = new List<uint>();
                    for (uint i = 0; i < totalPics; ++i)
                    {
                        uint index = reader.ReadUInt32();
                        spriteIndexes.Add(index);
                    }

                    uint id = 1;
                    foreach (uint element in spriteIndexes)
                    {
                        uint index = element + 3;
                        reader.BaseStream.Seek(index, SeekOrigin.Begin);
                        ushort size = reader.ReadUInt16();

                        Sprite sprite;
                        if (sprites.TryGetValue(id, out sprite))
                        {
                            if (sprite != null && size > 0)
                            {
                                if (sprite.Size > 0)
                                {
                                    // generate warning
                                }
                                else
                                {
                                    sprite.ID = id;
                                    sprite.Size = size;
                                    sprite.CompressedPixels = reader.ReadBytes(size);
                                    sprite.Transparent = transparency;

                                    sprites[id] = sprite;
                                }
                            }
                        }
                        else
                        {
                            reader.BaseStream.Seek(size, SeekOrigin.Current);
                        }

                        ++id;
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }

            return true;
        }

        #endregion
    }
}
