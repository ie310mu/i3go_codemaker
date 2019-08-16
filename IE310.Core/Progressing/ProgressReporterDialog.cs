using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.Progressing
{
    public partial class ProgressReporterDialog : Component, IProgressReporter
    {
        public ProgressReporterDialog()
        {
            InitializeComponent();
            this.InitDialog();
        }

        public ProgressReporterDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.InitDialog();
        }

        private void InitDialog()
        {
            if (this.DesignMode || this.reporterForm != null)
            {
                return;
            }

            reporterForm = new ProgressReporterForm();
            reporterForm.Reporter.StopByUser += new StopByUserEvent(Reporter_StopByUser);
            reporterForm.Show();
            reporterForm.Hide();
        }

        void Reporter_StopByUser()
        {
            if (StopByUser != null)
            {
                StopByUser();
            }
        }

        private ProgressReporterForm reporterForm;

        public void ChangeProgress(I3ProgressingEventArgs e)
        {
            if (reporterForm != null && !reporterForm.IsDisposed)
            {
                reporterForm.Reporter.ChangeProgress(e);
            }
        }

        public event StopByUserEvent StopByUser;
    }
}
