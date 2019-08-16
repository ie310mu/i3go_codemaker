

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using IE310.Table.Events;
using IE310.Table.Models; 
using IE310.Table.Win32;
using IE310.Table.Cell;


namespace IE310.Table.Editors
{
	/// <summary>
    /// Cell editors的基类
	/// Base class for Cell editors
	/// </summary>
	public abstract class I3CellEditor : II3CellEditor, II3MouseMessageFilterClient, II3KeyMessageFilterClient
	{
		#region Event Handlers

		/// <summary>
        /// 开始编辑事件
		/// Occurs when the CellEditor begins editing a Cell
		/// </summary>
		public event I3CellEditEventHandler BeginEdit;

		/// <summary>
        /// 结束编辑事件  注意，e.Cancel无意义
		/// Occurs when the CellEditor stops editing a Cell
		/// </summary>
		public event I3CellEditEventHandler EndEdit;

		/// <summary>
        /// 退出编辑事件 注意，e.Cancel无意义
		/// Occurs when the editing of a Cell is cancelled
		/// </summary>
		public event I3CellEditEventHandler CancelEdit;

		#endregion


		#region Class Data

		/// <summary>
        /// 编辑控件
		/// The Control that is performing the editing
		/// </summary>
		private Control control;

		/// <summary>
        /// 被编辑的Cell
		/// The Cell that is being edited
		/// </summary>
		internal I3Cell cell;

		/// <summary>
        /// 被编辑的Table
		/// The Table that contains the Cell being edited
		/// </summary>
        private I3Table table;

		/// <summary>
        /// 被编辑的Cell的位置
		/// A CellPos that represents the position of the Cell being edited
		/// </summary>
		private I3CellPos cellPos;

		/// <summary>
        /// 被编辑的Cell的位置和大小
		/// The Rectangle that represents the Cells location and size
		/// </summary>
		private Rectangle cellRect;

		/// <summary>
        /// 鼠标消息过滤器
		/// A MouseMessageFilter that receives mouse messages before they 
		/// are dispatched to their destination
		/// </summary>
		private I3MouseMessageFilter mouseMessageFilter;

		/// <summary>
        /// 键盘消息过滤器
		/// A KeyMessageFilter that receives key messages before they 
		/// are dispatched to their destination
		/// </summary>
		private I3KeyMessageFilter keyMessageFilter;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellEditor class with default settings
		/// </summary>
		protected I3CellEditor()
		{
			this.control = null;
			this.cell = null;
			this.table = null;
			this.cellPos = I3CellPos.Empty;
			this.cellRect = Rectangle.Empty;

			this.mouseMessageFilter = new I3MouseMessageFilter(this);
			this.keyMessageFilter = new I3KeyMessageFilter(this);
		}

		#endregion


		#region Methods

        /// <summary>
        /// 编辑器是否拦截鼠标中键事件
        /// </summary>
        /// <returns></returns>
        public virtual bool HandleMouseWheel()
        {
            return false;
        }

		/// <summary>
        /// 虚拟方法，准备编辑，在ICellEditor中定义
		/// Prepares the CellEditor to edit the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to be edited</param>
		/// <param name="table">The Table that contains the Cell</param>
		/// <param name="cellPos">A CellPos representing the position of the Cell</param>
		/// <param name="cellRect">The Rectangle that represents the Cells location and size</param>
		/// <param name="userSetEditorValues">Specifies whether the ICellEditors 
		/// starting value has already been set by the user</param>
		/// <returns>true if the ICellEditor can continue editing the Cell, false otherwise</returns>
        public virtual bool PrepareForEditing(I3Cell cell, I3Table table, I3CellPos cellPos, Rectangle cellRect, bool userSetEditorValues)
		{
			//初始化值
			this.cell = cell;
			this.table = table;
			this.cellPos = cellPos;
			this.cellRect = cellRect;

            //检查用户是否已经自己设置了编辑控件的值，如果没有则调用 SetEditValue() 方法进行设置
			// check if the user has already set the editors value for us
			if (!userSetEditorValues)
			{
				this.SetEditValue();
			}

            //设置编辑控件的位置和大小
			this.SetEditLocation(cellRect);

            //引发开始编辑事件
			// raise the BeginEdit event
			I3CellEditEventArgs e = new I3CellEditEventArgs(cell, this, table, cellPos.Row, cellPos.Column, cellRect);
			e.Handled = userSetEditorValues;

			this.OnBeginEdit(e);
			
            //如果编辑被退出，移动编辑控件并返回false
			// if the edit has been canceled, remove the editor and return false
			if (e.Cancel)
			{
				this.RemoveEditControl();

				return false;
			}

			return true;
		}


		/// <summary>
        /// 抽象方法设置位置和大小
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected abstract void SetEditLocation(Rectangle cellRect);


		/// <summary>
        /// 抽象方法，设置编辑的值
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected abstract void SetEditValue();


		/// <summary>
        /// 抽象方法，设置Cell的值
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected abstract void SetCellValue();


		/// <summary>
        /// 虚拟方法，显示编辑控件
		/// Displays the editor to the user and adds it to the Table's Control
		/// collection
		/// </summary>
		protected virtual void ShowEditControl()
		{
			this.control.Parent = this.table;

			this.control.Visible = true;
		}


		/// <summary>
        /// 虚拟方法，隐藏编辑控件
		/// Conceals the editor from the user, but does not remove it from the 
		/// Table's Control collection
		/// </summary>
		protected virtual void HideEditControl()
		{
			this.control.Visible = false;
		}


		/// <summary>
        /// 虚拟方法，从Table中移除编辑控件
		/// Conceals the editor from the user and removes it from the Table's 
		/// Control collection
		/// </summary>
		protected virtual void RemoveEditControl()
		{
			this.control.Visible = false;
			this.control.Parent = null;

			this.table.Focus();

			this.cell = null;
			this.table = null;
			this.cellPos = I3CellPos.Empty;
			this.cellRect = Rectangle.Empty;
		}


		/// <summary>
        /// 虚拟方法，开始编辑，在ICellEditor中定义
		/// Starts editing the Cell
		/// </summary>
        public virtual void StartEditing()
        {
            this.ShowEditControl();

            Application.AddMessageFilter(this.keyMessageFilter);
            Application.AddMessageFilter(this.mouseMessageFilter);
        }


		/// <summary>
        /// 虚拟方法，结束编辑，在ICellEditor中定义
		/// Stops editing the Cell and commits any changes
		/// </summary>
        public virtual void StopEditing()
        {
            Application.RemoveMessageFilter(this.keyMessageFilter);
            Application.RemoveMessageFilter(this.mouseMessageFilter);

            //
            I3CellEditEventArgs e = new I3CellEditEventArgs(this.cell, this, this.table, this.cellPos.Row, this.cellPos.Column, this.cellRect);

            this.OnEndEdit(e);

            if (!e.Cancel && !e.Handled)
            {
                this.SetCellValue();
            }
            this.table.OnEditingStopped(e);

            this.RemoveEditControl();
        }


		/// <summary>
        /// 虚拟方法，退出编辑，在ICellEditor中定义
		/// Stops editing the Cell and ignores any changes
		/// </summary>
        public virtual void CancelEditing()
        {
            Application.RemoveMessageFilter(this.keyMessageFilter);
            Application.RemoveMessageFilter(this.mouseMessageFilter);

            //
            I3CellEditEventArgs e = new I3CellEditEventArgs(this.cell, this, this.table, this.cellPos.Row, this.cellPos.Column, this.cellRect);

            this.table.OnEditingCancelled(e);
            this.OnCancelEdit(e);

            this.RemoveEditControl();
        }


		/// <summary>
        /// 虚拟方法，过滤鼠标消息
		/// Filters out a mouse message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public virtual bool ProcessMouseMessage(Control target, I3WindowMessage msg, int wParam, int lParam)
		{
			if (msg == I3WindowMessage.WM_LBUTTONDOWN || msg == I3WindowMessage.WM_RBUTTONDOWN || 
				msg == I3WindowMessage.WM_MBUTTONDOWN || msg == I3WindowMessage.WM_XBUTTONDOWN || 
				msg == I3WindowMessage.WM_NCLBUTTONDOWN || msg == I3WindowMessage.WM_NCRBUTTONDOWN || 
				msg == I3WindowMessage.WM_NCMBUTTONDOWN || msg == I3WindowMessage.WM_NCXBUTTONDOWN)
			{	
				Point cursorPos = Cursor.Position;
				
				if (target != this.EditingTable && target != this.Control)
				{
					this.EditingTable.StopEditing();
				}
			}
			
			return false;
		}


		/// <summary>
        /// 虚拟方法，过滤键盘消息
		/// Filters out a key message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public virtual bool ProcessKeyMessage(Control target, I3WindowMessage msg, int wParam, int lParam)
		{
			return false;
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取或设置Control
		/// Gets or sets the Control that is being used to edit the Cell
		/// </summary>
		protected Control Control
		{
			get
			{
				return this.control;
			}

			set
			{
				this.control = value;
			}
		}


		/// <summary>
        /// 获取被编辑的Cell
		/// Gets the Cell that is being edited
		/// </summary>
		public I3Cell EditingCell
		{
			get
			{
				return this.cell;
			}
		}


		/// <summary>
        /// 获取被编辑的Table
		/// Gets the Table that contains the Cell being edited
		/// </summary>
        public I3Table EditingTable
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
        /// 获取被编辑的Cell的位置
		/// Gets a CellPos that represents the position of the Cell being edited
		/// </summary>
		public I3CellPos EditingCellPos
		{
			get
			{
				return this.cellPos;
			}
		}


		/// <summary>
        /// 获取当前是否正在编辑一个Cell
		/// Gets whether the CellEditor is currently editing a Cell
		/// </summary>
		public bool IsEditing
		{
			get
			{
				return this.cell != null;
			}
		}

		#endregion


		#region Events

		/// <summary>
        /// 引发开始编辑事件
		/// Raises the BeginEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnBeginEdit(I3CellEditEventArgs e)
		{
			if (this.BeginEdit != null)
			{
				this.BeginEdit(this, e);
			}
		}


		/// <summary>
        /// 引发结束编辑事件
		/// Raises the EndEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnEndEdit(I3CellEditEventArgs e)
		{
			if (this.EndEdit != null)
			{
				this.EndEdit(this, e);
			}
		}


		/// <summary>
        /// 引发取消编辑事件
		/// Raises the CancelEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnCancelEdit(I3CellEditEventArgs e)
		{
			if (this.CancelEdit != null)
			{
				this.CancelEdit(this, e);
			}
		}

		#endregion


        public virtual Control GetDataInputControl()
        {
            return null;
        }
    }
}
