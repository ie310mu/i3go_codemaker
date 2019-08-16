using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.Controls.PopupControl
{
    public partial class I3PopupComboBox : ComboBox
    {
        public I3PopupComboBox()
            : base()
        {
        }

        private const int WM_LBUTTONDOWN = 0x201, WM_LBUTTONDBLCLK = 0x203;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_LBUTTONDOWN)
            {
                OnPopup();

                return;
            }
            base.WndProc(ref m);
        }

        private I3PopupControlHost hostControl;
        private I3PopupControlHost HostControl
        {
            get
            {
                if (this.hostControl == null)
                {
                    if (this.popupControl != null)
                    {
                        this.hostControl = new I3PopupControlHost(this.popupControl);
                        this.hostControl.AfterPopup += new I3AfterPopupEvent(hostControl_AfterPopup);
                    }
                }
                return this.hostControl;
            }
        }

        private void hostControl_AfterPopup(object sender, I3AfterPopupEventArgs e)
        {
            this.OnAfterPopup();
        }

        private Control popupControl;
        /// <summary>
        /// 弹出的下拉控件
        /// </summary>
        public Control PopupControl
        {
            get
            {
                return this.popupControl;
            }
            set
            {
                if (this.hostControl != null)
                {
                    this.hostControl.AfterPopup -= new I3AfterPopupEvent(hostControl_AfterPopup);
                    this.hostControl.Dispose();
                }
                this.popupControl = value;
            }
        }



        public void OnPopup()
        {
            if (this.HostControl == null)
            {
                return;
            }

            I3BeforePopupEventArgs e = this.OnBeforePopup();
            if (e.Cancel)
            {
                return;
            }

            this.HostControl.Show(this);
        }

        public void ClosePopup()
        {
            this.HostControl.Hide();
        }




        public event I3BeforePopupEvent BeforePopup;
        private I3BeforePopupEventArgs OnBeforePopup()
        {
            I3BeforePopupEventArgs e = new I3BeforePopupEventArgs();
            if (this.BeforePopup != null)
            {
                this.BeforePopup(this, e);
            }
            return e;
        }

        public event I3AfterPopupEvent AfterPopup;
        private I3AfterPopupEventArgs OnAfterPopup()
        {
            I3AfterPopupEventArgs e = new I3AfterPopupEventArgs();
            if (this.AfterPopup != null)
            {
                this.AfterPopup(this, e);
            }
            return e;
        }
    }



}
