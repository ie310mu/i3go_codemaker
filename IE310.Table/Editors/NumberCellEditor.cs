


using System;
using System.Drawing;
using System.Globalization;
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
	/// A class for editing Cells that contain numbers
	/// </summary>
	public class I3NumberCellEditor : I3CellEditor, II3EditorUsesRendererButtons
	{
		#region Class Data

		/// <summary>
        /// UP按钮的ID
		/// ID number for the up button
		/// </summary>
		protected static readonly int UpButtonID = 1;

		/// <summary>
        /// Down按钮的ID
		/// ID number for the down button
		/// </summary>
		protected static readonly int DownButtonID = 2;

		/// <summary>
        /// 当前值
		/// The current value of the editor
		/// </summary>
		private decimal currentValue;

		/// <summary>
        /// 目标值
		/// The value to increment or decrement when the up or down buttons are clicked
		/// </summary>
        private decimal increment;

		/// <summary>
        /// 最大值 
		/// The maximum value for the editor
		/// </summary>
        private decimal maximum;

		/// <summary>
        /// 最小值
		/// The inximum value for the editor
		/// </summary>
        private decimal minimum;

		/// <summary>
        /// 格式化字符串
		/// A string that specifies how editors value is formatted
		/// </summary>
		private string format;

		/// <summary>
        /// 鼠标滚动时的缩进量
		/// The amount the mouse wheel has moved
		/// </summary>
		private int wheelDelta;

		/// <summary>
		/// Indicates whether the arrow keys should be passed to the editor
		/// </summary>
		private bool interceptArrowKeys;

		/// <summary>
        /// 标记Text属性是否正在被改变
		/// Specifies whether the editors text value is changing
		/// </summary>
		private bool changingText;

		/// <summary>
        /// timer控件的初始值
		/// Initial interval between timer events
		/// </summary>
		private const int TimerInterval = 500;

		/// <summary>
        /// timer控件的值
		/// Current interval between timer events
		/// </summary>
		private int interval;

		/// <summary>
		/// Indicates whether the user has changed the editors value
		/// </summary>
		private bool userEdit;

		/// <summary>
		/// The bounding Rectangle of the up and down buttons
		/// </summary>
		private Rectangle buttonBounds;

		/// <summary>
		/// The id of the button that was pressed
		/// </summary>
		private int buttonID;

		/// <summary>
		/// Timer to to fire button presses at regular intervals while 
		/// a button is pressed
		/// </summary>
		private Timer timer;

        private NumberColumnType numberColumnType;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the NumberCellEditor class with default settings
		/// </summary>
		public I3NumberCellEditor()
		{
			TextBox textbox = new TextBox();
			textbox.AutoSize = false;
			textbox.BorderStyle = BorderStyle.None;
			this.Control = textbox;

            this.numberColumnType = NumberColumnType.DECIMAL;
			this.currentValue = new decimal(0);
			this.increment = new decimal(1);
			this.minimum = new decimal(0);
			this.maximum = new decimal(100);
			this.format = "G";

			this.wheelDelta = 0;
			this.interceptArrowKeys = true;
			this.userEdit = false;
			this.changingText = false;
			this.buttonBounds = Rectangle.Empty;
			this.buttonID = 0;
			this.interval = TimerInterval;
		}

		#endregion


		#region Methods

        public override Control GetDataInputControl()
        {
            return this.TextBox;
        }

        public override bool HandleMouseWheel()
        {
            return true;
        }

		/// <summary>
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
			//
			if (!(table.ColumnModel.Columns[cellPos.Column] is I3NumberColumn))
			{
				throw new InvalidOperationException("Cannot edit Cell as NumberCellEditor can only be used with a NumberColumn");
			}
			
			if (!(table.ColumnModel.GetCellRenderer(cellPos.Column) is I3NumberCellRenderer))
			{
				throw new InvalidOperationException("Cannot edit Cell as NumberCellEditor can only be used with a NumberColumn that uses a NumberCellRenderer");
			}

            this.NumberColumnType = ((I3NumberColumn)table.ColumnModel.Columns[cellPos.Column]).NumberColumnType;
			this.Minimum = Convert.ToDecimal(((I3NumberColumn) table.ColumnModel.Columns[cellPos.Column]).Minimum);
            this.Maximum = Convert.ToDecimal(((I3NumberColumn)table.ColumnModel.Columns[cellPos.Column]).Maximum);
            this.Increment = Convert.ToDecimal(((I3NumberColumn)table.ColumnModel.Columns[cellPos.Column]).Increment);
			
			return base.PrepareForEditing (cell, table, cellPos, cellRect, userSetEditorValues);
		}


		/// <summary>
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected override void SetEditValue()
		{
			// make sure we start with a valid value
			this.Value = this.Minimum;

			// attempt to get the cells data
            this.Value = Convert.ToDecimal(this.EditingCell.Data);
		}


		/// <summary>
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected override void SetCellValue()
		{
            //this.EditingCell.Data = this.Value;

            switch (this.numberColumnType)
            {
                case NumberColumnType.SBYTE:
                    this.EditingCell.Data = Convert.ToSByte(this.Value);
                    break;
                case NumberColumnType.BYTE:
                    this.EditingCell.Data = Convert.ToByte(this.Value);
                    break;
                case NumberColumnType.SHORT:
                    this.EditingCell.Data = Convert.ToInt16(this.Value);
                    break;
                case NumberColumnType.USHORT:
                    this.EditingCell.Data = Convert.ToUInt16(this.Value);
                    break;
                case NumberColumnType.INT:
                    this.EditingCell.Data = Convert.ToInt32(this.Value);
                    break;
                case NumberColumnType.UINT:
                    this.EditingCell.Data = Convert.ToUInt32(this.Value);
                    break;
                case NumberColumnType.LONG:
                    this.EditingCell.Data = Convert.ToInt64(this.Value);
                    break;
                case NumberColumnType.ULONG:
                    this.EditingCell.Data = Convert.ToUInt64(this.Value);
                    break;
                case NumberColumnType.FLOAT:
                    this.EditingCell.Data = Convert.ToSingle(this.Value);
                    break;
                case NumberColumnType.DOUBLE:
                    this.EditingCell.Data = Convert.ToDouble(this.Value);
                    break;
                case NumberColumnType.DECIMAL:
                    this.EditingCell.Data = Convert.ToDecimal(this.Value);
                    break;
                default:
                    this.EditingCell.Data = Convert.ToDecimal(this.Value);
                    break;
            }
		}


		/// <summary>
		/// Starts editing the Cell
		/// </summary>
		public override void StartEditing()
		{
			this.TextBox.MouseWheel += new MouseEventHandler(OnMouseWheel);
			this.TextBox.KeyDown += new KeyEventHandler(OnTextBoxKeyDown);
			this.TextBox.KeyPress += new KeyPressEventHandler(OnTextBoxKeyPress);
			this.TextBox.LostFocus += new EventHandler(OnTextBoxLostFocus);
			
			base.StartEditing();

			this.TextBox.Focus();
		}


		/// <summary>
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public override void StopEditing()
		{
			this.TextBox.MouseWheel -= new MouseEventHandler(OnMouseWheel);
			this.TextBox.KeyDown -= new KeyEventHandler(OnTextBoxKeyDown);
			this.TextBox.KeyPress -= new KeyPressEventHandler(OnTextBoxKeyPress);
			this.TextBox.LostFocus -= new EventHandler(OnTextBoxLostFocus);
			
			base.StopEditing();
		}


		/// <summary>
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public override void CancelEditing()
		{
			this.TextBox.MouseWheel -= new MouseEventHandler(OnMouseWheel);
			this.TextBox.KeyDown -= new KeyEventHandler(OnTextBoxKeyDown);
			this.TextBox.KeyPress -= new KeyPressEventHandler(OnTextBoxKeyPress);
			this.TextBox.LostFocus -= new EventHandler(OnTextBoxLostFocus);
			
			base.CancelEditing();
		}


		/// <summary>
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected override void SetEditLocation(Rectangle cellRect)
		{
			// calc the size of the textbox
			II3CellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
			int buttonWidth = ((I3NumberCellRenderer) renderer).ButtonWidth;

			this.TextBox.Size = new Size(cellRect.Width - 2 - buttonWidth, cellRect.Height-2);
			
			// calc the location of the textbox
            this.TextBox.Location = cellRect.Location;
            this.TextBox.Left++;
            this.TextBox.Top++;
			this.buttonBounds = new Rectangle(this.TextBox.Left + 1, this.TextBox.Top, buttonWidth, this.TextBox.Height);

			if (((I3NumberColumn) this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column]).UpDownAlign == LeftRightAlignment.Left)
			{
				this.TextBox.Location = new Point(cellRect.Left + buttonWidth, cellRect.Top);
				this.buttonBounds.Location = new Point(cellRect.Left, cellRect.Top);
			}
		}


		/// <summary>
		/// Simulates the up button being pressed
		/// </summary>
		protected void UpButton()
		{
			if (this.UserEdit)
			{
				this.ParseEditText();
			}

			decimal num = this.currentValue;

			if (num > (new decimal(-1, -1, -1, false, 0) - this.increment))
			{
				num = new decimal(-1, -1, -1, false, 0);
			}
			else
			{
				num += this.increment;

				if (num > this.maximum)
				{
					num = this.maximum;
				}
			}

			this.Value = num;
		}


		/// <summary>
		/// Simulates the down button being pressed
		/// </summary>
		protected void DownButton()
		{
			if (this.UserEdit)
			{
				this.ParseEditText();
			}

			decimal num = this.currentValue;

			if (num < (new decimal(-1, -1, -1, true, 0) + this.increment))
			{
				num = new decimal(-1, -1, -1, true, 0);
			}
			else
			{
				num -= this.increment;

				if (num < this.minimum)
				{
					num = this.minimum;
				}
			}

			this.Value = num;
		}


		/// <summary>
		/// Updates the editors text value to the current value
		/// </summary>
		protected void UpdateEditText()
		{
			if (this.UserEdit)
			{
				this.ParseEditText();
			}

			this.ChangingText = true;

			this.Control.Text = this.currentValue.ToString(this.Format);
		}


		/// <summary>
		/// Checks the current value and updates the editors text value
		/// </summary>
		protected virtual void ValidateEditText()
		{
			this.ParseEditText();
			this.UpdateEditText();
		}


		/// <summary>
        /// 更新Value并设置userEdit=false
		/// Converts the editors current value to a number
		/// </summary>
		protected void ParseEditText()
		{
			try
			{
				this.Value = this.Constrain(decimal.Parse(this.Control.Text));
			}
			catch (Exception)
			{
				return;
			}
			finally
			{
				this.UserEdit = false;
			}
		}


		/// <summary>
		/// Ensures that the specified value is between the editors Maximun and 
		/// Minimum values
		/// </summary>
		/// <param name="value">The value to be checked</param>
		/// <returns>A value is between the editors Maximun and Minimum values</returns>
		private decimal Constrain(decimal value)
		{
			if (value < this.minimum)
			{
				value = this.minimum;
			}

			if (value > this.maximum)
			{
				value = this.maximum;
			}

			return value;
		}


		/// <summary>
		/// Starts the Timer
		/// </summary>
		protected void StartTimer()
		{
			if (this.timer == null)
			{
				this.timer = new Timer();
				this.timer.Tick += new EventHandler(this.TimerHandler);
			}

			this.interval = TimerInterval;
			this.timer.Interval = this.interval;
			this.timer.Start();
		}


		/// <summary>
		/// Stops the Timer
		/// </summary>
		protected void StopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Stop();
				this.timer.Dispose();
				this.timer = null;
			}
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the TextBox used to edit the Cells contents
		/// </summary>
		public TextBox TextBox
		{
			get
			{
				return this.Control as TextBox;
			}
		}


		/// <summary>
		/// Gets or sets the editors current value
		/// </summary>
        protected decimal Value
		{
			get
			{
				if (this.UserEdit)
				{
					this.ValidateEditText();
				}

				return this.currentValue;
			}

			set
			{
				if (value != this.currentValue)
				{
					if (value < this.minimum)
					{
						value = this.maximum;
					}

					if (value > this.maximum)
					{
						value = this.maximum;
					}

					this.currentValue = value;

					this.UpdateEditText();
				}
			}
		}

		/// <summary>
		/// Gets or sets the value to increment or decrement when the up or down 
		/// buttons are clicked
		/// </summary>
        protected decimal Increment
		{
			get
			{
				return this.increment;
			}

			set
			{
				if (value < new decimal(0))
				{
					throw new ArgumentException("increment must be greater than zero");
				}

				this.increment = value;
			}
		}


		/// <summary>
		/// Gets or sets the maximum value for the editor
		/// </summary>
        protected decimal Maximum
		{
			get
			{
				return this.maximum;
			}

			set
			{
				this.maximum = value;
				
				if (this.minimum > this.maximum)
				{
					this.minimum = this.maximum;
				}
			}
		}


		/// <summary>
		/// Gets or sets the minimum value for the editor
		/// </summary>
        protected decimal Minimum
		{
			get
			{
				return this.minimum;
			}

			set
			{
				this.minimum = value;

				if (this.minimum > this.maximum)
				{
					this.maximum = value;
				}
			}
		}

        protected NumberColumnType NumberColumnType
        {
            get
            {
                return this.numberColumnType;
            }
            set
            {
                this.numberColumnType = value;
            }
        }


		/// <summary>
		/// Gets or sets the string that specifies how the editors contents 
		/// are formatted
		/// </summary>
		protected string Format
		{
			get
			{
				return this.format;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				
				this.format = value;

				this.UpdateEditText();
			}
		}


		/// <summary>
		/// Gets or sets whether the editors text is being updated
		/// </summary>
		protected bool ChangingText
		{
			get
			{
				return this.changingText;
			}

			set
			{
				this.changingText = value;
			}
		}


		/// <summary>
		/// Gets or sets whether the arrow keys should be passed to the editor
		/// </summary>
		public bool InterceptArrowKeys
		{
			get
			{
				return this.interceptArrowKeys;
			}

			set
			{
				this.interceptArrowKeys = value;
			}
		}


		/// <summary>
		/// Gets or sets whether the user has changed the editors value
		/// </summary>
		protected bool UserEdit
		{
			get
			{
				return this.userEdit;
			}
			set
			{
				this.userEdit = value;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Handler for the editors TextBox.MouseWheel event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A MouseEventArgs that contains the event data</param>
		protected internal virtual void OnMouseWheel(object sender, MouseEventArgs e)
		{
			bool up = true;

			this.wheelDelta += e.Delta;

			if (Math.Abs(this.wheelDelta) >= 120)
			{
				if (this.wheelDelta < 0)
				{
					up = false;
				}

				if (up)
				{
					this.UpButton();
				}
				else
				{
					this.DownButton();
				}

				this.wheelDelta = 0;
			}
		}


		/// <summary>
		/// Handler for the editors TextBox.KeyDown event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A KeyEventArgs that contains the event data</param>
		protected virtual void OnTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (this.interceptArrowKeys)
			{
				if (e.KeyData == Keys.Up)
				{
					this.UpButton();

					e.Handled = true;
				}
				else if (e.KeyData == Keys.Down)
				{
					this.DownButton();

					e.Handled = true;
				}
			}

			if (e.KeyCode == Keys.Return)
			{
				this.ValidateEditText();
			}
		}


		/// <summary>
		/// Handler for the editors TextBox.KeyPress event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A KeyPressEventArgs that contains the event data</param>
		protected virtual void OnTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			char enter = I3AsciiChars.CarriageReturn;
			char escape = I3AsciiChars.Escape;
			char tab = I3AsciiChars.HorizontalTab;
			
			NumberFormatInfo info = CultureInfo.CurrentCulture.NumberFormat;
			
			string decimalSeparator = info.NumberDecimalSeparator;
			string groupSeparator = info.NumberGroupSeparator;
			string negativeSign = info.NegativeSign;
			string character = e.KeyChar.ToString();

            if ((!char.IsDigit(e.KeyChar) && !character.Equals(decimalSeparator) && !character.Equals(groupSeparator)) &&
                !character.Equals(negativeSign) && (e.KeyChar != tab))
            {
                if ((Control.ModifierKeys & (Keys.Alt | Keys.Control)) == Keys.None)
                {
                    e.Handled = true;

                    if (e.KeyChar == enter)
                    {
                        if (this.EditingTable != null)
                        {
                            this.EditingTable.StopEditing();
                        }
                    }
                    else if (e.KeyChar == escape)
                    {
                        if (this.EditingTable != null)
                        {
                            this.EditingTable.CancelEditing();
                        }
                    }
                    else
                    {
                        I3NativeMethods.MessageBeep(0 /*MB_OK*/);
                    }
                }
            }
            else
            {
                this.userEdit = true;
            }
		}


		/// <summary>
		/// Handler for the editors TextBox.LostFocus event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnTextBoxLostFocus(object sender, EventArgs e)
		{
            if (this.UserEdit)
            {
                this.ValidateEditText();
            }

			if (this.EditingTable != null)
			{
				this.EditingTable.StopEditing();
			}
		}


		/// <summary>
		/// Handler for the editors buttons MouseDown event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public void OnEditorButtonMouseDown(object sender, I3CellMouseEventArgs e)
		{
			this.ParseEditText();

			if (e.Y < this.buttonBounds.Top + (this.buttonBounds.Height / 2))
			{
				this.buttonID = UpButtonID;
				
				this.UpButton();
			}
			else
			{
				this.buttonID = DownButtonID;
				
				this.DownButton();
			}

			this.StartTimer();
		}


		/// <summary>
		/// Handler for the editors buttons MouseUp event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public void OnEditorButtonMouseUp(object sender, I3CellMouseEventArgs e)
		{
			this.StopTimer();

			this.buttonID = 0;
		}


		/// <summary>
		/// Handler for the editors Timer event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void TimerHandler(object sender, EventArgs e)
		{
			if (buttonID == 0)
			{
				this.StopTimer();

				return;
			}

			if (buttonID == UpButtonID)
			{
				this.UpButton();
			}
			else
			{
				this.DownButton();
			}
				
			this.interval *= 7;
			this.interval /= 10;
			
			if (this.interval < 1)
			{
				this.interval = 1;
			}
			
			this.timer.Interval = this.interval;
		}


		#endregion
	}
}
