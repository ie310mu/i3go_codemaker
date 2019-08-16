
namespace IE310.Core.Progressing
{
    partial class ProgressReporterControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbInfo = new System.Windows.Forms.Label();
            this.ibtnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Location = new System.Drawing.Point(5, 13);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(53, 12);
            this.lbInfo.TabIndex = 17;
            this.lbInfo.Text = "处理消息";
            // 
            // ibtnCancel
            // 
            this.ibtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnCancel.Location = new System.Drawing.Point(341, 57);
            this.ibtnCancel.Name = "ibtnCancel";
            this.ibtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ibtnCancel.TabIndex = 16;
            this.ibtnCancel.Text = "取消";
            this.ibtnCancel.UseVisualStyleBackColor = true;
            this.ibtnCancel.Click += new System.EventHandler(this.ibtnCancel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(5, 32);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(411, 20);
            this.progressBar.TabIndex = 15;
            // 
            // ProgressReporterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.ibtnCancel);
            this.Controls.Add(this.progressBar);
            this.Name = "ProgressReporterControl";
            this.Size = new System.Drawing.Size(420, 86);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button ibtnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
