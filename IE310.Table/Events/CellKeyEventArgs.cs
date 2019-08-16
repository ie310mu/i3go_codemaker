


using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the CellKeyDown and CellKeyUp 
	/// events of a Table
	/// </summary>
	public delegate void I3CellKeyEventHandler(object sender, I3CellKeyEventArgs e);

	#endregion



	#region CellKeyEventArgs
	
	/// <summary>
	/// Provides data for the CellKeyDown and CellKeyUp events of a Table
	/// </summary>
	public class I3CellKeyEventArgs : KeyEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Cell that Raised the event
		/// </summary>
		private I3Cell cell;
		
		/// <summary>
		/// The Table the Cell belongs to
		/// </summary>
        private I3Table table;
		
		/// <summary>
		/// The Row index of the Cell
		/// </summary>
		private int row;
		
		/// <summary>
		/// The Column index of the Cell
		/// </summary>
		private int column;
		
		/// <summary>
		/// The Cells bounding rectangle
		/// </summary>
		private Rectangle cellRect;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellKeyEventArgs class with 
		/// the specified source Cell, table, row index, column index, cell 
		/// bounds and KeyEventArgs
		/// </summary>
		/// <param name="cell">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		/// <param name="kea"></param>
        public I3CellKeyEventArgs(I3Cell cell, I3Table table, int row, int column, Rectangle cellRect, KeyEventArgs kea)
            : base(kea.KeyData)
		{
			this.cell = cell;
			this.table = table;
			this.row = row;
			this.column = column;
			this.cellRect = cellRect;
		}  
		
		
		/// <summary>
		/// Initializes a new instance of the CellKeyEventArgs class with 
		/// the specified source Cell, table, row index, column index and 
		/// cell bounds
		/// </summary>
		/// <param name="cell">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="cellPos"></param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		/// <param name="kea"></param>
        public I3CellKeyEventArgs(I3Cell cell, I3Table table, I3CellPos cellPos, Rectangle cellRect, KeyEventArgs kea)
            : base(kea.KeyData)
		{
			this.cell = cell;
			this.table = table;
			this.row = cellPos.Row;
			this.column = cellPos.Column;
			this.cellRect = cellRect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Cell that Raised the event
		/// </summary>
		public I3Cell Cell
		{
			get
			{
				return this.cell;
			}
		}


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
		/// Gets the Row index of the Cell
		/// </summary>
		public int Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets the Column index of the Cell
		/// </summary>
		public int Column
		{
			get
			{
				return this.column;
			}
		}


		/// <summary>
		/// Gets the Cells bounding rectangle
		/// </summary>
		public Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}


		/// <summary>
		/// Gets the position of the Cell
		/// </summary>
		public I3CellPos CellPos
		{
			get
			{
				return new I3CellPos(this.Row, this.Column);
			}
		}

		#endregion
	}

	#endregion
}
