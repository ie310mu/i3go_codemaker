
using System;

using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the RowAdded and RowRemoved 
	/// events of a TableModel
	/// </summary>
	public delegate void I3TableModelEventHandler(object sender, I3TableModelEventArgs e);

	#endregion



	#region TableModelEventArgs

	/// <summary>
	/// Provides data for a TableModel's RowAdded and RowRemoved events
	/// </summary>
	public class I3TableModelEventArgs : EventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The TableModel that Raised the event
		/// </summary>
		private I3TableModel source;

		/// <summary>
		/// The affected Row
		/// </summary>
		private I3Row row;

		/// <summary>
		/// The start index of the affected Row(s)
		/// </summary>
		private int toIndex;

		/// <summary>
		/// The end index of the affected Row(s)
		/// </summary>
		private int fromIndex;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		public I3TableModelEventArgs(I3TableModel source) : this(source, null, -1, -1)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="fromIndex">The start index of the affected Row(s)</param>
		/// <param name="toIndex">The end index of the affected Row(s)</param>
		public I3TableModelEventArgs(I3TableModel source, int fromIndex, int toIndex) : this(source, null, fromIndex, toIndex)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="row">The affected Row</param>
		/// <param name="fromIndex">The start index of the affected Row(s)</param>
		/// <param name="toIndex">The end index of the affected Row(s)</param>
		public I3TableModelEventArgs(I3TableModel source, I3Row row, int fromIndex, int toIndex)
		{
			this.source = source;
			this.row = row;
			this.fromIndex = fromIndex;
			this.toIndex = toIndex;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the TableModel that Raised the event
		/// </summary>
		public I3TableModel TableModel
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// Gets the affected Row
		/// </summary>
		public I3Row Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets the start index of the affected Row(s)
		/// </summary>
		public int RowFromIndex
		{
			get
			{
				return this.fromIndex;
			}
		}


		/// <summary>
		/// Gets the end index of the affected Row(s)
		/// </summary>
		public int RowToIndex
		{
			get
			{
				return this.toIndex;
			}
		}

		#endregion
	}

	#endregion
}
