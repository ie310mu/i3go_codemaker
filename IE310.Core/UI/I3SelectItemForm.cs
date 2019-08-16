using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using IE310.Core.Utils;

namespace IE310.Core.UI
{
    public partial class I3SelectItemForm : Form
    {
        public I3SelectItemForm()
        {
            InitializeComponent();
        }

        private bool ok = false;
        private string selectItem = "";

        private void btCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (lbItem.SelectedItem == null)
            {
                I3MessageHelper.ShowWarning("请进行选择！");
                return;
            }

            selectItem = (string)lbItem.SelectedItem;
            ok = true;
            Close();
        }

        public static bool Excute(string caption, string[] items, out string selectItem)
        {
            using (I3SelectItemForm form = new I3SelectItemForm())
            {
                form.Text = caption;
                form.lbItem.DataSource = items;
                form.ShowDialog();

                selectItem = form.selectItem;
                return form.ok;
            }
        }

        private void lbItem_DoubleClick(object sender, EventArgs e)
        {
            btOK_Click(null, null);
        }
    }
}
