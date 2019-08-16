using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;

namespace IE310.Core.Win32
{
    public static class I3Win32PrintHelper
    {
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );

        const int PHYSICALOFFSETX = 112;
        const int PHYSICALOFFSETY = 113;
        const int PHYSICALWIDTH = 110;
        const int PHYSICALHEIGHT = 111;

        /// <summary>
        /// 获取纸张宽度（打印机单位）
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        public static int GetPaperWidth(IntPtr hdc)
        {
            return GetDeviceCaps(hdc, PHYSICALWIDTH);    //纸张的宽度（打印机单位）
        }

        /// <summary>
        /// 获取纸张高度（打印机单位）
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        public static int GetPaperHeight(IntPtr hdc)
        {
            return GetDeviceCaps(hdc, PHYSICALHEIGHT);    //纸张的宽度（打印机单位）
        }

        /// <summary>
        /// 获取纸张X方向的物理偏移量（打印机单位）
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        public static int GetPaperPhyOffsetX(IntPtr hdc)
        {
            return GetDeviceCaps(hdc, PHYSICALOFFSETX);
        }

        /// <summary>
        /// 获取纸张Y方向的物理偏移量（打印机单位）
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        public static int GetPaperPhyOffsetY(IntPtr hdc)
        {
            return GetDeviceCaps(hdc, PHYSICALOFFSETY);
        }
    }
}
