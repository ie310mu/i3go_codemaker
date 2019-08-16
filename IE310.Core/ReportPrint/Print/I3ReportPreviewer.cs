using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using IE310.Core.Utils;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

namespace IE310.Core.ReportPrint
{
    public partial class I3ReportPreviewer : Form
    {
        public I3ReportPreviewer()
        {
            InitializeComponent();

            InitPrinter();
            InitPrintCount();
            InitPageControls();
            InitNudScale();
        }

        public I3ReportPreviewerControl PreviewerControl
        {
            get
            {
                return this.rpc;
            }
        }

        private I3ReportDatas reportDatas;
        private string documentName = "";
        private string formOldText;
        public void Init(I3ReportDatas reportDatas, string documentName)
        {
            this.reportDatas = reportDatas;
            this.documentName = documentName;
            if (string.IsNullOrEmpty(this.formOldText))
            {
                this.formOldText = this.Text;
            }
            this.Text = this.formOldText + "     " + documentName;

            if (reportDatas != null)
            {
                reportDatas.ReCalSizeAndPageInfo();
                startPage.Maximum = reportDatas.PrintAreas.Dic.Count;
                startPage.Value = 1;
                endPage.Maximum = reportDatas.PrintAreas.Dic.Count;
                endPage.Value = reportDatas.PrintAreas.Dic.Count;
                showPage.Maximum = reportDatas.PrintAreas.Dic.Count;
                showPage.Value = 1;
                showPage.Maximum = reportDatas.PrintAreas.Dic.Count;
                lbPage2.Text = "/" + reportDatas.PrintAreas.Dic.Count.ToString() + "页";
            }
        }

        public void Init(I3ReportData reportData, string documentName)
        {
            Init(new I3ReportDatas(reportData), documentName);
        }

        public static void Excute(I3ReportDatas reportDatas, string documentName, Form owner)
        {
            using (I3ReportPreviewer form = new I3ReportPreviewer())
            {
                form.Owner = owner;
                form.Init(reportDatas, documentName);
                form.ShowDialog();
            }
        }



        public static void ExcuteWithThum(I3ReportDatas reportDatas, string documentName, Form owner)
        {
            using (I3ReportPreviewer form = new I3ReportPreviewer())
            {
                form.Owner = owner;
                form.ThumVisible = true;
                form.Init(reportDatas, documentName);
                form.ShowDialog();
            }
        }

        private void tmStart_Tick(object sender, EventArgs e)
        {
            tmStart.Enabled = false;


            RefreshToolstripVisibleState();
            ShowPreviewResult();
        }

        /// <summary>
        /// 显示预览结果
        /// </summary>
        public void ShowPreviewResult()
        {
            if (reportDatas == null)
            {
                return;
            }

            rpc.Init(reportDatas);
            Scale = 100;
            this.rpc.Invalidate();

            if (rpcThum.Visible)
            {
                rpcThum.Init(reportDatas);
                rpcThum.FullWidth();
                this.rpc.Invalidate();
            }
        }

        #region 注入控件
        private void InitPrinter()
        {
            PrinterSettings ps = new PrinterSettings();
            string defaultPrinter = ps.PrinterName;
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cbbPrinter.Items.Add(printer);
                ps.PrinterName = printer;
                if (ps.IsValid && string.Equals(defaultPrinter, printer))
                {
                    cbbPrinter.SelectedIndex = cbbPrinter.Items.Count - 1;
                }
            }
        }

        private NumericUpDown startPage;
        private NumericUpDown endPage;
        private NumericUpDown showPage;
        private NumericUpDown printCount;
        private void InitPageControls()
        {
            startPage = new NumericUpDown();
            startPage.Minimum = 1;
            startPage.Value = 1;
            startPage.Increment = 1;
            ToolStripControlHost host = new ToolStripControlHost(startPage);
            host.AutoSize = false;
            host.Width = 45;
            startPage.Tag = host;
            int index = ts.Items.IndexOf(lbPrintPage);
            ts.Items.Insert(index, host);

            endPage = new NumericUpDown();
            endPage.Minimum = 1;
            endPage.Value = 1;
            endPage.Increment = 1;
            host = new ToolStripControlHost(endPage);
            host.AutoSize = false;
            host.Width = 45;
            endPage.Tag = host;
            index = ts.Items.IndexOf(lbPrintPage) + 1;
            ts.Items.Insert(index, host);

            showPage = new NumericUpDown();
            showPage.Minimum = 1;
            showPage.Value = 1;
            showPage.Increment = 1;
            showPage.ValueChanged += new EventHandler(showPage_ValueChanged);
            host = new ToolStripControlHost(showPage);
            host.AutoSize = false;
            host.Width = 45;
            showPage.Tag = host;
            index = ts.Items.IndexOf(lbPage2);
            ts.Items.Insert(index, host);
        }

        private void InitPrintCount()
        {
            printCount = new NumericUpDown();
            printCount.Minimum = 1;
            printCount.Maximum = 100;
            printCount.Value = 1;
            printCount.Increment = 1;
            ToolStripControlHost host = new ToolStripControlHost(printCount);
            host.AutoSize = false;
            host.Width = 45;
            printCount.Tag = host;
            int index = ts.Items.IndexOf(lbPrintCount);
            ts.Items.Insert(index, host);
        }

        void showPage_ValueChanged(object sender, EventArgs e)
        {
            int curPageIndex = (int)showPage.Value - 1;
            rpc.SetCurPageIndex(curPageIndex);
        }


        private NumericUpDown nudScale;
        private void InitNudScale()
        {
            nudScale = new NumericUpDown();
            nudScale.Maximum = 500;
            nudScale.Minimum = 10;
            nudScale.Value = 100;
            nudScale.Increment = 10;
            nudScale.ValueChanged += new EventHandler(nudScale_ValueChanged);
            ToolStripControlHost host = new ToolStripControlHost(nudScale);
            host.AutoSize = false;
            host.Width = 45;
            nudScale.Tag = host;
            int index = ts.Items.IndexOf(tssScale);
            ts.Items.Insert(index, host);
        }

        void nudScale_ValueChanged(object sender, EventArgs e)
        {
            Scale = (float)nudScale.Value;
        }
        #endregion

        //10--500
        private float scale = 10;
        /// <summary>
        /// 显示比例 10--500
        /// </summary>
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                value = value < 10 ? 10 : value;
                value = value > 500 ? 500 : value;
                if (value.Equals(scale))
                {
                    return;
                }

                scale = value;
                rpc.Scale = Scale / 100;

                if (!nudScale.Value.Equals((decimal)scale))
                {
                    nudScale.Value = (decimal)scale;
                }
            }
        }

        private void FormReportResultPreview_Shown(object sender, EventArgs e)
        {
            tmStart.Enabled = true;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int start = (int)startPage.Value;
                start = start < 1 ? 1 : start;
                start = start > reportDatas.PrintAreas.Dic.Count ? reportDatas.PrintAreas.Dic.Count : start;
                int end = (int)endPage.Value;
                end = end < 1 ? 1 : end;
                end = end > reportDatas.PrintAreas.Dic.Count ? reportDatas.PrintAreas.Dic.Count : end;
                end = end < start ? start : end;
                int count = (int)printCount.Value;
                I3ReportPrintController.PrintToPrinter(cbbPrinter.Text, documentName, reportDatas, start, end, count, rpc.PaintPageIndex2);
            }
            catch (Exception ex)
            {
                I3MessageHelper.ShowError("打印失败，原因：\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 预览控件显示比例改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="scale"></param>
        private void rpc_ScaleChanged(object sender, float scale)
        {
            this.Scale = scale * 100;
        }

        /// <summary>
        /// 整页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFull_Click(object sender, EventArgs e)
        {
            rpc.OriginalScale();
        }

        /// <summary>
        /// 适应页宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFullWidth_Click(object sender, EventArgs e)
        {
            rpc.FullWidth();
        }

        /// <summary>
        /// 适应页高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFullHeight_Click(object sender, EventArgs e)
        {
            rpc.FullHeight();
        }

        /// <summary>
        /// 预览控件当前页改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="curPageIndex"></param>
        private void rpc_CurPageIndexChanged(object sender, int curPageIndex)
        {
            showPage.ValueChanged -= new EventHandler(showPage_ValueChanged);
            showPage.Value = curPageIndex + 1;
            showPage.ValueChanged += new EventHandler(showPage_ValueChanged);
        }

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFirst_Click(object sender, EventArgs e)
        {
            ChangeShowPageValue(1);
        }

        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbLast_Click(object sender, EventArgs e)
        {
            ChangeShowPageValue(showPage.Maximum);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPre_Click(object sender, EventArgs e)
        {
            if (showPage.Value > 1)
            {
                ChangeShowPageValue(showPage.Value - 1);
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNext_Click(object sender, EventArgs e)
        {
            if (showPage.Value < showPage.Maximum)
            {
                ChangeShowPageValue(showPage.Value + 1);
            }
        }

        /// <summary>
        /// 改变当前页控件的值
        /// </summary>
        /// <param name="value"></param>
        private void ChangeShowPageValue(decimal value)
        {
            showPage.ValueChanged -= new EventHandler(showPage_ValueChanged);
            showPage.Value = value;
            showPage.ValueChanged += new EventHandler(showPage_ValueChanged);
            showPage_ValueChanged(null, null);
        }

        private bool toolstripPrinterVisible = true;
        /// <summary>
        /// 打印机选择是否可视
        /// </summary>
        [DisplayName("打印机选择是否可视")]
        public bool ToolstripPrinterVisible
        {
            get
            {
                return toolstripPrinterVisible;
            }
            set
            {
                toolstripPrinterVisible = value;
                cbbPrinter.Visible = value;
                ChecktssPrintVisible();
            }
        }

        private bool toolstripPrintPageVisible = true;
        /// <summary>
        /// 打印页范围是否可视 
        /// </summary>
        [DisplayName("打印页范围是否可视 ")]
        public bool ToolstripPrintPageVisible
        {
            get
            {
                return toolstripPrintPageVisible;
            }
            set
            {
                toolstripPrintPageVisible = value;
                if (startPage != null)
                {
                    (startPage.Tag as ToolStripControlHost).Visible = value;
                }
                if (endPage != null)
                {
                    (endPage.Tag as ToolStripControlHost).Visible = value;
                }
                lbPrintPage.Visible = value;
                lbPrintPage2.Visible = value;
                ChecktssPrintVisible();
            }
        }


        private bool toolstripPrintButtonVisible = true;
        /// <summary>
        /// "打印按钮是否可视"
        /// </summary>
        [DisplayName("打印按钮是否可视")]
        public bool ToolstripPrintButtonVisible
        {
            get
            {
                return toolstripPrintButtonVisible;
            }
            set
            {
                toolstripPrintButtonVisible = value;
                if (printCount != null)
                {
                    (printCount.Tag as ToolStripControlHost).Visible = value;
                }
                cbbPrinter.Visible = value;
                lbPrintPage.Visible = value;
                lbPrintPage2.Visible = value;
                startPage.Visible = value;
                endPage.Visible = value;
                lbPrintCount.Visible = value;
                tsbPrint.Visible = value;
                tsbExcel.Visible = value;
                ChecktssPrintVisible();
            }
        }

        /// <summary>
        /// 检查tssPrint是否应该显示
        /// </summary>
        private void ChecktssPrintVisible()
        {
            tssPrint.Visible = ToolstripPrinterVisible || ToolstripPrintPageVisible || ToolstripPrintButtonVisible;
        }

        private bool toolstripScaleControlVisible = true;
        /// <summary>
        /// "比例调节是否可视"
        /// </summary>
        [DisplayName("比例调节是否可视")]
        public bool ToolstripScaleControlVisible
        {
            get
            {
                return toolstripScaleControlVisible;
            }
            set
            {
                toolstripScaleControlVisible = value;
                tsbFull.Visible = value;
                tsbFullWidth.Visible = value;
                tsbFullHeight.Visible = value;
                lbScale.Visible = value;
                if (nudScale != null)
                {
                    (nudScale.Tag as ToolStripControlHost).Visible = value;
                }
                tssScale.Visible = value;
            }
        }

        private bool toolstripPageControlVisible = true;
        /// <summary>
        /// 页面调节是否可视 
        /// </summary>
        [DisplayName("页面调节是否可视")]
        public bool ToolstripPageControlVisible
        {
            get
            {
                return toolstripPageControlVisible;
            }
            set
            {
                toolstripPageControlVisible = value;
                tsbFirst.Visible = value;
                tsbPre.Visible = value;
                tsbNext.Visible = value;
                tsbLast.Visible = value;
                lbPage.Visible = value;
                if (showPage != null)
                {
                    (showPage.Tag as ToolStripControlHost).Visible = value;
                }
                lbPage2.Visible = value;
                tssPage.Visible = value;
            }
        }

        private bool toolstripCloseButtonVisible = true;
        /// <summary>
        /// 退出按钮是否可视
        /// </summary>
        [DisplayName("退出按钮是否可视")]
        public bool ToolstripCloseButtonVisible
        {
            get
            {
                return toolstripCloseButtonVisible;
            }
            set
            {
                toolstripCloseButtonVisible = value;
                tsbClose.Visible = value;
            }
        }

        private bool thumVisible = false;
        /// <summary>
        /// 是否使用缩略图功能
        /// </summary>
        [DisplayName("是否使用缩略图功能")]
        public bool ThumVisible
        {
            get
            {
                return thumVisible;
            }
            set
            {
                thumVisible = value;
                rpcThum.Visible = value;
            }
        }

        private void RefreshToolstripVisibleState()
        {
            ToolstripPrinterVisible = ToolstripPrinterVisible;
            ToolstripPrintPageVisible = ToolstripPrintPageVisible;
            ToolstripPrintButtonVisible = ToolstripPrintButtonVisible;
            ToolstripScaleControlVisible = ToolstripScaleControlVisible;
            ToolstripPageControlVisible = ToolstripPageControlVisible;
            ToolstripCloseButtonVisible = ToolstripCloseButtonVisible;
        }

        private void rpcThum_PageClicked(object sender, int curPageIndex)
        {
            rpc.SetCurPageIndex(curPageIndex);
        }

        private void rpcThum_SizeChanged(object sender, EventArgs e)
        {
            rpcThum.FullWidth();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void I3ReportPreviewer_Load(object sender, EventArgs e)
        {
            this.btCancel.Width = 0;
        }
        protected string getTmpFileName()
        {
            string tmpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");
            //MessageBox.Show(tmpDir);
            if (!I3DirectoryUtil.CreateDirctory(tmpDir).State)
            {
                throw new Exception("临时目录创建失败");
            }

            try
            {
                I3DirectoryUtil.CheckAndClearDirctory(tmpDir);
            }
            catch
            {
            }

            string tmpFileName = Path.Combine(tmpDir, Guid.NewGuid().ToString() + ".xls");
            return tmpFileName;
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            HSSFWorkbook workbook = null;
            using (MemoryStream ms = new MemoryStream(Resource.template))
            {
                workbook = new HSSFWorkbook(ms); // 工作簿
            }
            Dictionary<string, HSSFCellStyle> styleDic = new Dictionary<string, HSSFCellStyle>();
            Dictionary<string, IFont> fontDic = new Dictionary<string, IFont>();

            //准备sheet
            var sheetIndex = 0;
            foreach (I3ReportData rd in reportDatas.Datas)
            {
                if (sheetIndex > 0)
                {
                    workbook.CloneSheet(0);
                }
                sheetIndex++;
            }

            sheetIndex = 0;
            foreach (I3ReportData rd in reportDatas.Datas)
            {
                string sheetName = string.IsNullOrEmpty(rd.Name) ? ("sheet" + (sheetIndex + 1).ToString()) : rd.Name;
                //ISheet sheet = workbook.CreateSheet(sheetName);
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                workbook.SetSheetName(sheetIndex, sheetName);

                //设置列宽
                var colIndex = -1;
                foreach (I3ReportCol col in rd.Cols)
                {
                    colIndex++;
                    if (col.Width == 0)
                    {
                        sheet.SetColumnHidden(colIndex, true);
                    }
                    else
                    {
                        //int width = (int)Math.Ceiling(col.Width * 34.612159);  //向上取整，与excel中保持一致  //34.612159是从ebiao导入excel处获取的
                        //在列旁边按住鼠标不动，可以显示默认单位与像素的对应关系，10单位对应85像素，即1像素对应10/85单位  (好像与excel默认字体有关)
                        //SetColumnWidth的单位是1/256，因此1像素对应10*256/85=30.117647
                        double dw = (double)col.Width * (double)10 * (double)256 / (double)85;
                        int width = (int)Math.Ceiling(dw);   
                        sheet.SetColumnWidth(colIndex, width);
                    }
                }

                //导出数据
                int rowCount = 0;
                foreach (I3PrintArea area in rd.PrintAreas.Dic.Values)
                {
                    exportAreaToSheet(area, workbook, sheet, ref rowCount, styleDic, fontDic);
                }

                //页面设置  //列宽会有微调导致在excel中左右分页
                sheet.SetMargin(MarginType.RightMargin, rd.PageSetting.PaperRightMarginMM / (float)25.4);
                sheet.SetMargin(MarginType.TopMargin, rd.PageSetting.PaperTopMarginMM / (float)25.4);
                sheet.SetMargin(MarginType.LeftMargin, rd.PageSetting.PaperLeftMarginMM / (float)25.4);
                sheet.SetMargin(MarginType.BottomMargin, rd.PageSetting.PaperBottomMarginMM / (float)25.4);
                sheet.PrintSetup.Landscape = rd.PageSetting.PaperOrientation == PaperOrientation.横向;
                int paperType = rd.PageSetting.GetNPOIPaperType();
                if (paperType == 0)  //自定义，不清楚NPOI中怎样设置，用A4
                {
                    sheet.PrintSetup.PaperSize = 9;
                }
                else if (paperType == -1)  //未打到对应关系
                {
                    sheet.PrintSetup.PaperSize = 9;
                }
                else
                {
                    sheet.PrintSetup.PaperSize = (short)paperType;
                }
                sheet.IsPrintGridlines = true;
                sheet.FitToPage = false;//默认值是true

                //设置锁定 
                sheet.ProtectSheet("{D49C4D85-F46E-456F-9C71-DB7D880B5B04}");
                sheet.IsPrintGridlines = false;  //不设置这个，打印时会打印出空白单元格的网络线（即使没有网格线也会打印）

                sheetIndex++;
            }


            string tmpFileName = getTmpFileName();
            using (FileStream file = new FileStream(tmpFileName, FileMode.Create))
            {
                workbook.Write(file);
                file.Close();
            }
            I3PCUtil.CreateAndWaitProcessByEvent(null, tmpFileName, "", IntPtr.Zero, 0, 0);
        }

        private void exportAreaToSheet(I3PrintArea area, HSSFWorkbook workbook, ISheet sheet, ref int rowCount, Dictionary<string, HSSFCellStyle> styleDic, Dictionary<string, IFont> fontDic)
        {
            HSSFCellStyle emptyStyle = createEmptyStyle(workbook);

            //导出行、单元格
            foreach (int rowIndex in area.AllRows)
            {
                //设置行高
                rowCount++;
                IRow row = sheet.CreateRow(rowCount - 1);
                I3ReportRow rowData = area.ReportData.Rows[rowIndex];
                if (rowData.PageBreak)  //分页符
                {
                    sheet.SetRowBreak(rowCount - 1);
                }
                double dh = (double)rowData.Height * (double)15.305838;
                dh = dh * (double)1.1;
                int height = (int)Math.Ceiling(dh); 
                row.Height = (short)height;

                //设置文本
                int colIndex = -1;
                foreach (I3ReportCell cellData in rowData.Cells)
                {
                    colIndex++;
                    ICell cell = row.CreateCell(colIndex);
                    bool hasReturnInText = false;
                    if (cellData.MergState != I3MergeState.Merged)
                    {
                        hasReturnInText = !string.IsNullOrEmpty(cellData.Text) && cellData.Text.IndexOf("{r}") >= 0;
                        if (cellData is I3ReportImageCell)
                        {
                            I3ReportImageCell imageCell = (I3ReportImageCell)cellData;
                            byte[] bytes = imageCell.ImageData;
                            if (bytes != null && bytes.Length > 0)
                            {
                                try
                                {
                                    int pictureIdx = workbook.AddPicture(bytes, PictureType.JPEG);
                                    HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                                    // 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
                                    int row1 = rowCount - 1;
                                    int row2 = rowCount - 1;
                                    int col1 = colIndex;
                                    int col2 = colIndex;
                                    if (cellData.MergState == I3MergeState.FirstCell)
                                    {
                                        I3MergeRange mr = area.ReportData.GetMergeRange(cellData.Row, cellData.Col);
                                        row2 = rowCount - 1 + mr.EndRow - mr.StartRow;
                                        col2 = mr.EndCol;
                                    }
                                    HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, col1, row1, col2, row2);
                                    //把图片插到相应的位置
                                    HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {
                            string text = string.IsNullOrEmpty(cellData.Text) ? "" : cellData.Text.Replace("{r}", "\r\n");
                            //特殊符号....使用替换符如{1级钢筋}{WL}{屈特比}
                            cell.SetCellValue(text);
                        }
                    }

                    //设置样式 
                    int lastRowIndex = area.AllRows[area.AllRows.Length - 1];
                    bool isLastRow = rowData.Row == lastRowIndex;
                    bool isLastCol = cellData.Col == area.ReportData.Cols.Length - 1;
                    setCellStyle(workbook, sheet, cell, area, cellData, styleDic, fontDic, hasReturnInText, isLastRow, isLastCol, emptyStyle);

                    //设置合并
                    if (cellData.MergState == I3MergeState.FirstCell)
                    {
                        I3MergeRange mr = area.ReportData.GetMergeRange(cellData.Row, cellData.Col);
                        CellRangeAddress ra = new CellRangeAddress(rowCount - 1, rowCount - 1 + mr.EndRow - mr.StartRow, mr.StartCol, mr.EndCol);
                        sheet.AddMergedRegion(ra);
                    }
                }
            }
        }

        private void setCellStyle(HSSFWorkbook workbook, ISheet sheet, ICell cell, I3PrintArea area, I3ReportCell cellData, 
            Dictionary<string, HSSFCellStyle> styleDic, Dictionary<string, IFont> fontDic, bool hasReturnInText, bool isLastRow, bool isLastCol,
            HSSFCellStyle emptyStyle)
        {
            I3ReportCellStyle cs = null;
            if (cellData.MergState == I3MergeState.Merged)
            {
                cellData = area.ReportData.GetMergedStartedCell(cellData.Row, cellData.Col);
            }
            cs = area.ReportData.GetCellStyle(cellData.StyleName);
            if (cs == null)
            {
                cell.CellStyle = emptyStyle;
                return;
            }

            string styleKey = getStyleKey(cellData, cs, hasReturnInText, isLastRow, isLastCol);
            HSSFCellStyle style = null;
            if (styleDic.ContainsKey(styleKey))
            {
                style = styleDic[styleKey];
            }
            else
            {
                style = createStyle(workbook, cellData, cs, fontDic, hasReturnInText, isLastRow, isLastCol);
                styleDic.Add(styleKey, style);
            }

            cell.CellStyle = style;
        }


        private HSSFCellStyle createStyle(HSSFWorkbook workbook, I3ReportCell cellData, I3ReportCellStyle cs, Dictionary<string, IFont> fontDic, bool hasReturnInText, bool isLastRow, bool isLastCol)
        {
            HSSFCellStyle style = (HSSFCellStyle)workbook.CreateCellStyle();

            //设置边框格式
            //top
            if (cs.TopBorder == null || cs.TopBorder.Width <= 0)
            {
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.None;
            }
            else if (cs.TopBorder.Width >= 2)
            {
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;//粗实线
            }
            else
            {
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;//细实线
            }
            //left
            if (cs.LeftBorder == null || cs.LeftBorder.Width <= 0)
            {
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
            }
            else if (cs.LeftBorder.Width >= 2)
            {
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            }
            else
            {
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            }
            //right  //有边框行、列，右、下不用设置
            //if (isLastCol)
            //{
            //    if (cs.RightBorder == null || cs.RightBorder.Width <= 0)
            //    {
            //        style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
            //    }
            //    else if (cs.RightBorder.Width >= 2)
            //    {
            //        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            //    }
            //    else
            //    {
            //        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //    }
            //}
            //bottom
            //if (isLastRow)
            //{
            //    if (cs.BottomBorder == null || cs.BottomBorder.Width <= 0)
            //    {
            //        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.None;
            //    }
            //    else if (cs.BottomBorder.Width >= 2)
            //    {
            //        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
            //    }
            //    else
            //    {
            //        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            //    }
            //}

            //边框颜色
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;

            //居中
            switch (cs.Alignment) //水平
            {
                case StringAlignment.Center:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    break;
                case StringAlignment.Far:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    break;
                default:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    break;
            }
            switch (cs.LineAlignment)  //垂直
            {
                case StringAlignment.Center:
                    style.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case StringAlignment.Far:
                    style.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                default:
                    style.VerticalAlignment = VerticalAlignment.Top;
                    break;
            }

            //字体   //判断了自动缩小
            double fontSize = cs.AdjustSize == I3AdjustSize.缩小内容 && cellData.HasCalFontSize ? (double)cellData.CalFontSize : (double)cs.FontSize;
            var fontHash = string.Format("{0}{1}{2}{3}", cs.FontName, fontSize, cs.FontColor.ToArgb(), cs.FontStyle).GetHashCode().ToString();
            if (fontDic.ContainsKey(fontHash))
            {
                style.SetFont(fontDic[fontHash]);
            }
            else
            {
                IFont font = workbook.CreateFont();
                font.FontName = cs.FontName;
                fontSize = fontSize * (double)10 / (double)13;
                //fontSize = cs.FontSize * (double)72 / (double)96;  
                font.FontHeightInPoints = (short)fontSize;
                style.SetFont(font);
                fontDic.Add(fontHash, font);
            }

            //自动换行  
            style.WrapText = cs.WordWrap || hasReturnInText;

            //锁定
            style.IsLocked = cellData.Lock;

            return style;
        }

        private HSSFCellStyle createEmptyStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle style = (HSSFCellStyle)workbook.CreateCellStyle();

            //设置边框格式
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.None;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.None;

            return style;
        }

        private string getStyleKey(I3ReportCell cellData, I3ReportCellStyle cs, bool hasReturnInText, bool isLastRow, bool isLastCol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(cs.TopBorder == null ? "nullTopBorder" : cs.TopBorder.ToString());
            sb.Append(cs.LeftBorder == null ? "nullLeftBorder" : cs.LeftBorder.ToString());
            sb.Append(cs.RightBorder == null ? "nullRightBorder" : cs.RightBorder.ToString());
            sb.Append(cs.BottomBorder == null ? "nullBottomBorder" : cs.BottomBorder.ToString());
            sb.Append(cs.Alignment);
            sb.Append(cs.LineAlignment);
            double fontSize = cs.AdjustSize == I3AdjustSize.缩小内容 && cellData.HasCalFontSize ? (double)cellData.CalFontSize : (double)cs.FontSize;
            sb.Append(fontSize);
            sb.Append(cs.FontName);
            sb.Append(cs.FontColor);
            sb.Append(cs.FontStyle);
            sb.Append(cs.WordWrap || hasReturnInText);
            sb.Append(isLastRow);
            sb.Append(isLastCol);
            sb.Append(cs.AdjustSize);
            return sb.ToString().GetHashCode().ToString();
        }

    }
}
