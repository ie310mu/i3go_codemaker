using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IE310.Core.Utils
{
    public static class I3UIUtil
    {
        /// <summary>
        /// 使弹出窗体的位置适应鼠标的当前位置
        /// </summary>
        public static void AdjustLocation(Form form)
        {
            Rectangle r = Screen.AllScreens[0].WorkingArea;
            Point pSize = Control.MousePosition;
            if (form.Width + pSize.X > r.Width)
            {
                pSize.X = r.Width - form.Width;
            }
            if (form.Height + pSize.Y > r.Height)
            {
                pSize.Y = r.Height - form.Height;
            }
            form.Location = pSize;
        }
    }
}
