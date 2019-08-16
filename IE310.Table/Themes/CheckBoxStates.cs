using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a CheckBox
	/// </summary>
	public enum I3CheckBoxStates
	{
		/// <summary>
		/// The CheckBox is unchecked and in its normal state
		/// </summary>
		UncheckedNormal = 1,
		
		/// <summary>
		/// The CheckBox is unchecked and is currently highlighted
		/// </summary>
		UncheckedHot = 2,
		
		/// <summary>
		/// The CheckBox is unchecked and is currently pressed by 
		/// the mouse
		/// </summary>
		UncheckedPressed = 3,
		
		/// <summary>
		/// The CheckBox is unchecked and is disabled
		/// </summary>
		UncheckedDisabled = 4,
		
		/// <summary>
		/// The CheckBox is checked and in its normal state
		/// </summary>
		CheckedNormal = 5,
		
		/// <summary>
		/// The CheckBox is checked and is currently highlighted
		/// </summary>
		CheckedHot = 6,
		
		/// <summary>
		/// The CheckBox is checked and is currently pressed by the 
		/// mouse
		/// </summary>
		CheckedPressed = 7,
		
		/// <summary>
		/// The CheckBox is checked and is disabled
		/// </summary>
		CheckedDisabled = 8,
		
		/// <summary>
		/// The CheckBox is in an indeterminate state
		/// </summary>
		MixedNormal = 9,
		
		/// <summary>
		/// The CheckBox is in an indeterminate state and is currently 
		/// highlighted
		/// </summary>
		MixedHot = 10,
		
		/// <summary>
		/// The CheckBox is in an indeterminate state and is currently 
		/// pressed by the mouse
		/// </summary>
		MixedPressed = 11,
		
		/// <summary>
		/// The CheckBox is in an indeterminate state and is disabled
		/// </summary>
		MixedDisabled = 12
	}
}
