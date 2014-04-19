#region Licence
/**
* Copyright (C) 2014 <https://github.com/Mignari/ItemEditor>
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

using ImageSimilarity;
using ItemEditor.Diagnostics;
using ItemEditor.Dialogs;
using ItemEditor.Helpers;
using ItemEditor.Host;
using ItemEditor.Settings;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ItemEditor
{
	public partial class MainForm : Form
	{
		#region Private Properties

		public const string applicationName = "ItemEditor";
		public const string versionString = "0.3.1";
		private const int itemMargin = 5;
		private const int spritePixels = 32;

		private bool showOnlyMismatchedItems = false;
		private bool showOnlyDeprecatedItems = false;
		private TextBoxTraceListener textBoxListener;

		private ServerItemList items = new ServerItemList();
		private ServerItem currentItem = null;

		//The plugin that is used to compare, sync and display sprite/dat data
		public Plugin currentPlugin;
		public UInt32 currentOtbVersion = 0;
		string currentOtbFullPath = "";

		//The original plugin that was used to open the currently loaded OTB
		public Plugin previousPlugin;

		private bool loaded = false;
		private bool saved = true;
		private bool isTemp = false;

		#endregion

		#region Public Properties

		public bool Loaded
		{
			get { return this.loaded; }
		}

		public bool Saved
		{
			get { return this.saved; }
		}

		public UInt16 MinItemId
		{
			get { return items.minId; }
		}

		public UInt16 MaxItemId
		{
			get { return items.maxId; }
		}

		#endregion

		#region Construtor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region General Methods

		public void Open(string fileName = null)
		{
			if (String.IsNullOrEmpty(fileName))
			{
				FileDialog dialog = new OpenFileDialog();
				dialog.Filter = "OTB files (*.otb)|*.otb|All files (*.*)|*.*";
				dialog.Title = "Open OTB File";

				if (dialog.ShowDialog() != DialogResult.OK || dialog.FileName.Length == 0)
				{
					return;
				}

				fileName = dialog.FileName;
				isTemp = false;
				saved = true;
			}

			if (this.Loaded)
			{
				this.Clear();
			}

			if (Otb.Open(fileName, ref items))
			{
				currentOtbFullPath = fileName;
				currentOtbVersion = items.dwMinorVersion;

				//try find a plugin that can handle this version of otb
				currentPlugin = Program.plugins.AvailablePlugins.Find(currentOtbVersion);
				if (currentPlugin == null)
				{
					items.Clear();
					MessageBox.Show(String.Format("Could not find a plugin that could handle client version {0}", currentOtbVersion));
					return;
				}

				if (!LoadClient(currentPlugin, currentOtbVersion))
				{
					currentPlugin = null;
					items.Clear();
					return;
				}

				this.BuildItemsListBox();
				this.fileSaveAsMenuItem.Enabled = true;
				this.fileSaveMenuItem.Enabled = true;
				this.editCreateItemMenuItem.Enabled = true;
				this.editFindItemMenuItem.Enabled = true;
				this.viewShowOnlyMismatchedMenuItem.Enabled = true;
				this.viewShowDecaptedItemsMenuItem.Enabled = true;
				this.viewUpdateItemsListMenuItem.Enabled = true;
				this.toolsUpdateVersionMenuItem.Enabled = true;
				this.toolsReloadItemAttributesMenuItem.Enabled = true;
				this.toolStripSaveButton.Enabled = true;
				this.toolStripSaveAsButton.Enabled = true;
				this.toolStripFindItemButton.Enabled = true;
				this.itemsListBox.Enabled = true;
				this.loaded = true;
			}
		}

		public void Save()
		{
			if (isTemp)
			{
				this.SaveAs();
				return;
			}

			if (!this.Loaded)
			{
				return;
			}

			try
			{
				Otb.Save(currentOtbFullPath, ref items);
				saved = true;
				Trace.WriteLine("Saved.");
			}
			catch (UnauthorizedAccessException exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public void SaveAs()
		{
			if (!this.Loaded)
			{
				return;
			}

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "OTB files (*.otb)|*.otb|All files (*.*)|*.*";
			dialog.Title = "Save OTB File";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				if (dialog.FileName.Length == 0)
				{
					return;
				}

				try
				{
					Otb.Save(dialog.FileName, ref items);
					currentOtbFullPath = dialog.FileName;
					Trace.WriteLine("Saved.");
					this.isTemp = false;
					this.saved = true;
				}
				catch (UnauthorizedAccessException exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		public bool SelectItem(UInt16 sid)
		{
			if (!this.Loaded)
			{
				return false;
			}

			if (sid >= items.minId && sid <= items.maxId)
			{
				ServerItem item = items.Find(i => i.id == sid);
				if (item != null)
				{
					return SelectItem(item);
				}
			}
			return false;
		}

		public bool SelectItem(ServerItem item)
		{
			if (!this.Loaded)
			{
				return false;
			}

			if (currentItem == item)
			{
				return true;
			}

			int index;
			if (item == null || (index = itemsListBox.Items.IndexOf(item)) == -1)
			{
				this.ResetControls();
				return false;
			}

			EditItem(item);
			editDuplicateItemMenuItem.Enabled = true;
			editReloadItemMenuItem.Enabled = true;
			optionsGroupBox.Enabled = true;
			appearanceGroupBox.Enabled = true;
			itemsListBox.SelectedIndex = index;
			return true;
		}

		public ServerItem AddNewItem()
		{
			if (!this.Loaded)
			{
				return null;
			}

			ServerItem item = this.CreateItem();
			item.id = (UInt16)(items.maxId + 1);
			items.Add(item);
			itemsListBox.Items.Add(item);
			SelectItem(item);
			this.itemsCountLabel.Text = itemsListBox.Items.Count + " Items";
			Trace.WriteLine(String.Format("Create item id {0}", item.id));
			return item;
		}

		public bool DuplicateItem(Item item)
		{
			return this.DuplicateItem(item as ServerItem);
		}

		public bool DuplicateItem(ServerItem item)
		{
			if (!this.Loaded || item == null)
			{
				return false;
			}

			ServerItem copyItem = this.CopyItem(item);
			copyItem.id = (UInt16)(items.maxId + 1);
			items.Add(copyItem);
			itemsListBox.Items.Add(copyItem);
			SelectItem(copyItem);
			this.itemsCountLabel.Text = itemsListBox.Items.Count + " Items";

			Trace.WriteLine(String.Format("Duplicate item id {0} to new item id {1}", item.id, copyItem.id));
			return true;
		}

		public void CreateEmptyOTB(string filePath, SupportedClient client, bool isTemp = true)
		{
			ServerItem item = new ServerItem();
			item.SpriteHash = new byte[16];
			item.spriteId = 100;
			item.id = 100;

			ServerItemList items = new ServerItemList();
			items.dwMajorVersion = 3;
			items.dwMinorVersion = client.OtbVersion;
			items.dwBuildNumber = 1;
			items.clientVersion = client.Version;
			items.Add(item);

			if (!File.Exists(filePath))
			{
				using (File.Create(filePath)) { }
			}

			if (Otb.Save(filePath, ref items))
			{
				this.Open(filePath);
				this.isTemp = isTemp;
				this.saved = !isTemp;
			}
		}

		public void Clear()
		{
			this.SelectItem(null);
			this.currentItem = null;
			this.currentPlugin = null;
			this.previousPlugin = null;
			this.currentOtbVersion = 0;
			this.currentOtbFullPath = "";
			this.items.Clear();
			this.itemsListBox.Items.Clear();
			this.itemsListBox.Enabled = false;
			this.fileSaveMenuItem.Enabled = false;
			this.fileSaveAsMenuItem.Enabled = false;
			this.editCreateItemMenuItem.Enabled = false;
			this.editDuplicateItemMenuItem.Enabled = false;
			this.editReloadItemMenuItem.Enabled = false;
			this.editFindItemMenuItem.Enabled = false;
			this.viewShowOnlyMismatchedMenuItem.Enabled = false;
			this.viewShowDecaptedItemsMenuItem.Enabled = false;
			this.viewUpdateItemsListMenuItem.Enabled = false;
			this.toolsUpdateVersionMenuItem.Enabled = false;
			this.toolsReloadItemAttributesMenuItem.Enabled = false;
			this.toolStripSaveButton.Enabled = false;
			this.toolStripSaveAsButton.Enabled = false;
			this.toolStripFindItemButton.Enabled = false;
			this.loaded = false;
			this.textBoxListener.Clear();
		}

		private Bitmap GetBitmap(ClientItem clientItem)
		{
			int Width = spritePixels;
			int Height = spritePixels;

			if (clientItem.width > 1 || clientItem.height > 1)
			{
				Width = spritePixels * 2;
				Height = spritePixels * 2;
			}

			Bitmap canvas = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
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
						Bitmap bmp = ImageUtils.getBitmap(clientItem.GetRGBData(frameIndex), PixelFormat.Format24bppRgb, spritePixels, spritePixels);

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
			return canvas;
		}

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
						Bitmap bmp = ImageUtils.getBitmap(clientItem.GetRGBData(frameIndex), PixelFormat.Format24bppRgb, spritePixels, spritePixels);

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

		private void DrawSprite(PictureBox picturBox, ClientItem clientItem)
		{
			Bitmap canvas = new Bitmap(64, 64, PixelFormat.Format24bppRgb);
			using (Graphics g = Graphics.FromImage(canvas))
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb(0x11, 0x11, 0x11)), 0, 0, canvas.Width, canvas.Height);
				g.Save();
			}

			DrawSprite(ref canvas, clientItem);

			Bitmap newImage = new Bitmap(64, 64, PixelFormat.Format24bppRgb);
			using (Graphics g = Graphics.FromImage(newImage))
			{
				g.DrawImage(canvas, new Point((canvas.Width > spritePixels ? 0 : spritePixels), (canvas.Height > spritePixels ? 0 : spritePixels)));
				g.Save();
			}

			newImage.MakeTransparent(Color.FromArgb(0x11, 0x11, 0x11));
			picturBox.Image = newImage;
		}

		private void BuildItemsListBox()
		{
			ClearItemsListBox();
			ResetControls();

			// Make the itemsListBox owner drawn.
			this.itemsListBox.DrawMode = DrawMode.OwnerDrawVariable;

			this.loadingItemsProgressBar.Visible = true;
			this.loadingItemsProgressBar.Minimum = 0;
			this.loadingItemsProgressBar.Maximum = items.Count + 1;
			ushort index = 0;

			foreach (ServerItem item in items)
			{
				if (showOnlyMismatchedItems && CompareItem(item, true))
				{
					continue;
				}

				if ((showOnlyDeprecatedItems && item.type != ItemType.Deprecated) ||
					(!showOnlyDeprecatedItems && item.type == ItemType.Deprecated))
				{
					continue;
				}

				itemsListBox.Items.Add(item);
				this.loadingItemsProgressBar.Value = index;
				index++;
			}

			this.loadingItemsProgressBar.Visible = false;
			this.itemsCountLabel.Text = itemsListBox.Items.Count + " Items";
		}

		private void ClearItemsListBox()
		{
			itemsListBox.Items.Clear();
		}

		private bool CompareItem(ServerItem item, bool compareHash)
		{
			if (item.type == ItemType.Deprecated)
			{
				return true;
			}

			ClientItem clientItem;
			if (currentPlugin.Instance.Items.TryGetValue(item.spriteId, out clientItem))
			{
				if (compareHash && !Utils.ByteArrayCompare(item.SpriteHash, clientItem.SpriteHash))
				{
					return false;
				}

				return item.IsEqual(clientItem);
			}

			return false;
		}

		private void ReloadItems()
		{
			foreach (ServerItem item in items)
			{
				if (!CompareItem(item, true))
				{
					ReloadItem(item);
				}
			}
		}

		private void ReloadItem(ServerItem item)
		{
			if (!Loaded || item == null)
			{
				return;
			}

			//to avoid problems with events
			ServerItem tmpItem = currentItem;
			currentItem = null;

			ClientItem clientItem;
			if (currentPlugin.Instance.Items.TryGetValue(item.spriteId, out clientItem))
			{
				Trace.WriteLine(String.Format("Reloading item id: {0}.", item.id));

				UInt16 tmpId = item.id;
				item.itemImpl = (ItemImpl)clientItem.itemImpl.Clone();
				item.id = tmpId;
				Buffer.BlockCopy(clientItem.SpriteHash, 0, item.SpriteHash, 0, clientItem.SpriteHash.Length);

				currentItem = tmpItem;
			}
		}

		private void ReloadSelectedItem()
		{
			ServerItem item = currentItem;
			ReloadItem(item);
			SelectItem(null);
			SelectItem(item);
		}

		private bool EditItem(ServerItem item)
		{
			currentItem = null;
			ResetDataBindings(this);
			ResetToolTips();

			if (item == null)
			{
				return false;
			}

			ClientItem clientItem;
			if (!currentPlugin.Instance.Items.TryGetValue(item.spriteId, out clientItem))
			{
				return false;
			}

			DrawSprite(pictureBox, clientItem);
			if (!item.isCustomCreated && item.SpriteHash != null && clientItem.SpriteHash != null)
			{
				pictureBox.BackColor = ((Utils.ByteArrayCompare(item.SpriteHash, clientItem.SpriteHash) ? Color.White : Color.Red));
			}

			typeCombo.Text = item.type.ToString();
			typeCombo.ForeColor = (item.type == clientItem.type ? Color.Black : Color.Red);

			//
			serverIdLbl.DataBindings.Add("Text", item, "id");
			clientIdUpDown.Minimum = items.minId;
			clientIdUpDown.Maximum = (currentPlugin.Instance.Items.Count + items.minId) - 1;
			clientIdUpDown.DataBindings.Add("Value", clientItem, "id");

			// Attributes
			AddBinding(unpassableCheck, "Checked", item, "isUnpassable", item.isUnpassable, clientItem.isUnpassable);
			AddBinding(blockMissilesCheck, "Checked", item, "blockMissiles", item.blockMissiles, clientItem.blockMissiles);
			AddBinding(blockPathfinderCheck, "Checked", item, "blockPathfinder", item.blockPathfinder, clientItem.blockPathfinder);
			AddBinding(moveableCheck, "Checked", item, "isMoveable", item.isMoveable, clientItem.isMoveable);
			AddBinding(hasElevationCheck, "Checked", item, "hasElevation", item.hasElevation, clientItem.hasElevation);
			AddBinding(pickupableCheck, "Checked", item, "isPickupable", item.isPickupable, clientItem.isPickupable);
			AddBinding(hangableCheck, "Checked", item, "isHangable", item.isHangable, clientItem.isHangable);
			AddBinding(useableCheck, "Checked", item, "multiUse", item.multiUse, clientItem.multiUse);
			AddBinding(rotatableCheck, "Checked", item, "isRotatable", item.isRotatable, clientItem.isRotatable);
			AddBinding(stackableCheck, "Checked", item, "isStackable", item.isStackable, clientItem.isStackable);
			AddBinding(verticalCheck, "Checked", item, "isVertical", item.isVertical, clientItem.isVertical);
			AddBinding(fullGroundCheck, "Checked", item, "fullGround", item.fullGround, clientItem.fullGround);
			AddBinding(horizontalCheck, "Checked", item, "isHorizontal", item.isHorizontal, clientItem.isHorizontal);
			AddBinding(alwaysOnTopCheck, "Checked", item, "alwaysOnTop", item.alwaysOnTop, clientItem.alwaysOnTop);
			AddBinding(readableCheck, "Checked", item, "isReadable", item.isReadable, clientItem.isReadable);
			AddBinding(ignoreLookCheck, "Checked", item, "ignoreLook", item.ignoreLook, clientItem.ignoreLook);
			AddBinding(groundSpeedText, "Text", item, "groundSpeed", item.groundSpeed, clientItem.groundSpeed, true);
			AddBinding(topOrderText, "Text", item, "alwaysOnTopOrder", item.alwaysOnTopOrder, clientItem.alwaysOnTopOrder, true);
			AddBinding(lightLevelText, "Text", item, "lightLevel", item.lightLevel, clientItem.lightLevel, true);
			AddBinding(lightColorText, "Text", item, "lightColor", item.lightColor, clientItem.lightColor, true);
			AddBinding(maxReadCharsText, "Text", item, "maxReadChars", item.maxReadChars, clientItem.maxReadChars, true);
			AddBinding(maxReadWriteCharsText, "Text", item, "maxReadWriteChars", item.maxReadWriteChars, clientItem.maxReadWriteChars, true);
			AddBinding(minimapColorText, "Text", item, "minimapColor", item.minimapColor, clientItem.minimapColor, true);
			AddBinding(wareIdText, "Text", item, "tradeAs", item.tradeAs, clientItem.tradeAs, true);
			AddBinding(nameText, "Text", item, "name", item.name, clientItem.name, true);

			candidatesButton.Enabled = false;
			for (int i = 0; i < candidatesTableLayoutPanel.ColumnCount; ++i)
			{
				PictureBox box = (PictureBox)candidatesTableLayoutPanel.GetControlFromPosition(i, 0);
				box.Image = null;
			}
			
			if (previousPlugin != null)
			{
				ClientItem prevClientItem;
				if (previousPlugin.Instance.Items.TryGetValue(item.prevSpriteId, out prevClientItem))
				{
					DrawSprite(previousPictureBox, prevClientItem);
					if (prevClientItem.SpriteSignature != null)
					{
						//Sprite does not match, use the sprite signature to find possible candidates
						ShowSpriteCandidates(prevClientItem);
					}
				}
				else
				{
					previousPictureBox.Image = null;
				}
			}

			currentItem = item;
			return true;
		}

		private void AddBinding(Control control, string propertyName, object dataSource, string dataMember, object value, object clientValue, bool setToolTip = false)
		{
			bool equals = value.Equals(clientValue);
			control.DataBindings.Add(propertyName, dataSource, dataMember);
			control.ForeColor = equals ? Color.Black : Color.Red;

			if (!equals && setToolTip)
			{
				this.toolTip.SetToolTip(control, clientValue.ToString());
			}
		}

		private void ResetDataBindings(Control control)
		{
			control.DataBindings.Clear();
			if (control.HasChildren)
			{
				foreach (Control childControl in control.Controls)
				{
					ResetDataBindings(childControl);
				}
			}
		}

		private void ResetToolTips()
		{
			toolTip.RemoveAll();
		}

		private void ResetControls()
		{
			currentItem = null;
			editDuplicateItemMenuItem.Enabled = false;
			optionsGroupBox.Enabled = false;
			appearanceGroupBox.Enabled = false;
			pictureBox.Image = null;
			pictureBox.BackColor = Color.White;
			previousPictureBox.Image = null;
			previousPictureBox.BackColor = Color.White;
			clientIdUpDown.Value = clientIdUpDown.Minimum;
			serverIdLbl.Text = "0";
			typeCombo.Text = "";
			typeCombo.ForeColor = Color.Black;
			editDuplicateItemMenuItem.Enabled = false;
			candidatesButton.Enabled = false;

			foreach (Control control in optionsGroupBox.Controls)
			{
				if (control is CheckBox)
				{
					((CheckBox)control).Checked = false;
					control.ForeColor = Color.Black;
				}
				else if (control is TextBox)
				{
					((TextBox)control).Text = "";
					control.ForeColor = Color.Black;
				}
			}
		}

		private void ShowSpriteCandidates(ClientItem clientItem)
		{
			candidatesButton.Enabled = true;

			//list with the top 5 results
			List<KeyValuePair<double, ServerItem>> signatureList = new List<KeyValuePair<double, ServerItem>>();

			foreach (ServerItem cmpItem in items)
			{
				if (cmpItem.type == ItemType.Deprecated)
				{
					continue;
				}

				ClientItem cmpClientItem;
				if (!currentPlugin.Instance.Items.TryGetValue(cmpItem.spriteId, out cmpClientItem))
				{
					continue;
				}

				double similarity = ImageUtils.CompareSignature(clientItem.SpriteSignature, cmpClientItem.SpriteSignature);

				foreach (KeyValuePair<double, ServerItem> kvp in signatureList)
				{
					if (similarity < kvp.Key)
					{
						//TODO: Use isEqual aswell to match against attributes.
						signatureList.Remove(kvp);
						break;
					}
				}

				if (signatureList.Count < 5)
				{
					KeyValuePair<double, ServerItem> kvp = new KeyValuePair<double, ServerItem>(similarity, cmpItem);
					signatureList.Add(kvp);
				}
			}

			signatureList.Sort(
				delegate(KeyValuePair<double, ServerItem> item1, KeyValuePair<double, ServerItem> item2)
				{
					return item1.Key.CompareTo(item2.Key);
				});

			//those with lowest value are the closest match
			int index = 0;
			foreach (KeyValuePair<double, ServerItem> kvp in signatureList)
			{
				PictureBox box = (PictureBox)candidatesTableLayoutPanel.GetControlFromPosition(index, 0);
				toolTip.SetToolTip(box, kvp.Value.spriteId.ToString());
				box.Tag = kvp.Value;

				ClientItem spriteCandidateItem;
				if (currentPlugin.Instance.Items.TryGetValue(kvp.Value.spriteId, out spriteCandidateItem))
				{
					DrawSprite(box, spriteCandidateItem);
				}
				++index;
			}
		}

		private ServerItem CreateItem(Item item = null)
		{
			//create a new otb item
			ServerItem newItem = new ServerItem(item);
			newItem.id = (UInt16)(items.maxId + 1);
			newItem.SpriteHash = new byte[16];

			if (item != null)
			{
				newItem.spriteId = item.id;
				Buffer.BlockCopy(item.SpriteHash, 0, newItem.SpriteHash, 0, newItem.SpriteHash.Length);
			}
			else
			{
				newItem.spriteId = items.minId;
				newItem.isCustomCreated = true;
			}

			return newItem;
		}

		private ServerItem CopyItem(ServerItem item)
		{
			if (item == null)
			{
				return null;
			}

			ServerItem copy = new ServerItem(item);
			copy.SpriteHash = new byte[16];
			copy.spriteId = item.spriteId;
			Buffer.BlockCopy(item.SpriteHash, 0, copy.SpriteHash, 0, copy.SpriteHash.Length);
			return copy;
		}

		private bool LoadClient(Plugin plugin, UInt32 otbVersion)
		{
			SupportedClient client = plugin.Instance.SupportedClients.Find(
				delegate(SupportedClient sc)
				{
					return sc.OtbVersion == otbVersion;
				});

			if (client == null)
			{
				MessageBox.Show("The selected plugin does not support this version.");
				return false;
			}

			UInt32 datSignature = Program.preferences.DatSignature;
			UInt32 sprSignature = Program.preferences.SprSignature;

			if (client.DatSignature != datSignature || client.SprSignature != sprSignature)
			{
				string message;
				if (datSignature == 0 || sprSignature == 0)
					message = "No client is selected. Please navigate to the client folder.";
				else
					message = String.Format("The selected client is not compatible with this otb. Please navigate to the folder of a compatible client {0}.", client.Version);
				MessageBox.Show(message);
				PreferencesForm form = new PreferencesForm();
				form.ShowDialog();
				return false;
			}

			string clientFolder = Program.preferences.ClientDirectory;

			if (String.IsNullOrEmpty(clientFolder))
			{
				return false;
			}

			string datPath = Utils.FindClientFile(clientFolder, ".dat");
			string sprPath = Utils.FindClientFile(clientFolder, ".spr");
			bool extended = Program.preferences.Extended;
			bool transparency = Program.preferences.Transparency;

			extended = (extended || client.Version >= 960);

			if (!File.Exists(datPath) || !File.Exists(sprPath))
			{
				MessageBox.Show("Client files not found.");
				return false;
			}

			Trace.WriteLine(String.Format("OTB version {0}.", otbVersion));

			bool result;

			try
			{
				result = plugin.Instance.LoadClient(client, extended, transparency, datPath, sprPath);
			}
			catch (UnauthorizedAccessException error)
			{
				MessageBox.Show(error.Message + " Please run this program as administrator.");
				return false;
			}

			Trace.WriteLine("Loading client files.");
			if (!result)
			{
				MessageBox.Show(String.Format("The plugin could not load dat or spr."));
			}

			items.clientVersion = client.Version;
			Trace.WriteLine(String.Format("Client version {0}.", client.Version));
			return result;
		}

		private bool GenerateSpriteSignatures(ref ClientItems items)
		{
			if (items.signatureCalculated)
			{
				return true;
			}

			ProgressForm progress = new ProgressForm();
			progress.StartPosition = FormStartPosition.Manual;
			progress.Location = new Point(Location.X + ((Width - progress.Width) / 2),
										  Location.Y + ((Height - progress.Height) / 2));
			progress.bar.Minimum = 0;
			progress.bar.Maximum = items.Count;
			progress.Show(this);

			foreach (ClientItem clientItem in items.Values)
			{
				Bitmap spriteBmp = GetBitmap(clientItem);
				Bitmap ff2dBmp = Fourier.fft2dRGB(spriteBmp, false);
				clientItem.SpriteSignature = ImageUtils.CalculateEuclideanDistance(ff2dBmp, 1);

				if (progress.bar.Value % 20 == 0)
				{
					Application.DoEvents();
				}
				progress.progressLbl.Text = String.Format("Calculating image signature for item {0}.", clientItem.id);
				++progress.bar.Value;
			}

			items.signatureCalculated = true;
			progress.Close();
			return true;
		}

		private bool CheckSave()
		{
			if (this.Loaded && !this.Saved)
			{
				DialogResult result = MessageBox.Show("Do you want to save the changes?", "Save", MessageBoxButtons.YesNoCancel);

				if (result == DialogResult.Yes)
				{
					this.Save();
				}
				else if (result == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = applicationName;
			typeCombo.DataSource = Enum.GetNames(typeof(ItemType));

			this.candidatesDropDown.Items.Add(new ToolStripControlHost(this.candidatesTableLayoutPanel));

			Trace.Listeners.Clear();
			textBoxListener = new TextBoxTraceListener(outputTextBox);
			Trace.Listeners.Add(textBoxListener);

			SelectItem(null);

			Program.preferences.Load();
			Program.plugins.FindPlugins();
			Sprite.CreateBlankSprite();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CheckSave())
			{
				e.Cancel = true;
			}
		}

		private void fileNewMenuItem_Click(object sender, EventArgs e)
		{
			NewOtbFileForm newOtbForm = new NewOtbFileForm();
			DialogResult result = newOtbForm.ShowDialog();

			if (result == DialogResult.OK)
			{
				this.CreateEmptyOTB(newOtbForm.FilePath, newOtbForm.SelectedClient);
			}
		}

		private void fileOpenMenuItem_Click(object sender, EventArgs e)
		{
			this.Open();
		}

		private void fileSaveMenuItem_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		private void fileSaveAsMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveAs();
		}

		private void fileExitMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void filePreferencesMenuItem_Click(object sender, EventArgs e)
		{
			PreferencesForm form = new PreferencesForm();
			form.ShowDialog();
		}

		private void itemsListBoxContextMenu_Opening(object sender, CancelEventArgs e)
		{
			this.itemsListBoxContextMenu.Items.Clear();
			if (this.Loaded)
			{
				this.itemsListBoxContextMenu.Items.Add("Duplicate");
				this.itemsListBoxContextMenu.Items.Add("Reload");
			}
		}

		private void itemsListBoxContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripItem menuItem = e.ClickedItem;
			switch (menuItem.Text)
			{
				case "Duplicate":
					DuplicateItem(currentItem);
					break;
				case "Reload":
					ReloadSelectedItem();
					break;
			}
		}

		private void itemsListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			ListBox list = sender as ListBox;

			if (e.Index == -1 || e.Index >= list.Items.Count)
			{
				return;
			}

			ServerItem item = (ServerItem)list.Items[e.Index];

			e.DrawBackground();
			e.Graphics.DrawRectangle(Pens.Gray, e.Bounds); // Border.

			Brush brush;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				brush = SystemBrushes.HighlightText;
			}
			else
			{
				brush = new SolidBrush(e.ForeColor);
			}

			// Find the area in which to put the text and draw.
			int x = e.Bounds.Left + spritePixels + 3 * itemMargin;
			int y = e.Bounds.Top + itemMargin * 2;
			int width = e.Bounds.Right - itemMargin - x;
			int height = e.Bounds.Bottom - itemMargin - y;
			Rectangle layoutRect = new Rectangle(x, y, width, height);
			string text = item.id.ToString();
			e.Graphics.DrawString(text, this.Font, brush, layoutRect);

			Rectangle destRect = new Rectangle(e.Bounds.Left + itemMargin, e.Bounds.Top + itemMargin, spritePixels, spritePixels);

			ClientItem clientItem;
			if (currentPlugin.Instance.Items.TryGetValue(item.spriteId, out clientItem))
			{
				Bitmap bitmap = GetSpriteBitmap(clientItem);
				if (bitmap != null)
				{
					Rectangle sourceRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
					e.Graphics.DrawImage(bitmap, destRect, sourceRect, GraphicsUnit.Pixel);
				}
			}

			e.Graphics.DrawRectangle(new Pen(brush), destRect);
			e.DrawFocusRectangle();
		}

		private void itemsListBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = (int)(spritePixels + 2 * itemMargin);
		}

		private void itemsListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SelectItem(itemsListBox.SelectedItem as ServerItem);
		}

		private void itemsListBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				itemsListBox.SelectedIndex = itemsListBox.IndexFromPoint(e.Location);
				itemsListBoxContextMenu.Show();
			}
		}

		private void textBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void toolsCompareOtbFilesMenuItem_Click(object sender, EventArgs e)
		{
			CompareOtbForm form = new CompareOtbForm();
			form.ShowDialog();
		}

		private void showOnlyUnmatchedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showOnlyMismatchedItems = !showOnlyMismatchedItems;
			BuildItemsListBox();
		}

		private void viewShowDecaptedItemsMenuItem_Click(object sender, EventArgs e)
		{
			showOnlyDeprecatedItems = !showOnlyDeprecatedItems;
			BuildItemsListBox();
		}

		private void toolsReloadItemAttributesMenuItem_Click(object sender, EventArgs e)
		{
			ReloadItems();
			EditItem(currentItem);
			BuildItemsListBox();
		}

		private void toolsUpdateVersionMenuItem_Click(object sender, EventArgs e)
		{
			UpdateForm form = new UpdateForm();
			form.mainForm = this;

			DialogResult result = form.ShowDialog();
			if (result == DialogResult.OK)
			{
				//Update OTB
				Plugin updatePlugin = form.selectedPlugin;
				SupportedClient updateClient = form.updateClient;

				if (updatePlugin == null)
				{
					return;
				}

				if (!LoadClient(updatePlugin, updateClient.OtbVersion))
				{
					return;
				}

				UpdateSettingsForm updateSettingsForm = new UpdateSettingsForm();
				result = updateSettingsForm.ShowDialog();
				if (result != DialogResult.OK)
				{
					return;
				}

				if (updateSettingsForm.generateSignatureCheck.Checked)
				{
					//Calculate an image signature using fourier transformation and calculate a signature we can
					//use to compare it to other images (kinda similar to md5 hash) except this
					//can also be used to find images with some variation.
					ClientItems currentClientItems = currentPlugin.Instance.Items;
					GenerateSpriteSignatures(ref currentClientItems);

					ClientItems updateClientItems = updatePlugin.Instance.Items;
					GenerateSpriteSignatures(ref updateClientItems);
				}

				ClientItems currentItems = currentPlugin.Instance.Items;
				ClientItems updateItems = updatePlugin.Instance.Items;
				List<UInt16> assignedSpriteIdList = new List<UInt16>();

				//store the previous plugin (so we can display previous sprite, and do other comparisions)
				previousPlugin = currentPlugin;

				//update the current plugin the one we are updating to
				currentPlugin = updatePlugin;

				//update version information
				items.clientVersion = updateClient.Version;
				items.dwMinorVersion = updateClient.OtbVersion;
				items.dwBuildNumber = items.dwBuildNumber + 1;
				currentOtbVersion = items.dwMinorVersion;

				//Most items does have the same sprite after an update, so lets try that first
				UInt32 foundItemCounter = 0;
				foreach (ServerItem item in items)
				{
					item.spriteAssigned = false;

					if (item.type == ItemType.Deprecated)
					{
						continue;
					}

					ClientItem updateClientItem;
					if (updateItems.TryGetValue(item.spriteId, out updateClientItem))
					{
						bool compareResult = updateClientItem.IsEqual(item);

						if (Utils.ByteArrayCompare(updateClientItem.SpriteHash, item.SpriteHash))
						{
							if (compareResult)
							{
								item.prevSpriteId = item.spriteId;
								item.spriteId = updateClientItem.id;
								item.spriteAssigned = true;

								assignedSpriteIdList.Add(updateClientItem.id);
								++foundItemCounter;

								//Trace.WriteLine(String.Format("Match found id: {0}, clientid: {1}", item.otb.id, item.dat.id));
							}
							else
							{
								//Sprite matches, but not the other attributes.
								//Trace.WriteLine(String.Format("Attribute changes found id: {0}.", item.id));
							}
						}
					}
				}

				if (updateSettingsForm.reassignUnmatchedSpritesCheck.Checked)
				{
					foreach (Item updateItem in updateItems.Values)
					{
						foreach (ServerItem item in items)
						{
							if (item.type == ItemType.Deprecated)
							{
								continue;
							}

							if (item.spriteAssigned)
							{
								continue;
							}

							if (Utils.ByteArrayCompare(updateItem.SpriteHash, item.SpriteHash))
							{
								if (updateItem.IsEqual(item))
								{
									if (updateItem.id != item.spriteId)
									{
										Trace.WriteLine(String.Format("New sprite found id: {0}, old: {1}, new: {2}.", item.id, item.spriteId, updateItem.id));
									}

									item.prevSpriteId = item.spriteId;
									item.spriteId = updateItem.id;
									item.spriteAssigned = true;

									assignedSpriteIdList.Add(updateItem.id);
									++foundItemCounter;
									break;
								}
							}
						}
					}
				}

				Trace.WriteLine(String.Format("Found {0} of {1}.", foundItemCounter, items.maxId));

				if (updateSettingsForm.reloadItemAttributesCheck.Checked)
				{
					UInt32 reloadedItemCounter = 0;
					foreach (ServerItem item in items)
					{
						if (item.type == ItemType.Deprecated)
						{
							continue;
						}

						//implicit assigned
						item.prevSpriteId = item.spriteId;
						item.spriteAssigned = true;

						if (!assignedSpriteIdList.Contains(item.spriteId))
						{
							assignedSpriteIdList.Add(item.spriteId);
						}

						if (!CompareItem(item, true))
						{
							//sync with dat info
							ReloadItem(item);
							++reloadedItemCounter;
						}
					}

					Trace.WriteLine(String.Format("Reloaded {0} of {1} items.", reloadedItemCounter, items.maxId));
				}

				if (updateSettingsForm.createNewItemsCheck.Checked)
				{
					UInt32 newItemCounter = 0;
					foreach (Item updateItem in updateItems.Values)
					{
						if (!assignedSpriteIdList.Contains(updateItem.id))
						{
							++newItemCounter;
							ServerItem newItem = CreateItem(updateItem);
							items.Add(newItem);
							Trace.WriteLine(String.Format("Creating item id {0}", newItem.id));
						}
					}

					Trace.WriteLine(String.Format("Created {0} new items.", newItemCounter));
				}

				//done
				BuildItemsListBox();
			}
		}

		private void candidatePictureBox_Click(object sender, EventArgs e)
		{
			if (currentItem != null)
			{
				PictureBox box = (PictureBox)sender;
				if (box.Tag is ServerItem)
				{
					ServerItem newItem = (ServerItem)box.Tag;

					ClientItem clientItem;
					if (!currentPlugin.Instance.Items.TryGetValue(newItem.spriteId, out clientItem))
					{
						return;
					}

					if (!clientItem.IsEqual(currentItem))
					{
						DialogResult result = MessageBox.Show(
							"The item attributes does not match the current information, would you like to continue anyway?",
							"Item attributes does not match",
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Question,
							MessageBoxDefaultButton.Button1);

						if (result != DialogResult.Yes)
						{
							return;
						}
					}

					currentItem.prevSpriteId = currentItem.spriteId;
					currentItem.spriteId = clientItem.id;
					EditItem(currentItem);
				}
			}
		}

		private void clientIdUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (currentItem != null)
			{
				ClientItem newClientItem;
				if (currentPlugin.Instance.Items.TryGetValue((UInt16)clientIdUpDown.Value, out newClientItem))
				{
					currentItem.prevSpriteId = currentItem.spriteId;
					currentItem.spriteId = newClientItem.id;
					EditItem(currentItem);
				}
			}
		}

		private void helpAboutMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm form = new AboutForm();
			form.ShowDialog();
		}

		private void editCreateItemMenuItem_Click(object sender, EventArgs e)
		{
			this.AddNewItem();
		}

		private void editDuplicateItemMenuItem_Click(object sender, EventArgs e)
		{
			this.DuplicateItem(currentItem);
		}

		private void editReloadItemMenuItem_Click(object sender, EventArgs e)
		{
			this.ReloadSelectedItem();
		}

		private void typeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (currentItem != null)
			{
				currentItem.type = (ItemType)Enum.Parse(typeof(ItemType), typeCombo.SelectedValue.ToString());
			}
		}

		private void viewUpdateItemsListMenuItem_Click(object sender, EventArgs e)
		{
			this.ResetControls();
			this.BuildItemsListBox();
		}

		private void editFindItemMenuItem_Click(object sender, EventArgs e)
		{
			FindItemForm form = new FindItemForm();
			form.MainForm = this;
			form.Show(this);
		}

		private void candidatesButton_Click(object sender, EventArgs e)
		{
			this.candidatesDropDown.Show(this, new Point(192, 355));
		}

		#endregion
	}
}
