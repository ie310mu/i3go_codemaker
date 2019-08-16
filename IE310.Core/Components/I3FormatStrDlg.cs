/*                     ieFormatStr.IEFS_FormatStrDlg
 * 
 * 
 *          类型: 组件
 * 
 *          说明: 得到数字格式化字符串
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                  或者:自定义变量，手动生成与释放
 *                2.调用Execute方法，传入一个格式字符串，也可以传入""
 *                  成功Execute返回true  然后通过属性FormatStr获取字符串
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
    public partial class I3FormatStrDlg : Component
    {
        private string formatStr = "";

        /// <summary>
        /// 在调用Execute返回true后通过此属性返回一个格式化字符串
        /// </summary>
        public string FormatStr
        {
            get
            {
                return formatStr;
            }
        }

        public I3FormatStrDlg()
        {
            InitializeComponent();
            //this.Visible = false;
        }

        /// <summary>
        /// 弹出窗口，以获取一个格式化字符串，返回true后可通过属性FormatStr得到新的格式化字符串
        /// aFormatStr:老的格式化字符串
        /// 
        /// 错误处理：屏蔽
        /// 
        /// </summary>
        /// <param name="aFormatStr"></param>
        /// <returns></returns>
        public bool Excute(string aFormatStr)
        {
            return I3FormatStrForm.Excute(aFormatStr, out formatStr);
        }
    }
}