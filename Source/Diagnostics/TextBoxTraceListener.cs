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

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ItemEditor.Diagnostics
{
    public class TextBoxTraceListener : TraceListener
    {
        #region Properties

        const uint updateFrequency = 10;
        uint updateCounter = 0;

        private TextBox _target;
        private StringSendDelegate _invokeWrite;

        #endregion

        #region Constructor

        public TextBoxTraceListener(TextBox target)
        {
            _target = target;
            _invokeWrite = new StringSendDelegate(SendString);
        }

        #endregion

        #region General Methods

        public void Clear()
        {
            _target.Clear();
        }

        public override void Write(string message)
        {
            _target.Invoke(_invokeWrite, new object[] { ">> " + message });
        }

        public override void WriteLine(string message)
        {
            _target.Invoke(_invokeWrite, new object[] { ">> " + message + Environment.NewLine });
        }

        private delegate void StringSendDelegate(string message);

        private void SendString(string message)
        {
            // No need to lock text box as this function will only 
            // ever be executed from the UI thread
            _target.AppendText(message);

            ++updateCounter;
            if (updateCounter >= updateFrequency)
            {
                updateCounter = 0;
                Application.DoEvents();
            }
        }

        #endregion
    }
}
