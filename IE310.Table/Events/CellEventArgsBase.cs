


using System;

using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{ 
	/// <summary>
    /// Cell事件数据的基础类，包含Cell、行、列、CellPos等信息
	/// Base class for classes containing Cell event data
	/// </summary>
	public class I3CellEventArgsBase : EventArgs
	{
		#region Class Data
		
		/// <summary>
        /// 引发事件的Cell
		/// The Cell that Raised the event
		/// </summary>
		private I3Cell source;

		/// <summary>
        /// Cell的行号
		/// The Column index of the Cell
		/// </summary>
		private int column;

		/// <summary>
        /// Cell的列号
		/// The Row index of the Cell
		/// </summary>
		private int row;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source and event type
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		public I3CellEventArgsBase(I3Cell source) : this(source, -1, -1)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="row">The Row index of the Cell</param>
		public I3CellEventArgsBase(I3Cell source, int column, int row) : base()
		{
			this.source = source;
			this.column = column;
			this.row = row;
		}

		#endregion


		#region Properties

        /// <summary>
        /// 引发事件的Cell
		/// Returns the Cell that Raised the event
		/// </summary>
		public I3Cell Cell
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
        /// 获取引发事件的Cell的列号
		/// Gets the Column index of the Cell
		/// </summary>
		public int Column
		{
			get
			{
				return this.column;
			}
		}


        /// <summary>
        /// 设置列号
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(int column)
		{
			this.column = column;
		}


        /// <summary>
        /// 获取引发事件的Cell的行号
		/// Gets the Row index of the Cell
		/// </summary>
		public int Row
		{
			get
			{
				return this.row;
			}
		}


        /// <summary>
        /// 设置行号
		/// </summary>
		/// <param name="row"></param>
		internal void SetRow(int row)
		{
			this.row = row;
		}


		/// <summary>
        /// 获取CellPost对象
		/// Gets the position of the Cell
		/// </summary>
		public I3CellPos CellPos
		{
			get
			{
				return new I3CellPos(this.Row, this.Column);
			}
		}

		#endregion
	}
}
