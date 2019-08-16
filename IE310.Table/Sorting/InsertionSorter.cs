using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// An InsertionSort implementation for sorting the Cells contained in a TableModel
	/// </summary>
	public class I3InsertionSorter : I3SorterBase
	{
		/// <summary>
		/// Initializes a new instance of the InsertionSorter class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3InsertionSorter(I3TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder) : base(tableModel, column, comparer, sortOrder)
		{
			
		}

		
		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public override void Sort()
		{
			int i;
			int j;
			I3Row b;

			for (i=1; i<this.TableModel.Rows.Count; i++)
			{
				j = i;
				b = this.TableModel.Rows[i];
				
				while ((j > 0) && (this.Compare(this.TableModel[j-1, this.SortColumn], b.Cells[this.SortColumn]) > 0))
				{
					this.Set(j, this.TableModel.Rows[j-1]);
					
					--j;
				}

				this.Set(j, b);
			}					
		}
	}
}
