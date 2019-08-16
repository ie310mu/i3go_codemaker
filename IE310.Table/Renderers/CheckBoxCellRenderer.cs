
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
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A CellRenderer that draws Cell contents as CheckBoxes
	/// </summary>
	public class I3CheckBoxCellRenderer : I3CellRenderer
	{
		#region Class Data
		
		/// <summary>
		/// The size of the checkbox
		/// </summary>
		private Size checkSize;

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CheckBoxCellRenderer class with 
		/// default settings
		/// </summary>
		public I3CheckBoxCellRenderer() : base()
		{
			this.checkSize = new Size(13, 13);
			this.drawText = true;
		}

		#endregion


		#region Methods

        /// <summary>
        /// 获取CheckStyle=Image时使用的Image
        /// </summary>
        public Image CheckBoxImage(I3CheckBoxColumn checkBoxColumn)
        {
            if (checkBoxColumn.CustomCheckImage != null)
            {
                return checkBoxColumn.CustomCheckImage;
            }
            else
            {
                return global::IE310.Table.Resources.I3Resource.CheckBoxColumnImage;
            }
        }

		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the check box contained in the current Cell
		/// </summary>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the check box contained in the current Cell</returns>
        protected Rectangle CalcCheckBoxDisplayRect(I3RowAlignment rowAlignment, I3ColumnAlignment columnAlignment, I3CheckBoxColumn checkBoxColumn)
		{
            if (checkBoxColumn.CustomCheckImage != null && checkBoxColumn.CustomCheckImageFillClient)
            {
                return this.ClientRectangle;
            }

			Rectangle checkRect = new Rectangle(this.ClientRectangle.Location, this.CheckSize);
			
			if (checkRect.Height > this.ClientRectangle.Height)
			{
				checkRect.Height = this.ClientRectangle.Height;
				checkRect.Width = checkRect.Height;
			}

			switch (rowAlignment)
			{
				case I3RowAlignment.Center:
				{
					checkRect.Y += (this.ClientRectangle.Height - checkRect.Height) / 2;

					break;
				}

				case I3RowAlignment.Bottom:
				{
					checkRect.Y = this.ClientRectangle.Bottom - checkRect.Height;

					break;
				}
			}

			if (!this.DrawText)
			{
				if (columnAlignment == I3ColumnAlignment.Center)
				{
					checkRect.X += (this.ClientRectangle.Width - checkRect.Width) / 2;
				}
				else if (columnAlignment == I3ColumnAlignment.Right)
				{
					checkRect.X = this.ClientRectangle.Right - checkRect.Width;
				}
			}

            if (checkBoxColumn != null && checkBoxColumn.CheckBoxColumnStyle == I3CheckBoxColumnStyle.Image)
            {
                Size size = checkBoxColumn.CustomCheckImageSize;
                checkRect.X -= Convert.ToInt32((size.Width - checkRect.Width) / 2);
                checkRect.Width = size.Width;
                checkRect.Y -= Convert.ToInt32((size.Height - checkRect.Height) / 2);
                checkRect.Height = size.Height;
            }

			return checkRect;
		}

        /// <summary>
        /// 计算点击哪个区域时进行编辑
        /// </summary>
        /// <param name="checkBoxColumn"></param>
        /// <returns></returns>
        protected Rectangle CalcCheckBoxCheckRect(I3Row row, I3CheckBoxColumn checkBoxColumn)
        {
            if (checkBoxColumn.CheckBoxCheckStyle == I3CheckBoxCheckStyle.CheckInClientArea)
            {
                return this.ClientRectangle;
            }
            else
            {
                return this.CalcCheckBoxDisplayRect(row.Alignment, checkBoxColumn.CellAlignment, checkBoxColumn);
            }
        }


		/// <summary>
		/// Gets the CheckBoxCellRenderer specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the CheckBoxCellRenderer data for</param>
		/// <returns>The CheckBoxCellRenderer data for the specified Cell</returns>
		protected I3CheckBoxRendererData GetCheckBoxRendererData(I3Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is I3CheckBoxRendererData))
			{
				if (cell.CheckState == CheckState.Unchecked)
				{
					rendererData = new I3CheckBoxRendererData(I3CheckBoxStates.UncheckedNormal);
				}
				else if (cell.CheckState == CheckState.Indeterminate && cell.ThreeState)
				{
					rendererData = new I3CheckBoxRendererData(I3CheckBoxStates.MixedNormal);
				}
				else 
				{
					rendererData = new I3CheckBoxRendererData(I3CheckBoxStates.CheckedNormal);
				}

				this.SetRendererData(cell, rendererData);
			}

			this.ValidateCheckState(cell, (I3CheckBoxRendererData) rendererData);

			return (I3CheckBoxRendererData) rendererData;
		}


		/// <summary>
		/// Corrects any differences between the check state of the specified Cell 
		/// and the check state in its rendererData
		/// </summary>
		/// <param name="cell">The Cell to chech</param>
		/// <param name="rendererData">The CheckBoxRendererData to check</param>
		private void ValidateCheckState(I3Cell cell, I3CheckBoxRendererData rendererData)
		{
			switch (cell.CheckState)
			{
				case CheckState.Checked:
				{		
					if (rendererData.CheckState <= I3CheckBoxStates.UncheckedDisabled)
					{
						rendererData.CheckState |= (I3CheckBoxStates) 4;
					}
					else if (rendererData.CheckState >= I3CheckBoxStates.MixedNormal)
					{
						rendererData.CheckState -= (I3CheckBoxStates) 4;
					}
					
					break;
				}

				case CheckState.Indeterminate:
				{		
					if (rendererData.CheckState <= I3CheckBoxStates.UncheckedDisabled)
					{
						rendererData.CheckState |= (I3CheckBoxStates) 8;
					}
					else if (rendererData.CheckState <= I3CheckBoxStates.CheckedDisabled)
					{
						rendererData.CheckState |= (I3CheckBoxStates) 4;
					}
					
					break;
				}

				default:
				{
					if (rendererData.CheckState >= I3CheckBoxStates.MixedNormal)
					{
						rendererData.CheckState -= (I3CheckBoxStates) 8;
					}
					else if (rendererData.CheckState >= I3CheckBoxStates.CheckedNormal)
					{
						rendererData.CheckState -= (I3CheckBoxStates) 4;
					}
					
					break;
				}
			}
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the size of the checkbox
		/// </summary>
		protected Size CheckSize
		{
			get
			{
				return this.checkSize;
			}
		}

		
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

		#region Keys

		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		public override void OnKeyDown(I3CellKeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.KeyData == Keys.Space && e.Table.IsCellEditable(e.CellPos))
			{
				// get the renderer data
				I3CheckBoxRendererData rendererData = this.GetCheckBoxRendererData(e.Cell);

				//
				if (e.Cell.CheckState == CheckState.Checked)
				{
					rendererData.CheckState = I3CheckBoxStates.CheckedPressed;
				}
				else if (e.Cell.CheckState == CheckState.Indeterminate)
				{
					rendererData.CheckState = I3CheckBoxStates.MixedPressed;
				}
				else //if (e.Cell.CheckState == CheckState.Unchecked)
				{
					rendererData.CheckState = I3CheckBoxStates.UncheckedPressed;
				}

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

			if (e.KeyData == Keys.Space && e.Table.IsCellEditable(e.CellPos))
			{
				// get the renderer data
				I3CheckBoxRendererData rendererData = this.GetCheckBoxRendererData(e.Cell);

				//
				if (e.Cell.CheckState == CheckState.Checked)
				{
					if (!e.Cell.ThreeState || !(e.Table.ColumnModel.Columns[e.Column] is I3CheckBoxColumn) || 
						((I3CheckBoxColumn) e.Table.ColumnModel.Columns[e.Column]).CheckBoxColumnStyle == I3CheckBoxColumnStyle.RadioButton)
					{
						rendererData.CheckState = I3CheckBoxStates.UncheckedNormal;
						e.Cell.CheckState = CheckState.Unchecked;
					}
					else
					{
						rendererData.CheckState = I3CheckBoxStates.MixedNormal;
						e.Cell.CheckState = CheckState.Indeterminate;
					}
				}
				else if (e.Cell.CheckState == CheckState.Indeterminate)
				{
					rendererData.CheckState = I3CheckBoxStates.UncheckedNormal;
					e.Cell.CheckState = CheckState.Unchecked;
				}
				else //if (e.Cell.CheckState == CheckState.Unchecked)
				{
					rendererData.CheckState = I3CheckBoxStates.CheckedNormal;
					e.Cell.CheckState = CheckState.Checked;
				}

				e.Table.Invalidate(e.CellRect);
			}
		}

		#endregion

		#region Mouse

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseLeave(I3CellMouseEventArgs e)
		{
			base.OnMouseLeave(e);

			if (e.Table.IsCellEditable(e.CellPos))
			{
				// get the renderer data
				I3CheckBoxRendererData rendererData = this.GetCheckBoxRendererData(e.Cell);

				if (e.Cell.CheckState == CheckState.Checked)
				{
					if (rendererData.CheckState != I3CheckBoxStates.CheckedNormal)
					{
						rendererData.CheckState = I3CheckBoxStates.CheckedNormal;

						e.Table.Invalidate(e.CellRect);
					}
				}
				else if (e.Cell.CheckState == CheckState.Indeterminate)
				{
					if (rendererData.CheckState != I3CheckBoxStates.MixedNormal)
					{
						rendererData.CheckState = I3CheckBoxStates.MixedNormal;

						e.Table.Invalidate(e.CellRect);
					}
				}
				else //if (e.Cell.CheckState == CheckState.Unchecked)
				{
					if (rendererData.CheckState != I3CheckBoxStates.UncheckedNormal)
					{
						rendererData.CheckState = I3CheckBoxStates.UncheckedNormal;

						e.Table.Invalidate(e.CellRect);
					}
				}
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

			if (e.Table.IsCellEditable(e.CellPos))
			{
				// get the renderer data
				I3CheckBoxRendererData rendererData = this.GetCheckBoxRendererData(e.Cell);

                Rectangle rect = this.CalcCheckBoxCheckRect(e.Table.TableModel.Rows[e.Row], e.Table.ColumnModel.Columns[e.Column] as I3CheckBoxColumn);
				if (rect.Contains(e.X, e.Y))
				{
					if (e.Button == MouseButtons.Left && e.Table.LastMouseDownCell.Row == e.Row && e.Table.LastMouseDownCell.Column == e.Column)
					{
						//
						if (e.Cell.CheckState == CheckState.Checked)
						{
							if (!e.Cell.ThreeState || !(e.Table.ColumnModel.Columns[e.Column] is I3CheckBoxColumn) || 
								((I3CheckBoxColumn) e.Table.ColumnModel.Columns[e.Column]).CheckBoxColumnStyle == I3CheckBoxColumnStyle.RadioButton)
							{
								rendererData.CheckState = I3CheckBoxStates.UncheckedHot;
								e.Cell.CheckState = CheckState.Unchecked;
							}
							else
							{
								rendererData.CheckState = I3CheckBoxStates.MixedHot;
								e.Cell.CheckState = CheckState.Indeterminate;
							}
						}
						else if (e.Cell.CheckState == CheckState.Indeterminate)
						{
							rendererData.CheckState = I3CheckBoxStates.UncheckedHot;
							e.Cell.CheckState = CheckState.Unchecked;
						}
						else //if (e.Cell.CheckState == CheckState.Unchecked)
						{
							rendererData.CheckState = I3CheckBoxStates.CheckedHot;
							e.Cell.CheckState = CheckState.Checked;
						}

						e.Table.Invalidate(e.CellRect);
					}
				}
			}
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

			if (e.Table.IsCellEditable(e.CellPos))
			{
				// get the renderer data
				I3CheckBoxRendererData rendererData = this.GetCheckBoxRendererData(e.Cell);

                if (this.CalcCheckBoxDisplayRect(e.Table.TableModel.Rows[e.Row].Alignment, e.Table.ColumnModel.Columns[e.Column].CellAlignment, e.Table.ColumnModel.Columns[e.Column] as I3CheckBoxColumn).Contains(e.X, e.Y))
				{
					//
					if (e.Cell.CheckState == CheckState.Checked)
					{
						rendererData.CheckState = I3CheckBoxStates.CheckedPressed;
					}
					else if (e.Cell.CheckState == CheckState.Indeterminate)
					{
						rendererData.CheckState = I3CheckBoxStates.MixedPressed;
					}
					else //if (e.Cell.CheckState == CheckState.Unchecked)
					{
						rendererData.CheckState = I3CheckBoxStates.UncheckedPressed;
					}

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
		public override void OnMouseMove(IE310.Table.Events.I3CellMouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Table.IsCellEditable(e.CellPos))
			{
				// get the renderer data
				I3CheckBoxRendererData rendererData = this.GetCheckBoxRendererData(e.Cell);

                if (this.CalcCheckBoxDisplayRect(e.Table.TableModel.Rows[e.Row].Alignment, e.Table.ColumnModel.Columns[e.Column].CellAlignment, e.Table.ColumnModel.Columns[e.Column] as I3CheckBoxColumn).Contains(e.X, e.Y))
				{
					if (e.Cell.CheckState == CheckState.Checked)
					{
						if (rendererData.CheckState == I3CheckBoxStates.CheckedNormal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								rendererData.CheckState = I3CheckBoxStates.CheckedPressed;
							}
							else
							{
								rendererData.CheckState = I3CheckBoxStates.CheckedHot;
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
					else if (e.Cell.CheckState == CheckState.Indeterminate)
					{
						if (rendererData.CheckState == I3CheckBoxStates.MixedNormal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								rendererData.CheckState = I3CheckBoxStates.MixedPressed;
							}
							else
							{
								rendererData.CheckState = I3CheckBoxStates.MixedHot;
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
					else //if (e.Cell.CheckState == CheckState.Unchecked)
					{
						if (rendererData.CheckState == I3CheckBoxStates.UncheckedNormal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								rendererData.CheckState = I3CheckBoxStates.UncheckedPressed;
							}
							else
							{
								rendererData.CheckState = I3CheckBoxStates.UncheckedHot;
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
				}
				else
				{
					if (e.Cell.CheckState == CheckState.Checked)
					{
						rendererData.CheckState = I3CheckBoxStates.CheckedNormal;
					}
					else if (e.Cell.CheckState == CheckState.Indeterminate)
					{
						rendererData.CheckState = I3CheckBoxStates.MixedNormal;
					}
					else //if (e.Cell.CheckState == CheckState.Unchecked)
					{
						rendererData.CheckState = I3CheckBoxStates.UncheckedNormal;
					}

					e.Table.Invalidate(e.CellRect);
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
			if (e.Table.ColumnModel.Columns[e.Column] is I3CheckBoxColumn)
			{
				I3CheckBoxColumn column = (I3CheckBoxColumn) e.Table.ColumnModel.Columns[e.Column];

				this.checkSize = column.CheckSize;
				this.drawText = column.DrawText;
			}
			else
			{
				this.checkSize = new Size(13, 13);
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

			// don't bother if the Cell is null
			if (e.Cell == null)
			{
				return;
			}

			Rectangle checkRect = this.CalcCheckBoxDisplayRect(this.LineAlignment, this.Alignment, e.Table.ColumnModel.Columns[e.Column] as I3CheckBoxColumn);

			I3CheckBoxStates state = this.GetCheckBoxRendererData(e.Cell).CheckState;

			if (!e.Enabled)
			{
				if (e.Cell.CheckState == CheckState.Checked)
				{
					state = I3CheckBoxStates.CheckedDisabled;
				}
				else if (e.Cell.CheckState == CheckState.Indeterminate)
				{
					state = I3CheckBoxStates.MixedDisabled;
				}
				else // if (e.Cell.CheckState == CheckState.Unchecked)
				{
					state = I3CheckBoxStates.UncheckedDisabled;
				}
			}

            if (e.Table.ColumnModel.Columns[e.Column] is I3CheckBoxColumn)
            {
                I3CheckBoxColumn checkBoxColumn = e.Table.ColumnModel.Columns[e.Column] as I3CheckBoxColumn;
                switch (checkBoxColumn.CheckBoxColumnStyle)
                {
                    case I3CheckBoxColumnStyle.RadioButton:
                        // remove any mixed states
                        switch (state)
                        {
                            case I3CheckBoxStates.MixedNormal:
                                state = I3CheckBoxStates.CheckedNormal;
                                break;

                            case I3CheckBoxStates.MixedHot:
                                state = I3CheckBoxStates.CheckedHot;
                                break;

                            case I3CheckBoxStates.MixedPressed:
                                state = I3CheckBoxStates.CheckedPressed;
                                break;

                            case I3CheckBoxStates.MixedDisabled:
                                state = I3CheckBoxStates.CheckedDisabled;
                                break;
                        }
                        I3ThemeManager.DrawRadioButton(e.Graphics, checkRect, (I3RadioButtonStates)state);
                        break;
                    case I3CheckBoxColumnStyle.Image:
                        if (e.Cell.Checked)
                        {
                            Image image = this.CheckBoxImage(checkBoxColumn);
                            this.DrawImage(e.Graphics, image, checkRect, e.Cell.Enabled);
                        }
                        break;
                    default:
                        I3ThemeManager.DrawCheck(e.Graphics, checkRect, state);
                        break;
                }
            }

            string text = "";
			if (this.DrawText)
			{
                //string text = e.Cell.Text;
                text = e.Table.ColumnModel.Columns[e.Column].DataToString(e.Cell.Data);

				if (text != null && text.Length != 0)
				{
					Rectangle textRect = this.ClientRectangle;
					textRect.X += checkRect.Width + 1;
					textRect.Width -= checkRect.Width + 1;

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

            //cal needWidth needHeigth
            int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
            int buttonWidth = checkRect.Width;
            CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat, maxWidth, 6 + buttonWidth, 0);
			
			if (e.Focused && e.Enabled)
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
			}
		}

		#endregion

		#endregion
	}
}
