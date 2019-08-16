

using System;

using IE310.Table.Models;
using IE310.Table.Cell;


namespace IE310.Table.Events
{
	#region Delegates

	/// <summary>
    /// Cell的属性改变的委托
	/// Represents the methods that will handle the PropertyChanged event of a Cell
	/// </summary>
	public delegate void I3CellEventHandler(object sender, I3CellEventArgs e);

	#endregion

	
	
	#region CellEventArgs
	
	/// <summary>
    /// CellEventHandler的参数
	/// Provides data for a Cell's PropertyChanged event
	/// </summary>
	public class I3CellEventArgs : I3CellEventArgsBase
	{
		#region Class Data

		/// <summary>
        /// 事件的类型
		/// The type of event
		/// </summary>
		private I3CellEventType eventType;

		/// <summary>
        /// 改变的属性的原始值
		/// The old value of the property
		/// </summary>
		private object oldValue;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source and event type
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the property</param>
		public I3CellEventArgs(I3Cell source, I3CellEventType eventType, object oldValue) : this(source, -1, -1, eventType, oldValue)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index, row index and event type
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the property</param>
		public I3CellEventArgs(I3Cell source, int column, int row, I3CellEventType eventType, object oldValue) : base(source, column, row)
		{
			this.eventType = eventType;
			this.oldValue = oldValue;
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取事件的类型
		/// Gets or sets the type of event
		/// </summary>
		public I3CellEventType EventType
		{
			get
			{
				return this.eventType;
			}
		}


		/// <summary>
        /// 获取改变的属性的原始值
		/// Gets the old value of the property
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
