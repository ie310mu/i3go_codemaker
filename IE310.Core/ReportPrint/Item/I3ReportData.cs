using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;
using IE310.Core.ReportPrint.Item;
using IE310.Core.Json;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// 报表数据
    /// </summary>
    [Serializable]
    public class I3ReportData
    {
        #region 构造

        public I3ReportData()
            : this(0, 0)
        {
        }

        /// <summary>
        /// 定义行列信息
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        public I3ReportData(int rowCount, int colCount)
        {
            rows = new I3ReportRow[rowCount];
            for (int i = 0; i < rows.Length; i++)  //创建行
            {
                rows[i] = new I3ReportRow(i, colCount);
            }

            cols = new I3ReportCol[colCount];
            for (int j = 0; j < cols.Length; j++)  //创建列
            {
                cols[j] = new I3ReportCol(j);
            }
        }
        #endregion

        #region 行、列、单元格

        public string Name { get; set; }

        private I3ReportRow[] rows;
        /// <summary>
        /// 行高度
        /// </summary>
        public I3ReportRow[] Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
            }
        }

        public I3ReportRow this[int index]
        {
            get
            {
                return rows[index];
            }
        }

        private I3ReportCol[] cols;
        /// <summary>
        /// 列宽度
        /// </summary>
        public I3ReportCol[] Cols
        {
            get
            {
                return cols;
            }
            set
            {
                cols = value;
            }
        }

        /// <summary>
        /// 行数量
        /// </summary>
        public int RowCount
        {
            get
            {
                return rows == null ? 0 : rows.Length;
            }
        }

        /// <summary>
        ///列数量
        /// </summary>
        public int ColCount
        {
            get
            {
                return cols == null ? 0 : cols.Length;
            }
        }

        /// <summary>
        /// 获取单元格的文本信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string GetCellText(int row, int col)
        {
            I3ReportCell cell = GetCellItem(row, col);
            return cell == null ? null : cell.Text;
        }

        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns></returns>
        public I3ReportCell GetCellItem(int row, int col)
        {
            if (row >= 0 && row < RowCount && col >= 0 && col < ColCount)
            {
                return this[row][col];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取或创建文本单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public I3ReportCell GetOrCreateCellItem(int row, int col)
        {
            I3ReportCell item = GetCellItem(row, col);
            if (item == null)
            {
                item = new I3ReportCell(row, col);
                this[row][col] = item;
            }
            return item;
        }


        /// <summary>
        /// 获取或创建斜线单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public I3ReportSlashCell GetOrCreateSlashCellItem(int row, int col)
        {
            I3ReportSlashCell item = GetCellItem(row, col) as I3ReportSlashCell;
            if (item == null)
            {
                item = new I3ReportSlashCell(row, col);
                this[row][col] = item;
            }
            return item;
        }

        /// <summary>
        /// 获取或创建带图片的单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public I3ReportImageCell GetOrCreateImageItem(int row, int col)
        {
            I3ReportImageCell item = GetCellItem(row, col) as I3ReportImageCell;
            if (item == null)
            {
                item = new I3ReportImageCell(row, col);
                this[row][col] = item;
            }
            return item;
        }

        /// <summary>
        /// 转换为图片单元格，复制所有属性（目的是为了在ReportData经过json转换后，找回Cell中丢失的单元格类型信息，要求I3ReportImageCell代码中的属性，全部写在I3ReportCell中）
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public I3ReportImageCell ConvertToImageItem(int row, int col)
        {
            I3ReportCell old = GetCellItem(row, col);
            if(old != null && old is I3ReportImageCell)
            {
                return old as I3ReportImageCell;
            }

            I3ReportImageCell item = null;
            if (old == null)
            {
                item = new I3ReportImageCell(row, col);
            }
            else
            {
                string json = I3JsonConvert.ToJson(old);
                item = (I3ReportImageCell)I3JsonConvert.FromJson(json, typeof(I3ReportImageCell));
            }
            this[row][col] = item;

            return item;
        }

        #endregion

        #region 样式

        private Dictionary<string, I3ReportCellStyle> cellStyles = new Dictionary<string, I3ReportCellStyle>();
        /// <summary>
        /// 样式列表
        /// </summary>
        public I3ReportCellStyle[] CellStyles
        {
            get
            {
                I3ReportCellStyle[] styles = new I3ReportCellStyle[cellStyles.Values.Count];
                cellStyles.Values.CopyTo(styles, 0);
                return styles;
            }
            set
            {
                cellStyles.Clear();
                if (value != null)
                {
                    foreach (I3ReportCellStyle style in value)
                    {
                        cellStyles.Add(style.Name, style);
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否包含某个样式
        /// </summary>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public bool ContainsStyle(string styleName)
        {
            if (string.IsNullOrEmpty(styleName))
            {
                return false;
            }
            return cellStyles.ContainsKey(styleName);
        }

        public I3ReportCellStyle GetCellStyle(string styleName)
        {
            if (!ContainsStyle(styleName))
            {
                return null;
            }
            return cellStyles[styleName];
        }

        /// <summary>
        /// 增加单元样式属性
        /// </summary>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public I3ReportCellStyle AddCellStyle(string styleName)
        {
            I3ReportCellStyle style = new I3ReportCellStyle(styleName);
            cellStyles.Add(styleName, style);
            return style;
        }
        /// <summary>
        /// 增加单元样式属性
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public I3ReportCellStyle AddCellStyle(I3ReportCellStyle style)
        {
            if (string.IsNullOrEmpty(style.Name))
            {
                throw new Exception("styleName不能为空");
            }
            if (cellStyles.ContainsKey(style.Name))
            {
                cellStyles.Remove(style.Name);
            }
            cellStyles.Add(style.Name, style);
            return style;
        }
        #endregion

        #region 合并区域

        private Dictionary<int, Dictionary<int, I3MergeRange>> mergeDic;

        public void RemoveMergeRange(int row, int col)
        {
            if (mergeDic == null)
            {
                return;
            }
            if (!mergeDic.ContainsKey(row))
            {
                return;
            }
            if (!mergeDic[row].ContainsKey(col))
            {
                return;
            }
            mergeDic[row].Remove(col);
        }

        public I3MergeRange GetMergeRange(int row, int col)
        {
            if (mergeDic == null)
            {
                return null;
            }
            if (!mergeDic.ContainsKey(row))
            {
                return null;
            }
            if (!mergeDic[row].ContainsKey(col))
            {
                return null;
            }
            return mergeDic[row][col];
        }
        public I3MergeRange[] MergeRanges
        {
            get
            {
                if (mergeDic == null)
                {
                    return null;
                }
                List<I3MergeRange> list = new List<I3MergeRange>();
                foreach (int row in mergeDic.Keys)
                {
                    Dictionary<int, I3MergeRange> item = mergeDic[row];
                    foreach (int col in item.Keys)
                    {
                        list.Add(item[col]);
                    }
                }
                return list.ToArray();
            }
            set
            {
                if (mergeDic != null)
                {
                    mergeDic.Clear();
                }
                if (value != null)
                {
                    foreach (I3MergeRange mr in value)
                    {
                        AddMergeRange(mr.StartRow, mr.StartCol, mr.EndRow, mr.EndCol);
                    }
                }
            }
        }

        /// <summary>
        /// 增加一个合并区域
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        public void AddMergeRange(int startRow, int startCol, int endRow, int endCol)
        {
            if (mergeDic == null)
            {
                mergeDic = new Dictionary<int, Dictionary<int, I3MergeRange>>();
            }
            if (!mergeDic.ContainsKey(startRow))
            {
                mergeDic.Add(startRow, new Dictionary<int, I3MergeRange>());
            }
            if (!mergeDic[startRow].ContainsKey(startCol))
            {
                mergeDic[startRow].Add(startCol, null);
            }

            mergeDic[startRow][startCol] = new I3MergeRange(startRow, startCol, endRow, endCol);
        }

        /// <summary>
        /// 单元格为合并单元格时，获取合并单元格的第一个格子
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public I3ReportCell GetMergedStartedCell(int row, int col)
        {
            I3ReportCell cell = this.GetCellItem(row, col);
            if (cell == null || cell.MergState != I3MergeState.Merged)
            {
                return null;
            }

            foreach (I3MergeRange range in MergeRanges)
            {
                if (range.StartRow <= row && row <= range.EndRow && range.StartCol <= col && col <= range.EndCol)
                {
                    return this.GetCellItem(range.StartRow, range.StartCol);
                }
            }

            return null;
        }
        #endregion

        #region 页面设置、分页

        private I3PageSetting pageSetting = null;
        /// <summary>
        /// 页面设置
        /// </summary>
        public I3PageSetting PageSetting
        {
            get
            {
                if (pageSetting == null)
                {
                    pageSetting = I3PageSetting.Default;
                }
                return pageSetting;
            }
            set
            {
                pageSetting = value;
            }
        }
        [NonSerialized]
        private I3PrintAreas printAreas = null;
        /// <summary>
        /// 经过计算后的打印区域数据，一页对应一个PrintArea  不序列化
        /// </summary>
        public I3PrintAreas PrintAreas
        {
            get
            {
                return printAreas;
            }
        }

        internal void SetPrintAreas(I3PrintAreas printAreas)
        {
            this.printAreas = printAreas;
        }

        /// <summary>
        /// 重新计算报表大小和分页信息
        /// </summary>
        public void ReCalSizeAndPageInfo()
        {
            I3ReportPrintController.RePaging(this);
            ReSetMergeCellsInDifPage();
        }

        /// <summary>
        /// 重置合并单元格位于多页的情况（分解成多个格子）
        /// </summary>
        private void ReSetMergeCellsInDifPage()
        {
            I3ReportPrintController.ReSetMergeCellsInDifPage(this);
        }


        private int pageIndexStart = 1;
        /// <summary>
        /// 页码的开始序号
        /// </summary>
        public int PageIndexStart
        {
            get
            {
                return pageIndexStart;
            }
            set
            {
                pageIndexStart = value;
            }
        }

        private int totalPageCount = -1;

        public int TotalPageCount
        {
            get
            {
                if (totalPageCount == -1)
                {
                    //没有指定值，默认返回本数据的总页数
                    if (PrintAreas == null || PrintAreas.Dic == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return PrintAreas.Dic.Count;
                    }
                }
                return totalPageCount;
            }
            set
            {
                totalPageCount = value;
            }
        }

        #endregion

        #region 自适应

        /// <summary>
        /// 根据内容调整页面高度为一页(自定义字纸张时才有效)
        /// </summary>
        public void AdjustPaperHeightToOnePage()
        {
            if (this.PageSetting.PaperType != PaperType.Custom)
            {
                throw new Exception("纸张类型为Custom时，才可调用AdjustPaperHeightToOnePage方法");
            }

            this.ReCalSizeAndPageInfo();
            float totalHeight = 0;
            foreach (I3ReportRow row in this.Rows)
            {
                totalHeight += row.Height;
            }


            float customHeight = this.PageSetting.CustomPaperHeightPX - this.PageSetting.PaperContentRect.Height + totalHeight;
            this.PageSetting.CustomPaperHeightMM = PaperSizeHelper.PX2MM(customHeight + 1);
        }


        /// <summary>
        /// 根据内容调整页面宽度为一页(自定义字纸张时才有效)
        /// </summary>
        public void AdjustPaperWidthToOnePage()
        {
            if (this.PageSetting.PaperType != PaperType.Custom)
            {
                throw new Exception("纸张类型为Custom时，才可调用AdjustPaperWidthToOnePage方法");
            }

            this.ReCalSizeAndPageInfo();
            float totalWidth = 0;
            foreach (I3ReportCol col in this.cols)
            {
                totalWidth += col.Width;
            }


            float customWidth = this.PageSetting.CustomPaperWidthPX - this.PageSetting.PaperContentRect.Width + totalWidth;
            this.PageSetting.CustomPaperWidthMM = PaperSizeHelper.PX2MM(customWidth + 1);
        }

        #endregion


        public object UserData
        {
            get;
            set;
        }

        public I3PageHeader PageHeader { get; set; }
        public I3PageHeader PageFooter { get; set; }

        private bool printQrCode = true;
        /// <summary>
        /// 是否打印二维码
        /// </summary>
        public bool PrintQrCode
        {
            get
            {
                return printQrCode;
            }
            set { printQrCode = value; }
        }
    }
}
