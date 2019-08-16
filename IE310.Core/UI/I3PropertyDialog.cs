using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.UI
{
    internal partial class I3PropertyDialog : Form
    {
        public I3PropertyDialog(object obj)
        {
            InitializeComponent();
            //获取类型
            Type type = obj.GetType();
            if (type.IsValueType)
            {
                this.tbContent.Text = obj.ToString();
                this.grid.Visible = false;
            }
            else
            {
                this.grid.SelectedObject = obj;
                this.tbContent.Visible = false;
            }
        }
    }
}