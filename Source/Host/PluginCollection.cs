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
using PluginInterface;
using System;
using System.Collections;
#endregion

namespace ItemEditor.Host
{
    /// <summary>
    /// Collection of AvailablePlugin Type
    /// </summary>
    public class PluginCollection : CollectionBase
    {
        #region Public Methods

        /// <summary>
        /// Add a plugin
        /// </summary>
        /// <param name="plugin">The Plugin to Add</param>
        public void Add(Plugin plugin)
        {
            this.List.Add(plugin);
        }

        /// <summary>
        /// Removes a plugin
        /// </summary>
        /// <param name="plugin">The Plugin to Remove</param>
        public void Remove(Plugin plugin)
        {
            this.List.Remove(plugin);
        }

        /// <summary>
        /// Search for a plugin by name
        /// </summary>
        /// <param name="pluginNameOrPath">The name or File path of the plugin to find</param>
        /// <returns>a plugin, or null if the plugin is not found</returns>
        public Plugin Find(string pluginNameOrPath)
        {
            foreach (Plugin plugin in this.List)
            {
                foreach (SupportedClient client in plugin.Instance.SupportedClients)
                {
                    if (client.Description.Equals(pluginNameOrPath))
                    {
                        return plugin;
                    }
                }

                if (plugin.AssemblyPath.Equals(pluginNameOrPath))
                {
                    return plugin;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for a plugin by compatibility
        /// </summary>
        /// <param name="otbVersion">The otb version</param>
        /// <returns>a plugin, or null if the plugin is not found</returns>
        public Plugin Find(uint otbVersion)
        {
            foreach (Plugin plugin in this.List)
            {
                foreach (SupportedClient client in plugin.Instance.SupportedClients)
                {
                    if (client.OtbVersion == otbVersion)
                    {
                        return plugin;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Search for a plugin by signatures
        /// </summary>
        /// <param name="datSignature">The dat file signature</param>
        /// <param name="sprSignature">The spr file signature</param>
        /// <returns>a plugin, or null if the plugin is not found</returns>
        public Plugin Find(uint datSignature, uint sprSignature)
        {
            foreach (Plugin plugin in this.List)
            {
                foreach (SupportedClient client in plugin.Instance.SupportedClients)
                {
                    if (client.DatSignature == datSignature && client.SprSignature == sprSignature)
                    {
                        return plugin;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
