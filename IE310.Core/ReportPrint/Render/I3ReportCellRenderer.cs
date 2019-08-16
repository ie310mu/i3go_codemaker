using System;
using System.Collections.Generic;

using System.Text;

using System.Drawing;
using IE310.Core.Utils;
using System.Windows.Forms;

namespace IE310.Core.ReportPrint
{
    public class I3ReportCellRenderer : II3CellRenderer
    {
        public virtual void DrawBackground(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style)
        {
            using (Brush brush = new SolidBrush(style.BackColor))
            {
                g.FillRectangle(brush, rect);
            }
        }

        public virtual RectangleF DrawContent(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style, I3PrintArea area, bool draw)
        {
            string text = GetText(cell, area);
            if (string.IsNullOrEmpty(text))
            {
                return RectangleF.Empty;
            }

            StringFormat sf = GetStringFormat(cell, style);
            //TextFormatFlags ff = GetTextFormatFlags(cell, style);
            Brush brush = new SolidBrush(style.FontColor);
            Font font = GetFont(scale, cell, style);


            string[] strs = text.Split(new string[] { NewLineFlag }, StringSplitOptions.None);

            try
            {
                //先计算正常位置
                RectangleF rr = RectangleF.Empty;
                List<RectangleF> rectList = new List<RectangleF>();
                float startY = float.MaxValue;//上线
                float endY = float.MinValue;//下线
                foreach (string str in strs)
                {
                    #region 计算文本绘制区域
                    SizeF sizeF = g.MeasureString(str, font, (int)rect.Width, sf);
                    //Size sizeF = TextRenderer.MeasureText(g, str, font, new Size((int)rect.Width, 200), ff);
                    RectangleF r = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
                    if (style.Alignment == StringAlignment.Center)
                    {
                        r.X += r.Width / 2 - sizeF.Width / 2;
                        r.Width = sizeF.Width;
                    }
                    else if (style.Alignment == StringAlignment.Far)
                    {
                        r.X += r.Width - sizeF.Width;
                        r.Width = sizeF.Width;
                    }
                    else
                    {
                        r.Width = sizeF.Width;
                    }
                    if (style.LineAlignment == StringAlignment.Center)
                    {
                        r.Y += r.Height / 2 - sizeF.Height / 2;
                        r.Height = sizeF.Height;
                    }
                    else if (style.LineAlignment == StringAlignment.Far)
                    {
                        r.Y += r.Height - sizeF.Height;
                        r.Height = sizeF.Height;
                    }
                    else
                    {
                        r.Height = sizeF.Height;
                    }
                    #endregion

                    #region 居中或居下时的处理
                    if (style.LineAlignment == StringAlignment.Center)
                    {
                        //下面会有一点空白区域，下移以便于达到真正的垂直居中
                        //RectangleF stringRect = new RectangleF(r.X, r.Y + r.Height * 0.1F, r.Width, sizeF.Height);
                        //r = stringRect;
                        //2017.10.09  调整了一下偏移量，不然自动换行时，上面有间距而下面没有
                        RectangleF stringRect = new RectangleF(r.X, r.Y + r.Height * 0.05F, r.Width, sizeF.Height);
                        r = stringRect;
                    }
                    else if (style.LineAlignment == StringAlignment.Far)
                    {
                        //下面会有一点空白区域，下移以便于达到真正的垂直居下
                        RectangleF stringRect = new RectangleF(r.X, r.Y + r.Height * 0.2F, r.Width, r.Height);
                        r = stringRect;
                    }
                    #endregion

                    rectList.Add(r);
                    startY = Math.Min(startY, r.Y);
                    endY = Math.Max(endY, r.Y + r.Height);
                }
                float centerY = startY + (endY - startY) / 2;//中线
                float stepY = endY - startY;

                //再计算换行引起的位置偏移
                int i = -1;
                foreach (string str in strs)
                {
                    i++;
                    RectangleF r = rectList[i];
                    #region 换行位置调整
                    if (strs.Length > 0)//有多行才做调整
                    {
                        if (style.LineAlignment == StringAlignment.Center)//居中
                        {
                            float totalHeight = strs.Length * stepY;
                            float totalStartY = centerY - totalHeight / 2;
                            r.Y = totalStartY + stepY * i;
                        }
                        else if (style.LineAlignment == StringAlignment.Far)//靠下
                        {
                            float thisEndY = endY - stepY * i;
                            r.Y = thisEndY - r.Height;
                        }
                        else//靠上
                        {
                            r.Y = startY + stepY * i;
                        }
                    }
                    #endregion

                    #region 绘制
                    if (draw && !r.IsEmpty)
                    {
                        ReportCellStringDrawingArgs e = new ReportCellStringDrawingArgs(g, str, font, brush, r, sf);
                        OnReportCellStringDrawing(e);
                        if (!e.Hand)
                        {
                            //try
                            //{

                            //用这句，有的电脑上会报错  A generic error occurred in GDI+.
                            g.DrawString(str, font, brush, r, sf);

                            //用这句，不报错了，但是放大缩小时，有的文字会不显示，而且参数不一样，估计输出结果可能也不一样
                            //Rectangle r2 = new Rectangle((int)Math.Ceiling(r.Left), (int)Math.Ceiling(r.Top), (int)Math.Ceiling(r.Width), (int)Math.Ceiling(r.Height));
                            //TextRenderer.DrawText(g, str, font, r2, style.FontColor, ff);

                            //}
                            //catch (Exception ex)
                            //{
                            //    I3LocalLogUtil.Current.WriteExceptionLog("drawstring error", ex);
                            //    I3LocalLogUtil.Current.WriteInfoLog(str);
                            //    I3LocalLogUtil.Current.WriteInfoLog(font.ToString());
                            //    I3LocalLogUtil.Current.WriteInfoLog(brush.ToString());
                            //    I3LocalLogUtil.Current.WriteInfoLog(r.ToString());
                            //    I3LocalLogUtil.Current.WriteInfoLog(sf.ToString());
                            //    I3LocalLogUtil.Current.CompleteLog();
                            //}
                        }
                    }
                    #endregion

                    #region 相交区域处理
                    if (rr == RectangleF.Empty)
                    {
                        rr = r;
                    }
                    else
                    {
                        rr.Y = Math.Min(rr.Y, r.Y);//y取最小值
                        float yEnd = Math.Max(rr.Y + rr.Height, r.Y + r.Height);//yEnd取最大值
                        rr.Height = yEnd - rr.Y;
                        rr.X = Math.Min(rr.X, r.X);//x取最小值
                        float xEnd = Math.Max(rr.X + rr.Width, r.X + r.Width);//xEnd取最大值
                        rr.Width = xEnd - rr.X;
                    }
                    #endregion
                }

                return rr;
            }
            finally
            {
                font.Dispose();
                brush.Dispose();
                sf.Dispose();
            }
        }

        protected virtual StringFormat GetStringFormat(I3ReportCell cell, I3ReportCellStyle style)
        {
            StringFormat sf = StringFormat.GenericDefault;
            sf.Alignment = style.Alignment;
            sf.LineAlignment = style.LineAlignment;
            sf.Trimming = cell.StringTrimming;
            if (!string.IsNullOrEmpty(cell.Text) && cell.Text.IndexOf(NewLineFlag) >= 0)//文本中手工插入了换行符，强制不能进行自动换行
            {
                sf.FormatFlags = StringFormatFlags.NoWrap;
            }
            else
            {
                sf.FormatFlags = style.WordWrap ? (StringFormatFlags)0 : StringFormatFlags.NoWrap;
            }
            sf.FormatFlags = sf.FormatFlags | StringFormatFlags.NoClip; //上下有显示不全而需要剪切时，指定不剪切
            return sf;
        }

        //protected virtual TextFormatFlags GetTextFormatFlags(I3ReportCell cell, I3ReportCellStyle style)
        //{
        //    TextFormatFlags sf = TextFormatFlags.Default;

        //    switch (style.Alignment)
        //    {
        //        case StringAlignment.Near:
        //            sf = sf | TextFormatFlags.Left;
        //            break;
        //        case StringAlignment.Center:
        //            sf = sf | TextFormatFlags.HorizontalCenter;
        //            break;
        //        case StringAlignment.Far:
        //            sf = sf | TextFormatFlags.Right;
        //            break;
        //    }
        //    switch (style.LineAlignment)
        //    {
        //        case StringAlignment.Near:
        //            sf = sf | TextFormatFlags.Top;
        //            break;
        //        case StringAlignment.Center:
        //            sf = sf | TextFormatFlags.VerticalCenter;
        //            break;
        //        case StringAlignment.Far:
        //            sf = sf | TextFormatFlags.Bottom;
        //            break;
        //    }
        //    if (!string.IsNullOrEmpty(cell.Text) && cell.Text.IndexOf(NewLineFlag) >= 0)//文本中手工插入了换行符，强制不能进行自动换行
        //    {
        //        //sf.FormatFlags = StringFormatFlags.NoWrap;
        //    }
        //    else
        //    {
        //        if (style.WordWrap)
        //        {
        //            sf = sf | TextFormatFlags.WordBreak;
        //            sf = sf | TextFormatFlags.NoPadding;
        //        }
        //    }

        //    //sf.Trimming = cell.StringTrimming;
        //    //sf.FormatFlags = sf.FormatFlags | StringFormatFlags.NoClip; //上下有显示不全而需要剪切时，指定不剪切
        //    return sf;
        //}

        protected virtual Font GetFont(float scale, I3ReportCell cell, I3ReportCellStyle style)
        {
            float fontSize = 0;
            if (cell.CalFontSize > 0)
            {
                fontSize = cell.CalFontSize;
            }
            else
            {
                fontSize = style.FontSize == 0 ? 13 : style.FontSize;
            }
            fontSize *= scale;
            Font font = new Font(style.FontName, fontSize, style.FontStyle, GraphicsUnit.Pixel);
            return font;
        }

        private const string PageIndexFlag = "{pageIndex}";
        private const string PageCountFlag = "{pageCount}";
        private const string NewLineFlag = "{r}";

        protected string GetText(I3ReportCell cell, I3PrintArea area)
        {
            if (string.IsNullOrEmpty(cell.Text))
            {
                return cell.Text;
            }

            string text = cell.Text;
            if (text.IndexOf(PageIndexFlag) >= 0)
            {
                text = text.Replace(PageIndexFlag, (area.Index + 1).ToString());
            }
            if (text.IndexOf(PageCountFlag) >= 0)
            {
                text = text.Replace(PageCountFlag, area.Parent.Dic.Count.ToString());
            }
            return text;
        }

        public virtual void DrawCellBorder(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style)
        {
            I3MergeRange range = cell.GetRange_Mode1(reportData);
            if (range == null)
            {
                return;
            }

            if (cell.Row == range.StartRow)
            {
                #region 上边框
                if (style.TopBorder != null)
                {
                    using (Brush brush = new SolidBrush(style.TopBorder.Color))
                    {
                        using (Pen pen = new Pen(brush))
                        {
                            //pen.Width = style.TopBorder.Width * scale;  //缩放不影响边框
                            pen.Width = style.TopBorder.Width;
                            if (pen.Width > 0 && pen.Width < 1)
                            {
                                pen.Width = 1;
                            }
                            pen.Color = style.TopBorder.Color;
                            pen.DashStyle = style.TopBorder.Type;
                            g.DrawLine(pen, new PointF(rect.X, rect.Y), new PointF(rect.X + rect.Width, rect.Y));
                        }
                    }
                }
                #endregion
            }

            if (cell.Col == range.StartCol)
            {
                #region 左边框
                if (style.LeftBorder != null)
                {
                    using (Brush brush = new SolidBrush(style.LeftBorder.Color))
                    {
                        using (Pen pen = new Pen(brush))
                        {
                            //pen.Width = style.LeftBorder.Width * scale;
                            pen.Width = style.LeftBorder.Width;
                            if (pen.Width > 0 && pen.Width < 1)
                            {
                                pen.Width = 1;
                            }
                            pen.Color = style.LeftBorder.Color;
                            pen.DashStyle = style.LeftBorder.Type;
                            g.DrawLine(pen, new PointF(rect.X, rect.Y), new PointF(rect.X, rect.Y + rect.Height));
                        }
                    }
                }
                #endregion
            }

            if (cell.Row == range.EndRow)
            {
                #region 下边框
                if (style.BottomBorder != null)
                {
                    using (Brush brush = new SolidBrush(style.BottomBorder.Color))
                    {
                        using (Pen pen = new Pen(brush))
                        {
                            //pen.Width = style.BottomBorder.Width * scale;
                            pen.Width = style.BottomBorder.Width;
                            if (pen.Width > 0 && pen.Width < 1)
                            {
                                pen.Width = 1;
                            }
                            pen.Color = style.BottomBorder.Color;
                            pen.DashStyle = style.BottomBorder.Type;
                            g.DrawLine(pen, new PointF(rect.X, rect.Y + rect.Height), new PointF(rect.X + rect.Width, rect.Y + rect.Height));
                        }
                    }
                }
                #endregion
            }

            //if (cell.Col == range.EndCol)
            //如果是除边框空白列外最右侧的列，强制画右边框线（要求表格4周都有单位1的空白行列）
            if (range.EndCol == reportData.Cols[reportData.Cols.Length - 1].Col - 1)
            {
                #region 右边框
                if (style.RightBorder != null)
                {
                    using (Brush brush = new SolidBrush(style.RightBorder.Color))
                    {
                        using (Pen pen = new Pen(brush))
                        {
                            //pen.Width = style.RightBorder.Width * scale;
                            pen.Width = style.RightBorder.Width;
                            if (pen.Width > 0 && pen.Width < 1)
                            {
                                pen.Width = 1;
                            }
                            pen.Color = style.RightBorder.Color;
                            pen.DashStyle = style.RightBorder.Type;
                            g.DrawLine(pen, new PointF(rect.X + rect.Width, rect.Y), new PointF(rect.X + rect.Width, rect.Y + rect.Height));
                        }
                    }
                }
                #endregion
            }
        }

        public virtual SizeF CalCellNeedSize(int orgWidth, int orgHeight, I3ReportCellStyle style, I3ReportCell cell)
        {
            float testFontSize = style.FontSize == 0 ? 13 : style.FontSize;  //字体默认值13像素
            return CalCellNeedSize(orgWidth, orgHeight, style, cell, testFontSize);
        }

        private SizeF CalCellNeedSize(int orgWidth, int orgHeight, I3ReportCellStyle style, I3ReportCell cell, float testFontSize)
        {
            SizeF sizeF = new SizeF(orgWidth, orgHeight);
            Bitmap bm = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bm);
            StringFormat stringFormat = StringFormat.GenericDefault;
            Brush brush = new SolidBrush(style.FontColor);
            Font font = new Font(style.FontName, testFontSize, style.FontStyle, GraphicsUnit.Pixel);
            //TextFormatFlags ff = GetTextFormatFlags(cell, style);

            try
            {
                stringFormat.Alignment = style.Alignment;
                stringFormat.LineAlignment = style.LineAlignment;
                stringFormat.Trimming = StringTrimming.None;  //不减小内容以测试实际需要的大小
                stringFormat.FormatFlags = style.WordWrap ? (StringFormatFlags)0 : StringFormatFlags.NoWrap;
                Rectangle rect = new Rectangle(0, 0, orgWidth, orgHeight);
                sizeF = style.WordWrap ? g.MeasureString(cell.Text, font, rect.Width, stringFormat)   //换行以测试高度  //这里仍然用cell.Text测试，还没有分页
                                                                       : g.MeasureString(cell.Text, font, 1000, stringFormat);  //不换行测试宽度，最大宽度1000;
                //sizeF = style.WordWrap ? TextRenderer.MeasureText(g, cell.Text, font, new Size((int)rect.Width, 200), ff)   //换行以测试高度  //这里仍然用cell.Text测试，还没有分页
                //                                                       : TextRenderer.MeasureText(g, cell.Text, font, new Size(1000, 200), ff);  //不换行测试宽度，最大宽度1000;
            }
            finally
            {
                font.Dispose();
                brush.Dispose();
                stringFormat.Dispose();
                g.Dispose();
                bm.Dispose();
            }
            return sizeF;
        }

        public virtual void AdjustCellSize(int width, int height, SizeF needSizeF, I3ReportCellStyle style, I3ReportCell cell, I3MergeRange range, I3ReportData reportData, bool prepareNarrow)
        {
            int fontSize = style.FontSize == 0 ? 13 : style.FontSize;  //字体默认值13像素

            if (needSizeF.Width > width || needSizeF.Height > height)
            {
                switch (style.AdjustSize)
                {
                    case I3AdjustSize.扩大单元格:
                        #region 扩大单元格
                        if (needSizeF.Width > width)
                        {
                            needSizeF.Width += 4F; //加大一点
                            float pro = needSizeF.Width / width;
                            for (int i = range.StartCol; i <= range.EndCol; i++)
                            {
                                reportData.Cols[i].Width = (int)(reportData.Cols[i].Width * pro);
                            }
                        }
                        if (needSizeF.Height > height)
                        {
                            float pro = needSizeF.Height / height;
                            for (int i = range.StartRow; i <= range.EndRow; i++)
                            {
                                reportData[i].Height = (int)(reportData[i].Height * pro);
                            }
                        }
                        cell.StringTrimming = StringTrimming.None;
                        #endregion
                        break;
                    case I3AdjustSize.缩小内容:
                        #region 缩小内容
                        if (prepareNarrow)
                        {
                            //换行时字号递减1进行测试 ,   不换行时根据宽度
                            if (style.WordWrap)
                            {
                                if (needSizeF.Height > height)
                                {
                                    float calFontSize = fontSize;
                                    while (calFontSize > 5 && needSizeF.Height > height)
                                    {
                                        calFontSize--;
                                        needSizeF = CalCellNeedSize(width, height, style, cell, calFontSize);
                                    }
                                    cell.CalFontSize = calFontSize;
                                    cell.HasCalFontSize = true;
                                    cell.StringTrimming = StringTrimming.None;
                                }
                            }
                            else
                            {
                                if (needSizeF.Width > width)
                                {
                                    float pro2 = width / needSizeF.Width;
                                    float calFontSize = fontSize * pro2;
                                    calFontSize = calFontSize > 5F ? calFontSize : 5F;  //最小5号
                                    cell.CalFontSize = calFontSize;
                                    cell.StringTrimming = StringTrimming.None;
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        #region 都不变
                        cell.StringTrimming = StringTrimming.None;
                        #endregion
                        break;
                }
            }
        }


        public static event ReportCellStringDrawing ReportCellStringDrawing;
        public static void OnReportCellStringDrawing(ReportCellStringDrawingArgs e)
        {
            if (ReportCellStringDrawing != null)
            {
                ReportCellStringDrawing(e);
            }
        }
    }
}


public delegate void ReportCellStringDrawing(ReportCellStringDrawingArgs e);

public class ReportCellStringDrawingArgs
{
    public ReportCellStringDrawingArgs(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
    {
        this.g = g;
        this.s = s;
        this.font = font;
        this.brush = brush;
        this.layoutRectangle = layoutRectangle;
        this.format = format;
    }

    private bool hand = false;

    public bool Hand
    {
        get
        {
            return hand;
        }
        set
        {
            hand = value;
        }
    }

    private Graphics g;

    public Graphics G
    {
        get
        {
            return g;
        }
        set
        {
            g = value;
        }
    }
    private string s;

    public string S
    {
        get
        {
            return s;
        }
        set
        {
            s = value;
        }
    }
    private Font font;

    public Font Font
    {
        get
        {
            return font;
        }
        set
        {
            font = value;
        }
    }
    private Brush brush;

    public Brush Brush
    {
        get
        {
            return brush;
        }
        set
        {
            brush = value;
        }
    }
    private RectangleF layoutRectangle;

    public RectangleF LayoutRectangle
    {
        get
        {
            return layoutRectangle;
        }
        set
        {
            layoutRectangle = value;
        }
    }
    private StringFormat format;

    public StringFormat Format
    {
        get
        {
            return format;
        }
        set
        {
            format = value;
        }
    }
}
