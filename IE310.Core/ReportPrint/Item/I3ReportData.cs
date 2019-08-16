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
    /// ��������
    /// </summary>
    [Serializable]
    public class I3ReportData
    {
        #region ����

        public I3ReportData()
            : this(0, 0)
        {
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        public I3ReportData(int rowCount, int colCount)
        {
            rows = new I3ReportRow[rowCount];
            for (int i = 0; i < rows.Length; i++)  //������
            {
                rows[i] = new I3ReportRow(i, colCount);
            }

            cols = new I3ReportCol[colCount];
            for (int j = 0; j < cols.Length; j++)  //������
            {
                cols[j] = new I3ReportCol(j);
            }
        }
        #endregion

        #region �С��С���Ԫ��

        public string Name { get; set; }

        private I3ReportRow[] rows;
        /// <summary>
        /// �и߶�
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
        /// �п��
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
        /// ������
        /// </summary>
        public int RowCount
        {
            get
            {
                return rows == null ? 0 : rows.Length;
            }
        }

        /// <summary>
        ///������
        /// </summary>
        public int ColCount
        {
            get
            {
                return cols == null ? 0 : cols.Length;
            }
        }

        /// <summary>
        /// ��ȡ��Ԫ����ı���Ϣ
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
        /// ��ȡ��Ԫ��
        /// </summary>
        /// <param name="row">��</param>
        /// <param name="col">��</param>
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
        /// ��ȡ�򴴽��ı���Ԫ��
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
        /// ��ȡ�򴴽�б�ߵ�Ԫ��
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
        /// ��ȡ�򴴽���ͼƬ�ĵ�Ԫ��
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
        /// ת��ΪͼƬ��Ԫ�񣬸����������ԣ�Ŀ����Ϊ����ReportData����jsonת�����һ�Cell�ж�ʧ�ĵ�Ԫ��������Ϣ��Ҫ��I3ReportImageCell�����е����ԣ�ȫ��д��I3ReportCell�У�
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

        #region ��ʽ

        private Dictionary<string, I3ReportCellStyle> cellStyles = new Dictionary<string, I3ReportCellStyle>();
        /// <summary>
        /// ��ʽ�б�
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
        /// �ж��Ƿ����ĳ����ʽ
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
        /// ���ӵ�Ԫ��ʽ����
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
        /// ���ӵ�Ԫ��ʽ����
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public I3ReportCellStyle AddCellStyle(I3ReportCellStyle style)
        {
            if (string.IsNullOrEmpty(style.Name))
            {
                throw new Exception("styleName����Ϊ��");
            }
            if (cellStyles.ContainsKey(style.Name))
            {
                cellStyles.Remove(style.Name);
            }
            cellStyles.Add(style.Name, style);
            return style;
        }
        #endregion

        #region �ϲ�����

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
        /// ����һ���ϲ�����
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
        /// ��Ԫ��Ϊ�ϲ���Ԫ��ʱ����ȡ�ϲ���Ԫ��ĵ�һ������
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

        #region ҳ�����á���ҳ

        private I3PageSetting pageSetting = null;
        /// <summary>
        /// ҳ������
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
        /// ���������Ĵ�ӡ�������ݣ�һҳ��Ӧһ��PrintArea  �����л�
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
        /// ���¼��㱨���С�ͷ�ҳ��Ϣ
        /// </summary>
        public void ReCalSizeAndPageInfo()
        {
            I3ReportPrintController.RePaging(this);
            ReSetMergeCellsInDifPage();
        }

        /// <summary>
        /// ���úϲ���Ԫ��λ�ڶ�ҳ��������ֽ�ɶ�����ӣ�
        /// </summary>
        private void ReSetMergeCellsInDifPage()
        {
            I3ReportPrintController.ReSetMergeCellsInDifPage(this);
        }


        private int pageIndexStart = 1;
        /// <summary>
        /// ҳ��Ŀ�ʼ���
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
                    //û��ָ��ֵ��Ĭ�Ϸ��ر����ݵ���ҳ��
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

        #region ����Ӧ

        /// <summary>
        /// �������ݵ���ҳ��߶�Ϊһҳ(�Զ�����ֽ��ʱ����Ч)
        /// </summary>
        public void AdjustPaperHeightToOnePage()
        {
            if (this.PageSetting.PaperType != PaperType.Custom)
            {
                throw new Exception("ֽ������ΪCustomʱ���ſɵ���AdjustPaperHeightToOnePage����");
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
        /// �������ݵ���ҳ����Ϊһҳ(�Զ�����ֽ��ʱ����Ч)
        /// </summary>
        public void AdjustPaperWidthToOnePage()
        {
            if (this.PageSetting.PaperType != PaperType.Custom)
            {
                throw new Exception("ֽ������ΪCustomʱ���ſɵ���AdjustPaperWidthToOnePage����");
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
        /// �Ƿ��ӡ��ά��
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
