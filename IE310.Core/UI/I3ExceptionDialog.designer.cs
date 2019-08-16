
namespace IE310.Core.UI
{
    partial class I3ExceptionDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(I3ExceptionDialog));
            this.picError = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabError = new System.Windows.Forms.TabControl();
            this.tpDetail = new System.Windows.Forms.TabPage();
            this.tbDetail = new System.Windows.Forms.TextBox();
            this.tpStackTrace = new System.Windows.Forms.TabPage();
            this.tbStackTrace = new System.Windows.Forms.TextBox();
            this.tpContext = new System.Windows.Forms.TabPage();
            this.dgContext = new System.Windows.Forms.DataGridView();
            this.keyCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnOp = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picError)).BeginInit();
            this.tabError.SuspendLayout();
            this.tpDetail.SuspendLayout();
            this.tpStackTrace.SuspendLayout();
            this.tpContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgContext)).BeginInit();
            this.SuspendLayout();
            // 
            // picError
            // 
            this.picError.ErrorImage = null;
            this.picError.Image = ((System.Drawing.Image)(resources.GetObject("picError.Image")));
            this.picError.InitialImage = null;
            this.picError.Location = new System.Drawing.Point(15, 8);
            this.picError.Name = "picError";
            this.picError.Size = new System.Drawing.Size(64, 64);
            this.picError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picError.TabIndex = 1;
            this.picError.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(150, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabError
            // 
            this.tabError.Controls.Add(this.tpDetail);
            this.tabError.Controls.Add(this.tpStackTrace);
            this.tabError.Controls.Add(this.tpContext);
            this.tabError.Location = new System.Drawing.Point(8, 78);
            this.tabError.Name = "tabError";
            this.tabError.SelectedIndex = 0;
            this.tabError.Size = new System.Drawing.Size(360, 254);
            this.tabError.TabIndex = 3;
            this.tabError.Visible = false;
            // 
            // tpDetail
            // 
            this.tpDetail.Controls.Add(this.tbDetail);
            this.tpDetail.Location = new System.Drawing.Point(4, 22);
            this.tpDetail.Name = "tpDetail";
            this.tpDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetail.Size = new System.Drawing.Size(352, 228);
            this.tpDetail.TabIndex = 0;
            this.tpDetail.Text = "详细信息";
            this.tpDetail.ToolTipText = "详细信息";
            this.tpDetail.UseVisualStyleBackColor = true;
            // 
            // tbDetail
            // 
            this.tbDetail.Location = new System.Drawing.Point(3, 3);
            this.tbDetail.Multiline = true;
            this.tbDetail.Name = "tbDetail";
            this.tbDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDetail.Size = new System.Drawing.Size(346, 222);
            this.tbDetail.TabIndex = 2;
            this.tbDetail.WordWrap = false;
            // 
            // tpStackTrace
            // 
            this.tpStackTrace.Controls.Add(this.tbStackTrace);
            this.tpStackTrace.Location = new System.Drawing.Point(4, 22);
            this.tpStackTrace.Name = "tpStackTrace";
            this.tpStackTrace.Padding = new System.Windows.Forms.Padding(3);
            this.tpStackTrace.Size = new System.Drawing.Size(352, 228);
            this.tpStackTrace.TabIndex = 2;
            this.tpStackTrace.Text = "堆栈跟踪";
            this.tpStackTrace.ToolTipText = "堆栈跟踪";
            this.tpStackTrace.UseVisualStyleBackColor = true;
            // 
            // tbStackTrace
            // 
            this.tbStackTrace.Location = new System.Drawing.Point(3, 3);
            this.tbStackTrace.Multiline = true;
            this.tbStackTrace.Name = "tbStackTrace";
            this.tbStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbStackTrace.Size = new System.Drawing.Size(346, 222);
            this.tbStackTrace.TabIndex = 3;
            this.tbStackTrace.WordWrap = false;
            // 
            // tpContext
            // 
            this.tpContext.Controls.Add(this.dgContext);
            this.tpContext.Location = new System.Drawing.Point(4, 22);
            this.tpContext.Name = "tpContext";
            this.tpContext.Padding = new System.Windows.Forms.Padding(3);
            this.tpContext.Size = new System.Drawing.Size(352, 228);
            this.tpContext.TabIndex = 1;
            this.tpContext.Text = "上下文数据";
            this.tpContext.ToolTipText = "上下文数据";
            this.tpContext.UseVisualStyleBackColor = true;
            // 
            // dgContext
            // 
            this.dgContext.AllowUserToAddRows = false;
            this.dgContext.AllowUserToDeleteRows = false;
            this.dgContext.AllowUserToResizeRows = false;
            this.dgContext.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgContext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgContext.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgContext.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyCol,
            this.valueCol,
            this.spCol});
            this.dgContext.Location = new System.Drawing.Point(3, 3);
            this.dgContext.MultiSelect = false;
            this.dgContext.Name = "dgContext";
            this.dgContext.RowHeadersWidth = 20;
            this.dgContext.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgContext.RowTemplate.Height = 23;
            this.dgContext.Size = new System.Drawing.Size(346, 222);
            this.dgContext.TabIndex = 0;
            this.dgContext.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgContext_CellContentClick);
            // 
            // keyCol
            // 
            this.keyCol.DataPropertyName = "Key";
            this.keyCol.HeaderText = "键";
            this.keyCol.MinimumWidth = 20;
            this.keyCol.Name = "keyCol";
            // 
            // valueCol
            // 
            this.valueCol.DataPropertyName = "Value";
            this.valueCol.HeaderText = "值";
            this.valueCol.MinimumWidth = 100;
            this.valueCol.Name = "valueCol";
            this.valueCol.Width = 200;
            // 
            // spCol
            // 
            this.spCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.spCol.HeaderText = "";
            this.spCol.Name = "spCol";
            this.spCol.ReadOnly = true;
            this.spCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spCol.Text = ">";
            this.spCol.ToolTipText = "详细内容";
            this.spCol.UseColumnTextForButtonValue = true;
            this.spCol.Width = 20;
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnOp
            // 
            this.btnOp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOp.Location = new System.Drawing.Point(324, 87);
            this.btnOp.Name = "btnOp";
            this.btnOp.Size = new System.Drawing.Size(40, 23);
            this.btnOp.TabIndex = 6;
            this.btnOp.Text = ">>";
            this.btnOp.UseVisualStyleBackColor = true;
            this.btnOp.Click += new System.EventHandler(this.btnOp_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(91, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(260, 64);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExceptionDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(374, 122);
            this.Controls.Add(this.btnOp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.picError);
            this.Controls.Add(this.tabError);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误";
            this.Shown += new System.EventHandler(this.ExceptionDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picError)).EndInit();
            this.tabError.ResumeLayout(false);
            this.tpDetail.ResumeLayout(false);
            this.tpDetail.PerformLayout();
            this.tpStackTrace.ResumeLayout(false);
            this.tpStackTrace.PerformLayout();
            this.tpContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgContext)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picError;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabError;
        private System.Windows.Forms.TabPage tpDetail;
        private System.Windows.Forms.TabPage tpContext;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox tbDetail;
        private System.Windows.Forms.Button btnOp;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridView dgContext;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueCol;
        private System.Windows.Forms.DataGridViewButtonColumn spCol;
        private System.Windows.Forms.TabPage tpStackTrace;
        private System.Windows.Forms.TextBox tbStackTrace;
    }
}