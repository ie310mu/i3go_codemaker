

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Renderers;
using IE310.Table.Win32;
using IE310.Table.Column;


namespace IE310.Table.Editors
{
    /// <summary>
    /// 下拉框编辑器
    /// A class for editing Cells that look like a ComboBox
    /// </summary>
    public class I3ComboBoxCellEditor : I3DropDownCellEditor
    {
        #region EventHandlers

        /// <summary>
        /// 选择项改变事件
        /// Occurs when the SelectedIndex property has changed
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// DrawItem事件
        /// Occurs when a visual aspect of an owner-drawn ComboBoxCellEditor changes
        /// </summary>
        public event DrawItemEventHandler DrawItem;

        /// <summary>
        /// MeasureItem事件
        /// Occurs each time an owner-drawn ComboBoxCellEditor item needs to be 
        /// drawn and when the sizes of the list items are determined
        /// </summary>
        public event MeasureItemEventHandler MeasureItem;

        #endregion


        #region Class Data

        /// <summary>
        /// 列表框，将显示在下拉容器中
        /// The ListBox that contains the items to be shown in the 
        /// drop-down portion of the ComboBoxCellEditor
        /// </summary>
        private ListBox listbox;

        /// <summary>
        /// 列表框中显示的项的最大个数
        /// The maximum number of items to be shown in the drop-down 
        /// portion of the ComboBoxCellEditor
        /// </summary>
        private int maxDropDownItems;

        /// <summary>
        /// 被编辑的Cell的宽度
        /// The width of the Cell being edited
        /// </summary>
        private int cellWidth;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ComboBoxCellEditor class with default settings
        /// </summary>
        public I3ComboBoxCellEditor() : base()
        {
            //生成并设置ListBox
            this.listbox = new ListBox();
            this.listbox.BorderStyle = BorderStyle.None;
            this.listbox.Location = new Point(0, 0);
            this.listbox.Size = new Size(100, 100);
            this.listbox.Dock = DockStyle.Fill;
            this.listbox.DrawItem += new DrawItemEventHandler(this.listbox_DrawItem);
            this.listbox.MeasureItem += new MeasureItemEventHandler(this.listbox_MeasureItem);
            this.listbox.MouseEnter += new EventHandler(this.listbox_MouseEnter);
            this.listbox.KeyDown += new KeyEventHandler(this.OnKeyDown);
            this.listbox.KeyPress += new KeyPressEventHandler(base.OnKeyPress);
            this.listbox.Click += new EventHandler(listbox_Click);
            this.listbox.ItemHeight = 24;
            this.listbox.DrawMode = DrawMode.OwnerDrawVariable;
            this.listbox.DrawItem += Listbox_DrawItem;

            this.TextBox.KeyDown += new KeyEventHandler(OnKeyDown);
            this.TextBox.MouseWheel += new MouseEventHandler(OnMouseWheel);

            this.maxDropDownItems = 8;

            this.cellWidth = 0;

            this.DropDown.Control = this.listbox;
        }

        private void Listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            if (e.Index >= 0)
            {
                StringFormat sStringFormat = new StringFormat();
                sStringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
            }
            e.DrawFocusRectangle();
        }

        #endregion


        #region Methods

        public override bool PrepareForEditing(Cell.I3Cell cell, I3Table table, Cell.I3CellPos cellPos, Rectangle cellRect, bool userSetEditorValues)
        {
            if (!(table.ColumnModel.Columns[cellPos.Column] is I3ComboBoxColumn))
            {
                throw new InvalidOperationException("Cannot edit Cell as ComboBoxColumn can only be used with a ComboBoxColumn");
            }

            I3ComboBoxColumn comboBoxColumn = table.ColumnModel.Columns[cellPos.Column] as I3ComboBoxColumn;
            this.Items = comboBoxColumn.Items;

            this.TextBox.ReadOnly = comboBoxColumn.DropDownStyle == I3DropDownStyle.DropDownList;

            return base.PrepareForEditing(cell, table, cellPos, cellRect, userSetEditorValues);
        }

        public override Control GetDataInputControl()
        {
            return this.TextBox;
        }

        /// <summary>
        /// 设置位置和大小，从CellEditor继承
        /// Sets the location and size of the CellEditor
        /// </summary>
        /// <param name="cellRect">A Rectangle that represents the size and location 
        /// of the Cell being edited</param>
        protected override void SetEditLocation(Rectangle cellRect)
        {
            // calc the size of the textbox
            II3CellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
            int buttonWidth = ((I3ComboBoxCellRenderer)renderer).ButtonWidth;

            this.TextBox.Size = new Size(cellRect.Width - 2 - buttonWidth, cellRect.Height - 2);
            this.TextBox.Location = cellRect.Location;
            this.TextBox.Left++;
            this.TextBox.Top++;


            this.cellWidth = cellRect.Width;
        }


        /// <summary>
        /// 设置当前编辑的值，从CellEditor继承
        /// Sets the initial value of the editor based on the contents of 
        /// the Cell being edited
        /// </summary>
        protected override void SetEditValue()
        {
            this.TextBox.Text = this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column].DataToString(this.EditingCell.Data);
            this.listbox.SelectedItem = this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column].DataToString(this.EditingCell.Data);
        }


        /// <summary>
        /// 设置编辑的Cell的值，从CellEditor继承
        /// Sets the contents of the Cell being edited based on the value 
        /// in the editor
        /// </summary>
        protected override void SetCellValue()
        {
            this.EditingCell.Data = this.TextBox.Text;
        }


        /// <summary>
        /// 开始编辑，增加ListBox项改变事件
        /// Starts editing the Cell
        /// </summary>
        public override void StartEditing()
        {
            this.listbox.SelectedIndexChanged += new EventHandler(listbox_SelectedIndexChanged);

            base.StartEditing();
        }


        /// <summary>
        /// 结束编辑，取消ListBox项改变事件
        /// Stops editing the Cell and commits any changes
        /// </summary>
        public override void StopEditing()
        {
            this.listbox.SelectedIndexChanged -= new EventHandler(listbox_SelectedIndexChanged);

            base.StopEditing();
        }


        /// <summary>
        /// 取消编辑，取消ListBox项改变事件
        /// Stops editing the Cell and ignores any changes
        /// </summary>
        public override void CancelEditing()
        {
            this.listbox.SelectedIndexChanged -= new EventHandler(listbox_SelectedIndexChanged);

            base.CancelEditing();
        }



        /// <summary>
        /// 显示下拉容器，根据要显示的项的个数计算高度
        /// Displays the drop down portion to the user
        /// </summary>
        protected override void ShowDropDown()
        {
            if (this.InternalDropDownWidth == -1)
            {
                this.DropDown.Width = this.cellWidth;
                this.listbox.Width = this.cellWidth;
            }

            if (this.IntegralHeight)
            {
                int visItems = this.listbox.Height / this.ItemHeight;

                if (visItems > this.MaxDropDownItems)
                {
                    visItems = this.MaxDropDownItems;
                }

                if (this.listbox.Items.Count < this.MaxDropDownItems)
                {
                    visItems = this.listbox.Items.Count;
                }

                if (visItems == 0)
                {
                    visItems = 1;
                }

                this.DropDown.Height = (visItems * this.ItemHeight) + 2;
                this.listbox.Height = visItems * this.ItemHeight;
            }

            base.ShowDropDown();
        }

        #endregion


        #region Properties

        /// <summary>
        /// 获取或设置显示项的最大个数
        /// Gets or sets the maximum number of items to be shown in the drop-down 
        /// portion of the ComboBoxCellEditor
        /// </summary>
        public int MaxDropDownItems
        {
            get
            {
                return this.maxDropDownItems;
            }

            set
            {
                if ((value < 1) || (value > 100))
                {
                    throw new ArgumentOutOfRangeException("MaxDropDownItems must be between 1 and 100");
                }

                this.maxDropDownItems = value;
            }
        }


        /// <summary>
        /// 获取或设置ListBox的DrawMode
        /// Gets or sets a value indicating whether your code or the operating 
        /// system will handle drawing of elements in the list
        /// </summary>
        public DrawMode DrawMode
        {
            get
            {
                return this.listbox.DrawMode;
            }

            set
            {
                if (!Enum.IsDefined(typeof(DrawMode), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(DrawMode));
                }

                this.listbox.DrawMode = value;
            }
        }


        /// <summary>
        /// 获取或设置ListBox的IntegralHeight
        /// Gets or sets a value indicating whether the drop-down portion of the 
        /// editor should resize to avoid showing partial items
        /// </summary>
        public bool IntegralHeight
        {
            get
            {
                return this.listbox.IntegralHeight;
            }

            set
            {
                this.listbox.IntegralHeight = value;
            }
        }


        /// <summary>
        /// 获取或设置ListBox的ItemHeight
		/// Gets or sets the height of an item in the editor
		/// </summary>
		public int ItemHeight
        {
            get
            {
                return this.listbox.ItemHeight;
            }

            set
            {
                this.listbox.ItemHeight = value;
            }
        }


        /// <summary>
        /// 获取ListBox的Items
        /// Gets an object representing the collection of the items contained 
        /// in this ComboBoxCellEditor
        /// </summary>
        public ListBox.ObjectCollection Items
        {
            get
            {
                return this.listbox.Items;
            }
            set
            {
                this.listbox.Items.Clear();
                foreach (object obj in value)
                {
                    this.listbox.Items.Add(obj);
                }
            }
        }
        


        /// <summary>
        /// 获取或设置TextBox的最大长度
        /// Gets or sets the maximum number of characters allowed in the editable 
        /// portion of a ComboBoxCellEditor
        /// </summary>
        public int MaxLength
        {
            get
            {
                return this.TextBox.MaxLength;
            }

            set
            {
                this.TextBox.MaxLength = value;
            }
        }


        /// <summary>
        /// 获取或设置TextBox的开始选择位置
        /// Gets or sets the index specifying the currently selected item
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return this.listbox.SelectedIndex;
            }

            set
            {
                if (this.listbox.Items.Count == 0 || value < 0 || value > this.listbox.Items.Count - 1)
                {
                    this.listbox.SelectedIndex = -1;
                    return;
                }
                this.listbox.SelectedIndex = value;
            }
        }


        /// <summary>
        /// 获取或设置ListBox的选择项
        /// Gets or sets currently selected item in the ComboBoxCellEditor
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return this.listbox.SelectedItem;
            }

            set
            {
                this.listbox.SelectedItem = value;
            }
        }

        #endregion


        #region Events

        /// <summary>
        /// TextBox和ListBox的键盘按下事件
        /// Handler for the editors TextBox.KeyDown and ListBox.KeyDown events
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">A KeyEventArgs that contains the event data</param>
        protected virtual void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                int index = this.SelectedIndex;

                if (index == -1)
                {
                    this.SelectedIndex = 0;
                }
                else if (index > 0)
                {
                    this.SelectedIndex--;
                }

                e.Handled = true;
            }
            else if (e.KeyData == Keys.Down)
            {
                int index = this.SelectedIndex;

                if (index == -1)
                {
                    this.SelectedIndex = 0;
                }
                else if (index < this.Items.Count - 1)
                {
                    this.SelectedIndex++;
                }

                e.Handled = true;
            }
        }


        /// <summary>
        /// TextBox的鼠标中键滚动事件
        /// Handler for the editors TextBox.MouseWheel event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected virtual void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int index = this.SelectedIndex;

            if (index == -1)
            {
                this.SelectedIndex = 0;
            }
            else
            {
                if (e.Delta > 0)
                {
                    if (index > 0)
                    {
                        this.SelectedIndex--;
                    }
                }
                else
                {
                    if (index < this.Items.Count - 1)
                    {
                        this.SelectedIndex++;
                    }
                }
            }
        }


        /// <summary>
        /// ListBox的DrawItem事件处理代码,引发ComboBoxCellEditor的DrawItem事件
        /// Raises the DrawItem event
        /// </summary>
        /// <param name="e">A DrawItemEventArgs that contains the event data</param>
        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            if (DrawItem != null)
            {
                DrawItem(this, e);
            }
        }


        /// <summary>
        /// ListBox的MeasureItem事件处理代码,引发ComboBoxCellEditor的MeasureItem事件
        /// Raises the MeasureItem event
        /// </summary>
        /// <param name="e">A MeasureItemEventArgs that contains the event data</param>
        protected virtual void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (MeasureItem != null)
            {
                MeasureItem(this, e);
            }
        }


        /// <summary>
        /// ListBox的OnSelectedIndexChanged事件处理代码,引发ComboBoxCellEditor的OnSelectedIndexChanged事件
        /// Raises the SelectedIndexChanged event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }

            this.TextBox.Text = this.SelectedItem.ToString();
        }


        /// <summary>
        /// ListBox的点击事件处理代码，结束编辑
        /// Handler for the editors ListBox.Click event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">An EventArgs that contains the event data</param>
        private void listbox_Click(object sender, EventArgs e)
        {
            this.DroppedDown = false;

            this.EditingTable.StopEditing();
        }


        /// <summary>
        /// ListBox的OnSelectedIndexChanged事件处理代码,引发ComboBoxCellEditor的OnSelectedIndexChanged事件
        /// Handler for the editors ListBox.SelectedIndexChanged event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">An EventArgs that contains the event data</param>
        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnSelectedIndexChanged(e);
        }


        /// <summary>
        /// ListBox的鼠标进入事件，引发Table的CellMouseLeave事件
        /// Handler for the editors ListBox.MouseEnter event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">An EventArgs that contains the event data</param>
        private void listbox_MouseEnter(object sender, EventArgs e)
        {
            this.EditingTable.RaiseCellMouseLeave(this.EditingCellPos);
        }


        /// <summary>
        /// Handler for the editors ListBox.DrawItem event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">A DrawItemEventArgs that contains the event data</param>
        private void listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.OnDrawItem(e);
        }


        /// <summary>
        /// Handler for the editors ListBox.MeasureItem event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">A MeasureItemEventArgs that contains the event data</param>
        private void listbox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            this.OnMeasureItem(e);
        }

        #endregion
    }


}
