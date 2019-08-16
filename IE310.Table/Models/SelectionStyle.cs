
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// 选择项的样式
	/// Specifies how selected Cells are drawn by a Table
	/// </summary>
	public enum I3SelectionStyle
	{
		/// <summary>
        /// 只有被选行的第一个单元格被作为选择项来绘画
		/// The first visible Cell in the selected Cells Row is drawn as selected
		/// </summary>
        //ListView = 0,

		/// <summary>
        /// 正常模式，被选择的单元格被作为选择项来绘画
		/// The selected Cells are drawn as selected
		/// </summary>
		Grid = 1
	}
}
