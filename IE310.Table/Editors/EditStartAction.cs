

using System;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 开始编辑的枚举
	/// Specifies the action that causes a Cell to start editing
	/// </summary>
	public enum I3EditStartAction
	{
		/// <summary>
        /// 双击
		/// A double click will start cell editing
		/// </summary>
		DoubleClick = 1,

		/// <summary>
        /// 单击
		/// A single click will start cell editing
		/// </summary>
		SingleClick = 2,

		/// <summary>
        /// 自定义热键
		/// A user defined key press will start cell editing
		/// </summary>
		CustomKey = 3,

        DataInputKey = 4,

        DoubleClick_CustomKey = 5,
        DoubleClick_DataInputKey = 6,
	}
}
