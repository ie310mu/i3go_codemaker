
using IE310.Core.Controls;
namespace IE310.Core.Progressing
{
    partial class SimpleProgressReporterControl
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
            this.btCancel = new System.Windows.Forms.Button();
            this.progressBar = new IE310.Core.Controls.I3PercentProgressBar();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.BackgroundImage = global::IE310.Core.Resource.stop;
            this.btCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCancel.Location = new System.Drawing.Point(613, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(20, 20);
            this.btCancel.TabIndex = 16;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Visible = false;
            this.btCancel.Click += new System.EventHandler(this.ibtnCancel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(613, 20);
            this.progressBar.TabIndex = 15;
            this.progressBar.Tag = "info";
            // 
            // SimpleProgressReporterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btCancel);
            this.Name = "SimpleProgressReporterControl";
            this.Size = new System.Drawing.Size(633, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private I3PercentProgressBar progressBar;
    }
}
