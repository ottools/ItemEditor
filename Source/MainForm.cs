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
using DarkUI.Forms;
using ImageSimilarity;
using ItemEditor.Controls;
using ItemEditor.Diagnostics;
using ItemEditor.Dialogs;
using ItemEditor.Host;
using OTLib.Collections;
using OTLib.OTB;
using OTLib.Server.Items;
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
#endregion

namespace ItemEditor
{
    public partial class MainForm : DarkForm
    {
        #region Private Properties

        public static string ApplicationName { get; private set; }
        public static string ApplicationVersion { get; private set; }

        private bool showOnlyMismatchedItems = false;
        private bool showOnlyDeprecatedItems = false;
        private TextBoxTraceListener textBoxListener;
        private FindItemForm findItemForm;

        #endregion

        #region Construtor

        public MainForm()
        {
            this.InitializeComponent();
            this.InitializeTooltips();
            this.CurrentOtbFullPath = string.Empty;
        }

        #endregion

        #region Events

        public event EventHandler OnCleaned;

        #endregion

        #region Public Properties

        /// <summary>
        /// The original plugin that was used to open the currently loaded OTB
        /// </summary>
        public Plugin PreviousPlugin { get; private set; }

        /// <summary>
        /// The plugin that is used to compare, sync and display sprite/dat data
        /// </summary>
        public Plugin CurrentPlugin { get; private set; }

        public uint CurrentOtbVersion { get; private set; }

        public string CurrentOtbFullPath { get; private set; }

        public ServerItem CurrentServerItem { get; private set; }

        public bool Loaded { get; private set; }

        public bool IsTemporary { get; private set; }

        public bool Saved { get; private set; }

        public ServerItemList ServerItems { get; private set; }

        public ushort MinServerItemId
        {
            get { return this.ServerItems.MinId; }
        }

        public ushort MaxServerItemId
        {
            get { return this.ServerItems.MaxId; }
        }

        public ushort MinClientItemId
        {
            get { return this.CurrentPlugin != null ? this.CurrentPlugin.Instance.MinItemId : (ushort)100; }
        }

        public ushort MaxClientItemId
        {
            get { return this.CurrentPlugin != null ? this.CurrentPlugin.Instance.MaxItemId : (ushort)100; }
        }

        #endregion

        #region Public Methods

        public void Open(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                FileDialog dialog = new OpenFileDialog();
                dialog.Filter = "OTB files (*.otb)|*.otb";
                dialog.Title = "Open OTB File";

                if (dialog.ShowDialog() != DialogResult.OK || dialog.FileName.Length == 0)
                {
                    return;
                }

                path = dialog.FileName;
                this.IsTemporary = false;
                this.Saved = true;
            }

            if (this.Loaded)
            {
                this.Clear();
            }

            OtbReader reader = new OtbReader();
            if (reader.Read(path))
            {
                this.ServerItems = reader.Items;
                this.CurrentOtbFullPath = path;
                this.CurrentOtbVersion = this.ServerItems.MinorVersion;

                // try find a plugin that can handle this version of otb
                this.CurrentPlugin = Program.plugins.AvailablePlugins.Find(this.CurrentOtbVersion);
                if (this.CurrentPlugin == null)
                {
                    this.ServerItems.Clear();
                    MessageBox.Show(string.Format("Could not find a plugin that could handle client version {0}", this.CurrentOtbVersion));
                    return;
                }

                if (!this.LoadClient(this.CurrentPlugin, this.CurrentOtbVersion))
                {
                    this.Clear(false);
                    return;
                }

                this.fileSaveAsMenuItem.Enabled = true;
                this.fileSaveMenuItem.Enabled = true;
                this.editCreateItemMenuItem.Enabled = true;
                this.editFindItemMenuItem.Enabled = true;
                this.editCreateMissingItemsMenuItem.Enabled = true;
                this.viewShowOnlyMismatchedMenuItem.Enabled = true;
                this.viewShowDecaptedItemsMenuItem.Enabled = true;
                this.viewUpdateItemsListMenuItem.Enabled = true;
                this.toolsUpdateVersionMenuItem.Enabled = true;
                this.toolsReloadItemAttributesMenuItem.Enabled = true;
                this.toolStripSaveButton.Enabled = true;
                this.toolStripSaveAsButton.Enabled = true;
                this.toolStripFindItemButton.Enabled = true;
                this.serverItemListBox.Plugin = this.CurrentPlugin.Instance;
                this.serverItemListBox.Enabled = true;
                this.newItemButton.Enabled = true;
                this.duplicateItemButton.Enabled = true;
                this.reloadItemButton.Enabled = true;
                this.findItemButton.Enabled = true;
                this.Loaded = true;
                this.BuildItemsListBox();
            }

            try
            {
                string directory = Path.GetDirectoryName(path);
                if (Directory.Exists(directory))
                {
                    ItemsXmlReader xmlReader = new ItemsXmlReader();
                    xmlReader.Read(directory, ServerItems);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
        }

        public void Save()
        {
            if (this.IsTemporary)
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
                OtbWriter writer = new OtbWriter(this.ServerItems);
                if (writer.Write(this.CurrentOtbFullPath))
                {
                    this.Saved = true;
                    Trace.WriteLine("Saved.");
                }
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
            dialog.Filter = "OTB files (*.otb)|*.otb";
            dialog.Title = "Save OTB File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.Length == 0)
                {
                    return;
                }

                try
                {
                    OtbWriter writer = new OtbWriter(this.ServerItems);
                    if (writer.Write(dialog.FileName))
                    {
                        this.CurrentOtbFullPath = dialog.FileName;
                        this.IsTemporary = false;
                        this.Saved = true;
                        Trace.WriteLine("Saved.");
                    }
                }
                catch (UnauthorizedAccessException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public bool SelectItem(ushort sid)
        {
            if (!this.Loaded)
            {
                return false;
            }

            if (sid >= this.ServerItems.MinId && sid <= this.ServerItems.MaxId)
            {
                ServerItem item = this.ServerItems.Items.Find(i => i.ID == sid);
                if (item != null)
                {
                    return this.SelectItem(item);
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

            if (this.CurrentServerItem == item)
            {
                return true;
            }

            int index;
            if (item == null || (index = this.serverItemListBox.Items.IndexOf(item)) == -1)
            {
                this.ResetControls();
                return false;
            }

            this.EditItem(item);
            this.editDuplicateItemMenuItem.Enabled = true;
            this.editReloadItemMenuItem.Enabled = true;
            this.optionsGroupBox.Enabled = true;
            this.appearanceGroupBox.Enabled = true;
            this.serverItemListBox.SelectedIndex = index;
            return true;
        }

        public ServerItem AddNewItem()
        {
            if (!this.Loaded)
            {
                return null;
            }

            ServerItem item = this.CreateItem();
            item.ID = (ushort)(this.ServerItems.MaxId + 1);
            this.ServerItems.Add(item);
            this.serverItemListBox.Add(item);
            this.SelectItem(item);
            this.itemsCountLabel.Text = this.serverItemListBox.Count + " Items";
            Trace.WriteLine(string.Format("Create item id {0}", item.ID));
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
            copyItem.ID = (ushort)(this.ServerItems.MaxId + 1);
            this.ServerItems.Add(copyItem);
            this.serverItemListBox.Add(copyItem);
            this.SelectItem(copyItem);
            this.itemsCountLabel.Text = this.serverItemListBox.Count + " Items";

            Trace.WriteLine(string.Format("Duplicated item id {0} to new item id {1}", item.ID, copyItem.ID));
            return true;
        }

        public void CreateEmptyOTB(string filePath, SupportedClient client, bool isTemporary = true)
        {
            ServerItem item = new ServerItem();
            item.SpriteHash = new byte[16];
            item.ClientId = 100;
            item.ID = 100;

            ServerItemList items = new ServerItemList();
            items.MajorVersion = 3;
            items.MinorVersion = client.OtbVersion;
            items.BuildNumber = 1;
            items.ClientVersion = client.Version;
            items.Add(item);

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                    ////
                }
            }

            OtbWriter writer = new OtbWriter(items);
            if (writer.Write(filePath))
            {
                this.Open(filePath);
                this.IsTemporary = isTemporary;
                this.Saved = !isTemporary;
            }
        }

        public void Clear()
        {
            this.Clear(true);
        }

        public void Clear(bool clearLog)
        {
            this.SelectItem(null);
            this.CurrentServerItem = null;
            this.CurrentPlugin = null;
            this.PreviousPlugin = null;
            this.CurrentOtbVersion = 0;
            this.CurrentOtbFullPath = string.Empty;
            this.ServerItems.Clear();
            this.serverItemListBox.Plugin = null;
            this.serverItemListBox.Enabled = false;
            this.fileSaveMenuItem.Enabled = false;
            this.fileSaveAsMenuItem.Enabled = false;
            this.editCreateItemMenuItem.Enabled = false;
            this.editDuplicateItemMenuItem.Enabled = false;
            this.editReloadItemMenuItem.Enabled = false;
            this.editFindItemMenuItem.Enabled = false;
            this.editCreateMissingItemsMenuItem.Enabled = false;
            this.viewShowOnlyMismatchedMenuItem.Enabled = false;
            this.viewShowDecaptedItemsMenuItem.Enabled = false;
            this.viewUpdateItemsListMenuItem.Enabled = false;
            this.toolsUpdateVersionMenuItem.Enabled = false;
            this.toolsReloadItemAttributesMenuItem.Enabled = false;
            this.toolStripSaveButton.Enabled = false;
            this.toolStripSaveAsButton.Enabled = false;
            this.toolStripFindItemButton.Enabled = false;
            this.newItemButton.Enabled = false;
            this.duplicateItemButton.Enabled = false;
            this.reloadItemButton.Enabled = false;
            this.findItemButton.Enabled = false;
            this.Loaded = false;

            if (clearLog)
            {
                this.textBoxListener.Clear();
            }

            if (this.OnCleaned != null)
            {
                this.OnCleaned.Invoke(this, new EventArgs());
            }
        }

        #endregion

        #region Private Methods

        private void InitializeTooltips()
        {
            this.toolTip.SetToolTip(this.newItemButton, "Create Item");
            this.toolTip.SetToolTip(this.duplicateItemButton, "Duplicate Item");
            this.toolTip.SetToolTip(this.reloadItemButton, "Reaload Item");
            this.toolTip.SetToolTip(this.findItemButton, "Find Item");
        }

        private void BuildItemsListBox()
        {
            this.ClearItemsListBox();
            this.ResetControls();

            this.loadingItemsProgressBar.Visible = true;
            this.loadingItemsProgressBar.Minimum = 0;
            this.loadingItemsProgressBar.Maximum = this.ServerItems.Count + 1;

            List<ServerItem> items = new List<ServerItem>();
            ushort index = 0;

            foreach (ServerItem item in this.ServerItems)
            {
                if (this.showOnlyMismatchedItems && this.CompareItem(item, true))
                {
                    continue;
                }

                if ((this.showOnlyDeprecatedItems && item.Type != ServerItemType.Deprecated) || (!this.showOnlyDeprecatedItems && item.Type == ServerItemType.Deprecated))
                {
                    continue;
                }

                items.Add(item);
                index++;
                this.loadingItemsProgressBar.Value = index;
            }

            this.serverItemListBox.Add(items);
            this.loadingItemsProgressBar.Visible = false;
            this.itemsCountLabel.Text = this.serverItemListBox.Count + " Items";
        }

        private void ClearItemsListBox()
        {
            this.serverItemListBox.Clear();
        }

        private bool CompareItem(ServerItem item, bool compareHash)
        {
            if (item.Type == ServerItemType.Deprecated)
            {
                return true;
            }

            ClientItem clientItem;
            if (this.CurrentPlugin.Instance.Items.TryGetValue(item.ClientId, out clientItem))
            {
                if (compareHash && !Utils.ByteArrayCompare(item.SpriteHash, clientItem.SpriteHash))
                {
                    return false;
                }

                return item.Equals(clientItem);
            }

            return false;
        }

        private void ReloadItems()
        {
            foreach (ServerItem item in this.ServerItems)
            {
                if (!this.CompareItem(item, true))
                {
                    this.ReloadItem(item);
                }
            }
        }

        private void ReloadItem(ServerItem item)
        {
            if (!this.Loaded || item == null)
            {
                return;
            }

            // to avoid problems with events
            ServerItem tmpItem = this.CurrentServerItem;
            this.CurrentServerItem = null;

            ClientItem clientItem;
            if (this.CurrentPlugin.Instance.Items.TryGetValue(item.ClientId, out clientItem))
            {
                Trace.WriteLine(string.Format("Reloading item id: {0}.", item.ID));

                ushort tmpId = item.ID;
                item.CopyPropertiesFrom(clientItem);
                item.ID = tmpId;
                Buffer.BlockCopy(clientItem.SpriteHash, 0, item.SpriteHash, 0, clientItem.SpriteHash.Length);

                this.CurrentServerItem = tmpItem;
            }
        }

        private void ReloadSelectedItem()
        {
            ServerItem item = this.CurrentServerItem;
            this.ReloadItem(item);
            this.SelectItem(null);
            this.SelectItem(item);
        }

        private bool EditItem(ServerItem item)
        {
            this.CurrentServerItem = null;
            this.ResetDataBindings(this);
            this.ResetToolTips();

            if (item == null)
            {
                return false;
            }

            ClientItem clientItem;
            if (!this.CurrentPlugin.Instance.Items.TryGetValue(item.ClientId, out clientItem))
            {
                return false;
            }

            this.pictureBox.ClientItem = clientItem;
            if (!item.IsCustomCreated && item.SpriteHash != null && clientItem.SpriteHash != null)
            {
                this.pictureBox.BackColor = Utils.ByteArrayCompare(item.SpriteHash, clientItem.SpriteHash) ? Colors.DarkBackground : Color.Red;
            }

            this.typeCombo.Text = item.Type.ToString();
            this.typeCombo.ForeColor = item.Type == clientItem.Type ? Colors.LightText : Color.Red;

            this.stackOrderComboBox.Text = item.StackOrder.ToString();
            this.stackOrderComboBox.ForeColor = item.StackOrder == clientItem.StackOrder ? Colors.LightText : Color.Red;

            this.serverIdLbl.DataBindings.Add("Text", item, "ID");
            this.clientIdUpDown.Minimum = this.ServerItems.MinId;
            this.clientIdUpDown.Maximum = (this.CurrentPlugin.Instance.Items.Count + this.ServerItems.MinId) - 1;
            this.clientIdUpDown.DataBindings.Add("Value", clientItem, "ID");

            // Attributes
            this.AddBinding(this.unpassableCheck, "Checked", item, "Unpassable", item.Unpassable, clientItem.Unpassable);
            this.AddBinding(this.movableCheck, "Checked", item, "Movable", item.Movable, clientItem.Movable);
            this.AddBinding(this.blockMissilesCheck, "Checked", item, "BlockMissiles", item.BlockMissiles, clientItem.BlockMissiles);
            this.AddBinding(this.blockPathfinderCheck, "Checked", item, "BlockPathfinder", item.BlockPathfinder, clientItem.BlockPathfinder);
            this.AddBinding(this.forceUseCheckBox, "Checked", item, "ForceUse", item.ForceUse, clientItem.ForceUse);
            this.AddBinding(this.useableCheck, "Checked", item, "MultiUse", item.MultiUse, clientItem.MultiUse);
            this.AddBinding(this.pickupableCheck, "Checked", item, "Pickupable", item.Pickupable, clientItem.Pickupable);
            this.AddBinding(this.stackableCheck, "Checked", item, "Stackable", item.Stackable, clientItem.Stackable);
            this.AddBinding(this.readableCheck, "Checked", item, "Readable", item.Readable, clientItem.Readable);
            this.AddBinding(this.rotatableCheck, "Checked", item, "Rotatable", item.Rotatable, clientItem.Rotatable);
            this.AddBinding(this.hangableCheck, "Checked", item, "Hangable", item.Hangable, clientItem.Hangable);
            this.AddBinding(this.hookSouthCheck, "Checked", item, "HookSouth", item.HookSouth, clientItem.HookSouth);
            this.AddBinding(this.hookEastCheck, "Checked", item, "HookEast", item.HookEast, clientItem.HookEast);
            this.AddBinding(this.hasElevationCheck, "Checked", item, "HasElevation", item.HasElevation, clientItem.HasElevation);
            this.AddBinding(this.ignoreLookCheck, "Checked", item, "IgnoreLook", item.IgnoreLook, clientItem.IgnoreLook);
            this.AddBinding(this.fullGroundCheck, "Checked", item, "FullGround", item.FullGround, clientItem.FullGround);
            this.AddBinding(this.groundSpeedText, "Text", item, "GroundSpeed", item.GroundSpeed, clientItem.GroundSpeed, true);
            this.AddBinding(this.lightLevelText, "Text", item, "LightLevel", item.LightLevel, clientItem.LightLevel, true);
            this.AddBinding(this.lightColorText, "Text", item, "LightColor", item.LightColor, clientItem.LightColor, true);
            this.AddBinding(this.maxReadCharsText, "Text", item, "MaxReadChars", item.MaxReadChars, clientItem.MaxReadChars, true);
            this.AddBinding(this.maxReadWriteCharsText, "Text", item, "MaxReadWriteChars", item.MaxReadWriteChars, clientItem.MaxReadWriteChars, true);
            this.AddBinding(this.minimapColorText, "Text", item, "MinimapColor", item.MinimapColor, clientItem.MinimapColor, true);
            this.AddBinding(this.wareIdText, "Text", item, "TradeAs", item.TradeAs, clientItem.TradeAs, true);
            this.AddBinding(this.nameText, "Text", item, "Name", item.Name, clientItem.Name, true);

            this.candidatesButton.Enabled = false;
            for (int i = 0; i < this.candidatesTableLayoutPanel.ColumnCount; ++i)
            {
                ClientItemView box = (ClientItemView)this.candidatesTableLayoutPanel.GetControlFromPosition(i, 0);
                box.ClientItem = null;
            }

            if (this.PreviousPlugin != null)
            {
                ClientItem prevClientItem;
                if (this.PreviousPlugin.Instance.Items.TryGetValue(item.PreviousClientId, out prevClientItem))
                {
                    this.previousPictureBox.ClientItem = prevClientItem;
                    if (prevClientItem.SpriteSignature != null)
                    {
                        // Sprite does not match, use the sprite signature to find possible candidates
                        this.ShowSpriteCandidates(prevClientItem);
                    }
                }
                else
                {
                    this.previousPictureBox.ClientItem = null;
                }
            }

            this.CurrentServerItem = item;
            return true;
        }

        private void AddBinding(Control control, string propertyName, object dataSource, string dataMember, object value, object clientValue, bool setToolTip = false)
        {
            bool equals = value.Equals(clientValue);
            control.DataBindings.Add(propertyName, dataSource, dataMember);
            control.ForeColor = equals ? Colors.LightText : Color.Red;

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
                    this.ResetDataBindings(childControl);
                }
            }
        }

        private void ResetToolTips()
        {
            this.toolTip.RemoveAll();
        }

        private void ResetControls()
        {
            this.CurrentServerItem = null;
            this.editDuplicateItemMenuItem.Enabled = false;
            this.optionsGroupBox.Enabled = false;
            this.appearanceGroupBox.Enabled = false;
            this.pictureBox.ClientItem = null;
            this.pictureBox.BackColor = Colors.DarkBackground;
            this.previousPictureBox.ClientItem = null;
            this.previousPictureBox.BackColor = Colors.DarkBackground;
            this.clientIdUpDown.Value = clientIdUpDown.Minimum;
            this.serverIdLbl.Text = "0";
            this.typeCombo.Text = string.Empty;
            this.typeCombo.ForeColor = Colors.LightText;
            this.stackOrderComboBox.Text = string.Empty;
            this.stackOrderComboBox.ForeColor = Colors.LightText;
            this.editDuplicateItemMenuItem.Enabled = false;
            this.candidatesButton.Enabled = false;

            foreach (Control control in this.optionsGroupBox.Controls)
            {
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                    control.ForeColor = Colors.LightText;
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                    control.ForeColor = Colors.LightText;
                }
            }
        }

        private void ShowSpriteCandidates(ClientItem clientItem)
        {
            this.candidatesButton.Enabled = true;

            // list with the top 5 results
            List<KeyValuePair<double, ServerItem>> signatureList = new List<KeyValuePair<double, ServerItem>>();

            foreach (ServerItem cmpItem in this.ServerItems)
            {
                if (cmpItem.Type == ServerItemType.Deprecated)
                {
                    continue;
                }

                ClientItem cmpClientItem;
                if (!this.CurrentPlugin.Instance.Items.TryGetValue(cmpItem.ClientId, out cmpClientItem))
                {
                    continue;
                }

                double similarity = ImageUtils.CompareSignature(clientItem.SpriteSignature, cmpClientItem.SpriteSignature);

                foreach (KeyValuePair<double, ServerItem> kvp in signatureList)
                {
                    if (similarity < kvp.Key)
                    {
                        // TODO: Use isEqual aswell to match against attributes.
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

            // those with lowest value are the closest match
            int index = 0;
            foreach (KeyValuePair<double, ServerItem> kvp in signatureList)
            {
                ClientItemView box = (ClientItemView)candidatesTableLayoutPanel.GetControlFromPosition(index, 0);
                toolTip.SetToolTip(box, kvp.Value.ClientId.ToString());
                box.Tag = kvp.Value;

                ClientItem candidateItem;
                if (this.CurrentPlugin.Instance.Items.TryGetValue(kvp.Value.ClientId, out candidateItem))
                {
                    box.ClientItem = candidateItem;
                }

                ++index;
            }
        }

        private ServerItem CreateItem(Item item = null)
        {
            ushort newId = (ushort)(this.ServerItems.MaxId + 1);

            // create a new otb item
            ServerItem newItem = new ServerItem();

            if (item != null)
            {
                newItem.CopyPropertiesFrom(item);
                newItem.ID = newId;
                newItem.ClientId = item.ID;
                newItem.SpriteHash = new byte[16];
                Buffer.BlockCopy(item.SpriteHash, 0, newItem.SpriteHash, 0, newItem.SpriteHash.Length);
            }
            else
            {
                newItem.ID = newId;
                newItem.ClientId = this.ServerItems.MinId;
                newItem.SpriteHash = new byte[16];
                newItem.IsCustomCreated = true;
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
            copy.ClientId = item.ClientId;
            Buffer.BlockCopy(item.SpriteHash, 0, copy.SpriteHash, 0, copy.SpriteHash.Length);
            return copy;
        }

        private bool LoadClient(Plugin plugin, uint otbVersion)
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

            uint datSignature = (uint)Properties.Settings.Default["DatSignature"];
            uint sprSignature = (uint)Properties.Settings.Default["SprSignature"];

            if (client.DatSignature != datSignature || client.SprSignature != sprSignature)
            {
                string message;
                if (datSignature == 0 || sprSignature == 0)
                {
                    message = "No client is selected. Please navigate to the client folder.";
                }
                else
                {
                    message = string.Format("The selected client is not compatible with this OTB(version {0}). Please navigate to the folder of a compatible client {1}.", client.OtbVersion, client.Version);
                }

                MessageBox.Show(message);

                PreferencesForm form = new PreferencesForm();
                if (form.ShowDialog() == DialogResult.OK && form.Plugin != null)
                {
                    return this.LoadClient(form.Plugin, otbVersion);
                }
                else
                {
                    return false;
                }
            }

            string clientFolder = (string)Properties.Settings.Default["ClientDirectory"];

            if (string.IsNullOrEmpty(clientFolder))
            {
                return false;
            }

            string datPath = Utils.FindClientFile(clientFolder, ".dat");
            string sprPath = Utils.FindClientFile(clientFolder, ".spr");
            bool extended = (bool)Properties.Settings.Default["Extended"];
            bool frameDurations = (bool)Properties.Settings.Default["FrameDurations"];
            bool transparency = (bool)Properties.Settings.Default["Transparency"];

            extended = extended || client.Version >= 960;
            frameDurations = frameDurations || client.Version >= 1050;

            if (!File.Exists(datPath) || !File.Exists(sprPath))
            {
                MessageBox.Show("Client files not found.");
                return false;
            }

            Trace.WriteLine(string.Format("OTB version {0}.", otbVersion));

            bool result;

            try
            {
                result = plugin.Instance.LoadClient(client, extended, frameDurations, transparency, datPath, sprPath);
            }
            catch (UnauthorizedAccessException error)
            {
                MessageBox.Show(error.Message + " Please run this program as administrator.");
                return false;
            }

            Trace.WriteLine("Loading client files.");
            if (!result)
            {
                MessageBox.Show(string.Format("The plugin could not load dat or spr."));
            }

            this.ServerItems.ClientVersion = client.Version;
            Trace.WriteLine(string.Format("Client version {0}.", client.Version));
            return result;
        }

        private bool GenerateSpriteSignatures(ref ClientItems items)
        {
            if (items.SignatureCalculated)
            {
                return true;
            }

            ProgressForm progress = new ProgressForm();
            progress.StartPosition = FormStartPosition.Manual;
            progress.Location = new Point(Location.X + ((Width - progress.Width) / 2), Location.Y + ((Height - progress.Height) / 2));
            progress.bar.Minimum = 0;
            progress.bar.Maximum = items.Count;
            progress.Show(this);
            progress.progressLbl.Text = "Calculating image signatures...";

            foreach (ClientItem clientItem in items.Values)
            {
                clientItem.GenerateSignature();

                if (progress.bar.Value % 20 == 0)
                {
                    Application.DoEvents();
                }

                progress.bar.Value++;
            }

            items.SignatureCalculated = true;
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
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            ApplicationName = assemblyName.Name;
            ApplicationVersion = assemblyName.Version.Major + "." + assemblyName.Version.Minor;

            this.Text = ApplicationName + " " + ApplicationVersion;
            this.typeCombo.DataSource = Enum.GetNames(typeof(ServerItemType));
            this.stackOrderComboBox.DataSource = Enum.GetNames(typeof(TileStackOrder));

            this.candidatesDropDown.Items.Add(new ToolStripControlHost(this.candidatesTableLayoutPanel));

            Trace.Listeners.Clear();
            this.textBoxListener = new TextBoxTraceListener(this.outputTextBox);
            Trace.Listeners.Add(this.textBoxListener);

            this.SelectItem(null);

            Program.plugins.FindPlugins();
            Sprite.CreateBlankSprite();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CheckSave())
            {
                e.Cancel = true;
            }
        }

        private void FileNewMenuItem_Click(object sender, EventArgs e)
        {
            NewOtbFileForm newOtbForm = new NewOtbFileForm();

            if (newOtbForm.ShowDialog() == DialogResult.OK)
            {
                this.CreateEmptyOTB(newOtbForm.FilePath, newOtbForm.SelectedClient);
            }
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            this.Open();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilePreferencesMenuItem_Click(object sender, EventArgs e)
        {
            PreferencesForm form = new PreferencesForm();
            form.ShowDialog();
        }

        private void ItemsListBoxContextMenu_Opening(object sender, CancelEventArgs e)
        {
            itemsListBoxContextMenu.Items.Clear();
            if (Loaded)
            {
                itemsListBoxContextMenu.Items.Add("Duplicate");
                itemsListBoxContextMenu.Items.Add("Reload");
                itemsListBoxContextMenu.Items.Add("-");
                itemsListBoxContextMenu.Items.Add("Copy Server ID");
                itemsListBoxContextMenu.Items.Add("Copy Client ID");
                itemsListBoxContextMenu.Items.Add("Copy Name");
            }
        }

        private void ItemsListBoxContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem menuItem = e.ClickedItem;
            switch (menuItem.Text)
            {
                case "Duplicate":
                    DuplicateItem(CurrentServerItem);
                    break;

                case "Reload":
                    ReloadSelectedItem();
                    break;

                case "Copy Server ID":
                    Clipboard.SetText(CurrentServerItem.ID.ToString());
                    break;

                case "Copy Client ID":
                    Clipboard.SetText(CurrentServerItem.ClientId.ToString());
                    break;

                case "Copy Name":
                    Clipboard.SetText(CurrentServerItem.NameXml);
                    break;
            }
        }

        private void ItemsListBox_SelectedIndexChanged(object sender)
        {
            this.SelectItem(this.serverItemListBox.SelectedItem as ServerItem);
        }

        private void ItemsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.serverItemListBox.SelectedIndex = this.serverItemListBox.IndexFromPoint(e.Location);
                this.itemsListBoxContextMenu.Show();
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void ToolsCompareOtbFilesMenuItem_Click(object sender, EventArgs e)
        {
            CompareOtbForm form = new CompareOtbForm();
            form.ShowDialog();
        }

        private void ShowOnlyUnmatchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showOnlyMismatchedItems = !this.showOnlyMismatchedItems;
            this.BuildItemsListBox();
        }

        private void ViewShowDecaptedItemsMenuItem_Click(object sender, EventArgs e)
        {
            this.showOnlyDeprecatedItems = !this.showOnlyDeprecatedItems;
            this.BuildItemsListBox();
        }

        private void ToolsReloadItemAttributesMenuItem_Click(object sender, EventArgs e)
        {
            this.ReloadItems();
            this.EditItem(this.CurrentServerItem);
            this.BuildItemsListBox();
        }

        private void ToolsUpdateVersionMenuItem_Click(object sender, EventArgs e)
        {
            UpdateForm form = new UpdateForm();
            form.MainForm = this;

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Update OTB
                Plugin updatePlugin = form.SelectedPlugin;
                SupportedClient updateClient = form.UpdateClient;

                if (updatePlugin == null)
                {
                    return;
                }

                if (!this.LoadClient(updatePlugin, updateClient.OtbVersion))
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
                    // Calculate an image signature using fourier transformation and calculate a signature we can
                    // use to compare it to other images (kinda similar to md5 hash) except this
                    // can also be used to find images with some variation.
                    ClientItems currentClientItems = this.CurrentPlugin.Instance.Items;
                    this.GenerateSpriteSignatures(ref currentClientItems);

                    ClientItems updateClientItems = updatePlugin.Instance.Items;
                    this.GenerateSpriteSignatures(ref updateClientItems);
                }

                ClientItems currentItems = this.CurrentPlugin.Instance.Items;
                ClientItems updateItems = updatePlugin.Instance.Items;
                List<ushort> assignedSpriteIdList = new List<ushort>();

                // store the previous plugin (so we can display previous sprite, and do other comparisions)
                this.PreviousPlugin = this.CurrentPlugin;

                // update the current plugin the one we are updating to
                this.CurrentPlugin = updatePlugin;

                // update version information
                this.ServerItems.ClientVersion = updateClient.Version;
                this.ServerItems.MinorVersion = updateClient.OtbVersion;
                this.ServerItems.BuildNumber = this.ServerItems.BuildNumber + 1;
                this.CurrentOtbVersion = this.ServerItems.MinorVersion;

                // Most items does have the same sprite after an update, so lets try that first
                uint foundItemCounter = 0;
                foreach (ServerItem item in this.ServerItems)
                {
                    item.SpriteAssigned = false;

                    if (item.Type == ServerItemType.Deprecated)
                    {
                        continue;
                    }

                    ClientItem updateClientItem;
                    if (updateItems.TryGetValue(item.ClientId, out updateClientItem))
                    {
                        bool compareResult = updateClientItem.Equals(item);

                        if (Utils.ByteArrayCompare(updateClientItem.SpriteHash, item.SpriteHash))
                        {
                            if (compareResult)
                            {
                                item.PreviousClientId = item.ClientId;
                                item.ClientId = updateClientItem.ID;
                                item.SpriteAssigned = true;

                                assignedSpriteIdList.Add(updateClientItem.ID);
                                ++foundItemCounter;

                                // Trace.WriteLine(String.Format("Match found id: {0}, clientid: {1}", item.otb.id, item.dat.id));
                            }
                            else
                            {
                                // Sprite matches, but not the other attributes.
                                // Trace.WriteLine(String.Format("Attribute changes found id: {0}.", item.id));
                            }
                        }
                    }
                }

                if (updateSettingsForm.reassignUnmatchedSpritesCheck.Checked)
                {
                    foreach (Item updateItem in updateItems.Values)
                    {
                        foreach (ServerItem item in this.ServerItems)
                        {
                            if (item.Type == ServerItemType.Deprecated)
                            {
                                continue;
                            }

                            if (item.SpriteAssigned)
                            {
                                continue;
                            }

                            if (Utils.ByteArrayCompare(updateItem.SpriteHash, item.SpriteHash))
                            {
                                if (updateItem.Equals(item))
                                {
                                    if (updateItem.ID != item.ClientId)
                                    {
                                        Trace.WriteLine(string.Format("New sprite found id: {0}, old: {1}, new: {2}.", item.ID, item.ClientId, updateItem.ID));
                                    }

                                    item.PreviousClientId = item.ClientId;
                                    item.ClientId = updateItem.ID;
                                    item.SpriteAssigned = true;

                                    assignedSpriteIdList.Add(updateItem.ID);
                                    ++foundItemCounter;
                                    break;
                                }
                            }
                        }
                    }
                }

                Trace.WriteLine(string.Format("Found {0} of {1}.", foundItemCounter, this.ServerItems.MaxId));

                if (updateSettingsForm.reloadItemAttributesCheck.Checked)
                {
                    uint reloadedItemCounter = 0;
                    foreach (ServerItem item in this.ServerItems)
                    {
                        if (item.Type == ServerItemType.Deprecated)
                        {
                            continue;
                        }

                        // implicit assigned
                        item.PreviousClientId = item.ClientId;
                        item.SpriteAssigned = true;

                        if (!assignedSpriteIdList.Contains(item.ClientId))
                        {
                            assignedSpriteIdList.Add(item.ClientId);
                        }

                        if (!this.CompareItem(item, true))
                        {
                            // sync with dat info
                            this.ReloadItem(item);
                            ++reloadedItemCounter;
                        }
                    }

                    Trace.WriteLine(string.Format("Reloaded {0} of {1} items.", reloadedItemCounter, this.ServerItems.MaxId));
                }

                if (updateSettingsForm.createNewItemsCheck.Checked)
                {
                    uint newItemCounter = 0;
                    foreach (Item updateItem in updateItems.Values)
                    {
                        if (!assignedSpriteIdList.Contains(updateItem.ID))
                        {
                            ++newItemCounter;
                            ServerItem newItem = this.CreateItem(updateItem);
                            this.ServerItems.Add(newItem);
                            Trace.WriteLine(string.Format("Creating item id {0}", newItem.ID));
                        }
                    }

                    Trace.WriteLine(string.Format("Created {0} new items.", newItemCounter));
                }

                // done
                this.BuildItemsListBox();
            }
        }

        private void CandidatePictureBox_Click(object sender, EventArgs e)
        {
            if (this.CurrentServerItem != null)
            {
                ClientItemView box = (ClientItemView)sender;
                if (box.Tag is ServerItem)
                {
                    ServerItem newItem = (ServerItem)box.Tag;

                    ClientItem clientItem;
                    if (!this.CurrentPlugin.Instance.Items.TryGetValue(newItem.ClientId, out clientItem))
                    {
                        return;
                    }

                    if (!clientItem.Equals(this.CurrentServerItem))
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

                    this.CurrentServerItem.PreviousClientId = this.CurrentServerItem.ClientId;
                    this.CurrentServerItem.ClientId = clientItem.ID;
                    this.EditItem(this.CurrentServerItem);
                }
            }
        }

        private void ClientIdUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.CurrentServerItem != null)
            {
                ClientItem newClientItem;
                if (this.CurrentPlugin.Instance.Items.TryGetValue((ushort)clientIdUpDown.Value, out newClientItem))
                {
                    this.CurrentServerItem.PreviousClientId = this.CurrentServerItem.ClientId;
                    this.CurrentServerItem.ClientId = newClientItem.ID;
                    this.EditItem(this.CurrentServerItem);
                }
            }
        }

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void EditCreateItemMenuItem_Click(object sender, EventArgs e)
        {
            this.AddNewItem();
        }

        private void EditDuplicateItemMenuItem_Click(object sender, EventArgs e)
        {
            this.DuplicateItem(this.CurrentServerItem);
        }

        private void EditReloadItemMenuItem_Click(object sender, EventArgs e)
        {
            this.ReloadSelectedItem();
        }

        private void EditCreateMissingItemsMenu_Click(object sender, EventArgs e)
        {
            ushort lastCid = 0;
            ushort maxCid = this.CurrentPlugin.Instance.MaxItemId;

            foreach (ServerItem item in this.ServerItems.Items)
            {
                if (item.ClientId > lastCid)
                {
                    lastCid = item.ClientId;
                }
            }

            ushort newItemCounter = 0;

            if (lastCid < maxCid)
            {
                for (ushort i = (ushort)(lastCid + 1); i <= maxCid; i++)
                {
                    ServerItem item = this.CreateItem();
                    item.ClientId = i;
                    this.ServerItems.Add(item);

                    // sync with dat info
                    this.ReloadItem(item);
                    newItemCounter++;
                }

                // done
                this.BuildItemsListBox();
            }

            Trace.WriteLine(string.Format("Created {0} new items.", newItemCounter));
        }

        private void TypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrentServerItem != null)
            {
                this.CurrentServerItem.Type = (ServerItemType)Enum.Parse(typeof(ServerItemType), this.typeCombo.SelectedValue.ToString());
            }
        }

        private void StackOrderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrentServerItem != null)
            {
                this.CurrentServerItem.StackOrder = (TileStackOrder)Enum.Parse(typeof(TileStackOrder), this.stackOrderComboBox.SelectedValue.ToString());
                this.CurrentServerItem.HasStackOrder = this.CurrentServerItem.StackOrder != TileStackOrder.None;
            }
        }

        private void ViewUpdateItemsListMenuItem_Click(object sender, EventArgs e)
        {
            this.ResetControls();
            this.BuildItemsListBox();
        }

        private void FindItemButton_Click(object sender, EventArgs e)
        {
            if (this.findItemForm != null)
            {
                return;
            }

            this.findItemForm = new FindItemForm();
            this.findItemForm.MainForm = this;
            this.findItemForm.FormClosed += FindItemForm_CloseHandler;
            this.findItemForm.Show(this);
        }

        private void CandidatesButton_Click(object sender, EventArgs e)
        {
            this.candidatesDropDown.Show(this, new Point(192, 355));
        }

        private void NewItemButton_Click(object sender, EventArgs e)
        {
            this.AddNewItem();
        }

        private void DuplicateItemButton_Click(object sender, EventArgs e)
        {
            this.DuplicateItem(this.CurrentServerItem);
        }

        private void ReloadItemButton_Click(object sender, EventArgs e)
        {
            this.ReloadSelectedItem();
        }

        private void FindItemForm_CloseHandler(object sender, FormClosedEventArgs e)
        {
            this.findItemForm = null;
        }

        #endregion
    }
}
