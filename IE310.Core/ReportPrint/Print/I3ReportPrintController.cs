using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using IE310.Core.Win32;
using IE310.Core.ReportPrint.Item;

namespace IE310.Core.ReportPrint
{
    public static class I3ReportPrintController
    {
        #region 打印

        public static void PrintToPrinter(string printerName, string documentName, I3ReportDatas reportDatas, int startPage, int endPage, int printCount, bool paintPageIndex)
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printerName;
            if (!ps.IsValid)
            {
                throw new Exception("打印机" + printerName + "不存在或不可用！");
            }

            for (int i = 1; i <= printCount; i++)
            {
                PrintToPrinter(printerName, documentName, reportDatas, startPage, endPage, paintPageIndex);
            }
        }

        /// <summary>
        /// 输出到打印机
        /// </summary>
        /// <param name="reportData"></param>
        public static void PrintToPrinter(string printerName, string documentName, I3ReportDatas reportDatas, int startPage, int endPage, bool paintPageIndex)
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printerName;
            if (!ps.IsValid)
            {
                throw new Exception("打印机" + printerName + "不存在或不可用！");
            }

            if (string.IsNullOrEmpty(documentName))
            {
                documentName = "I3ReportDocument";
            }
            I3PrintDocument document = new I3PrintDocument(reportDatas, startPage, endPage);
            document.PaintPageIndex = paintPageIndex;
            document.PrinterSettings = ps;
            document.DocumentName = documentName;
            document.QueryPageSettings += new QueryPageSettingsEventHandler(document_QueryPageSettings);
            document.PrintPage += new PrintPageEventHandler(document_PrintPage);
            document.Print();

            #region 这段代码可显示默认的打印预览窗体。。。不好用。。。
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            //ppd.WindowState = FormWindowState.Maximized;//设定窗体最大化
            //ppd.PrintPreviewControl.Zoom = 1;
            //ppd.FormBorderStyle = FormBorderStyle.Fixed3D;
            //ppd.Document = document;
            //ppd.ShowDialog();
            #endregion
        }

        static void document_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            I3PrintDocument document = sender as I3PrintDocument;
            I3ReportData reportData = document.ReportDatas.GetReportDataByAreaIndex(document.NextPage - 1);
            e.PageSettings.Landscape = reportData.PageSetting.PaperOrientation == PaperOrientation.横向;
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);
        }

        static void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            //float scale = (100F / 96F) * (600F / e.Graphics.DpiX);  //打印机600DPI对应屏幕96DPI  //这个转换与dpi无关
            float scale = (100F / 96F);

            float phyOffsetX = 0;
            float phyOffsetY = 0;
            IntPtr hdc = e.Graphics.GetHdc();
            try
            {
                float width = I3Win32PrintHelper.GetPaperWidth(hdc);   //纸张的宽度（打印机单位）
                float height = I3Win32PrintHelper.GetPaperHeight(hdc);

                phyOffsetX = I3Win32PrintHelper.GetPaperPhyOffsetX(hdc);   //纸张的X偏移（打印机单位）
                phyOffsetX = phyOffsetX * e.PageBounds.Width / width;  //转换为 1/100 英寸
                phyOffsetX *= scale;  //转换为像素

                phyOffsetY = I3Win32PrintHelper.GetPaperPhyOffsetY(hdc);
                phyOffsetY = phyOffsetY * e.PageBounds.Height / height;
                phyOffsetY *= scale;
            }
            finally
            {
                e.Graphics.ReleaseHdc(hdc);
            }

            I3PrintDocument document = sender as I3PrintDocument;
            I3ReportData reportData = document.ReportDatas.GetReportDataByAreaIndex(document.NextPage - 1);
            I3PageSetting setting = reportData.PageSetting;
            if (document.NextPage > 0 && document.NextPage <= document.EndPage)
            {
                RectangleF rect = new RectangleF(e.MarginBounds.X, e.MarginBounds.Y, e.MarginBounds.Width, e.MarginBounds.Height);
                rect.X -= phyOffsetX;
                rect.Y -= phyOffsetY;
                RectangleF contentRect = new RectangleF(setting.PaperContentRect.X * scale, setting.PaperContentRect.Y * scale,
                    setting.PaperContentRect.Width * scale, setting.PaperContentRect.Height * scale);
                contentRect.X -= phyOffsetX;
                contentRect.Y -= phyOffsetY;
                PrintAreaToGraphics(e.Graphics, scale, rect, contentRect, document.ReportDatas,
                    document.ReportDatas.PrintAreas.Dic[document.NextPage - 1], document.PaintPageIndex, document.NextPage - 1);
                if (document.NextPage < document.EndPage)
                {
                    document.NextPage += 1;
                    e.HasMorePages = true;
                }
                else
                {
                    document.NextPage = -1;
                    e.HasMorePages = false;
                }
            }
            else
            {
                e.HasMorePages = false;
            }
        }


        #endregion

        #region 绘制

        /// <summary>
        /// 在画布的指定区域绘制内容
        /// fullRect、dataRect都要求是缩放、平移过后的，在画面上的正确区域(由于来源可能是控件、纸张，因此需要调用处计算好后传进来)
        /// </summary>
        /// <param name="graphicsKey"></param>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        /// <param name="fullRect">页面的完整绘制区域</param>
        /// <param name="dataRect">页面的内容区域（去除页边距）</param>
        /// <param name="reportData"></param>
        /// <param name="area"></param>
        public static void PrintAreaToGraphics(Graphics g, float scale, RectangleF fullRect, RectangleF dataRect,
            I3ReportDatas reportDatas, I3PrintArea area, bool paintPageIndex, int index)
        {
            //求剪切区域
            RectangleF clipRect = fullRect;
            clipRect.Intersect(g.ClipBounds);
            RectangleF areaRect = CalAreaDrawRect_Scale(reportDatas, area, dataRect, scale);  //先准备好避免重复计算
            clipRect.Intersect(areaRect);

            //画整个背景
            g.FillRectangle(Brushes.White, fullRect);

            if (clipRect.IsEmpty)
            {
                PaintPageIndex(g, scale, fullRect, dataRect, reportDatas, area, paintPageIndex, index);
                PaintPageHeader(g, scale, fullRect, dataRect, reportDatas, area, paintPageIndex, index);
                return;
            }

            #region 先画背景和内容
            foreach (int row in area.AllRows)
            {
                #region 行测试
                //2017.04.26 为加快绘制速度，合并单元格不单独处理，以绘制合并首格为准
                //因此需要去掉行、列测试，因为有可能只需要重绘合并单元格，但合并首格不丰重绘区域内
                //if (reportData.Rows[row].Type == I3RowColType.None)
                //{
                //    continue;
                //}
                //I3ReportCell testCell = reportData.GetCellItem(row, 0);
                //RectangleF testRect = CalCellClipRect_Scale(reportData, testCell, area, scale, dataRect, fullRect, areaRect);
                //if (testRect.Bottom < clipRect.Top || testRect.Top > clipRect.Bottom)
                //{
                //    continue;
                //}
                #endregion


                foreach (int col in area.AllCols)
                {
                    #region 列测试
                    //if (reportData.Cols[col].Type == I3RowColType.None)
                    //{
                    //    continue;
                    //}
                    //testCell = reportData.GetCellItem(0, col);
                    //testRect = CalCellClipRect_Scale(reportData, testCell, area, scale, dataRect, fullRect, areaRect);
                    //if (testRect.Right < clipRect.Left || testRect.Left > clipRect.Right)
                    //{
                    //    continue;
                    //}
                    #endregion


                    I3ReportCell cell = area.ReportData.GetCellItem(row, col);
                    //2017.04.26 为加快绘制速度，合并单元格不单独处理
                    if (cell.MergState != I3MergeState.Merged)
                    {
                        //内容
                        g.SetClip(clipRect);
                        DrawCell(g, fullRect, dataRect, areaRect, scale, reportDatas, cell, area, null, true, false, true);

                        //边框  //在这里画边框，缩放时有的边框线会显示不出来
                        //g.SetClip(clipRect);
                        //DrawCell(g, fullRect, dataRect, areaRect, scale, reportDatas, cell, area, null, false, true, false);
                    }
                }
            }
            #endregion

            #region 画边框  //在这里画边框，合并单元格跨页时，会将第2页的线条多画在第1页的末尾，因此必须拆分一下合并单元格
            foreach (int row in area.AllRows)
            {
                foreach (int col in area.AllCols)
                {
                    I3ReportCell cell = area.ReportData.GetCellItem(row, col);
                    //2017.04.26 为加快绘制速度，合并单元格不单独处理
                    if (cell.MergState != I3MergeState.Merged)
                    {
                        //边框
                        g.SetClip(clipRect);
                        DrawCell(g, fullRect, dataRect, areaRect, scale, reportDatas, cell, area, null, false, true, false);
                    }
                }
            }
            #endregion


            #region 页码、页眉
            PaintPageIndex(g, scale, fullRect, dataRect, reportDatas, area, paintPageIndex, index);
            PaintPageHeader(g, scale, fullRect, dataRect, reportDatas, area, paintPageIndex, index);
            #endregion


            #region 111
            //2017.04.26 为加快绘制速度，将画边框放到上面的代码中，减少行、列测试
            //再画边框
            //foreach (int row in area.AllRows)
            //{
            //    #region 行测试
            //    if (reportData.Rows[row].Type == I3RowColType.None)
            //    {
            //        continue;
            //    }
            //    I3ReportCell testCell = reportData.GetCellItem(row, 0);
            //    RectangleF testRect = CalCellClipRect_Scale(reportData, testCell, area, scale, dataRect, fullRect, areaRect);
            //    if (testRect.Bottom < clipRect.Top || testRect.Top > clipRect.Bottom)
            //    {
            //        continue;
            //    }
            //    #endregion

            //    foreach (int col in area.AllCols)
            //    {
            //        #region 列测试
            //        if (reportData.Cols[col].Type == I3RowColType.None)
            //        {
            //            continue;
            //        }
            //        testCell = reportData.GetCellItem(0, col);
            //        testRect = CalCellClipRect_Scale(reportData, testCell, area, scale, dataRect, fullRect, areaRect);
            //        if (testRect.Right < clipRect.Left || testRect.Left > clipRect.Right)
            //        {
            //            continue;
            //        }
            //        #endregion

            //        I3ReportCell cell = reportData.GetCellItem(row, col);
            //        g.SetClip(clipRect);
            //        DrawCell(g, fullRect, dataRect, areaRect, scale, reportData, cell, area, null, false, true, false);
            //    }
            //}
            #endregion
        }

        /// <summary>
        /// 打印页码
        /// </summary>
        private static void PaintPageIndex(Graphics g, float scale, RectangleF fullRect, RectangleF dataRect,
            I3ReportDatas reportDatas, I3PrintArea area, bool paintPageIndex, int index)
        {
            if (!paintPageIndex)
            {
                return;
            }

            int currentIndex = index + 1 + reportDatas.PageIndexStart - 1;
            if (currentIndex > 0)//当前的页码必须大于0才打印
            {
                string text = string.Format("第{0}页/共{1}页", currentIndex, reportDatas.TotalPageCount);
                g.SetClip(fullRect);
                float fontSize = 13 * scale;
                using (Font font = new Font("宋体", fontSize))
                {
                    StringFormat stringFormat = StringFormat.GenericDefault;
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    stringFormat.Trimming = StringTrimming.None;
                    stringFormat.FormatFlags = (StringFormatFlags)0;
                    SizeF sizeF = g.MeasureString(text, font, 5000, stringFormat);
                    RectangleF textRect = new RectangleF(0, 0, sizeF.Width + 2, sizeF.Height + 2);
                    textRect.Y = fullRect.Y + 20 * scale;
                    textRect.X = fullRect.X + fullRect.Width - 50 * scale - textRect.Width;
                    g.SetClip(textRect);
                    g.DrawString(text, font, Brushes.Black, textRect.X + 1, textRect.Y + 1);
                }
            }
        }

        private const string NewLineFlag = "{r}";

        /// <summary>
        /// 打印页眉
        /// </summary>
        private static void PaintPageHeader(Graphics g, float scale, RectangleF fullRect, RectangleF dataRect,
            I3ReportDatas reportDatas, I3PrintArea area, bool paintPageIndex, int index)
        {
            //test
            //area.ReportData.PageHeader = I3PageHeader.Default;
            //area.ReportData.PageHeader.Text = "报表打印工具报表打印工具报表打印工具{r}报表打印工具报表打印111报表打印工具报表打印工具报表打印工具报表打印工具报表打印222报表打印工具报表打印工具报表打印工具报表打印工具报表打印333报表打印工具报表打印工具报表打印工具报表打印工具报表打印444报表打印工具报表打印工具报表打印工具报表打印工具报表打印555";

            if (area.ReportData.PageHeader == null || string.IsNullOrEmpty(area.ReportData.PageHeader.Text))
            {
                return;
            }

            I3PageHeader ph = area.ReportData.PageHeader;
            Font font = new Font(ph.FontName, ph.FontSize * scale);
            Brush brush = new SolidBrush(ph.BrushColor);
            string[] strs = ph.Text.Split(new string[] { NewLineFlag }, StringSplitOptions.None);
            if (strs.Length > 1)//多于2行时强制关闭自动换行功能，由代码来进行控制
            {
                ph.StringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
            }


            //毫米转换为像素公式是 y = y * 96 / 25.39999918
            float marginLeft = ph.MarginLeft * scale;
            float marginTop = ph.MarginTop * scale;
            float marginRight = ph.MarginRight * scale;
            RectangleF rect = fullRect;
            rect.X += marginLeft;
            rect.Width -= marginLeft;
            rect.Y += marginTop;
            rect.Height -= marginTop;
            rect.Width -= marginRight;
            g.SetClip(fullRect);

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
                    SizeF sizeF = g.MeasureString(str, font, (int)rect.Width, ph.StringFormat);
                    RectangleF r = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
                    if (ph.StringFormat.LineAlignment == StringAlignment.Center)
                    {
                        r.X += r.Width / 2 - sizeF.Width / 2;
                        r.Width = sizeF.Width;
                    }
                    else if (ph.StringFormat.LineAlignment == StringAlignment.Far)
                    {
                        r.X += r.Width - sizeF.Width;
                        r.Width = sizeF.Width;
                    }
                    else
                    {
                        r.Width = sizeF.Width;
                    }
                    r.Height = sizeF.Height;
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
                        r.Y = startY + stepY * i;
                    }
                    #endregion

                    #region 绘制
                    g.DrawString(str, font, brush, r, ph.StringFormat);
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
            }
            finally
            {
                font.Dispose();
                brush.Dispose();
            }
        }


        /// <summary>
        /// 将单元格输出到画布上
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void DrawCell(Graphics g, RectangleF fullRect, RectangleF dataRect, RectangleF areaRect, float scale, I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area, I3ReportCell mergedCell,
            bool drawBackground, bool drawBorder, bool drawContect)
        {
            #region 合并单元格中的格子处理
            if (cell != null && cell.MergState == I3MergeState.Merged)
            {
                //2017.04.26 为加快绘制速度，合并单元格不处理
                return;
                //I3ReportCell firstCell = reportData.GetMergedStartedCell(cell.Row, cell.Col);
                //if (firstCell != null)
                //{
                //    DrawCell(g, fullRect, dataRect, areaRect, scale, reportData, firstCell, area, cell, drawBackground, drawBorder, drawContect);
                //}
                //return;
            }
            #endregion

            #region 绘制区域、剪切区域
            RectangleF cellDrawRect = CalCellDrawRect_Scale(reportDatas, cell, area, scale, dataRect, mergedCell);
            if (cellDrawRect.IsEmpty)
            {
                return;
            }

            //2017.04.26 为加快绘制速度，不做单元格剪切区域计算，直接使用原始的剪切区域
            //I3ReportCell destCell = mergedCell == null ? cell : mergedCell;  //计算剪切区域使用真实单元格
            //RectangleF clipRect = CalCellClipRect_Scale(reportData, destCell, area, scale, dataRect, fullRect, areaRect);
            ////destCell.SetCellRect(graphicsKey, clipRect);
            //clipRect.Intersect(g.ClipBounds);
            //if (clipRect.IsEmpty)
            //{
            //    return;
            //}

            //g.SetClip(clipRect);
            #endregion

            #region 样式
            I3ReportCellStyle style = area.ReportData.GetCellStyle(cell.StyleName);
            if (style == null)
            {
                return;
            }
            #endregion

            II3CellRenderer renderer = I3CellRendererBuilder.GetRenderer(cell);
            RectangleF oldClip = g.ClipBounds;

            //背景
            if (drawBackground)
            {
                g.SetClip(oldClip);
                g.FillRectangle(Brushes.White, cellDrawRect);
                renderer.DrawBackground(g, scale, area.ReportData, cell, cellDrawRect, style);
            }

            //边框
            if (drawBorder)
            {
                RectangleF borderClipRect = oldClip;  //扩大避免边框画不全
                borderClipRect.Inflate(1, 1);
                g.SetClip(borderClipRect);
                renderer.DrawCellBorder(g, scale, area.ReportData, cell, cellDrawRect, style);
            }

            //内容
            if (drawContect)
            {
                g.SetClip(oldClip);
                renderer.DrawContent(g, scale, area.ReportData, cell, cellDrawRect, style, area, true);
            }
        }

        #endregion

        #region 分页计算

        /// <summary>
        /// 重新计算报表大小和分页信息
        /// </summary>
        /// <param name="reportData"></param>
        public static void RePaging(I3ReportData reportData)
        {
            ReCalCellsSize(reportData, false);
            ReCalCellsSize(reportData, true);   //重新计算一次，有的单元格被其他行列影响调大了，缩小的字体要重新计算
            reportData.SetPrintAreas(CalPrintAreas(reportData));
        }

        /// <summary>
        /// 重置合并单元格位于多页的情况（分解成多个格子）  注意，只针对数据区的内容
        /// 2018.05.30暂时只考虑纵向分解的情况
        /// </summary>
        /// <param name="reportData"></param>
        public static void ReSetMergeCellsInDifPage(I3ReportData reportData)
        {
            foreach (I3PrintArea area in reportData.PrintAreas.Dic.Values)
            {
                area.UpdateMinAndMaxDataRowIndex(reportData);
            }

            I3MergeRange[] merges = reportData.MergeRanges;
            List<I3MergeRange> list = new List<I3MergeRange>();
            foreach (I3MergeRange merge in merges)
            {
                I3MergeRange[] arr = splitMemrgeRangeInDifPage(merge, reportData);
                list.AddRange(arr);
            }
            reportData.MergeRanges = list.ToArray();
        }

        /// <summary>
        /// 拆分单个合并单元格
        /// </summary>
        /// <param name="m"></param>
        /// <param name="reportData"></param>
        /// <returns></returns>
        private static I3MergeRange[] splitMemrgeRangeInDifPage(I3MergeRange m, I3ReportData reportData)
        {
            List<I3MergeRange> list = new List<I3MergeRange>();

            I3PrintArea startArea = getPrintAreaByRowIndex(m.StartRow, reportData);
            I3PrintArea endArea = getPrintAreaByRowIndex(m.EndRow, reportData);

            //不在数据区，不做处理
            if (startArea == null || endArea == null)
            {
                list.Add(m);
                return list.ToArray();
            }

            //在同一页，不做处理
            if (startArea.Index == endArea.Index)
            {
                list.Add(m);
                return list.ToArray();
            }

            //开始拆分
            I3MergeRange m1 = new I3MergeRange(m.StartRow, m.StartCol, startArea.MaxDataAreaRowIndex, m.EndCol);
            if (m1.EndRow > m1.StartRow || m1.EndCol > m1.StartCol)//判断是否是一个有效的合并单元格
            {
                list.Add(m1);
            }
            I3MergeRange m2 = new I3MergeRange(endArea.MinDataAreaRowIndex, m.StartCol, m.EndRow, m.EndCol);
            //m2的样式同m
            I3ReportCell mCell = reportData.Rows[m.StartRow][m.StartCol];
            I3ReportCell m2Cell = reportData.Rows[m2.StartRow][m2.StartCol];
            m2Cell.MergState = I3MergeState.FirstCell;
            m2Cell.StyleName = mCell.StyleName;
            if (!string.IsNullOrEmpty(mCell.Text))
            {
                m2Cell.Text = "...";
            }
            if (m2.EndRow - m2.StartRow > 0)//后面的部分超过了一行
            {
                I3MergeRange[] newArr = splitMemrgeRangeInDifPage(m2, reportData);//继续拆分
                list.AddRange(newArr);
            }
            else//后面的部分只有一行
            {
                if (m2.EndCol > m2.StartCol)//存在列的合并
                {
                    list.Add(m2);
                }
            }

            return list.ToArray();
        }

        private static I3PrintArea getPrintAreaByRowIndex(int rowIndex, I3ReportData reportData)
        {
            foreach (I3PrintArea area in reportData.PrintAreas.Dic.Values)
            {
                if (area.DataRows.ContainsKey(rowIndex))
                {
                    return area;
                }
            }

            return null;
        }

        /// <summary>
        /// 按纸张尺寸分页
        /// </summary>
        /// <param name="reportData"></param>
        private static I3PrintAreas CalPrintAreas(I3ReportData reportData)
        {
            I3PrintAreas rowPringAreas = CalPrintAreasByRows(reportData);
            I3PrintAreas printAreas = CalPrintAreasByCols(reportData, rowPringAreas);
            return printAreas;
        }

        private static I3PrintAreas CalPrintAreasByRows(I3ReportData reportData)
        {
            I3PageSetting setting = reportData.PageSetting;
            float paperHeight = setting.PaperContentRect.Height;
            I3PrintArea defaultArea = GetDefaultRowPrintArea(reportData);

            //先按行划分
            I3PrintAreas rowPrintAreas = new I3PrintAreas();
            float totalHeight = defaultArea.Height;
            List<int> rows = new List<int>();
            for (int i = 0; i < reportData.RowCount; i++)
            {
                if (reportData[i].Type != I3RowColType.数据 || reportData[i].Type == I3RowColType.None)
                {
                    continue;
                }
                int rowHeight = reportData[i].Height;

                #region 在此行前分页
                //已有行被添加，前面一行的行后分页属性=true，已有行的总高度>0
                bool breakBeforeThisRow = rows.Count > 0 && reportData[rows[rows.Count - 1]].PageBreak && totalHeight > defaultArea.Height;
                //尺寸超过  条件：加上当前行后超过
                breakBeforeThisRow = breakBeforeThisRow || (setting.RowsPagerStyle == PagerStyle.按纸张尺寸分页 && totalHeight + rowHeight > paperHeight);
                //行数超过
                breakBeforeThisRow = breakBeforeThisRow || (setting.RowsPagerStyle == PagerStyle.按数据行列数分页 && rows.Count >= setting.RowsPerPage);

                if (breakBeforeThisRow)
                {
                    I3PrintArea area = defaultArea.Clone().AddRows(rows);
                    area.Height = totalHeight;
                    rowPrintAreas.Add(rowPrintAreas.Dic.Count, area);
                    totalHeight = defaultArea.Height + rowHeight;
                    rows.Clear();
                    rows.Add(i);  //不能用i--，如果某行大于整页宽度，会死循环
                    continue;
                }
                #endregion

                //不分页
                totalHeight += rowHeight;
                rows.Add(i);
            }

            //最后有些行未能分页
            if (rows.Count > 0)
            {
                I3PrintArea area = defaultArea.Clone().AddRows(rows);
                area.Height = totalHeight;
                rowPrintAreas.Add(rowPrintAreas.Dic.Count, area);
            }

            return rowPrintAreas;
        }

        /// <summary>
        /// 获取默认打印区域信息（包含标题行、表头、表尾、页眉、页脚）
        /// </summary>
        /// <param name="area"></param>
        /// <param name="reportData"></param>
        private static I3PrintArea GetDefaultRowPrintArea(I3ReportData reportData)
        {
            I3PrintArea area = new I3PrintArea();

            #region 行
            for (int i = 0; i < reportData.RowCount; i++)
            {
                switch (reportData[i].Type)
                {
                    case I3RowColType.页眉:
                        area.HeaderRows.Add(i, i);
                        break;
                    case I3RowColType.页脚:
                        area.FooterRows.Add(i, i);
                        break;
                    case I3RowColType.标题:
                    case I3RowColType.表头:
                    case I3RowColType.表尾:
                        area.DataRows.Add(i, i);
                        area.Height += reportData[i].Height;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 列
            for (int i = 0; i < reportData.ColCount; i++)
            {
                switch (reportData.Cols[i].Type)
                {
                    case I3RowColType.页眉:
                        area.HeaderCols.Add(i, i);
                        break;
                    case I3RowColType.页脚:
                        area.FooterCols.Add(i, i);
                        break;
                    case I3RowColType.标题:
                    case I3RowColType.表头:
                    case I3RowColType.表尾:
                        area.DataCols.Add(i, i);
                        area.Width += reportData.Cols[i].Width;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 检查
            I3PageSetting setting = reportData.PageSetting;
            if (setting.RowsPagerStyle == PagerStyle.按纸张尺寸分页)
            {
                if (area.Width > setting.PaperContentRect.Width)
                {
                    throw new Exception("标题列、表头列、表尾列宽度之和大于纸张可用区域，无法分页！");
                }
                if (area.Height > setting.PaperContentRect.Height)
                {
                    throw new Exception("标题行、表头行、表尾行高度之和大于纸张可用区域，无法分页！");
                }
            }
            #endregion

            return area;
        }

        private static I3PrintAreas CalPrintAreasByCols(I3ReportData reportData, I3PrintAreas rowAreas)
        {
            I3PageSetting setting = reportData.PageSetting;
            float paperWidth = setting.PaperContentRect.Width;

            I3PrintAreas printAreas = new I3PrintAreas();
            foreach (int row in rowAreas.Dic.Keys)
            {
                I3PrintArea rowArea = rowAreas.Dic[row];
                float totalWidth = rowArea.Width;
                List<int> cols = new List<int>();
                for (int j = 0; j < reportData.ColCount; j++)
                {
                    if (reportData.Cols[j].Type != I3RowColType.数据 || reportData.Cols[j].Type == I3RowColType.None)
                    {
                        continue;
                    }
                    int colWidth = reportData.Cols[j].Width;

                    #region 在此列前分页
                    //列后分页,条件：已有列被添加，前面一列的列后分页属性=true，已有列的总宽度>0
                    bool breakBeforeThisCol = cols.Count > 0 && reportData.Cols[cols[cols.Count - 1]].PageBreak && totalWidth > rowArea.Width;
                    //尺寸超过
                    breakBeforeThisCol = breakBeforeThisCol || (setting.ColsPagerStyle == PagerStyle.按纸张尺寸分页 && totalWidth + colWidth > paperWidth);
                    //列数超过
                    breakBeforeThisCol = breakBeforeThisCol || (setting.ColsPagerStyle == PagerStyle.按数据行列数分页 && cols.Count >= setting.ColsPerPage);

                    if (breakBeforeThisCol)
                    {
                        I3PrintArea area = rowArea.Clone().AddCols(cols);
                        area.Width = totalWidth;
                        printAreas.Add(printAreas.Dic.Count, area);
                        totalWidth = rowArea.Width + colWidth;
                        cols.Clear();
                        cols.Add(j);
                        continue;
                    }
                    #endregion

                    //不分页
                    totalWidth += colWidth;
                    cols.Add(j);
                }

                //最后有些列未能分页
                if (cols.Count > 0)
                {
                    I3PrintArea area = rowArea.Clone().AddCols(cols);
                    area.Height = totalWidth;
                    printAreas.Add(printAreas.Dic.Count, area);
                }
            }
            return printAreas;
        }

        /// <summary>
        /// 重新计算大小和分页信息
        /// prepareNarrow:第一次传false，不计算缩小的字体、图像大小，第2次传ture，才进行计算（考虑后面有调大单元格的情况）
        /// </summary>
        private static void ReCalCellsSize(I3ReportData reportData, bool needCalFontSize)
        {
            for (int i = 0; i < reportData.RowCount; i++)
            {
                for (int j = 0; j < reportData.ColCount; j++)
                {
                    ReCalCellSize(reportData, i, j, needCalFontSize);
                }
            }
        }

        /// <summary>
        /// 重新计算单元格的大小，借以调整行、列的大小
        /// prepareNarrow:是否处理内容缩放
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void ReCalCellSize(I3ReportData reportData, int row, int col, bool prepareNarrow)
        {
            #region 获取单元格、样式、合并区域对象
            I3ReportCell cell = reportData.GetCellItem(row, col);
            if (cell == null || cell.MergState == I3MergeState.Merged)  //单元格为空，或者是被合并的，不需要重新计算
            {
                return;
            }
            I3ReportCellStyle style = reportData.GetCellStyle(cell.StyleName); //没有样式设置，不用重新计算
            if (style == null)
            {
                return;
            }
            I3MergeRange range = cell.MergState == I3MergeState.FirstCell ? reportData.GetMergeRange(row, col) : null;
            if (range == null)
            {
                range = new I3MergeRange((short)row, (short)col, (short)row, (short)col);
            }
            #endregion

            #region 得到默认宽度、高度
            int width = 0;
            int height = 0;
            for (int i = range.StartRow; i <= range.EndRow; i++)
            {
                height += reportData[i].Height;
            }
            for (int i = range.StartCol; i <= range.EndCol; i++)
            {
                width += reportData.Cols[i].Width;
            }
            if (width == 0 || height == 0)
            {
                return;
            }
            #endregion


            II3CellRenderer renderer = I3CellRendererBuilder.GetRenderer(reportData[row][col]);
            SizeF needSize = renderer.CalCellNeedSize(width, height, style, cell);
            if (needSize != SizeF.Empty)
            {
                renderer.AdjustCellSize(width, height, needSize, style, cell, range, reportData, prepareNarrow);
            }
        }

        #endregion

        #region 绘制区域、剪切区域计算

        /// <summary>
        /// 计算输出区域   (以内容输出区域左上角为0，0开始计算、不考虑缩放、页眉页脚平移）
        /// 如果是合并单元格，包含整个合并区域
        /// 必须经过平移、缩放放才有意义
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="reportData"></param>
        /// <param name="cell"></param>
        private static RectangleF CalCellDrawRect_UnScale(I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area, I3ReportCell mergedCell)
        {
            //X、Y先求原单元格的
            int row = mergedCell == null ? cell.Row : mergedCell.Row;
            int col = mergedCell == null ? cell.Col : mergedCell.Col;
            I3RowColType rowType = area.ReportData[row].Type;
            I3RowColType colType = area.ReportData.Cols[col].Type;

            //Width、Heigth求合并的
            I3MergeRange range = cell.GetRange_Mode2(area.ReportData);
            RectangleF rect = new RectangleF(0F, 0F, 0F, 0F);

            #region Y
            IList<int> rows = null;
            switch (rowType)
            {
                case I3RowColType.页眉:
                    rows = area.HeaderRows.Keys;
                    break;
                case I3RowColType.页脚:
                    rows = area.FooterRows.Keys;
                    break;
                default:
                    rows = area.DataRows.Keys;
                    break;
            }
            foreach (int i in rows)
            {
                if (i < row)
                {
                    rect.Y += area.ReportData[i].Height;
                }
            }
            #endregion

            #region Height
            for (int i = range.StartRow; i <= range.EndRow; i++)  //高度为合并区域的高度的和
            {
                rect.Height += area.ReportData[i].Height;
            }
            #endregion

            #region X
            IList<int> cols = null;
            switch (colType)
            {
                case I3RowColType.页眉:
                    cols = area.HeaderCols.Keys;
                    break;
                case I3RowColType.页脚:
                    cols = area.FooterCols.Keys;
                    break;
                default:
                    cols = area.DataCols.Keys;
                    break;
            }
            foreach (int i in cols)
            {
                if (i < col)
                {
                    rect.X += area.ReportData.Cols[i].Width;
                }
            }
            #endregion

            #region Width
            for (int i = range.StartCol; i <= range.EndCol; i++)
            {
                rect.Width += area.ReportData.Cols[i].Width;
            }
            #endregion

            #region 画合并单元格的部分时，左移、上移
            if (mergedCell != null)
            {
                for (int i = cell.Row; i < mergedCell.Row; i++)
                {
                    rect.Y -= area.ReportData[i].Height;
                }
                for (int i = cell.Col; i < mergedCell.Col; i++)
                {
                    rect.X -= area.ReportData.Cols[i].Width;
                }
            }
            #endregion

            return rect;
        }

        /// <summary>
        /// 计算剪切区域
        /// 如果是合并单元格，不包含整个合并区域，只包含单元格本身
        /// 必须经过平移、缩放放才有意义
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="cell"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private static RectangleF CalCellClipRect_UnScale(I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area)
        {
            int row = cell.Row;
            int col = cell.Col;
            I3RowColType rowType = area.ReportData[row].Type;
            I3RowColType colType = area.ReportData.Cols[col].Type;

            RectangleF rect = new RectangleF(0F, 0F, 0F, 0F);

            #region Y
            IList<int> rows = null;
            switch (rowType)
            {
                case I3RowColType.页眉:
                    rows = area.HeaderRows.Keys;
                    break;
                case I3RowColType.页脚:
                    rows = area.FooterRows.Keys;
                    break;
                default:
                    rows = area.DataRows.Keys;
                    break;
            }
            foreach (int i in rows)
            {
                if (i < row)
                {
                    rect.Y += area.ReportData[i].Height;
                }
            }
            #endregion

            rect.Height = area.ReportData[cell.Row].Height;

            #region X
            IList<int> cols = null;
            switch (colType)
            {
                case I3RowColType.页眉:
                    cols = area.HeaderCols.Keys;
                    break;
                case I3RowColType.页脚:
                    cols = area.FooterCols.Keys;
                    break;
                default:
                    cols = area.DataCols.Keys;
                    break;
            }
            foreach (int i in cols)
            {
                if (i < col)
                {
                    rect.X += area.ReportData.Cols[i].Width;
                }
            }
            #endregion

            rect.Width = area.ReportData.Cols[cell.Col].Width;

            return rect;
        }

        /// <summary>
        /// 计算输出区域   (以内容输出区域左上角为0，0开始计算)
        /// 如果是合并单元格，包含整个合并区域
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="reportData"></param>
        /// <param name="cell"></param>
        public static RectangleF CalCellDrawRect_Scale(I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area, float scale, RectangleF dataRect, I3ReportCell mergedCell)
        {
            RectangleF rect = CalCellDrawRect_UnScale(reportDatas, cell, area, mergedCell);

            I3ReportCell destCell = mergedCell == null ? cell : mergedCell;  //移动位置时使用真实单元格
            rect = ScaleAndMoveCellRect(rect, reportDatas, destCell, area, scale, dataRect);
            return rect;
        }

        private static RectangleF ScaleAndMoveCellRect(RectangleF rect, I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area, float scale, RectangleF dataRect)
        {
            #region 缩放
            rect.X *= scale;
            rect.Y *= scale;
            rect.Width *= scale;
            rect.Height *= scale;
            #endregion

            #region 将Y位置根据行的类型移动
            float headerHeight = 0;
            foreach (int i in area.HeaderRows.Keys)
            {
                headerHeight += area.ReportData[i].Height;
            }
            switch (area.ReportData[cell.Row].Type)
            {
                case I3RowColType.页眉:
                    rect.Y += dataRect.Y - headerHeight;
                    break;
                case I3RowColType.页脚:
                    rect.Y += dataRect.Y + dataRect.Height;
                    break;
                default:
                    rect.Y += dataRect.Y;
                    break;
            }
            #endregion

            #region 将X位置根据列的类型移动
            float headerWidth = 0;
            foreach (int i in area.HeaderCols.Keys)
            {
                headerWidth += area.ReportData.Cols[i].Width;
            }
            switch (area.ReportData.Cols[cell.Col].Type)
            {
                case I3RowColType.页眉:
                    rect.X += dataRect.X - headerWidth;
                    break;
                case I3RowColType.页脚:
                    rect.X += dataRect.X + dataRect.Width;
                    break;
                default:
                    rect.X += dataRect.X;
                    break;
            }
            #endregion

            return rect;
        }

        /// <summary>
        /// 计算单元格的剪切区域
        /// 如果是合并单元格，不包含合并区域的其他格子
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="cell"></param>
        /// <param name="area"></param>
        /// <param name="scale"></param>
        /// <param name="dataRect"></param>
        /// <param name="mergedCell"></param>
        /// <returns></returns>
        private static RectangleF CalCellClipRect_Scale(I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area, float scale, RectangleF dataRect, RectangleF fullRect, RectangleF areaRect)
        {
            RectangleF rect = CalCellClipRect_UnScale(reportDatas, cell, area);

            rect = ScaleAndMoveCellRect(rect, reportDatas, cell, area, scale, dataRect);
            RectangleF clipRect = CalCellClipRect_Large(area, cell, reportDatas, fullRect, dataRect, areaRect);
            rect.Intersect(clipRect);

            return rect;
        }


        /// <summary>
        /// 计算单元格的剪切区域
        /// 如果是合并单元格，不包含合并区域的其他格子
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="cell"></param>
        /// <param name="area"></param>
        /// <param name="scale"></param>
        /// <param name="dataRect"></param>
        /// <param name="mergedCell"></param>
        /// <returns></returns>
        public static RectangleF CalCellClipRect_Scale(I3ReportDatas reportDatas, I3ReportCell cell, I3PrintArea area, float scale, RectangleF dataRect, RectangleF fullRect)
        {
            RectangleF areaRect = CalAreaDrawRect_Scale(reportDatas, area, dataRect, scale);
            RectangleF rect = CalCellClipRect_UnScale(reportDatas, cell, area);

            rect = ScaleAndMoveCellRect(rect, reportDatas, cell, area, scale, dataRect);
            RectangleF clipRect = CalCellClipRect_Large(area, cell, reportDatas, fullRect, dataRect, areaRect);
            rect.Intersect(clipRect);

            return rect;
        }

        /// <summary>
        /// 计算实际内容页面的绘制区域(经过缩放后)  (可能不会占满内容区域，也可能超过)
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="area"></param>
        /// <param name="dataRect"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private static RectangleF CalAreaDrawRect_Scale(I3ReportDatas reportDatas, I3PrintArea area, RectangleF dataRect, float scale)
        {
            RectangleF rect = new RectangleF(0, 0, 0, 0);
            foreach (int row in area.DataRows.Keys)
            {
                rect.Height += area.ReportData[row].Height;
            }
            foreach (int col in area.DataCols.Keys)
            {
                rect.Width += area.ReportData.Cols[col].Width;
            }


            rect.X *= scale;
            rect.Width *= scale;
            rect.X += dataRect.X;

            rect.Y *= scale;
            rect.Height *= scale;
            rect.Y += dataRect.Y;

            return rect;
        }

        /// <summary>
        /// 将页面分为9个区间，根据行、列类型获取剪切区域
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="reportData"></param>
        /// <param name="fullRect"></param>
        /// <param name="dataRect"></param>
        /// <param name="areaRect"></param>
        /// <returns></returns>
        private static RectangleF CalCellClipRect_Large(I3PrintArea area, I3ReportCell cell, I3ReportDatas reportDatas, RectangleF fullRect, RectangleF dataRect, RectangleF areaRect)
        {
            float x1 = fullRect.X;
            float x2 = dataRect.X;
            float x3 = dataRect.X + dataRect.Width;
            float x4 = fullRect.X + fullRect.Width;
            float y1 = fullRect.Y;
            float y2 = dataRect.Y;
            float y3 = dataRect.Y + dataRect.Height;
            float y4 = fullRect.Y + fullRect.Height;

            RectangleF result;
            switch (area.ReportData[cell.Row].Type)
            {
                case I3RowColType.页眉:
                    switch (area.ReportData.Cols[cell.Col].Type)
                    {
                        case I3RowColType.页眉:  //左上
                            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
                        case I3RowColType.页脚:  //右上
                            return new RectangleF(x3, y1, x4 - x3, y2 - y1);
                        default:  //中上
                            result = new RectangleF(x2, y1, x3 - x2, y2 - y1);
                            result.Width = Math.Min(result.Width, areaRect.Width);
                            return result;
                    }
                case I3RowColType.页脚:
                    switch (area.ReportData.Cols[cell.Col].Type)
                    {
                        case I3RowColType.页眉:  //左下
                            return new RectangleF(x1, y3, x2 - x1, y4 - y3);
                        case I3RowColType.页脚:  //右下
                            return new RectangleF(x3, y3, x4 - x3, y4 - y3);
                        default:  //中下
                            result = new RectangleF(x2, y3, x3 - x2, y4 - y3);
                            result.Width = Math.Min(result.Width, areaRect.Width);
                            return result;
                    }
                default:
                    switch (area.ReportData.Cols[cell.Col].Type)
                    {
                        case I3RowColType.页眉:  //左中
                            result = new RectangleF(x1, y2, x2 - x1, y3 - y2);
                            result.Height = Math.Min(result.Height, areaRect.Height);
                            return result;
                        case I3RowColType.页脚:  //右中
                            result = new RectangleF(x3, y2, x4 - x3, y3 - y2);
                            result.Height = Math.Min(result.Height, areaRect.Height);
                            return result;
                        default:  //中中
                            RectangleF dataClipRect = dataRect;
                            dataClipRect.Intersect(areaRect);
                            return dataClipRect;
                    }
            }
        }

        #endregion
    }
}
