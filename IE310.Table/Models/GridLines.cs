
using System;


namespace IE310.Table.Models
{
	/// <summary>
	/// Specifies how a Table draws grid lines between its rows and columns
	/// </summary>
	public enum I3GridLines
	{
		/// <summary>
		/// No grid lines are drawn
		/// </summary>
		None = 0,

		/// <summary>
		/// Grid lines are only drawn between columns
		/// </summary>
		Columns = 1,

		/// <summary>
		/// Grid lines are only drawn between rows
		/// </summary>
		Rows = 2,

		/// <summary>
		/// Grid lines are drawn between rows and columns
		/// </summary>
		Both = 3
	}
}
