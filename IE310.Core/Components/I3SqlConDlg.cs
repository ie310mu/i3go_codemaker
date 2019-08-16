/*                     ieSqlCon.ieSqlConDlg
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 返回一个Sql数据库的连接字符串
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                  或者:自定义变量，手动生成与释放
 *                2.调用Execute(string ConnectionString)方法
 *                  成功返回true  然后通过属性ConnectionString返回 
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
    public partial class I3SqlConDlg : Component
    {
        private string connectionString = "";

        /// <summary>
        /// 在Excute返回true后，通过此属性可返回连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return ConnectionString;
            }
        }

        public I3SqlConDlg()
        {
            InitializeComponent();
            //this.Visible = false;
        }

        /// <summary>
        /// 弹出一个窗口，以返回一个SQL连接字符串，返回true后，可通过ConnectionString属性获取连接字符串
        /// aConnectionString:老的连接字符串
        /// 
        /// 错误处理：屏蔽
        /// 
        /// </summary>
        public bool Excute(string aConnectionString)
        {
            return I3SqlConForm.Excute(aConnectionString, out aConnectionString);
        }
    }
}