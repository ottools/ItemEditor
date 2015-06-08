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
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Controls
{
    public class ServerItemListBox : ListBox
    {
        #region Private Properties

        private const int ItemMargin = 5;

        private IPlugin plugin;
        private Rectangle layoutRect;
        private Rectangle destRect;
        private Rectangle sourceRect;
        private Pen pen;

        #endregion

        #region Constructor

        public ServerItemListBox()
        {
            this.layoutRect = new Rectangle();
            this.destRect = new Rectangle(ItemMargin, 0, 32, 32);
            this.sourceRect = new Rectangle();
            this.pen = new Pen(Color.Transparent);
            this.MeasureItem += new MeasureItemEventHandler(this.MeasureItemHandler);
            this.DrawItem += new DrawItemEventHandler(this.DrawItemHandler);
            this.DrawMode = DrawMode.OwnerDrawVariable;
        }

        #endregion

        #region Public Properties

        public IPlugin Plugin
        {
            get
            {
                return this.plugin;
            }

            set
            {
                this.Clear();
                this.plugin = value;
            }
        }

        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        #endregion

        #region Public Methods

        public void Add(ServerItem item)
        {
            Items.Add(item);
        }

        public void Add(List<ServerItem> items)
        {
            Items.AddRange(items.ToArray());
        }

        public void Clear()
        {
            Items.Clear();
        }

        #endregion

        #region Event Handlers

        private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)(32 + (2 * ItemMargin));
        }

        private void DrawItemHandler(object sender, DrawItemEventArgs ev)
        {
            if (this.plugin == null || ev.Index == -1)
            {
                return;
            }

            Rectangle bounds = ev.Bounds;

            ev.DrawBackground();
            ev.Graphics.DrawRectangle(Pens.Gray, bounds); // Border.

            Brush brush;
            if ((ev.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                brush = SystemBrushes.HighlightText;
            }
            else
            {
                brush = new SolidBrush(ev.ForeColor);
            }

            ServerItem item = (ServerItem)this.Items[ev.Index];

            // Find the area in which to put the text and draw.
            this.layoutRect.X = bounds.Left + 32 + (3 * ItemMargin);
            this.layoutRect.Y = bounds.Top + (ItemMargin * 2);
            this.layoutRect.Width = bounds.Right - ItemMargin - this.layoutRect.X;
            this.layoutRect.Height = bounds.Bottom - ItemMargin - this.layoutRect.Y;

            // Draw sprite name
            ev.Graphics.DrawString(item.ToString(), this.Font, brush, this.layoutRect);

            this.destRect.Y = bounds.Top + ItemMargin;

            ClientItem clientItem;
            if (this.plugin.Items.TryGetValue(item.ClientId, out clientItem))
            {
                Bitmap bitmap = this.GetSpriteBitmap(clientItem);
                if (bitmap != null)
                {
                    this.sourceRect.Width = bitmap.Width;
                    this.sourceRect.Height = bitmap.Height;
                    ev.Graphics.DrawImage(bitmap, this.destRect, this.sourceRect, GraphicsUnit.Pixel);
                }
            }

            this.pen.Brush = brush;
            ev.Graphics.DrawRectangle(this.pen, this.destRect);
            ev.DrawFocusRectangle();
        }

        #endregion

        private int spritePixels = 32;

        private Bitmap GetSpriteBitmap(ClientItem clientItem)
        {
            int Width = spritePixels;
            int Height = spritePixels;

            if (clientItem.width > 1 || clientItem.height > 1)
            {
                Width = spritePixels * 2;
                Height = spritePixels * 2;
            }

            Bitmap canvas = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0x11, 0x11, 0x11)), 0, 0, canvas.Width, canvas.Height);
                g.Save();
            }

            DrawSprite(ref canvas, clientItem);

            Bitmap newImage = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(canvas, new Point(0, 0));
                g.Save();
            }

            newImage.MakeTransparent(Color.FromArgb(0x11, 0x11, 0x11));
            return newImage;
        }

        private void DrawSprite(ref Bitmap canvas, ClientItem clientItem)
        {
            Graphics g = Graphics.FromImage(canvas);
            Rectangle rect = new Rectangle();

            //draw sprite
            for (int l = 0; l < clientItem.layers; l++)
            {
                for (int h = 0; h < clientItem.height; ++h)
                {
                    for (int w = 0; w < clientItem.width; ++w)
                    {
                        int frameIndex = w + h * clientItem.width + l * clientItem.width * clientItem.height;
                        Bitmap bmp = ImageUtils.GetBitmap(clientItem.GetRGBData(frameIndex), PixelFormat.Format24bppRgb, spritePixels, spritePixels);

                        if (canvas.Width == spritePixels)
                        {
                            rect.X = 0;
                            rect.Y = 0;
                            rect.Width = bmp.Width;
                            rect.Height = bmp.Height;
                        }
                        else
                        {
                            rect.X = Math.Max(spritePixels - w * spritePixels, 0);
                            rect.Y = Math.Max(spritePixels - h * spritePixels, 0);
                            rect.Width = bmp.Width;
                            rect.Height = bmp.Height;
                        }
                        g.DrawImage(bmp, rect);
                    }
                }
            }

            g.Save();
        }
    }
}
