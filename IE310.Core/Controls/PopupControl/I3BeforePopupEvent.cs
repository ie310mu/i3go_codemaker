using System;

namespace IE310.Core.Controls.PopupControl
{
    public delegate void I3BeforePopupEvent(object sender, I3BeforePopupEventArgs e);

    public class I3BeforePopupEventArgs : EventArgs
    {
        bool cancel = false;
        /// <summary>
        /// ��ʶ�Ƿ��˳��¼�
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



        public I3BeforePopupEventArgs()
            : base()
        {
        }
    }
}
