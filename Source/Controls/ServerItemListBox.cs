#region Licence
/**
* Copyright © 2014-2016 OTTools <https://github.com/ottools>
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
using PluginInterface;
using System.Collections.Generic;
using System.Drawing;
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

            // draw background
            ev.DrawBackground();

            // draw border
            ev.Graphics.DrawRectangle(Pens.Gray, bounds);

            ServerItem serverItem = (ServerItem)this.Items[ev.Index];

            // Find the area in which to put the text and draw.
            this.layoutRect.X = bounds.Left + 32 + (3 * ItemMargin);
            this.layoutRect.Y = bounds.Top + (ItemMargin * 2);
            this.layoutRect.Width = bounds.Right - ItemMargin - this.layoutRect.X;
            this.layoutRect.Height = bounds.Bottom - ItemMargin - this.layoutRect.Y;

            // draw server item id and name
            if ((ev.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                this.pen.Brush = WhiteBrush;
                ev.Graphics.DrawString(serverItem.ToString(), this.Font, WhiteBrush, this.layoutRect);
            }
            else
            {
                this.pen.Brush = BlackBrush;
                ev.Graphics.DrawString(serverItem.ToString(), this.Font, BlackBrush, this.layoutRect);
            }

            this.destRect.Y = bounds.Top + ItemMargin;

            ClientItem clientItem = this.plugin.GetClientItem(serverItem.ClientId);
            if (clientItem != null)
            {
                Bitmap bitmap = clientItem.GetBitmap();
                if (bitmap != null)
                {
                    this.sourceRect.Width = bitmap.Width;
                    this.sourceRect.Height = bitmap.Height;
                    ev.Graphics.DrawImage(bitmap, this.destRect, this.sourceRect, GraphicsUnit.Pixel);
                }
            }

            // draw item border
            ev.Graphics.DrawRectangle(this.pen, this.destRect);

            // draw focus rect
            ev.DrawFocusRectangle();
        }

        #endregion

        #region Class Properties

        private static readonly Brush WhiteBrush = new SolidBrush(Color.White);
        private static readonly Brush BlackBrush = new SolidBrush(Color.Black);

        #endregion
    }
}
