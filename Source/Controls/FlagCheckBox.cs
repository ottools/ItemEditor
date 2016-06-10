using OTLib.Server.Items;
using System.Windows.Forms;

namespace ItemEditor.Controls
{
    public class FlagCheckBox : CheckBox
    {
        public ServerItemFlag ServerItemFlag { get; set; }
    }
}
