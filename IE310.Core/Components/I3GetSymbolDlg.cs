/*                     IE310_FuncServer.IECT_GetSymbolDlg
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 弹出一个窗口供选择，以返回一个符号
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                  或者:自定义变量，手动生成与释放
 *                2.调用Execute方法
 *                  成功返回true  然后通过属性Symbol返回 
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
    public partial class I3GetSymbolDlg : Component
    {
        private string symbol = "";

        /// <summary>
        /// 在调用Excute返回true后，可通过此属性来返回选择的符号
        /// </summary>
        public string Symbol
        {
            get
            {
                return symbol;
            }
        }

        public I3GetSymbolDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 弹出一个窗口，以选择一个符号，返回true后，可通过属性Symbol来获取选择的符号
        /// 
        /// 错误处理：无
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Excute()
        {
            return I3GetSymbolForm.Excute(out symbol);
        }
    }
}
