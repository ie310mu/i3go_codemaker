

using System;
using System.Drawing;

using IE310.Table.Editors;
using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
    /// Cell编辑事件委托
	/// Represents the methods that will handle the BeginEdit, StopEdit and 
	/// CancelEdit events of a Table
	/// </summary>
	public delegate void I3CellEditEventHandler(object sender, I3CellEditEventArgs e);

	#endregion

	
	
	#region CellEditEventArgs
	
	/// <summary>
    /// Cell编辑事件参数
	/// Provides data for the BeginEdit, StopEdit and CancelEdit events of a Table
	/// </summary>
	public class I3CellEditEventArgs : I3CellEventArgsBase
	{
		#region Class Data

		/// <summary>
        /// 引发事件的编辑对象
		/// The CellEditor used to edit the Cell
		/// </summary>
		private II3CellEditor editor;
		
		/// <summary>
        /// 引发事件的Table
		/// The Table the Cell belongs to
		/// </summary>
        private I3Table table;

		/// <summary>
        /// Cell的区域
		/// The Cells bounding Rectangle
		/// </summary>
		private Rectangle cellRect;

		/// <summary>
        /// 事件是否要被退出
		/// Specifies whether the event should be cancelled
		/// </summary>
		private bool cancel;

		/// <summary>
        /// 事件是否被处理过
		/// Indicates whether the event was handled
		/// </summary>
		private bool handled;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="editor">The CellEditor used to edit the Cell</param>
		/// <param name="table">The Table that the Cell belongs to</param>
        public I3CellEditEventArgs(I3Cell source, II3CellEditor editor, I3Table table)
            : this(source, editor, table, -1, -1, Rectangle.Empty)
		{
			
		}


		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="editor">The CellEditor used to edit the Cell</param>
		/// <param name="table">The Table that the Cell belongs to</param>
		/// <param name="row">The Column index of the Cell</param>
		/// <param name="column">The Row index of the Cell</param>
		/// <param name="cellRect"></param>
        public I3CellEditEventArgs(I3Cell source, II3CellEditor editor, I3Table table, int row, int column, Rectangle cellRect)
            : base(source, column, row)
		{
			this.editor = editor;
			this.table = table;
			this.cellRect = cellRect;

			this.cancel = false;
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取引发事件的编辑对象
		/// Gets the CellEditor used to edit the Cell
		/// </summary>
		public II3CellEditor Editor
		{
			get
			{
				return this.editor;
			}
		}


		/// <summary>
        /// 获取引发事件的Table
		/// Gets the Table the Cell belongs to
		/// </summary>
        public I3Table Table
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
        /// 获取Cell的区域
		/// Gets the Cells bounding Rectangle
		/// </summary>
		public Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}


		/// <summary>
        /// 获取或设置事件是否要被退出
		/// Gets or sets whether the event should be cancelled
		/// </summary>
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}

			set
			{
				this.cancel = value;
			}
		}


		/// <summary>
        /// 获取或设置事件是否被处理过
		/// Gets or sets a value indicating whether the event was handled
		/// </summary>
		public bool Handled
		{
			get
			{
				return this.handled;
			}

			set
			{
				this.handled = value;
			}
		}

		#endregion
	}

	#endregion
}
