

using System;
using System.Drawing;

using IE310.Table.Editors;
using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
    /// Cell�༭�¼�ί��
	/// Represents the methods that will handle the BeginEdit, StopEdit and 
	/// CancelEdit events of a Table
	/// </summary>
	public delegate void I3CellEditEventHandler(object sender, I3CellEditEventArgs e);

	#endregion

	
	
	#region CellEditEventArgs
	
	/// <summary>
    /// Cell�༭�¼�����
	/// Provides data for the BeginEdit, StopEdit and CancelEdit events of a Table
	/// </summary>
	public class I3CellEditEventArgs : I3CellEventArgsBase
	{
		#region Class Data

		/// <summary>
        /// �����¼��ı༭����
		/// The CellEditor used to edit the Cell
		/// </summary>
		private II3CellEditor editor;
		
		/// <summary>
        /// �����¼���Table
		/// The Table the Cell belongs to
		/// </summary>
        private I3Table table;

		/// <summary>
        /// Cell������
		/// The Cells bounding Rectangle
		/// </summary>
		private Rectangle cellRect;

		/// <summary>
        /// �¼��Ƿ�Ҫ���˳�
		/// Specifies whether the event should be cancelled
		/// </summary>
		private bool cancel;

		/// <summary>
        /// �¼��Ƿ񱻴����
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
        /// ��ȡ�����¼��ı༭����
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
        /// ��ȡ�����¼���Table
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
        /// ��ȡCell������
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
        /// ��ȡ�������¼��Ƿ�Ҫ���˳�
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
        /// ��ȡ�������¼��Ƿ񱻴����
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
