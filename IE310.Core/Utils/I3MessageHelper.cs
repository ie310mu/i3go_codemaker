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
        /// �Ƿ�������ϵ���ģʽ
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
        /// �Ƿ��Է���ģʽ����
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
        /// ������־��
        /// </summary>
        protected static I3LocalLogUtil logger = I3LocalLogUtil.Current;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender">��Ϣ������</param>
        /// <param name="information">��Ϣ</param>
        public static void ShowInfo(IWin32Window owner, string information)
        {
            if (RunAsService)
            {
                logger.WriteInfoLog(information);
            }
            else
            {
                MessageBox.Show(owner, information, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="information">��Ϣ</param>
        public static void ShowInfo(string information)
        {
            ShowInfo(null, information);
        }


        /// <summary>
        /// ����ѯ�� 
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="information">��Ϣ</param>
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
                return MessageBox.Show(sender, information, "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }
        }

        /// <summary>
        /// ����ѯ��
        /// </summary>
        /// <param name="information">��Ϣ</param>
        /// <returns></returns>
        public static bool ShowQuestion(string information)
        {
            return ShowQuestion(null, information);
        }

        /// <summary>
        /// ����ѯ��
        /// �Զ��尴ť��Ĭ�ϰ�ť
        /// </summary>
        /// <param name="information">��Ϣ</param>
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
        /// ����ѯ��
        /// �Զ��尴ť��Ĭ�ϰ�ť
        /// </summary>
        /// <param name="information">��Ϣ</param>
        /// <returns></returns>
        public static DialogResult ShowQuestion(string sTitle, string information, MessageBoxButtons messageBoxButtons)
        {
            return ShowQuestion(sTitle, information, messageBoxButtons, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="information">��Ϣ</param>
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
                    MessageBox.Show(information, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="information">��Ϣ</param>
        /// <param name="ex">�쳣</param>
        public static void ShowError(string information, Exception ex)
        {
            ShowError(null, information, ex);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="information">��Ϣ</param>
        public static void ShowError(string information)
        {
            ShowError(information, null);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        public static void ShowError(Form sender, string information)
        {
            ShowError(sender, information, null);
        }

        /// <summary>
        /// ������  AbortRetryIgnore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static DialogResult ProcessWarning(Form sender, string information)
        {
            //��־����
            logger.WriteInfoLog(information);
            if (RunAsService)
            {
                return DialogResult.Ignore;
            }
            else
            {
                return MessageBox.Show(sender, information, "����", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
            }
        }

        
        /// <summary>
        /// ������  AbortRetryIgnore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static DialogResult ProcessWarning(string information)
        {
            return ProcessWarning(null, information);
        }

        /// <summary>
        /// ��ʾ����  OK
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
                MessageBox.Show(sender, information, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        /// <summary>
        /// ��ʾ����  OK
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
