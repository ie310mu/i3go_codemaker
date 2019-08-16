

using System;

using IE310.Table.Models;
using IE310.Table.Column;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
    /// 列事件的委托定义
	/// Represents the methods that will handle the PropertyChanged event of a Column, 
	/// or a Table's BeginSort and EndSort events
	/// </summary>
	public delegate void I3ColumnEventHandler(object sender, I3ColumnEventArgs e);

	#endregion
	
	
	
	#region ColumnEventArgs

	/// <summary>
    /// 列事件的参数定义
	/// Provides data for a Column's PropertyChanged event, or a Table's 
	/// BeginSort and EndSort events
	/// </summary>
	public class I3ColumnEventArgs
	{
		#region Class Data

		/// <summary>
        /// 引发事件的列
		/// The Column that Raised the event
		/// </summary>
		private I3Column source;

		/// <summary>
        /// 引发事件的列在Table.ColumnModel中的序号
		/// The index of the Column in the ColumnModel
		/// </summary>
		private int index;

		/// <summary>
        /// 改变的属性的原始值
		/// The old value of the property that changed
		/// </summary>
		private object oldValue;

		/// <summary>
        /// 列事件的类型
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
        /// 获取引发事件的列
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
		/// 设置引发事件的列
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(I3Column column)
		{
			this.source = column;
		}


		/// <summary>
        /// 获取引发事件的列在Table.ColumnModel中的序号
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
        /// 设置引发事件的列在Table.ColumnModel中的序号
		/// </summary>
		/// <param name="index"></param>
		internal void SetIndex(int index)
		{
			this.index = index;
		}


		/// <summary>
        /// 获取事件类型
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
        /// 获取原始值
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
