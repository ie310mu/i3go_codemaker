


using System;
using System.Drawing;

using IE310.Table.Models;
using IE310.Table.Cell;
using System.Windows.Forms;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 接口，方法有：准备编辑，编辑，保存编辑，取消编辑
	/// Exposes common methods provided by Cell editors
	/// </summary>
	public interface II3CellEditor
	{
		/// <summary>
        /// 准备编辑Cell
		/// Prepares the ICellEditor to edit the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to be edited</param>
		/// <param name="table">The Table that contains the Cell</param>
		/// <param name="cellPos">A CellPos representing the position of the Cell</param>
		/// <param name="cellRect">The Rectangle that represents the Cells location and size</param>
		/// <param name="userSetEditorValues">Specifies whether the ICellEditors 
		/// starting value has already been set by the user</param>
		/// <returns>true if the ICellEditor can continue editing the Cell, false otherwise</returns>
		bool PrepareForEditing(I3Cell cell, I3Table table, I3CellPos cellPos, Rectangle cellRect, bool userSetEditorValues);


		/// <summary>
        /// 开始编辑Cell
		/// Starts editing the Cell
		/// </summary>
		void StartEditing();


		/// <summary>
        /// 结束编辑Cell并提交更改
		/// Stops editing the Cell and commits any changes
		/// </summary>
		void StopEditing();


		/// <summary>
        /// 结束编辑Cell并取消更改
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		void CancelEditing();

        /// <summary>
        /// 编辑器是否拦截鼠标中键事件
        /// </summary>
        /// <returns></returns>
        bool HandleMouseWheel();

        Control GetDataInputControl();
	}
}
