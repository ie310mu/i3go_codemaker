

using System;

using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the CellCheckChanged event of a Table
	/// </summary>
	public delegate void I3CellCheckBoxEventHandler(object sender, I3CellCheckBoxEventArgs e);

	#endregion



	#region CellCheckBoxEventArgs
	
	/// <summary>
	/// Provides data for the CellCheckChanged event of a Table
	/// </summary>
	public class I3CellCheckBoxEventArgs : I3CellEventArgsBase
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CellButtonEventArgs class with 
		/// the specified Cell source, row index and column index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="row">The Row index of the Cell</param>
		public I3CellCheckBoxEventArgs(I3Cell source, int column, int row) : base(source, column, column)
		{
			
		}

		#endregion
	}

	#endregion
}
