

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
	/// ��Header�����¼� ί�ж���
	/// </summary>
	public delegate void I3PaintColumnHeaderEventHandler(object sender, I3PaintColumnHeaderEventArgs e);

    /// <summary>
    /// ��Header�����¼� ί�ж���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void I3PatinRowHeaderEventHandler(object sender,I3PaintRowHeaderEventArgs e);

	#endregion



	#region PaintColumnHeaderEventArgs
	
	/// <summary>
	/// ��Header�����¼�����
	/// </summary>
	public class I3PaintColumnHeaderEventArgs : PaintEventArgs
	{
		#region Class Data

		/// <summary>
		/// ���Ƶ���
		/// </summary>
		private I3Column _column;
		
		/// <summary>
		/// ����Table
		/// </summary>
        private I3Table _table;
		
		/// <summary>
		/// ����ColumnModel�е����
		/// </summary>
		private int _columnIndex;
		
		/// <summary>
		/// �е���ʽ
		/// </summary>
		private ColumnHeaderStyle _headerStyle;

		/// <summary>
		/// ��Header�Ļ�������
		/// </summary>
		private Rectangle _headerRect;

		/// <summary>
		/// ���û���Ǵ��¼��Ƿ��Ѿ��������������Ѿ��������򽫷�������
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
		/// ��ȡ ���Ƶ���
		/// </summary>
		public I3Column Column
		{
			get
			{
				return this._column;
			}
		}


		/// <summary>
		/// ���û��Ƶ���
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(I3Column column)
		{
			this._column = column;
		}


		/// <summary>
		/// ��ȡTable
		/// </summary>
        public I3Table Table
		{
			get
			{
				return this._table;
			}
		}


		/// <summary>
		/// ����Table
		/// </summary>
		/// <param name="table"></param>
        internal void SetTable(I3Table table)
		{
			this._table = table;
		}


		/// <summary>
		/// ��ȡ�е����
		/// </summary>
		public int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}


		/// <summary>
		/// �����е����
		/// </summary>
		/// <param name="columnIndex"></param>
		internal void SetColumnIndex(int columnIndex)
		{
			this._columnIndex = columnIndex;
		}


		/// <summary>
		/// ��ȡ�е���ʽ
		/// </summary>
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this._headerStyle;
			}
		}


		/// <summary>
		/// �����е���ʽ
		/// </summary>
		/// <param name="headerStyle"></param>
		internal void SetHeaderStyle(ColumnHeaderStyle headerStyle)
		{
			this._headerStyle = headerStyle;
		}


		/// <summary>
		/// ��ȡ��������
		/// </summary>
		public Rectangle HeaderRect
		{
			get
			{
				return this._headerRect;
			}
		}


		/// <summary>
		/// ���û�������
		/// </summary>
		/// <param name="headerRect"></param>
		internal void SetHeaderRect(Rectangle headerRect)
		{
			this._headerRect = headerRect;
		}


		/// <summary>
		/// ��ȡ������һ��ֵ������ֵ��Ϊtrueʱ����ʾ�û��Ѿ�������¼�������������
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
    /// ��Header�����¼�����
    /// </summary>
    public class I3PaintRowHeaderEventArgs : PaintEventArgs
    {
		#region Class Data

		/// <summary>
		/// ���Ƶ���
		/// </summary>
		private I3Row _row;
		
		/// <summary>
		/// ����Table
		/// </summary>
        private I3Table _table;
		
		/// <summary>
		/// �е����
		/// </summary>
		private int _rowIndex;
		

		/// <summary>
		/// ��Header�Ļ�������
		/// </summary>
		private Rectangle _headerRect;

		/// <summary>
		/// ���û���Ǵ��¼��Ƿ��Ѿ��������������Ѿ��������򽫷�������
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
		/// ��ȡ ���Ƶ���
		/// </summary>
		public I3Row Row
		{
			get
			{
				return this._row;
			}
		}


		/// <summary>
		/// ���û��Ƶ���
		/// </summary>
		/// <param name="column"></param>
		internal void SetRow(I3Row row)
		{
			this._row = row;
		}


		/// <summary>
		/// ��ȡTable
		/// </summary>
        public I3Table Table
		{
			get
			{
				return this._table;
			}
		}


		/// <summary>
		/// ����Table
		/// </summary>
		/// <param name="table"></param>
        internal void SetTable(I3Table table)
		{
			this._table = table;
		}


		/// <summary>
		/// ��ȡ�е����
		/// </summary>
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}


		/// <summary>
		/// �����е����
		/// </summary>
		/// <param name="rowIndex"></param>
		internal void SetRowIndex(int rowIndex)
		{
			this._rowIndex = rowIndex;
		}




		/// <summary>
		/// ��ȡ��������
		/// </summary>
		public Rectangle HeaderRect
		{
			get
			{
				return this._headerRect;
			}
		}


		/// <summary>
		/// ���û�������
		/// </summary>
		/// <param name="headerRect"></param>
		internal void SetHeaderRect(Rectangle headerRect)
		{
			this._headerRect = headerRect;
		}


		/// <summary>
		/// ��ȡ������һ��ֵ������ֵ��Ϊtrueʱ����ʾ�û��Ѿ�������¼�������������
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
