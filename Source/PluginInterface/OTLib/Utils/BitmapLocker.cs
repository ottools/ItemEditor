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
using System.Runtime.InteropServices;
#endregion

namespace OTLib.Utils
{
    public class BitmapLocker : IDisposable
    {
        #region Private Properties

        private Bitmap bitmap;
        private BitmapData bitmapData;
        private byte[] pixels;
        private IntPtr address = IntPtr.Zero;

        #endregion

        #region Constructor

        public BitmapLocker(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        #endregion

        #region Public Methods

        public void LockBits()
        {
            // create rectangle to lock
            Rectangle rect = new Rectangle(0, 0, this.bitmap.Width, this.bitmap.Height);

            // lock bitmap and return bitmap data
            this.bitmapData = this.bitmap.LockBits(rect, ImageLockMode.ReadWrite, this.bitmap.PixelFormat);

            // create byte array to copy pixel values
            this.pixels = new byte[this.bitmap.Width * this.bitmap.Height * 4];
            this.address = this.bitmapData.Scan0;

            // copy data from pointer to array
            Marshal.Copy(this.address, this.pixels, 0, this.pixels.Length);
        }

        public void CopyPixels(Bitmap source, int x, int y)
        {
            for (int py = 0; py < source.Height; py++)
            {
                for (int px = 0; px < source.Width; px++)
                {
                    this.SetPixel(px + x, py + y, source.GetPixel(px, py));
                }
            }
        }

        public void CopyPixels(Bitmap source, int rx, int ry, int rw, int rh, int px, int py)
        {
            for (int y = 0; y < rh; y++)
            {
                for (int x = 0; x < rw; x++)
                {
                    this.SetPixel(px + x, py + y, source.GetPixel(rx + x, ry + y));
                }
            }
        }

        public void UnlockBits()
        {
            // copy data from byte array to pointer
            Marshal.Copy(this.pixels, 0, this.address, this.pixels.Length);

            // unlock bitmap data
            this.bitmap.UnlockBits(this.bitmapData);
        }

        public void Dispose()
        {
            this.bitmap = null;
            this.bitmapData = null;
            this.pixels = null;
        }

        #endregion

        #region Private Methods

        private void SetPixel(int x, int y, Color color)
        {
            // get start index of the specified pixel
            int i = ((y * this.bitmap.Width) + x) * 4;
            if (i > this.pixels.Length - 4)
            {
                return;
            }

            this.pixels[i] = color.B;
            this.pixels[i + 1] = color.G;
            this.pixels[i + 2] = color.R;
            this.pixels[i + 3] = color.A;
        }

        #endregion
    }
}
