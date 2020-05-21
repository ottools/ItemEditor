using DarkUI.Controls;
using OTLib.Server.Items;

namespace ItemEditor.Controls
{
    public class FlagCheckBox : DarkCheckBox
    {
        public ServerItemFlag ServerItemFlag { get; set; }
    }
}
