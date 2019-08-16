

using System;


namespace IE310.Table.Events
{
	/// <summary>
    /// 列事件的类型
	/// Specifies the type of event generated when the value of a 
	/// Column's property changes
	/// </summary>
	public enum I3ColumnEventType 
	{
		/// <summary>
        /// 未知
		/// Occurs when the Column's property change type is unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
        /// Text改变事件
		/// Occurs when the value of a Column's Text property changes
		/// </summary>
		TextChanged = 1,

		/// <summary>
        /// 列Alignmnet属性改变事件
		/// Occurs when the value of a Column's Alignment property changes
		/// </summary>
		AlignmentChanged = 2,

		/// <summary>
        /// HeaderAlignment改变事件
		/// Occurs when the value of a Column's HeaderAlignment property changes
		/// </summary>
		HeaderAlignmentChanged = 3,

		/// <summary>
        /// Width属性改变事件
		/// Occurs when the value of a Column's Width property changes
		/// </summary>
		WidthChanged = 4,

		/// <summary>
        /// Visible属性改变事件
		/// Occurs when the value of a Column's Visible property changes
		/// </summary>
		VisibleChanged = 5,

		/// <summary>
        /// Image改变事件
		/// Occurs when the value of a Column's Image property changes
		/// </summary>
		ImageChanged = 6,

		/// <summary>
        /// Format改变事件
		/// Occurs when the value of a Column's Format property changes
		/// </summary>
		FormatChanged = 7,

		/// <summary>
        /// ColumnState改变事件
		/// Occurs when the value of a Column's ColumnState property changes
		/// </summary>
		StateChanged = 8,

		/// <summary>
        /// Renderer改变事件
		/// Occurs when the value of a Column's Renderer property changes
		/// </summary>
		RendererChanged = 9,

		/// <summary>
        /// Editor改变事件
		/// Occurs when the value of a Column's Editor property changes
		/// </summary>
		EditorChanged = 10, 

		/// <summary>
        /// Comparer改变事件
		/// Occurs when the value of a Column's Comparer property changes
		/// </summary>
		ComparerChanged = 11, 

		/// <summary>
        /// Enabled改变事件
		/// Occurs when the value of a Column's Enabled property changes
		/// </summary>
		EnabledChanged = 12,

		/// <summary>
        /// Editable改变事件
		/// Occurs when the value of a Column's Editable property changes
		/// </summary>
		EditableChanged = 13,

		/// <summary>
        /// Selectable改变事件
		/// Occurs when the value of a Column's Selectable property changes
		/// </summary>
		SelectableChanged = 14,

		/// <summary>
        /// Sortable改变事件
		/// Occurs when the value of a Column's Sortable property changes
		/// </summary>
		SortableChanged = 15,

		/// <summary>
        /// SortOrder改变事件
		/// Occurs when the value of a Column's SortOrder property changes
		/// </summary>
		SortOrderChanged = 16,

		/// <summary>
        /// ToolTipText改变事件
		/// Occurs when the value of a Column's ToolTipText property changes
		/// </summary>
		ToolTipTextChanged = 17,

		/// <summary>
        /// 开始排序事件
		/// Occurs when a Column is being sorted
		/// </summary>
		Sorting = 18,
	}
}
