using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.Controls.PopupControl
{
    public partial class PopupComboBox : ComboBox
    {
        public PopupComboBox()
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

        private PopupControlHost hostControl;
        private PopupControlHost HostControl
        {
            get
            {
                if (this.hostControl == null)
                {
                    if (this.popupControl != null)
                    {
                        this.hostControl = new PopupControlHost(this.popupControl);
                        this.hostControl.AfterPopup += new AfterPopupEvent(hostControl_AfterPopup);
                    }
                }
                return this.hostControl;
            }
        }

        private void hostControl_AfterPopup(object sender, AfterPopupEventArgs e)
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
                    this.hostControl.AfterPopup -= new AfterPopupEvent(hostControl_AfterPopup);
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

            BeforePopupEventArgs e = this.OnBeforePopup();
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




        public event BeforePopupEvent BeforePopup;
        private BeforePopupEventArgs OnBeforePopup()
        {
            BeforePopupEventArgs e = new BeforePopupEventArgs();
            if (this.BeforePopup != null)
            {
                this.BeforePopup(this, e);
            }
            return e;
        }

        public event AfterPopupEvent AfterPopup;
        private AfterPopupEventArgs OnAfterPopup()
        {
            AfterPopupEventArgs e = new AfterPopupEventArgs();
            if (this.AfterPopup != null)
            {
                this.AfterPopup(this, e);
            }
            return e;
        }
    }



}
