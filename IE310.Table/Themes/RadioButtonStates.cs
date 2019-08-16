using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a RadioButton
	/// </summary>
	public enum I3RadioButtonStates
	{
		/// <summary>
		/// The RadioButton is unchecked and in its normal state
		/// </summary>
		UncheckedNormal = 1,
		
		/// <summary>
		/// The RadioButton is unchecked and is currently highlighted
		/// </summary>
		UncheckedHot = 2,
		
		/// <summary>
		/// The RadioButton is unchecked and is currently pressed by 
		/// the mouse
		/// </summary>
		UncheckedPressed = 3,
		
		/// <summary>
		/// The RadioButton is unchecked and is disabled
		/// </summary>
		UncheckedDisabled = 4,
		
		/// <summary>
		/// The RadioButton is checked and in its normal state
		/// </summary>
		CheckedNormal = 5,
		
		/// <summary>
		/// The RadioButton is checked and is currently highlighted
		/// </summary>
		CheckedHot = 6,
		
		/// <summary>
		/// The RadioButton is checked and is currently pressed by the 
		/// mouse
		/// </summary>
		CheckedPressed = 7,
		
		/// <summary>
		/// The RadioButton is checked and is disabled
		/// </summary>
		CheckedDisabled = 8
	}
}
