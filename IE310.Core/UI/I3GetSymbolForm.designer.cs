using IE310.Table.Column;
using IE310.Table.Row;
using IE310.Table.Header;
namespace IE310.Core.UI
{
    partial class I3GetSymbolForm
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
            this.cbbSymbolStyle = new System.Windows.Forms.ComboBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.i3Table = new IE310.Table.Models.I3Table();
            this.i3ColumnModel = new I3ColumnModel();
            this.i3TableModel = new I3TableModel();
            ((System.ComponentModel.ISupportInitialize)(this.i3Table)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbSymbolStyle
            // 
            this.cbbSymbolStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSymbolStyle.FormattingEnabled = true;
            this.cbbSymbolStyle.Location = new System.Drawing.Point(257, 12);
            this.cbbSymbolStyle.Name = "cbbSymbolStyle";
            this.cbbSymbolStyle.Size = new System.Drawing.Size(159, 20);
            this.cbbSymbolStyle.TabIndex = 0;
            this.cbbSymbolStyle.SelectedIndexChanged += new System.EventHandler(this.cbbSymbolStyle_SelectedIndexChanged);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(245, 256);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(326, 256);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "符号类型:";
            // 
            // i3Table
            // 
            this.i3Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.i3Table.ColumnHeaderDisplayMode = I3ColumnHeaderDisplayMode.Text;
            this.i3Table.ColumnHeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.i3Table.ColumnModel = this.i3ColumnModel;
            this.i3Table.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.i3Table.GridLines = IE310.Table.Models.I3GridLines.Both;
            this.i3Table.HeaderFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.i3Table.Location = new System.Drawing.Point(12, 38);
            this.i3Table.Name = "i3Table";
            this.i3Table.RowHeaderDisplayMode = I3RowHeaderDisplayMode.Num;
            this.i3Table.RowHeaderVisible = false;
            this.i3Table.RowResizing = false;
            this.i3Table.Size = new System.Drawing.Size(404, 203);
            this.i3Table.TabIndex = 5;
            this.i3Table.TableModel = this.i3TableModel;
            this.i3Table.Text = "i3Table1";
            this.i3Table.UnfocusedSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.i3Table.UnfocusedSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.i3Table.CellDoubleClick += new IE310.Table.Events.I3CellMouseEventHandler(this.i3Table_CellDoubleClick);
            // 
            // i3ColumnModel
            // 
            this.i3ColumnModel.ColumnHeaderHeight = 20;
            // 
            // i3TableModel
            // 
            this.i3TableModel.DefaultRowHeight = 20;
            this.i3TableModel.RowHeaderWidth = 30;
            // 
            // I3GetSymbolForm
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(431, 291);
            this.Controls.Add(this.i3Table);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cbbSymbolStyle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "I3GetSymbolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "符号";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TfrmGetSymbol_FormClosed);
            this.Load += new System.EventHandler(this.TfrmGetSymbol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.i3Table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbSymbolStyle;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private Table.Models.I3Table i3Table;
        private I3ColumnModel i3ColumnModel;
        private I3TableModel i3TableModel;
    }
}