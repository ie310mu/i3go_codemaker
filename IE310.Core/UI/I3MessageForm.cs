/*                     ieMessage.IEFS_MessageForm
 * 
 * 
 *          类型: 界面类
 * 
 *          说明: 用于操作消息
 * 
 *      使用方法: 1.用CreateMessageDialog生成TfrmMessage对象 会自动显示:Show
 *                2.使用完毕后Dispose()
 * 
 *      注意事项: 1.当多次使用本窗口，比如显示多个文件的压缩进度时，可以只生成一次本窗口
 *                  压缩一个新文件时，SetPosition(0)即可。
 *                2.同上，多次使用本窗口时，可以利用窗口的visible属性来得知是否应该中止操作
 *                  其实现原理是在窗口的关闭事件中记录变量toClose = true，当本次操作完成即pro.postion=100时隐藏窗口
 *                  可以通过closeMessage来控制关闭事件中是否需要询问，closeMessage=null时不进行询问
 * 
 *      修改记录: 1.
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

namespace IE310.Core.UI
{
    public partial class I3MessageForm : Form
    {
        private I3MessageForm()
        {
            InitializeComponent();
        }

        public string Message
        {
            set
            {
                label1.Text = value;
            }
        }

        /// <summary>
        /// 生成消息窗口对象，并初始化，返回IEFS_MessageForm
        /// message即为需要显示的消息
        /// 注意：生成后需要调用Show()方法以进行显示
        /// 
        /// 错误处理：无
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static I3MessageForm CreateMessageDialog(string message)
        {
            I3MessageForm form = new I3MessageForm();
            form.TopMost = true;
            form.label1.Text = message;

            return form;
        }

        private void TfrmMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}