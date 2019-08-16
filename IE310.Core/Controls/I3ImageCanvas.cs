using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace XldDamLib.InsControl.Preview
{
    [ToolboxBitmap(typeof(ColorDialog))]
    public class InsImageCanvas : Control
    {
        public InsImageCanvas()
            : this(new Size(400, 300))
        {
        }
        public InsImageCanvas(Size canvasSize)
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.DarkGray;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.Size = canvasSize;
        }



        private Image image;
        /// <summary>
        /// 获取或设置显示的图像
        /// </summary>
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                ZoomToControl();
            }
        }



        private float maxScale = 5F;
        /// <summary>
        /// 最大缩放倍数
        /// </summary>
        public float MaxScale
        {
            get
            {
                return maxScale;
            }
            set
            {
                maxScale = value;
            }
        }

        private float minScale = 0.01F;
        /// <summary>
        /// 最小缩放倍数
        /// </summary>
        public float MinScale
        {
            get
            {
                return minScale;
            }
            set
            {
                minScale = value;
            }
        }

        private float curScale = 1;
        /// <summary>
        /// 当前的缩放倍数
        /// </summary>
        public float CurScale
        {
            get
            {
                return this.curScale;
            }
            set
            {
                //计算缩放前，“中心点”与绘制边界的距离占绘制宽度（高度）的比例
                //在这里，使用鼠标进行缩放时，“中心点”就是鼠标所在的位置，否则“中心点”就是绘制坐标的正中心，此属性会在设置CurScale的值之前被设置
                float xCoefficient = this.imageWidth == 0 ? 0 : ((float)(this.lastPoint.X - this.imageLeft - this.offsetX)) / ((float)this.imageWidth);
                float yCoefficient = this.imageHeight == 0 ? 0 : ((float)(this.lastPoint.Y - this.imageTop - this.offsetY)) / ((float)this.imageHeight);

                //根据缩放级别，重新计算图像的绘制坐标
                if (value > MaxScale)
                {
                    value = MaxScale;
                }
                if (value < MinScale)
                {
                    value = MinScale;
                }
                this.curScale = value;
                this.CalImageLocation();

                //根据中心点和原有的偏移量，计算新的偏移量
                this.offsetX = xCoefficient == 0 ? 0 : Convert.ToInt32((float)this.lastPoint.X - (float)this.imageLeft - xCoefficient * (float)this.imageWidth);
                this.offsetY = yCoefficient == 0 ? 0 : Convert.ToInt32((float)this.lastPoint.Y - (float)this.imageTop - yCoefficient * (float)this.imageHeight);

                //检查X偏移量是否超出范围
                if (this.imageWidth > this.Width)
                {
                    if (this.imageLeft + this.offsetX > 0)
                    {
                        this.offsetX = 0 - this.imageLeft;
                    }
                    if (this.imageLeft + this.imageWidth + this.offsetX < this.Width)
                    {
                        this.offsetX = this.Width - this.imageLeft - this.imageWidth;
                    }
                }
                else
                {
                    this.offsetX = 0;
                }

                //检查Y偏移量是否超出范围
                if (this.imageHeight > this.Height)
                {
                    if (this.imageTop + this.offsetY > 0)
                    {
                        this.offsetY = 0 - this.imageTop;
                    }
                    if (this.imageTop + this.imageHeight + this.offsetY < this.Height)
                    {
                        this.offsetY = this.Height - this.imageTop - this.imageHeight;
                    }
                }
                else
                {
                    this.offsetY = 0;
                }

                this.Invalidate();
            }
        }

        /// <summary>
        /// 缩放到适应控件的大小
        /// </summary>
        public void ZoomToControl()
        {
            if (this.image == null)
            {
                return;
            }

            if (image != null)
            {
                float scaleX = this.Parent.Width * 1.0f / image.Width;
                float scaleY = this.Parent.Height * 1.0f / image.Height;
                float scale = scaleX < scaleY ? scaleX : scaleY;
                if (scale >= 1)
                {
                    scale = 1;
                }

                this.lastPoint.X = Convert.ToInt32(this.Width / 2);
                this.lastPoint.Y = Convert.ToInt32(this.Height / 2);
                this.CurScale = scale;
            }
        }

        //计算图像的绘制坐标
        private void CalImageLocation()
        {
            if (this.image == null)
            {
                return;
            }
            this.imageWidth = Convert.ToInt32(this.image.Width * this.curScale);
            this.imageHeight = Convert.ToInt32(this.image.Height * this.curScale);
            this.imageLeft = (this.Width - this.imageWidth) / 2;
            this.imageTop = (this.Height - this.imageHeight) / 2;
        }

        //图像的绘制坐标
        private int imageLeft = 0;
        private int imageTop = 0;
        private int imageWidth = 0;
        private int imageHeight = 0;
        //绘制偏移量
        private int offsetX = 0;
        private int offsetY = 0;

        //重绘
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(this.BackColor);
            if (this.image != null)
            {
                this.CalImageLocation();
                e.Graphics.DrawImage(this.image,
                    this.imageLeft + this.offsetX,
                    this.imageTop + this.offsetY,
                    this.imageWidth,
                    this.imageHeight);
            }
        }

        protected bool mousePressed = false;
        /// <summary>
        /// 获取鼠标是否按下
        /// </summary>
        public bool MousePressed
        {
            get
            {
                return mousePressed;
            }
        }

        private Point lastPoint = new Point();

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            this.ZoomToControl();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.lastPoint.X = e.X;
            this.lastPoint.Y = e.Y;
            if (e.Delta > 0)
            {
                this.CurScale = this.CurScale * 1.1F;
            }
            else if (e.Delta < 0)
            {
                this.CurScale = this.CurScale / 1.1F;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.MousePressed && this.imageWidth > this.Width)
            {
                this.offsetX = this.offsetX + e.X - this.lastPoint.X;
                if (this.imageLeft + this.offsetX > 0)
                {
                    this.offsetX = 0 - this.imageLeft;
                }
                if (this.imageLeft + this.imageWidth + this.offsetX < this.Width)
                {
                    this.offsetX = this.Width - this.imageLeft - this.imageWidth;
                }
            }

            if (this.MousePressed && this.imageHeight > this.Height)
            {
                this.offsetY = this.offsetY + e.Y - this.lastPoint.Y;
                if (this.imageTop + this.offsetY > 0)
                {
                    this.offsetY = 0 - this.imageTop;
                }
                if (this.imageTop + this.imageHeight + this.offsetY < this.Height)
                {
                    this.offsetY = this.Height - this.imageTop - this.imageHeight;
                }
            }

            this.lastPoint.X = e.X;
            this.lastPoint.Y = e.Y;
            this.Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.mousePressed = true;
            this.lastPoint.X = e.X;
            this.lastPoint.Y = e.Y;
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.mousePressed = false;
            this.Invalidate();
        }


        protected override void Dispose(bool disposing)
        {
            if (this.image != null)
            {
                this.image.Dispose();
                this.image = null;
            }
            base.Dispose(disposing);
        }
    }
}
