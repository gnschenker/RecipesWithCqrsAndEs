using System.Drawing;

namespace Audit.Util
{
    public static class CommonColors
    {
        public static readonly Color Green = Color.FromArgb(204, 255, 204);
        public static readonly Color Blue = Color.FromArgb(204, 255, 255);
        public static readonly Color Red = Color.FromArgb(255, 204, 204);
        public static readonly Color Gray = Color.FromArgb(204, 204, 204);

        /// <summary>
        /// Courtesy of http://ethanschoonover.com/solarized#usage-development
        /// </summary>
        public static class Solarized
        {
            public static readonly Color Base03 = Color.FromArgb(0, 43, 54);
            public static readonly Color Base02 = Color.FromArgb(7, 54, 66);
            public static readonly Color Base01 = Color.FromArgb(88, 110, 117);
            public static readonly Color Base00 = Color.FromArgb(101, 123, 131);
            public static readonly Color Base0 = Color.FromArgb(131, 148, 150);
            public static readonly Color Base1 = Color.FromArgb(147, 161, 161);
            public static readonly Color Base2 = Color.FromArgb(238, 232, 213);
            public static readonly Color Base3 = Color.FromArgb(253, 246, 227);
            public static readonly Color Yellow = Color.FromArgb(181, 137, 0);
            public static readonly Color Orange = Color.FromArgb(203, 75, 22);
            public static readonly Color Red = Color.FromArgb(220, 50, 47);
            public static readonly Color Magenta = Color.FromArgb(211, 54, 130);
            public static readonly Color Violet = Color.FromArgb(108, 113, 196);
            public static readonly Color Blue = Color.FromArgb(38, 139, 210);
            public static readonly Color Cyan = Color.FromArgb(42, 161, 152);
            public static readonly Color Green = Color.FromArgb(133, 153, 0);
        }
    }
}