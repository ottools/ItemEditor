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

using PluginInterface;
using System;

namespace ItemEditor.Host
{
	/// <summary>
	/// Data class for plugins.
	/// Holds and instance of the loaded plugin, as well as the plugins assembly path
	/// </summary>
	public class Plugin
	{
		#region Private Properties

		private IPlugin _instance;
		private String _assemblyPath;

		#endregion

		#region Public Properties

		public IPlugin Instance
		{
			get { return _instance; }
			set { _instance = value; }
		}

		public string AssemblyPath
		{
			get { return _assemblyPath; }
			set { _assemblyPath = value; }
		}

		#endregion
	}
}
