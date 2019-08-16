
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// 标示点击了Table的哪个区域
	/// Specifies the part of the Table the user has clicked
	/// </summary>
	public enum I3TableRegion
	{
		/// <summary>
        /// 点击了一个Cell
		/// A cell in the Table
		/// </summary>
		Cells = 1,

		/// <summary>
        /// 点击了一个列头
		/// A column header in the Table
		/// </summary>
		ColumnHeader = 2,

		/// <summary>
        /// 点击了无客户区域部份，如边框，如空白区
		/// The non-client area of a Table, such as the border
		/// </summary>
		NonClientArea = 3,

		/// <summary>
        /// 点击了Table外面
		/// </summary>
		NoWhere = 4,

        /// <summary>
        /// 点击了行头
        /// </summary>
        RowHeader = 5,

        /// <summary>
        /// 点击了行列的交界处
        /// </summary>
        RowColumnHeader = 6,
	}
}
