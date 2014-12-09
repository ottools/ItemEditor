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

using ItemEditor.Helpers;
using PluginInterface;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ItemEditor.Host
{
	/// <summary>
	/// Summary description for PluginServices.
	/// </summary>
	public class PluginServices : IPluginHost
	{
		#region Private Properties

		private PluginCollection _collection = new PluginCollection();

		#endregion

		#region Public Properties

		/// <summary>
		/// A collection of all plugins found by FindPlugins()
		/// </summary>
		public PluginCollection AvailablePlugins
		{
			get { return _collection; }
			set { _collection = value; }
		}

		#endregion

		#region Contructor

		/// <summary>
		/// Constructor of PluginServices
		/// </summary>
		public PluginServices()
		{
		}

		#endregion

		#region Methods

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

			_collection.Clear();

			foreach (string file in Directory.GetFiles(path, "*.dll"))
			{
				string name = Path.GetFileNameWithoutExtension(file);

				if (name != "PluginInterface")
				{
					AddPlugin(file);
				}
			}
		}

		/// <summary>
		/// Unloads all plugins
		/// </summary>
		public void ClosePlugins()
		{
			foreach (Plugin pluginOn in _collection)
			{
				pluginOn.Instance.Dispose();
				pluginOn.Instance = null;
			}

			_collection.Clear();
		}

		private void AddPlugin(string FileName)
		{
			Assembly pluginAssembly = Assembly.LoadFrom(FileName);

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
							newPlugin.AssemblyPath = FileName;
							newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
							newPlugin.Instance.Host = this;
							newPlugin.Instance.Initialize();
							_collection.Add(newPlugin);

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
