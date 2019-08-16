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
    internal partial class I3ExceptionDialog : Form
    {
        private int start;
        private int current;
        private int end;
        private double a;
        private int initHeight = 0;

        public I3ExceptionDialog(string message, Exception ex)
        {
            InitializeComponent();
            if (ex != null)
            {
                //摘要
                this.lblMessage.Text = message == null ? ex.Message : (message + Environment.NewLine + ex.Message);
                //详细信息
                this.tbDetail.Text = ex.ToString();
                //堆栈跟踪
                this.tbStackTrace.Text = ex.StackTrace;
                //上下文数据
                this.dgContext.AutoGenerateColumns = false;
                if (ex.Data != null)
                {
                    ArrayList list = new ArrayList();
                    foreach (DictionaryEntry entry in ex.Data)
                    {
                        this.dgContext.Rows.Add(entry.Key, entry.Value);
                        list.Add(entry.Value);
                    }
                    this.dgContext.Tag = list;
                }
            }
            else
            {
                this.lblMessage.Text = message;
                this.tbDetail.Text = message;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.current < 10)
            {
                this.current++;
                this.Height = this.start + (int)(this.a * this.current * this.current);
            }
            else
            {
                this.timer.Enabled = false;
                this.btnOp.Enabled = true;
                this.tabError.Visible = !this.tabError.Visible;
            }
        }

        private void btnOp_Click(object sender, EventArgs e)
        {
            if (this.tabError.Visible)
            {
                this.start = this.initHeight + 250;
                this.end = this.initHeight;
                this.btnOp.Text = ">>";
            }
            else
            {
                this.start = this.initHeight;
                this.end = this.initHeight + 250;
                this.btnOp.Text = "<<";
            }
            this.a = (this.end - this.start) / 100d;
            this.current = 0;
            this.btnOp.Enabled = false;
            this.timer.Enabled = true;
        }

        private void ExceptionDialog_Shown(object sender, EventArgs e)
        {
            this.initHeight = this.Height;
        }

        private void dgContext_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ArrayList list = this.dgContext.Tag as ArrayList;
                if (list != null)
                {
                    object key = this.dgContext.Rows[e.RowIndex].Cells[0].Value;
                    object value = list[e.RowIndex];
                    if (value != null)
                    {
                        //查看属性
                        using (I3PropertyDialog dialog = new I3PropertyDialog(value))
                        {
                            dialog.Text = string.Format("{0}({1})", key, value.GetType());
                            dialog.Left = this.Location.X + 72;
                            dialog.Top = this.Location.Y + 72;
                            dialog.ShowDialog(this);
                        }
                    }
                    else
                    {
                        I3MessageHelper.ShowInfo("指定的值为Null");
                    }
                }
            }
        }
    }
}