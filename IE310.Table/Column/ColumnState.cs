


using System;

namespace IE310.Table.Column
{
	/// <summary>
    /// 列的状态
	/// Specifies the state of a Column
	/// </summary>
	public enum I3ColumnState
	{
		/// <summary>
        /// 普通
		/// Column is in its normal state
		/// </summary>
		Normal = 1,

		/// <summary>
        /// 具有焦点
		/// Mouse is over the Column
		/// </summary>
		Hot = 2,

		/// <summary>
        /// 被按下
		/// Column is being pressed
		/// </summary>
		Pressed = 3
	}

    /// <summary>
    /// 行的状态
    /// Specifies the state of a Column
    /// </summary>
    public enum I3RowState
    {
        /// <summary>
        /// 普通
        /// Column is in its normal state
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 具有焦点
        /// Mouse is over the Column
        /// </summary>
        Hot = 2,

        /// <summary>
        /// 被按下
        /// Column is being pressed
        /// </summary>
        Pressed = 3
    }
}
