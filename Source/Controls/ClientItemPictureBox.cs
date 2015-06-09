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
        private Rectangle destRect;
        private Rectangle sourceRect;

        #endregion

        #region Constructor

        public ClientItemPictureBox()
        {
            this.destRect = new Rectangle();
            this.sourceRect = new Rectangle();
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
                if (this.item != value)
                {
                    this.item = value;
                    this.Image = null;
                    this.DrawItem();
                }
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

            Bitmap bitmap = this.item.GetBitmap();
            if (bitmap != null)
            {
                this.destRect.X = Math.Max(0, (int)((canvas.Width - bitmap.Width) * 0.5));
                this.destRect.Y = Math.Max(0, (int)((canvas.Height - bitmap.Height) * 0.5));
                this.destRect.Width = Math.Min(canvas.Width, bitmap.Width);
                this.destRect.Height = Math.Min(canvas.Height, bitmap.Height);

                this.sourceRect.Width = bitmap.Width;
                this.sourceRect.Height = bitmap.Height;

                g.DrawImage(bitmap, this.destRect, this.sourceRect, GraphicsUnit.Pixel);
                g.Save();
            }

            canvas.MakeTransparent(Color.FromArgb(0x11, 0x11, 0x11));
            this.Image = canvas;
        }

        #endregion
    }
}
