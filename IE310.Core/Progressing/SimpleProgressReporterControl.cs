using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.Progressing
{
    public partial class SimpleProgressReporterControl : UserControl, IProgressReporter
    {
        public SimpleProgressReporterControl()
        {
            InitializeComponent();

            this.Visible = false;
        }

        public void ChangeProgress(I3ProgressingEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        ChangeProgress(e);
                    }));
                }
                catch
                {
                }
                return;
            }

            double positionD = Math.Abs(e.Max - e.Min) < 0.00001 ? 0 : e.Position / (e.Max - e.Min);
            positionD = positionD * 100;
            int position = (int)positionD;
            if (position != progressBar.Value || !string.Equals(progressBar.Tag.ToString(), e.Message))
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                progressBar.Value = position;
                progressBar.Tag = e.Message;
                Application.DoEvents();
            }
        }

        public event StopByUserEvent StopByUser;
        private void ibtnCancel_Click(object sender, EventArgs e)
        {
            if (StopByUser != null)
            {
                StopByUser();
            }
        }

        public bool CanCloseByUser
        {
            get
            {
                return btCancel.Visible;
            }
            set
            {
                btCancel.Visible = value;
            }
        }
    }
}
