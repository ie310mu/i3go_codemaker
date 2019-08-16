/*                     ieGetSymbol.IEFS_GetSymbolForm
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 返回一个符号
 * 
 *      使用方法: 调用static bool Execute(out string selectSymbol)
 * 
 *      使用条件: 与此类相应的，有一个外部配置文件Symbol.set，请将此文件放在exe同目录下
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
using System.Collections;
using IE310.Core.Utils;
using System.IO;
using IE310.Table.Models;
using IE310.Table.Column;
using IE310.Table.Row;
using IE310.Table.Cell;

namespace IE310.Core.UI
{
    public partial class I3GetSymbolForm : Form
    {
        private DataSet dataSet = new DataSet();
        private DataTable newDataTable = new DataTable("text");
        private ArrayList nameList, textList;
        private bool busy;
        private bool ok = false;
        private string selectSymbol = "";

        /// <summary>
        /// 弹出窗口以选择一个符号，没有选择时返回false
        /// 通过selectSymbol返回选择的符号
        /// 
        /// 错误处理：MessageBox
        /// 
        /// </summary>
        /// <param name="selectSymbol"></param>
        /// <returns></returns>
        public static bool Excute(out string selectSymbol)
        {
            using (I3GetSymbolForm form = new I3GetSymbolForm())
            {
                form.ShowDialog();
                selectSymbol = form.selectSymbol;
                return form.ok;
            }
        }



        private I3GetSymbolForm()
        {
            InitializeComponent();
        }

        private void TfrmGetSymbol_Load(object sender, EventArgs e)
        {
            busy = true;

            try
            {
                nameList = new ArrayList();
                textList = new ArrayList();

                #region 读取数据，并检查数据正确性
                try
                {
                    dataSet.ReadXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Symbol.set"));
                }
                catch (Exception ex)
                {
                    I3MessageHelper.ShowError(ex.Message, ex);
                    return;
                }

                if (dataSet.Tables.Count == 0)
                {
                    return;
                }
                if (dataSet.Tables[0].Columns.Count == 0)
                {
                    return;
                }
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                #endregion

                #region 写入默认数据
                /*dataSet.Tables[0].Columns[0].ColumnName = "数学序号";
                dataSet.Tables[0].Columns[1].ColumnName = "数学符号";
                dataSet.Tables[0].Columns[2].ColumnName = "单位符号";
                dataSet.Tables[0].Columns[3].ColumnName = "特殊符号";
                dataSet.Tables[0].Rows[0][0] = "①②③④⑤⑥⑦⑧⑨⑩ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅪⅫ";
                dataSet.Tables[0].Rows[0][1] = "×÷≠≤≥≮≯±∽≈≌⊕⊙∈∏∑∝∞∫∮";
                dataSet.Tables[0].Rows[0][2] = "℃℉﹪‰￥№㎡23㎝㎞㏎㎎㎏㏄";
                dataSet.Tables[0].Rows[0][3] = "Ф★☆○☉◎●▲△▼▽■□♀♂《》「」『』〖〗【】";
                dataSet.WriteXml(AppDomain.CurrentDomain.BaseDirectory + @"\Symbol.set");*/
                #endregion

                #region 将数据保存到动态数组中
                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    nameList.Add(dataSet.Tables[0].Columns[i].ColumnName);
                    textList.Add(dataSet.Tables[0].Rows[0][i].ToString());
                }
                cbbSymbolStyle.DataSource = nameList;
                #endregion
                
                #region 增加20列
                for (int i = 0; i < 20; i++)
                {
                    DataColumn newColumn = new DataColumn("col" + (i + 1).ToString(), typeof(string));
                    newDataTable.Columns.Add(newColumn);

                    I3TextColumn textColumn = new I3TextColumn();
                    textColumn.Width = Convert.ToInt32(this.i3Table.Width / 20);
                    textColumn.Sortable = false;
                    textColumn.Editable = false;
                    this.i3ColumnModel.Columns.Add(textColumn);
                }
                #endregion

                #region 得到完全字符串
                string s = "";
                for (int i = 0; i < textList.Count; i++)
                {
                    s = s + textList[i].ToString();
                }
                #endregion

                #region 得到行数，并生成DataRow
                int rowcount = s.Length / 20;
                if (s.Length % 20 > 0)
                {
                    rowcount++;
                }
                for (int i = 0; i < rowcount; i++)
                {
                    DataRow newRow = newDataTable.NewRow();
                    newDataTable.Rows.Add(newRow);
                }
                #endregion

                #region 给每行每列赋值
                int row, col;
                for (int i = 0; i < s.Length; i++)
                {
                    row = (i + 1) / 20;//整除
                    col = (i + 1) % 20;//求余
                    if (col == 0)
                    {
                        row--;
                        col += 20;
                    }
                    col--;
                    newDataTable.Rows[row].BeginEdit();
                    newDataTable.Rows[row][col] = I3StringUtil.SubString(s, i, 1);
                    newDataTable.Rows[row].EndEdit();
                }
                foreach (DataRow dataRow in newDataTable.Rows)
                {
                    I3Row i3Row = new I3Row();
                    this.i3TableModel.Rows.Add(i3Row);
                    foreach (DataColumn column in newDataTable.Columns)
                    {
                        I3Cell i3Cell = new I3Cell(dataRow[column].ToString());
                        i3Row.Cells.Add(i3Cell);
                    }
                }
                #endregion

                this.i3TableModel.Selections.SelectCell(0, 0);
            }
            finally
            {
                busy = false;
            }
        }

        private void TfrmGetSymbol_FormClosed(object sender, FormClosedEventArgs e)
        {
            //释放对象
            if (dataSet != null)
            {
                dataSet.Dispose();
            }
            if (newDataTable != null)
            {
                newDataTable.Dispose();
            }
        }

        private void cbbSymbolStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (busy)
            {
                return;
            }

            int index = nameList.IndexOf(cbbSymbolStyle.Text);
            if (index == -1)
            {
                return;
            }

            int len = 0;
            for (int i = 0; i < index; i++)
            {
                len += textList[i].ToString().Length;
            }

            int row, col;
            row = (len + 1) / 20;//整除
            col = (len + 1) % 20;//求余
            if (col == 0)
            {
                row--;
                col += 20;
            }
            col--;

            this.i3TableModel.Selections.Clear();
            this.i3TableModel.Selections.SelectCell(row, col);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            selectSymbol = SelectSymbol;
            if (string.IsNullOrEmpty(selectSymbol))
            {
                return;
            }

            ok = true;
            Close();
        }

        private string SelectSymbol
        {
            get
            {
                return this.i3TableModel.Selections.SelectedItems[0].SelectedItems[0].Data.ToString();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btOK_Click(null, null);
        }

        private void i3Table_CellDoubleClick(object sender, Table.Events.I3CellMouseEventArgs e)
        {
            btOK_Click(null, null);
        }
    }
}