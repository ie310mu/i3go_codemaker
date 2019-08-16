

using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
	/// 列Header绘制事件 委托定义
	/// </summary>
	public delegate void I3PaintColumnHeaderEventHandler(object sender, I3PaintColumnHeaderEventArgs e);

    /// <summary>
    /// 行Header绘制事件 委托定义
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void I3PatinRowHeaderEventHandler(object sender,I3PaintRowHeaderEventArgs e);

	#endregion



	#region PaintColumnHeaderEventArgs
	
	/// <summary>
	/// 列Header绘制事件参数
	/// </summary>
	public class I3PaintColumnHeaderEventArgs : PaintEventArgs
	{
		#region Class Data

		/// <summary>
		/// 绘制的列
		/// </summary>
		private I3Column _column;
		
		/// <summary>
		/// 所在Table
		/// </summary>
        private I3Table _table;
		
		/// <summary>
		/// 列在ColumnModel中的序号
		/// </summary>
		private int _columnIndex;
		
		/// <summary>
		/// 列的样式
		/// </summary>
		private ColumnHeaderStyle _headerStyle;

		/// <summary>
		/// 列Header的绘制区域
		/// </summary>
		private Rectangle _headerRect;

		/// <summary>
		/// 由用户标记此事件是否已经被处理过，如果已经被处理，则将放弃绘制
		/// </summary>
		private bool _handled;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the PaintHeaderEventArgs class with 
		/// the specified graphics and clipping rectangle
		/// </summary>
		/// <param name="g">The Graphics used to paint the Column header</param>
		/// <param name="headerRect">The Rectangle that represents the rectangle 
		/// in which to paint</param>
		public I3PaintColumnHeaderEventArgs(Graphics g, Rectangle headerRect) : this(g, null, null, -1, ColumnHeaderStyle.None, headerRect)
		{

		}
		
		
		/// <summary>
		/// Initializes a new instance of the PaintHeaderEventArgs class with 
		/// the specified graphics, column, table, column index, header style 
		/// and clipping rectangle
		/// </summary>
		/// <param name="g">The Graphics used to paint the Column header</param>
		/// <param name="column">The Column to be painted</param>
		/// <param name="table">The Table the Column's ColumnModel belongs to</param>
		/// <param name="columnIndex">The index of the Column in the Table's ColumnModel</param>
		/// <param name="headerStyle">The style of the Column's header</param>
		/// <param name="headerRect">The Rectangle that represents the rectangle 
		/// in which to paint</param>
        public I3PaintColumnHeaderEventArgs(Graphics g, I3Column column, I3Table table, int columnIndex, ColumnHeaderStyle headerStyle, Rectangle headerRect)
            : base(g, headerRect)
		{
			this._column = column;
			this._table = table;
			this._columnIndex = columnIndex;
			this._column = column;
			this._headerStyle = headerStyle;
			this._headerRect = headerRect;
			this._handled = false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// 获取 绘制的列
		/// </summary>
		public I3Column Column
		{
			get
			{
				return this._column;
			}
		}


		/// <summary>
		/// 设置绘制的列
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(I3Column column)
		{
			this._column = column;
		}


		/// <summary>
		/// 获取Table
		/// </summary>
        public I3Table Table
		{
			get
			{
				return this._table;
			}
		}


		/// <summary>
		/// 设置Table
		/// </summary>
		/// <param name="table"></param>
        internal void SetTable(I3Table table)
		{
			this._table = table;
		}


		/// <summary>
		/// 获取列的序号
		/// </summary>
		public int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}


		/// <summary>
		/// 设置列的序号
		/// </summary>
		/// <param name="columnIndex"></param>
		internal void SetColumnIndex(int columnIndex)
		{
			this._columnIndex = columnIndex;
		}


		/// <summary>
		/// 获取列的样式
		/// </summary>
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this._headerStyle;
			}
		}


		/// <summary>
		/// 设置列的样式
		/// </summary>
		/// <param name="headerStyle"></param>
		internal void SetHeaderStyle(ColumnHeaderStyle headerStyle)
		{
			this._headerStyle = headerStyle;
		}


		/// <summary>
		/// 获取绘制区域
		/// </summary>
		public Rectangle HeaderRect
		{
			get
			{
				return this._headerRect;
			}
		}


		/// <summary>
		/// 设置绘制区域
		/// </summary>
		/// <param name="headerRect"></param>
		internal void SetHeaderRect(Rectangle headerRect)
		{
			this._headerRect = headerRect;
		}


		/// <summary>
		/// 获取或设置一个值，将此值置为true时，表示用户已经处理此事件，将放弃绘制
		/// </summary>
		public bool Handled
		{
			get
			{
				return this._handled;
			}

			set
			{
				this._handled = value;
			}
		}

		#endregion
	}
	#endregion


    
	#region PaintRowHeaderEventArgs
    /// <summary>
    /// 行Header绘制事件参数
    /// </summary>
    public class I3PaintRowHeaderEventArgs : PaintEventArgs
    {
		#region Class Data

		/// <summary>
		/// 绘制的行
		/// </summary>
		private I3Row _row;
		
		/// <summary>
		/// 所在Table
		/// </summary>
        private I3Table _table;
		
		/// <summary>
		/// 行的序号
		/// </summary>
		private int _rowIndex;
		

		/// <summary>
		/// 列Header的绘制区域
		/// </summary>
		private Rectangle _headerRect;

		/// <summary>
		/// 由用户标记此事件是否已经被处理过，如果已经被处理，则将放弃绘制
		/// </summary>
		private bool _handled;

		#endregion


		#region Constructor

		public I3PaintRowHeaderEventArgs(Graphics g, Rectangle headerRect) : this(g, null, null, -1, headerRect)
		{

		}


        public I3PaintRowHeaderEventArgs(Graphics g, I3Row row, I3Table table, int columnIndex, Rectangle headerRect)
            : base(g, headerRect)
		{
			this._row = row;
			this._table = table;
			this._rowIndex = columnIndex;
			this._headerRect = headerRect;
			this._handled = false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// 获取 绘制的行
		/// </summary>
		public I3Row Row
		{
			get
			{
				return this._row;
			}
		}


		/// <summary>
		/// 设置绘制的行
		/// </summary>
		/// <param name="column"></param>
		internal void SetRow(I3Row row)
		{
			this._row = row;
		}


		/// <summary>
		/// 获取Table
		/// </summary>
        public I3Table Table
		{
			get
			{
				return this._table;
			}
		}


		/// <summary>
		/// 设置Table
		/// </summary>
		/// <param name="table"></param>
        internal void SetTable(I3Table table)
		{
			this._table = table;
		}


		/// <summary>
		/// 获取行的序号
		/// </summary>
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}


		/// <summary>
		/// 设置行的序号
		/// </summary>
		/// <param name="rowIndex"></param>
		internal void SetRowIndex(int rowIndex)
		{
			this._rowIndex = rowIndex;
		}




		/// <summary>
		/// 获取绘制区域
		/// </summary>
		public Rectangle HeaderRect
		{
			get
			{
				return this._headerRect;
			}
		}


		/// <summary>
		/// 设置绘制区域
		/// </summary>
		/// <param name="headerRect"></param>
		internal void SetHeaderRect(Rectangle headerRect)
		{
			this._headerRect = headerRect;
		}


		/// <summary>
		/// 获取或设置一个值，将此值置为true时，表示用户已经处理此事件，将放弃绘制
		/// </summary>
		public bool Handled
		{
			get
			{
				return this._handled;
			}

			set
			{
				this._handled = value;
			}
		}

		#endregion
    }
    #endregion
}
