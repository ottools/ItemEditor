#region Licence
/**
* Copyright (C) 2015 Nailson S. <nailsonnego@gmail.com>
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
using System;
#endregion

namespace OTLib.Server.Items
{
    [Flags]
    public enum ServerItemFlag
    {
        BLOCK_SOLID = 1,
        BLOCK_MISSILE = 2,
        BLOCK_PATHFINDER = 4,
        HAS_ELEVATION = 8,
        USEABLE = 16,
        PICKUPABLE = 32,
        MOVEABLE = 64,
        STACKABLE = 128,
        FLOOR_CHANGE_DOWN = 256,
        FLOOR_CHANGE_NORTH = 512,
        FLOOR_CHANGE_EAST = 1024,
        FLOOR_CHANGE_SOUTH = 2048,
        FLOOR_CHANGE_WEST = 4096,
        ALWAYS_ON_TOP = 8192,
        READABLE = 16384,
        ROTABLE = 32768,
        HANGABLE = 65536,
        VERTICAL_WALL = 131072,
        HORIZONTAL_WALL = 262144,
        ALLOW_DISTANCE_READ = 1048576,
        IGNORE_LOOK = 8388608,
        ANIMATION = 16777216,
        FULL_GROUND = 33554432
    }
}
