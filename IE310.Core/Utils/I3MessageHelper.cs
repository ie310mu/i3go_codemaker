using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using IE310.Core.Utils;
using IE310.Core.UI;

namespace IE310.Core.Utils
{
    public class I3MessageHelper
    {
        private static bool debugMode = true;
        /// <summary>
        /// 是否启用诊断调试模式
        /// </summary>
        public static bool DebugMode
        {
            get
            {
                return debugMode;
            }
            set
            {
                debugMode = value;
            }
        }


        private static bool runAsService = false;
        /// <summary>
        /// 是否以服务模式运行
        /// </summary>
        public static bool RunAsService
        {
            get
            {
                return runAsService;
            }
            set
            {
                runAsService = value;
            }
        }

        /// <summary>
        /// 构造日志类
        /// </summary>
        protected static I3LocalLogUtil logger = I3LocalLogUtil.Current;

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="sender">消息发送者</param>
        /// <param name="information">消息</param>
        public static void ShowInfo(IWin32Window owner, string information)
        {
            if (RunAsService)
            {
                logger.WriteInfoLog(information);
            }
            else
            {
                MessageBox.Show(owner, information, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="information">消息</param>
        public static void ShowInfo(string information)
        {
            ShowInfo(null, information);
        }


        /// <summary>
        /// 处理询问 
        /// </summary>
        /// <param name="sender">窗体</param>
        /// <param name="information">信息</param>
        /// <returns></returns>
        public static bool ShowQuestion(Form sender, string information)
        {
            if (RunAsService)
            {
                logger.WriteInfoLog(information);
                return false;
            }
            else
            {
                return MessageBox.Show(sender, information, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }
        }

        /// <summary>
        /// 处理询问
        /// </summary>
        /// <param name="information">信息</param>
        /// <returns></returns>
        public static bool ShowQuestion(string information)
        {
            return ShowQuestion(null, information);
        }

        /// <summary>
        /// 处理询问
        /// 自定义按钮和默认按钮
        /// </summary>
        /// <param name="information">信息</param>
        /// <returns></returns>
        public static DialogResult ShowQuestion(string sTitle, string information, MessageBoxButtons messageBoxButtons, MessageBoxDefaultButton defaultButton)
        {
            if (RunAsService)
            {
                logger.WriteInfoLog(information);
                return DialogResult.Cancel;
            }
            else
            {
                return MessageBox.Show(information, sTitle, messageBoxButtons, MessageBoxIcon.Question, defaultButton);
            }
        }


        /// <summary>
        /// 处理询问
        /// 自定义按钮和默认按钮
        /// </summary>
        /// <param name="information">信息</param>
        /// <returns></returns>
        public static DialogResult ShowQuestion(string sTitle, string information, MessageBoxButtons messageBoxButtons)
        {
            return ShowQuestion(sTitle, information, messageBoxButtons, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="sender">窗体</param>
        /// <param name="information">信息</param>
        public static void ShowError(Form sender, string information, Exception ex)
        {
            if (RunAsService)
            {
                logger.WriteInfoLog(information + "   \r\n" + ex.ToString());
            }
            else
            {
                if (DebugMode && ex != null)
                {
                    using (I3ExceptionDialog dialog = new I3ExceptionDialog(information, ex))
                    {
                        dialog.ShowDialog(sender);
                    }
                }
                else
                {
                    MessageBox.Show(information, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="information">信息</param>
        /// <param name="ex">异常</param>
        public static void ShowError(string information, Exception ex)
        {
            ShowError(null, information, ex);
        }

        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="information">信息</param>
        public static void ShowError(string information)
        {
            ShowError(information, null);
        }

        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        public static void ShowError(Form sender, string information)
        {
            ShowError(sender, information, null);
        }

        /// <summary>
        /// 处理警告  AbortRetryIgnore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static DialogResult ProcessWarning(Form sender, string information)
        {
            //日志操作
            logger.WriteInfoLog(information);
            if (RunAsService)
            {
                return DialogResult.Ignore;
            }
            else
            {
                return MessageBox.Show(sender, information, "警告", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
            }
        }

        
        /// <summary>
        /// 处理警告  AbortRetryIgnore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static DialogResult ProcessWarning(string information)
        {
            return ProcessWarning(null, information);
        }

        /// <summary>
        /// 显示警告  OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static void ShowWarning(Form sender, string information)
        {
            if (RunAsService)
            {
                logger.WriteInfoLog(information);
            }
            else
            {
                MessageBox.Show(sender, information, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        /// <summary>
        /// 显示警告  OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static void ShowWarning(string information)
        {
            ShowWarning(null, information);
        }
    }
}
