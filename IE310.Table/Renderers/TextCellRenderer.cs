using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Row;
using IE310.Table.Column;


namespace IE310.Table.Renderers
{
    /// <summary>
    /// A CellRenderer that draws Cell contents as strings
    /// </summary>
    public class I3TextCellRenderer : I3CellRenderer
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TextCellRenderer class with 
        /// default settings
        /// </summary>
        public I3TextCellRenderer()
            : base()
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

            string text = e.Table.ColumnModel.Columns[e.Column].DataToString(e.Cell.Data);

            if (text != null && text.Length != 0)
            {
                Brush brush;
                if (e.Enabled)
                {
                    brush = this.ForeBrush;
                }
                else
                {
                    brush = this.GrayTextBrush;
                }

                e.Graphics.DrawString(text, this.Font, brush, this.ClientRectangle, this.StringFormat);
            }

            //cal needWidth needHeigth
            int maxWidth = e.Table.ColumnModel.Columns[e.Column].CellTextAutoWarp ? e.Table.ColumnModel.Columns[e.Column].Width : 0;
            CalCellNeedSize(e.Graphics, e.Cell, text, this.Font, this.StringFormat, maxWidth, 6, 0);

            if (e.Focused && e.Enabled)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
            }
        }

        #endregion

        #endregion
    }
}
