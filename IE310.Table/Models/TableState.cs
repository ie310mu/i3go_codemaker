
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// Table的状态
	/// Specifies the current state of the Table
	/// </summary>
    public enum I3TableState
    {
        /// <summary>
        /// 普通状态
        /// The Table is in its normal state
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 选择一个列的状态
        /// The Table is selecting a Column
        /// </summary>
        ColumnSelecting = 1,

        /// <summary>
        /// 列被重定义宽度的状态
        /// The Table is resizing a Column
        /// </summary>
        ColumnResizing = 2,

        /// <summary>
        /// 正在编辑的状态
        /// The Table is editing a Cell
        /// </summary>
        Editing = 3,

        /// <summary>
        /// 正在排序的状态
        /// The Table is sorting a Column
        /// </summary>
        Sorting = 4,

        /// <summary>
        /// 正在选择项的状态
        /// The Table is selecting Cells
        /// </summary>
        Selecting = 5,

        RowSelecting = 6,

        RowResizing = 7,
    }
}
