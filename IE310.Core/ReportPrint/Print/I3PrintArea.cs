using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// 打印区域
    /// </summary>
    public class I3PrintArea
    {
        public I3PrintArea()
            : this(0)
        {
        }

        public I3PrintArea(int index)
        {
            this.index = index;
        }

        private int index = 0;
        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        private SortedList<int, int> headerRows = new SortedList<int, int>();
        /// <summary>
        /// 页眉行
        /// </summary>
        public SortedList<int, int> HeaderRows
        {
            get
            {
                return headerRows;
            }
        }

        private SortedList<int, int> footerRows = new SortedList<int, int>();
        /// <summary>
        /// 页脚行
        /// </summary>
        public SortedList<int, int> FooterRows
        {
            get
            {
                return footerRows;
            }
        }

        private SortedList<int, int> dataRows = new SortedList<int, int>();
        /// <summary>
        /// 此打印区域所需要绘制的行
        /// </summary>
        public SortedList<int, int> DataRows
        {
            get
            {
                return dataRows;
            }
        }


        public int MinDataAreaRowIndex { get; set; }
        public int MaxDataAreaRowIndex { get; set; }
        /// <summary>
        /// 更新最小最大的数据区行号（注意，DataRows中实际存储的还包含标题、表头、表尾，这里排除之）
        /// </summary>
        public void UpdateMinAndMaxDataRowIndex(I3ReportData reportData)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach(int rowIndex in DataRows.Keys)
            {
                if(reportData.Rows[rowIndex].Type != I3RowColType.数据)
                {
                    continue;
                }
                min = Math.Min(min, rowIndex);
                max = Math.Max(max, rowIndex);
            }

            MinDataAreaRowIndex = min;
            MaxDataAreaRowIndex = max;
        }


        public int DataRowCount
        {
            get
            {
                return dataRows.Count;
            }
        }

        /// <summary>
        /// 返回所有的行
        /// </summary>
        public int[] AllRows
        {
            get
            {
                List<int> list = new List<int>();
                list.AddRange(headerRows.Keys);
                list.AddRange(dataRows.Keys);
                list.AddRange(footerRows.Keys);
                return list.ToArray();
            }
        }

        private SortedList<int, int> headerCols = new SortedList<int, int>();
        /// <summary>
        /// 页眉列
        /// </summary>
        public SortedList<int, int> HeaderCols
        {
            get
            {
                return headerCols;
            }
            set
            {
                headerCols = value;
            }
        }

        private SortedList<int, int> footerCols = new SortedList<int, int>();
        /// <summary>
        /// 页脚列
        /// </summary>
        public SortedList<int, int> FooterCols
        {
            get
            {
                return footerCols;
            }
            set
            {
                footerCols = value;
            }
        }

        private SortedList<int, int> dataCols = new SortedList<int, int>();
        /// <summary>
        /// 此打印区域所需要绘制的列
        /// </summary>
        public SortedList<int, int> DataCols
        {
            get
            {
                return dataCols;
            }
            set
            {
                dataCols = value;
            }
        }


        public int DataColCount
        {
            get
            {
                return dataCols.Count;
            }
        }

        /// <summary>
        /// 返回所有的列
        /// </summary>
        public int[] AllCols
        {
            get
            {
                List<int> list = new List<int>();
                list.AddRange(headerCols.Keys);
                list.AddRange(dataCols.Keys);
                list.AddRange(footerCols.Keys);
                return list.ToArray();
            }
        }

        private float width = 0;
        /// <summary>
        /// 此打印区域需要的宽度（像素）  (页眉页脚不计算)
        /// </summary>
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private float height = 0;
        /// <summary>
        /// 此打印区域需要的高度（像素）   (页眉页脚不计算)
        /// </summary>
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /// <summary>
        /// 创建副本
        /// </summary>
        /// <returns></returns>
        public I3PrintArea Clone()
        {
            I3PrintArea area = new I3PrintArea();
            CopyList(this.headerRows, area.headerRows);
            CopyList(this.footerRows, area.footerRows);
            CopyList(this.dataRows, area.dataRows);
            CopyList(this.headerCols, area.headerCols);
            CopyList(this.footerCols, area.footerCols);
            CopyList(this.dataCols, area.dataCols);
            area.width = this.width;
            area.height = this.height;
            return area;
        }


        public I3PrintArea AddRow(int row)
        {
            return AddRows(new int[] { row });
        }

        public I3PrintArea AddRows(List<int> rowList)
        {
            return AddRows(rowList.ToArray());
        }

        public I3PrintArea AddRows(int[] rowArray)
        {
            foreach (int row in rowArray)
            {
                this.dataRows.Add(row, row);
            }
            return this;
        }

        public I3PrintArea AddCol(int col)
        {
            return AddCols(new int[] { col });
        }

        public I3PrintArea AddCols(List<int> colList)
        {
            return AddCols(colList.ToArray());
        }

        public I3PrintArea AddCols(int[] colArray)
        {
            foreach (int col in colArray)
            {
                this.dataCols.Add(col, col);
            }
            return this;
        }

        private void CopyList(SortedList<int, int> source, SortedList<int, int> dest)
        {
            dest.Clear();
            foreach (int key in source.Keys)
            {
                dest.Add(key, source[key]);
            }
        }

        private I3PrintAreas parent;
        public I3PrintAreas Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        /// <summary>
        /// 用于多个ReportData时，标识Area属于哪个ReportData
        /// </summary>
        public I3ReportData ReportData
        {
            get;
            set;
        }
    }

    public class I3PrintAreas
    {
        private Dictionary<int, I3PrintArea> dic;
        public Dictionary<int, I3PrintArea> Dic
        {
            get
            {
                if (dic == null)
                {
                    dic = new Dictionary<int, I3PrintArea>();
                }
                return dic;
            }
            set
            {
                dic = value;
            }
        }


        public new void Add(int index, I3PrintArea area)
        {
            area.Index = index;
            area.Parent = this;
            this.Dic.Add(index, area);
        }
    }
}
