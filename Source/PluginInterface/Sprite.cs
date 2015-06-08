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
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
#endregion

namespace ItemEditor
{
    public class Sprite
    {
        #region Properties

        public uint id;
        public uint size;
        public byte[] dump;
        public bool useAlpha;

        #endregion

        #region Contructor

        public Sprite()
        {
            id = 0;
            size = 0;
            dump = null;
            useAlpha = false;
        }

        #endregion

        #region Methods

        public byte[] GetRGBData()
        {
            try
            {
                const byte transparentColor = 0x11;
                return GetRGBData(transparentColor);
            }
            catch
            {
                Trace.WriteLine(String.Format("Failed to get sprite id {0}", this.id));
                return blankSprite;
            }
        }

        private byte[] GetRGBData(byte transparentColor)
        {
            byte[] rgb32x32x3 = new byte[32 * 32 * 3];
            uint bytes = 0;
            uint x = 0;
            uint y = 0;
            Int32 chunkSize;
            byte channels = (byte)(useAlpha ? 4 : 3);

            if (dump == null || dump.Length != size)
            {
                return blankSprite;
            }

            while (bytes < size)
            {
                chunkSize = dump[bytes] | dump[bytes + 1] << 8;
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

                if (bytes >= size) break; // We're done
                // Now comes a pixel chunk, read it!
                chunkSize = dump[bytes] | dump[bytes + 1] << 8;
                bytes += 2;
                for (int i = 0; i < chunkSize; ++i)
                {
                    byte red = dump[bytes + 0];
                    byte green = dump[bytes + 1];
                    byte blue = dump[bytes + 2];

                    rgb32x32x3[96 * y + x * 3 + 0] = red;
                    rgb32x32x3[96 * y + x * 3 + 1] = green;
                    rgb32x32x3[96 * y + x * 3 + 2] = blue;

                    bytes += channels;

                    x++;
                    if (x >= 32)
                    {
                        x = 0;
                        ++y;
                    }
                }
            }

            // Fill up any trailing pixels
            while (y < 32 && x < 32)
            {
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

            return rgb32x32x3;
        }

        public byte[] GetRGBAData()
        {
            byte[] rgb32x32x3 = GetRGBData();
            byte[] rgbx32x32x4 = new byte[32 * 32 * 4];

            //reverse rgb
            for (int y = 0; y < 32; ++y)
            {
                for (int x = 0; x < 32; ++x)
                {
                    rgbx32x32x4[128 * y + x * 4 + 0] = rgb32x32x3[(32 - y - 1) * 96 + x * 3 + 2]; //blue
                    rgbx32x32x4[128 * y + x * 4 + 1] = rgb32x32x3[(32 - y - 1) * 96 + x * 3 + 1]; //green
                    rgbx32x32x4[128 * y + x * 4 + 2] = rgb32x32x3[(32 - y - 1) * 96 + x * 3 + 0]; //red
                    rgbx32x32x4[128 * y + x * 4 + 3] = 0;
                }
            }

            return rgbx32x32x4;
        }

        #endregion

        #region Static

        private static byte[] blankSprite = new byte[32 * 32 * 3];

        public static void CreateBlankSprite()
        {
            for (short i = 0; i < blankSprite.Length; i++)
            {
                blankSprite[i] = 0x11;
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
                        Trace.WriteLine(String.Format(message, client.SprSignature, sprSignature));
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
                                if (sprite.size > 0)
                                {
                                    //generate warning
                                }
                                else
                                {
                                    sprite.id = id;
                                    sprite.size = size;
                                    sprite.dump = reader.ReadBytes(size);
                                    sprite.useAlpha = transparency;

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
