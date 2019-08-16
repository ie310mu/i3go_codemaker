/*


补充说明:      1.如果是在线程中，需要在线程类中实现下面的代码:
               
               IECT_BubbleMessage message = new IECT_BubbleMessage("caption", "zcxvzxcvzxcvzxcvzxcvzxcv", true);
               Thread thread = new Thread(new ThreadStart(message.ShowMessageBox));
               thread.IsBackground = true;
               thread.Start();


                因为窗口的创建只能在主线程中进行

                2.本窗口是ShowModal模式，且只有确定、取消按钮，如果需要其他模式，可以从TfrmBubble另行扩展
    　　　　　　　或者自己扩展修改BubbleMessageBox函数
*/


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
    public partial class I3BubbleMessageDlg : Component
    {
        public I3BubbleMessageDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 执行功能 返回结果  为DialogResult.Yes或者DialogResult.NO
        /// </summary>
        /// <param name="aCaption"></param>
        /// <param name="aMessage"></param>
        /// <param name="aHideCancelButton"></param>
        /// <returns></returns>
        public DialogResult Excute(string aCaption, string aMessage, bool aHideCancelButton)
        {
            IECT_BubbleMessage message = new IECT_BubbleMessage(aCaption, aMessage, aHideCancelButton);
            message.ShowMessageBox();
            return message.Result;
        }
    }



    public class IECT_BubbleMessage
    {
        public string Caption;
        public string Message;
        public bool HideCancelButton;

        private DialogResult result;
        /// <summary>
        /// 返回结果  为DialogResult.Yes或者DialogResult.NO
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return result;
            }
        }

        public IECT_BubbleMessage(string aCaption, string aMessage, bool aHideCancelButton)
        {
            Caption = aCaption;
            Message = aMessage;
            HideCancelButton = aHideCancelButton;
        }

        public void ShowMessageBox()
        {
            using (I3BubbleMessageForm form = new I3BubbleMessageForm())
            {
                form.Init(5, 100, false, false, 3000);
                form.InitMessage(Caption, Message, HideCancelButton);
                form.ShowDialog();
                result = form.DialogResult;
            }
        }
    }
}
