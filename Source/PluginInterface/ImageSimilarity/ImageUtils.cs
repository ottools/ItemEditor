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
using System;
using System.Drawing;
using System.Drawing.Imaging;
#endregion

namespace ImageSimilarity
{
    public class ImageUtils
    {
        #region Static Methods

        public static double CompareSignature(double[,] signature1, double[,] signature2)
        {
            if (signature1.Length != signature2.Length)
            {
                return 1.0d;
            }

            double rSum = 0.0, gSum = 0.0, bSum = 0;
            for (int i = 0; i < signature2.GetLength(1); i++)
            {
                rSum += (signature1[0, i] - signature2[0, i]) * (signature1[0, i] - signature2[0, i]);
                gSum += (signature1[1, i] - signature2[1, i]) * (signature1[1, i] - signature2[1, i]);
                bSum += (signature1[2, i] - signature2[2, i]) * (signature1[2, i] - signature2[2, i]);
            }

            rSum = Math.Sqrt(rSum);
            gSum = Math.Sqrt(gSum);
            bSum = Math.Sqrt(bSum);

            return rSum + gSum + bSum;
        }

        public static double[,] CalculateEuclideanDistance(Bitmap input, int blockSize)
        {
            BitmapData bmpData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.WriteOnly, input.PixelFormat);

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * input.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);
            input.UnlockBits(bmpData);

            int blockNum = 0;
            double[,] lSignature = new double[3, (input.Width / blockSize) * (input.Height / blockSize)];
            double rSum = 0.0, gSum = 0.0, bSum = 0;

            for (int y = 0; y < input.Height; y += blockSize)
            {
                for (int x = 0; x < input.Width; x += blockSize)
                {
                    lSignature[0, blockNum] = 0.0;
                    lSignature[1, blockNum] = 0.0;
                    lSignature[2, blockNum] = 0.0;

                    for (int blocky = 0; blocky < blockSize; ++blocky)
                    {
                        for (int blockx = 0; blockx < blockSize; ++blockx)
                        {
                            byte r = rgbValues[((y + blocky) * 3 * input.Height) + ((x + blockx) * 3) + 0];
                            byte g = rgbValues[((y + blocky) * 3 * input.Height) + ((x + blockx) * 3) + 1];
                            byte b = rgbValues[((y + blocky) * 3 * input.Height) + ((x + blockx) * 3) + 2];

                            rSum += (double)r;
                            gSum += (double)g;
                            bSum += (double)b;
                        }
                    }

                    lSignature[0, blockNum] = Math.Sqrt(rSum);
                    lSignature[1, blockNum] = Math.Sqrt(gSum);
                    lSignature[2, blockNum] = Math.Sqrt(bSum);
                    ++blockNum;
                }
            }

            // Normalize
            for (int i = 0; i < lSignature.GetLength(1); i++)
            {
                rSum += lSignature[0, i];
                gSum += lSignature[1, i];
                bSum += lSignature[2, i];
            }

            for (int i = 0; i < lSignature.GetLength(1); i++)
            {
                lSignature[0, i] /= rSum;
                lSignature[1, i] /= gSum;
                lSignature[2, i] /= bSum;
            }

            return lSignature;
        }

        public static byte[] GreyScale(Bitmap input)
        {
            BitmapData bmpData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadWrite, input.PixelFormat);
            int width = input.Width;
            int height = input.Height;
            byte[] data = new byte[input.Width * input.Height * 3];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

            input.UnlockBits(bmpData);

            byte[] greyscale = new byte[width * height];

            int counter = 0;
            for (int i = 0; i < data.Length; i += 3)
            {
                greyscale[counter++] = (byte)(((66 * data[i + 2] + 129 * data[i] + 25 * data[i + 1] + 128) >> 8) + 16);
            }

            return greyscale;
        }

        public static byte[] CombineColorChannels(RGB[] rgb)
        {
            byte[] data = new byte[rgb.Length * 3];

            int counter = 0;
            for (int i = 0; i < data.Length; i += 3)
            {
                data[i] = rgb[counter].r;
                data[i + 1] = rgb[counter].g;
                data[i + 2] = rgb[counter].b;

                ++counter;
            }

            return data;
        }

        public static RGB[] SplitColorChannels(Bitmap input)
        {
            BitmapData bmpData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadWrite, input.PixelFormat);
            int width = input.Width;
            int height = input.Height;
            byte[] data = new byte[input.Width * input.Height * 3];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

            input.UnlockBits(bmpData);

            RGB[] rgb = new RGB[width * height];

            int counter = 0;
            for (int i = 0; i < data.Length; i += 3)
            {
                rgb[counter].r = data[i + 0];
                rgb[counter].b = data[i + 1];
                rgb[counter].g = data[i + 2];

                ++counter;
            }

            return rgb;
        }

        public static Bitmap GetBitmap(byte[] rgbData, PixelFormat pixelFormat, int Width, int Height)
        {
            int bitPerPixel = Image.GetPixelFormatSize(pixelFormat);
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            if (pixelFormat == PixelFormat.Format24bppRgb)
            {
                // reverse rgb
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        byte r = rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 0];
                        byte g = rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 1];
                        byte b = rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 2];

                        rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 0] = b;
                        rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 1] = g;
                        rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 2] = r;
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbData, 0, bmpData.Scan0, rgbData.Length);
            }
            else
            {
                byte[] grayscale = new byte[Width * Height * 3];
                int n = 0;
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        grayscale[n * 3 + 0] = rgbData[n];
                        grayscale[n * 3 + 1] = rgbData[n];
                        grayscale[n * 3 + 2] = rgbData[n];
                        ++n;
                    }
                }

                // bmpData.Stride = -bmpData.Stride;
                System.Runtime.InteropServices.Marshal.Copy(grayscale, 0, bmpData.Scan0, grayscale.Length);
            }


            bmp.UnlockBits(bmpData);
            return bmp;
        }

        #endregion

        #region RGB Struct

        public struct RGB
        {
            public byte r;
            public byte g;
            public byte b;
        }

        #endregion
    }
}
