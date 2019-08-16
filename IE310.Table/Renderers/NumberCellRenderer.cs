
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Column;
using IE310.Table.Cell;


namespace IE310.Table.Renderers
{
    /// <summary>
    /// A base class for drawing Cells contents as numbers
    /// </summary>
    public class I3NumberCellRenderer : I3CellRenderer
    {
        #region Class Data

        /// <summary>
        /// The width of the ComboBox's dropdown button
        /// </summary>
        private int buttonWidth;

        /// <summary>
        /// Specifies whether the up and down buttons should be drawn
        /// </summary>
        private bool showUpDownButtons;

        /// <summary>
        /// The alignment of the up and down buttons in the Cell
        /// </summary>
        private LeftRightAlignment upDownAlignment;

        /// <summary>
        /// The maximum value for the Cell
        /// </summary>
        //private decimal maximum;

        /// <summary>
        /// The minimum value for the Cell
        /// </summary>
        //private decimal minimum;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the NumberCellRenderer class with 
        /// default settings
        /// </summary>
        public I3NumberCellRenderer()
            : base()
        {
            this.StringFormat.Trimming = StringTrimming.None;
            //this.Format = "G";
            this.buttonWidth = 15;
            this.showUpDownButtons = false;
            this.upDownAlignment = LeftRightAlignment.Right;
            //this.maximum = (decimal)100;
            //this.minimum = (decimal)0;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Returns a Rectangle that specifies the size and location of the 
        /// up and down buttons
        /// </summary>
        /// <returns>A Rectangle that specifies the size and location of the 
        /// up and down buttons</returns>
        protected Rectangle CalcButtonBounds()
        {
            Rectangle buttonRect = this.ClientRectangle;

            buttonRect.Width = this.ButtonWidth;

            if (this.UpDownAlign == LeftRightAlignment.Right)
            {
                buttonRect.X = this.ClientRectangle.Right - buttonRect.Width;
            }

            if (buttonRect.Width > this.ClientRectangle.Width)
            {
                buttonRect = this.ClientRectangle;
            }

            return buttonRect;
        }


        /// <summary>
        /// Returns a Rectangle that specifies the size and location of the up button
        /// </summary>
        /// <returns>A Rectangle that specifies the size and location of the up button</returns>
        protected Rectangle GetUpButtonBounds()
        {
            Rectangle buttonRect = this.CalcButtonBounds();

            buttonRect.Height /= 2;

            return buttonRect;
        }


        /// <summary>
        /// Returns a Rectangle that specifies the size and location of the down button
        /// </summary>
        /// <returns>A Rectangle that specifies the size and location of the down button</returns>
        protected Rectangle GetDownButtonBounds()
        {
            Rectangle buttonRect = this.CalcButtonBounds();

            int height = buttonRect.Height / 2;

            buttonRect.Height -= height;
            buttonRect.Y += height;

            return buttonRect;
        }


        /// <summary>
        /// Gets the NumberRendererData specific data used by the Renderer from 
        /// the specified Cell
        /// </summary>
        /// <param name="cell">The Cell to get the NumberRendererData data for</param>
        /// <returns>The NumberRendererData data for the specified Cell</returns>
        protected I3NumberRendererData GetNumberRendererData(I3Cell cell)
        {
            object rendererData = this.GetRendererData(cell);

            if (rendererData == null || !(rendererData is I3NumberRendererData))
            {
                rendererData = new I3NumberRendererData();

                this.SetRendererData(cell, rendererData);
            }

            return (I3NumberRendererData)rendererData;
        }


        /// <summary>
        /// Gets whether the specified Table is using a NumericCellEditor to edit the 
        /// Cell at the specified CellPos
        /// </summary>
        /// <param name="table">The Table to check</param>
        /// <param name="cellPos">A CellPos that represents the Cell to check</param>
        /// <returns>true if the specified Table is using a NumericCellEditor to edit the 
        /// Cell at the specified CellPos, false otherwise</returns>
        internal bool TableUsingNumericCellEditor(I3Table table, I3CellPos cellPos)
        {
            return (table.IsEditing && cellPos == table.EditingCell && table.EditingCellEditor is I3NumberCellEditor);
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
        /// Gets or sets whether the up and down buttons should be drawn
        /// </summary>
        protected bool ShowUpDownButtons
        {
            get
            {
                return this.showUpDownButtons;
            }

            set
            {
                this.showUpDownButtons = value;
            }
        }


        /// <summary>
        /// Gets or sets the alignment of the up and down buttons in the Cell
        /// </summary>
        protected LeftRightAlignment UpDownAlign
        {
            get
            {
                return this.upDownAlignment;
            }

            set
            {
                if (!Enum.IsDefined(typeof(LeftRightAlignment), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
                }

                this.upDownAlignment = value;
            }
        }


        /// <summary>
        /// Gets or sets the maximum value for the Cell
        /// </summary>
        //protected decimal Maximum
        //{
        //    get
        //    {
        //        return this.maximum;
        //    }

        //    set
        //    {
        //        this.maximum = value;

        //        if (this.minimum > this.maximum)
        //        {
        //            this.minimum = this.maximum;
        //        }
        //    }
        //}


        /// <summary>
        /// Gets or sets the minimum value for the Cell
        /// </summary>
        //protected decimal Minimum
        //{
        //    get
        //    {
        //        return this.minimum;
        //    }

        //    set
        //    {
        //        this.minimum = value;

        //        if (this.minimum > this.maximum)
        //        {
        //            this.maximum = value;
        //        }
        //    }
        //}

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

            if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
            {
                if (e.Table.IsCellEditable(e.CellPos))
                {
                    // get the button renderer data
                    I3NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

                    if (rendererData.UpButtonState != I3UpDownStates.Normal)
                    {
                        rendererData.UpButtonState = I3UpDownStates.Normal;

                        e.Table.Invalidate(e.CellRect);
                    }
                    else if (rendererData.DownButtonState != I3UpDownStates.Normal)
                    {
                        rendererData.DownButtonState = I3UpDownStates.Normal;

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

            //
            if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
            {
                if (e.Table.IsCellEditable(e.CellPos))
                {
                    // get the renderer data
                    I3NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

                    rendererData.ClickPoint = new Point(-1, -1);

                    if (this.GetUpButtonBounds().Contains(e.X, e.Y))
                    {
                        rendererData.UpButtonState = I3UpDownStates.Hot;

                        if (!e.Table.IsEditing)
                        {
                            e.Table.EditCell(e.CellPos);
                        }

                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);

                        e.Table.Invalidate(e.CellRect);
                    }
                    else if (this.GetDownButtonBounds().Contains(e.X, e.Y))
                    {
                        rendererData.DownButtonState = I3UpDownStates.Hot;

                        if (!e.Table.IsEditing)
                        {
                            e.Table.EditCell(e.CellPos);
                        }

                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);

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

            //
            if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
            {
                if (e.Table.IsCellEditable(e.CellPos))
                {
                    // get the button renderer data
                    I3NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

                    rendererData.ClickPoint = new Point(e.X, e.Y);

                    if (this.CalcButtonBounds().Contains(e.X, e.Y))
                    {
                        if (!(e.Table.ColumnModel.GetCellEditor(e.CellPos.Column) is I3NumberCellEditor))
                        {
                            throw new InvalidOperationException("Cannot edit Cell as NumberCellRenderer requires a NumberColumn that uses a NumberCellEditor");
                        }

                        if (!e.Table.IsEditing)
                        {
                            e.Table.EditCell(e.CellPos);
                        }

                        if (this.GetUpButtonBounds().Contains(e.X, e.Y))
                        {
                            rendererData.UpButtonState = I3UpDownStates.Pressed;

                            ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);

                            e.Table.Invalidate(e.CellRect);
                        }
                        else if (this.GetDownButtonBounds().Contains(e.X, e.Y))
                        {
                            rendererData.DownButtonState = I3UpDownStates.Pressed;

                            ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);

                            e.Table.Invalidate(e.CellRect);
                        }
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

            if (this.ShowUpDownButtons || this.TableUsingNumericCellEditor(e.Table, e.CellPos))
            {
                if (e.Table.IsCellEditable(e.CellPos))
                {
                    // get the button renderer data
                    I3NumberRendererData rendererData = this.GetNumberRendererData(e.Cell);

                    if (this.GetUpButtonBounds().Contains(e.X, e.Y))
                    {
                        if (rendererData.UpButtonState == I3UpDownStates.Normal)
                        {
                            if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
                            {
                                if (this.GetUpButtonBounds().Contains(rendererData.ClickPoint))
                                {
                                    rendererData.UpButtonState = I3UpDownStates.Pressed;

                                    if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
                                    {
                                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);
                                    }
                                }
                                else if (this.GetDownButtonBounds().Contains(rendererData.ClickPoint))
                                {
                                    rendererData.DownButtonState = I3UpDownStates.Normal;

                                    if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
                                    {
                                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);
                                    }
                                }
                            }
                            else
                            {
                                rendererData.UpButtonState = I3UpDownStates.Hot;

                                if (rendererData.DownButtonState == I3UpDownStates.Hot)
                                {
                                    rendererData.DownButtonState = I3UpDownStates.Normal;
                                }
                            }

                            e.Table.Invalidate(e.CellRect);
                        }
                    }
                    else if (this.GetDownButtonBounds().Contains(e.X, e.Y))
                    {
                        if (rendererData.DownButtonState == I3UpDownStates.Normal)
                        {
                            if (e.Button == MouseButtons.Left && e.Row == e.Table.LastMouseDownCell.Row && e.Column == e.Table.LastMouseDownCell.Column)
                            {
                                if (this.GetDownButtonBounds().Contains(rendererData.ClickPoint))
                                {
                                    rendererData.DownButtonState = I3UpDownStates.Pressed;

                                    if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
                                    {
                                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseDown(this, e);
                                    }
                                }
                                else if (this.GetUpButtonBounds().Contains(rendererData.ClickPoint))
                                {
                                    rendererData.UpButtonState = I3UpDownStates.Normal;

                                    if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
                                    {
                                        ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);
                                    }
                                }
                            }
                            else
                            {
                                rendererData.DownButtonState = I3UpDownStates.Hot;

                                if (rendererData.UpButtonState == I3UpDownStates.Hot)
                                {
                                    rendererData.UpButtonState = I3UpDownStates.Normal;
                                }
                            }

                            e.Table.Invalidate(e.CellRect);
                        }
                    }
                    else
                    {
                        if (rendererData.UpButtonState != I3UpDownStates.Normal || rendererData.DownButtonState != I3UpDownStates.Normal)
                        {
                            rendererData.UpButtonState = I3UpDownStates.Normal;
                            rendererData.DownButtonState = I3UpDownStates.Normal;

                            if (this.TableUsingNumericCellEditor(e.Table, e.CellPos))
                            {
                                ((II3EditorUsesRendererButtons)e.Table.EditingCellEditor).OnEditorButtonMouseUp(this, e);
                            }

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
            if (e.Table.ColumnModel.Columns[e.Column] is I3NumberColumn)
            {
                I3NumberColumn column = (I3NumberColumn)e.Table.ColumnModel.Columns[e.Column];

                this.ShowUpDownButtons = column.ShowUpDownButtons;
                this.UpDownAlign = column.UpDownAlign;
                //this.Maximum = Convert.ToDecimal(column.Maximum);
                //this.Minimum = Convert.ToDecimal(column.Minimum);

                // if the table is editing this cell and the editor is a 
                // NumberCellEditor then we should display the updown buttons
                if (e.Table.IsEditing && e.Table.EditingCell == e.CellPos && e.Table.EditingCellEditor is I3NumberCellEditor)
                {
                    this.ShowUpDownButtons = true;
                }
            }
            else
            {
                this.ShowUpDownButtons = false;
                this.UpDownAlign = LeftRightAlignment.Right;
                //this.Maximum = 100;
                //this.Minimum = 0;
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

            if (this.ShowUpDownButtons)
            {
                I3UpDownStates upButtonState = this.GetNumberRendererData(e.Cell).UpButtonState;
                I3UpDownStates downButtonState = this.GetNumberRendererData(e.Cell).DownButtonState;

                if (!e.Enabled)
                {
                    upButtonState = I3UpDownStates.Disabled;
                    downButtonState = I3UpDownStates.Disabled;
                }

                I3ThemeManager.DrawUpDownButtons(e.Graphics, this.GetUpButtonBounds(), upButtonState, this.GetDownButtonBounds(), downButtonState);
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

            string text = e.Table.ColumnModel.Columns[e.Column].DataToString(e.Cell.Data);
            Rectangle textRect = this.ClientRectangle;

            if (this.ShowUpDownButtons)
            {
                textRect.Width -= this.CalcButtonBounds().Width - 1;

                if (this.UpDownAlign == LeftRightAlignment.Left)
                {
                    textRect.X = this.ClientRectangle.Right - textRect.Width;
                }
            }

            if (e.Enabled)
            {
                e.Graphics.DrawString(text, this.Font, this.ForeBrush, textRect, this.StringFormat);
            }
            else
            {
                e.Graphics.DrawString(text, this.Font, this.GrayTextBrush, textRect, this.StringFormat);
            }

            //cal needWidth needHeigth
            int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
            int buttonWidth = this.ShowUpDownButtons ? this.CalcButtonBounds().Width : 0;
            CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat,maxWidth, 6 + buttonWidth, 0);

            if (e.Focused && e.Enabled)
            {
                Rectangle focusRect = this.ClientRectangle;

                if (this.ShowUpDownButtons)
                {
                    focusRect.Width -= this.CalcButtonBounds().Width;

                    if (this.UpDownAlign == LeftRightAlignment.Left)
                    {
                        focusRect.X = this.ClientRectangle.Right - focusRect.Width;
                    }
                }

                ControlPaint.DrawFocusRectangle(e.Graphics, focusRect);
            }
        }


        #endregion

        #endregion
    }
}
