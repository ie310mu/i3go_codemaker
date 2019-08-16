using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Drawing;

namespace IE310.Core.ReportPrint
{
    public class I3ReportImageCellRenderer : I3ReportCellRenderer
    {
        public static int ImageSplit = 2;

        public override RectangleF DrawContent(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, System.Drawing.RectangleF rect, I3ReportCellStyle style, I3PrintArea area, bool draw)
        {
            I3ReportImageCell imageCell = cell as I3ReportImageCell;
            if (imageCell.ImageData == null)
            {
                return RectangleF.Empty;
            }

            try
            {
                using (MemoryStream stream = new MemoryStream(imageCell.ImageData))
                {
                    using (Bitmap bitmap = new Bitmap(stream))
                    {
                        float width = imageCell.CalWidth > 0 ? imageCell.CalWidth : imageCell.Width;
                        width *= scale;
                        float heigth = imageCell.CalHeight > 0 ? imageCell.CalHeight : imageCell.Height;
                        heigth *= scale;

                        RectangleF destRect = new RectangleF((float)rect.X + (float)rect.Width / 2F - (float)width / 2F,
                            (float)rect.Y + (float)rect.Height / 2F - (float)heigth / 2F,
                            width,
                            heigth);

                        RectangleF srcRect = new RectangleF(0F, 0F, (float)bitmap.Width, (float)bitmap.Height);
                        if (draw && !destRect.IsEmpty)
                        {
                            g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
                        }

                        return destRect;
                    }
                }
            }
            catch
            {
                RectangleF destRect = new RectangleF((float)rect.X + (float)rect.Width / 2F - (float)100 / 2F,
                    (float)rect.Y + (float)rect.Height / 2F - (float)100 / 2F,
                    100,
                    100);
                return destRect;
            }
        }


        public override SizeF CalCellNeedSize(int orgWidth, int orgHeight, I3ReportCellStyle style, I3ReportCell cell)
        {
            SizeF sizeF = new SizeF();
            I3ReportImageCell imageCell = cell as I3ReportImageCell;
            sizeF.Width = imageCell.Width;
            sizeF.Height = imageCell.Height;
            return sizeF;
        }

        public override void AdjustCellSize(int width, int height, SizeF needSizeF, I3ReportCellStyle style, I3ReportCell cell, I3MergeRange range, I3ReportData reportData, bool prepareNarrow)
        {
            I3ReportImageCell imageCell = cell as I3ReportImageCell;
            int doubleSplit = ImageSplit * 2;  
            if (needSizeF.Width > width - doubleSplit || needSizeF.Height > height - doubleSplit)
            {
                switch (style.AdjustSize)
                {
                    case I3AdjustSize.扩大单元格:
                        #region 扩大单元格
                        if (needSizeF.Width > width - doubleSplit)
                        {
                            float pro = (needSizeF.Width + doubleSplit) / width;
                            for (int i = range.StartCol; i <= range.EndCol; i++)
                            {
                                reportData.Cols[i].Width = (int)(reportData.Cols[i].Width * pro);
                            }
                        }
                        if (needSizeF.Height > height - doubleSplit)
                        {
                            float pro = (needSizeF.Height + doubleSplit) / height;
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
                            //先使宽度一致
                            imageCell.CalWidth = width - doubleSplit;
                            imageCell.CalHeight = imageCell.Height * imageCell.CalWidth / imageCell.Width;
                            //如果高度太大，则使高度一致
                            if (imageCell.CalHeight > height - doubleSplit)
                            {
                                imageCell.CalHeight = height - doubleSplit;
                                imageCell.CalWidth = imageCell.Width * imageCell.CalHeight / imageCell.Height;
                            }
                        }
                        #endregion
                        break;
                    default:   //都不变，调整为图片不按比例缩小
                        #region 图片不按比例缩小
                        imageCell.CalWidth = width - doubleSplit;
                        imageCell.CalHeight = height - doubleSplit;
                        #endregion
                        break;
                }
            }
        }
    }
}
