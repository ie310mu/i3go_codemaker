using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Controls.PopupControl
{
    public delegate void AfterPopupEvent(object sender, AfterPopupEventArgs e);

    public class AfterPopupEventArgs : EventArgs
    {


        public AfterPopupEventArgs()
            : base()
        {
        }
    }
}
