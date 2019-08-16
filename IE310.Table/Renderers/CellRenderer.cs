
using System;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Cell;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
    /// <summary>
    /// Base class for Renderers that draw Cells
    /// </summary>
    public abstract class I3CellRenderer : I3Renderer, II3CellRenderer
    {
        #region Class Data

        /// <summary>
        /// A string that specifies how a Cells contents are formatted
        /// </summary>
        //private string format;

        /// <summary>
        /// The Brush used to draw disabled text
        /// </summary>
        private SolidBrush grayTextBrush;

        /// <summary>
        /// The amount of padding for the cell being rendered
        /// </summary>
        private I3CellPadding padding;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CellRenderer class with default settings
        /// </summary>
        protected I3CellRenderer()
            : base()
        {
            //this.format = "";

            this.grayTextBrush = new SolidBrush(SystemColors.GrayText);
            this.padding = I3CellPadding.Empty;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Releases the unmanaged resources used by the Renderer and 
        /// optionally releases the managed resources
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if (this.grayTextBrush != null)
            {
                this.grayTextBrush.Dispose();
                this.grayTextBrush = null;
            }
        }


        /// <summary>
        /// Gets the renderer specific data used by the Renderer from 
        /// the specified Cell
        /// </summary>
        /// <param name="cell">The Cell to get the renderer data for</param>
        /// <returns>The renderer data for the specified Cell</returns>
        protected object GetRendererData(I3Cell cell)
        {
            return cell.RendererData;
        }


        /// <summary>
        /// Sets the specified renderer specific data used by the Renderer for 
        /// the specified Cell
        /// </summary>
        /// <param name="cell">The Cell for which the data is to be stored</param>
        /// <param name="value">The renderer specific data to be stored</param>
        protected void SetRendererData(I3Cell cell, object value)
        {
            cell.RendererData = value;
        }

        protected void DrawImage(Graphics g, Image image, Rectangle imageRect, bool enabled)
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

        /// <summary>
        /// 计算将Cell绘制完全时需要的大小
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cell"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="sf"></param>
        public virtual void CalCellNeedSize(Graphics g, I3Cell cell, string text, Font font, StringFormat sf, int maxWidth, int addWidth, int addHeight)
        {
            if (maxWidth <= 0)
            {
                maxWidth = I3ColumnModel.MaxAutoColumnWidth_Const;
            }

            if (text != null && text.Length != 0)
            {
                SizeF sizeF = g.MeasureString(text, font, maxWidth, sf);
                cell.NeedWidth = sizeF.Width + cell.Padding.Left + cell.Padding.Right  + addWidth;
                cell.NeedHeight = sizeF.Height + cell.Padding.Top + cell.Padding.Bottom + addHeight;
            }
            else
            {
                cell.NeedWidth = I3ColumnModel.MinColumnWidth_Const;
                cell.NeedHeight = I3TableModel.MinRowHeight_Const;
            }
        }

        #endregion


        #region Properties

        /// <summary>
        /// Overrides Renderer.ClientRectangle
        /// </summary>
        public override Rectangle ClientRectangle
        {
            get
            {
                Rectangle client = new Rectangle(this.Bounds.Location, this.Bounds.Size);

                // take borders into account
                client.Width -= I3Renderer.BorderWidth;
                client.Height -= I3Renderer.BorderWidth;

                // take cell padding into account
                client.X += this.Padding.Left + 1;
                client.Y += this.Padding.Top;
                client.Width -= this.Padding.Left + this.Padding.Right + 1;
                client.Height -= this.Padding.Top + this.Padding.Bottom;

                return client;
            }
        }


        /// <summary>
        /// Gets or sets the string that specifies how a Cells contents are formatted
        /// </summary>
        //protected string Format
        //{
        //    get
        //    {
        //        return this.format;
        //    }

        //    set
        //    {
        //        this.format = value;
        //    }
        //}


        /// <summary>
        /// Gets the Brush used to draw disabled text
        /// </summary>
        protected Brush GrayTextBrush
        {
            get
            {
                return this.grayTextBrush;
            }
        }


        /// <summary>
        /// Gets or sets the amount of padding around the Cell being rendered
        /// </summary>
        protected I3CellPadding Padding
        {
            get
            {
                return this.padding;
            }

            set
            {
                this.padding = value;
            }
        }

        #endregion


        #region Events

        #region Focus

        /// <summary>
        /// Raises the GotFocus event
        /// </summary>
        /// <param name="e">A CellFocusEventArgs that contains the event data</param>
        public virtual void OnGotFocus(I3CellFocusEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }

            e.Table.Invalidate(e.CellRect);
        }


        /// <summary>
        /// Raises the LostFocus event
        /// </summary>
        /// <param name="e">A CellFocusEventArgs that contains the event data</param>
        public virtual void OnLostFocus(I3CellFocusEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }

            e.Table.Invalidate(e.CellRect);
        }

        #endregion

        #region Keys

        /// <summary>
        /// Raises the KeyDown event
        /// </summary>
        /// <param name="e">A CellKeyEventArgs that contains the event data</param>
        public virtual void OnKeyDown(I3CellKeyEventArgs e)
        {

        }


        /// <summary>
        /// Raises the KeyUp event
        /// </summary>
        /// <param name="e">A CellKeyEventArgs that contains the event data</param>
        public virtual void OnKeyUp(I3CellKeyEventArgs e)
        {

        }

        #endregion

        #region Mouse

        #region MouseEnter

        /// <summary>
        /// Raises the MouseEnter event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnMouseEnter(I3CellMouseEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }

            bool tooltipActive = e.Table.ToolTip.Active;

            if (tooltipActive)
            {
                e.Table.ToolTip.Active = false;
            }

            e.Table.ResetMouseEventArgs();

            e.Table.ToolTip.SetToolTip(e.Table, e.Cell.ToolTipText);

            if (tooltipActive)
            {
                e.Table.ToolTip.Active = true;
            }
        }

        #endregion

        #region MouseLeave

        /// <summary>
        /// Raises the MouseLeave event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnMouseLeave(I3CellMouseEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }
        }

        #endregion

        #region MouseUp

        /// <summary>
        /// Raises the MouseUp event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnMouseUp(I3CellMouseEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }
        }

        #endregion

        #region MouseDown

        /// <summary>
        /// Raises the MouseDown event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnMouseDown(I3CellMouseEventArgs e)
        {
            if (!e.Table.Focused)
            {
                if (!(e.Table.IsEditing && e.Table.EditingCell == e.CellPos && e.Table.EditingCellEditor is II3EditorUsesRendererButtons))
                {
                    e.Table.Focus();
                }
            }

            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }
        }

        #endregion

        #region MouseMove

        /// <summary>
        /// Raises the MouseMove event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnMouseMove(I3CellMouseEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }
        }

        #endregion

        #region Click

        /// <summary>
        /// Raises the Click event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnClick(I3CellMouseEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }

            if ((e.Table.EditStartAction == I3EditStartAction.SingleClick) 
                && e.Table.IsCellEditable(e.CellPos))
            {
                e.Table.EditCell(e.CellPos);
            }
        }


        /// <summary>
        /// Raises the DoubleClick event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        public virtual void OnDoubleClick(I3CellMouseEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell == null)
            {
                this.Padding = I3CellPadding.Empty;
            }
            else
            {
                this.Padding = e.Cell.Padding;
            }

            if ((e.Table.EditStartAction == I3EditStartAction.DoubleClick || e.Table.EditStartAction == I3EditStartAction.DoubleClick_DataInputKey || e.Table.EditStartAction == I3EditStartAction.DoubleClick_DataInputKey) 
                && e.Table.IsCellEditable(e.CellPos))
            {
                e.Table.EditCell(e.CellPos);
            }
        }

        #endregion

        #endregion

        #region Paint

        /// <summary>
        /// Raises the PaintCell event
        /// </summary>
        /// <param name="e">A PaintCellEventArgs that contains the event data</param>
        public virtual void OnPaintCell(I3PaintCellEventArgs e)
        {
            this.Bounds = e.CellRect;

            if (e.Cell != null)
            {
                this.Padding = e.Cell.Padding;

                this.Alignment = e.Table.ColumnModel.Columns[e.Column].CellAlignment;
                this.AutoWarp = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp;
                this.LineAlignment = e.Table.TableModel.Rows[e.Row].Alignment;

                //this.Format = e.Table.ColumnModel.Columns[e.Column].Format;

                this.Font = e.Cell.Font;
            }
            else
            {
                this.Padding = I3CellPadding.Empty;

                this.Alignment = I3ColumnAlignment.Left;
                this.LineAlignment = I3RowAlignment.Center;

                //this.Format = "";

                this.Font = null;
            }

            // if the font is null, use the default font
            if (this.Font == null)
            {
                this.Font = Control.DefaultFont;
            }

            // paint the Cells background
            this.OnPaintBackground(e);

            // paint the Cells foreground
            this.OnPaint(e);
        }


        /// <summary>
        /// Raises the PaintBackground event
        /// </summary>
        /// <param name="e">A PaintCellEventArgs that contains the event data</param>
        protected virtual void OnPaintBackground(I3PaintCellEventArgs e)
        {
            this.OnPaintBackgroundNotVirtual(e);
        }

        protected void OnPaintBackgroundNotVirtual(I3PaintCellEventArgs e)
        {
            if (e.Selected && (!e.Table.HideSelection || (e.Table.HideSelection && (e.Table.Focused || e.Table.IsEditing))))
            {
                if (e.Table.Focused || e.Table.IsEditing)
                {
                    this.ForeColor = e.Table.SelectionForeColor;
                    this.BackColor = e.Table.SelectionBackColor;
                }
                else
                {
                    this.BackColor = e.Table.UnfocusedSelectionBackColor;
                    this.ForeColor = e.Table.UnfocusedSelectionForeColor;
                }

                if (this.BackColor.A != 0)
                {
                    e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                }
            }
            else
            {
                this.ForeColor = e.Cell != null ? e.Cell.ForeColor : Color.Black;

                //if (!e.Sorted /*|| (e.Sorted && e.Table.SortedColumnBackColor.A < 255)*/)
                //{
                if (e.Cell != null)
                {
                    if (e.Cell.BackColor.A < 255)
                    {
                        if (e.Row % 2 == 1)
                        {
                            if (e.Table.AlternatingRowColor.A != 0)
                            {
                                this.BackColor = e.Table.AlternatingRowColor;
                                e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                            }
                        }

                        this.BackColor = e.Cell.BackColor;
                        if (e.Cell.BackColor.A != 0)
                        {
                            e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                        }
                    }
                    else
                    {
                        this.BackColor = e.Cell.BackColor;
                        if (e.Cell.BackColor.A != 0)
                        {
                            e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                        }
                    }
                }
                else
                {
                    if (e.Row % 2 == 1)
                    {
                        if (e.Table.AlternatingRowColor.A != 0)
                        {
                            this.BackColor = e.Table.AlternatingRowColor;
                            e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                        }
                    }
                }

                //if (e.Sorted)
                //{
                //    this.BackColor = e.Table.SortedColumnBackColor;
                //    if (e.Table.SortedColumnBackColor.A != 0)
                //    {
                //        e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                //    }
                //}
                //}
                //else
                //{
                //    this.BackColor = e.Table.SortedColumnBackColor;
                //    e.Graphics.FillRectangle(this.BackBrush, e.CellRect);
                //}
            }
        }


        /// <summary>
        /// Raises the Paint event
        /// </summary>
        /// <param name="e">A PaintCellEventArgs that contains the event data</param>
        protected virtual void OnPaint(I3PaintCellEventArgs e)
        {

        }


        /// <summary>
        /// Raises the PaintBorder event
        /// </summary>
        /// <param name="e">A PaintCellEventArgs that contains the event data</param>
        /// <param name="pen">The pen used to draw the border</param>
        protected virtual void OnPaintBorder(I3PaintCellEventArgs e, Pen pen)
        {
            // bottom
            e.Graphics.DrawLine(pen, e.CellRect.Left, e.CellRect.Bottom, e.CellRect.Right, e.CellRect.Bottom);

            // right
            e.Graphics.DrawLine(pen, e.CellRect.Right, e.CellRect.Top, e.CellRect.Right, e.CellRect.Bottom);
        }

        #endregion

        #endregion
    }
}
