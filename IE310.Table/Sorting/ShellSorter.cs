using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// A ShellSort implementation for sorting the Cells contained in a TableModel
	/// </summary>
	public class I3ShellSorter : I3SorterBase
	{
		/// <summary>
		/// Initializes a new instance of the ShellSorter class with the specified 
		/// TableModel, Column index, IComparer and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="comparer">The IComparer used to sort the Column's Cells</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3ShellSorter(I3TableModel tableModel, int column, IComparer comparer, SortOrder sortOrder) : base(tableModel, column, comparer, sortOrder)
		{
			
		}

		
		/// <summary>
		/// Starts sorting the Cells in the TableModel
		/// </summary>
		public override void Sort()
		{
			int h;
			int i;
			int j;
			I3Row b;
			bool loop = true;

			h = 1;

			while (h * 3 + 1 <= this.TableModel.Rows.Count) 
			{
				h = 3 * h + 1;
			}

			while (h > 0) 
			{
				for (i=h-1; i<this.TableModel.Rows.Count; i++) 
				{
					b = this.TableModel.Rows[i];
					j = i;
					loop = true;

					while (loop) 
					{
						if (j >= h) 
						{
							if (this.Compare(this.TableModel[j - h, this.SortColumn], b.Cells[this.SortColumn]) > 0) 
							{
								this.Set(j, j-h);
								
								j = j - h;
							} 
							else 
							{
								loop = false;
							}
						} 
						else 
						{
							loop = false;
						}
					}

					this.Set(j, b);
				}

				h = h / 3;
			}
		}
	}
}
