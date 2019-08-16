

using System;

using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the CellButtonClicked event of a Table
	/// </summary>
	public delegate void I3CellButtonEventHandler(object sender, I3CellButtonEventArgs e);

	#endregion



	#region CellButtonEventArgs
	
	/// <summary>
	/// Provides data for the CellButtonClicked event of a Table
	/// </summary>
	public class I3CellButtonEventArgs : I3CellEventArgsBase
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CellButtonEventArgs class with 
		/// the specified Cell source, row index and column index
		/// </summary>
		/// <param name="source">The Cell that raised the event</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="row">The Row index of the Cell</param>
        public I3CellButtonEventArgs(I3Cell source, int column, int row)
            : base(source, column, row)
		{
			
		}

		#endregion
	}

	#endregion
}
