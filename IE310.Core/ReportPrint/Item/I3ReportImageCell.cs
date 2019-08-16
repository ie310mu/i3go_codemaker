using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;


namespace IE310.Core.ReportPrint
{
    [Serializable]
    [I3CellRenderer(typeof(I3ReportImageCellRenderer))]
    public class I3ReportImageCell : I3ReportCell
    {
        public I3ReportImageCell()
            : this(-1, -1)
        {
        }

        public I3ReportImageCell(int row, int col)
            : base(row, col)
        {
        }




        [NonSerialized]
        private int calImageWidth = 0;
        [JsonProperty(PropertyName = "cw")]
        /// <summary>
        /// 经过单元格大小调整后的尺寸，不需要序列化
        /// </summary>
        public int CalWidth
        {
            get
            {
                return calImageWidth;
            }
            set
            {
                calImageWidth = value;
            }
        }

        [NonSerialized]
        private int calImageHeight = 0;
        [JsonProperty(PropertyName = "cl")]
        /// <summary>
        /// 经过单元格大小调整后的尺寸，不需要序列化
        /// </summary>
        public int CalHeight
        {
            get
            {
                return calImageHeight;
            }
            set
            {
                calImageHeight = value;
            }
        }

    }
}
