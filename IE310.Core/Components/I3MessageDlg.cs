/*                     IE310_FuncServer.IECT_MessageDlg
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 弹出一个窗口供选择，以显示一个消息，此窗口不可人工关闭
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                  或者:自定义变量，手动生成与释放
 *                2.通过Message属性设置要显示的消息
 *                3.调用Execute方法以显示
 *                4.调用Hide隐藏
 * 
 *      修改记录: 1.
 *  
 *                                                  Created by ie
 *                                                            2008-01-01      
 * 
 * */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using IE310.Core.UI;

namespace IE310.Core.Components
{
    public partial class I3MessageDlg : Component
    {
        private I3MessageForm form = I3MessageForm.CreateMessageDialog("");

        private string message = "";

        /// <summary>
        /// 设置消息提示窗口上要显示的消息
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                form.Message = message;
            }
        }

        /// <summary>
        /// 弹出一个消息提示窗口，这个窗口上显示Message属性所表示的字符串
        /// 
        /// 错误处理:无
        ///
        /// </summary>
        public void Excute()
        {
            form.Visible = true;
            Application.DoEvents();
        }

        /// <summary>
        /// 隐藏消息提示窗口
        /// </summary>
        public void Hide()
        {
            form.Visible = false;
            Application.DoEvents();
        }

        public I3MessageDlg()
        {
            InitializeComponent();
        }
    }
}
