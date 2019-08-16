
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// ѡ�������ʽ
	/// Specifies how selected Cells are drawn by a Table
	/// </summary>
	public enum I3SelectionStyle
	{
		/// <summary>
        /// ֻ�б�ѡ�еĵ�һ����Ԫ����Ϊѡ�������滭
		/// The first visible Cell in the selected Cells Row is drawn as selected
		/// </summary>
        //ListView = 0,

		/// <summary>
        /// ����ģʽ����ѡ��ĵ�Ԫ����Ϊѡ�������滭
		/// The selected Cells are drawn as selected
		/// </summary>
		Grid = 1
	}
}
