

using System;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 下拉控件风格
	/// Specifies the DropDownCellEditor style
	/// </summary>
	public enum I3DropDownStyle
    {

        /// <summary>
        /// Textbox不可编辑
        /// The user cannot directly edit the text portion. The user must 
        /// click the arrow button to display the list portion
        /// </summary>
        DropDownList = 0,

		/// <summary>
        /// Textbox可编辑
		/// The text portion is editable. The user must click the arrow 
		/// button to display the list portion
		/// </summary>
		DropDown = 1,
	}
}