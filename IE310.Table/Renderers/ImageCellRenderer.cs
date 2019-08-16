
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as Images
	/// </summary>
	public class I3ImageCellRenderer : I3CellRenderer
	{
		#region Class Data

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ImageCellRenderer class with 
		/// default settings
		/// </summary>
		public I3ImageCellRenderer() : base()
		{
			this.drawText = true;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the Image contained in the current Cell
		/// </summary>
		/// <param name="image">The Image to be drawn</param>
		/// <param name="sizeMode">An ImageSizeMode that specifies how the 
		/// specified Image is scaled</param>
		/// <param name="rowAlignment">The alignment of the current Cell's row</param>
		/// <param name="columnAlignment">The alignment of the current Cell's Column</param>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the Image contained in the current Cell</returns>
		protected Rectangle CalcImageRect(Image image, I3ImageSizeMode sizeMode, I3RowAlignment rowAlignment, I3ColumnAlignment columnAlignment)
		{
			if (this.DrawText)
			{
				sizeMode = I3ImageSizeMode.ScaledToFit;
			}

			Rectangle imageRect = this.ClientRectangle;

			if (sizeMode == I3ImageSizeMode.Normal)
			{
				if (image.Width < imageRect.Width)
				{
					imageRect.Width = image.Width;
				}

				if (image.Height < imageRect.Height)
				{
					imageRect.Height = image.Height;
				}
			}
			else if (sizeMode == I3ImageSizeMode.ScaledToFit)
			{
				if (image.Width >= imageRect.Width || image.Height >= imageRect.Height)
				{
					double hScale = ((double) imageRect.Width) / ((double) image.Width);
					double vScale = ((double) imageRect.Height) / ((double) image.Height);

					double scale = Math.Min(hScale, vScale);

					imageRect.Width = (int) (((double) image.Width) * scale);
					imageRect.Height = (int) (((double) image.Height) * scale);
				}
				else
				{
					imageRect.Width = image.Width;
					imageRect.Height = image.Height;
				}
			}

			if (rowAlignment == I3RowAlignment.Center)
			{
				imageRect.Y += (this.ClientRectangle.Height - imageRect.Height) / 2;
			}
			else if (rowAlignment == I3RowAlignment.Bottom)
			{
				imageRect.Y = this.ClientRectangle.Bottom - imageRect.Height;
			}

			if (!this.DrawText)
			{
				if (columnAlignment == I3ColumnAlignment.Center)
				{
					imageRect.X += (this.ClientRectangle.Width - imageRect.Width) / 2;
				}
				else if (columnAlignment == I3ColumnAlignment.Right)
				{
					imageRect.X = this.ClientRectangle.Width - imageRect.Width;
				}
			}

			return imageRect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether any text contained in the Cell should be drawn
		/// </summary>
		public bool DrawText
		{
			get
			{
				return this.drawText;
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
			if (e.Table.ColumnModel.Columns[e.Column] is I3ImageColumn)
			{
				this.drawText = ((I3ImageColumn) e.Table.ColumnModel.Columns[e.Column]).DrawText;
			}
			else
			{
				this.drawText = true;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		protected override void OnPaint(I3PaintCellEventArgs e)
		{
			base.OnPaint(e);
			
			// don't bother if the Cell is null or doesn't have an image
			if (e.Cell == null || e.Cell.Image == null)
			{
				return;
			}

			// work out the size and location of the image
			Rectangle imageRect = this.CalcImageRect(e.Cell.Image, e.Cell.ImageSizeMode, this.LineAlignment, this.Alignment);
			
			// draw the image
			bool scaled = (this.DrawText || e.Cell.ImageSizeMode != I3ImageSizeMode.Normal);
			this.DrawImage(e.Graphics, e.Cell.Image, imageRect, scaled, e.Table.Enabled);

			// check if we need to draw any text
            string text = "";
			if (this.DrawText)
			{
                text = e.Table.ColumnModel.Columns[e.Column].DataToString(e.Cell.Data);
                if (text != null && text.Length != 0)
				{
					// rectangle the text will be drawn in
					Rectangle textRect = this.ClientRectangle;
				
					// take the imageRect into account so we don't 
					// draw over it
					textRect.X += imageRect.Width;
					textRect.Width -= imageRect.Width;

					// check that we will be able to see the text
					if (textRect.Width > 0)
					{
						// draw the text
						if (e.Enabled)
						{
                            e.Graphics.DrawString(text, this.Font, this.ForeBrush, textRect, this.StringFormat);
						}
						else
						{
                            e.Graphics.DrawString(text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
						}
					}
				}
			}


            //cal needWidth needHeigth
            if (this.DrawText || e.Cell.ImageSizeMode != I3ImageSizeMode.SizedToFit)
            {
                int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
                int addWidth = string.IsNullOrEmpty(text) ? 0 : 6;
                CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat, maxWidth, addWidth + imageRect.Width, 0);
            }
            else
            {
                e.Cell.NeedWidth = e.Cell.Image == null ? 1 : e.Cell.Image.Width + e.Cell.Padding.Left + e.Cell.Padding.Right;
                if (e.Cell.NeedWidth > 200)
                {
                    e.Cell.NeedWidth = 200;
                }
            }
            e.Cell.NeedHeight = e.Cell.Image == null ? 1 : e.Cell.Image.Height + e.Cell.Padding.Top + e.Cell.Padding.Bottom;
            if (e.Cell.NeedHeight > 100)
            {
                e.Cell.NeedHeight = 100;
            }
			
			if (e.Focused && e.Enabled)
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
			}
		}


		/// <summary>
		/// Draws the Image contained in the Cell
		/// </summary>
		/// <param name="g">The Graphics used to paint the Image</param>
		/// <param name="image">The Image to be drawn</param>
		/// <param name="imageRect">A rectangle that specifies the Size and 
		/// Location of the Image</param>
		/// <param name="scaled">Specifies whether the image is to be scaled</param>
		/// <param name="enabled">Specifies whether the Image should be drawn 
		/// in an enabled state</param>
		protected void DrawImage(Graphics g, Image image, Rectangle imageRect, bool scaled, bool enabled)
		{
			if (scaled)
			{
				if (enabled)
				{
					g.DrawImage(image, imageRect);
				}
				else
				{
					using (Image im = new Bitmap(image, imageRect.Width, imageRect.Height))
					{
						ControlPaint.DrawImageDisabled(g, im, imageRect.X, imageRect.Y, this.BackBrush.Color);
					}
				}
			}
			else
			{
				if (enabled)
				{
					g.DrawImageUnscaled(image, imageRect);
				}
				else
				{
					ControlPaint.DrawImageDisabled(g, image, imageRect.X, imageRect.Y, this.BackBrush.Color);
				}
			}
		}

		#endregion

		#endregion
	}
}
