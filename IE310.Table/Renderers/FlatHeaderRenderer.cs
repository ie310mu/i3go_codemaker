
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Util;
using IE310.Table.Header;
using IE310.Table.Column;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A HeaderRenderer that draws flat Column headers
	/// </summary>
	public class I3FlatHeaderRenderer : I3HeaderRenderer 
	{

        #region Class Data

        /// <summary>
        /// 选中状态下的颜色
        /// </summary>
        private Color selectedColor;

        #endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the XPHeaderRenderer class 
		/// with default settings
		/// </summary>
		public I3FlatHeaderRenderer() : base()
		{
			this.SetBackBrushColor(SystemColors.Control);
            this.selectedColor = Color.Empty;
		}

		#endregion

        #region Properties

        /// <summary>
        /// 获取或设置选中状态下的颜色
        /// </summary>
        public Color SelectedColor
        {
            get
            {
                return this.selectedColor;
            }

            set
            {
                if (this.selectedColor != value)
                {
                    this.selectedColor = value;
                }
            }
        }

        #endregion


		#region Events

		#region Paint

		/// <summary>
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected override void OnPaintBackground(I3PaintColumnHeaderEventArgs e)
		{
			base.OnPaintBackground(e);

            SolidBrush brush = this.BackBrush;
            if (e.Column != null && e.Column.IsSelected)
            {
                if (this.SelectedColor == Color.Empty)
                {
                    brush = new SolidBrush(Color.Orange);
                }
                else
                {
                    brush = new SolidBrush(this.SelectedColor);
                }
            }

            e.Graphics.FillRectangle(brush, this.Bounds);
		}

        /// <summary>
        /// Raises the PaintBackground event
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        protected override void OnPaintBackground(I3PaintRowHeaderEventArgs e)
        {
            base.OnPaintBackground(e);

            SolidBrush brush = this.BackBrush;
            if (e.Row != null && e.Row.IsSelected)
            {
                if (this.SelectedColor == Color.Empty)
                {
                    brush = new SolidBrush(Color.Orange);
                }
                else
                {
                    brush = new SolidBrush(this.SelectedColor);
                }
            }

            e.Graphics.FillRectangle(brush, this.Bounds);
        }


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected override void OnPaint(I3PaintColumnHeaderEventArgs e)
		{
			base.OnPaint(e);

			if (e.Column == null)
			{
				return;
			}

			Rectangle textRect = this.ClientRectangle;
			Rectangle imageRect = Rectangle.Empty;

			if (e.Column.HeaderImage != null)
			{
				imageRect = this.CalcImageRect();

				textRect.Width -= imageRect.Width;
				textRect.X += imageRect.Width;

				if (e.Column.HeaderImageOnRight)
				{
					imageRect.X = this.ClientRectangle.Right - imageRect.Width;
					textRect.X = this.ClientRectangle.X;
				}

				this.DrawColumnHeaderImage(e.Graphics, e.Column.HeaderImage, imageRect, e.Column.Enabled);
			}

            Rectangle arrowRect = Rectangle.Empty;
			if (e.Column.SortOrder != SortOrder.None)
			{
				arrowRect = this.CalcSortArrowRect();
				
				arrowRect.X = textRect.Right - arrowRect.Width;
				textRect.Width -= arrowRect.Width;

				this.DrawSortArrow(e.Graphics, arrowRect, e.Column.SortOrder, e.Column.Enabled);
			}

            StringAlignment old = this.StringFormat.Alignment;
            string drawText = e.Column.Caption;
			if (e.Column.Caption.Length > 0 && textRect.Width > 0)
            {
                //计算绘制文本的起始点
                switch (e.Column.HeaderAlignment)
                {
                    case I3ColumnAlignment.Center:
                        this.StringFormat.Alignment = StringAlignment.Center;
                        break;
                    case I3ColumnAlignment.Right:
                        this.StringFormat.Alignment = StringAlignment.Far;
                        break;
                    default:
                        this.StringFormat.Alignment = StringAlignment.Near;
                        break;
                }

                //this.StringFormat.Trimming=EllipsisCharacter,指定文本超出范围时的剪切方式为以....替代
                drawText = e.Column.Caption;
                int columnIndex = e.Table.ColumnModel.Columns.IndexOf(e.Column);
                switch (e.Table.ColumnHeaderDisplayMode)
                {
                    case I3ColumnHeaderDisplayMode.Num:
                        drawText = (columnIndex + 1).ToString();
                        break;
                    case I3ColumnHeaderDisplayMode.Reference:
                        drawText = I3ReferenceUtil.GetColumnName(columnIndex + 1);
                        break;
                    default:
                        break;
                }
				if (e.Column.Enabled)
				{
                    e.Graphics.DrawString(drawText, this.Font, this.ForeBrush, textRect, this.StringFormat);
				}
				else
				{
					using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
					{
                        e.Graphics.DrawString(drawText, this.Font, brush, textRect, this.StringFormat);
					}
                }
            }

            //cal needsize
            CalColumnHeaderNeedWidth(e.Graphics, e.Column, drawText, this.Font, this.StringFormat, imageRect.Width + arrowRect.Width);

            this.StringFormat.Alignment = old;
		}

        /// <summary>
        /// Raises the Paint event
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        protected override void OnPaint(I3PaintRowHeaderEventArgs e)
        {
            base.OnPaint(e);

            if (e.Row == null || e.Table.RowHeaderDisplayMode != I3RowHeaderDisplayMode.Num)
            {
                return;
            }

            Rectangle textRect = this.ClientRectangle;


            //计算绘制文本的起始点
            StringAlignment old = this.StringFormat.Alignment;
            this.StringFormat.Alignment = StringAlignment.Center;

            //this.StringFormat.Trimming=EllipsisCharacter,指定文本超出范围时的剪切方式为以....替代
            string text = (e.RowIndex + 1).ToString();
            if (e.Row.Enabled)
            {
                e.Graphics.DrawString(text, this.Font, this.ForeBrush, textRect, this.StringFormat);
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
                {
                    e.Graphics.DrawString(text, this.Font, brush, textRect, this.StringFormat);
                }
            }

            //cal needsize
            CalRowHeaderNeedHeight(e.Graphics, e.Row, text, this.Font, this.StringFormat);

            this.StringFormat.Alignment = old;
        }

		#endregion

		#endregion
	}
}
