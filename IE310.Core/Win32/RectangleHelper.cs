using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace IE310.Core.Win32
{
    public static class I3RectangleHelper
    {
        /// <summary>
        /// 判断一个Rectangle是否是空，与系统中的判断不同，Rectangle.IsEmpty是X Y Width Height都为0时返回true
        /// 此方法当Width或者Heigth任意一个为空时就返回true
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool IsEmpty(Rectangle rect)
        {
            return rect.Width == 0 || rect.Height == 0;
        }
    }
}
