using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Drawing;


namespace IE310.Core.ReportPrint
{
    [Serializable]
    [I3CellRenderer(typeof(I3ReportSlashCellRenderer))]
    public class I3ReportSlashCell : I3ReportCell
    {
        public I3ReportSlashCell()
            : this(-1, -1)
        {
        }

        public I3ReportSlashCell(int row, int col)
            : base(row, col)
        {
        }
    }
}
