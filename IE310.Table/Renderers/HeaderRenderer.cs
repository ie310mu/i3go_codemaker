
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
	/// <summary>
    /// ��ͷ��Ⱦ���ĸ��࣬������
	/// Base class for Renderers that draw Column headers
	/// </summary>
	public abstract class I3HeaderRenderer : I3Renderer, II3HeaderRenderer
	{


		#region Constructor
		
		/// <summary>
        /// ���캯��
		/// Initializes a new instance of the HeaderRenderer class with default settings
		/// </summary>
		protected I3HeaderRenderer() : base()
		{
			this.StringFormat.Alignment = StringAlignment.Near;
		}

		#endregion


		#region Methods

		/// <summary>
        /// ������ͷ����ʾͼ������� (��ʾͼ�������С������Ϊ16��16)(λ��Ϊ��ʾ�����)
		/// </summary>
		/// <returns>A Rectangle that represents the size and location of the Image 
		/// displayed on the ColumnHeader</returns>
		protected Rectangle CalcImageRect()
		{
			Rectangle imageRect = this.ClientRectangle;

			if (imageRect.Width > 16)
			{
				imageRect.Width = 16;
			}

			if (imageRect.Height > 16)
			{
				imageRect.Height = 16;

				imageRect.Y += (this.ClientRectangle.Height - imageRect.Height) / 2;
			}

			return imageRect;
		}


		/// <summary>
        /// ������ʾ�����ǩ������
		/// Returns a Rectangle that represents the size and location of the sort arrow
		/// </summary>
		/// <returns>A Rectangle that represents the size and location of the sort arrow</returns>
		protected Rectangle CalcSortArrowRect()
		{
			Rectangle arrowRect = this.ClientRectangle;

			arrowRect.Width = 12;
			arrowRect.X = this.ClientRectangle.Right - arrowRect.Width;

			return arrowRect;
		}

        /// <summary>
        /// ���㽫ColumnHeader������ȫʱ��Ҫ�Ĵ�С
        /// </summary>
        /// <param name="g"></param>
        /// <param name="column"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="sf"></param>
        /// <param name="imageWidth"></param>
        public virtual void CalColumnHeaderNeedWidth(Graphics g, I3Column column, string text, Font font, StringFormat sf, int imageWidth)
        {
            if (text != null && text.Length != 0)
            {
                SizeF sizeF = g.MeasureString(text, font, I3ColumnModel.MaxAutoColumnWidth_Const, sf);
                column.NeedWidth = sizeF.Width + imageWidth + 6;
            }
            else
            {
                column.NeedWidth = I3ColumnModel.MinColumnWidth_Const;
            }
        }

        public virtual void CalRowHeaderNeedHeight(Graphics g, I3Row row, string text, Font font, StringFormat sf)
        {
            if (text != null && text.Length != 0)
            {
                SizeF sizeF = g.MeasureString(text, font, I3ColumnModel.MaxAutoColumnWidth_Const, sf);
                row.NeedHeight = sizeF.Height + 6;
            }
            else
            {
                row.NeedHeight = I3ColumnModel.MinColumnWidth_Const;
            }
        }



		#endregion
		

		#region Properties
		
		/// <summary>
        /// ��ȡ��������
		/// Overrides Renderer.ClientRectangle
		/// </summary>
		[Browsable(false)]
		public override Rectangle ClientRectangle
		{
			get
			{
				Rectangle client = new Rectangle(this.Bounds.Location, this.Bounds.Size);
				
				//
				client.Inflate(-2, -2);

				return client;
			}
		}


		#endregion


		#region Events

		#region Mouse

		#region MouseEnter

		/// <summary>
        /// �������¼�
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnMouseEnter(I3ColumnHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;

            bool tooltipActive = e.Table.ToolTip.Active;

            if (tooltipActive)
            {
                e.Table.ToolTip.Active = false;
            }

            e.Table.ResetMouseEventArgs();

            if (e.Column != null )
            {
                e.Table.ToolTip.SetToolTip(e.Table, e.Column.ToolTipText);

                if (tooltipActive)
                {
                    e.Table.ToolTip.Active = true;
                }
            }
        }

        /// <summary>
        /// �������¼�
        /// Raises the MouseEnter event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnMouseEnter(I3RowHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;

            bool tooltipActive = e.Table.ToolTip.Active;

            if (tooltipActive)
            {
                e.Table.ToolTip.Active = false;
            }

            e.Table.ResetMouseEventArgs();

            if (e.Row != null )
            {
                e.Table.ToolTip.SetToolTip(e.Table, e.Row.ToolTipText);

                if (tooltipActive)
                {
                    e.Table.ToolTip.Active = true;
                }
            }
        }

		#endregion

		#region MouseLeave

		/// <summary>
        /// ����뿪�¼�
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseLeave(I3ColumnHeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

        /// <summary>
        /// ����뿪�¼�
        /// Raises the MouseLeave event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnMouseLeave(I3RowHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;
        }

		#endregion

		#region MouseUp

		/// <summary>
        /// ��굯���¼�
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseUp(I3ColumnHeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

        /// <summary>
        /// ��굯���¼�
        /// Raises the MouseUp event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnMouseUp(I3RowHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;
        }

		#endregion

		#region MouseDown

		/// <summary>
        /// ��갴���¼�
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseDown(I3ColumnHeaderMouseEventArgs e)
		{
			if (!e.Table.Focused)
			{
				e.Table.Focus();
			}
			
			this.Bounds = e.HeaderRect;
		}

        /// <summary>
        /// ��갴���¼�
        /// Raises the MouseDown event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnMouseDown(I3RowHeaderMouseEventArgs e)
        {
            if (!e.Table.Focused)
            {
                e.Table.Focus();
            }

            this.Bounds = e.HeaderRect;
        }

		#endregion

		#region MouseMove

		/// <summary>
        /// ����ƶ��¼�
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnMouseMove(I3ColumnHeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

        /// <summary>
        /// ����ƶ��¼�
        /// Raises the MouseMove event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnMouseMove(I3RowHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;
        }

		#endregion

		#region Click

		/// <summary>
        /// ������¼�
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnClick(I3ColumnHeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

        /// <summary>
        /// ������¼�
        /// Raises the Click event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnClick(I3RowHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;
        }


		/// <summary>
        /// ���˫���¼�
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		public virtual void OnDoubleClick(I3ColumnHeaderMouseEventArgs e)
		{
			this.Bounds = e.HeaderRect;
		}

        /// <summary>
        /// ���˫���¼�
        /// Raises the DoubleClick event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        public virtual void OnDoubleClick(I3RowHeaderMouseEventArgs e)
        {
            this.Bounds = e.HeaderRect;
        }

		#endregion

		#endregion
		
		#region Paint

		/// <summary>
        /// �ػ���ͷ
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		public virtual void OnPaintColumnHeader(I3PaintColumnHeaderEventArgs e)
		{
			// paint the Column header's background
			this.OnPaintBackground(e);

			// paint the Column headers foreground
			this.OnPaint(e);
		}

        /// <summary>
        /// �ػ���ͷ
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        public virtual void OnPaintRowHeader(I3PaintRowHeaderEventArgs e)
        {
            // paint the Column header's background
            this.OnPaintBackground(e);

            // paint the Column headers foreground
            this.OnPaint(e);
        }


		/// <summary>
        /// �ػ汳�����Ӻ���ʵ��
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected virtual void OnPaintBackground(I3PaintColumnHeaderEventArgs e)
		{
			
		}

        /// <summary>
        /// �ػ汳�����Ӻ���ʵ��
        /// Raises the PaintBackground event
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        protected virtual void OnPaintBackground(I3PaintRowHeaderEventArgs e)
        {

        }


		/// <summary>
        /// �ػ���ͷ
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		protected virtual void OnPaint(I3PaintColumnHeaderEventArgs e)
		{
			
		}

        /// <summary>
        /// �ػ���ͷ 
        /// Raises the Paint event
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        protected virtual void OnPaint(I3PaintRowHeaderEventArgs e)
        {

        }
		
		
		/// <summary>
        /// ������ͷͼ��
		/// Draws the Image contained in the ColumnHeader
		/// </summary>
		/// <param name="g">The Graphics used to paint the Image</param>
		/// <param name="image">The Image to be drawn</param>
		/// <param name="imageRect">A rectangle that specifies the Size and 
		/// Location of the Image</param>
		/// <param name="enabled">Specifies whether the Image should be drawn 
		/// in an enabled state</param>
		protected void DrawColumnHeaderImage(Graphics g, Image image, Rectangle imageRect, bool enabled)
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
        /// ���������ǩ
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="drawRect">A Rectangle that specifies the location 
		/// of the sort arrow</param>
		/// <param name="direction">The direction of the sort arrow</param>
		/// <param name="enabled">Specifies whether the sort arrow should be 
		/// drawn in an enabled state</param>
		protected virtual void DrawSortArrow(Graphics g, Rectangle drawRect, SortOrder direction, bool enabled)
		{
			if (direction != SortOrder.None)
			{
				using (Font font = new Font("Marlett", 9f))
				{
					using (StringFormat format = new StringFormat())
					{
						format.Alignment = StringAlignment.Far;
						format.LineAlignment = StringAlignment.Center;

						if (direction == SortOrder.Ascending)
						{
							if (enabled)
							{
								g.DrawString("t", font, SystemBrushes.ControlDarkDark, drawRect, format);
							}
							else
							{
								using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
								{
									g.DrawString("t", font, brush, drawRect, format);
								}
							}
						}
						else
						{
							if (enabled)
							{
								g.DrawString("u", font, SystemBrushes.ControlDarkDark, drawRect, format);
							}
							else
							{
								using (SolidBrush brush = new SolidBrush(SystemPens.GrayText.Color))
								{
									g.DrawString("u", font, brush, drawRect, format);
								}
							}
						}
					}
				}
			}
		}

		#endregion

		#endregion
	}
}
