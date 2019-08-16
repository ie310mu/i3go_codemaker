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
    internal partial class I3FormatStrForm : Form
    {
        private bool ok = false;
        private string FFormatStr = "null";


        private I3FormatStrForm()
        {
            InitializeComponent();
        }

        
        private string typeStr
        {
            get
            {
                if (c.Checked) return "C";
                if (d.Checked) return "D";
                if (e.Checked) return "E";
                if (f.Checked) return "F";
                if (n.Checked) return "N";
                if (p.Checked) return "P";
                if (x1.Checked) return "X";
                if (x2.Checked) return "x";
                return "F";
            }
            set
            {
                if (value.ToUpper().IndexOf("X") > -1)
                {
                    if (value == "X") x1.Checked = true;
                    if (value == "x") x2.Checked = true;
                }
                else
                {
                    value = value.ToUpper();
                    if (value == "C") c.Checked = true;
                    if (value == "D") d.Checked = true;
                    if (value == "E") d.Checked = true;
                    if (value == "F") f.Checked = true;
                    if (value == "N") n.Checked = true;
                    if (value == "P") p.Checked = true;
                }
            }
        }
        private int Len
        {
            get
            {
                try
                {
                    return Convert.ToInt32(textBox1.Text);
                }
                catch (Exception)
                {
                    return 2;
                }
            }
            set
            {
                textBox1.Text = value.ToString();
            }
        }

        /// <summary>
        /// 分解与合成FormatStr
        /// 
        /// 错误处理：屏蔽
        /// 
        /// </summary>
        private string ExtratFormatStr
        {
            get
            {
                return typeStr + Convert.ToString(Len);
            }
            set
            {
                try
                {
                    typeStr = I3StringUtil.SubString(value, 0, 1);
                    Len = Convert.ToInt32(I3StringUtil.SubString(value, 1, value.Length - 1));
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 显示窗口以获取一个格式化字符串，获取失败时返回false
        /// 通过aFormatStr传入原始格式化字符串，可不传入
        /// 通过aNewFormatStr返回新的格式化字符串
        /// 
        /// 错误处理：屏蔽
        /// 
        /// </summary>
        /// <param name="aFormatStr"></param>
        /// <param name="aNewFormatStr"></param>
        /// <returns></returns>
        static public bool Excute(string aFormatStr, out string aNewFormatStr)
        {
            I3FormatStrForm form = new I3FormatStrForm();
            try
            {
                form.ExtratFormatStr = aFormatStr;
                form.ShowDialog();
                aNewFormatStr = form.FFormatStr;
                return form.ok;
            }
            finally
            {
                form.Dispose();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double d = Convert.ToInt32(textBox1.Text);
                textBox1.ForeColor = System.Drawing.Color.Black;
                int i = 12345;
                this.Text = "格式: " + ExtratFormatStr + "  " + i.ToString(ExtratFormatStr);
            }
            catch (Exception)
            {
                textBox1.ForeColor = System.Drawing.Color.Red;
                this.Text = "格式: " + typeStr;
            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            int i = 12345;
            this.Text = "格式: " + ExtratFormatStr + "  " + i.ToString(ExtratFormatStr);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            FFormatStr = ExtratFormatStr;
            ok = true;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            this.Close();
        }

        private void IEFS_FormatStrForm_Load(object sender, EventArgs e)
        {

        }
    }
}