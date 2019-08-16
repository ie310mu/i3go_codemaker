

using System;


namespace IE310.Table.Events
{
	/// <summary>
    /// ���¼�������
	/// Specifies the type of event generated when the value of a 
	/// Column's property changes
	/// </summary>
	public enum I3ColumnEventType 
	{
		/// <summary>
        /// δ֪
		/// Occurs when the Column's property change type is unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
        /// Text�ı��¼�
		/// Occurs when the value of a Column's Text property changes
		/// </summary>
		TextChanged = 1,

		/// <summary>
        /// ��Alignmnet���Ըı��¼�
		/// Occurs when the value of a Column's Alignment property changes
		/// </summary>
		AlignmentChanged = 2,

		/// <summary>
        /// HeaderAlignment�ı��¼�
		/// Occurs when the value of a Column's HeaderAlignment property changes
		/// </summary>
		HeaderAlignmentChanged = 3,

		/// <summary>
        /// Width���Ըı��¼�
		/// Occurs when the value of a Column's Width property changes
		/// </summary>
		WidthChanged = 4,

		/// <summary>
        /// Visible���Ըı��¼�
		/// Occurs when the value of a Column's Visible property changes
		/// </summary>
		VisibleChanged = 5,

		/// <summary>
        /// Image�ı��¼�
		/// Occurs when the value of a Column's Image property changes
		/// </summary>
		ImageChanged = 6,

		/// <summary>
        /// Format�ı��¼�
		/// Occurs when the value of a Column's Format property changes
		/// </summary>
		FormatChanged = 7,

		/// <summary>
        /// ColumnState�ı��¼�
		/// Occurs when the value of a Column's ColumnState property changes
		/// </summary>
		StateChanged = 8,

		/// <summary>
        /// Renderer�ı��¼�
		/// Occurs when the value of a Column's Renderer property changes
		/// </summary>
		RendererChanged = 9,

		/// <summary>
        /// Editor�ı��¼�
		/// Occurs when the value of a Column's Editor property changes
		/// </summary>
		EditorChanged = 10, 

		/// <summary>
        /// Comparer�ı��¼�
		/// Occurs when the value of a Column's Comparer property changes
		/// </summary>
		ComparerChanged = 11, 

		/// <summary>
        /// Enabled�ı��¼�
		/// Occurs when the value of a Column's Enabled property changes
		/// </summary>
		EnabledChanged = 12,

		/// <summary>
        /// Editable�ı��¼�
		/// Occurs when the value of a Column's Editable property changes
		/// </summary>
		EditableChanged = 13,

		/// <summary>
        /// Selectable�ı��¼�
		/// Occurs when the value of a Column's Selectable property changes
		/// </summary>
		SelectableChanged = 14,

		/// <summary>
        /// Sortable�ı��¼�
		/// Occurs when the value of a Column's Sortable property changes
		/// </summary>
		SortableChanged = 15,

		/// <summary>
        /// SortOrder�ı��¼�
		/// Occurs when the value of a Column's SortOrder property changes
		/// </summary>
		SortOrderChanged = 16,

		/// <summary>
        /// ToolTipText�ı��¼�
		/// Occurs when the value of a Column's ToolTipText property changes
		/// </summary>
		ToolTipTextChanged = 17,

		/// <summary>
        /// ��ʼ�����¼�
		/// Occurs when a Column is being sorted
		/// </summary>
		Sorting = 18,
	}
}
