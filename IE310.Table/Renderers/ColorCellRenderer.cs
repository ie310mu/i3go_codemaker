
using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Row;
using IE310.Table.Column;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as Buttons
	/// </summary>
	public class I3ColorCellRenderer : I3DropDownCellRenderer
	{
		#region Class Data
		
		/// <summary>
		/// Specifies whether the Cells Color should be drawn
		/// </summary>
		private bool showColor;

		/// <summary>
		/// Specifies whether the Cells Color name should be drawn
		/// </summary>
		private bool showColorName;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColorCellRenderer class with 
		/// default settings
		/// </summary>
		public I3ColorCellRenderer() : base()
		{
			this.showColor = true;
			this.showColorName = true;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the Color 
		/// rectangle
		/// </summary>
		/// <param name="rowAlignment">The alignment of the Cells Row</param>
		/// <param name="columnAlignment">The alignment of the Cells Column</param>
		/// <returns>A Rectangle that specifies the size and location of the Color 
		/// rectangle</returns>
		protected Rectangle CalcColorRect(I3RowAlignment rowAlignment, I3ColumnAlignment columnAlignment)
		{
			Rectangle rect = this.ClientRectangle;

			rect.X += 2;
			rect.Y += 2;
			rect.Height -= 6;
			rect.Width = 16;

			return rect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether the Cells Color should be drawn
		/// </summary>
		public bool ShowColor
		{
			get
			{
				return this.showColor;
			}

			set
			{
				this.showColor = value;
			}
		}


		/// <summary>
		/// Gets or sets whether the Cells Color name should be drawn
		/// </summary>
		public bool ShowColorName
		{
			get
			{
				return this.showColorName;
			}

			set
			{
				this.showColorName = value;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(I3PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is I3ColorColumn)
			{
				I3ColorColumn column = (I3ColorColumn) e.Table.ColumnModel.Columns[e.Column];

				this.ShowColor = column.ShowColor;
				this.ShowColorName = column.ShowColorName;
			}
			else
			{
				this.ShowColor = false;
				this.ShowColorName = true;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(I3PaintCellEventArgs e)
		{
			base.OnPaint (e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			// get the Cells value
			Color color = Color.Empty;

			if (e.Cell.Data != null && e.Cell.Data is Color)
			{
				color = (Color) e.Cell.Data;
			}

			Rectangle buttonRect = this.CalcDropDownButtonBounds();

			Rectangle textRect = this.ClientRectangle;

			if (this.ShowDropDownButton)
			{
				textRect.Width -= buttonRect.Width - 1;
			}

			e.Graphics.SetClip(textRect);

            Rectangle colorRect = Rectangle.Empty;
			if (this.ShowColor)
			{
                colorRect = this.CalcColorRect(e.Table.TableModel.Rows[e.Row].Alignment, e.Table.ColumnModel.Columns[e.Column].CellAlignment);

				if (color != Color.Empty)
				{
					using (SolidBrush brush = new SolidBrush(color))
					{
						if (e.Enabled)
						{
							e.Graphics.FillRectangle(brush, colorRect);
							e.Graphics.DrawRectangle(SystemPens.ControlText, colorRect);
						}
						else
						{
							using (Bitmap b = new Bitmap(colorRect.Width, colorRect.Height))
							{
								using (Graphics g = Graphics.FromImage(b))
								{
									g.FillRectangle(brush, 0, 0, colorRect.Width, colorRect.Height);
									g.DrawRectangle(SystemPens.ControlText, 0, 0, colorRect.Width-1, colorRect.Height-1);
								}

								ControlPaint.DrawImageDisabled(e.Graphics, b, colorRect.X, colorRect.Y, this.BackColor);
							}
						}
					}

					textRect.X = colorRect.Right + 2;
					textRect.Width -= colorRect.Width + 4;
				}
			}

            string text = "";
			if (this.ShowColorName)
			{

				if (color.IsEmpty)
				{
					text = "Empty";
				}
				else if (color.IsNamedColor || color.IsSystemColor)
				{
					text = color.Name;
				}
				else
				{
					if (color.A != 255)
					{
						text += color.A + ", ";
					}

					text += color.R +", " + color.G + ", " + color.B;
				}

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
            CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat, maxWidth, 6 + buttonWidth + colorRect.Width, 0);
			
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
	}
}
