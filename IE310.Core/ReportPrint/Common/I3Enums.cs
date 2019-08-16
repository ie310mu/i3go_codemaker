using System;
using System.Collections.Generic;

using System.Text;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// 内容超过单元格时的扩充模式
    /// </summary>
    public enum I3AdjustSize
    {
        扩大单元格 = 1,
        都不变 = 2,
        缩小内容 = 3,
    }

    public enum I3BorderType
    {
        Left = 1,
        Top = 2,
        Right = 3,
        Bottom = 4,
    }
    /// <summary>
    /// 单元合并的状态
    /// </summary>
    public enum I3MergeState
    {
        None,
        FirstCell,
        Merged
    }

    public enum I3RowColType
    {
        None = -1,
        标题 = 0,
        表头 = 1,
        表尾 = 2,
        页眉 = 3,
        页脚 = 4,
        数据 = 10,
    }
}
