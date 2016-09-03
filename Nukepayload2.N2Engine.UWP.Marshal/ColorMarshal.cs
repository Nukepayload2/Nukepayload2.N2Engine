using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nukepayload2.N2Engine.UWP.Marshal
{
    public static class ColorMarshal
    {
        /// <summary>
        /// 将引擎定义的颜色转换为 Windows RT 的颜色
        /// </summary>
        public unsafe static Windows.UI.Color AsWindowsColor(this Foundation.Color color)
        {
            return *(Windows.UI.Color*)(&color);
        }

    }
}
