using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a ComboBox
	/// </summary>
	public enum I3ComboBoxStates
	{
		/// <summary>
		/// The ColumnBox dropdown button is in its normal state
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// The ColumnBox dropdown button is highlighted
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// The ColumnBox dropdown button is being pressed by the mouse
		/// </summary>
		Pressed = 3,
		
		/// <summary>
		/// The ColumnBox dropdown button is disabled
		/// </summary>
		Disabled = 4
	}
}
