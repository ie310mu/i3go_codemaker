using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// 合并项目列表
    /// </summary>
    [Serializable]
    public class I3MergeRange
    {
        private int startRow;
        [JsonProperty(PropertyName = "sr")]
        public int StartRow
        {
            get { return startRow; }
            set { startRow = value; }
        }

        private int endRow;
        [JsonProperty(PropertyName = "er")]
        public int EndRow
        {
            get { return endRow; }
            set { endRow = value; }
        }

        private int startCol;
        [JsonProperty(PropertyName = "sc")]
        public int StartCol
        {
            get { return startCol; }
            set { startCol = value; }
        }

        private int endCol;
        [JsonProperty(PropertyName = "ec")]
        public int EndCol
        {
            get { return endCol; }
            set { endCol = value; }
        }

        public I3MergeRange()
        {
        }
        public I3MergeRange(int rowStart, int colStart, int rowEnd, int colEnd)
        {
            this.StartRow = rowStart;
            this.startCol = colStart;
            this.EndRow = rowEnd;
            this.endCol = colEnd;
        }

        public bool InRange(int row, int col)
        {
            return (this.startRow <= row && this.endRow >= row &&
                this.startCol <= col && this.endCol >= col);
        }
    }
}
