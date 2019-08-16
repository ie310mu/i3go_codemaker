using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IE310.Core.Controls;

namespace IE310.Core.UI
{
    public partial class I3SearchForm : Form
    {
        public static bool Excute(I3SearchInfo searchInfo, string aSearchName)
        {
            I3SearchInfo tmpSearchInfo = new I3SearchInfo();
            tmpSearchInfo.Copy(searchInfo);

            using (I3SearchForm form = new I3SearchForm())
            {
                form.iecT_Search1.Init(tmpSearchInfo, aSearchName);
                form.ShowDialog();
                if (form.ok)
                {
                    searchInfo.Copy(tmpSearchInfo);
                }

                return form.ok;
            }
        }

        public I3SearchForm()
        {
            InitializeComponent();
        }

        private bool ok = false;

        private void btSearch_Click(object sender, EventArgs e)
        {
            ok = true;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iecT_Search1_Search(object sender)
        {
            ok = true;
            Close();
        }
    }
}
