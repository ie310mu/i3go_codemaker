
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Column;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as a DateTime
	/// </summary>
	public class I3DateTimeCellRenderer : I3DropDownCellRenderer
	{
		#region Class Data

		/// <summary>
		/// The format of the date and time displayed in the Cell
		/// </summary>
        //private DateTimePickerFormat dateFormat;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the DateTimeCellRenderer class with 
		/// default settings
		/// </summary>
		public I3DateTimeCellRenderer() : base() 
		{
            //this.dateFormat = DateTimePickerFormat.Long;
            //this.Format = I3DateTimeColumn.LongDateFormat;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the format of the date and time displayed in the Cell
		/// </summary>
        //public DateTimePickerFormat DateTimeFormat
        //{
        //    get
        //    {
        //        return this.dateFormat;
        //    }

        //    set
        //    {
        //        if (!Enum.IsDefined(typeof(DateTimePickerFormat), value)) 
        //        {
        //            throw new InvalidEnumArgumentException("value", (int) value, typeof(DateTimePickerFormat));
        //        }
					
        //        this.dateFormat = value;
        //    }
        //}

		#endregion


		#region Events

		#region Paint

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(I3PaintCellEventArgs e)
		{
            //if (e.Table.ColumnModel.Columns[e.Column] is I3DateTimeColumn)
            //{
            //    I3DateTimeColumn column = (I3DateTimeColumn) e.Table.ColumnModel.Columns[e.Column];

            //    //this.DateTimeFormat = column.DateTimeFormat;
            //    this.Format = column.Format;
            //}
            //else
            //{
            //    //this.DateTimeFormat = DateTimePickerFormat.Long;
            //    this.Format = "";
            //}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(I3PaintCellEventArgs e)
		{
			base.OnPaint(e);

			// don't bother going any further if the Cell is null 
			// or doesn't contain any data
			if (e.Cell == null || e.Cell.Data == null || !(e.Cell.Data is DateTime))
			{
				return;
			}

			Rectangle buttonRect = this.CalcDropDownButtonBounds();

			Rectangle textRect = this.ClientRectangle;
			
			if (this.ShowDropDownButton)
			{
				textRect.Width -= buttonRect.Width - 1;
			}

            // draw the text
            string text = e.Table.ColumnModel.Columns[e.Column].DataToString(e.Cell.Data);
			if (e.Enabled)
			{
                this.DrawText(text, e.Graphics, this.ForeBrush, textRect);
			}
			else
			{
                this.DrawText(text, e.Graphics, this.GrayTextBrush, textRect);
            }

            //cal needWidth needHeigth
            int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
            int buttonWidth = this.ShowDropDownButton ? buttonRect.Width : 0;
            CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat, maxWidth, 6 + buttonWidth, 0);
			
			if (e.Focused && e.Enabled)
			{
				Rectangle focusRect = this.ClientRectangle;

				if (this.ShowDropDownButton)
				{
					focusRect.Width -= buttonRect.Width;
				}
				
				ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
			}
		}


		/// <summary>
		/// Draws the DateTime text
		/// </summary>
		/// <param name="dateTime">The DateTime value to be drawn</param>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="brush">The Brush to draw the text with</param>
		/// <param name="textRect">A Rectangle that specifies the bounds of the text</param>
		protected void DrawText(string text, Graphics g, Brush brush, Rectangle textRect)
		{
			// get the custom format
            //string format = this.Format;
			
			// if a custom format hasn't been defined, use 
			// one of the default formats
            //if (format.Length == 0)
            //{
            //    format = I3DateTimeColumn.ShortDateFormat;
                //switch (this.DateTimeFormat)
                //{
                //    case DateTimePickerFormat.Long:	
                //        format = DateTimeColumn.LongDateFormat;
                //        break;

                //    case DateTimePickerFormat.Short:	
                //        format = DateTimeColumn.ShortDateFormat;
                //        break;

                //    case DateTimePickerFormat.Time:	
                //        format = DateTimeColumn.LongTimeFormat;
                //        break;
                //}
            //}
            
            g.DrawString(text, this.Font, brush, textRect, this.StringFormat);
		}

		#endregion

		#endregion
	}
}
