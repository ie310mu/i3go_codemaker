
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Util;
using IE310.Table.Column;
using IE310.Table.Header;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// A HeaderRenderer that draws gradient Column headers
	/// </summary>
	public class I3GradientHeaderRenderer : I3HeaderRenderer 
	{
		#region Class Data

		/// <summary>
		/// The start Color of the gradient
		/// </summary>
		private Color startColor;

		/// <summary>
		/// The ned Color of the gradient
		/// </summary>
		private Color endColor;

		/// <summary>
		/// The Color of the Column header when it is pressed
		/// </summary>
		private Color pressedColor;

        /// <summary>
        /// 选中状态下的颜色
        /// </summary>
        private Color selectedColor;

		#endregion

		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the GradientHeaderRenderer class 
		/// with default settings
		/// </summary>
		public I3GradientHeaderRenderer() : base()
		{
			// steel blue gradient
			this.startColor = Color.FromArgb(200, 209, 215);
			this.endColor = Color.FromArgb(239, 239, 239);
			this.pressedColor = Color.Empty;
            this.selectedColor = Color.Empty;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the start Color of the gradient
		/// </summary>
		[Category("Appearance"),
		Description("The start color of a ColumnHeaders gradient")]
		public Color StartColor
		{
			get
			{
				return this.startColor;
			}

			set
			{
				if (this.startColor != value)
				{
					this.startColor = value;
				}
			}
		}
		

		/// <summary>
		/// Gets or sets the end Color of the gradient
		/// </summary>
		[Category("Appearance"),
		Description("The end color of a ColumnHeaders gradient")]
		public Color EndColor
		{
			get
			{
				return this.endColor;
			}

			set
			{
				if (this.endColor != value)
				{
					this.endColor = value;
				}
			}
		}
		

		/// <summary>
		/// Gets or sets the Color of the Column header when it is pressed
		/// </summary>
		[Category("Appearance"),
		Description("The color of a ColumnHeader when it is in a pressed state")]
		public Color PressedColor
		{
			get
			{
				return this.pressedColor;
			}

			set
			{
				if (this.pressedColor != value)
				{
					this.pressedColor = value;
				}
			}
		}

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

            Color start = this.StartColor;
            Color end = this.EndColor;
            if (e.Column != null && e.Column.IsSelected)
            {
                start = this.SelectedColor;
                if (start == Color.Empty)
                {
                    start = Color.Orange;
                }
                end = ControlPaint.Light(start);
            }

            if (e.Column == null || e.Column.ColumnState != I3ColumnState.Pressed)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.HeaderRect, start, end, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.HeaderRect);
                }

                using (Pen pen = new Pen(end))
                {
                    e.Graphics.DrawLine(pen, e.HeaderRect.Left, e.HeaderRect.Top, e.HeaderRect.Right - 2, e.HeaderRect.Top);
                    e.Graphics.DrawLine(pen, e.HeaderRect.Left, e.HeaderRect.Top, e.HeaderRect.Left, e.HeaderRect.Bottom - 1);
                }

                using (Pen pen = new Pen(start))
                {
                    e.Graphics.DrawLine(pen, e.HeaderRect.Right - 1, e.HeaderRect.Top, e.HeaderRect.Right - 1, e.HeaderRect.Bottom - 1);
                    e.Graphics.DrawLine(pen, e.HeaderRect.Left + 1, e.HeaderRect.Bottom - 1, e.HeaderRect.Right - 1, e.HeaderRect.Bottom - 1);
                }
            }
            else
            {
                Color pressed = this.PressedColor;

                if (pressed == Color.Empty)
                {
                    pressed = ControlPaint.Light(start);
                }

                using (SolidBrush brush = new SolidBrush(pressed))
                {
                    e.Graphics.FillRectangle(brush, e.HeaderRect);
                }

                using (Pen pen = new Pen(start))
                {
                    e.Graphics.DrawRectangle(pen, e.HeaderRect.X, e.HeaderRect.Y, e.HeaderRect.Width - 1, e.HeaderRect.Height - 1);
                }
            }
        }

        /// <summary>
        /// Raises the PaintBackground event
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        protected override void OnPaintBackground(I3PaintRowHeaderEventArgs e)
        {
            base.OnPaintBackground(e);

            Color start = this.StartColor;
            Color end = this.EndColor;
            if (e.Row != null && e.Row.IsSelected)
            {
                start = this.SelectedColor;
                if (start == Color.Empty)
                {
                    start = Color.Orange;
                }
                end = ControlPaint.Light(start);
            }

            if (e.Row == null || e.Row.RowState != I3RowState.Pressed)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.HeaderRect, start, end, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.HeaderRect);
                }

                using (Pen pen = new Pen(end))
                {
                    e.Graphics.DrawLine(pen, e.HeaderRect.Left, e.HeaderRect.Top, e.HeaderRect.Right - 2, e.HeaderRect.Top);
                    e.Graphics.DrawLine(pen, e.HeaderRect.Left, e.HeaderRect.Top, e.HeaderRect.Left, e.HeaderRect.Bottom - 1);
                }

                using (Pen pen = new Pen(start))
                {
                    e.Graphics.DrawLine(pen, e.HeaderRect.Right - 1, e.HeaderRect.Top, e.HeaderRect.Right - 1, e.HeaderRect.Bottom - 1);
                    e.Graphics.DrawLine(pen, e.HeaderRect.Left + 1, e.HeaderRect.Bottom - 1, e.HeaderRect.Right - 1, e.HeaderRect.Bottom - 1);
                }
            }
            else
            {
                Color pressed = this.PressedColor;

                if (pressed == Color.Empty)
                {
                    pressed = ControlPaint.Light(start);
                }

                using (SolidBrush brush = new SolidBrush(pressed))
                {
                    e.Graphics.FillRectangle(brush, e.HeaderRect);
                }

                using (Pen pen = new Pen(this.StartColor))
                {
                    e.Graphics.DrawRectangle(pen, e.HeaderRect.X, e.HeaderRect.Y, e.HeaderRect.Width - 1, e.HeaderRect.Height - 1);
                }
            }
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

                if (e.Column.ColumnState == I3ColumnState.Pressed)
                {
                    imageRect.X += 1;
                    imageRect.Y += 1;
                }

                this.DrawColumnHeaderImage(e.Graphics, e.Column.HeaderImage, imageRect, e.Column.Enabled);
            }

            if (e.Column.ColumnState == I3ColumnState.Pressed)
            {
                textRect.X += 1;
                textRect.Y += 1;
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
                        drawText = (columnIndex + 1).ToString();break;
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
