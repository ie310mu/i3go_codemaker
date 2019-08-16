
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// Table��״̬
	/// Specifies the current state of the Table
	/// </summary>
    public enum I3TableState
    {
        /// <summary>
        /// ��ͨ״̬
        /// The Table is in its normal state
        /// </summary>
        Normal = 0,

        /// <summary>
        /// ѡ��һ���е�״̬
        /// The Table is selecting a Column
        /// </summary>
        ColumnSelecting = 1,

        /// <summary>
        /// �б��ض����ȵ�״̬
        /// The Table is resizing a Column
        /// </summary>
        ColumnResizing = 2,

        /// <summary>
        /// ���ڱ༭��״̬
        /// The Table is editing a Cell
        /// </summary>
        Editing = 3,

        /// <summary>
        /// ���������״̬
        /// The Table is sorting a Column
        /// </summary>
        Sorting = 4,

        /// <summary>
        /// ����ѡ�����״̬
        /// The Table is selecting Cells
        /// </summary>
        Selecting = 5,

        RowSelecting = 6,

        RowResizing = 7,
    }
}
