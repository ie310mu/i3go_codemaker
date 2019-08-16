
namespace IE310.Core.ReportPrint
{
    partial class I3ReportPreviewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmStart = new System.Windows.Forms.Timer(this.components);
            this.ts = new System.Windows.Forms.ToolStrip();
            this.cbbPrinter = new System.Windows.Forms.ToolStripComboBox();
            this.lbPrintPage = new System.Windows.Forms.ToolStripLabel();
            this.lbPrintPage2 = new System.Windows.Forms.ToolStripLabel();
            this.lbPrintCount = new System.Windows.Forms.ToolStripLabel();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.tssPrint = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFull = new System.Windows.Forms.ToolStripButton();
            this.tsbFullWidth = new System.Windows.Forms.ToolStripButton();
            this.tsbFullHeight = new System.Windows.Forms.ToolStripButton();
            this.lbScale = new System.Windows.Forms.ToolStripLabel();
            this.tssScale = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFirst = new System.Windows.Forms.ToolStripButton();
            this.tsbPre = new System.Windows.Forms.ToolStripButton();
            this.tsbNext = new System.Windows.Forms.ToolStripButton();
            this.tsbLast = new System.Windows.Forms.ToolStripButton();
            this.lbPage = new System.Windows.Forms.ToolStripLabel();
            this.lbPage2 = new System.Windows.Forms.ToolStripLabel();
            this.tssPage = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.btCancel = new System.Windows.Forms.Button();
            this.rpc = new IE310.Core.ReportPrint.I3ReportPreviewerControl();
            this.rpcThum = new IE310.Core.ReportPrint.I3ReportPreviewerControl();
            this.ts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmStart
            // 
            this.tmStart.Tick += new System.EventHandler(this.tmStart_Tick);
            // 
            // ts
            // 
            this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbPrinter,
            this.lbPrintPage,
            this.lbPrintPage2,
            this.lbPrintCount,
            this.tsbPrint,
            this.tsbExcel,
            this.tssPrint,
            this.tsbFull,
            this.tsbFullWidth,
            this.tsbFullHeight,
            this.lbScale,
            this.tssScale,
            this.tsbFirst,
            this.tsbPre,
            this.tsbNext,
            this.tsbLast,
            this.lbPage,
            this.lbPage2,
            this.tssPage,
            this.tsbClose});
            this.ts.Location = new System.Drawing.Point(0, 0);
            this.ts.Name = "ts";
            this.ts.Size = new System.Drawing.Size(979, 50);
            this.ts.TabIndex = 0;
            this.ts.Text = "toolStrip1";
            // 
            // cbbPrinter
            // 
            this.cbbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrinter.Name = "cbbPrinter";
            this.cbbPrinter.Size = new System.Drawing.Size(180, 50);
            // 
            // lbPrintPage
            // 
            this.lbPrintPage.Name = "lbPrintPage";
            this.lbPrintPage.Size = new System.Drawing.Size(17, 47);
            this.lbPrintPage.Text = "~";
            // 
            // lbPrintPage2
            // 
            this.lbPrintPage2.Name = "lbPrintPage2";
            this.lbPrintPage2.Size = new System.Drawing.Size(20, 47);
            this.lbPrintPage2.Text = "页";
            // 
            // lbPrintCount
            // 
            this.lbPrintCount.Name = "lbPrintCount";
            this.lbPrintCount.Size = new System.Drawing.Size(20, 47);
            this.lbPrintCount.Text = "份";
            // 
            // tsbPrint
            // 
            this.tsbPrint.AutoSize = false;
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::IE310.Core.Resource.printer32;
            this.tsbPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(47, 47);
            this.tsbPrint.Text = "打印";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // tsbExcel
            // 
            this.tsbExcel.AutoSize = false;
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::IE310.Core.Resource.excel1;
            this.tsbExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(47, 47);
            this.tsbExcel.Text = "导出excel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // tssPrint
            // 
            this.tssPrint.Name = "tssPrint";
            this.tssPrint.Size = new System.Drawing.Size(6, 50);
            // 
            // tsbFull
            // 
            this.tsbFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFull.Image = global::IE310.Core.Resource.fullpage32;
            this.tsbFull.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFull.Name = "tsbFull";
            this.tsbFull.Size = new System.Drawing.Size(36, 47);
            this.tsbFull.Text = "原始大小";
            this.tsbFull.Click += new System.EventHandler(this.tsbFull_Click);
            // 
            // tsbFullWidth
            // 
            this.tsbFullWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFullWidth.Image = global::IE310.Core.Resource.fullwidth32;
            this.tsbFullWidth.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFullWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullWidth.Name = "tsbFullWidth";
            this.tsbFullWidth.Size = new System.Drawing.Size(36, 47);
            this.tsbFullWidth.Text = "页宽";
            this.tsbFullWidth.Click += new System.EventHandler(this.tsbFullWidth_Click);
            // 
            // tsbFullHeight
            // 
            this.tsbFullHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFullHeight.Image = global::IE310.Core.Resource.fullheight32;
            this.tsbFullHeight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFullHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullHeight.Name = "tsbFullHeight";
            this.tsbFullHeight.Size = new System.Drawing.Size(36, 47);
            this.tsbFullHeight.Text = "页高";
            this.tsbFullHeight.Click += new System.EventHandler(this.tsbFullHeight_Click);
            // 
            // lbScale
            // 
            this.lbScale.Name = "lbScale";
            this.lbScale.Size = new System.Drawing.Size(54, 47);
            this.lbScale.Text = "比例(%):";
            // 
            // tssScale
            // 
            this.tssScale.Name = "tssScale";
            this.tssScale.Size = new System.Drawing.Size(6, 50);
            // 
            // tsbFirst
            // 
            this.tsbFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFirst.Image = global::IE310.Core.Resource.firstpage32;
            this.tsbFirst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFirst.Name = "tsbFirst";
            this.tsbFirst.Size = new System.Drawing.Size(36, 47);
            this.tsbFirst.Text = "第一页";
            this.tsbFirst.Click += new System.EventHandler(this.tsbFirst_Click);
            // 
            // tsbPre
            // 
            this.tsbPre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPre.Image = global::IE310.Core.Resource.prepage32;
            this.tsbPre.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPre.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPre.Name = "tsbPre";
            this.tsbPre.Size = new System.Drawing.Size(36, 47);
            this.tsbPre.Text = "上一页";
            this.tsbPre.Click += new System.EventHandler(this.tsbPre_Click);
            // 
            // tsbNext
            // 
            this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNext.Image = global::IE310.Core.Resource.next32;
            this.tsbNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new System.Drawing.Size(36, 47);
            this.tsbNext.Text = "下一页";
            this.tsbNext.Click += new System.EventHandler(this.tsbNext_Click);
            // 
            // tsbLast
            // 
            this.tsbLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLast.Image = global::IE310.Core.Resource.lastpage32;
            this.tsbLast.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLast.Name = "tsbLast";
            this.tsbLast.Size = new System.Drawing.Size(36, 47);
            this.tsbLast.Text = "最后一页";
            this.tsbLast.Click += new System.EventHandler(this.tsbLast_Click);
            // 
            // lbPage
            // 
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(20, 47);
            this.lbPage.Text = "第";
            // 
            // lbPage2
            // 
            this.lbPage2.Name = "lbPage2";
            this.lbPage2.Size = new System.Drawing.Size(32, 47);
            this.lbPage2.Text = "/7页";
            // 
            // tssPage
            // 
            this.tssPage.Name = "tssPage";
            this.tssPage.Size = new System.Drawing.Size(6, 50);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::IE310.Core.Resource.close32;
            this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(36, 47);
            this.tsbClose.Text = "退出";
            this.tsbClose.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(833, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // rpc
            // 
            this.rpc.AlignTop = false;
            this.rpc.BackColor = System.Drawing.Color.Gray;
            this.rpc.CellItemEventMode = IE310.Core.ReportPrint.I3CellItemEventMode.None;
            this.rpc.ChangeScaleByMouseWheel = true;
            this.rpc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpc.FoucesedCell = null;
            this.rpc.HighlightFoucesedCell = false;
            this.rpc.HighlightMouseOnCell = false;
            this.rpc.Location = new System.Drawing.Point(150, 50);
            this.rpc.MouseOnCell = null;
            this.rpc.Name = "rpc";
            this.rpc.PageInterval = 10F;
            this.rpc.PaintPageIndex = false;
            this.rpc.PaintPageIndex2 = false;
            this.rpc.Scale = 5F;
            this.rpc.Size = new System.Drawing.Size(829, 619);
            this.rpc.SmallChange = 20;
            this.rpc.TabIndex = 1;
            this.rpc.ScaleChanged += new IE310.Core.ReportPrint.ScaleChanged(this.rpc_ScaleChanged);
            this.rpc.CurPageIndexChanged += new IE310.Core.ReportPrint.CurPageIndexChanged(this.rpc_CurPageIndexChanged);
            // 
            // rpcThum
            // 
            this.rpcThum.AlignTop = true;
            this.rpcThum.BackColor = System.Drawing.Color.LightSteelBlue;
            this.rpcThum.CellItemEventMode = IE310.Core.ReportPrint.I3CellItemEventMode.None;
            this.rpcThum.ChangeScaleByMouseWheel = false;
            this.rpcThum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rpcThum.Dock = System.Windows.Forms.DockStyle.Left;
            this.rpcThum.FoucesedCell = null;
            this.rpcThum.HighlightFoucesedCell = false;
            this.rpcThum.HighlightMouseOnCell = false;
            this.rpcThum.Location = new System.Drawing.Point(0, 50);
            this.rpcThum.MouseOnCell = null;
            this.rpcThum.Name = "rpcThum";
            this.rpcThum.PageInterval = 5F;
            this.rpcThum.PaintPageIndex = true;
            this.rpcThum.PaintPageIndex2 = false;
            this.rpcThum.Scale = 0.1F;
            this.rpcThum.Size = new System.Drawing.Size(150, 619);
            this.rpcThum.SmallChange = 20;
            this.rpcThum.TabIndex = 0;
            this.rpcThum.Visible = false;
            this.rpcThum.PageClicked += new IE310.Core.ReportPrint.CurPageIndexChanged(this.rpcThum_PageClicked);
            this.rpcThum.SizeChanged += new System.EventHandler(this.rpcThum_SizeChanged);
            // 
            // I3ReportPreviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(979, 669);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.rpc);
            this.Controls.Add(this.rpcThum);
            this.Controls.Add(this.ts);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "I3ReportPreviewer";
            this.ShowIcon = false;
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.I3ReportPreviewer_Load);
            this.Shown += new System.EventHandler(this.FormReportResultPreview_Shown);
            this.ts.ResumeLayout(false);
            this.ts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmStart;
        private System.Windows.Forms.ToolStripButton tsbExcel;
        private System.Windows.Forms.ToolStripSeparator tssPrint;
        private System.Windows.Forms.ToolStripSeparator tssScale;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripLabel lbScale;
        private System.Windows.Forms.ToolStripButton tsbFull;
        private System.Windows.Forms.ToolStripButton tsbFullWidth;
        private System.Windows.Forms.ToolStripButton tsbFullHeight;
        private System.Windows.Forms.ToolStripComboBox cbbPrinter;
        private System.Windows.Forms.ToolStripLabel lbPrintPage2;
        private System.Windows.Forms.ToolStripLabel lbPrintPage;
        private System.Windows.Forms.ToolStripButton tsbFirst;
        private System.Windows.Forms.ToolStripButton tsbPre;
        private System.Windows.Forms.ToolStripButton tsbNext;
        private System.Windows.Forms.ToolStripButton tsbLast;
        private System.Windows.Forms.ToolStripLabel lbPage;
        private System.Windows.Forms.ToolStripLabel lbPage2;
        private System.Windows.Forms.ToolStripSeparator tssPage;
        private System.Windows.Forms.ToolStripLabel lbPrintCount;
        private I3ReportPreviewerControl rpc;
        private I3ReportPreviewerControl rpcThum;
        private System.Windows.Forms.Button btCancel;
        public System.Windows.Forms.ToolStrip ts;
        private System.Windows.Forms.ToolStripButton tsbPrint;
    }
}