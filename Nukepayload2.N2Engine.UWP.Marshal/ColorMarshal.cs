namespace Nukepayload2.N2Engine.UWP.Marshal
{
    public static class ColorMarshal
    {
        /// <summary>
        /// 将引擎定义的颜色转换为 Windows RT 的颜色
        /// </summary>
        public unsafe static Windows.UI.Color AsWindowsColor(this Foundation.Color color)
        {
            return *(Windows.UI.Color*)(&color); // Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

    }
}
