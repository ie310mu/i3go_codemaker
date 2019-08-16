/*                     ieMessage.IEFS_ProcessForm
 * 
 * 
 *          类型: 界面类
 * 
 *          说明: 用于显示进度消息
 * 
 *      使用方法: 1.用CreateMessageProcessDialog生成TfrmMessageProcess对象
 *                2.设置属性max
 *                3.设置属性message,closemessage
 *                4.通过调用SetPosition(int)来设置当前进度
 *                  通过visible=false来知道应该退出操作
 *                5.使用完毕后Dispose()
 * 
 *      注意事项: 1.当多次使用本窗口，比如显示多个文件的压缩进度时，可以只生成一次本窗口
 *                  压缩一个新文件时，SetPosition(0)即可。
 *                2.同上，多次使用本窗口时，可以利用窗口的visible属性来得知是否应该中止操作
 *                  其实现原理是在窗口的关闭事件中记录变量toClose = true，当本次操作完成即pro.postion=100时隐藏窗口
 *                  可以通过closeMessage来控制关闭事件中是否需要询问，closeMessage=null时不进行询问
 * 
 *      修改记录: 1.给fmax赋值时，判断if fmax <= 0 then fmax = 1                  modifed by id 2008-04-04
 *  
 *                                                  Created by ie
 *                                                            2008-03-31      
 * 
 * */



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
    public partial class I3ProgressForm : Form
    {
        private int fmax;
        private int fposition;
        public string closeMessage = "是否停止工作？";

        private I3ProgressForm()
        {
            InitializeComponent();
        }

        private void TfrmMessageProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            //不让窗口自己关闭，必须使e.Cancel=true
            e.Cancel = true;

            //不能被用户关闭
            if (!canCloseByUser)
            {
                return;
            }

            //提示，且选择“否”时，不让处理函数关闭窗口
            if (I3MessageHelper.ShowQuestion(closeMessage))
            {
                Visible = false;
            }
        }

        /// <summary>
        /// 设置进度条的当前位置
        /// </summary>
        /// <param name="po"></param>
        public void SetPosition(int po)
        {
            fposition = po;

            if (fposition == 0)
            {
                this.pb.Value = 0;
                Application.DoEvents();
            }

            double d = (double)fposition / (double)max;
            d = d * 100;
            int bl = Convert.ToInt32(d);
            if (bl > this.pb.Value)
            {
                this.pb.Value = bl;
                Application.DoEvents();
            }
        }

        private bool canCloseByUser = false;
        public bool CanCloseByUser
        {
            get
            {
                return canCloseByUser;
            }
            set
            {
                canCloseByUser = value;
            }
        }

        /// <summary>
        /// 设置进度条上方显示的消息，可随时更改
        /// </summary>
        public string message
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 设置进度条的最大值
        /// </summary>
        public int max
        {
            get
            {
                return fmax;
            }
            set
            {
                fmax = value;
                if (fmax <= 0)
                    fmax = 1;
            }
        }

        /// <summary>
        /// 将进度条的值增加add个值
        /// </summary>
        /// <param name="add"></param>
        public void AddPosition(int add)
        {
            fposition += add;
            double d = (double)fposition / (double)max;
            d = d * 100;
            int bl = Convert.ToInt32(d);
            if (bl > this.pb.Value)
            {
                this.pb.Value = bl;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 生成进度条窗口对象，并初始化，返回IEFS_ProcessForm
        /// 注意：先需要用其他属性进行初始化，显示时需要调用Show()方法
        /// 
        /// 错误处理：无
        /// 
        /// </summary>
        /// <returns></returns>
        public static I3ProgressForm CreateMessageProcessDialog()
        {
            I3ProgressForm form = new I3ProgressForm();
            form.label1.Text = "";
            form.fposition = 0;
            form.pb.Maximum = 100;
            form.pb.Minimum = 0;
            form.pb.Value = 0;
            form.TopMost = true;

            return form;
        }

        private void progressBarControl1_PositionChanged(object sender, EventArgs e)
        {
            if (this.pb.Value == 100)
            {
                Visible = false;
            }
        }
    }
}