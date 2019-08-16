
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// ��ʾ�����Table���ĸ�����
	/// Specifies the part of the Table the user has clicked
	/// </summary>
	public enum I3TableRegion
	{
		/// <summary>
        /// �����һ��Cell
		/// A cell in the Table
		/// </summary>
		Cells = 1,

		/// <summary>
        /// �����һ����ͷ
		/// A column header in the Table
		/// </summary>
		ColumnHeader = 2,

		/// <summary>
        /// ������޿ͻ����򲿷ݣ���߿���հ���
		/// The non-client area of a Table, such as the border
		/// </summary>
		NonClientArea = 3,

		/// <summary>
        /// �����Table����
		/// </summary>
		NoWhere = 4,

        /// <summary>
        /// �������ͷ
        /// </summary>
        RowHeader = 5,

        /// <summary>
        /// ��������еĽ��紦
        /// </summary>
        RowColumnHeader = 6,
	}
}
