using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// A HeapSort implementation for sorting the Cells contained in a TableModel
	/// </summary>
	public class I3HeapSorter : I3SorterBase
	{
		/// <summary>
		/// Initializes a new instance of the HeapSorter class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3HeapSorter(I3TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder) : base(tableModel, column, comparer, sortOrder)
		{
			
		}

		
		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public override void Sort()
		{
			int n;
			int i;

			n = this.TableModel.Rows.Count;

			for (i=n/2; i>0; i--) 
			{
				this.DownHeap(i, n);
			}
			do 
			{
				this.Swap(0, n-1);

				n = n - 1;

				this.DownHeap(1, n);
			} while (n>1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="k"></param>
		/// <param name="n"></param>
		private void DownHeap(int k, int n)
		{
			int j;
			bool loop = true;

			while ((k <= n / 2) && loop) 
			{
				j = k + k;

				if (j < n) 
				{
					if (this.Compare(this.TableModel[j-1, this.SortColumn], this.TableModel[j, this.SortColumn]) < 0) 
					{					
						j++;
					}
				}	

				if (this.Compare(this.TableModel[k-1, this.SortColumn], this.TableModel[j-1, this.SortColumn]) >= 0) 
				{
					loop = false;
				} 
				else 
				{
					this.Swap(k-1, j-1);
					
					k = j;
				}
			}
		}
	}
}
