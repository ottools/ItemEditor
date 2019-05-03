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
using ItemEditor.Helpers;
using PluginInterface;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Host
{
    /// <summary>
    /// Summary description for PluginServices.
    /// </summary>
    public class PluginServices : IPluginHost
    {
        #region Contructor

        /// <summary>
        /// Constructor of PluginServices
        /// </summary>
        public PluginServices()
        {
            this.AvailablePlugins = new PluginCollection();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A collection of all plugins found by FindPlugins()
        /// </summary>
        public PluginCollection AvailablePlugins { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Searches the Path for plugins
        /// </summary>
        public void FindPlugins()
        {
            string path = PathHelper.Plugins;
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Plugins were not found. Please reinstall the program.");
                return;
            }

            this.AvailablePlugins.Clear();

            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                if (name != "PluginInterface")
                {
                    this.AddPlugin(file);
                }
            }
        }

        /// <summary>
        /// Unloads all plugins
        /// </summary>
        public void ClosePlugins()
        {
            foreach (Plugin pluginOn in this.AvailablePlugins)
            {
                pluginOn.Instance.Dispose();
                pluginOn.Instance = null;
            }

            this.AvailablePlugins.Clear();
        }

        private void AddPlugin(string path)
        {
            Assembly pluginAssembly = Assembly.LoadFrom(path);

            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic)
                {
                    if (!pluginType.IsAbstract)
                    {
                        Type typeInterface = pluginType.GetInterface("PluginInterface.IPlugin", true);
                        if (typeInterface != null)
                        {
                            Plugin newPlugin = new Plugin();
                            newPlugin.AssemblyPath = path;
                            newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
                            newPlugin.Instance.Host = this;
                            newPlugin.Instance.Initialize();

                            this.AvailablePlugins.Add(newPlugin);

                            newPlugin = null;
                        }

                        typeInterface = null;
                    }
                }
            }

            pluginAssembly = null;
        }

        #endregion
    }
}
