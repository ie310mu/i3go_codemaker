namespace IE310.Core.UI
{
    partial class I3BubbleMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(I3BubbleMessageForm));
            this.lbCaption = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.edMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbCaption
            // 
            this.lbCaption.AutoSize = true;
            this.lbCaption.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCaption.Location = new System.Drawing.Point(24, 21);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(54, 12);
            this.lbCaption.TabIndex = 0;
            this.lbCaption.Text = "Caption";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(225, 126);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(144, 126);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.button2_Click);
            // 
            // edMessage
            // 
            this.edMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edMessage.Location = new System.Drawing.Point(26, 41);
            this.edMessage.Multiline = true;
            this.edMessage.Name = "edMessage";
            this.edMessage.ReadOnly = true;
            this.edMessage.Size = new System.Drawing.Size(274, 65);
            this.edMessage.TabIndex = 3;
            this.edMessage.Text = resources.GetString("edMessage.Text");
            // 
            // IEFS_BubbleMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(312, 163);
            this.Controls.Add(this.edMessage);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.lbCaption);
            this.Name = "IEFS_BubbleMessageForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCaption;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TextBox edMessage;
    }
}
