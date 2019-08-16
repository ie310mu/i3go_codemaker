using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;
using IE310.Table.Cell;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// Base class for the sorters used to sort the Cells contained in a TableModel
	/// </summary>
	public abstract class I3SorterBase
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
		/// The IComparer used to sort the Column's Cells
		/// </summary>
		private IComparer comparer;

		/// <summary>
		/// Specifies how the Column is to be sorted
		/// </summary>
		private SortOrder sortOrder;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the SorterBase class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3SorterBase(I3TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder)
		{
			this.tableModel = tableModel;
			this.column = column;
			this.comparer = comparer;
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
		protected int Compare(I3Cell a, I3Cell b)
		{
			switch (this.SortOrder)
			{
				case SortOrder.None:
					return 0;

				case SortOrder.Descending:
					return -this.Comparer.Compare(a, b);

				default:
					return this.Comparer.Compare(a, b);
			}
		}


		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public abstract void Sort();


		/// <summary>
		/// Swaps the Rows in the TableModel at the specified indexes
		/// </summary>
		/// <param name="a">The index of the first Row to be swapped</param>
		/// <param name="b">The index of the second Row to be swapped</param>
		protected void Swap(int a, int b)
		{
			I3Row swap = this.TableModel.Rows[a];
			
			this.TableModel.Rows.SetRow(a, this.TableModel.Rows[b]);
			this.TableModel.Rows.SetRow(b, swap);
		}
		

		/// <summary>
		/// Replaces the Row in the TableModel located at index a with the Row 
		/// located at index b
		/// </summary>
		/// <param name="a">The index of the Row that will be replaced</param>
		/// <param name="b">The index of the Row that will be moved to index a</param>
		protected void Set(int a, int b)
		{
			this.TableModel.Rows.SetRow(a, this.TableModel.Rows[b]);
		}


		/// <summary>
		/// Replaces the Row in the TableModel located at index a with the specified Row
		/// </summary>
		/// <param name="a">The index of the Row that will be replaced</param>
		/// <param name="row">The Row that will be moved to index a</param>
		protected void Set(int a, I3Row row)
		{
			this.TableModel.Rows.SetRow(a, row);
		}

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
		/// Gets the IComparer used to sort the Column's Cells
		/// </summary>
		public IComparer Comparer
		{
			get
			{
				return this.comparer;
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
