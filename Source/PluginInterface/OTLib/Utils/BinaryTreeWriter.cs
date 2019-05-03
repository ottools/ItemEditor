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
using OTLib.Server.Items;
using System;
using System.IO;
#endregion

namespace OTLib.Utils
{
    public class BinaryTreeWriter : IDisposable
    {
        #region Private Properties

        private BinaryReader writer;

        #endregion

        #region Constructor

        public BinaryTreeWriter(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("input");
            }

            this.writer = new BinaryReader(new FileStream(path, FileMode.Create));
            this.Disposed = false;
        }

        #endregion

         #region Public Properties

        public bool Disposed { get; private set; }

        #endregion

        #region Public Properties

        public void CreateNode(byte type)
        {
            this.WriteByte((byte)SpecialChar.NodeStart, false);
            this.WriteByte(type);
        }

        public void WriteByte(byte value)
        {
            this.WriteBytes(new byte[1] { value }, true);
        }

        public void WriteByte(byte value, bool unescape)
        {
            this.WriteBytes(new byte[1] { value }, unescape);
        }

        public void WriteUInt16(ushort value)
        {
            this.WriteBytes(BitConverter.GetBytes(value), true);
        }

        public void WriteUInt16(ushort value, bool unescape)
        {
            this.WriteBytes(BitConverter.GetBytes(value), unescape);
        }

        public void WriteUInt32(uint value)
        {
            this.WriteBytes(BitConverter.GetBytes(value), true);
        }

        public void WriteUInt32(uint value, bool unescape)
        {
            this.WriteBytes(BitConverter.GetBytes(value), unescape);
        }

        public void WriteProp(ServerItemAttribute attribute, BinaryWriter writer)
        {
            writer.BaseStream.Position = 0;
            byte[] bytes = new byte[writer.BaseStream.Length];
            writer.BaseStream.Read(bytes, 0, (int)writer.BaseStream.Length);
            writer.BaseStream.Position = 0;
            writer.BaseStream.SetLength(0);

            this.WriteProp((byte)attribute, bytes);
        }

        public void WriteProp(RootAttribute attribute, BinaryWriter writer)
        {
            writer.BaseStream.Position = 0;
            byte[] bytes = new byte[writer.BaseStream.Length];
            writer.BaseStream.Read(bytes, 0, (int)writer.BaseStream.Length);
            writer.BaseStream.Position = 0;
            writer.BaseStream.SetLength(0);

            this.WriteProp((byte)attribute, bytes);
        }

        public void WriteBytes(byte[] bytes, bool unescape)
        {
            foreach (byte b in bytes)
            {
                if (unescape && (b == (byte)SpecialChar.NodeStart || b == (byte)SpecialChar.NodeEnd || b == (byte)SpecialChar.EscapeChar))
                {
                    this.writer.BaseStream.WriteByte((byte)SpecialChar.EscapeChar);
                }

                this.writer.BaseStream.WriteByte(b);
            }
        }

        public void CloseNode()
        {
            this.WriteByte((byte)SpecialChar.NodeEnd, false);
        }

        public void Dispose()
        {
            if (this.writer != null)
            {
                this.writer.Dispose();
                this.writer = null;
                this.Disposed = true;
            }
        }

        #endregion

        #region Private Methods

        private void WriteProp(byte attr, byte[] bytes)
        {
            this.WriteByte((byte)attr);
            this.WriteUInt16((ushort)bytes.Length);
            this.WriteBytes(bytes, true);
        }

        #endregion
    }
}
