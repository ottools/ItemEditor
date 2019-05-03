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
using System.Diagnostics;
using System.Windows.Forms;
#endregion

namespace ItemEditor.Diagnostics
{
    public class TextBoxTraceListener : TraceListener
    {
        #region Private Properties

        private delegate void StringSendDelegate(string message);

        private const uint UpdateFrequency = 10;
        private uint updateCounter = 0;

        private TextBox target;
        private StringSendDelegate invokeWrite;

        #endregion

        #region Constructor

        public TextBoxTraceListener(TextBox target)
        {
            this.target = target;
            this.invokeWrite = new StringSendDelegate(this.SendString);
        }

        #endregion

        #region Public Methods

        public void Clear()
        {
            this.target.Clear();
        }

        public override void Write(string message)
        {
            this.target.Invoke(this.invokeWrite, new object[] { ">> " + message });
        }

        public override void WriteLine(string message)
        {
            this.target.Invoke(this.invokeWrite, new object[] { ">> " + message + Environment.NewLine });
        }

        #endregion

        #region Private Methods

        private void SendString(string message)
        {
            // No need to lock text box as this function will only
            // ever be executed from the UI thread
            this.target.AppendText(message);

            this.updateCounter++;
            if (this.updateCounter >= UpdateFrequency)
            {
                this.updateCounter = 0;
                Application.DoEvents();
            }
        }

        #endregion
    }
}
