using DarkUI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GameEngine.Controls
{
    public delegate void SelectedIndexChangedHandler(object sender);

    public enum ListBaseLayout
    {
        Vertical,
        Horizontal
    }

    public class ListBase<T> : DarkScrollView
    {
        private readonly ListBaseLayout m_layout;
        private int m_itemSize;
        private int m_anchoredItemStart;
        private int m_anchoredItemEnd;
        private bool m_updating;

        public ListBase(ListBaseLayout layout)
        {
            m_layout = layout;
            m_itemSize = 64;
            m_anchoredItemStart = -1;
            m_anchoredItemEnd = -1;

            Items = new ObservableCollection<T>();
            Items.CollectionChanged += ItemsCollectionChanged_Handler;
            SelectedIndices = new List<int>();
            MultiSelect = true;
            HorizontalScrollBarHidden = true;

            UpdateListBox();
        }

        public event SelectedIndexChangedHandler SelectedIndexChanged;

        public ObservableCollection<T> Items { get; }
        public List<int> SelectedIndices { get; }
        public int Count => Items.Count;

        public int SelectedIndex
        {
            get => SelectedIndices.Count == 1 ? SelectedIndices[0] : -1;
            set
            {
                if (Items.Count != 0 && (SelectedIndices.Count != 1 || value != SelectedIndices[0]))
                {
                    SelectItem(value);
                    EnsureVisible();
                }
            }
        }

        public T SelectedItem
        {
            get
            {
                int index = SelectedIndex;
                return index != -1 ? Items[index] : default;
            }

            set
            {
                if (Items.Count != 0 && value != null)
                {
                    int index = Items.IndexOf(value);
                    if (index != -1)
                        SelectedIndex = index;
                }
            }
        }

        public List<T> SelectedItems
        {
            get
            {
                List<T> items = new List<T>();
                foreach (int index in SelectedIndices)
                    items.Add(Items[index]);
                return items;
            }
        }

        public int ItemSize
        {
            get => m_itemSize;
            set
            {
                if (value != m_itemSize)
                {
                    m_itemSize = value;
                    Ticks = value;
                    UpdateListBox();
                }
            }
        }

        public bool MultiSelect { get; set; }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        public int GetIndexOf(T item)
        {
            if (item == null)
                return -1;
            return Items.IndexOf(item);
        }

        public int IndexFromPoint(Point point)
        {
            if (Items.Count == 0)
                return -1;

            point.Offset(Viewport.Left, Viewport.Top);

            List<int> range = GetIndexesInView().ToList();
            int min = range.Min();
            int max = range.Max();

            if (m_layout == ListBaseLayout.Vertical)
            {
                int width = Math.Max(ContentSize.Width, Viewport.Width);
                Rectangle rect = new Rectangle(0, 0, width, ItemSize);

                for (int i = min; i <= max; i++)
                {
                    rect.Y = i * ItemSize;
                    if (rect.Contains(point))
                        return i;
                }
            }
            else
            {
                int height = Math.Max(ContentSize.Height, Viewport.Height);
                Rectangle rect = new Rectangle(0, 0, ItemSize, height);

                for (int i = min; i <= max; i++)
                {
                    rect.X = i * ItemSize;
                    if (rect.Contains(point))
                        return i;
                }
            }

            return -1;
        }

        public void SelectItem(int index)
        {
            if (index < 0 || index > Items.Count - 1)
                throw new IndexOutOfRangeException($"Value '{index}' is outside of valid range.");

            SelectedIndices.Clear();
            SelectedIndices.Add(index);

            SelectedIndexChanged?.Invoke(this);

            m_anchoredItemStart = index;
            m_anchoredItemEnd = index;

            Invalidate();
        }

        public void SelectItems(IEnumerable<int> indexes)
        {
            SelectedIndices.Clear();

            foreach (int index in indexes)
            {
                if (index < 0 || index > Items.Count - 1)
                    throw new IndexOutOfRangeException($"Value '{index}' is outside of valid range.");

                SelectedIndices.Add(index);
            }

            SelectedIndexChanged?.Invoke(this);

            List<int> list = indexes.ToList();
            m_anchoredItemStart = list[list.Count - 1];
            m_anchoredItemEnd = list[list.Count - 1];

            Invalidate();
        }

        public void ToggleItem(int index)
        {
            if (SelectedIndices.Contains(index))
            {
                SelectedIndices.Remove(index);

                // If we just removed both the anchor start AND end then reset them
                if (m_anchoredItemStart == index && m_anchoredItemEnd == index)
                {
                    if (SelectedIndices.Count > 0)
                    {
                        m_anchoredItemStart = SelectedIndices[0];
                        m_anchoredItemEnd = SelectedIndices[0];
                    }
                    else
                    {
                        m_anchoredItemStart = -1;
                        m_anchoredItemEnd = -1;
                    }
                }

                // If we just removed the anchor start then update it accordingly
                if (m_anchoredItemStart == index)
                {
                    if (m_anchoredItemEnd < index)
                        m_anchoredItemStart = index - 1;
                    else if (m_anchoredItemEnd > index)
                        m_anchoredItemStart = index + 1;
                    else
                        m_anchoredItemStart = m_anchoredItemEnd;
                }

                // If we just removed the anchor end then update it accordingly
                if (m_anchoredItemEnd == index)
                {
                    if (m_anchoredItemStart < index)
                        m_anchoredItemEnd = index - 1;
                    else if (m_anchoredItemStart > index)
                        m_anchoredItemEnd = index + 1;
                    else
                        m_anchoredItemEnd = m_anchoredItemStart;
                }
            }
            else
            {
                SelectedIndices.Add(index);
                m_anchoredItemStart = index;
                m_anchoredItemEnd = index;
            }

            SelectedIndexChanged?.Invoke(this);

            Invalidate();
        }

        public void SelectItems(int startRange, int endRange)
        {
            SelectedIndices.Clear();

            if (startRange == endRange)
                SelectedIndices.Add(startRange);

            if (startRange < endRange)
            {
                for (int i = startRange; i <= endRange; i++)
                    SelectedIndices.Add(i);
            }
            else if (startRange > endRange)
            {
                for (int i = startRange; i >= endRange; i--)
                    SelectedIndices.Add(i);
            }

            SelectedIndexChanged?.Invoke(this);

            Invalidate();
        }

        public void Clear()
        {
            Items.Clear();
        }

        private void SelectAnchoredRange(int index)
        {
            m_anchoredItemEnd = index;
            SelectItems(m_anchoredItemStart, index);
        }

        private void UpdateListBox()
        {
            using (Graphics graphics = CreateGraphics())
            {
                int length = Items.Count - 1;
                for (int i = 0; i <= length; i++)
                {
                    T item = Items[i];
                    UpdateItemPosition(item, i);
                }
            }

            UpdateContentSize();
        }

        protected virtual void UpdateItemPosition(T item, int index)
        {
            throw new NotImplementedException();
        }

        private void UpdateContentSize()
        {
            int width, height;
            if (m_layout == ListBaseLayout.Vertical)
            {
                width = Viewport.Width;
                height = Items.Count * ItemSize;
            }
            else
            {
                width = Items.Count * ItemSize;
                height = Viewport.Height;
            }

            if (ContentSize.Width != width || height != ContentSize.Height)
            {
                ContentSize = new Size(width, height);
                Invalidate();
            }
        }

        public void EnsureVisible()
        {
            if (SelectedIndices.Count != 1)
                return;

            if (m_layout == ListBaseLayout.Vertical)
            {
                int top = SelectedIndices[0] * ItemSize;
                if (top < Viewport.Top)
                    VScrollTo(top);

                int bottom = top + ItemSize;
                if (bottom > Viewport.Bottom)
                    VScrollTo(bottom - Viewport.Height);
            }
            else
            {
                int left = SelectedIndices[0] * ItemSize;
                if (left < Viewport.Left)
                    HScrollTo(left);

                int right = left + ItemSize;
                if (right > Viewport.Right)
                    HScrollTo(right - Viewport.Width);
            }
        }

        protected void BeginUpdate()
        {
            m_updating = true;
        }

        protected void EndUpdate()
        {
            m_updating = false;
            UpdateContentSize();
        }

        protected IEnumerable<int> GetIndexesInView()
        {
            if (m_layout == ListBaseLayout.Vertical)
            {
                int top = (Viewport.Top / ItemSize) - 1;
                if (top < 0)
                    top = 0;

                int bottom = ((Viewport.Top + Viewport.Height) / ItemSize) + 1;
                if (bottom > Items.Count)
                    bottom = Items.Count;

                return Enumerable.Range(top, bottom - top);
            }
            else
            {
                int left = (Viewport.Left / ItemSize) - 1;
                if (left < 0)
                    left = 0;

                int right = ((Viewport.Left + Viewport.Width) / ItemSize) + 1;
                if (right > Items.Count)
                    right = Items.Count;

                return Enumerable.Range(left, right - left);
            }
        }

        protected override void PaintContent(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);

            if (args.Button != MouseButtons.Left && args.Button != MouseButtons.Right)
                return;

            int index = IndexFromPoint(args.Location);
            if (index != -1)
            {
                if (MultiSelect && ModifierKeys == Keys.Shift)
                    SelectAnchoredRange(index);
                else if (MultiSelect && ModifierKeys == Keys.Control)
                    ToggleItem(index);
                else
                    SelectItem(index);
            }
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            if (Items.Count == 0)
                return;

            Keys code = args.KeyCode;

            if (code != Keys.Up && code != Keys.Down && code != Keys.Left && code != Keys.Right)
                return;

            if (MultiSelect && ModifierKeys == Keys.Shift)
            {
                if (code == Keys.Up || code == Keys.Left)
                {
                    if (m_anchoredItemEnd - 1 >= 0)
                    {
                        SelectAnchoredRange(m_anchoredItemEnd - 1);
                        EnsureVisible();
                    }
                }
                else if (code == Keys.Down || code == Keys.Right)
                {
                    if (m_anchoredItemEnd + 1 <= Items.Count - 1)
                        SelectAnchoredRange(m_anchoredItemEnd + 1);
                }
            }
            else
            {
                if (code == Keys.Up || code == Keys.Left)
                {
                    if (m_anchoredItemEnd - 1 >= 0)
                        SelectItem(m_anchoredItemEnd - 1);
                }
                else if (code == Keys.Down || code == Keys.Right)
                {
                    if (m_anchoredItemEnd + 1 <= Items.Count - 1)
                        SelectItem(m_anchoredItemEnd + 1);
                }
            }

            EnsureVisible();
        }

        private void ItemsCollectionChanged_Handler(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (m_updating)
                return;

            if (args.NewItems != null)
            {
                // Find the starting index of the new item list and update anything past that
                if (args.NewStartingIndex < (Items.Count - 1))
                {
                    for (int i = args.NewStartingIndex; i <= Items.Count - 1; i++)
                        UpdateItemPosition(Items[i], i);
                }
            }

            if (args.OldItems != null)
            {
                // Find the starting index of the old item list and update anything past that
                if (args.OldStartingIndex < (Items.Count - 1))
                {
                    for (int i = args.OldStartingIndex; i <= Items.Count - 1; i++)
                        UpdateItemPosition(Items[i], i);
                }
            }

            if (Items.Count == 0 && SelectedIndices.Count > 0)
            {
                SelectedIndices.Clear();
                SelectedIndexChanged?.Invoke(this);
            }

            UpdateContentSize();
        }
    }
}
