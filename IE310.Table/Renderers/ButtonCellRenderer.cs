using System; 
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Cell;
using IE310.Table.Column;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as Buttons
	/// </summary>
	public class I3ButtonCellRenderer : I3CellRenderer
	{
		#region Class Data

		/// <summary>
		/// Specifies the alignment of the Image displayed on the button
		/// </summary>
		private ContentAlignment imageAlignment;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ButtonCellRenderer class with 
		/// default settings
		/// </summary>
		public I3ButtonCellRenderer() : base()
		{
			this.imageAlignment = ContentAlignment.MiddleCenter;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Gets the ButtonCellRenderer specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the ButtonCellRenderer data for</param>
		/// <returns>The ButtonCellRenderer data for the specified Cell</returns>
		protected I3ButtonRendererData GetButtonRendererData(I3Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is I3ButtonRendererData))
			{
				rendererData = new I3ButtonRendererData();

				this.SetRendererData(cell, rendererData);
			}

			return (I3ButtonRendererData) rendererData;
		}


		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the button
		/// </summary>
		/// <returns>A Rectangle that specifies the size and location of the button</returns>
		protected virtual Rectangle CalcButtonBounds()
		{
			return this.ClientRectangle;
		}


		/// <summary>
		/// Returns a Rectangle that specifies the size and location of the buttons Image
		/// </summary>
		/// <param name="image">The buttons image</param>
		/// <param name="imageAlignment">The alignment of the image</param>
		/// <returns>A Rectangle that specifies the size and location of the buttons Image</returns>
		protected Rectangle CalcImageRect(Image image, ContentAlignment imageAlignment)
		{
			Rectangle imageRect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, image.Width, image.Height);

			switch (imageAlignment)
			{
				case ContentAlignment.TopCenter:
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.BottomCenter:
				{
					imageRect.X += (this.ClientRectangle.Width - image.Width) / 2;

					break;
				}

				case ContentAlignment.TopRight:
				case ContentAlignment.MiddleRight:
				case ContentAlignment.BottomRight:
				{
					imageRect.X = this.ClientRectangle.Right - image.Width;

					break;
				}
			}

			switch (imageAlignment)
			{
				case ContentAlignment.TopLeft:
				case ContentAlignment.TopCenter:
				case ContentAlignment.TopRight:
				{
					imageRect.Y += 2;

					break;
				}
				
				case ContentAlignment.MiddleLeft:
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.MiddleRight:
				{
					imageRect.Y += (this.ClientRectangle.Height - image.Height) / 2;

					break;
				}

				case ContentAlignment.BottomLeft:
				case ContentAlignment.BottomCenter:
				case ContentAlignment.BottomRight:
				{
					imageRect.Y = this.ClientRectangle.Bottom - image.Height - 2;

					break;
				}
			}

			return imageRect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the alignment of the Image displayed on the buttons
		/// </summary>
		public ContentAlignment ImageAlignment
		{
			get
			{
				return this.imageAlignment;
			}

			set
			{
				this.imageAlignment = value;
			}
		}

		#endregion


		#region Events

		#region Focus

		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		public override void OnGotFocus(I3CellFocusEventArgs e)
		{
			base.OnGotFocus(e);

			// get the table to redraw the cell
			e.Table.Invalidate(e.CellRect);
		}


		/// <summary>
		/// Raises the LostFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		public override void OnLostFocus(I3CellFocusEventArgs e)
		{
			base.OnLostFocus(e);

			// get the table to redraw the cell
			e.Table.Invalidate(e.CellRect);
		}

		#endregion

		#region Keys

		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		public override void OnKeyDown(I3CellKeyEventArgs e)
		{
			base.OnKeyDown(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// 
			if (e.KeyData == Keys.Enter || e.KeyData == Keys.Space)
			{
				rendererData.ButtonState = I3PushButtonStates.Pressed;

				e.Table.Invalidate(e.CellRect);
			}
		}


		/// <summary>
		/// Raises the KeyUp event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		public override void OnKeyUp(I3CellKeyEventArgs e)
		{
			base.OnKeyUp(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// 
			if (e.KeyData == Keys.Enter || e.KeyData == Keys.Space)
			{
				rendererData.ButtonState = I3PushButtonStates.Normal;

                e.Table.Invalidate(e.CellRect);
                I3Column column = e.Table.ColumnModel.Columns[e.Column];
                if (column.GetType() == typeof(I3ButtonColumn))
                {
                    I3ButtonColumn buttonColumn = (I3ButtonColumn)column;
                    buttonColumn.OnButtonClicked(new I3CellButtonEventArgs(e.Cell, e.Column, e.Row));
                }
			}
		}

		#endregion

		#region Mouse

		#region MouseEnter

		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseEnter(I3CellMouseEventArgs e)
		{
			base.OnMouseEnter(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// if the mouse is inside the button, make sure it is "hot"
			if (this.CalcButtonBounds().Contains(e.X, e.Y))
			{
				if (rendererData.ButtonState != I3PushButtonStates.Hot)
				{
					rendererData.ButtonState = I3PushButtonStates.Hot;

					e.Table.Invalidate(e.CellRect);
				}
			}
				// the mouse isn't inside the button, so it is in its normal state
			else
			{
				if (rendererData.ButtonState != I3PushButtonStates.Normal)
				{
					rendererData.ButtonState = I3PushButtonStates.Normal;

					e.Table.Invalidate(e.CellRect);
				}
			}
		}

		#endregion

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseLeave(I3CellMouseEventArgs e)
		{
			base.OnMouseLeave(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// make sure the button is in its normal state
			if (rendererData.ButtonState != I3PushButtonStates.Normal)
			{
				rendererData.ButtonState = I3PushButtonStates.Normal;

				e.Table.Invalidate(e.CellRect);
			}
		}

		#endregion

		#region MouseUp

		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseUp(I3CellMouseEventArgs e)
		{
			base.OnMouseUp(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// check for the left mouse button
			if (e.Button == MouseButtons.Left)
			{
				Rectangle buttonRect = this.CalcButtonBounds();
				
				// if the mouse pointer is over the button, make sure 
				// the button is "hot"
				if (buttonRect.Contains(e.X, e.Y))
				{
					rendererData.ButtonState = I3PushButtonStates.Hot;

					e.Table.Invalidate(e.CellRect);

					// check if the click started inside the button.  if 
					// it did, Raise the tables CellButtonClicked event
					if (buttonRect.Contains(rendererData.ClickPoint))
                    {
                        I3Column column = e.Table.ColumnModel.Columns[e.Column];
                        if (column.GetType() == typeof(I3ButtonColumn))
                        {
                            I3ButtonColumn buttonColumn = (I3ButtonColumn)column;
                            buttonColumn.OnButtonClicked(new I3CellButtonEventArgs(e.Cell, e.Column, e.Row));
                        }
                        //e.Table.OnCellButtonClicked(new CellButtonEventArgs(e.Cell, e.Column, e.Row));
					}
				}
				else
				{
					// the mouse was released somewhere outside of the button, 
					// so make set the button back to its normal state
					if (rendererData.ButtonState != I3PushButtonStates.Normal)
					{
						rendererData.ButtonState = I3PushButtonStates.Normal;

						e.Table.Invalidate(e.CellRect);
					}
				}
			}

			// reset the click point
			rendererData.ClickPoint = Point.Empty;
		}

		#endregion

		#region MouseDown

		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseDown(I3CellMouseEventArgs e)
		{
			base.OnMouseDown(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			// check if the left mouse button is pressed
			if (e.Button == MouseButtons.Left)
			{
				// record where the click started
				rendererData.ClickPoint = new Point(e.X, e.Y);
				
				// if the click was inside the button, set the button state to pressed
				if (this.CalcButtonBounds().Contains(rendererData.ClickPoint))
				{
					rendererData.ButtonState = I3PushButtonStates.Pressed;

					e.Table.Invalidate(e.CellRect);
				}
			}
		}

		#endregion

		#region MouseMove

		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseMove(I3CellMouseEventArgs e)
		{
			base.OnMouseMove(e);

			// get the button renderer data
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);

			Rectangle buttonRect = this.CalcButtonBounds();
			
			// check if the left mouse button is pressed
			if (e.Button == MouseButtons.Left)
			{
				// check if the mouse press originated in the button area
				if (buttonRect.Contains(rendererData.ClickPoint))
				{
					// check if the mouse is currently in the button
					if (buttonRect.Contains(e.X, e.Y))
					{
						// make sure the button is pressed
						if (rendererData.ButtonState != I3PushButtonStates.Pressed)
						{
							rendererData.ButtonState = I3PushButtonStates.Pressed;

							e.Table.Invalidate(e.CellRect);
						}
					}
					else
					{
						// the mouse isn't inside the button so make sure it is "hot"
						if (rendererData.ButtonState != I3PushButtonStates.Hot)
						{
							rendererData.ButtonState = I3PushButtonStates.Hot;

							e.Table.Invalidate(e.CellRect);
						}
					}
				}
			}
			else
			{
				// check if the mouse is currently in the button
				if (buttonRect.Contains(e.X, e.Y))
				{
					// the mouse is inside the button so make sure it is "hot"
					if (rendererData.ButtonState != I3PushButtonStates.Hot)
					{
						rendererData.ButtonState = I3PushButtonStates.Hot;

						e.Table.Invalidate(e.CellRect);
					}
				}
				else
				{
					// not inside the button so make sure it is in its normal state
					if (rendererData.ButtonState != I3PushButtonStates.Normal)
					{
						rendererData.ButtonState = I3PushButtonStates.Normal;

						e.Table.Invalidate(e.CellRect);
					}
				}
			}
		}

		#endregion

		#endregion

		#region Paint

		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		public override void OnPaintCell(I3PaintCellEventArgs e)
		{
			if (e.Table.ColumnModel.Columns[e.Column] is I3ButtonColumn)
			{
				this.ImageAlignment = ((I3ButtonColumn) e.Table.ColumnModel.Columns[e.Column]).ImageAlignment;
			}
			else
			{
				this.ImageAlignment = ContentAlignment.MiddleLeft;
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

			// don't bother going any further if the Cell is null 
			if (e.Cell == null)
			{
				return;
			}

			// get the button state
			I3ButtonRendererData rendererData = this.GetButtonRendererData(e.Cell);
			I3PushButtonStates state = rendererData.ButtonState;

			// if the cell has focus and is in its normal state, 
			// make the button look like a default button
			if (state == I3PushButtonStates.Normal && e.Focused)
			{
				state = I3PushButtonStates.Default;
			}

			// if the table is not enabled, make sure the button is disabled
			if (!e.Enabled)
			{
				state = I3PushButtonStates.Disabled;
			}

			// draw the button
            I3ThemeManager.DrawButton(e.Graphics, this.CalcButtonBounds(), state);
		}


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

			Rectangle textRect = this.CalcButtonBounds();
			textRect.Inflate(-4, -2);

			if (e.Cell.Image != null)
			{
				Rectangle imageRect = this.CalcImageRect(e.Cell.Image, this.ImageAlignment);

				if (this.GetButtonRendererData(e.Cell).ButtonState == I3PushButtonStates.Pressed && !I3ThemeManager.VisualStylesEnabled)
				{
					imageRect.X += 1;
					imageRect.Y += 1;
				}
				
				this.DrawImage(e.Graphics, e.Cell.Image, imageRect, e.Enabled);
			}

			// draw the text
            string text = e.Table.ColumnModel.Columns[e.Column].DataToString(e.Cell.Data);
            if (string.IsNullOrEmpty(text))
            {
                text = e.Table.ColumnModel.Columns[e.Column].Caption;
            }
            if (text != null && text.Length != 0)
			{
				if (e.Enabled)
				{
					if (!I3ThemeManager.VisualStylesEnabled && this.GetButtonRendererData(e.Cell).ButtonState == I3PushButtonStates.Pressed)
					{
						textRect.X += 1;
						textRect.Y += 1;
					}

					// if the cell or the row it is in is selected 
					// our forecolor will be the selection forecolor.
					// we'll ignore this and reset our forecolor to 
					// that of the cell being rendered
					if (e.Selected)
					{
						this.ForeColor = e.Cell.ForeColor;
					}

                    e.Graphics.DrawString(text, this.Font, this.ForeBrush, textRect, this.StringFormat);
				}
				else
				{
                    e.Graphics.DrawString(text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
				}
            }

            //cal needWidth needHeigth
            int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
            CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat, maxWidth, 6 + 6, 0);

			// draw focus
			if (e.Focused && e.Enabled)
			{
				Rectangle focusRect = this.CalcButtonBounds();
				
				if (I3ThemeManager.VisualStylesEnabled)
				{
					focusRect.Inflate(-3, -3);

					if (this.GetButtonRendererData(e.Cell).ButtonState != I3PushButtonStates.Pressed)
					{
						ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
					}
				}
				else
				{
					focusRect.Inflate(-4, -4);

					ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
				}
			}
		}
		
		
		/// <summary>
		/// Draws the Image displayed on the button
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="image">The Image to draw</param>
		/// <param name="imageRect">A Rectangle that specifies the location 
		/// of the Image</param>
		/// <param name="enabled">Specifies whether the Image should be drawn 
		/// in an enabled state</param>
		protected void DrawImage(Graphics g, Image image, Rectangle imageRect, bool enabled)
		{
			if (enabled)
			{
				g.DrawImageUnscaled(image, imageRect);
			}
			else
			{
				ControlPaint.DrawImageDisabled(g, image, imageRect.X, imageRect.Y, this.BackColor);
			}
		}

		#endregion

		#endregion
	}
}
