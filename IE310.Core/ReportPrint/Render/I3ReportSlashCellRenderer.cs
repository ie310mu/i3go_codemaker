using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IE310.Core.ReportPrint
{
    public class I3ReportSlashCellRenderer : I3ReportCellRenderer
    {
        public override RectangleF DrawContent(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style, I3PrintArea area, bool draw)
        {
            string text = GetText(cell, area);
            if (!draw || string.IsNullOrEmpty(text))
            {
                return RectangleF.Empty;
            }

            string[] values = text.Split(new char[] { '|', ',', ';' });


            if (values.Length > 2)
            {
                DrawLine2(g, rect);
                DrawString1(values[0], g, scale, reportData, cell, rect, style, area);
                DrawString2(values[1], g, scale, reportData, cell, rect, style, area);
                DrawString3(values[2], g, scale, reportData, cell, rect, style, area);
            }
            else
            {
                DrawLine1(g, rect);
                DrawString1(values[0], g, scale, reportData, cell, rect, style, area);
                if (values.Length > 1)
                {
                    DrawString2(values[1], g, scale, reportData, cell, rect, style, area);
                }
            }

            return RectangleF.Empty;
        }

        /// <summary>
        /// 左下角
        /// </summary>
        /// <param name="text"></param>
        private void DrawString1(string text, Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style, I3PrintArea area)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            StringFormat sf = GetStringFormat(cell, style);
            Brush brush = new SolidBrush(style.FontColor);
            Font font = GetFont(scale, cell, style);

            try
            {
                #region 计算文本绘制区域
                SizeF sizeF = g.MeasureString(text, font, (int)rect.Width, sf);
                RectangleF r = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
                r.X += 3; //居左
                r.Width = sizeF.Width;
                r.Y += r.Height - sizeF.Height;  //居下
                r.Height = sizeF.Height;
                #endregion

                if (!r.IsEmpty)
                {
                    g.DrawString(text, font, brush, r, sf);
                }
            }
            finally
            {
                font.Dispose();
                brush.Dispose();
                sf.Dispose();
            }
        }

        /// <summary>
        /// 右上角
        /// </summary>
        /// <param name="text"></param>
        private void DrawString2(string text, Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style, I3PrintArea area)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            StringFormat sf = GetStringFormat(cell, style);
            Brush brush = new SolidBrush(style.FontColor);
            Font font = GetFont(scale, cell, style);

            try
            {
                #region 计算文本绘制区域
                SizeF sizeF = g.MeasureString(text, font, (int)rect.Width, sf);
                RectangleF r = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
                r.X += r.Width - 3 - sizeF.Width;  //居右
                r.Width = sizeF.Width;
                r.Y += 5;  //居上
                r.Height = sizeF.Height;
                #endregion

                if (!r.IsEmpty)
                {
                    g.DrawString(text, font, brush, r, sf);
                }
            }
            finally
            {
                font.Dispose();
                brush.Dispose();
                sf.Dispose();
            }
        }

        /// <summary>
        /// 斜中间
        /// </summary>
        /// <param name="text"></param>
        private void DrawString3(string text, Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style, I3PrintArea area)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            StringFormat sf = GetStringFormat(cell, style);
            Brush brush = new SolidBrush(style.FontColor);
            Font font = GetFont(scale, cell, style);

            try
            {
                char[] chars = text.ToCharArray();
                float posx = rect.Right - 3;
                float xsplit = 1;
                float posy = rect.Bottom;
                float totalHeigth = GetTotalHeight(chars, g, font, sf);
                float ysplit = chars.Length == 0 ? 0 : (rect.Height / 2 - totalHeigth) / (chars.Length - 1);

                for (int i = chars.Length - 1; i >= 0; i--)
                {
                    string str = chars[i].ToString();
                    SizeF sizeF = g.MeasureString(str, font, (int)rect.Width, sf);
                    RectangleF r = new RectangleF(posx - sizeF.Width, posy - sizeF.Height, sizeF.Width, sizeF.Height);

                    if (!r.IsEmpty)
                    {
                        g.DrawString(str, font, brush, r, sf);
                    }

                    posx = posx - sizeF.Width - xsplit;
                    posy = posy - sizeF.Height - ysplit;
                }
            }
            finally
            {
                font.Dispose();
                brush.Dispose();
                sf.Dispose();
            }
        }

        private float GetTotalHeight(char[] chars, Graphics g, Font font, StringFormat sf)
        {
            float total = 0;
            foreach (char c in chars)
            {
                string str = c.ToString();
                SizeF sizeF = g.MeasureString(str, font, 1000, sf);
                total += sizeF.Height;
            }

            return total;
        }

        /// <summary>
        /// 左上角、右下角连线
        /// </summary>
        private void DrawLine1(Graphics g, RectangleF rect)
        {
            SmoothingMode oldMode = g.SmoothingMode;
            try
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                PointF p1 = new PointF(rect.X, rect.Y);
                PointF p2 = new PointF(rect.X + rect.Width, rect.Y + rect.Height);
                using (Pen pen = new Pen(Brushes.Black, 1))
                {
                    g.DrawLine(pen, p1, p2);
                }
            }
            finally
            {
                g.SmoothingMode = oldMode;
            }
        }

        /// <summary>
        /// -------------------------------------------------
        /// --    -                                                                -
        /// -      -             -                                                 -
        /// -            -                      -                                  -
        /// -                -                                  -                  -
        /// -                    -                                            -    -
        /// -                        -                                             -
        /// -                             -                                        -
        /// -------------------------------------------------
        /// </summary>
        private void DrawLine2(Graphics g, RectangleF rect)
        {
            SmoothingMode oldMode = g.SmoothingMode;
            try
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                PointF p1 = new PointF(rect.X, rect.Y);
                PointF p2 = new PointF(rect.X + rect.Width * 2 / 3, rect.Y + rect.Height);
                PointF p3 = new PointF(rect.X + rect.Width, rect.Y + rect.Height * 2 / 3);
                using (Pen pen = new Pen(Brushes.Black, 1))
                {
                    g.DrawLine(pen, p1, p2);
                    g.DrawLine(pen, p1, p3);
                }
            }
            finally
            {
                g.SmoothingMode = oldMode;
            }
        }


        public override SizeF CalCellNeedSize(int orgWidth, int orgHeight, I3ReportCellStyle style, I3ReportCell cell)
        {
            return SizeF.Empty;  //返回空，表示不需要做大小调整
        }

        public override void AdjustCellSize(int width, int height, SizeF needSizeF, I3ReportCellStyle style, I3ReportCell cell, I3MergeRange range, I3ReportData reportData, bool prepareNarrow)
        {
            //不做处理
        }
    }
}
