

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
	public class I3ExtendCellEditor : I3DropDownCellEditor
    {

        #region Class Data


        #endregion


        #region Constructor

        /// <summary>
		/// Initializes a new instance of the DateTimeCellEditor class with default settings
		/// </summary>
        public I3ExtendCellEditor()
            : base()
		{
            this.BeginEdit += new I3CellEditEventHandler(ExtendCellEditor_BeginEdit);
		}

        void ExtendCellEditor_BeginEdit(object sender, I3CellEditEventArgs e)
        {
            I3Column column=e.Table.ColumnModel.Columns[e.Column];
            if (column.GetType() == typeof(I3ExtendColumn))
            {
                I3ExtendColumn extendColumn = (I3ExtendColumn)column;
                extendColumn.OnExtendEdit(e);
            }
            e.Cancel = true;
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
            int buttonWidth = ((I3ExtendCellRenderer)renderer).ButtonWidth;

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


		#endregion


		#region Events

		#endregion
	}
}
