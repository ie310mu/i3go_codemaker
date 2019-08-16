using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Controls.PopupControl
{
    public delegate void I3AfterPopupEvent(object sender, I3AfterPopupEventArgs e);

    public class I3AfterPopupEventArgs : EventArgs
    {
        public I3AfterPopupEventArgs()
            : base()
        {
        }
    }
}
