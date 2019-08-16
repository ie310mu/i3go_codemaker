using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a TextBox
	/// </summary>
	public enum I3TextBoxStates
	{
		/// <summary>
		/// The TextBox is in its normal state
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// The TextBox is highlighted
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// The TextBox is selected
		/// </summary>
		Selected = 3,
		
		/// <summary>
		/// The TextBox is disabled
		/// </summary>
		Disabled = 4,
		
		/// <summary>
		/// The TextBox has focus
		/// </summary>
		Focused = 5,
		
		/// <summary>
		/// The TextBox is readonly
		/// </summary>
		ReadOnly = 6
	}
}
