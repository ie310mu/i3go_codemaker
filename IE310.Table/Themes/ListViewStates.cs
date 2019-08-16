using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different states of a ListView
	/// </summary>
	public enum I3ListViewStates
	{
		/// <summary>
		/// The ListView is in its normal state
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// The ListView is highlighted
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// The ListView is selected
		/// </summary>
		Selected = 3,
		
		/// <summary>
		/// The ListView is disabled
		/// </summary>
		Disabled = 4,
		
		/// <summary>
		/// The ListView is selected but does not have focus
		/// </summary>
		SelectedNotFocus = 5
	}
}
