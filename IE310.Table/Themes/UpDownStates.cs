using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a UpDown control's button
	/// </summary>
	public enum I3UpDownStates
	{
		/// <summary>
		/// The UpDown button is in its normal state
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// The UpDown button is highlighted
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// The UpDown button is being pressed by the mouse
		/// </summary>
		Pressed = 3,
		
		/// <summary>
		/// The UpDown button disabled
		/// </summary>
		Disabled = 4
	}
}
