
using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Cell;
using IE310.Table.Column;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Base class for CellRenderers that Cell contents like ComboBoxes
	/// </summary>
	public abstract class I3DropDownCellRenderer : I3CellRenderer
	{
		#region Class Data

		/// <summary>
		/// The width of the DropDownCellRenderer's dropdown button
		/// </summary>
		private int buttonWidth;

		/// <summary>
		/// Specifies whether the DropDownCellRenderer dropdown button should be drawn
		/// </summary>
		private bool showButton;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the DropDownCellRenderer class with 
		/// default settings
		/// </summary>
		protected I3DropDownCellRenderer() : base()
		{
			this.buttonWidth = 18;
			this.showButton = true;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Gets the Rectangle that specifies the Size and Location of 
		/// the current Cell's dropdown button
		/// </summary>
		/// <returns>A Rectangle that specifies the Size and Location of 
		/// the current Cell's dropdown button</returns>
		protected internal Rectangle CalcDropDownButtonBounds()
		{
			Rectangle buttonRect = this.ClientRectangle;

			buttonRect.Width = this.ButtonWidth;
			buttonRect.X = this.ClientRectangle.Right - buttonRect.Width;

			if (buttonRect.Width > this.ClientRectangle.Width)
			{
				buttonRect = this.ClientRectangle;
			}

			return buttonRect;
		}


		/// <summary>
		/// Gets the DropDownRendererData specific data used by the Renderer from 
		/// the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to get the DropDownRendererData data for</param>
		/// <returns>The DropDownRendererData data for the specified Cell</returns>
		protected I3DropDownRendererData GetDropDownRendererData(I3Cell cell)
		{
			object rendererData = this.GetRendererData(cell);

			if (rendererData == null || !(rendererData is I3DropDownRendererData))
			{
				rendererData = new I3DropDownRendererData();

				this.SetRendererData(cell, rendererData);
			}

			return (I3DropDownRendererData) rendererData;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the width of the dropdown button
		/// </summary>
		protected internal int ButtonWidth
		{
			get
			{
				return this.buttonWidth;
			}

			set
			{
				this.buttonWidth = value;
			}
		}


		/// <summary>
		/// Gets or sets whether the DropDownCellRenderer dropdown button should be drawn
		/// </summary>
		protected bool ShowDropDownButton
		{
			get
			{
				return this.showButton;
			}

			set
			{
				this.showButton = value;
			}
		}

		#endregion


		#region Events

		#region Mouse

		#region MouseLeave

		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		public override void OnMouseLeave(I3CellMouseEventArgs e)
		{
			base.OnMouseLeave(e);

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					I3DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (rendererData.ButtonState != I3ComboBoxStates.Normal)
					{
						rendererData.ButtonState = I3ComboBoxStates.Normal;

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

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the renderer data
					I3DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (this.CalcDropDownButtonBounds().Contains(e.X, e.Y))
					{
						rendererData.ButtonState = I3ComboBoxStates.Hot;

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

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					I3DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (this.CalcDropDownButtonBounds().Contains(e.X, e.Y))
					{
                        if (!(e.Table.ColumnModel.GetCellEditor(e.CellPos.Column) is I3DropDownCellEditor))
                        {
                            throw new InvalidOperationException("Cannot edit Cell as DropDownCellRenderer requires a DropDownColumn that uses a DropDownCellEditor");
                        }

                        rendererData.ButtonState = I3ComboBoxStates.Pressed;

                        if (!e.Table.IsEditing)
                        {
                            if (!e.Table.EditCell(e.CellPos))
                            {
                                e.Table.Invalidate(e.CellRect);
                                return;
                            }
                        }

                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);

                        e.Table.Invalidate(e.CellRect);
					}
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

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				if (e.Table.IsCellEditable(e.CellPos))
				{
					// get the button renderer data
					I3DropDownRendererData rendererData = this.GetDropDownRendererData(e.Cell);

					if (this.CalcDropDownButtonBounds().Contains(e.X, e.Y))
					{
						if (rendererData.ButtonState == I3ComboBoxStates.Normal)
						{
							if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
							{
								rendererData.ButtonState = I3ComboBoxStates.Pressed;
							}
							else
							{
								rendererData.ButtonState = I3ComboBoxStates.Hot;
							}

							e.Table.Invalidate(e.CellRect);
						}
					}
					else
					{
						if (rendererData.ButtonState != I3ComboBoxStates.Normal)
						{
							rendererData.ButtonState = I3ComboBoxStates.Normal;

							e.Table.Invalidate(e.CellRect);
						}
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
			if (e.Table.ColumnModel.Columns[e.Column] is I3DropDownColumn)
			{
				this.showButton = ((I3DropDownColumn) e.Table.ColumnModel.Columns[e.Column]).ShowDropDownButton;
			}
			else
			{
				this.showButton = true;
			}
			
			base.OnPaintCell(e);
		}


		/// <summary>
		/// Paints the Cells background
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

			if (this.ShowDropDownButton || (e.Table.IsEditing && e.CellPos == e.Table.EditingCell))
			{
				I3ComboBoxStates state = this.GetDropDownRendererData(e.Cell).ButtonState;

				if (!e.Enabled)
				{
					state = I3ComboBoxStates.Disabled;
				}

				I3ThemeManager.DrawComboBoxButton(e.Graphics, this.CalcDropDownButtonBounds(), state);
			}
		}
		#endregion

		#endregion
	}
}
