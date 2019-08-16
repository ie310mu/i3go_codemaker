using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Row;
using IE310.Table.Cell;


namespace IE310.Table.Sorting
{
	/// <summary>
	/// An IComparer for sorting Cells that contain Color information
	/// </summary>
	public class I3ColorComparer : I3ComparerBase
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColorComparer class with the specified 
		/// TableModel, Column index and SortOrder
		/// </summary>
		/// <param name="tableModel">The TableModel that contains the data to be sorted</param>
		/// <param name="column">The index of the Column to be sorted</param>
		/// <param name="sortOrder">Specifies how the Column is to be sorted</param>
		public I3ColorComparer(I3TableModel tableModel, int column, SortOrder sortOrder) : base(tableModel, column, sortOrder)
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

			// check for null data
			if (cell1.Data == null && cell2.Data == null)
			{
				return 0;
			}
			else if (cell1.Data == null)
			{
				return -1;
			}
			else if (cell2.Data == null)
			{
				return 1;
			}

			Color color1 = (Color) cell1.Data;
			Color color2 = (Color) cell2.Data;

			if (color1.GetHue() < color2.GetHue()) 
			{
				return -1; 
			}
			else if (color1.GetHue() > color2.GetHue()) 
			{
				return 1;
			}
			else 
			{
				if (color1.GetSaturation() < color2.GetSaturation()) 
				{
					return -1;
				}
				else if (color1.GetSaturation() > color2.GetSaturation()) 
				{
					return 1;
				}
				else 
				{
					if (color1.GetBrightness() < color2.GetBrightness()) 
					{
						return -1;
					}
					else if (color1.GetBrightness() > color2.GetBrightness()) 
					{
						return 1;
					}
					
					return 0;
				}
			}
		}

		#endregion
	}
}
