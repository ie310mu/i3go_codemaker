
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as a ProgressBar
	/// </summary>
	public class I3ProgressBarCellRenderer : I3CellRenderer
	{
		#region Class Data

		/// <summary>
		/// Specifies whether the ProgressBar's value as a string 
		/// should be displayed
		/// </summary>
		private bool drawPercentageText;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ProgressBarCellRenderer class with 
		/// default settings
		/// </summary>
		public I3ProgressBarCellRenderer() : base()
		{
			this.drawPercentageText = true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the rectangle that represents the client area of the Renderer
		/// </summary>
		public new Rectangle ClientRectangle
		{
			get
			{
				Rectangle client = base.ClientRectangle;

				client.Inflate(-1, -1);

				return client;
			}
		}

		/// <summary>
		/// Gets or sets whether the ProgressBar's value as a string 
		/// should be displayed
		/// </summary>
		public bool DrawPercentageText
		{
			get
			{
				return this.drawPercentageText;
			}

			set
			{
				this.drawPercentageText = value;
			}
		}

		#endregion


		#region Events

		#region Paint

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(I3PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is I3ProgressBarColumn)
			{
				this.drawPercentageText = ((I3ProgressBarColumn) e.Table.ColumnModel.Columns[e.Column]).DrawPercentageText;
			}
			else
			{
				this.drawPercentageText = false;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaintBackground(I3PaintCellEventArgs e)
		{
			base.OnPaintBackground(e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			// fill the client area with the window color (this 
			// will be the background color of the progress bar)
			e.Graphics.FillRectangle(SystemBrushes.Window, this.ClientRectangle);

			Rectangle progressRect = this.ClientRectangle;

			// draw the border
			if (e.Enabled)
			{
				// if xp themes are enabled, shrink the size of the 
				// progress bar as otherwise the focus rect appears 
				// to go awol if the cell has focus
				if (I3ThemeManager.VisualStylesEnabled)
				{
					progressRect.Inflate(-1, -1);
				}

				I3ThemeManager.DrawProgressBar(e.Graphics, progressRect);
			}
			else
			{
				using (Bitmap b = new Bitmap(progressRect.Width, progressRect.Height))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						I3ThemeManager.DrawProgressBar(g, new Rectangle(0, 0, progressRect.Width, progressRect.Height));
					}

					ControlPaint.DrawImageDisabled(e.Graphics, b, progressRect.X, progressRect.Y, this.BackBrush.Color);
				}
			}
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(I3PaintCellEventArgs e)
		{
			base.OnPaint(e);

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}
			
			// get the Cells value
			int intVal = 0;

			if (e.Cell.Data != null && e.Cell.Data is int)
			{
				intVal = (int) e.Cell.Data;
			}

            if (intVal < 0)
            {
                intVal = 0;
            }
            else if (intVal > 100)
            {
                intVal = 100;
            }

			// adjust the chunk rect so we don't draw over the
			// progress bars borders
			Rectangle chunkRect = this.ClientRectangle;
			chunkRect.Inflate(-2, -2);

			// if xp themes are enabled, shrink the size of the 
			// progress bar as otherwise the focus rect appears 
			// to go awol if the cell has focus
			if (I3ThemeManager.VisualStylesEnabled)
			{
				chunkRect.Inflate(-1, -1);
			}

			chunkRect.Width = (int) ((((double) intVal) / 100d) * ((double) chunkRect.Width));

			if (e.Enabled)
			{
				I3ThemeManager.DrawProgressBarChunks(e.Graphics, chunkRect);
			}
			else
			{
				using (Bitmap b = new Bitmap(chunkRect.Width, chunkRect.Height))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						I3ThemeManager.DrawProgressBarChunks(g, new Rectangle(0, 0, chunkRect.Width, chunkRect.Height));
					}

					ControlPaint.DrawImageDisabled(e.Graphics, b, chunkRect.X, chunkRect.Y, this.BackBrush.Color);
				}
			}

			if (this.DrawPercentageText)
			{
				this.Alignment = I3ColumnAlignment.Center;
				this.LineAlignment = I3RowAlignment.Center;

				Font font = new Font(this.Font.FontFamily, this.Font.SizeInPoints, FontStyle.Bold);

				if (e.Enabled)
				{
					e.Graphics.DrawString("" + intVal + "%", font, SystemBrushes.ControlText, this.ClientRectangle, this.StringFormat);
				}
				else
				{
					e.Graphics.DrawString("" + intVal + "%", font, Brushes.White, this.ClientRectangle, this.StringFormat);
				}
				
				if (!I3ThemeManager.VisualStylesEnabled)
				{
					// remember the old clip area
					Region oldClip = e.Graphics.Clip;
					
					Rectangle clipRect = this.ClientRectangle;
					clipRect.Width = chunkRect.Width + 2;
					e.Graphics.SetClip(clipRect);

					if (e.Table.Enabled)
					{
						e.Graphics.DrawString("" + intVal + "%", font, SystemBrushes.HighlightText, this.ClientRectangle, this.StringFormat);
					}
					else
					{
						e.Graphics.DrawString("" + intVal + "%", font, Brushes.White, this.ClientRectangle, this.StringFormat);
					}

					// restore the old clip area
					e.Graphics.SetClip(oldClip, CombineMode.Replace);
                }

                //cal needWidth needHeigth
                int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
                CalCellNeedSize(e.Graphics, e.Cell, "" + intVal + "%%", this.Font, this.StringFormat,maxWidth, 0, 0);
            }
			
			if (e.Focused && e.Enabled)
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
			}
		}

		#endregion

		#endregion
	}
}
