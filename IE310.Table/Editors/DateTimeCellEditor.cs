

using System;
using System.Drawing;
using System.Windows.Forms; 

using IE310.Table.Models;
using IE310.Table.Renderers;
using System.ComponentModel;
using IE310.Table.Column;


namespace IE310.Table.Editors
{
	/// <summary>
    /// ����ʱ��༭��
	/// A class for editing Cells that contain DateTimes
	/// </summary>
	public class I3DateTimeCellEditor : I3DropDownCellEditor
	{
		#region EventHandlers

		/// <summary>
        /// ����ѡ���¼�
		/// Occurs when the user makes an explicit date selection using the mouse
		/// </summary>
        public event EventHandler DateChanged;
		#endregion
		
		
		#region Class Data

        /// <summary>
        /// ����ʱ��༭��
        /// </summary>
        private DateTimePicker dateTimePicker;

		#endregion


		#region Constructor


		/// <summary>
		/// Initializes a new instance of the DateTimeCellEditor class with default settings
		/// </summary>
		public I3DateTimeCellEditor() : base()
		{
            this.dateTimePicker = new DateTimePicker();
            this.dateTimePicker.Location = new System.Drawing.Point(0, 0);

            this.DropDown.Control = this.dateTimePicker;

			base.DropDownStyle = I3DropDownStyle.DropDownList;
            this.DropDown.PanelBorderStyle = BorderStyle.None;
		}

		#endregion


		#region Methods


		/// <summary>
        /// ���ñ༭���Ĵ�С��λ��
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
        protected override void SetEditLocation(Rectangle cellRect)
        {
            // calc the size of the textbox
            II3CellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
            int buttonWidth = ((I3DateTimeCellRenderer)renderer).ButtonWidth;

            this.TextBox.Size = new Size(cellRect.Width - 1 - buttonWidth, cellRect.Height - 1);
            this.TextBox.Location = cellRect.Location;
            this.TextBox.Width = 0;

            this.dateTimePicker.Width = this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column].Width;
            this.DropDown.Height = this.EditingCell.Row.Height;
            this.DropDown.Width = this.dateTimePicker.Width - 1;
        }


		/// <summary>
        /// ����TextBox��ֵ
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected override void SetEditValue()
		{
			// set default values incase we can't find what we're looking for
			DateTime date = DateTime.Now;
			String format = I3DateTimeColumn.ShortDateFormat;
			
			if (this.EditingCell.Data != null && this.EditingCell.Data is DateTime)
			{
				date = (DateTime) this.EditingCell.Data;

				if (this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column] is I3DateTimeColumn)
				{
					I3DateTimeColumn dtCol = (I3DateTimeColumn) this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column];

                    format = dtCol.Format;
				}
			}
				
            this.dateTimePicker.Value = date;
			this.TextBox.Text = date.ToString(format);
		}


		/// <summary>
        /// ����Cell��Data
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected override void SetCellValue()
		{
            this.EditingCell.Data = this.dateTimePicker.Value;
		}


		/// <summary>
        /// ��ʼ�༭�����������ؼ����¼���
		/// Starts editing the Cell
		/// </summary>
		public override void StartEditing()
		{
            this.dateTimePicker.CloseUp += new EventHandler(dateTimePicker_CloseUp);

            I3Column column = this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column];
            if (column.GetType() == typeof(I3DateTimeColumn))
            {
                I3DateTimeColumn dateTimeColumn = column as I3DateTimeColumn;
                if (dateTimeColumn.DateTimeColumnType == I3DateTimeColumnType.Date)
                {
                    this.dateTimePicker.Format = DateTimePickerFormat.Short;
                    this.dateTimePicker.ShowUpDown = false;
                }
                else
                {
                    this.dateTimePicker.Format = DateTimePickerFormat.Time;
                    this.dateTimePicker.ShowUpDown = true;
                }
                this.dateTimePicker.CustomFormat = dateTimeColumn.Format;
            }

			this.TextBox.SelectionLength = 0;
			
			base.StartEditing();
		}


        protected override int GetDropDownContainerTop(Point p)
        {
            return p.Y;
        }
        

		/// <summary>
        /// �����༭��ȡ�������ؼ����¼���
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public override void StopEditing()
		{
            this.dateTimePicker.CloseUp -= new EventHandler(dateTimePicker_CloseUp);
			
			base.StopEditing();
		}


		/// <summary>
        /// ȡ���༭��ȡ�������ؼ����¼���
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public override void CancelEditing()
		{
            this.dateTimePicker.CloseUp -= new EventHandler(dateTimePicker_CloseUp);
			
			base.CancelEditing();
		}

		#endregion


		#region Properties

		/// <summary>
        /// ��ȡ������ʽ�����ܽ���ֵ����
		/// Gets or sets a value specifying the style of the drop down editor
		/// </summary>
		public new I3DropDownStyle DropDownStyle
		{
			get
			{
				return base.DropDownStyle;
			}

			set
			{
				throw new NotSupportedException();
			}
		}

		#endregion


		#region Events

		/// <summary>
        /// ��������ѡ���¼�
		/// Raises the DateSelected event
		/// </summary>
		/// <param name="e">A DateRangeEventArgs that contains the event data</param>
        protected virtual void OnDateSelected(EventArgs e)
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }


		/// <summary>
        /// ���ڿؼ����¼��������
		/// Handler for the editors MonthCalendar.DateSelected events
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A DateRangeEventArgs that contains the event data</param>
		private void calendar_DateSelected(object sender, DateRangeEventArgs e)
		{
			this.DroppedDown = false;

			this.OnDateSelected(e);

			this.EditingTable.StopEditing();
		}




        void dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            this.DroppedDown = false;

            this.OnDateSelected(e);

            this.EditingTable.StopEditing();
        }

		#endregion
	}
}
