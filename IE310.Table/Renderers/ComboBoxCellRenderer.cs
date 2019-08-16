
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization; 
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as a ComboBox
	/// </summary>
	public class I3ComboBoxCellRenderer : I3DropDownCellRenderer
	{
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ComboBoxCellRenderer class with 
		/// default settings
		/// </summary>
		public I3ComboBoxCellRenderer() : base()
		{
			
		}

		#endregion


		#region Events

		#region Paint

		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(I3PaintCellEventArgs e)
		{
			base.OnPaint(e);

			// don't bother going any further if the Cell is null 
			if (e.Cell == null)
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
            if (text != null && text.Length != 0)
			{
				if (e.Enabled)
				{
                    e.Graphics.DrawString(text, this.Font, this.ForeBrush, textRect, this.StringFormat);
				}
				else
				{
                    e.Graphics.DrawString(text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
				}
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

		#endregion

		#endregion
	}
}
