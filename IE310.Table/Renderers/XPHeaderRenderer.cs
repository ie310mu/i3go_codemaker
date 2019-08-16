using System;
using System.Drawing;
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
    /// XP������ͷ��Ⱦ��
	/// A HeaderRenderer that draws Windows XP themed Column headers
	/// </summary>
	public class I3XPHeaderRenderer : I3HeaderRenderer 
	{
        #region Class Data

        /// <summary>
        /// ѡ��״̬�µ���ɫ
        /// </summary>
        private Color selectedColor;

        #endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the XPHeaderRenderer class 
		/// with default settings
		/// </summary>
		public I3XPHeaderRenderer() : base()
		{
			
		}

		#endregion

        #region Properties

        /// <summary>
        /// ��ȡ������ѡ��״̬�µ���ɫ
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
		/// �ػ汳��
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected override void OnPaintBackground(I3PaintColumnHeaderEventArgs e)
		{
			base.OnPaintBackground(e);

            if (e.Column != null && e.Column.IsSelected)
            {
                SolidBrush brush;
                if (this.SelectedColor == Color.Empty)
                {
                    brush = new SolidBrush(Color.Orange);
                }
                else
                {
                    brush = new SolidBrush(this.SelectedColor);
                }

                e.Graphics.FillRectangle(brush, this.Bounds);
                return;
            }


			if (e.Column == null)
			{
				I3ThemeManager.DrawColumnHeader(e.Graphics, e.HeaderRect, I3ColumnHeaderStates.Normal);
			}
			else
			{
				I3ThemeManager.DrawColumnHeader(e.Graphics, e.HeaderRect, (I3ColumnHeaderStates) e.Column.ColumnState);
			}
		}

        /// <summary>
        /// �ػ汳��
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        protected override void OnPaintBackground(I3PaintRowHeaderEventArgs e)
        {
            base.OnPaintBackground(e);

            if (e.Row != null && e.Row.IsSelected)
            {
                SolidBrush brush;
                if (this.SelectedColor == Color.Empty)
                {
                    brush = new SolidBrush(Color.Orange);
                }
                else
                {
                    brush = new SolidBrush(this.SelectedColor);
                }

                e.Graphics.FillRectangle(brush, this.Bounds);
                return;
            }

            if (e.Row == null)
            {
                I3ThemeManager.DrawRowHeader(e.Graphics, e.HeaderRect, I3RowHeaderStates.Normal);
            }
            else
            {
                I3ThemeManager.DrawRowHeader(e.Graphics, e.HeaderRect, (I3RowHeaderStates)e.Row.RowState);
            }
        }


		/// <summary>
		/// �ػ�
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

            //����image
			if (e.Column.HeaderImage != null)
			{
                //����ͼ����ʾ����
				imageRect = this.CalcImageRect();

                //�����ı���ʾ�����ֵ
				textRect.Width -= imageRect.Width;
				textRect.X += imageRect.Width;

                //���ͼ����ʾ���ұߣ�����ͼ����ʾ�����ı���ʾ�����ֵ
				if (e.Column.HeaderImageOnRight)
				{
					imageRect.X = this.ClientRectangle.Right - imageRect.Width;
					textRect.X = this.ClientRectangle.X;
				}

                //VisualStylesEnabled=true����ͷ����ʱ��ͼ����ʾ�������¡����Ҹ��ƶ�1����
				if (!I3ThemeManager.VisualStylesEnabled && e.Column.ColumnState == I3ColumnState.Pressed)
				{
					imageRect.X += 1;
					imageRect.Y += 1;
				}

                //����ͼ��
				this.DrawColumnHeaderImage(e.Graphics, e.Column.HeaderImage, imageRect, e.Column.Enabled);
			}

            //VisualStylesEnabled=true����ͷ����ʱ���ı���ʾ�������¡����Ҹ��ƶ�1����
			if (!I3ThemeManager.VisualStylesEnabled && e.Column.ColumnState == I3ColumnState.Pressed)
			{
				textRect.X += 1;
				textRect.Y += 1;
			}

            //�������ǩ
            Rectangle arrowRect = Rectangle.Empty;
			if (e.Column.SortOrder != SortOrder.None)
			{
                //���������ǩ��ʾ����
				arrowRect = this.CalcSortArrowRect();
				
				arrowRect.X = textRect.Right - arrowRect.Width;
				textRect.Width -= arrowRect.Width;

                //���������ǩ
				this.DrawSortArrow(e.Graphics, arrowRect, e.Column.SortOrder, e.Column.Enabled);
			}

            //�����ı�
            StringAlignment old = this.StringFormat.Alignment;
            string drawText = e.Column.Caption;
			if (e.Column.Caption.Length > 0 && textRect.Width > 0)
            {
                //��������ı�����ʼ��
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

                //this.StringFormat.Trimming=EllipsisCharacter,ָ���ı�������Χʱ�ļ��з�ʽΪ��....���
                int columnIndex = e.Table.ColumnModel.Columns.IndexOf(e.Column);
                switch (e.Table.ColumnHeaderDisplayMode)
                {
                    case I3ColumnHeaderDisplayMode.Num:
                        drawText = (columnIndex + 1).ToString();
                        break;
                    case I3ColumnHeaderDisplayMode.Reference:
                        drawText = I3ReferenceUtil.GetColumnName(columnIndex + 1);
                        break;
                    case I3ColumnHeaderDisplayMode.None:
                        drawText = "";
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

            //VisualStylesEnabled=true����ͷ����ʱ���ı���ʾ�������¡����Ҹ��ƶ�1����
            if (!I3ThemeManager.VisualStylesEnabled && e.Row.RowState == I3RowState.Pressed)
            {
                textRect.X += 1;
                textRect.Y += 1;
            }

            //��������ı�����ʼ��
            StringAlignment old = this.StringFormat.Alignment;
            this.StringFormat.Alignment = StringAlignment.Center;


            //this.StringFormat.Trimming=EllipsisCharacter,ָ���ı�������Χʱ�ļ��з�ʽΪ��....���
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
