


using System;
using System.Collections; 
using System.ComponentModel;
using System.Drawing;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Renderers;
using IE310.Table.Models;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;


namespace IE310.Table.Column
{
	/// <summary>
	/// Summary description for ColumnModel.
	/// </summary>
	[DesignTimeVisible(true)]
    [Designer(typeof(I3ColumnModelDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(I3ColumnModel))]
	public class I3ColumnModel : Component
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when a Column has been added to the ColumnModel
		/// </summary>
		public event I3ColumnModelEventHandler ColumnAdded;

		/// <summary>
		/// Occurs when a Column is removed from the ColumnModel
		/// </summary>
		public event I3ColumnModelEventHandler ColumnRemoved;

		/// <summary>
		/// Occurs when the value of the HeaderHeight property changes
		/// </summary>
		public event EventHandler ColumnHeaderHeightChanged;

        public event ColumnPositionChangedEvent ColumnPositionChanged;

		#endregion
		
		
		#region Class Data

		/// <summary>
		/// The default height of a column header
		/// </summary>
		public static readonly int DefaultColumnHeaderHeight_Const = 20;

		/// <summary>
		/// The minimum height of a column header
		/// </summary>
		public static readonly int MinColumnHeaderHeight_Const = 16;

		/// <summary>
		/// The maximum height of a column header
		/// </summary>
		public static readonly int MaximumColumnHeaderHeight_Const = 128;

        public static readonly int DefaultColumnWidth_Const = 100;
        public static readonly int MinColumnWidth_Const = 20;
        public static readonly int MaxColumnWidth_Const = 2000;
        public static readonly int MaxAutoColumnWidth_Const = 600;
		
		/// <summary>
		/// The collection of Column's contained in the ColumnModel
		/// </summary>
		private I3ColumnCollection _columns;

		/// <summary>
		/// The list of all default CellRenderers used by the Columns in the ColumnModel
		/// </summary>
		private Hashtable _cellRenderers;

		/// <summary>
		/// The list of all default CellEditors used by the Columns in the ColumnModel
		/// </summary>
		private Hashtable cellEditors;

		/// <summary>
		/// The Table that the ColumnModel belongs to
		/// </summary>
		private I3Table _table;

		/// <summary>
		/// The height of the column headers
		/// </summary>
		private int columnHeaderHeight;

        private object userData;

        public object UserData
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
            }
        }

        private object tag;

        public object Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the ColumnModel class with default settings
		/// </summary>
		public I3ColumnModel()
		{
			this.Init();
		}


		/// <summary>
		/// Initializes a new instance of the ColumnModel class with an array of strings 
		/// representing TextColumns
		/// </summary>
		/// <param name="columns">An array of strings that represent the Columns of 
		/// the ColumnModel</param>
		public I3ColumnModel(string[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns", "string[] cannot be null");
			}
			
			this.Init();

			if (columns.Length > 0)
			{
				I3Column[] cols = new I3Column[columns.Length];

				for (int i=0; i<columns.Length; i++)
				{
					cols[i] = new I3TextColumn(columns[i]);
				}

				this.Columns.AddRange(cols);
			}
		}


		/// <summary>
		/// Initializes a new instance of the Row class with an array of Column objects
		/// </summary>
		/// <param name="columns">An array of Cell objects that represent the Columns 
		/// of the ColumnModel</param>
		public I3ColumnModel(I3Column[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns", "Column[] cannot be null");
			}
			
			this.Init();

			if (columns.Length > 0)
			{
				this.Columns.AddRange(columns);
			}
		}


		/// <summary>
		/// Initialise default settings
		/// </summary>
		private void Init()
		{
			this._columns = null;
			
			this._table = null;
			this.columnHeaderHeight = I3ColumnModel.DefaultColumnHeaderHeight_Const;

			this._cellRenderers = new Hashtable();
			this.SetCellRenderer("TEXT", new I3TextCellRenderer());

			this.cellEditors = new Hashtable();
			this.SetCellEditor("TEXT", new I3TextCellEditor());
		}

		#endregion


		#region Methods

		#region Coordinate Translation

		/// <summary>
        /// 根据X轴位置获取列序号（注：位置从0开始，即以DisplayRect为依准）
		/// Returns the index of the Column that lies on the specified position
		/// </summary>
		/// <param name="xPosition">The x-coordinate to check</param>
		/// <returns>The index of the Column or -1 if no Column is found</returns>
		public int ColumnIndexAtDisplayX(int xPosition) 
		{
			if (xPosition < 0 || xPosition > this.VisibleColumnsWidth)
			{
				return -1;
			}

			for (int i=0; i<this.Columns.Count; i++)
			{
				if (this.Columns[i].Visible && xPosition < this.Columns[i].Right)
				{
					return i;
				}
			}

			return -1;
		}


		/// <summary>
		/// Returns the Column that lies on the specified position
		/// </summary>
		/// <param name="xPosition">The x-coordinate to check</param>
		/// <returns>The Column that lies on the specified position, 
		/// or null if not found</returns>
		public I3Column ColumnAtDisplayX(int xPosition) 
		{
			if (xPosition < 0 || xPosition > this.VisibleColumnsWidth)
			{
				return null;
			}
			
			int index = this.ColumnIndexAtDisplayX(xPosition);

			if (index != -1)
			{
				return this.Columns[index];
			}

			return null;
		}


		/// <summary>
		/// Returns a rectangle that countains the header of the column 
		/// at the specified index in the ColumnModel
		/// </summary>
		/// <param name="index">The index of the column</param>
		/// <returns>that countains the header of the specified column</returns>
		public Rectangle ColumnHeaderDisplayRect(int index)
		{
			// make sure the index is valid and the column is not hidden
			if (index < 0 || index >= this.Columns.Count || !this.Columns[index].Visible)
			{
				return Rectangle.Empty;
			}

			return new Rectangle(this.Columns[index].Left, 0, this.Columns[index].Width, this.ColumnHeaderHeight);
		}


		/// <summary>
		/// Returns a rectangle that countains the header of the specified column
		/// </summary>
		/// <param name="column">The column</param>
		/// <returns>A rectangle that countains the header of the specified column</returns>
		public Rectangle ColumnHeaderDisplayRect(I3Column column)
		{
			// check if we actually own the column
			int index = this.Columns.IndexOf(column);
			
			if (index == -1)
			{
				return Rectangle.Empty;
			}

			return this.ColumnHeaderDisplayRect(index);
		}

		#endregion

		#region Dispose

		/// <summary> 
		/// Releases the unmanaged resources used by the ColumnModel and optionally 
		/// releases the managed resources
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				
			}

            while (_columns.Count > 0)
            {
                _columns[0].Dispose();
                _columns.RemoveAt(0);
            }

			base.Dispose(disposing);
		}

		#endregion

		#region Editors

		/// <summary>
		/// Returns the ICellEditor that is associated with the specified name
		/// </summary>
		/// <param name="name">The name thst is associated with an ICellEditor</param>
		/// <returns>The ICellEditor that is associated with the specified name, 
		/// or null if the name or ICellEditor do not exist</returns>
		public II3CellEditor GetCellEditor(string name)
		{
			if (name == null || name.Length == 0)
			{
				return null;
			}
			
			name = name.ToUpper();
			
			if (!this.cellEditors.ContainsKey(name))
			{
				if (this.cellEditors.Count == 0)
				{
					this.SetCellEditor("TEXT", new I3TextCellEditor());
				}
				
				return null;
			}

			return (II3CellEditor) this.cellEditors[name];
		}


		/// <summary>
		/// Gets the ICellEditor for the Column at the specified index in the 
		/// ColumnModel
		/// </summary>
		/// <param name="column">The index of the Column in the ColumnModel for 
		/// which an ICellEditor will be retrieved</param>
		/// <returns>The ICellEditor for the Column at the specified index, or 
		/// null if the editor does not exist</returns>
		public II3CellEditor GetCellEditor(int column)
		{
			if (column < 0 || column >= this.Columns.Count)
			{
				return null;
			}

			//
			if (this.Columns[column].Editor != null)
			{
				return this.Columns[column].Editor;
			}

			return this.GetCellEditor(this.Columns[column].GetDefaultEditorName());
		}


		/// <summary>
		/// Associates the specified ICellRenderer with the specified name
		/// </summary>
		/// <param name="name">The name to be associated with the specified ICellEditor</param>
		/// <param name="editor">The ICellEditor to be added to the ColumnModel</param>
		public void SetCellEditor(string name, II3CellEditor editor)
		{
			if (name == null || name.Length == 0 || editor == null)
			{
				return;
			}
			
			name = name.ToUpper();
			
			if (this.cellEditors.ContainsKey(name))
			{	
				this.cellEditors.Remove(name);
				
				this.cellEditors[name] = editor;
			}
			else
			{
				this.cellEditors.Add(name, editor);
			}
		}


		/// <summary>
		/// Gets whether the ColumnModel contains an ICellEditor with the 
		/// specified name
		/// </summary>
		/// <param name="name">The name associated with the ICellEditor</param>
		/// <returns>true if the ColumnModel contains an ICellEditor with the 
		/// specified name, false otherwise</returns>
		public bool ContainsCellEditor(string name)
		{
			if (name == null)
			{
				return false;
			}

			return this.cellEditors.ContainsKey(name);
		}


		/// <summary>
		/// Gets the number of ICellEditors contained in the ColumnModel
		/// </summary>
		internal int EditorCount
		{
			get
			{
				return this.cellEditors.Count;
			}
		}

		#endregion

		#region Renderers

		/// <summary>
		/// Returns the ICellRenderer that is associated with the specified name
		/// </summary>
		/// <param name="name">The name thst is associated with an ICellEditor</param>
		/// <returns>The ICellRenderer that is associated with the specified name, 
		/// or null if the name or ICellRenderer do not exist</returns>
		public II3CellRenderer GetCellRenderer(string name)
		{
			if (name == null)
			{
				name = "TEXT";
			}
			
			name = name.ToUpper();
			
			if (!this._cellRenderers.ContainsKey(name))
			{
				if (this._cellRenderers.Count == 0)
				{
					this.SetCellRenderer("TEXT", new I3TextCellRenderer());
				}
				
				return (II3CellRenderer) this._cellRenderers["TEXT"];
			}

			return (II3CellRenderer) this._cellRenderers[name];
		}


		/// <summary>
		/// Gets the ICellRenderer for the Column at the specified index in the 
		/// ColumnModel
		/// </summary>
		/// <param name="column">The index of the Column in the ColumnModel for 
		/// which an ICellRenderer will be retrieved</param>
		/// <returns>The ICellRenderer for the Column at the specified index, or 
		/// null if the renderer does not exist</returns>
		public II3CellRenderer GetCellRenderer(int column)
		{
			//
			if (column < 0 || column >= this.Columns.Count)
			{
				return null;
			}

			//
			if (this.Columns[column].Renderer != null)
			{
				return this.Columns[column].Renderer;
			}

			//
			return this.GetCellRenderer(this.Columns[column].GetDefaultRendererName());
		}


		/// <summary>
		/// Associates the specified ICellRenderer with the specified name
		/// </summary>
		/// <param name="name">The name to be associated with the specified ICellRenderer</param>
		/// <param name="renderer">The ICellRenderer to be added to the ColumnModel</param>
		public void SetCellRenderer(string name, II3CellRenderer renderer)
		{
			if (name == null || renderer == null)
			{
				return;
			}
			
			name = name.ToUpper();
			
			if (this._cellRenderers.ContainsKey(name))
			{	
				this._cellRenderers.Remove(name);
				
				this._cellRenderers[name] = renderer;
			}
			else
			{
				this._cellRenderers.Add(name, renderer);
			}
		}


		/// <summary>
		/// Gets whether the ColumnModel contains an ICellRenderer with the 
		/// specified name
		/// </summary>
		/// <param name="name">The name associated with the ICellRenderer</param>
		/// <returns>true if the ColumnModel contains an ICellRenderer with the 
		/// specified name, false otherwise</returns>
		public bool ContainsCellRenderer(string name)
		{
			if (name == null)
			{
				return false;
			}

			return this._cellRenderers.ContainsKey(name);
		}


		/// <summary>
		/// Gets the number of ICellRenderers contained in the ColumnModel
		/// </summary>
		internal int RendererCount
		{
			get
			{
				return this._cellRenderers.Count;
			}
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Returns the index of the first visible Column that is to the 
		/// left of the Column at the specified index in the ColumnModel
		/// </summary>
		/// <param name="index">The index of the Column for which the first 
		/// visible Column that is to the left of the specified Column is to 
		/// be found</param>
		/// <returns>the index of the first visible Column that is to the 
		/// left of the Column at the specified index in the ColumnModel, or 
		/// -1 if the Column at the specified index is the first visible column, 
		/// or there are no Columns in the Column model</returns>
		public int PreviousVisibleColumn(int index)
		{
			if (this.Columns.Count == 0)
			{
				return -1;
			}
			
			if (index <= 0)
			{
				return -1;
			}

			if (index >= this.Columns.Count)
			{
				if (this.Columns[this.Columns.Count-1].Visible)
				{
					return this.Columns.Count - 1;
				}
				
				index = this.Columns.Count - 1;
			}

			for (int i=index; i>0; i--)
			{
				if (this.Columns[i-1].Visible)
				{
					return i - 1;
				}
			}

			return -1;
		}


		/// <summary>
		/// Returns the index of the first visible Column that is to the 
		/// right of the Column at the specified index in the ColumnModel
		/// </summary>
		/// <param name="index">The index of the Column for which the first 
		/// visible Column that is to the right of the specified Column is to 
		/// be found</param>
		/// <returns>the index of the first visible Column that is to the 
		/// right of the Column at the specified index in the ColumnModel, or 
		/// -1 if the Column at the specified index is the last visible column, 
		/// or there are no Columns in the Column model</returns>
		public int NextVisibleColumn(int index)
		{
			if (this.Columns.Count == 0)
			{
				return -1;
			}
			
			if (index >= this.Columns.Count - 1)
			{
				return -1;
			}

			for (int i=index; i<this.Columns.Count-1; i++)
			{
				if (this.Columns[i+1].Visible)
				{
					return i + 1;
				}
			}

			return -1;
		}

		#endregion



        #region columns

        /// <summary>
        /// 替换两个列的位置
        /// </summary>
        /// <param name="moveColumn"></param>
        /// <param name="columnAfterNewIndex"></param>
        public void MoveColumn(I3Column moveColumn, I3Column columnAfterNewIndex)
        {
            MoveColumn(this._columns.IndexOf(moveColumn), this._columns.IndexOf(columnAfterNewIndex));
        }

        /// <summary>
        /// 替换两个列的位置
        /// </summary>
        /// <param name="moveColumnIndex"></param>
        /// <param name="newIndex"></param>
        public void MoveColumn(int moveColumnIndex, int newIndex)
        {
            if (moveColumnIndex < 0 || moveColumnIndex > this._columns.Count - 1)
            {
                throw new Exception("无法移动列，moveColumnIndex:" + moveColumnIndex.ToString() + "错误");
            }
            if (newIndex < 0 /*|| newIndex > this._columns.Count - 1*/)
            {
                throw new Exception("无法移动列，newIndex:" + newIndex.ToString() + "错误");
            }
            if (moveColumnIndex == newIndex)
            {
                throw new Exception("无法移动列，moveColumnIndex:" + newIndex.ToString() + "与columnIndexAfterNewIndex相等");
            }

            I3Column moveColumn = this._columns[moveColumnIndex];
            this._columns.Remove(moveColumn);
            this._columns.Insert(newIndex, moveColumn);

            this.OnColumnPositionChanged(moveColumnIndex, newIndex);
        }

        #endregion

        #endregion


        #region Properties

        /// <summary>
		/// A ColumnCollection representing the collection of 
		/// Columns contained within the ColumnModel
		/// </summary>
        [MergableProperty(false)]
		[Category("Behavior"),
		Description("Column Collection"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(I3ColumnCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public I3ColumnCollection Columns
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = new I3ColumnCollection(this);
				}
				
				return this._columns;
			}
		}


		/// <summary>
		/// Gets or sets the height of the column headers
		/// </summary>
		[Category("Appearance"),
		Description("The height of the column headers")]
		public int ColumnHeaderHeight
		{
			get
			{
				return this.columnHeaderHeight;
			}
			set
			{
				if (value < I3ColumnModel.MinColumnHeaderHeight_Const)
				{
					value = I3ColumnModel.DefaultColumnHeaderHeight_Const;
				}
				else if (value > I3ColumnModel.MaximumColumnHeaderHeight_Const)
				{
					value = I3ColumnModel.MaximumColumnHeaderHeight_Const;
				}
				
				if (this.columnHeaderHeight != value)
				{
					this.columnHeaderHeight = value;

					this.OnColumnHeaderHeightChanged(EventArgs.Empty);
				}
			}
		}


		/// <summary>
		/// Specifies whether the HeaderHeight property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the HeaderHeight property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeHeaderHeight()
		{
			return this.columnHeaderHeight != I3ColumnModel.DefaultColumnHeaderHeight_Const;
		}


		/// <summary>
		/// Gets a rectangle that specifies the width and height of all the 
		/// visible column headers in the model
		/// </summary>
		[Browsable(false)]
		public Rectangle HeaderRect
		{
			get
			{
				if (this.VisibleColumnCount == 0)
				{
					return Rectangle.Empty;
				}
				
				return new Rectangle(0, 0, this.VisibleColumnsWidth, this.ColumnHeaderHeight);
			}
		}


		/// <summary>
		/// Gets the total width of all the Columns in the model
		/// </summary>
		[Browsable(false)]
		public int TotalColumnsWidth
		{
			get
			{
				return this.Columns.TotalColumnWidth;
			}
		}


		/// <summary>
		/// Gets the total width of all the visible Columns in the model
		/// </summary>
		[Browsable(false)]
		public int VisibleColumnsWidth
		{
			get
			{
				return this.Columns.VisibleColumnsWidth;
			}
		}


		/// <summary>
		/// Gets the index of the last Column that is not hidden
		/// </summary>
		[Browsable(false)]
		public int LastVisibleColumnIndex
		{
			get
			{
				return this.Columns.LastVisibleColumn;
			}
		}

        /// <summary>
        /// Gets the index of the first Column that is not hidden
        /// </summary>
        [Browsable(false)]
        public int FirstVisibleColumnIndex
        {
            get
            {
                return this.Columns.FirstVisibleColumn;
            }
        }


		/// <summary>
		/// Gets the number of Columns in the ColumnModel that are visible
		/// </summary>
		[Browsable(false)]
		public int VisibleColumnCount
		{
			get
			{
				return this.Columns.VisibleColumnCount;
			}
		}


		/// <summary>
		/// Gets the Table the ColumnModel belongs to
		/// </summary>
		[Browsable(false)]
		public I3Table Table
		{
			get
			{
				return this._table;
			}
		}


		/// <summary>
		/// Gets or sets the Table the ColumnModel belongs to
		/// </summary>
		internal I3Table InternalTable
		{
			get
			{
				return this._table;
			}

			set
			{
				this._table = value;
			}
		}


		/// <summary>
		/// Gets whether the ColumnModel is able to raise events
		/// </summary>
		protected internal bool CanRaiseEvents
		{
			get
			{
				// check if the Table that the ColumModel belongs to is able to 
				// raise events (if it can't, the ColumModel shouldn't raise 
				// events either)
				if (this.Table != null)
				{
					return this.Table.CanRaiseEvents;
				}

				return true;
			}
		}


		/// <summary>
		/// Gets whether the ColumnModel is enabled
		/// </summary>
		internal bool Enabled
		{
			get
			{
				if (this.Table == null)
				{
					return true;
				}

				return this.Table.Enabled;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the ColumnAdded event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected internal virtual void OnColumnAdded(I3ColumnModelEventArgs e)
		{
			e.Column.ColumnModel = this;

			if (!this.ContainsCellRenderer(e.Column.GetDefaultRendererName()))
			{
				this.SetCellRenderer(e.Column.GetDefaultRendererName(), e.Column.CreateDefaultRenderer());
			}

			if (!this.ContainsCellEditor(e.Column.GetDefaultEditorName()))
			{
				this.SetCellEditor(e.Column.GetDefaultEditorName(), e.Column.CreateDefaultEditor());
			}

			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnAdded(e);
				}
				
				if (ColumnAdded != null)
				{
					ColumnAdded(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the ColumnRemoved event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected internal virtual void OnColumnRemoved(I3ColumnModelEventArgs e)
		{
			if (e.Column != null && e.Column.ColumnModel == this)
			{
				e.Column.ColumnModel = null;
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnRemoved(e);
				}
				
				if (ColumnRemoved != null)
				{
					ColumnRemoved(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the HeaderHeightChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnColumnHeaderHeightChanged(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnHeaderHeightChanged(e);
				}
				
				if (ColumnHeaderHeightChanged != null)
				{
					ColumnHeaderHeightChanged(this, e);
				}
			}
		}


		/// <summary>
		/// Raises the ColumnPropertyChanged event
		/// </summary>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		internal void OnColumnPropertyChanged(I3ColumnEventArgs e)
		{
			if (e.EventType == I3ColumnEventType.WidthChanged || e.EventType == I3ColumnEventType.VisibleChanged)
			{
				this.Columns.RecalcWidthCache();
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.Table != null)
				{
					this.Table.OnColumnPropertyChanged(e);
				}
			}
		}




        private void OnColumnPositionChanged(int oldIndex, int newIndex)
        {
            if (this.ColumnPositionChanged != null)
            {
                this.ColumnPositionChanged(new ColumnPositionChangedEventArgs(oldIndex, newIndex));
            }
        }

		#endregion
	}


    public class I3ColumnModelDesigner : ComponentDesigner
    {
        //获取与组件关联的子组件集合
        public override ICollection AssociatedComponents
        {
            get
            {
                I3ColumnModel cm = base.Component as I3ColumnModel;
                if (cm != null)
                {
                    return cm.Columns;
                }

                return base.AssociatedComponents;
            }
        }
    }

    public class ColumnPositionChangedEventArgs : EventArgs
    {
        public ColumnPositionChangedEventArgs(int moveColumnIndex, int newIndex)
        {
            this.moveColumnIndex = moveColumnIndex;
            this.newIndex = newIndex;
        }

        private int moveColumnIndex;
        /// <summary>
        /// 此序号是数组中的序号，从0到count-1
        /// </summary>
        public int MoveColumnIndex
        {
            get
            {
                return moveColumnIndex;
            }
            set
            {
                moveColumnIndex = value;
            }
        }

        private int newIndex;
        public int NewIndex
        {
            get
            {
                return newIndex;
            }
            set
            {
                newIndex = value;
            }
        }
    }

    public delegate void ColumnPositionChangedEvent(ColumnPositionChangedEventArgs args);
}
