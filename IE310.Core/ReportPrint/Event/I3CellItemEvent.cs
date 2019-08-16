using System;
using System.Collections.Generic;

using System.Text;

namespace IE310.Core.ReportPrint
{
    public class I3CellItemEventArgs
    {
        public I3CellItemEventArgs()
            : this(null)
        {
        }

        public I3CellItemEventArgs(I3ReportCell cell)
        {
            this.cell = cell;
        }

        private I3ReportCell cell;
        public I3ReportCell Cell
        {
            get
            {
                return cell;
            }
            set
            {
                cell = value;
            }
        }
    }

    public delegate void I3CellItemEvent(object sender, I3CellItemEventArgs e);

    /// <summary>
    /// 单元格选择模式
    /// </summary>
    public enum I3CellItemEventMode
    {
        None = 0,
        CellRect = 1,
        ContentRect = 2,
    }
}
