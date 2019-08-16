using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// A MergeSort implementation for sorting the Cells contained in a TableModel
	/// </summary>
	public class I3MergeSorter : I3SorterBase
	{
		/// <summary>
		/// Initializes a new instance of the MergeSorter class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3MergeSorter(I3TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder) : base(tableModel, column, comparer, sortOrder)
		{
			
		}

		
		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public override void Sort()
		{
			this.Sort(0, this.TableModel.Rows.Count - 1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="fromPos"></param>
		/// <param name="toPos"></param>
		private void Sort(int fromPos, int toPos) 
		{
			int end_low;
			int start_high;
			int i;
			I3Row tmp;
			int mid;
			
			if (fromPos < toPos) 
			{
				mid = (fromPos + toPos) / 2;
			
				this.Sort(fromPos, mid);
				this.Sort(mid + 1, toPos);

				end_low = mid;
				start_high = mid + 1;

				while (fromPos <= end_low & start_high <= toPos) 
				{
					if (this.Compare(this.TableModel[fromPos, this.SortColumn], this.TableModel[start_high, this.SortColumn]) < 0) 
					{
						fromPos++;
					} 
					else 
					{
						tmp = this.TableModel.Rows[start_high];
						
						for (i = start_high - 1; i >= fromPos; i--) 
						{
							this.Set(i+1, this.TableModel.Rows[i]);
						}

						this.Set(fromPos, tmp);
						
						fromPos++;
						end_low++;
						start_high++;
					}
				}
			}
		}
	}
}
