using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a Button
	/// </summary>
	public enum I3PushButtonStates
	{
		/// <summary>
		/// The Button is in its normal state
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// The Button is highlighted
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// The Button is being pressed by the mouse
		/// </summary>
		Pressed = 3,
		
		/// <summary>
		/// The Button is disabled
		/// </summary>
		Disabled = 4, 
		
		/// <summary>
		/// The Button is the default button
		/// </summary>
		Default = 5
	}
}
