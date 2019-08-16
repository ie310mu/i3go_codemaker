using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Table.Header
{
    public enum I3RowHeaderDisplayMode
    {
        None = 0,
        Num = 1,
    }

    public enum I3ColumnHeaderDisplayMode
    {
        None = 0,
        Num = 1,
        Text = 2,

        //将列号转换为字母，类似于Excel
        Reference = 3,
    }
}
