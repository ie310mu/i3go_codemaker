using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// Base class for comparers used to sort the Cells contained in a TableModel
	/// </summary>
	public abstract class I3ComparerBase : IComparer
	{
		#region Class Data

		/// <summary>
		/// The TableModel that contains the Cells to be sorted
		/// </summary>
		private I3TableModel tableModel;

		/// <summary>
		/// The index of the Column to be sorted
		/// </summary>
		private int column;

		/// <summary>
		/// Specifies how the Column is to be sorted
		/// </summary>
		private SortOrder sortOrder;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ComparerBase class with the specified 
		/// TableModel, Column index and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3ComparerBase(I3TableModel tableModel, int column, SortOrder sortOrder)
		{
			this.tableModel = tableModel;
			this.column = column;
			this.sortOrder = sortOrder;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less 
		/// than, equal to or greater than the other
		/// </summary>
		/// <param name="a">First object to compare</param>
		/// <param name="b">Second object to compare</param>
		/// <returns>-1 if a is less than b, 1 if a is greater than b, or 0 if a equals b</returns>
		public abstract int Compare(object a, object b);

		#endregion


		#region Properties

		/// <summary>
		/// Gets the TableModel that contains the Cells to be sorted
		/// </summary>
		public I3TableModel TableModel
		{
			get
			{
				return this.tableModel;
			}
		}


		/// <summary>
		/// Gets the index of the Column to be sorted
		/// </summary>
		public int SortColumn
		{
			get
			{
				return this.column;
			}
		}


		/// <summary>
		/// Gets how the Column is to be sorted
		/// </summary>
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		#endregion
	}
}
