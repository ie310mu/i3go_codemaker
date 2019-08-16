using System;
using System.Collections;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;
using IE310.Table.Cell;
using IE310.Table.Column;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// An IComparer for sorting Cells that contain CheckBoxes
	/// </summary>
	public class I3CheckBoxComparer : I3ComparerBase
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CheckBoxComparer class with the specified 
		/// TableModel, Column index and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3CheckBoxComparer(I3TableModel tableModel, int column, SortOrder sortOrder) : base(tableModel, column, sortOrder)
		{
			
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
		public override int Compare(object a, object b)
		{
			I3Cell cell1 = (I3Cell) a;
			I3Cell cell2 = (I3Cell) b;
			
			// check for null cells
			if (cell1 == null && cell2 == null)
			{
				return 0;
			}
			else if (cell1 == null)
			{
				return -1;
			}
			else if (cell2 == null)
			{
				return 1;
			}

			int retVal = 0;

			if (cell1.Checked && !cell2.Checked)
			{
				retVal = -1;
			}
			else if (!cell1.Checked && cell2.Checked)
			{
				retVal = 1;
			}

			// if the cells have the same checked value and the CheckBoxColumn 
			// they belong to allows text drawing, compare the text properties 
			// to determine order
			if (retVal == 0 && ((I3CheckBoxColumn) this.TableModel.Table.ColumnModel.Columns[this.SortColumn]).DrawText)
			{
                // check for null data
                string text = cell1.Row.TableModel.Table.ColumnModel.Columns[cell1.Index].DataToString(cell1.Data);
                string text2 = cell2.Row.TableModel.Table.ColumnModel.Columns[cell2.Index].DataToString(cell2.Data);
                if (text == null && text2 == null)
				{
					return 0;
				}
                else if (text == null)
				{
					return -1;
				}

                retVal = text.CompareTo(text2);
			}

			return retVal;
		}

		#endregion
	}
}
