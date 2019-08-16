

using System;

using IE310.Table.Events;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 接口，声明了下拉控件的下拉按钮的按下和释放事件
	/// Specifies that a CellEditor uses the buttons provided by its counter-part 
	/// CellRenderer during editing
	/// </summary>
	public interface II3EditorUsesRendererButtons
	{
		/// <summary>
		/// Raises the EditorButtonMouseDown event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnEditorButtonMouseDown(object sender, I3CellMouseEventArgs e);
		
		
		/// <summary>
		/// Raises the EditorButtonMouseUp event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnEditorButtonMouseUp(object sender, I3CellMouseEventArgs e);
	}
}
