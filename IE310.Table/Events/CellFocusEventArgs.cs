

using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the CellGotFocus and CellLostFocus 
	/// events of a Table
	/// </summary>
	public delegate void I3CellFocusEventHandler(object sender, I3CellFocusEventArgs e);

	#endregion



	#region CellFocusEventArgs
	
	/// <summary>
	/// Provides data for the CellGotFocus and CellLostFocus events of a Table
	/// </summary>
	public class I3CellFocusEventArgs : I3CellEventArgsBase
	{
		#region Class Data

		/// <summary>
		/// The Table the Cell belongs to
		/// </summary>
        private I3Table table;
		
		/// <summary>
		/// The Cells bounding rectangle
		/// </summary>
		private Rectangle cellRect;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellFocusEventArgs class with 
		/// the specified source Cell, table, row index, column index and 
		/// cell bounds
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
        public I3CellFocusEventArgs(I3Cell source, I3Table table, int row, int column, Rectangle cellRect)
            : base(source, column, row)
		{
			this.table = table;
			this.cellRect = cellRect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Table the Cell belongs to
		/// </summary>
        public I3Table Table
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
		/// Gets the Cell's bounding rectangle
		/// </summary>
		public Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}

		#endregion
	}

	#endregion
}
