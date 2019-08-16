using System;
using System.Collections;
using IE310.Table.Row;


namespace IE310.Table.Design
{
	/// <summary>
	/// 
	/// </summary>
	internal class I3RowComparer : IComparer
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			I3Row row1 = (I3Row) x;
			I3Row row2 = (I3Row) y;
			
			// check for null rows
			if (row1 == null && row2 == null)
			{
				return 0;
			}
			else if (row1 == null)
			{
				return -1;
			}
			else if (row2 == null)
			{
				return 1;
			}

			if (row1.InternalIndex < row2.InternalIndex)
			{
				return -1;
			}
			else if (row1.InternalIndex < row2.InternalIndex)
			{
				return 1;
			}

			return 0;
		}
	}
}
