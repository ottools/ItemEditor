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

using DarkUI.Config;
using GameEngine.Controls;
using OTLib.Server.Items;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ItemEditor.Controls
{
    public class ServerItemListBox : ListBase<ServerItem>
    {
        private const int ItemMargin = 5;

        private Rectangle m_layoutRect;
        private Rectangle m_destRect;
        private Rectangle m_sourceRect;

        public ServerItemListBox() : base(ListBaseLayout.Vertical)
        {
            m_layoutRect = new Rectangle();
            m_destRect = new Rectangle(ItemMargin, 0, 32, 32);
            m_sourceRect = new Rectangle();

            ItemSize = 32 + (ItemMargin * 2);
            MultiSelect = false;
        }

        public IPlugin Plugin { get; set; }
        public ushort MinimumID { get; private set; }
        public ushort MaximumID { get; private set; }

        public void Add(List<ServerItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            BeginUpdate();
            for (int i = 0, length = items.Count; i < length; i++)
            {
                ServerItem item = items[i];
                MinimumID = item.ID < MinimumID ? item.ID : MinimumID;
                MaximumID = item.ID > MaximumID ? item.ID : MaximumID;
                Items.Add(item);
            }
            EndUpdate();
        }

        protected override void UpdateItemPosition(ServerItem item, int index)
        {
            ////
        }

        protected override void PaintContent(Graphics graphics)
        {
            List<int> range = GetIndexesInView().ToList();
            if (range.Count == 0)
                return;

            int min = range.Min();
            int max = range.Max();
            int width = Math.Max(ContentSize.Width, Viewport.Width);
            Rectangle rect = new Rectangle(0, 0, width, ItemSize);

            for (int i = min; i <= max; i++)
            {
                rect.Y = i * ItemSize;

                if (SelectedIndices.Count > 0 && SelectedIndices.Contains(i))
                    graphics.FillRectangle(Colors.ListSelectionBrush, rect);
                else
                    graphics.FillRectangle(Colors.ListBackgroudBrush, rect);

                ServerItem item = Items[i];

                // find the area in which to put the text and draw.
                m_layoutRect.X = rect.Left + 32 + (3 * ItemMargin);
                m_layoutRect.Y = rect.Top + (ItemMargin * 2);
                m_layoutRect.Width = rect.Right - ItemMargin - m_layoutRect.X;
                m_layoutRect.Height = rect.Bottom - ItemMargin - m_layoutRect.Y;

                m_destRect.Y = rect.Top + ItemMargin;

                // draw view background
                graphics.FillRectangle(Colors.ListViewBackgroudBrush, m_destRect);

                // draw text
                graphics.DrawString(item.ToString(), Font, Colors.TextColorBrush, m_layoutRect);

                ClientItem clientItem = Plugin.GetClientItem(item.ClientId);
                if (clientItem != null)
                {
                    Bitmap bitmap = clientItem.GetBitmap();
                    if (bitmap != null)
                    {
                        m_sourceRect.Width = bitmap.Width;
                        m_sourceRect.Height = bitmap.Height;
                        graphics.DrawImage(bitmap, m_destRect, m_sourceRect, GraphicsUnit.Pixel);
                    }
                }

                // draw view border
                graphics.DrawRectangle(Colors.BorderColorPen, m_destRect);

                // draw border
                graphics.DrawRectangle(Colors.BorderColorPen, rect);
            }
        }
    }
}
