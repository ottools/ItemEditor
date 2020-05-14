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
using DarkUI.Config;
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
            this.BackColor = Colors.DarkBackground;
            this.BorderStyle = BorderStyle.None;
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

        private void DrawItemHandler(object sender, DrawItemEventArgs args)
        {
            if (Plugin == null || args.Index == -1)
            {
                return;
            }

            Graphics graphics = args.Graphics;
            Rectangle bounds = args.Bounds;
            bounds.Width--;

            ServerItem serverItem = (ServerItem)Items[args.Index];

            // find the area in which to put the text and draw.
            layoutRect.X = bounds.Left + 32 + (3 * ItemMargin);
            layoutRect.Y = bounds.Top + (ItemMargin * 2);
            layoutRect.Width = bounds.Right - ItemMargin - layoutRect.X;
            layoutRect.Height = bounds.Bottom - ItemMargin - layoutRect.Y;

            // draw background
            if ((args.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                graphics.FillRectangle(Colors.ListSelectionBrush, bounds);
            }
            else
            {
                graphics.FillRectangle(Colors.ListBackgroudBrush, bounds);
            }

            destRect.Y = bounds.Top + ItemMargin;

            // draw view background
            graphics.FillRectangle(Colors.ListViewBackgroudBrush, destRect);

            // draw text
            graphics.DrawString(serverItem.ToString(), Font, Colors.TextColorBrush, layoutRect);

            ClientItem clientItem = this.plugin.GetClientItem(serverItem.ClientId);
            if (clientItem != null)
            {
                Bitmap bitmap = clientItem.GetBitmap();
                if (bitmap != null)
                {
                    sourceRect.Width = bitmap.Width;
                    sourceRect.Height = bitmap.Height;
                    graphics.DrawImage(bitmap, destRect, sourceRect, GraphicsUnit.Pixel);
                }
            }

            // draw view border
            graphics.DrawRectangle(Colors.BorderColorPen, destRect);

            // draw border
            graphics.DrawRectangle(Colors.BorderColorPen, bounds);
        }

        #endregion

        #region Class Properties

        private static readonly Brush WhiteBrush = new SolidBrush(Color.White);
        private static readonly Brush BlackBrush = new SolidBrush(Color.Black);

        #endregion
    }
}
