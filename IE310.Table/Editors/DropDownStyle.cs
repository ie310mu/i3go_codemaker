

using System;


namespace IE310.Table.Editors
{
	/// <summary>
    /// �����ؼ����
	/// Specifies the DropDownCellEditor style
	/// </summary>
	public enum I3DropDownStyle
    {

        /// <summary>
        /// Textbox���ɱ༭
        /// The user cannot directly edit the text portion. The user must 
        /// click the arrow button to display the list portion
        /// </summary>
        DropDownList = 0,

		/// <summary>
        /// Textbox�ɱ༭
		/// The text portion is editable. The user must click the arrow 
		/// button to display the list portion
		/// </summary>
		DropDown = 1,
	}
}