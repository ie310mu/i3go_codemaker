using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Cell;
using IE310.Table.Column;


namespace IE310.Table.Row 
{   
	/// <summary>
    /// �ж��� 
	/// SRepresents a row of Cells displayed in a Table
	/// </summary>
	[DesignTimeVisible(true),
	TypeConverter(typeof(I3RowConverter))]
	public class I3Row : IDisposable 
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when a Cell is added to the Row
		/// </summary>
		public event I3RowEventHandler CellAdded;

		/// <summary>
		/// Occurs when a Cell is removed from the Row
		/// </summary>
		public event I3RowEventHandler CellRemoved;

		/// <summary>
		/// Occurs when the value of a Row's property changes
		/// </summary>
		public event I3RowEventHandler PropertyChanged;

		#endregion


		#region Class Data

		// Row state flags
		private static readonly int STATE_EDITABLE = 1;
        private static readonly int STATE_ENABLED = 2;

        public static readonly int ResizePadding_Const = 4;
        
        /// <summary>
        /// �е����߶�
        /// </summary>
        public static readonly int MaximumHeight_Const = 1024;

        /// <summary>
        /// �е���С�߶�
        /// </summary>
        public static readonly int MinimumHeight_Const = ResizePadding_Const * 4;

		/// <summary>
		/// The collection of Cells's contained in the Row
		/// </summary>
		private I3CellCollection cells;

		/// <summary>
		/// An object that contains data about the Row
		/// </summary>
		private object tag;

        private object userData;

		/// <summary>
		/// The TableModel that the Row belongs to
		/// </summary>
		private I3TableModel tableModel;

		/// <summary>
		/// The index of the Row
		/// </summary>
		private int index;

		/// <summary>
		/// the current state of the Row
		/// </summary>
		private byte state;
		
		/// <summary>
		/// The Row's RowStyle
		/// </summary>
		private I3RowStyle rowStyle;

		/// <summary>
		/// The number of Cells in the Row that are selected
		/// </summary>
		private int selectedCellCount;

		/// <summary>
		/// Specifies whether the Row has been disposed
		/// </summary>
		private bool disposed = false;

        /// <summary>
        /// �иߣ�ֵС��0ʱ��ʹ��Ĭ���и�
        /// </summary>
        private int height = -1;

        /// <summary>
        /// ��ʶ�е�״̬
        /// The current state of the Column
        /// </summary>
        private I3RowState rowState;

        /// <summary>
        /// ��ʶ���Ƿ���ѡ��״̬
        /// </summary>
        private bool isSelected;


        /// <summary>
        /// �б�������ʾ����ʾ��Ϣ
        /// </summary>
        private string tooltipText;
        private float needHeight = 0;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Row class with default settings
		/// </summary>
		public I3Row()
		{
			this.Init();
		}


		/// <summary>
		/// Initializes a new instance of the Row class with an array of strings 
		/// representing Cells
		/// </summary>
		/// <param name="items">An array of strings that represent the Cells of 
		/// the Row</param>
		public I3Row(string[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items", "string[] cannot be null");
			}
			
			this.Init();

			if (items.Length > 0)
			{
				I3Cell[] cells = new I3Cell[items.Length];

				for (int i=0; i<items.Length; i++)
				{
					cells[i] = new I3Cell(items[i]);
				}

				this.Cells.AddRange(cells);
			}
		}


		/// <summary>
		/// Initializes a new instance of the Row class with an array of Cell objects 
		/// </summary>
		/// <param name="cells">An array of Cell objects that represent the Cells of the Row</param>
		public I3Row(I3Cell[] cells)
		{
			if (cells == null)
			{
				throw new ArgumentNullException("cells", "Cell[] cannot be null");
			}
			
			this.Init();

			if (cells.Length > 0)
			{
				this.Cells.AddRange(cells);
			}
		}


		/// <summary>
		/// Initializes a new instance of the Row class with an array of strings 
		/// representing Cells and the foreground color, background color, and font 
		/// of the Row
		/// </summary>
		/// <param name="items">An array of strings that represent the Cells of the Row</param>
		/// <param name="foreColor">The foreground Color of the Row</param>
		/// <param name="backColor">The background Color of the Row</param>
		/// <param name="font">The Font used to draw the text in the Row's Cells</param>
		public I3Row(string[] items, Color foreColor, Color backColor, Font font)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items", "string[] cannot be null");
			}
			
			this.Init();

			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;

			if (items.Length > 0)
			{
				I3Cell[] cells = new I3Cell[items.Length];

				for (int i=0; i<items.Length; i++)
				{
					cells[i] = new I3Cell(items[i]);
				}

				this.Cells.AddRange(cells);
			}
		}


		/// <summary>
		/// Initializes a new instance of the Row class with an array of Cell objects and 
		/// the foreground color, background color, and font of the Row
		/// </summary>
		/// <param name="cells">An array of Cell objects that represent the Cells of the Row</param>
		/// <param name="foreColor">The foreground Color of the Row</param>
		/// <param name="backColor">The background Color of the Row</param>
		/// <param name="font">The Font used to draw the text in the Row's Cells</param>
		public I3Row(I3Cell[] cells, Color foreColor, Color backColor, Font font)
		{
			if (cells == null)
			{
				throw new ArgumentNullException("cells", "Cell[] cannot be null");
			}
			
			this.Init();

			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;

			if (cells.Length > 0)
			{
				this.Cells.AddRange(cells);
			}
		}


		/// <summary>
		/// Initialise default values
		/// </summary>
		private void Init()
		{
			this.cells = null;
            this.userData = null;
			this.tag = null;
			this.tableModel = null;
			this.index = -1;
			this.rowStyle = null;
			this.selectedCellCount = 0;
            this.rowState = I3RowState.Normal;

			this.state = (byte) (STATE_EDITABLE | STATE_ENABLED);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Releases all resources used by the Row
		/// </summary>
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.tag = null;

				if (this.tableModel != null)
				{
					this.tableModel.Rows.Remove(this);
				}

				this.tableModel = null;
				this.index = -1;

				if (this.cells != null)
				{
					I3Cell cell;
					
					for (int i=0; i<this.cells.Count; i++)
					{
						cell = this.cells[i];

						cell.InternalRow = null;
						cell.Dispose();
					}

					this.cells = null;
				}

				this.rowStyle = null;
				this.state = (byte) 0;
				
				this.disposed = true;
			}
		}


		/// <summary>
		/// Returns the state represented by the specified state flag
		/// </summary>
		/// <param name="flag">A flag that represents the state to return</param>
		/// <returns>The state represented by the specified state flag</returns>
		internal bool GetState(int flag)
		{
			return ((this.state & flag) != 0);
		}


		/// <summary>
		/// Sets the state represented by the specified state flag to the specified value
		/// </summary>
		/// <param name="flag">A flag that represents the state to be set</param>
		/// <param name="value">The new value of the state</param>
		internal void SetState(int flag, bool value)
		{
			this.state = (byte) (value ? (this.state | flag) : (this.state & ~flag));
		}

		#endregion


		#region Properties


		/// <summary>
		/// A CellCollection representing the collection of 
		/// Cells contained within the Row
		/// </summary>
		[Category("Behavior"),
		Description("Cell Collection"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
		Editor(typeof(I3CellCollectionEditor), typeof(UITypeEditor))]
		public I3CellCollection Cells
		{
			get
			{
				if (this.cells == null)
				{
					this.cells = new I3CellCollection(this);
				}
				
				return this.cells;
			}
		}


		/// <summary>
		/// Gets or sets the object that contains data about the Row
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("User defined data associated with the row"),
		TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}

			set
			{
				this.tag = value;
			}
		}

        [Browsable(false)]
        public object UserData
        {
            get
            {
                return this.userData;
            }
            set
            {
                this.userData = value;
            }
        }


		/// <summary>
		/// Gets or sets the RowStyle used by the Row
		/// </summary>
		[Browsable(false),
		DefaultValue(null)]
		public I3RowStyle RowStyle
		{
			get
			{
				return this.rowStyle;
			}

			set
			{
				if (this.rowStyle != value)
				{
					this.rowStyle = value;

					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.StyleChanged));
				}
			}
		}


		/// <summary>
		/// Gets or sets the background color for the Row
		/// </summary>
		[Browsable(true), 
		Category("Appearance"),
		Description("The background color used to display text and graphics in the row")]
		public Color BackColor
		{
			get
			{
				if (this.RowStyle == null)
				{
					return Color.Transparent;
				}
				
				return this.RowStyle.BackColor;
			}

			set
			{
				if (this.RowStyle == null)
				{
					this.RowStyle = new I3RowStyle();
				}
				
				if (this.RowStyle.BackColor != value)
				{
					this.RowStyle.BackColor = value;

					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.BackColorChanged));
				}
			}
		}


		/// <summary>
		/// Specifies whether the BackColor property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the BackColor property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeBackColor()
		{
			return (this.rowStyle != null && this.rowStyle.BackColor != Color.Empty);
		}


		/// <summary>
		/// Gets or sets the foreground Color for the Row
		/// </summary>
		[Browsable(true), 
		Category("Appearance"),
		Description("The foreground color used to display text and graphics in the row")]
		public Color ForeColor
		{
			get
			{
				if (this.RowStyle == null)
				{
					if (this.TableModel != null && this.TableModel.Table != null)
                    {
						return this.TableModel.Table.ForeColor;
					}

					return Color.Black;
				}
				else
				{
					if (this.RowStyle.ForeColor == Color.Empty || this.RowStyle.ForeColor == Color.Transparent)
					{
						if (this.TableModel != null && this.TableModel.Table != null)
                        {
							return this.TableModel.Table.ForeColor;
						}
					}

					return this.RowStyle.ForeColor;
				}
			}

			set
			{
				if (this.RowStyle == null)
				{
					this.RowStyle = new I3RowStyle();
				}
				
				if (this.RowStyle.ForeColor != value)
				{
					this.RowStyle.ForeColor = value;

					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.ForeColorChanged));
				}
			}
		}


		/// <summary>
		/// Specifies whether the ForeColor property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the ForeColor property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeForeColor()
		{
			return (this.rowStyle != null && this.rowStyle.ForeColor != Color.Empty);
		}


		/// <summary>
		/// Gets or sets the vertical alignment of the objects displayed in the Row
		/// </summary>
		[Browsable(true), 
		Category("Appearance"),
		DefaultValue(I3RowAlignment.Center),
		Description("The vertical alignment of the objects displayed in the row")]
		public I3RowAlignment Alignment
		{
			get
			{
				if (this.RowStyle == null)
				{
					return I3RowAlignment.Center;
				}
				
				return this.RowStyle.Alignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3RowAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3RowAlignment));
				}
					
				if (this.RowStyle == null)
				{
					this.RowStyle = new I3RowStyle();
				}
				
				if (this.RowStyle.Alignment != value)
				{
					this.RowStyle.Alignment = value;

					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.AlignmentChanged));
				}
			}
		}


		/// <summary>
		/// Gets or sets the Font used by the Row
		/// </summary>
		[Browsable(true), 
		Category("Appearance"),
		Description("The font used to display text in the row")]
		public Font Font
		{
			get
			{
				if (this.RowStyle == null)
				{
					if (this.TableModel != null && this.TableModel.Table != null)
					{
						return this.TableModel.Table.Font;
					}

					return null;
				}
				else
				{
					if (this.RowStyle.Font == null)
					{
						if (this.TableModel != null && this.TableModel.Table != null)
						{
							return this.TableModel.Table.Font;
						}
					}

					return this.RowStyle.Font;
				}
			}

			set
			{
				if (this.RowStyle == null)
				{
					this.RowStyle = new I3RowStyle();
				}
				
				if (this.RowStyle.Font != value)
				{
					this.RowStyle.Font = value;

					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.FontChanged));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Font property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Font property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeFont()
		{
			return (this.rowStyle != null && this.rowStyle.Font != null);
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Row's Cells are able 
		/// to be edited
		/// </summary>
		[Browsable(true), 
		Category("Appearance"),
		Description("Controls whether the row's cell contents are able to be changed by the user")]
		public bool Editable
		{
			get
			{
				if (!this.GetState(STATE_EDITABLE))
				{
					return false;
				}

				return this.Enabled;
			}

			set
			{
				bool editable = this.Editable;
				
				this.SetState(STATE_EDITABLE, value);
				
				if (editable != value)
				{
					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.EditableChanged));
				}
			}
		}

        /// <summary>
        /// ��ȡ������Visible���ԣ�������Visible�ı��¼�
        /// </summary>
        [Category("Appearance"),
        DefaultValue(true),
        Description("Determines whether the row is visible or hidden.")]
        public bool Visible
        {
            get
            {
                return true;
            }
            set
            {
            }

            //get
            //{
            //    return this.GetState(STATE_VISIBLE);
            //}

            //set
            //{
            //    bool visible = this.Visible;

            //    this.SetState(STATE_VISIBLE, value);

            //    if (visible != value)
            //    {
            //        this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.VisibleChanged, visible));
            //    }
            //}
        }


		/// <summary>
		/// Specifies whether the Editable property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Editable property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeEditable()
		{
			return !this.GetState(STATE_EDITABLE);
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Row's Cells can respond to 
		/// user interaction
		/// </summary>
		[Browsable(true), 
		Category("Appearance"),
		Description("Indicates whether the row's cells can respond to user interaction"),
		RefreshProperties(RefreshProperties.All)]
		public bool Enabled
		{
			get
			{
				if (!this.GetState(STATE_ENABLED))
				{
					return false;
				}

				if (this.TableModel == null)
				{
					return true;
				}

				return this.TableModel.Enabled;
			}

			set
			{
				bool enabled = this.Enabled;
				
				this.SetState(STATE_ENABLED, value);
				
				if (enabled != value)
				{
					this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.EnabledChanged));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Enabled property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Enabled property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeEnabled()
		{
			return !this.GetState(STATE_ENABLED);
		}


		/// <summary>
		/// Gets the TableModel the Row belongs to
		/// </summary>
		[Browsable(false)]
		public I3TableModel TableModel
		{
			get
			{
				return this.tableModel;
			}
		}


		/// <summary>
		/// Gets or sets the TableModel the Row belongs to
		/// </summary>
		internal I3TableModel InternalTableModel
		{
			get
			{
				return this.tableModel;
			}

			set
			{
				this.tableModel = value;
			}
		}


		/// <summary>
		/// Gets the index of the Row within its TableModel
		/// </summary>
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
		/// Gets or sets the index of the Row within its TableModel
		/// </summary>
		internal int InternalIndex
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}


		/// <summary>
		/// Updates the Cell's Index property so that it matches the Cells 
		/// position in the CellCollection
		/// </summary>
		/// <param name="start">The index to start updating from</param>
		internal void UpdateCellIndicies(int start)
		{
			if (start == -1)
			{
				start = 0;
			}
			
			for (int i=start; i<this.Cells.Count; i++)
			{
				this.Cells[i].InternalIndex = i;
			}
		}


		/// <summary>
		/// Gets whether the Row is able to raise events
		/// </summary>
		protected internal bool CanRaiseEvents
		{
			get
			{
				if (this.TableModel != null)
				{
					return this.TableModel.CanRaiseEvents;
				}
				
				return true;
			}
		}


		/// <summary>
		/// Gets the number of Cells that are selected within the Row
		/// </summary>
		[Browsable(false)]
		public int SelectedCellCount
		{
			get
			{
				return this.selectedCellCount;
			}
		}


		/// <summary>
		/// Gets or sets the number of Cells that are selected within the Row
		/// </summary>
		internal int InternalSelectedCellCount
		{
			get
			{
				return this.selectedCellCount;
			}

			set
			{
				this.selectedCellCount = value;
			}
		}


		/// <summary>
		/// Gets whether any Cells within the Row are selected
		/// </summary>
		[Browsable(false)]
		public bool AnyCellsSelected
		{
			get
			{
				return (this.selectedCellCount > 0);
			}
		}


		/// <summary>
		/// Returns whether the Cell at the specified index is selected
		/// </summary>
		/// <param name="index">The index of the Cell in the Row's Row.CellCollection</param>
		/// <returns>True if the Cell at the specified index is selected, 
		/// otherwise false</returns>
		public bool IsCellSelected(int index)
		{
			if (this.Cells.Count == 0)
			{
				return false;
			}

			if (index < 0 || index >= this.Cells.Count)
			{
				return false;
			}

			return this.Cells[index].Selected;
		}


		/// <summary>
		/// Removes the selected state from all the Cells within the Row
		/// </summary>
		internal void ClearSelection()
		{
			this.selectedCellCount = 0;

			for (int i=0; i<this.Cells.Count; i++)
			{
				this.Cells[i].SetSelected(false);
			}
		}


		/// <summary>
		/// Returns an array of Cells that contains all the selected Cells 
		/// within the Row
		/// </summary>
		[Browsable(false)]
		public I3Cell[] SelectedItems
		{
			get
			{
				if (this.SelectedCellCount == 0 || this.Cells.Count == 0)
				{
					return new I3Cell[0];
				}

				I3Cell[] items = new I3Cell[this.SelectedCellCount];
				int count = 0;

				for (int i=0; i<this.Cells.Count; i++)
				{
					if (this.Cells[i].Selected)
					{
						items[count] = this.Cells[i];
						count++;
					}
				}

				return items;
			}
		}


		/// <summary>
		/// Returns an array that contains the indexes of all the selected Cells 
		/// within the Row
		/// </summary>
		[Browsable(false)]
		public int[] SelectedIndicies
		{
			get
			{
				if (this.Cells.Count == 0)
				{
					return new int[0];
				}

				int[] indicies = new int[this.SelectedCellCount];
				int count = 0;

				for (int i=0; i<this.Cells.Count; i++)
				{
					if (this.Cells[i].Selected)
					{
						indicies[count] = i;
						count++;
					}
				}

				return indicies;
			}
		}

        /// <summary>
        /// ��ȡ�������и�
        /// </summary>
        public int Height
        {
            get
            {
                if (height == -1)
                {
                    return this.tableModel.DefaultRowHeight;
                }

                return height;
            }
            set
            {
                if (value < I3TableModel.MinRowHeight_Const)
                {
                    value = I3TableModel.MinRowHeight_Const;
                }
                else if (value > I3TableModel.MaxRowHeight_Const)
                {
                    value = I3TableModel.MaxRowHeight_Const;
                }

                if (this.height != value)
                {
                    this.height = value;

                    //������ø����¼������ͣ�������ϸ����Ϣ���ο��п�ȸı䣩
                    if (this.tableModel != null)
                    {
                        this.tableModel.OnRowHeightChanged(EventArgs.Empty);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ��״̬
        /// Gets the state of the Column
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public I3RowState RowState
        {
            get
            {
                return this.rowState;
            }
        }


        /// <summary>
        /// ��ȡ��������״̬��������State�ı��¼�
        /// Gets or sets the state of the Column
        /// </summary>
        internal I3RowState InternalRowState
        {
            get
            {
                return this.RowState;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3RowState), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3RowState));
                }

                if (this.rowState != value)
                {
                    //RowState oldState = this._rowState;

                    this.rowState = value;

                    this.OnPropertyChanged(new I3RowEventArgs(this, this.index, null, -1, -1, I3RowEventType.StateChanged));
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;

                    this.OnPropertyChanged(new I3RowEventArgs(this, this.index, null, -1, -1, I3RowEventType.StateChanged));
                }
            }
        }

        /// <summary>
        /// ��ȡ������ToolTipText���ԣ�������ToolTipText�ı��¼�
        /// Gets or sets the ToolTip text associated with the Column
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The ToolTip text associated with the Row")]
        public string ToolTipText
        {
            get
            {
                return this.tooltipText;
            }

            set
            {
                if (value == null)
                {
                    value = "";
                }

                if (!value.Equals(this.tooltipText))
                {
                    string oldTip = this.tooltipText;

                    this.tooltipText = value;

                    this.OnPropertyChanged(new I3RowEventArgs(this, I3RowEventType.ToolTipTextChanged));
                }
            }
        }


        /// <summary>
        /// ���ݻ�����ȫ����Ҫ�ĸ߶�
        /// </summary>
        [Browsable(false)]
        public float NeedHeight
        {
            get
            {
                //float result = 0;
                //if (needHeight > 0)
                //{
                //    result = needHeight;
                //}
                //else 
                //{
                //    result = I3TableModel.DefaultRowHeight_Const;
                //}

                float result = needHeight;
                if (result < I3TableModel.MinRowHeight_Const)
                {
                    result = I3TableModel.MinRowHeight_Const;
                }
                if (result > I3TableModel.MaxAutoRowHeight_Const)
                {
                    result = I3TableModel.MaxAutoRowHeight_Const;
                }

                return result;
            }
            set
            {
                needHeight = value;
            }
        }

		#endregion


		#region Events

		/// <summary>
		/// Raises the PropertyChanged event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected virtual void OnPropertyChanged(I3RowEventArgs e)
		{
			e.SetRowIndex(this.Index);
			
			if (this.CanRaiseEvents)
			{
				if (this.TableModel != null)
				{
					this.TableModel.OnRowPropertyChanged(e);
				}
				
				if (PropertyChanged != null)
				{
					PropertyChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the CellAdded event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected internal virtual void OnCellAdded(I3RowEventArgs e)
		{
			e.SetRowIndex(this.Index);
			
			e.Cell.InternalRow = this;
			e.Cell.InternalIndex = e.CellFromIndex;
			e.Cell.SetSelected(false);

			this.UpdateCellIndicies(e.CellFromIndex);
			
			if (this.CanRaiseEvents)
			{
				if (this.TableModel != null)
				{
					this.TableModel.OnCellAdded(e);
				}

				if (CellAdded != null)
				{
					CellAdded(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the CellRemoved event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected internal virtual void OnCellRemoved(I3RowEventArgs e)
		{
			e.SetRowIndex(this.Index);
			
			if (e.Cell != null)
			{
				if (e.Cell.Row == this)
				{
					e.Cell.InternalRow = null;
					e.Cell.InternalIndex = -1;

					if (e.Cell.Selected)
					{
						e.Cell.SetSelected(false);
						
						this.InternalSelectedCellCount--;

						if (this.SelectedCellCount == 0 && this.TableModel != null)
						{
							this.TableModel.Selections.RemoveRow(this);
						}
					}
				}
			}
			else
			{
				if (e.CellFromIndex == -1 && e.CellToIndex == -1)
				{
					if (this.SelectedCellCount != 0 && this.TableModel != null)
					{
						this.InternalSelectedCellCount = 0;
						
						this.TableModel.Selections.RemoveRow(this);
					}
				}
			}

			this.UpdateCellIndicies(e.CellFromIndex);
			
			if (this.CanRaiseEvents)
			{
				if (this.TableModel != null)
				{
					this.TableModel.OnCellRemoved(e);
				}
				
				if (CellRemoved != null)
				{
					CellRemoved(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the CellPropertyChanged event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		internal void OnCellPropertyChanged(I3CellEventArgs e)
		{
			if (this.TableModel != null)
			{
				this.TableModel.OnCellPropertyChanged(e);
			}
		}

		#endregion
	}
}
