

using System;

using IE310.Table.Models;
using IE310.Table.Column;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the ColumnInserted, ColumnRemoved 
	/// and HeaderHeightChanged event of a ColumnModel
	/// </summary>
	public delegate void I3ColumnModelEventHandler(object sender, I3ColumnModelEventArgs e);

	#endregion
	
	
	
	#region ColumnModelEventArgs

	/// <summary>
	/// Provides data for a ColumnModel's ColumnAdded, ColumnRemoved, 
	/// and HeaderHeightChanged events
	/// </summary>
	public class I3ColumnModelEventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The ColumnModel that Raised the event
		/// </summary>
		private I3ColumnModel source;

		/// <summary>
		/// The affected Column
		/// </summary>
		private I3Column column;

		/// <summary>
		/// The start index of the affected Column(s)
		/// </summary>
		private int fromIndex;

		/// <summary>
		/// The end index of the affected Column(s)
		/// </summary>
		private int toIndex;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColumnModelEventArgs class with 
		/// the specified ColumnModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The ColumnModel that originated the event</param>
		/// <param name="column">The affected Column</param>
		/// <param name="fromIndex">The start index of the affected Column(s)</param>
		/// <param name="toIndex">The end index of the affected Column(s)</param>
		public I3ColumnModelEventArgs(I3ColumnModel source, I3Column column, int fromIndex, int toIndex) : base()
		{
			this.source = source;
			this.column = column;
			this.fromIndex = fromIndex;
			this.toIndex = toIndex;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the ColumnModel that Raised the event
		/// </summary>
		public I3ColumnModel ColumnModel
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// Gets the affected Column
		/// </summary>
		public I3Column Column
		{
			get
			{
				return this.column;
			}
		}


		/// <summary>
		/// Gets the start index of the affected Column(s)
		/// </summary>
		public int FromIndex
		{
			get
			{
				return this.fromIndex;
			}
		}


		/// <summary>
		/// Gets the end index of the affected Column(s)
		/// </summary>
		public int ToIndex
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
