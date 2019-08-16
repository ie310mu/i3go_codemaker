
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table;
using IE310.Table.Events;
using IE310.Table.Win32;
using IE310.Table.Row;
using IE310.Table.Models;
using IE310.Table.Header;
using IE310.Table.Cell;

namespace IE310.Table.Column
{
    /// <summary>
    /// Summary description for ShowColumnsDialog.
    /// </summary>
    internal class I3ShowColumnsDialog : Form
    {
        #region Class Data

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private I3ColumnModel model = null;
        private GroupBox groupBox1;
        private Button okButton;
        private Button cancelButton;
        private I3ColumnModel columnModel1;
        private I3TableModel tableModel1;
        private I3CheckBoxColumn cbc;
        private I3TextColumn tc;
        private Button button1;
        private Button button2;
        private Button button3;
        private I3Table columnTable;

        #endregion


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public I3ShowColumnsDialog()
        {
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.columnTable = new IE310.Table.Models.I3Table();
            this.columnModel1 = new IE310.Table.Column.I3ColumnModel();
            this.cbc = new IE310.Table.Column.I3CheckBoxColumn();
            this.tc = new IE310.Table.Column.I3TextColumn();
            this.tableModel1 = new IE310.Table.Row.I3TableModel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.columnTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Location = new System.Drawing.Point(8, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 8);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(168, 395);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "确定";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(253, 395);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "取消";
            // 
            // columnTable
            // 
            this.columnTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnTable.ColumnHeaderDisplayMode = IE310.Table.Header.I3ColumnHeaderDisplayMode.Text;
            this.columnTable.ColumnModel = this.columnModel1;
            this.columnTable.EnableToolTips = true;
            this.columnTable.ExtendLastCol = true;
            this.columnTable.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.columnTable.HeaderFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.columnTable.Location = new System.Drawing.Point(12, 46);
            this.columnTable.Name = "columnTable";
            this.columnTable.RowHeaderDisplayMode = IE310.Table.Header.I3RowHeaderDisplayMode.Num;
            this.columnTable.Size = new System.Drawing.Size(316, 323);
            this.columnTable.TabIndex = 1;
            this.columnTable.TableModel = this.tableModel1;
            this.columnTable.ToolTipAutomaticDelay = 1000;
            this.columnTable.ToolTipAutoPopDelay = 10000;
            this.columnTable.UnfocusedSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.columnTable.UnfocusedSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            // 
            // columnModel1
            // 
            this.columnModel1.ColumnHeaderHeight = 20;
            this.columnModel1.Columns.AddRange(new IE310.Table.Column.I3Column[] {
            this.cbc,
            this.tc});
            // 
            // cbc
            // 
            this.cbc.Caption = "选择";
            this.cbc.CellAlignment = IE310.Table.Column.I3ColumnAlignment.Center;
            this.cbc.CheckBoxColumnStyle = IE310.Table.Column.I3CheckBoxColumnStyle.Image;
            this.cbc.CustomCheckImage = null;
            this.cbc.CustomCheckImageFillClient = false;
            this.cbc.CustomCheckImageSize = new System.Drawing.Size(24, 18);
            this.cbc.DataMember = "";
            this.cbc.Dictionary = null;
            this.cbc.DrawText = false;
            this.cbc.IsSelected = false;
            this.cbc.Key = "";
            this.cbc.NeedWidth = 34.76953F;
            this.cbc.Sortable = false;
            this.cbc.Tag = null;
            this.cbc.ToolTipText = "点击左键全选\r\n点击右键反选";
            this.cbc.Width = 49;
            // 
            // tc
            // 
            this.tc.Caption = "列名";
            this.tc.CellAlignment = IE310.Table.Column.I3ColumnAlignment.Center;
            this.tc.DataMember = "";
            this.tc.Dictionary = null;
            this.tc.Editable = false;
            this.tc.IsSelected = false;
            this.tc.Key = "";
            this.tc.NeedWidth = 34.76953F;
            this.tc.Sortable = false;
            this.tc.Tag = null;
            this.tc.Width = 225;
            // 
            // tableModel1
            // 
            this.tableModel1.DataSource = null;
            this.tableModel1.DefaultRowHeight = 20;
            // 
            // button3
            // 
            this.button3.Image = global::IE310.Table.Properties.Resources.backselect32;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(150, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(58, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "反选";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = global::IE310.Table.Properties.Resources.unselect32;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(76, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "全不选";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::IE310.Table.Properties.Resources.allselect32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "全选";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // I3ShowColumnsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(339, 431);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.columnTable);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "I3ShowColumnsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "列设置";
            ((System.ComponentModel.ISupportInitialize)(this.columnTable)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddColumns(I3ColumnModel model)
        {
            this.model = model;
            //this.columnTable.HeaderRenderer = model.Table.HeaderRenderer;



            I3CellStyle cellStyle = new I3CellStyle();
            cellStyle.Padding = new I3CellPadding(0, 0, 0, 0);

            this.columnTable.BeginUpdate();

            for (int i = 0; i < model.Columns.Count; i++)
            {
                I3Row row = new I3Row();

                I3Cell cell = new I3Cell("", model.Columns[i].Visible);
                cell.Tag = model.Columns[i].Width;
                cell.CellStyle = cellStyle;
                row.Cells.Add(cell);

                cell = new I3Cell(model.Columns[i].Caption);
                cell.Tag = model.Columns[i].Width;
                cell.CellStyle = cellStyle;
                row.Cells.Add(cell);

                this.columnTable.TableModel.Rows.Add(row);
            }

            this.columnTable.SelectionChanged += new IE310.Table.Events.I3SelectionEventHandler(columnTable_SelectionChanged);
            this.columnTable.CellCheckChanged += new IE310.Table.Events.I3CellCheckBoxEventHandler(columnTable_CellCheckChanged);

            if (this.columnTable.VScroll)
            {
                this.columnTable.ColumnModel.Columns[0].Width -= SystemInformation.VerticalScrollBarWidth;
            }

            if (this.columnTable.TableModel.Rows.Count > 0)
            {
                this.columnTable.TableModel.Selections.SelectCell(0, 0);
            }

            this.columnTable.EndUpdate();
        }

        #endregion


        #region Events

        /*/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void upButton_Click(object sender, System.EventArgs e)
			{
		
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void downButton_Click(object sender, System.EventArgs e)
			{
		
			}*/






        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            int[] indicies = this.columnTable.SelectedIndicies;

            for (int i = 0; i < this.columnTable.TableModel.Rows.Count; i++)
            {
                this.model.Columns[i].Visible = this.columnTable.TableModel[i, 0].Checked;
                this.model.Columns[i].Width = (int)this.columnTable.TableModel[i, 0].Tag;
            }

            this.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnTable_SelectionChanged(object sender, I3SelectionEventArgs e)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnTable_CellCheckChanged(object sender, I3CellCheckBoxEventArgs e)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void widthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != I3AsciiChars.Backspace && e.KeyChar != I3AsciiChars.Delete)
            {
                if ((Control.ModifierKeys & (Keys.Alt | Keys.Control)) == Keys.None)
                {
                    e.Handled = true;

                    I3NativeMethods.MessageBeep(0 /*MB_OK*/);
                }
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            int index = this.columnModel1.Columns.IndexOf(cbc);
            for (int i = 0; i < this.tableModel1.Rows.Count; i++)
            {
                this.tableModel1.Rows[i].Cells[index].Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = this.columnModel1.Columns.IndexOf(cbc);
            for (int i = 0; i < this.tableModel1.Rows.Count; i++)
            {
                this.tableModel1.Rows[i].Cells[index].Checked = !this.tableModel1.Rows[i].Cells[index].Checked;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = this.columnModel1.Columns.IndexOf(cbc);
            for (int i = 0; i < this.tableModel1.Rows.Count; i++)
            {
                this.tableModel1.Rows[i].Cells[index].Checked = false;
            }
        }



    }

}
