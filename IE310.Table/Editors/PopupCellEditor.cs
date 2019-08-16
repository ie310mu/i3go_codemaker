

using System;
using System.Drawing;
using System.Windows.Forms; 

using IE310.Table.Models;
using IE310.Table.Renderers;
using IE310.Table.Events;
using IE310.Table.Column;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 日期时间编辑器
	/// A class for editing Cells that contain DateTimes
	/// </summary>
	public class I3PopupCellEditor : I3DropDownCellEditor
	{
		
		#region Class Data

		/// <summary>
        /// 日历控件
		/// The MonthCalendar that will be shown in the drop-down portion of the 
		/// DateTimeCellEditor
		/// </summary>
		private Control popupControl;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the DateTimeCellEditor class with default settings
		/// </summary>
        public I3PopupCellEditor()
            : base()
		{
			base.DropDownStyle = I3DropDownStyle.DropDownList;

            this.BeginEdit += new I3CellEditEventHandler(PopupCellEditor_BeginEdit);
            this.EndEdit += new I3CellEditEventHandler(PopupCellEditor_EndEdit);
		}

        void PopupCellEditor_EndEdit(object sender, I3CellEditEventArgs e)
        {
            I3Column column = e.Table.ColumnModel.Columns[e.Column];
            if (column.GetType() == typeof(I3PopupColumn))
            {
                I3PopupColumn popupColumn = (I3PopupColumn)column;
                popupColumn.OnEndPopup(e);
            }
        }

        void PopupCellEditor_BeginEdit(object sender, I3CellEditEventArgs e)
        {
            I3Column column = e.Table.ColumnModel.Columns[e.Column];
            if (column.GetType() == typeof(I3PopupColumn))
            {
                I3PopupColumn popupColumn = (I3PopupColumn)column;
                popupColumn.OnBeforePopup(e);
            }
        }

		#endregion


		#region Methods

		/// <summary>
        /// 设置编辑器的大小和位置
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected override void SetEditLocation(Rectangle cellRect)
		{
			// calc the size of the textbox
            II3CellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
            int buttonWidth = ((I3PopupCellRenderer)renderer).ButtonWidth;

            this.TextBox.Size = new Size(cellRect.Width - 1 - buttonWidth, cellRect.Height - 1);
            this.TextBox.Location = cellRect.Location;
            this.TextBox.Width = 0;
		}


		/// <summary>
        /// 设置TextBox的值
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
        protected override void SetEditValue()
        {
            //if (this.EditingCell.Data != null)
            //{
            if (this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column] is I3PopupColumn)
            {
                I3PopupColumn popupCol = (I3PopupColumn)this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column];

                this.DropDown.Control = popupCol.PopupControl;
                if (this.DropDown.Control != null)
                {
                    this.DropDown.Control.Visible = true;
                }
            }
            //}


            this.TextBox.Text = this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column].DataToString(this.EditingCell.Data);
        }


		/// <summary>
        /// 设置Cell的Data
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected override void SetCellValue()
		{
		}


		/// <summary>
        /// 开始编辑
		/// Starts editing the Cell
		/// </summary>
		public override void StartEditing()
        {
            //this.TextBox.SelectionLength = 0;
			
			base.StartEditing();
		}


		/// <summary>
        /// 结束编辑
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public override void StopEditing()
		{
			base.StopEditing();
		}


		/// <summary>
        /// 取消编辑
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public override void CancelEditing()
		{
			base.CancelEditing();
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取下拉方式，不能进行值设置
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

		#endregion
	}
}
