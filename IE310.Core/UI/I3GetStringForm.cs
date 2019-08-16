using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IE310.Core.Utils;

namespace IE310.Core.UI
{
    public partial class I3GetStringForm : Form
    {
        bool ok = false;

        private I3GetStringForm()
        {
            InitializeComponent();
        }

        private bool canNull = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (!canNull && textBox1.Text == "")
            {
                I3MessageHelper.ShowWarning("请输入名称!");
                return;
            }
            ok = true;
            this.Close();
        }

        /// <summary>
        /// 显示窗口以获取一个字符串，获取失败时返回false
        /// isPassWord参数指定是否以获取密码的形式
        /// oldtext传入老字符串，newtext返回新字符串
        /// 
        /// 错误处理：无
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="oldtext"></param>
        /// <param name="newtext"></param>
        /// <param name="isPassWord"></param>
        /// <returns></returns>
        public static bool Excute(string caption, string oldtext, out string newtext, bool isPassWord, bool canNull)
        {
            I3GetStringForm getName = new I3GetStringForm();
            try
            {
                getName.Text = caption;
                getName.label1.Text = caption + ":";
                getName.textBox1.Text = oldtext;
                getName.canNull = canNull;

                if (isPassWord)
                {
                    getName.textBox1.PasswordChar = '*';
                }

                getName.ShowDialog();
                string s = getName.textBox1.Text;
                getName.Dispose();

                if (getName.ok)
                {
                    newtext = s;
                    return true;
                }
                else
                {
                    newtext = "";
                    return false;
                }
            }
            finally
            {
                getName.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = false;
            this.Close();
        }

        private void GetName_Load(object sender, EventArgs e)
        {

        }
    }
}