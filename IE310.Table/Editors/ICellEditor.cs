


using System;
using System.Drawing;

using IE310.Table.Models;
using IE310.Table.Cell;
using System.Windows.Forms;


namespace IE310.Table.Editors
{
	/// <summary>
    /// �ӿڣ������У�׼���༭���༭������༭��ȡ���༭
	/// Exposes common methods provided by Cell editors
	/// </summary>
	public interface II3CellEditor
	{
		/// <summary>
        /// ׼���༭Cell
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
        /// ��ʼ�༭Cell
		/// Starts editing the Cell
		/// </summary>
		void StartEditing();


		/// <summary>
        /// �����༭Cell���ύ����
		/// Stops editing the Cell and commits any changes
		/// </summary>
		void StopEditing();


		/// <summary>
        /// �����༭Cell��ȡ������
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		void CancelEditing();

        /// <summary>
        /// �༭���Ƿ���������м��¼�
        /// </summary>
        /// <returns></returns>
        bool HandleMouseWheel();

        Control GetDataInputControl();
	}
}
