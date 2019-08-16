using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace IE310.Core.ReportPrint
{
    [Serializable]
    public class I3ReportRow
    {
        public I3ReportRow()
            : this(-1, 0)
        {
        }

        public I3ReportRow(int row, int colCount)
        {
            this.row = row;
            this.cells = new I3ReportCell[colCount];

            //先默认创建为普通Cell
            for (int j = 0; j < colCount; j++)
            {
                this.cells[j] = new I3ReportCell(row, j);
            }
        }

        private int row;
        /// <summary>
        /// 行号
        /// </summary>
        [JsonProperty(PropertyName = "r")]
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }

        private int height;
        /// <summary>
        /// 行的高度（如果是隐藏的行，需要设置为0）
        /// </summary>
        [JsonProperty(PropertyName = "h")]
        public int Height
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

        private bool pageBreak;
        /// <summary>
        /// 行后是否分页
        /// </summary>
        [JsonProperty(PropertyName = "pb")]
        public bool PageBreak
        {
            get
            {
                return pageBreak;
            }
            set
            {
                pageBreak = value;
            }
        }

        private I3RowColType type = I3RowColType.数据;
        /// <summary>
        /// 行类型
        /// </summary>
        [JsonProperty(PropertyName = "t")]
        public I3RowColType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        private I3ReportCell[] cells;
        /// <summary>
        /// 此行下的所有单元格
        /// </summary>
        public I3ReportCell[] Cells
        {
            get
            {
                return cells;
            }
            set
            {
                this.cells = value;
            }
        }

        public I3ReportCell this[int index]
        {
            get
            {
                return cells[index];
            }
            set
            {
                cells[index] = value;
            }
        }
    }
}
