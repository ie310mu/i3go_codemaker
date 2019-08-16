using IE310.Core.Controls;
namespace IE310.Core.UI
{
    partial class I3SearchForm
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
            this.btSearch = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.iecT_Search1 = new IE310.Core.Controls.I3Search();
            this.SuspendLayout();
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(592, 418);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 1;
            this.btSearch.Text = "查找";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(674, 418);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // iecT_Search1
            // 
            this.iecT_Search1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iecT_Search1.Location = new System.Drawing.Point(0, 0);
            this.iecT_Search1.Name = "iecT_Search1";
            this.iecT_Search1.SearchInfo = null;
            this.iecT_Search1.SearchName = null;
            this.iecT_Search1.Size = new System.Drawing.Size(764, 447);
            this.iecT_Search1.TabIndex = 0;
            this.iecT_Search1.Search += new IE310.Core.Controls.I3ForSearch(this.iecT_Search1_Search);
            // 
            // I3SearchForm
            // 
            this.AcceptButton = this.btSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(764, 447);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.iecT_Search1);
            this.Name = "I3SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通用查找窗口";
            this.ResumeLayout(false);

        }

        #endregion

        private I3Search iecT_Search1;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Button btCancel;

    }
}