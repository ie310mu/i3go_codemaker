

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Renderers;
using IE310.Table.Win32;
using IE310.Table.Cell;
using IE310.Table.Column;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 下拉编辑器的父类
	/// A base class for editing Cells that contain drop down buttons
	/// </summary>
	public abstract class I3DropDownCellEditor : I3CellEditor, II3EditorUsesRendererButtons
	{
		#region Class Data

		/// <summary>
        /// 下拉控件容器，是一个Form
		/// The container that holds the Control displayed when editor is dropped down
		/// </summary>
		private I3DropDownContainer dropDownContainer;

		/// <summary>
        /// 标志下拉框当前是否显示
		/// Specifies whether the DropDownContainer is currently displayed
		/// </summary>
		private bool droppedDown;

		/// <summary>
        /// 标志下拉风格
		/// Specifies the DropDown style
		/// </summary>
		private I3DropDownStyle dropDownStyle;

		/// <summary>
        /// 用户为下拉窗口定义的宽度
		/// The user defined width of the DropDownContainer
		/// </summary>
		private int dropDownWidth;

		/// <summary>
        /// 窗体活动、取消活动的钩子
		/// Listener for WM_NCACTIVATE and WM_ACTIVATEAPP messages
		/// </summary>
		private I3ActivationListener activationListener;

		/// <summary>
        /// 拥有下拉窗口（Form）的Form
		/// The Form that will own the DropDownContainer
		/// </summary>
		private Form parentForm;

		/// <summary>
        /// 标志鼠标当前是否在下拉容器内
		/// Specifies whether the mouse is currently over the 
		/// DropDownContainer
		/// </summary>
		private bool containsMouse;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the DropDownCellEditor class with default settings
		/// </summary>
		public I3DropDownCellEditor() : base()
		{
            //编辑控件是一个TextBox
			TextBox textbox = new TextBox();
			textbox.AutoSize = false;
			textbox.BackColor = SystemColors.Window;
			textbox.BorderStyle = BorderStyle.None;
			textbox.MouseEnter += new EventHandler(textbox_MouseEnter);
			this.Control = textbox;

			this.dropDownContainer = new I3DropDownContainer(this);

			this.droppedDown = false;
			this.DropDownStyle = I3DropDownStyle.DropDownList;
			this.dropDownWidth = -1;

			this.parentForm = null;
			this.activationListener = new I3ActivationListener(this);
			this.containsMouse = false;
		}

		#endregion


		#region Methods

        /// <summary>
        /// 下拉控件的宽度增量
        /// </summary>
        /// <returns></returns>
        public virtual int WidthIncrement()
        {
            return 0;
        }
        /// <summary>
        /// 下拉控件的高度增量
        /// </summary>
        /// <returns></returns>
        public virtual int HeightIncrement()
        {
            return 0;
        }

		/// <summary>
        /// 准备编辑，继承父类的功能
		/// Prepares the CellEditor to edit the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to be edited</param>
		/// <param name="table">The Table that contains the Cell</param>
		/// <param name="cellPos">A CellPos representing the position of the Cell</param>
		/// <param name="cellRect">The Rectangle that represents the Cells location and size</param>
		/// <param name="userSetEditorValues">Specifies whether the ICellEditors 
		/// starting value has already been set by the user</param>
		/// <returns>true if the ICellEditor can continue editing the Cell, false otherwise</returns>
        public override bool PrepareForEditing(I3Cell cell, I3Table table, I3CellPos cellPos, Rectangle cellRect, bool userSetEditorValues)
		{
			if (!(table.ColumnModel.Columns[cellPos.Column] is I3DropDownColumn))
			{
				throw new InvalidOperationException("Cannot edit Cell as DropDownCellEditor can only be used with a DropDownColumn");
			}

            I3DropDownColumn dropDownColumn = table.ColumnModel.Columns[cellPos.Column] as I3DropDownColumn;
            this.DropDownStyle = dropDownColumn.DropDownStyle;
			
			return base.PrepareForEditing (cell, table, cellPos, cellRect, userSetEditorValues);
		}


		/// <summary>
        /// 开始编辑，设置了TextBox的KeyPress和LostFocus事件
        /// 并设置parentForm为Table的Form、显示下拉容器
		/// Starts editing the Cell
		/// </summary>
		public override void StartEditing()
		{
			this.TextBox.KeyPress += new KeyPressEventHandler(OnKeyPress);
			this.TextBox.LostFocus += new EventHandler(OnLostFocus);

			base.StartEditing();

			this.parentForm = this.EditingTable.FindForm();

			if (this.DroppedDown)
			{
				this.ShowDropDown();
			}

            this.TextBox.Focus();
		}


		/// <summary>
        /// 结束编辑：取消事件关联，隐藏下拉容器，parentForm = null
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public override void StopEditing()
		{
			this.TextBox.KeyPress -= new KeyPressEventHandler(OnKeyPress);
			this.TextBox.LostFocus -= new EventHandler(OnLostFocus);
			
			base.StopEditing();

			this.DroppedDown = false;

			this.parentForm = null;
		}


		/// <summary>
        /// 取消编辑：取消事件关联，隐藏下拉容器，parentForm = null
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public override void CancelEditing()
		{
			this.TextBox.KeyPress -= new KeyPressEventHandler(OnKeyPress);
			this.TextBox.LostFocus -= new EventHandler(OnLostFocus);
			
			base.CancelEditing();

			this.DroppedDown = false;

			this.parentForm = null;
		}

        /// <summary>
        /// 获取弹出框的Top，此处提供默认实现，子类可自行设置
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected virtual int GetDropDownContainerTop(Point p)
        {
            return p.Y + this.TextBox.Height + 1;
        }

		/// <summary>
        /// 显示下拉容器
		/// Displays the drop down portion to the user
		/// </summary>
		protected virtual void ShowDropDown()
		{
            //TextBox.Location在准备编辑方法中被赋值
			Point p = this.EditingTable.PointToScreen(this.TextBox.Location);
            //p.Y += this.TextBox.Height + 1;
            p.Y = GetDropDownContainerTop(p);

			Rectangle screenBounds = Screen.GetBounds(p);

			if (p.Y + this.dropDownContainer.Height > screenBounds.Bottom)
			{
				p.Y -= this.TextBox.Height + this.dropDownContainer.Height + 1;
			}

			if (p.X + this.dropDownContainer.Width > screenBounds.Right)
			{
				II3CellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
				int buttonWidth = ((I3DropDownCellRenderer) renderer).ButtonWidth;
				
				p.X = p.X + this.TextBox.Width + buttonWidth - this.dropDownContainer.Width;
			}
			
			this.dropDownContainer.Location = p;

			this.parentForm.AddOwnedForm(this.dropDownContainer);
			this.activationListener.AssignHandle(this.parentForm.Handle);

			this.dropDownContainer.ShowDropDown();
			this.dropDownContainer.Activate();

			// A little bit of fun.  We've shown the popup,
			// but because we've kept the main window's
			// title bar in focus the tab sequence isn't quite
			// right.  This can be fixed by sending a tab,
			// but that on its own would shift focus to the
			// second control in the form.  So send a tab,
			// followed by a reverse-tab.

			// Send a Tab command:
			I3NativeMethods.keybd_event((byte) Keys.Tab, 0, 0, 0);
			I3NativeMethods.keybd_event((byte) Keys.Tab, 0, I3KeyEventFFlags.KEYEVENTF_KEYUP, 0);

			// Send a reverse Tab command:
			I3NativeMethods.keybd_event((byte) Keys.ShiftKey, 0, 0, 0);
			I3NativeMethods.keybd_event((byte) Keys.Tab, 0, 0, 0);
			I3NativeMethods.keybd_event((byte) Keys.Tab, 0, I3KeyEventFFlags.KEYEVENTF_KEYUP, 0);
			I3NativeMethods.keybd_event((byte) Keys.ShiftKey, 0, I3KeyEventFFlags.KEYEVENTF_KEYUP, 0);
		}


		/// <summary>
        /// 隐藏下拉容器
		/// Conceals the drop down portion from the user
		/// </summary>
		protected virtual void HideDropDown()
		{
			this.dropDownContainer.HideDropDown();

			this.parentForm.RemoveOwnedForm(this.dropDownContainer);

			this.activationListener.ReleaseHandle();

			this.parentForm.Activate();
		}


		/// <summary>
        /// 指示下拉容器显示时，鼠标点击在区域外时，是否结束编辑
        /// （永远返回true）
		/// Gets whether the editor should stop editing if a mouse click occurs 
		/// outside of the DropDownContainer while it is dropped down
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="cursorPos">The current position of the mouse cursor</param>
		/// <returns>true if the editor should stop editing, false otherwise</returns>
		protected virtual bool ShouldStopEditing(Control target, Point cursorPos)
		{
			return true;
		}


		/// <summary>
        /// 过滤鼠标消息
		/// Filters out a mouse message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public override bool ProcessMouseMessage(Control target, I3WindowMessage msg, int wParam, int lParam)
		{
			if (this.DroppedDown)
			{
				if (msg == I3WindowMessage.WM_LBUTTONDOWN || msg == I3WindowMessage.WM_RBUTTONDOWN || 
					msg == I3WindowMessage.WM_MBUTTONDOWN || msg == I3WindowMessage.WM_XBUTTONDOWN || 
					msg == I3WindowMessage.WM_NCLBUTTONDOWN || msg == I3WindowMessage.WM_NCRBUTTONDOWN || 
					msg == I3WindowMessage.WM_NCMBUTTONDOWN || msg == I3WindowMessage.WM_NCXBUTTONDOWN)
				{	
					Point cursorPos = Cursor.Position;
				
					if (!this.DropDown.Bounds.Contains(cursorPos))
					{
						if (target != this.EditingTable && target != this.TextBox)
						{
							if (this.ShouldStopEditing(target, cursorPos))
							{
								this.EditingTable.StopEditing();
							}
						}
					}
				}
				else if (msg == I3WindowMessage.WM_MOUSEMOVE)
				{
					Point cursorPos = Cursor.Position;
				
					if (this.DropDown.Bounds.Contains(cursorPos))
					{
						if (!this.containsMouse)
						{
							this.containsMouse = true;

							this.EditingTable.RaiseCellMouseLeave(this.EditingCellPos);
						}
					}
					else
					{
						this.containsMouse = true;
					}
				}
			}
			
			return false;
		}


		/// <summary>
        /// 过滤键盘消息 
		/// Filters out a key message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public override bool ProcessKeyMessage(Control target, I3WindowMessage msg, int wParam, int lParam)
		{
			if (msg == I3WindowMessage.WM_KEYDOWN)
			{
				if (((Keys) wParam) == Keys.F4)
				{
					if (this.TextBox.Focused || this.DropDown.ContainsFocus)
					{
						this.DroppedDown = !this.DroppedDown;

						return true;
					}
				}
			}

			return false;
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取编辑控件
		/// Gets the TextBox used to edit the Cells contents
		/// </summary>
		protected TextBox TextBox
		{
			get
			{
				return this.Control as TextBox;
			}
		}


		/// <summary>
        /// 获取下拉容器
		/// Gets the container that holds the Control displayed when editor is dropped down
		/// </summary>
		protected I3DropDownContainer DropDown
		{
			get
			{
				return this.dropDownContainer;
			}
		}


		/// <summary>
        /// 获取或设置下拉容器是否显示
		/// Gets or sets whether the editor is displaying its drop-down portion
		/// </summary>
		public bool DroppedDown
		{
			get
			{
				return this.droppedDown;
			}

			set
			{
				if (this.droppedDown != value)
				{
					this.droppedDown = value;

					if (value)
					{
						this.ShowDropDown();
					}
					else
					{
						this.HideDropDown();
					}
				}
			}
		}


		/// <summary>
        /// 获取或设置下拉容器宽度
		/// Gets or sets the width of the of the drop-down portion of the editor
		/// </summary>
		public int DropDownWidth
		{
			get
			{
				if (this.dropDownWidth != -1)
				{
					return this.dropDownWidth;
				}
				
				return this.dropDownContainer.Width;
			}

			set
			{
				this.dropDownWidth = value;				
				this.dropDownContainer.Width = value;
			}
		}


		/// <summary>
        /// 获取用户指定的下拉容器的宽度
		/// Gets the user defined width of the of the drop-down portion of the editor
		/// </summary>
		internal int InternalDropDownWidth
		{
			get
			{
				return this.dropDownWidth;
			}
		}


		/// <summary>
        /// 获取或设置下拉风格
		/// Gets or sets a value specifying the style of the drop down editor
		/// </summary>
		protected I3DropDownStyle DropDownStyle
		{
			get
			{
				return this.dropDownStyle;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3DropDownStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3DropDownStyle));
				}
				
				if (this.dropDownStyle != value)
				{
					this.dropDownStyle = value;

					this.TextBox.ReadOnly = (value == I3DropDownStyle.DropDownList);
				}
			}
		}


		/// <summary>
        /// 获取或设置选择的文本
		/// Gets or sets the text that is selected in the editable portion of the editor
		/// </summary>
		public string SelectedText
		{
			get
			{
				if (this.DropDownStyle == I3DropDownStyle.DropDownList)
				{
					return "";
				}

				return this.TextBox.SelectedText;
			}

			set
			{
				if (this.DropDownStyle != I3DropDownStyle.DropDownList && value != null)
				{
					this.TextBox.SelectedText = value;
				}
			}
		}


		/// <summary>
        /// 获取或设置选择的文本的长度
		/// Gets or sets the number of characters selected in the editable portion 
		/// of the editor
		/// </summary>
		public int SelectionLength
		{
			get
			{
				return this.TextBox.SelectionLength;
			}

			set
			{
				this.TextBox.SelectionLength = value;
			}
		}


		/// <summary>
        /// 获取或设置选择文本的开始位置
		/// Gets or sets the starting index of text selected in the editor
		/// </summary>
		public int SelectionStart
		{
			get
			{
				return this.TextBox.SelectionStart;
			}

			set
			{
				this.TextBox.SelectionStart = value;
			}
		}


		/// <summary>
        /// 获取或设置文本
		/// Gets or sets the text associated with the editor
		/// </summary>
		public string Text
		{
			get
			{
				return this.TextBox.Text;
			}

			set
			{
				this.TextBox.Text = value;
			}
		}

		#endregion
		

		#region Events

		/// <summary>
        /// TextBox的OnKeyPress事件，如果是回车，结束编辑，如果是Cancel，取消编辑
		/// Handler for the editors TextBox.KeyPress event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A KeyPressEventArgs that contains the event data</param>
		protected virtual void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == I3AsciiChars.CarriageReturn /*Enter*/)
			{
				if (this.EditingTable != null)
				{
					this.EditingTable.StopEditing();
				}
			}
			else if (e.KeyChar == I3AsciiChars.Escape)
			{
				if (this.EditingTable != null)
				{
					this.EditingTable.CancelEditing();
				}
			}
		}


		/// <summary>
        /// TextBox的OnLostFocus事件，结束编辑
		/// Handler for the editors TextBox.LostFocus event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnLostFocus(object sender, EventArgs e)
		{
			if (this.TextBox.Focused || this.DropDown.ContainsFocus)
			{
				return;
			}
			
			if (this.EditingTable != null)
			{
				this.EditingTable.StopEditing();
			}
		}

		
		/// <summary>
        /// 下拉按钮按下事件，隐藏或显示下拉容器
		/// Handler for the editors drop down button MouseDown event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public virtual void OnEditorButtonMouseDown(object sender, I3CellMouseEventArgs e)
		{
			this.DroppedDown = !this.DroppedDown;
		}


        /// <summary>
        /// 下拉按钮弹起事件，不做任何处理
        /// Handler for the editors drop down but
		/// Handler for the editors drop down button MouseUp event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public virtual void OnEditorButtonMouseUp(object sender, I3CellMouseEventArgs e)
		{
			
		}


		/// <summary>
        /// ?????????补充
		/// Handler for the editors textbox MouseEnter event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void textbox_MouseEnter(object sender, EventArgs e)
		{
			this.EditingTable.RaiseCellMouseLeave(this.EditingCellPos);
		}

		#endregion


		#region ActivationListener

		/// <summary>
        /// 窗体活动、取消活动的监听器
		/// Listener for WM_NCACTIVATE and WM_ACTIVATEAPP messages
		/// </summary>
		internal class I3ActivationListener : IE310.Table.Win32.I3NativeWindow
		{
			/// <summary>
            /// 监听器的Owner：一个DropDownCellEditor对象
			/// The DropDownCellEditor that owns the listener
			/// </summary>
			private I3DropDownCellEditor owner;


			/// <summary>
            /// 构造函数
			/// Initializes a new instance of the DropDownCellEditor class with the 
			/// specified DropDownCellEditor owner
			/// </summary>
			/// <param name="owner">The DropDownCellEditor that owns the listener</param>
			public I3ActivationListener(I3DropDownCellEditor owner) : base()
			{
				this.owner = owner;
			}


			/// <summary>
            /// 获取或设置Editor，实际上就是owner
			/// Gets or sets the DropDownCellEditor that owns the listener
			/// </summary>
			public I3DropDownCellEditor Editor
			{
				get
				{
					return this.owner;
				}

				set
				{
					this.owner = value;
				}
			}


			/// <summary>
            /// 消息钩子，处理了WM_NCACTIVATE消息和WM_ACTIVATEAPP消息
            /// 这两个消息的意思？有待查找资料
			/// Processes Windows messages
			/// </summary>
			/// <param name="m">The Windows Message to process</param>
			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);
				
				if (this.owner != null && this.owner.DroppedDown)
				{
					if (m.Msg == (int) I3WindowMessage.WM_NCACTIVATE)
					{
						if (((int) m.WParam) == 0)
						{
							I3NativeMethods.SendMessage(this.Handle, (int) I3WindowMessage.WM_NCACTIVATE, 1, 0);
						}
					}
					else if (m.Msg == (int) I3WindowMessage.WM_ACTIVATEAPP)
					{
						if ((int)m.WParam == 0)
						{
							this.owner.DroppedDown = false;
							
							I3NativeMethods.PostMessage(this.Handle, (int) I3WindowMessage.WM_NCACTIVATE, 0, 0);
						}
					}
				}
			}
		}

		#endregion
	}
}
