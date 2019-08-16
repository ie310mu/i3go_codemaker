/*                     IE310_FuncServer.IECT_ProcessDlg
 * 
 * 
 *          类型: 组件
 * 
 *          说明: 用于显示进度消息
 * 
 *      使用方法: 1.属性Max设置最大值
 *                2.属性Message设置当前的提示消息
 *                3.属性CloseMessage设置关闭时的警告消息，不设置则不能关闭
 *                4.属性Position设置当前的位置
 *                5.通过属性OnWork=false得知当前代码应该结束处理
 * 
 *      注意事项: 
 * 
 *      修改记录: 
 *  
 *                                                  Created by ie
 *                                                            2010-12-03      
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
    public partial class I3ProgressDlg : Component
    {
        private I3ProgressForm form = I3ProgressForm.CreateMessageProcessDialog();

        /// <summary>
        /// 进度条的最大值属性
        /// </summary>
        public int Max
        {
            get
            {
                return form.max;
            }
            set
            {
                form.max = value;
            }
        }

        /// <summary>
        /// 进度条上方当前显示的消息
        /// </summary>
        public string Message
        {
            get
            {
                return form.message;
            }
            set
            {
                form.message = value;
            }
        }

        /// <summary>
        /// 关闭窗口时显示的警告消息，不设置时默认消息为停止工作
        /// </summary>
        public string CloseMessage
        {
            get
            {
                return form.closeMessage;
            }
            set
            {
                form.closeMessage = value;
            }
        }

        /// <summary>
        /// 进度条的当前位置
        /// </summary>
        public int Position
        {
            set
            {
                form.SetPosition(value);
            }
        }

        /// <summary>
        /// 当CanCloseByUser为true时，工作可以用户停止，此属性指示现在是否还在工作状态
        /// </summary>
        public bool OnWork
        {
            get
            {
                return form.Visible;
            }
        }

        /// <summary>
        /// 指定进度条提示窗口是否可以由用户停止工作
        /// 如果为true，可通过OnWork属性=false来得知显示停止工作
        /// </summary>
        public bool CanCloseByUser
        {
            get
            {
                return form.CanCloseByUser;
            }
            set
            {
                form.CanCloseByUser = value;
            }
        }

        /// <summary>
        /// 弹出一个进度条提示窗口
        /// 注意：需要先进行初始化
        /// 
        /// 错误处理：无
        /// 
        /// </summary>
        public void Excute()
        {
            form.Visible = true;
            Application.DoEvents();
        }

        /// <summary>
        /// 隐藏进度条提示窗口 
        /// </summary>
        public void Hide()
        {
            form.Visible = false;
            Application.DoEvents();
        }


        public I3ProgressDlg()
        {
            InitializeComponent();
        }
    }
}
