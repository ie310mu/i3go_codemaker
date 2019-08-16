using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace IE310.Core.ReportPrint
{

    [Serializable]
    public class I3ReportCol
    {
        public I3ReportCol()
            :this(-1)
        {
        }

        public I3ReportCol(int col)
        {
            this.col = col;
        }

        private int col;
        /// <summary>
        /// 列号
        /// </summary>
        [JsonProperty(PropertyName = "c")]
        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }

        private int width;
        /// <summary>
        /// 行的宽度（如果是隐藏的列，需要设置为0）
        /// </summary>
        [JsonProperty(PropertyName = "w")]
        public int Width
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


        private bool pageBreak;
        /// <summary>
        /// 列后是否分页
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
        /// 列类型
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
    }
}
