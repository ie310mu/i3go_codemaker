using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.UI
{
    public partial class I3BubbleMessageForm : I3BubbleForm
    {

        public I3BubbleMessageForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            Close();
        }

        public void InitMessage(string aCatipn, string aMessage, bool aHideCancelButton)
        {
            this.Text = aCatipn;
            lbCaption.Text = aCatipn;
            edMessage.Text = "    " + aMessage;

            if (aHideCancelButton)
            {
                btCancel.Visible = false;
                btOK.Left = btCancel.Left;
            }
        }
    }
}
