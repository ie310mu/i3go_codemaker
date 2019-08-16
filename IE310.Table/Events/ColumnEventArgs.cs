

using System;

using IE310.Table.Models;
using IE310.Table.Column;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
    /// ���¼���ί�ж���
	/// Represents the methods that will handle the PropertyChanged event of a Column, 
	/// or a Table's BeginSort and EndSort events
	/// </summary>
	public delegate void I3ColumnEventHandler(object sender, I3ColumnEventArgs e);

	#endregion
	
	
	
	#region ColumnEventArgs

	/// <summary>
    /// ���¼��Ĳ�������
	/// Provides data for a Column's PropertyChanged event, or a Table's 
	/// BeginSort and EndSort events
	/// </summary>
	public class I3ColumnEventArgs
	{
		#region Class Data

		/// <summary>
        /// �����¼�����
		/// The Column that Raised the event
		/// </summary>
		private I3Column source;

		/// <summary>
        /// �����¼�������Table.ColumnModel�е����
		/// The index of the Column in the ColumnModel
		/// </summary>
		private int index;

		/// <summary>
        /// �ı�����Ե�ԭʼֵ
		/// The old value of the property that changed
		/// </summary>
		private object oldValue;

		/// <summary>
        /// ���¼�������
		/// The type of event
		/// </summary>
		private I3ColumnEventType eventType;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColumnEventArgs class with 
		/// the specified Column source, column index and event type
		/// </summary>
		/// <param name="source">The Column that Raised the event</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the changed property</param>
		public I3ColumnEventArgs(I3Column source, I3ColumnEventType eventType, object oldValue) : this(source, -1, eventType, oldValue)
		{

		}

		
		/// <summary>
		/// Initializes a new instance of the ColumnEventArgs class with 
		/// the specified Column source, column index and event type
		/// </summary>
		/// <param name="source">The Column that Raised the event</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the changed property</param>
		public I3ColumnEventArgs(I3Column source, int index, I3ColumnEventType eventType, object oldValue) : base()
		{
			this.source = source;
			this.index = index;
			this.eventType = eventType;
			this.oldValue = oldValue;
		}

		#endregion


		#region Properties

		/// <summary>
        /// ��ȡ�����¼�����
		/// Gets the Column that Raised the event
		/// </summary>
		public I3Column Column
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// ���������¼�����
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(I3Column column)
		{
			this.source = column;
		}


		/// <summary>
        /// ��ȡ�����¼�������Table.ColumnModel�е����
		/// Gets the index of the Column
		/// </summary>
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
        /// ���������¼�������Table.ColumnModel�е����
		/// </summary>
		/// <param name="index"></param>
		internal void SetIndex(int index)
		{
			this.index = index;
		}


		/// <summary>
        /// ��ȡ�¼�����
		/// Gets the type of event
		/// </summary>
		public I3ColumnEventType EventType
		{
			get
			{
				return this.eventType;
			}
		}


		/// <summary>
        /// ��ȡԭʼֵ
		/// Gets the old value of the Columns changed property
		/// </summary>
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		#endregion
	}

	#endregion
}
