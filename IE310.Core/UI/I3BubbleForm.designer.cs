namespace IE310.Core.UI
{
    partial class I3BubbleForm
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
            this.tmHeight = new System.Windows.Forms.Timer(this.components);
            this.tmClose = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmHeight
            // 
            this.tmHeight.Interval = 1000;
            this.tmHeight.Tick += new System.EventHandler(this.tmHeight_Tick);
            // 
            // tmClose
            // 
            this.tmClose.Interval = 1000;
            this.tmClose.Tick += new System.EventHandler(this.tmClose_Tick);
            // 
            // IEFS_BubbleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 163);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IEFS_BubbleForm";
            this.Text = "IEFS_BubbleForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IEFS_BubbleForm_FormClosing);
            this.Load += new System.EventHandler(this.IEFS_BubbleForm_Load);
            this.Shown += new System.EventHandler(this.IEFS_BubbleForm_Shown);
            this.Click += new System.EventHandler(this.IEFS_BubbleForm_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmHeight;
        private System.Windows.Forms.Timer tmClose;
    }
}