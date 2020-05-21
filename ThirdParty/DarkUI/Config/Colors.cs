using System.Drawing;

namespace DarkUI.Config
{
    public sealed class Colors
    {
        public static readonly Color GreyBackground = Color.FromArgb(63, 63, 63);
        public static readonly Color HeaderBackground = Color.FromArgb(57, 60, 62);
        public static readonly Color BlueBackground = Color.FromArgb(66, 77, 95);
        public static readonly Color DarkBlueBackground = Color.FromArgb(52, 57, 66);
        public static readonly Color DarkBackground = Color.FromArgb(43, 43, 43);
        public static readonly Color MediumBackground = Color.FromArgb(49, 51, 53);
        public static readonly Color LightBackground = Color.FromArgb(0x63, 0x63, 0x63);
        public static readonly Color LighterBackground = Color.FromArgb(95, 101, 102);
        public static readonly Color LightestBackground = Color.FromArgb(178, 178, 178);
        public static readonly Color LightBorder = Color.FromArgb(81, 81, 81);
        public static readonly Color DarkBorder = Color.FromArgb(51, 51, 51);
        public static readonly Color LightText = Color.FromArgb(223, 223, 223);
        public static readonly Color DisabledText = Color.FromArgb(153, 153, 153);
        public static readonly Color BlueHighlight = Color.FromArgb(104, 151, 187);
        public static readonly Color BlueSelection = Color.FromArgb(75, 110, 175);
        public static readonly Color GreyHighlight = Color.FromArgb(122, 128, 132);
        public static readonly Color GreySelection = Color.FromArgb(92, 92, 92);
        public static readonly Color DarkGreySelection = Color.FromArgb(82, 82, 82);
        public static readonly Color DarkBlueBorder = Color.FromArgb(51, 61, 78);
        public static readonly Color LightBlueBorder = Color.FromArgb(86, 97, 114);
        public static readonly Color ActiveControl = Color.FromArgb(159, 178, 196);
        public static readonly Color BorderColor = Color.FromArgb(0x27, 0x27, 0x27);
        public static readonly Color ListBackgroudColor = Color.FromArgb(0x53, 0x53, 0x53);
        public static readonly Color ListSelectionColor = Color.FromArgb(0x29, 0x48, 0x67);


        public static readonly Pen BorderColorPen = new Pen(BorderColor);
        public static readonly Brush TextColorBrush = new SolidBrush(LightText);
        public static readonly Brush ListBackgroudBrush = new SolidBrush(ListBackgroudColor);
        public static readonly Brush ListSelectionBrush = new SolidBrush(ListSelectionColor);
        public static readonly Brush ListViewBackgroudBrush = new SolidBrush(LightBackground);
    }
}
