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
using System;
#endregion

namespace OTLib.Server.Items
{
    [Flags]
    public enum ServerItemFlag
    {
        None                = 0,
        Unpassable          = 1 << 0,
        BlockMissiles       = 1 << 1,
        BlockPathfinder     = 1 << 2,
        HasElevation        = 1 << 3,
        MultiUse            = 1 << 4,
        Pickupable          = 1 << 5,
        Movable             = 1 << 6,
        Stackable           = 1 << 7,
        FloorChangeDown     = 1 << 8,
        FloorChangeNorth    = 1 << 9,
        FloorChangeEast     = 1 << 10,
        FloorChangeSouth    = 1 << 11,
        FloorChangeWest     = 1 << 12,
        StackOrder          = 1 << 13,
        Readable            = 1 << 14,
        Rotatable           = 1 << 15,
        Hangable            = 1 << 16,
        HookSouth           = 1 << 17,
        HookEast            = 1 << 18,
        CanNotDecay         = 1 << 19,
        AllowDistanceRead   = 1 << 20,
        Unused              = 1 << 21,
        ClientCharges       = 1 << 22,
        IgnoreLook          = 1 << 23,
        IsAnimation         = 1 << 24,
        FullGround          = 1 << 25,
        ForceUse            = 1 << 26
    }
}
