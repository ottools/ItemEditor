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
using ImageSimilarity;
using OTLib.Server.Items;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Controls
{
    public class ClientItemPictureBox : PictureBox
    {
        #region Private Properties
        
        private ClientItem item;
        private Rectangle rect;

        #endregion

        #region Constructor

        public ClientItemPictureBox()
        {
            this.rect = new Rectangle();
        }

        #endregion

        #region Public Properties

        public ClientItem ClientItem
        {
            get
            {
                return this.item;
            }

            set
            {
                this.item = value;
                this.Image = null;
                this.DrawItem();
            }
        }

        #endregion

        #region Private Methods

        private void DrawItem()
        {
            if (this.item == null)
            {
                return;
            }

            Bitmap canvas = new Bitmap(64, 64, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(canvas);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0x11, 0x11, 0x11)), 0, 0, canvas.Width, canvas.Height);
            g.Save();

            // draw item
            for (int l = 0; l < this.item.layers; l++)
            {
                for (int h = 0; h < this.item.height; ++h)
                {
                    for (int w = 0; w < this.item.width; ++w)
                    {
                        int index = w + h * this.item.width + l * this.item.width * this.item.height;
                        Bitmap bitmap = ImageUtils.GetBitmap(this.item.GetRGBData(index), PixelFormat.Format24bppRgb, 32, 32);

                        if (canvas.Width == 32)
                        {
                            this.rect.X = 0;
                            this.rect.Y = 0;
                            this.rect.Width = bitmap.Width;
                            this.rect.Height = bitmap.Height;
                        }
                        else
                        {
                            this.rect.X = Math.Max(32 - (w * 32), 0);
                            this.rect.Y = Math.Max(32 - (h * 32), 0);
                            this.rect.Width = bitmap.Width;
                            this.rect.Height = bitmap.Height;
                        }

                        g.DrawImage(bitmap, this.rect);
                    }
                }
            }

            g.Save();

            canvas.MakeTransparent(Color.FromArgb(0x11, 0x11, 0x11));
            this.Image = canvas;
        }

        #endregion
    }
}
