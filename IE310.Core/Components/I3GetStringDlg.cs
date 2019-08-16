/*                     IEFS_GetANameDlg.IEFS_GetANameDlg
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 弹出一个对话框，得到用户输入的字符串
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                  或者:自定义变量，手动生成与释放
 *                2.调用Execute(string caption, string text)方法
 *                  成功返回true  然后通过属性Str返回字符串
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IE310.Core.UI;

namespace IE310.Core.Components
{
    public partial class I3GetStringDlg : Component
    {
        private string str = "";

        /// <summary>
        /// 在调用Execute返回true后，可通过此属性返回字符串
        /// </summary>
        public string Str
        {
            get
            {
                return str;
            }
        }

        public I3GetStringDlg()
        {
            InitializeComponent();
            //this.Visible = false;
        }

        /// <summary>
        /// 显示一个窗口，以获取一个字符串，返回true后可通过属性Str得到该字符串
        /// isPassWord:是否以获取密码的形式
        /// 
        /// 错误处理：无
        ///
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="text"></param>
        /// <param name="isPassWord"></param>
        /// <returns></returns>
        public bool Excute(string caption, string text, bool isPassWord, bool canNull)
        {
            return I3GetStringForm.Excute(caption, text, out str, isPassWord, canNull);
        }
    }
}