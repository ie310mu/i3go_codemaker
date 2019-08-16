using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Controls.PopupControl
{
    public delegate void BeforePopupEvent(object sender, BeforePopupEventArgs e);

    public class BeforePopupEventArgs : EventArgs
    {
        bool cancel = false;
        /// <summary>
        /// 标识是否退出事件
        /// </summary>
        public bool Cancel
        {
            get
            {
                return cancel;
            }
            set
            {
                cancel = value;
            }
        }



        public BeforePopupEventArgs()
            : base()
        {
        }
    }
}
