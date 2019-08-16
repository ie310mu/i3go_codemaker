using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IE310.Table.Design
{
    public partial class CustomPropertyEditForm : Form
    {
        private CustomPropertyEditForm()
        {
            InitializeComponent();
        }

        public static void Excute(ISite site, string caption, object obj)
        {
            using (CustomPropertyEditForm form = new CustomPropertyEditForm())
            {
                form.Site = site;
                form.propertyGrid1.Site = site;
                form.Text = caption;
                form.propertyGrid1.SelectedObject = obj;
                form.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
