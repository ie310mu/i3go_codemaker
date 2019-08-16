


using System;


namespace IE310.Table.Events
{
	/// <summary>
    /// Cell属性改变事件的类型
	/// Specifies the type of event generated when the value of a 
	/// Cell's property changes
	/// </summary>
	public enum I3CellEventType 
	{
		/// <summary>
        /// 未知
		/// Occurs when the Cell's property change type is unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
        /// 值改变
		/// Occurs when the value displayed by a Cell has changed
		/// </summary>
		ValueChanged = 1,

		/// <summary>
        /// 字体改变
		/// Occurs when the value of a Cell's Font property changes
		/// </summary>
		FontChanged = 2,

		/// <summary>
        /// 背景色改变
		/// Occurs when the value of a Cell's BackColor property changes
		/// </summary>
		BackColorChanged = 3,

		/// <summary>
        /// 前景色改变
		/// Occurs when the value of a Cell's ForeColor property changes
		/// </summary>
		ForeColorChanged = 4,

		/// <summary>
        /// Style改变
		/// Occurs when the value of a Cell's CellStyle property changes
		/// </summary>
		StyleChanged = 5,

		/// <summary>
        /// Padding改变？
		/// Occurs when the value of a Cell's Padding property changes
		/// </summary>
		PaddingChanged = 6,

		/// <summary>
        /// 可编辑属性改变
		/// Occurs when the value of a Cell's Editable property changes
		/// </summary>
		EditableChanged = 7,

		/// <summary>
        /// Enabled属性改变
		/// Occurs when the value of a Cell's Enabled property changes
		/// </summary>
		EnabledChanged = 8,

		/// <summary>
        /// 提示文本改变
		/// Occurs when the value of a Cell's ToolTipText property changes
		/// </summary>
		ToolTipTextChanged = 9,

		/// <summary>
        /// Check状态改变
		/// Occurs when the value of a Cell's CheckState property changes
		/// </summary>
		CheckStateChanged = 10,

		/// <summary>
        /// ThreeState改变？
		/// Occurs when the value of a Cell's ThreeState property changes
		/// </summary>
		ThreeStateChanged = 11,

		/// <summary>
        /// Image改变
		/// Occurs when the value of a Cell's Image property changes
		/// </summary>
		ImageChanged = 12,

		/// <summary>
        /// ImageSizeMode改变
		/// Occurs when the value of a Cell's ImageSizeMode property changes
		/// </summary>
		ImageSizeModeChanged = 13
	}
}
