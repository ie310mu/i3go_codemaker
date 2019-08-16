

using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the HeaderMouseEnter, HeaderMouseLeave, 
	/// HeaderMouseDown, HeaderMouseUp, HeaderMouseMove, HeaderClick and HeaderDoubleClick 
	/// events of a Table
	/// </summary>
	public delegate void I3ColumnHeaderMouseEventHandler(object sender, I3ColumnHeaderMouseEventArgs e);


    public delegate void I3RowHeaderMouseEventHandler(object sender, I3RowHeaderMouseEventArgs e);

	#endregion



	#region HeaderMouseEventArgs
	
	/// <summary>
	/// Provides data for the HeaderMouseEnter, HeaderMouseLeave, HeaderMouseDown, 
	/// HeaderMouseUp, HeaderMouseMove, HeaderClick and HeaderDoubleClick events of a Table
	/// </summary>
	public class I3ColumnHeaderMouseEventArgs : MouseEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Column that raised the event
		/// </summary>
		private I3Column column;
		
		/// <summary>
		/// The Table the Column belongs to
		/// </summary>
        private I3Table table;
		
		/// <summary>
		/// The index of the Column
		/// </summary>
		private int index;
		
		/// <summary>
		/// The column header's bounding rectangle
		/// </summary>
		private Rectangle headerRect;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the HeaderMouseEventArgs class with 
		/// the specified source Column, Table, column index and column header bounds
		/// </summary>
		/// <param name="column">The Column that Raised the event</param>
		/// <param name="table">The Table the Column belongs to</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="headerRect">The column header's bounding rectangle</param>
        public I3ColumnHeaderMouseEventArgs(I3Column column, I3Table table, int index, Rectangle headerRect)
            : base(MouseButtons.None, 0, -1, -1, 0)
		{
			this.column = column;
			this.table = table;
			this.index = index;
			this.headerRect = headerRect;
		} 

		
		/// <summary>
		/// Initializes a new instance of the HeaderMouseEventArgs class with 
		/// the specified source Column, Table, column index, column header bounds 
		/// and MouseEventArgs
		/// </summary>
		/// <param name="column">The Column that Raised the event</param>
		/// <param name="table">The Table the Column belongs to</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="headerRect">The column header's bounding rectangle</param>
		/// <param name="mea">The MouseEventArgs that contains data about the 
		/// mouse event</param>
        public I3ColumnHeaderMouseEventArgs(I3Column column, I3Table table, int index, Rectangle headerRect, MouseEventArgs mea)
            : base(mea.Button, mea.Clicks, mea.X, mea.Y, mea.Delta)
		{
			this.column = column;
			this.table = table;
			this.index = index;
			this.headerRect = headerRect;
		} 

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Column that Raised the event
		/// </summary>
		public I3Column Column
		{
			get
			{
				return this.column;
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
		/// Gets the index of the Column
		/// </summary>
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
		/// Gets the column header's bounding rectangle
		/// </summary>
		public Rectangle HeaderRect
		{
			get
			{
				return this.headerRect;
			}
		}

		#endregion
	}

    public class I3RowHeaderMouseEventArgs : MouseEventArgs
    {
        #region Class Data

        /// <summary>
        /// The Column that raised the event
        /// </summary>
        private I3Row row;

        /// <summary>
        /// The Table the Column belongs to
        /// </summary>
        private I3Table table;

        /// <summary>
        /// The index of the Column
        /// </summary>
        private int index;

        /// <summary>
        /// The column header's bounding rectangle
        /// </summary>
        private Rectangle headerRect;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the HeaderMouseEventArgs class with 
        /// the specified source Column, Table, column index and column header bounds
        /// </summary>
        /// <param name="column">The Column that Raised the event</param>
        /// <param name="table">The Table the Column belongs to</param>
        /// <param name="index">The index of the Column</param>
        /// <param name="headerRect">The column header's bounding rectangle</param>
        public I3RowHeaderMouseEventArgs(I3Row row, I3Table table, int index, Rectangle headerRect)
            : base(MouseButtons.None, 0, -1, -1, 0)
        {
            this.row = row;
            this.table = table;
            this.index = index;
            this.headerRect = headerRect;
        }


        /// <summary>
        /// Initializes a new instance of the HeaderMouseEventArgs class with 
        /// the specified source Column, Table, column index, column header bounds 
        /// and MouseEventArgs
        /// </summary>
        /// <param name="column">The Column that Raised the event</param>
        /// <param name="table">The Table the Column belongs to</param>
        /// <param name="index">The index of the Column</param>
        /// <param name="headerRect">The column header's bounding rectangle</param>
        /// <param name="mea">The MouseEventArgs that contains data about the 
        /// mouse event</param>
        public I3RowHeaderMouseEventArgs(I3Row row, I3Table table, int index, Rectangle headerRect, MouseEventArgs mea)
            : base(mea.Button, mea.Clicks, mea.X, mea.Y, mea.Delta)
        {
            this.row = row;
            this.table = table;
            this.index = index;
            this.headerRect = headerRect;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the Row that Raised the event
        /// </summary>
        public I3Row Row
        {
            get
            {
                return this.row;
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
        /// Gets the index of the Column
        /// </summary>
        public int Index
        {
            get
            {
                return this.index;
            }
        }


        /// <summary>
        /// Gets the column header's bounding rectangle
        /// </summary>
        public Rectangle HeaderRect
        {
            get
            {
                return this.headerRect;
            }
        }

        #endregion
    }

	#endregion
}
