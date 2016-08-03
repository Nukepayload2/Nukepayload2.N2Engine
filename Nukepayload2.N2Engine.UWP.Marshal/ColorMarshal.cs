using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nukepayload2.N2Engine.UWP.Marshal
{
    public static class ColorMarshal
    {
        public unsafe static Windows.UI.Color AsWindowsColor(this Core.Color color)
        {
            return *(Windows.UI.Color*)(&color);
        }
    }
}
