using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using IE310.Core.Win32;
using System.IO;

namespace IE310.Core.ReportPrint
{
    public partial class I3ReportPreviewerControl : UserControl
    {
        private HScrollBar hScrollBar;
        private VScrollBar vScrollBar;
        private float pageInterval = 10;
        /// <summary>
        /// 页面之间的间距
        /// </summary>
        public float PageInterval
        {
            get
            {
                return pageInterval;
            }
            set
            {
                pageInterval = value;
            }
        }

        private int smallChange = 20;
        /// <summary>
        /// smallChange像素
        /// </summary>
        public int SmallChange
        {
            get
            {
                return smallChange;
            }
            set
            {
                smallChange = value;
            }
        }


        public I3ReportPreviewerControl()
        {
            InitializeComponent();

            //使用自绘画、双缓冲模式
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            this.Dock = DockStyle.Fill;
            this.BackColor = Color.Gray;
            //this.Cursor = Cursors.Hand;

            //创建水平滚动条
            this.hScrollBar = new HScrollBar();
            this.hScrollBar.Visible = true;
            this.hScrollBar.Scroll += new ScrollEventHandler(this.OnHorizontalScroll);
            this.Controls.Add(this.hScrollBar);

            //创建垂直滚动条
            this.vScrollBar = new VScrollBar();
            this.vScrollBar.Visible = true;
            this.vScrollBar.Scroll += new ScrollEventHandler(this.OnVerticalScroll);
            this.Controls.Add(this.vScrollBar);

            ResetScrollBarPosition();
        }

        /// <summary>
        /// 重置滚动条的位置
        /// </summary>
        private void ResetScrollBarPosition()
        {
            this.vScrollBar.Location = new Point(this.Width - SystemInformation.VerticalScrollBarWidth, 0);
            this.vScrollBar.Height = this.Height;
            this.vScrollBar.Visible = VScrollVisible;

            this.hScrollBar.Location = new Point(0, this.Height - SystemInformation.HorizontalScrollBarHeight);
            this.hScrollBar.Width = VScrollVisible ? this.Width - SystemInformation.VerticalScrollBarWidth : this.Width;
            this.hScrollBar.Visible = HScrollVisible;
        }

        protected void OnHorizontalScroll(object sender, ScrollEventArgs e)
        {
            int scrollVal = e.OldValue - e.NewValue;
            if (scrollVal != 0)
            {
                Rectangle invalidateRect = new Rectangle(0, 0, this.Width - VScrollWidth, this.Height - HScrollHeight);
                I3RECT scrollRect = I3RECT.FromRectangle(invalidateRect);

                //移动窗体的绘画区
                I3NativeMethods.ScrollWindow(this.Handle, scrollVal, 0, ref scrollRect, ref scrollRect);

                //计算重绘区域
                if (scrollVal < 0)
                {
                    invalidateRect.X = invalidateRect.Right + scrollVal;
                }
                invalidateRect.Width = Math.Abs(scrollVal) + 1;

                this.Invalidate(invalidateRect, false);
            }
        }

        /// <summary>
        /// 响应垂直滚动条滚动事件
        /// Occurs when the Table's vertical scrollbar is scrolled
        /// </summary>
        /// <param name="sender">The object that Raised the event</param>
        /// <param name="e">A ScrollEventArgs that contains the event data</param>
        protected void OnVerticalScroll(object sender, ScrollEventArgs e)
        {
            int scrollVal = e.OldValue - e.NewValue;
            if (scrollVal != 0)
            {
                Rectangle invalidateRect = new Rectangle(0, 0, this.Width - VScrollWidth, this.Height - HScrollHeight);
                I3RECT scrollRect = I3RECT.FromRectangle(invalidateRect);

                scrollRect.top += 1;
                I3NativeMethods.ScrollWindow(this.Handle, 0, scrollVal, ref scrollRect, ref scrollRect);

                if (scrollVal < 0)
                {
                    invalidateRect.Y = invalidateRect.Bottom + scrollVal;
                }
                invalidateRect.Height = Math.Abs(scrollVal) + 1;

                this.Invalidate(invalidateRect, false);
            }
        }

        private I3ReportCell foucesedCell = null;
        public I3ReportCell FoucesedCell
        {
            get
            {
                return foucesedCell;
            }
            set
            {
                if (foucesedCell == value)
                {
                    return;
                }

                I3ReportCell old = foucesedCell;
                foucesedCell = value;
                InvalidateCell(old);
                InvalidateCell(foucesedCell);
            }
        }

        private bool highlightFoucesedCell = false;
        /// <summary>
        /// 是否高亮FoucesedCell
        /// </summary>
        [DisplayName("是否高亮FoucesedCell")]
        public bool HighlightFoucesedCell
        {
            get
            {
                return highlightFoucesedCell;
            }
            set
            {
                highlightFoucesedCell = value;
            }
        }

        //重绘单元格
        private void InvalidateCell(I3ReportCell cell)
        {
            if (cell == null)
            {
                this.Invalidate();
                return;
            }

            I3PrintArea area = null;
            foreach (I3PrintArea tmpArea in reportDatas.PrintAreas.Dic.Values)
            {
                if (new List<int>(tmpArea.AllRows).IndexOf(cell.Row) >= 0 && new List<int>(tmpArea.AllCols).IndexOf(cell.Col) >= 0)
                {
                    area = tmpArea;
                    break;
                }
            }
            if (area == null)
            {
                this.Invalidate();
                return;
            }

            RectangleF contentRect = GetAreaContentRect(area);
            RectangleF rectF = I3ReportPrintController.CalCellDrawRect_Scale(reportDatas, cell, area, Scale, contentRect, null);
            Rectangle drawRect = new Rectangle((int)Math.Ceiling(rectF.X), (int)Math.Ceiling(rectF.Y), (int)Math.Ceiling(rectF.Width), (int)Math.Ceiling(rectF.Height));
            this.Invalidate(drawRect, false);
        }

        private I3ReportCell mouseOnCell = null;
        public I3ReportCell MouseOnCell
        {
            get
            {
                return mouseOnCell;
            }
            set
            {
                if (mouseOnCell == value)
                {
                    return;
                }

                I3ReportCell old = mouseOnCell;
                mouseOnCell = value;
                InvalidateCell(old);
                InvalidateCell(mouseOnCell);
            }
        }

        private bool highlightMouseOnCell = false;
        /// <summary>
        /// 是否高亮MouseOnCell
        /// </summary>
        [DisplayName("是否高亮MouseOnCell")]
        public bool HighlightMouseOnCell
        {
            get
            {
                return highlightMouseOnCell;
            }
            set
            {
                highlightMouseOnCell = value;
            }
        }

        private I3ReportDatas reportDatas;
        public void Init(I3ReportDatas reportDatas)
        {
            this.reportDatas = reportDatas;
            this.foucesedCell = null;
            this.mouseOnCell = null;
            this.reportDatas.ReCalSizeAndPageInfo();
            this.ReCal();
        }

        //10--500
        private float scale = 0.1F;
        /// <summary>
        /// 缩放比例，0.1---5.0
        /// </summary>
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                //保留两位小数
                value *= 100;
                value = (float)Math.Round(value);
                value /= 100;

                value = value < 0.1F ? 0.1F : value;
                value = value > 5 ? 5 : value;
                if (value.Equals(scale))
                {
                    return;
                }

                scale = value;
                ReCal();
                OnScaleChanged(scale);
            }
        }

        /// <summary>
        /// 重新计算
        /// </summary>
        private void ReCal()
        {
            if (reportDatas == null)
            {
                return;
            }

            float curPositionYPecent = (vScrollBar.Maximum - vScrollBar.LargeChange) == 0 ? 0 : (float)vScrollBar.Value / ((float)vScrollBar.Maximum - (float)vScrollBar.LargeChange);
            curPositionYPecent = curPositionYPecent < 0 ? 0 : curPositionYPecent;
            float curPositionXPecent = (hScrollBar.Maximum - hScrollBar.LargeChange) == 0 ? 0 : (float)hScrollBar.Value / ((float)hScrollBar.Maximum - (float)hScrollBar.LargeChange);
            curPositionXPecent = curPositionXPecent < 0 ? 0 : curPositionXPecent;

            I3ReportData reportData = reportDatas.Datas[0];//取第1个报表的页面设置
            float singleHeight = reportData.PageSetting.PaperHeightPX * scale + pageInterval;
            vScrollBar.Maximum = (int)singleHeight + 1; //便于正确设置LargeChange
            vScrollBar.LargeChange = (int)singleHeight;
            //vScrollBar.SmallChange = (int)(singleHeight / SmallChange);
            vScrollBar.SmallChange = SmallChange;
            float singleWidth = reportData.PageSetting.PaperWidthPX * Scale + pageInterval;
            hScrollBar.Maximum = (int)singleWidth + 1;
            hScrollBar.LargeChange = (int)singleWidth;
            //hScrollBar.SmallChange = (int)(singleWidth / SmallChange);
            hScrollBar.SmallChange = SmallChange;

            //先假没有垂直滚动条
            float totalWidth = singleWidth + pageInterval;
            hScrollBar.Minimum = 0;
            int maxX = (int)totalWidth - (this.Width - 0);
            maxX = maxX < 0 ? 0 : maxX;
            hScrollBar.Maximum = maxX + hScrollBar.LargeChange - 1;
            int valueX = curPositionXPecent.Equals(0) ? 0 : (int)(maxX * curPositionXPecent);
            valueX = valueX < 0 ? 0 : valueX;
            hScrollBar.Value = valueX;

            //设置垂直滚动条
            //float totalHeight = singleHeight * (float)reportDatas.PrintAreas.Dic.Count + pageInterval;
            float totalHeight = 0;
            for (int i = 0; i < reportDatas.PrintAreas.Dic.Count; i++)
            {
                totalHeight = totalHeight + reportDatas.PrintAreas.Dic[i].ReportData.PageSetting.PaperHeightPX * scale + pageInterval;
            }
            totalHeight = totalHeight + pageInterval;
            vScrollBar.Minimum = 0;
            int maxY = (int)totalHeight - (this.Height - HScrollHeight);
            maxY = maxY < 0 ? 0 : maxY;
            vScrollBar.Maximum = maxY + vScrollBar.LargeChange - 1;
            int valueY = curPositionYPecent.Equals(0) ? 0 : (int)(maxY * curPositionYPecent);
            valueY = valueY < 0 ? 0 : valueY;
            vScrollBar.Value = valueY;

            //再次检查是否需要水平滚动条（垂直滚动条的出现可能导致原本不需要水平滚动条，而当前需要了）
            maxX = (int)totalWidth - (this.Width - VScrollWidth);
            maxX = maxX < 0 ? 0 : maxX;
            hScrollBar.Maximum = maxX + hScrollBar.LargeChange - 1;
            valueX = curPositionXPecent.Equals(0) ? 0 : (int)(maxX * curPositionXPecent);
            valueX = valueX < 0 ? 0 : valueX;
            hScrollBar.Value = valueX;

            ResetScrollBarPosition();
            this.Invalidate();
        }

        private int curPageIndex = 0;
        /// <summary>
        /// 当前显示的页数
        /// </summary>
        public int CurPageIndex
        {
            get
            {
                return curPageIndex;
            }
            private set
            {
                if (value == curPageIndex)
                {
                    return;
                }
                curPageIndex = value;
                OnCurPageIndexChanged(curPageIndex);
            }
        }

        /// <summary>
        /// 设置当前显示的页数  从页顶开始显示
        /// </summary>
        /// <param name="value"></param>
        public void SetCurPageIndex(int value)
        {
            if (reportDatas == null)
            {
                return;
            }
            value = value > reportDatas.PrintAreas.Dic.Count ? reportDatas.PrintAreas.Dic.Count : value;
            value = value < 0 ? 0 : value;
            I3ReportData reportData = reportDatas.Datas[0];//取第1个报表的页面设置
            float singleHeight = reportData.PageSetting.PaperHeightPX * Scale + pageInterval;
            float curHeight = value * singleHeight + pageInterval;
            int oldValue = vScrollBar.Value;
            curHeight = curHeight > vScrollBar.Maximum - vScrollBar.LargeChange ? vScrollBar.Maximum - vScrollBar.LargeChange : curHeight;
            curHeight = curHeight < 0 ? 0 : curHeight;
            vScrollBar.Value = (int)curHeight;
            ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, oldValue, (int)curHeight);
            OnVerticalScroll(vScrollBar, args);
            CurPageIndex = value;
        }

        private bool paintPageIndex = false;
        /// <summary>
        /// 是否另外绘制页码
        /// </summary>
        [DisplayName("是否另外绘制页码（在右下角以固定字体大小不考虑缩放进行绘制，用于缩略图）")]
        public bool PaintPageIndex
        {
            get
            {
                return paintPageIndex;
            }
            set
            {
                paintPageIndex = value;
            }
        }


        private bool paintPageIndex2 = false;
        /// <summary>
        /// 是否另外绘制页码
        /// </summary>
        [DisplayName("是否另外绘制页码（在右上角考虑缩放进行绘制，用于实际打印）")]
        public bool PaintPageIndex2
        {
            get
            {
                return paintPageIndex2;
            }
            set
            {
                paintPageIndex2 = value;
            }
        }


        private bool alignTop = false;
        /// <summary>
        /// 显示不全时，是否靠上显示
        /// </summary>
        public bool AlignTop
        {
            get
            {
                return alignTop;
            }
            set
            {
                alignTop = value;
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, this.Width, this.Height));
            }
            if (this.DesignMode || reportDatas == null)
            {
                return;
            }
            RectangleF oldClip = e.Graphics.ClipBounds;

            RectangleF clientRect = new RectangleF(0, 0, this.Width - VScrollWidth, this.Height - HScrollHeight);
            bool firstPageShowed = false;
            for (int index = 0; index < reportDatas.PrintAreas.Dic.Count; index++)
            {
                #region 纸张区域
                I3PrintArea area = reportDatas.PrintAreas.Dic[index];
                RectangleF paperRect = GetAreaPaperRect(area);
                if (!paperRect.IntersectsWith(oldClip) || !paperRect.IntersectsWith(clientRect))
                {
                    continue;
                }
                if (!firstPageShowed)
                {
                    firstPageShowed = true;
                    CurPageIndex = index;
                }
                #endregion

                #region 内容区域
                RectangleF contentRect = GetAreaContentRect(area);
                //内容区域不用作剪切区域判断，（非内容区域也需要绘制）
                #endregion

                #region 绘制
                I3ReportPrintController.PrintAreaToGraphics(e.Graphics, Scale, paperRect, contentRect, reportDatas, area, PaintPageIndex2, index);
                e.Graphics.SetClip(oldClip);
                #endregion

                #region 缩略图页码
                if (PaintPageIndex)
                {
                    string text = string.Format("{0}/{1}", index + 1, reportDatas.PrintAreas.Dic.Count);
                    //缩略图页面还是显示本数据的
                    //string text = string.Format("{0}/{1}", index + 1 + reportData.PageIndexStart - 1, reportData.TotalPageCount);
                    e.Graphics.SetClip(paperRect);
                    using (Font font = new Font("宋体", 10))
                    {
                        StringFormat stringFormat = StringFormat.GenericDefault;
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Near;
                        stringFormat.Trimming = StringTrimming.None;
                        stringFormat.FormatFlags = (StringFormatFlags)0;
                        SizeF sizeF = e.Graphics.MeasureString(text, font, 200, stringFormat);
                        RectangleF textRect = new RectangleF(0, 0, sizeF.Width, sizeF.Height);
                        textRect.Y = paperRect.Y + paperRect.Height - 2 - sizeF.Height;
                        textRect.X = paperRect.X + paperRect.Width - 2 - sizeF.Width;
                        e.Graphics.SetClip(textRect);
                        e.Graphics.DrawString(text, font, Brushes.Red, textRect);
                    }
                }
                #endregion


                #region FoucesedCell、MouseOnCell
                if (HighlightFoucesedCell && this.FoucesedCell != null)
                {
                    if (new List<int>(area.AllRows).IndexOf(this.FoucesedCell.Row) >= 0 && new List<int>(area.AllCols).IndexOf(this.FoucesedCell.Col) >= 0)
                    {
                        RectangleF rect = I3ReportPrintController.CalCellDrawRect_Scale(reportDatas, this.FoucesedCell, area, Scale, contentRect, null);
                        e.Graphics.SetClip(rect);
                        Pen pen = new Pen(Brushes.Red, 6);
                        e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                }

                if (HighlightMouseOnCell && this.MouseOnCell != null)
                {
                    if (new List<int>(area.AllRows).IndexOf(this.MouseOnCell.Row) >= 0 && new List<int>(area.AllCols).IndexOf(this.MouseOnCell.Col) >= 0)
                    {
                        RectangleF rect = I3ReportPrintController.CalCellDrawRect_Scale(reportDatas, this.MouseOnCell, area, Scale, contentRect, null);
                        e.Graphics.SetClip(rect);
                        Pen pen = new Pen(Brushes.Blue, 6);
                        e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                }
                #endregion

                e.Graphics.SetClip(oldClip);
            }
        }

        /// <summary>
        /// 是否需要垂直滚动
        /// </summary>
        private bool VScrollVisible
        {
            get
            {
                return this.vScrollBar.Maximum > this.vScrollBar.LargeChange;
            }
        }

        private int VScrollWidth
        {
            get
            {
                return VScrollVisible ? SystemInformation.VerticalScrollBarWidth : 0;
            }
        }

        /// <summary>
        /// 是否需要水平滚动
        /// </summary>
        private bool HScrollVisible
        {
            get
            {
                return this.hScrollBar.Maximum > this.hScrollBar.LargeChange;
            }
        }

        private int HScrollHeight
        {
            get
            {
                return HScrollVisible ? SystemInformation.HorizontalScrollBarHeight : 0;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            bool ctrl = (e.KeyData & Keys.Control) == Keys.Control;
            Keys key = e.KeyData & Keys.KeyCode;

            if (ctrl && key == Keys.C)
            {
                if (FoucesedCell != null)
                {
                    if (FoucesedCell is I3ReportImageCell)
                    {
                        byte[] data = (FoucesedCell as I3ReportImageCell).ImageData;
                        if (data != null)
                        {
                            try
                            {
                                using (MemoryStream ms = new MemoryStream(data))
                                {
                                    using (Image bitmap = Bitmap.FromStream(ms))
                                    {
                                        Clipboard.SetImage(bitmap);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        Clipboard.SetText(FoucesedCell.Text);
                    }
                }
            }
        }

        /// <summary>
        /// 大小改变
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            ResetScrollBarPosition();
            ReCal();
        }

        /// <summary>
        /// 中键滚动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int delta = 0 - SystemInformation.MouseWheelScrollLines * (e.Delta / 120);

            if (Control.ModifierKeys == Keys.Control)
            {
                if (ChangeScaleByMouseWheel)
                {
                    Scale -= delta > 0 ? 0.1F : -0.1F;
                }
            }
            else
            {
                if (this.VScrollVisible)
                {
                    #region 垂直滚动
                    int oldValue = vScrollBar.Value;
                    int newValue = vScrollBar.Value + vScrollBar.SmallChange * delta;
                    newValue = newValue > vScrollBar.Maximum - vScrollBar.LargeChange ? vScrollBar.Maximum - vScrollBar.LargeChange : newValue;
                    newValue = newValue < 0 ? 0 : newValue;
                    vScrollBar.Value = newValue;
                    ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, oldValue, newValue);
                    OnVerticalScroll(vScrollBar, args);
                    #endregion
                }
                else if (this.HScrollVisible)
                {
                    #region 水平滚动
                    int oldValue = hScrollBar.Value;
                    int newValue = hScrollBar.Value + hScrollBar.SmallChange * delta;
                    newValue = newValue > hScrollBar.Maximum - hScrollBar.LargeChange ? hScrollBar.Maximum - hScrollBar.LargeChange : newValue;
                    newValue = newValue < 0 ? 0 : newValue;
                    hScrollBar.Value = newValue;
                    ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, oldValue, newValue);
                    OnHorizontalScroll(hScrollBar, args);
                    #endregion
                }
            }
        }

        private bool changeScaleByMouseWheel = true;
        /// <summary>
        /// 是否用鼠标中键控制显示比例
        /// </summary>
        public bool ChangeScaleByMouseWheel
        {
            get
            {
                return changeScaleByMouseWheel;
            }
            set
            {
                changeScaleByMouseWheel = value;
            }
        }

        private Point mouseDownPoint = Point.Empty;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseDownPoint = new Point(e.X, e.Y);
            this.Cursor = Cursors.Hand;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseDownPoint = Point.Empty;
            this.Cursor = Cursors.Default;
        }

        private I3ReportCell lastMoveCell = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                #region 左键按下
                if (VScrollVisible)
                {
                    int oldValue = vScrollBar.Value;
                    int offsetY = e.Y - mouseDownPoint.Y;
                    int newValue = vScrollBar.Value - offsetY;
                    newValue = newValue > vScrollBar.Maximum - vScrollBar.LargeChange ? vScrollBar.Maximum - vScrollBar.LargeChange : newValue;
                    newValue = newValue < 0 ? 0 : newValue;
                    vScrollBar.Value = newValue;
                    ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, oldValue, newValue);
                    OnVerticalScroll(vScrollBar, args);
                }
                if (HScrollVisible)
                {
                    int oldValue = hScrollBar.Value;
                    int offsetX = e.X - mouseDownPoint.X;
                    int newValue = hScrollBar.Value - offsetX;
                    newValue = newValue > hScrollBar.Maximum - hScrollBar.LargeChange ? hScrollBar.Maximum - hScrollBar.LargeChange : newValue;
                    newValue = newValue < 0 ? 0 : newValue;
                    hScrollBar.Value = newValue;
                    ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, oldValue, newValue);
                    OnHorizontalScroll(vScrollBar, args);
                }
                mouseDownPoint = new Point(e.X, e.Y);
                #endregion
            }
            else
            {
                I3ReportCell cell = TestCell(e.X, e.Y);
                if (cell != lastMoveCell && lastMoveCell != null)
                {
                    OnCellItemMouseLeave(lastMoveCell);
                }
                if (cell != lastMoveCell && cell != null)
                {
                    OnCellItemMouseIn(cell);
                }
                lastMoveCell = cell;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (this.DesignMode || reportDatas == null)
            {
                return;
            }


            int pageIndex = TestPageIndex(e.X, e.Y);
            if (pageIndex >= 0)
            {
                OnPageClicked(pageIndex);

                I3ReportCell cell = TestCell(pageIndex, e.X, e.Y);
                if (cell != null)
                {
                    OnCellItemClicked(cell);
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (this.DesignMode || reportDatas == null)
            {
                return;
            }


            int pageIndex = TestPageIndex(e.X, e.Y);
            if (pageIndex >= 0)
            {
                //OnPageClicked(pageIndex);

                I3ReportCell cell = TestCell(pageIndex, e.X, e.Y);
                if (cell != null)
                {
                    OnCellItemDoubleClicked(cell);
                }
            }
        }

        /// <summary>
        /// 测试页面
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TestPageIndex(int x, int y)
        {
            if (reportDatas == null)
            {
                return -1;
            }

            RectangleF testRect = new RectangleF(x, y, 1, 1);
            foreach (I3PrintArea area in reportDatas.PrintAreas.Dic.Values)
            {
                RectangleF rect = GetAreaPaperRect(area);
                if (rect.IntersectsWith(testRect))
                {
                    return area.Index;
                }
            }

            return -1;
        }

        private RectangleF GetAreaPaperRect(I3PrintArea area)
        {
            I3ReportData reportData = reportDatas.GetReportDataByAreaIndex(area.Index);
            I3PageSetting setting = reportData.PageSetting;
            float offsetY = vScrollBar.Value;
            float offsetX = hScrollBar.Value;
            RectangleF paperRect = new RectangleF(setting.PaperRect.X * scale, setting.PaperRect.Y * scale, setting.PaperRect.Width * scale, setting.PaperRect.Height * scale);

            //调整y位置
            for (int i = 0; i < area.Index; i++)
            {
                I3ReportData rd = reportDatas.GetReportDataByAreaIndex(i);
                I3PageSetting s = rd.PageSetting;
                paperRect.Y = paperRect.Y + s.PaperRect.Height * scale + pageInterval;
            }
            paperRect.Y = paperRect.Y + pageInterval - offsetY;
            if (!VScrollVisible && !alignTop)
            {
                float totalHeight = 0;
                for (int i = 0; i < reportDatas.PrintAreas.Dic.Count; i++)
                {
                    I3ReportData rd = reportDatas.GetReportDataByAreaIndex(i);
                    I3PageSetting s = rd.PageSetting;
                    totalHeight = totalHeight + s.PaperRect.Height * scale + pageInterval;
                }
                paperRect.Y += (int)((this.Height - HScrollHeight - totalHeight) / 2);
            }

            //调整x位置
            paperRect.X = paperRect.X + pageInterval - offsetX;
            //if (!HScrollVisible)
            {
                float totalWidth = setting.PaperRect.Width * scale + pageInterval;
                paperRect.X += (int)((this.Width - VScrollWidth - totalWidth) / 2);
            }

            return paperRect;

            #region 111
            //I3ReportData reportData = reportDatas.Datas[0];//取第1个报表的页面设置
            //I3PageSetting setting = reportData.PageSetting;
            //float singleHeight = setting.PaperHeightPX * Scale + pageInterval;
            //float singleWidth = setting.PaperWidthPX * Scale + pageInterval;
            //float offsetY = vScrollBar.Value;
            //float offsetX = hScrollBar.Value;

            //RectangleF paperRect = new RectangleF(setting.PaperRect.X * scale, setting.PaperRect.Y * scale, setting.PaperRect.Width * scale, setting.PaperRect.Height * scale);
            //paperRect.Y = paperRect.Y + pageInterval + area.Index * singleHeight - offsetY;
            //paperRect.X = paperRect.X + pageInterval - offsetX;


            //if (!VScrollVisible && !alignTop)
            //{
            //    float totalHeight = singleHeight * reportDatas.PrintAreas.Dic.Count + pageInterval;
            //    paperRect.Y += (int)((this.Height - HScrollHeight - totalHeight) / 2);
            //}
            //if (!HScrollVisible)
            //{
            //    float totalWidth = singleWidth + pageInterval;
            //    paperRect.X += (int)((this.Width - VScrollWidth - totalWidth) / 2);
            //}

            //return paperRect;
            #endregion
        }

        private RectangleF GetAreaContentRect(I3PrintArea area)
        {
            I3ReportData reportData = reportDatas.GetReportDataByAreaIndex(area.Index);
            I3PageSetting setting = reportData.PageSetting;
            float offsetY = vScrollBar.Value;
            float offsetX = hScrollBar.Value;
            RectangleF contentRect = new RectangleF(setting.PaperContentRect.X * scale, setting.PaperContentRect.Y * scale, setting.PaperContentRect.Width * scale, setting.PaperContentRect.Height * scale);

            //调整y位置
            for (int i = 0; i < area.Index; i++)
            {
                I3ReportData rd = reportDatas.GetReportDataByAreaIndex(i);
                I3PageSetting s = rd.PageSetting;
                contentRect.Y = contentRect.Y + s.PaperRect.Height * scale + pageInterval;
            }
            contentRect.Y = contentRect.Y + pageInterval - offsetY;
            if (!VScrollVisible && !alignTop)
            {
                float totalHeight = 0;
                for (int i = 0; i < reportDatas.PrintAreas.Dic.Count; i++)
                {
                    I3ReportData rd = reportDatas.GetReportDataByAreaIndex(i);
                    I3PageSetting s = rd.PageSetting;
                    totalHeight = totalHeight + s.PaperRect.Height * scale + pageInterval;
                }
                contentRect.Y += (int)((this.Height - HScrollHeight - totalHeight) / 2);
            }

            //调整x位置
            contentRect.X = contentRect.X + pageInterval - offsetX;
            //if (!HScrollVisible)
            {
                float totalWidth = setting.PaperRect.Width * scale + pageInterval;
                contentRect.X += (int)((this.Width - VScrollWidth - totalWidth) / 2);
            }

            return contentRect;

            #region 111
            //I3ReportData reportData = reportDatas.Datas[0];//取第1个报表的页面设置
            //I3PageSetting setting = reportData.PageSetting;
            //float singleHeight = setting.PaperHeightPX * Scale + pageInterval;
            //float singleWidth = setting.PaperWidthPX * Scale + pageInterval;
            //float offsetY = vScrollBar.Value;
            //float offsetX = hScrollBar.Value;

            //RectangleF contentRect = new RectangleF(setting.PaperContentRect.X * scale, setting.PaperContentRect.Y * scale, setting.PaperContentRect.Width * scale, setting.PaperContentRect.Height * scale);
            //contentRect.Y = contentRect.Y + pageInterval + area.Index * singleHeight - offsetY;
            //contentRect.X = contentRect.X + pageInterval - offsetX;
            //if (!VScrollVisible && !alignTop)
            //{
            //    float totalHeight = singleHeight * reportDatas.PrintAreas.Dic.Count + pageInterval;
            //    contentRect.Y += (int)((this.Height - HScrollHeight - totalHeight) / 2);
            //}
            //if (!HScrollVisible)
            //{
            //    float totalWidth = singleWidth + pageInterval;
            //    contentRect.X += (int)((this.Width - VScrollWidth - totalWidth) / 2);
            //}

            //return contentRect;
            #endregion
        }


        /// <summary>
        /// 测试单元格
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private I3ReportCell TestCell(int x, int y)
        {
            int pageIndex = TestPageIndex(x, y);
            if (pageIndex >= 0)
            {
                return TestCell(pageIndex, x, y);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 测试行
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TestRow(int x, int y)
        {
            int pageIndex = TestPageIndex(x, y);
            if (pageIndex >= 0)
            {
                return TestRow(pageIndex, x, y);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 测试行
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TestRow(int pageIndex, int x, int y)
        {
            if (reportDatas == null || pageIndex < 0)
            {
                return -1;
            }

            I3PrintArea area = reportDatas.PrintAreas.Dic[pageIndex];
            RectangleF fullRect = GetAreaPaperRect(area);
            RectangleF dataRect = GetAreaContentRect(area);

            foreach (int row in area.AllRows)
            {
                int firstCol = area.AllCols[0];
                I3ReportCell cell = area.ReportData[row][firstCol];
                RectangleF rect = I3ReportPrintController.CalCellClipRect_Scale(reportDatas, cell, area, Scale, dataRect, fullRect);
                if (y >= rect.Top && y <= rect.Bottom)
                {
                    return row;
                }
            }

            return -1;
        }



        /// <summary>
        /// 测试列
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TestCol(int x, int y)
        {
            int pageIndex = TestPageIndex(x, y);
            if (pageIndex >= 0)
            {
                return TestCol(pageIndex, x, y);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 测试列
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TestCol(int pageIndex, int x, int y)
        {
            if (reportDatas == null || pageIndex < 0)
            {
                return -1;
            }

            I3PrintArea area = reportDatas.PrintAreas.Dic[pageIndex];
            RectangleF fullRect = GetAreaPaperRect(area);
            RectangleF dataRect = GetAreaContentRect(area);

            foreach (int col in area.AllCols)
            {
                int firstRow = area.AllRows[0];
                I3ReportCell cell = area.ReportData[firstRow][col];
                RectangleF rect = I3ReportPrintController.CalCellClipRect_Scale(reportDatas, cell, area, Scale, dataRect, fullRect);
                if (x >= rect.Left && x <= rect.Right)
                {
                    return col;
                }
            }

            return -1;
        }

        /// <summary>
        /// 测试单元格
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private I3ReportCell TestCell(int pageIndex, int x, int y)
        {
            if (reportDatas == null || CellItemEventMode == ReportPrint.I3CellItemEventMode.None)
            {
                return null;
            }

            I3ReportData reportData = reportDatas.GetReportDataByAreaIndex(pageIndex);
            int row = TestRow(pageIndex, x, y);
            int col = TestCol(pageIndex, x, y);
            if (row < 0 || col < 0)
            {
                return null;
            }

            I3ReportCell cell = reportData[row][col];
            cell = cell.MergState == I3MergeState.Merged ? reportData.GetMergedStartedCell(row, col) : cell;
            switch (CellItemEventMode)
            {
                case I3CellItemEventMode.CellRect:
                    return cell;
                case I3CellItemEventMode.ContentRect:
                    I3PrintArea area = reportDatas.PrintAreas.Dic[pageIndex];
                    RectangleF fullRect = GetAreaPaperRect(area);
                    RectangleF dataRect = GetAreaContentRect(area);
                    RectangleF rect = I3ReportPrintController.CalCellDrawRect_Scale(reportDatas, cell, area, Scale, dataRect, null);
                    II3CellRenderer renderer = I3CellRendererBuilder.GetRenderer(cell);
                    RectangleF contentRect = renderer.DrawContent(this.CreateGraphics(), Scale, reportData, cell, rect, reportData.GetCellStyle(cell.StyleName), area, false);
                    RectangleF testRect = new RectangleF(x, y, 1, 1);
                    return contentRect.IntersectsWith(testRect) ? cell : null;
                default:
                    return null;
            }
        }

        private I3CellItemEventMode cellItemEventMode = I3CellItemEventMode.None;
        public I3CellItemEventMode CellItemEventMode
        {
            get
            {
                return cellItemEventMode;
            }
            set
            {
                cellItemEventMode = value;
            }
        }

        /// <summary>
        /// 显示比例改变事件
        /// </summary>
        public event ScaleChanged ScaleChanged;
        public void OnScaleChanged(float value)
        {
            if (ScaleChanged != null)
            {
                ScaleChanged(this, value);
            }
        }

        /// <summary>
        /// 当前页改变事件
        /// </summary>
        public event CurPageIndexChanged CurPageIndexChanged;
        public void OnCurPageIndexChanged(int value)
        {
            if (CurPageIndexChanged != null)
            {
                CurPageIndexChanged(this, value);
            }
        }

        public event CurPageIndexChanged PageClicked;
        public void OnPageClicked(int value)
        {
            if (PageClicked != null)
            {
                PageClicked(this, value);
            }
        }

        /// <summary>
        /// 原始尺寸显示
        /// </summary>
        public void OriginalScale()
        {
            this.Scale = 1;
        }

        /// <summary>
        /// 适应页宽
        /// </summary>
        public void FullWidth()
        {
            if (reportDatas == null)
            {
                return;
            }
            //先假设没有垂直滚动条
            //I3ReportData reportData = reportDatas.Datas[0];//取第1个报表的页面设置
            float paperWidthPX = 0;
            for(int i = 0; i < reportDatas.PrintAreas.Dic.Count; i++)
            {
                I3ReportData rd = reportDatas.GetReportDataByAreaIndex(i);
                I3PageSetting p = rd.PageSetting;
                if(p.PaperWidthPX > paperWidthPX)
                {
                    paperWidthPX = p.PaperWidthPX;
                }
            }
            Scale = ((float)this.Width - 0 - pageInterval * 2) / paperWidthPX - 0.01F;
            if (VScrollVisible)  //如果有，再加上
            {
                Scale = ((float)this.Width - (float)SystemInformation.VerticalScrollBarWidth - pageInterval * 2) / paperWidthPX - 0.01F;
            }
        }

        /// <summary>
        /// 适应页高
        /// </summary>
        public void FullHeight()
        {
            if (reportDatas == null)
            {
                return;
            }
            //先假设没有水平滚动条
            I3ReportData reportData = reportDatas.Datas[0];//取第1个报表的页面设置
            Scale = ((float)this.Height - 0 - pageInterval * 2) / reportData.PageSetting.PaperHeightPX - 0.01F;
            if (HScrollVisible)  //如果有，再加上
            {
                Scale = ((float)this.Height - (float)SystemInformation.HorizontalScrollBarHeight - pageInterval * 2) / reportData.PageSetting.PaperHeightPX - 0.01F;
            }
        }

        #region 事件

        public event I3CellItemEvent CellItemClicked;
        public void OnCellItemClicked(I3ReportCell cell)
        {
            this.FoucesedCell = cell;

            if (!this.DesignMode && CellItemClicked != null)
            {
                CellItemClicked(this, new I3CellItemEventArgs(cell));
            }
        }


        public event I3CellItemEvent CellItemDoubleClicked;
        public void OnCellItemDoubleClicked(I3ReportCell cell)
        {
            this.FoucesedCell = cell;

            if (!this.DesignMode && CellItemDoubleClicked != null)
            {
                CellItemDoubleClicked(this, new I3CellItemEventArgs(cell));
            }
        }

        public event I3CellItemEvent CellItemMouseIn;
        public void OnCellItemMouseIn(I3ReportCell cell)
        {
            this.MouseOnCell = cell;

            if (!this.DesignMode && CellItemMouseIn != null)
            {
                CellItemMouseIn(this, new I3CellItemEventArgs(cell));
            }
        }

        public event I3CellItemEvent CellItemMouseLeave;
        public void OnCellItemMouseLeave(I3ReportCell cell)
        {
            if (cell == this.MouseOnCell)
            {
                this.MouseOnCell = null;
            }

            if (!this.DesignMode && CellItemMouseLeave != null)
            {
                CellItemMouseLeave(this, new I3CellItemEventArgs(cell));
            }
        }

        #endregion
    }

    public delegate void ScaleChanged(object sender, float scale);
    public delegate void CurPageIndexChanged(object sender, int curPageIndex);
}
