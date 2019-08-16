

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;

using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Models;
using IE310.Table.Cell;
using System.Windows.Forms;
using System.Collections.Generic;
using IE310.Table.Column;


namespace IE310.Table.Row
{
    /// <summary>
    /// Represents a collection of Rows and Cells displayed in a Table.
    /// </summary>
    [DesignTimeVisible(true),
    ToolboxItem(true),
    ToolboxBitmap(typeof(I3TableModel))]
    public class I3TableModel : Component
    {
        #region Event Handlers

        /// <summary>
        /// Occurs when a Row is added to the TableModel
        /// </summary>
        public event I3TableModelEventHandler RowAdded;

        /// <summary>
        /// Occurs when a Row is removed from the TableModel
        /// </summary>
        public event I3TableModelEventHandler RowRemoved;

        /// <summary>
        /// Occurs when the value of the TableModel Selection property changes
        /// </summary>
        public event I3SelectionEventHandler SelectionChanged;

        /// <summary>
        /// Occurs when the value of the RowHeight property changes
        /// </summary>
        public event EventHandler RowHeightChanged;

        /// <summary>
        /// 行头宽度改变事件
        /// </summary>
        public event EventHandler RowHeaderWidthChanged;
        #endregion


        #region Class Data

        /// <summary>
        /// The default height of a Row
        /// </summary>
        public static readonly int DefaultRowHeight_Const = 20;

        /// <summary>
        /// The minimum height of a Row
        /// </summary>
        public static readonly int MinRowHeight_Const = 14;

        /// <summary>
        /// The maximum height of a Row
        /// </summary>
        public static readonly int MaxRowHeight_Const = 1024;


        public static readonly int MaxAutoRowHeight_Const = 600;

        /// <summary>
        /// 默认行头宽度
        /// </summary>
        public static readonly int DefaultRowHeaderWidth_Const = 30;

        /// <summary>
        /// 最小行头宽度
        /// </summary>
        public static readonly int MinRowHeaderWidth_Const = 16;

        /// <summary>
        /// 最大行头宽度
        /// </summary>
        public static readonly int MaxRowHeaderWidth_Const = 128;

        /// <summary>
        /// The collection of Rows's contained in the TableModel
        /// </summary>
        private I3RowCollection _rows;

        /// <summary>
        /// The Table that the TableModel belongs to
        /// </summary>
        private I3Table _table;

        /// <summary>
        /// The currently selected Rows and Cells
        /// </summary>
        private Selection _selection;

        /// <summary>
        /// The height of each Row in the TableModel
        /// </summary>
        //private int rowHeight;
        private int _defaultRowHeight;

        /// <summary>
        /// 行头的宽度
        /// </summary>
        private int _rowHeaderWidth;

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
        /// Initializes a new instance of the TableModel class with default settings
        /// </summary>
        public I3TableModel()
        {
            this.Init();
        }


        /// <summary>
        /// Initializes a new instance of the TableModel class with an array of Row objects
        /// </summary>
        /// <param name="rows">An array of Row objects that represent the Rows 
        /// of the TableModel</param>
        public I3TableModel(I3Row[] rows)
        {
            if (rows == null)
            {
                throw new ArgumentNullException("rows", "Row[] cannot be null");
            }

            this.Init();

            if (rows.Length > 0)
            {
                this.Rows.AddRange(rows);
            }
        }


        /// <summary>
        /// Initialise default settings
        /// </summary>
        private void Init()
        {
            this._rows = null;

            this._selection = new Selection(this);
            this._table = null;
            this._defaultRowHeight = I3TableModel.DefaultRowHeight_Const;

            this._rowHeaderWidth = I3TableModel.DefaultRowHeaderWidth_Const;
        }

        #endregion


        #region Methods

        /// <summary> 
        /// Releases the unmanaged resources used by the TableModel and optionally 
        /// releases the managed resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }


        /// <summary>
        /// Returns the index of the Row that lies on the specified position
        /// </summary>
        /// <param name="yPosition">The y-coordinate to check</param>
        /// <returns>The index of the Row at the specified position or -1 if 
        /// no Row is found</returns>
        public int RowIndexAtDisplayY(int yPosition)
        {
            //int row = yPosition / this.RowHeight;

            //if (row < 0 || row > this.Rows.Count - 1)
            //{
            //    return -1;
            //}
            //return row;

            if (yPosition < 0 || yPosition > this.TotalRowHeight)
            {
                return -1;
            }

            int total = 0;
            for (int i = 0; i <= this.Rows.Count - 1; i++)
            {
                total = total + this.Rows[i].Height;
                if (yPosition < total)
                {
                    return i;
                }
            }

            return -1;
        }


        #endregion


        #region Properties

        /// <summary>
        /// Gets the Cell located at the specified row index and column index
        /// </summary>
        /// <param name="row">The row index of the Cell</param>
        /// <param name="column">The column index of the Cell</param>
        [Browsable(false)]
        public I3Cell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.Rows.Count)
                {
                    return null;
                }

                if (column < 0 || column >= this.Rows[row].Cells.Count)
                {
                    return null;
                }

                return this.Rows[row].Cells[column];
            }
        }


        /// <summary>
        /// Gets the Cell located at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        [Browsable(false)]
        public I3Cell this[I3CellPos cellPos]
        {
            get
            {
                return this[cellPos.Row, cellPos.Column];
            }
        }


        /// <summary>
        /// A TableModel.RowCollection representing the collection of 
        /// Rows contained within the TableModel
        /// </summary>
        [MergableProperty(false)]
        [Category("Behavior")]
        [Description("Row Collection")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(I3RowCollectionEditor), typeof(UITypeEditor))]
        public I3RowCollection Rows
        {
            get
            {
                if (this._rows == null)
                {
                    this._rows = new I3RowCollection(this);
                }

                return this._rows;
            }
        }


        /// <summary>
        /// A TableModel.Selection representing the collection of selected
        /// Rows and Cells contained within the TableModel
        /// </summary>
        [Browsable(false)]
        public Selection Selections
        {
            get
            {
                if (this._selection == null)
                {
                    this._selection = new Selection(this);
                }

                return this._selection;
            }
        }


        /// <summary>
        /// 获取或设置默认行高
        /// Gets or sets the default height of each Row in the TableModel
        /// </summary>
        [Category("Appearance"),
        Description("The height of each row")]
        public int DefaultRowHeight
        {
            get
            {
                return this._defaultRowHeight;
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

                if (this._defaultRowHeight != value)
                {
                    this._defaultRowHeight = value;

                    this.OnRowHeightChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the column headers
        /// </summary>
        [Category("Appearance"),
        DefaultValue(30),
        Description("The width of the row headers")]
        public int RowHeaderWidth
        {
            get
            {
                return this._rowHeaderWidth;
            }

            set
            {
                if (value < I3TableModel.MinRowHeaderWidth_Const)
                {
                    value = I3TableModel.DefaultRowHeaderWidth_Const;
                }
                else if (value > I3TableModel.MaxRowHeaderWidth_Const)
                {
                    value = I3TableModel.MaxRowHeaderWidth_Const;
                }

                if (this._rowHeaderWidth != value)
                {
                    this._rowHeaderWidth = value;

                    this.OnRowHeaderWidthChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Specifies whether the RowHeight property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the RowHeight property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeEachRowHeight()
        {
            return this._defaultRowHeight != I3TableModel.DefaultRowHeight_Const;
        }


        /// <summary>
        /// 获取所有行的行高之和  (需要改为行高改变时自动计算，而不是每次重绘都获取)
        /// Gets the total height of all the Rows in the TableModel
        /// </summary>
        [Browsable(false)]
        public int TotalRowHeight
        {
            get
            {
                int total = 0;
                foreach (I3Row row in this.Rows)
                {
                    total = total + row.Height;
                }
                return total;

                //return this.Rows.Count * this.RowHeight;
            }
        }




        /// <summary>
        /// Gets the Table the TableModel belongs to
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
        /// Gets or sets the Table the TableModel belongs to
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
        /// Gets whether the TableModel is able to raise events
        /// </summary>
        protected internal bool CanRaiseEvents
        {
            get
            {
                // check if the Table that the TableModel belongs to is able to 
                // raise events (if it can't, the TableModel shouldn't raise 
                // events either)
                if (this.Table != null)
                {
                    return this.Table.CanRaiseEvents;
                }

                return true;
            }
        }


        /// <summary>
        /// Gets whether the TableModel is enabled
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


        /// <summary>
        /// Updates the Row's Index property so that it matches the Rows 
        /// position in the RowCollection
        /// </summary>
        /// <param name="start">The index to start updating from</param>
        internal void UpdateRowIndicies(int start)
        {
            if (start == -1)
            {
                start = 0;
            }

            for (int i = start; i < this.Rows.Count; i++)
            {
                this.Rows[i].InternalIndex = i;
            }
        }

        #endregion


        #region Events

        /// <summary>
        /// Raises the RowAdded event
        /// </summary>
        /// <param name="e">A TableModelEventArgs that contains the event data</param>
        protected internal virtual void OnRowAdded(I3TableModelEventArgs e)
        {
            e.Row.InternalTableModel = this;
            e.Row.InternalIndex = e.RowFromIndex;
            e.Row.ClearSelection();

            this.UpdateRowIndicies(e.RowFromIndex);

            if (this.CanRaiseEvents)
            {
                if (this.Table != null)
                {
                    this.Table.OnRowAdded(e);
                }

                if (RowAdded != null)
                {
                    RowAdded(this, e);
                }
            }
        }


        /// <summary>
        /// Raises the RowRemoved event
        /// </summary>
        /// <param name="e">A TableModelEventArgs that contains the event data</param>
        protected internal virtual void OnRowRemoved(I3TableModelEventArgs e)
        {
            if (e.Row != null && e.Row.TableModel == this)
            {
                e.Row.InternalTableModel = null;
                e.Row.InternalIndex = -1;

                if (e.Row.AnyCellsSelected)
                {
                    e.Row.ClearSelection();

                    this.Selections.RemoveRow(e.Row);
                }
            }

            this.UpdateRowIndicies(e.RowFromIndex);

            if (this.CanRaiseEvents)
            {
                if (this.Table != null)
                {
                    this.Table.OnRowRemoved(e);
                }

                if (RowRemoved != null)
                {
                    RowRemoved(this, e);
                }
            }
        }


        /// <summary>
        /// Raises the SelectionChanged event
        /// </summary>
        /// <param name="e">A SelectionEventArgs that contains the event data</param>
        protected virtual void OnSelectionChanged(I3SelectionEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.Table != null)
                {
                    this.Table.OnSelectionChanged(e);
                }

                if (SelectionChanged != null)
                {
                    SelectionChanged(this, e);
                }
            }
        }


        /// <summary>
        /// Raises the RowHeightChanged event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        public virtual void OnRowHeightChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.Table != null)
                {
                    this.Table.OnRowHeightChanged(e);
                }

                if (RowHeightChanged != null)
                {
                    RowHeightChanged(this, e);
                }
            }
        }


        /// <summary>
        /// Raises the RowPropertyChanged event
        /// </summary>
        /// <param name="e">A RowEventArgs that contains the event data</param>
        internal void OnRowPropertyChanged(I3RowEventArgs e)
        {
            if (this.Table != null)
            {
                this.Table.OnRowPropertyChanged(e);
            }
        }


        /// <summary>
        /// Raises the CellAdded event
        /// </summary>
        /// <param name="e">A RowEventArgs that contains the event data</param>
        internal void OnCellAdded(I3RowEventArgs e)
        {
            if (this.Table != null)
            {
                this.Table.OnCellAdded(e);
            }
        }


        /// <summary>
        /// Raises the CellRemoved event
        /// </summary>
        /// <param name="e">A RowEventArgs that contains the event data</param>
        internal void OnCellRemoved(I3RowEventArgs e)
        {
            if (this.Table != null)
            {
                this.Table.OnCellRemoved(e);
            }
        }


        /// <summary>
        /// Raises the CellPropertyChanged event
        /// </summary>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        internal void OnCellPropertyChanged(I3CellEventArgs e)
        {
            if (this.Table != null)
            {
                this.Table.OnCellPropertyChanged(e);
            }
        }


        /// <summary>
        /// Raises the HeaderHeightChanged event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected virtual void OnRowHeaderWidthChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.Table != null)
                {
                    this.Table.OnRowHeaderWidthChanged(e);
                }

                if (RowHeaderWidthChanged != null)
                {
                    RowHeaderWidthChanged(this, e);
                }
            }
        }

        #endregion


        #region Selection

        /// <summary>
        /// 选择的行和其中的列
        /// Represents the collection of selected Rows and Cells in a TableModel.
        /// </summary>
        public class Selection
        {
            #region Class Data

            /// <summary>
            /// The TableModel that owns the Selection
            /// </summary>
            private I3TableModel owner;

            /// <summary>
            /// The list of Rows that have selected Cells
            /// </summary>
            private ArrayList rows;

            /// <summary>
            /// The starting cell of a selection that uses the shift key
            /// </summary>
            private I3CellPos shiftSelectStart;

            /// <summary>
            /// The ending cell of a selection that uses the shift key
            /// </summary>
            private I3CellPos shiftSelectEnd;

            private I3Row lastSelectRow;
            private I3Column lastSelectColumn;

            #endregion


            #region Constructor

            /// <summary>
            /// Initializes a new instance of the TableModel.Selection class 
            /// that belongs to the specified TableModel
            /// </summary>
            /// <param name="owner">A TableModel representing the tableModel that owns 
            /// the Selection</param>
            public Selection(I3TableModel owner)
            {
                if (owner == null)
                {
                    throw new ArgumentNullException("owner", "owner cannot be null");
                }

                this.owner = owner;
                this.rows = new ArrayList();
                this.lastSelectRow = null;
                this.lastSelectColumn = null;

                this.shiftSelectStart = I3CellPos.Empty;
                this.shiftSelectEnd = I3CellPos.Empty;
            }

            #endregion


            #region Methods

            #region Add

            /// <summary>
            /// Replaces the currently selected Cells with the Cell at the specified 
            /// row and column indexes
            /// </summary>
            /// <param name="row">The row index of the Cell to be selected</param>
            /// <param name="column">The column index of the Cell to be selected</param>
            public void SelectCell(int row, int column)
            {
                // don't bother going any further if the cell 
                // is already selected
                if (this.rows.Count == 1)
                {
                    I3Row r = (I3Row)this.rows[0];

                    if (r.InternalIndex == row && r.SelectedCellCount == 1)
                    {
                        if (column >= 0 && column < r.Cells.Count)
                        {
                            if (r.Cells[column].Selected)
                            {
                                return;
                            }
                        }
                    }
                }

                this.SelectCells(row, column, row, column);
            }


            /// <summary>
            /// Replaces the currently selected Cells with the Cell at the specified CellPos
            /// </summary>
            /// <param name="cellPos">A CellPos thst specifies the row and column indicies of 
            /// the Cell to be selected</param>
            public void SelectCell(I3CellPos cellPos)
            {
                this.SelectCell(cellPos.Row, cellPos.Column);
            }


            /// <summary>
            /// Replaces the currently selected Cells with the Cells located between the specified 
            /// start and end row/column indicies
            /// </summary>
            /// <param name="startRow">The row index of the start Cell</param>
            /// <param name="startColumn">The column index of the start Cell</param>
            /// <param name="endRow">The row index of the end Cell</param>
            /// <param name="endColumn">The column index of the end Cell</param>
            public void SelectCells(int startRow, int startColumn, int endRow, int endColumn)
            {
                int[] oldSelectedIndicies = this.SelectedIndicies;

                this.InternalClear();

                if (this.InternalAddCells(startRow, startColumn, endRow, endColumn))
                {
                    this.owner.OnSelectionChanged(new I3SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
                }

                this.shiftSelectStart = new I3CellPos(startRow, startColumn);
                this.shiftSelectEnd = new I3CellPos(endRow, endColumn);
            }


            /// <summary>
            /// Replaces the currently selected Cells with the Cells located between the specified 
            /// start and end CellPos
            /// </summary>
            /// <param name="start">A CellPos that specifies the start Cell</param>
            /// <param name="end">A CellPos that specifies the end Cell</param>
            public void SelectCells(I3CellPos start, I3CellPos end)
            {
                this.SelectCells(start.Row, start.Column, end.Row, end.Column);
            }


            /// <summary>
            /// Adds the Cell at the specified row and column indicies to the current selection
            /// </summary>
            /// <param name="row">The row index of the Cell to add to the selection</param>
            /// <param name="column">The column index of the Cell to add to the selection</param>
            public void AddCell(int row, int column)
            {
                this.AddCells(row, column, row, column);
            }


            /// <summary>
            /// Adds the Cell at the specified row and column indicies to the current selection
            /// </summary>
            /// <param name="cellPos">A CellPos that specifies the Cell to add to the selection</param>
            public void AddCell(I3CellPos cellPos)
            {
                this.AddCell(cellPos.Row, cellPos.Column);
            }


            /// <summary>
            /// Adds the Cells located between the specified start and end row/column indicies 
            /// to the current selection
            /// </summary>
            /// <param name="startRow">The row index of the start Cell</param>
            /// <param name="startColumn">The column index of the start Cell</param>
            /// <param name="endRow">The row index of the end Cell</param>
            /// <param name="endColumn">The column index of the end Cell</param>
            public void AddCells(int startRow, int startColumn, int endRow, int endColumn)
            {
                int[] oldSelectedIndicies = this.SelectedIndicies;

                if (InternalAddCells(startRow, startColumn, endRow, endColumn))
                {
                    this.owner.OnSelectionChanged(new I3SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
                }

                this.shiftSelectStart = new I3CellPos(startRow, startColumn);
                this.shiftSelectEnd = new I3CellPos(endRow, endColumn);
            }


            /// <summary>
            /// Adds the Cells located between the specified start and end CellPos to the
            /// current selection
            /// </summary>
            /// <param name="start">A CellPos that specifies the start Cell</param>
            /// <param name="end">A CellPos that specifies the end Cell</param>
            public void AddCells(I3CellPos start, I3CellPos end)
            {
                this.AddCells(start.Row, start.Column, end.Row, end.Column);
            }


            /// <summary>
            /// Adds the Cells located between the specified start and end CellPos to the
            /// current selection without raising an event
            /// </summary>
            /// <param name="start">A CellPos that specifies the start Cell</param>
            /// <param name="end">A CellPos that specifies the end Cell</param>
            /// <returns>true if any Cells were added, false otherwise</returns>
            private bool InternalAddCells(I3CellPos start, I3CellPos end)
            {
                return this.InternalAddCells(start.Row, start.Column, end.Row, end.Column);
            }


            /// <summary>
            /// Adds the Cells located between the specified start and end row/column indicies 
            /// to the current selection without raising an event
            /// </summary>
            /// <param name="startRow">The row index of the start Cell</param>
            /// <param name="startColumn">The column index of the start Cell</param>
            /// <param name="endRow">The row index of the end Cell</param>
            /// <param name="endColumn">The column index of the end Cell</param>
            /// <returns>true if any Cells were added, false otherwise</returns>
            private bool InternalAddCells(int startRow, int startColumn, int endRow, int endColumn)
            {
                this.Normalise(ref startRow, ref endRow);
                this.Normalise(ref startColumn, ref endColumn);

                bool anyAdded = false;
                bool anyAddedInRow = false;

                for (int i = startRow; i <= endRow; i++)
                {
                    if (i >= this.owner.Rows.Count)
                    {
                        break;
                    }

                    I3Row r = this.owner.Rows[i];

                    for (int j = startColumn; j <= endColumn; j++)
                    {
                        if (j >= r.Cells.Count)
                        {
                            break;
                        }

                        if (!r.Cells[j].Selected && r.Cells[j].Enabled)
                        {
                            if (this.owner.Table != null && !this.owner.Table.IsCellEnabled(i, j))
                            {
                                continue;
                            }

                            r.Cells[j].SetSelected(true);
                            r.InternalSelectedCellCount++;

                            anyAdded = true;
                            anyAddedInRow = true;
                        }
                    }

                    if (anyAddedInRow && !this.rows.Contains(r))
                    {
                        this.rows.Add(r);
                    }

                    anyAddedInRow = false;
                }

                //
                this.InnerLastSelectRow = this.owner.Rows[endRow];


                return anyAdded;
            }


            /// <summary>
            /// 增加cells:从lastSelectionCell到currentSelectionCell，不在此区域的都将被移除
            /// lastSelection是通过shiftSelectStart和shiftSelectEnd来记录的
            /// Adds the Cells between the last selection start Cell and the Cell at the 
            /// specified row/column indicies to the current selection.  Any Cells that are 
            /// between the last start and end Cells that are not in the new area are 
            /// removed from the current selection
            /// </summary>
            /// <param name="row">The row index of the shift selected Cell</param>
            /// <param name="column">The column index of the shift selected Cell</param>
            public void AddShiftSelectedCell(int row, int column)
            {
                int[] oldSelectedIndicies = this.SelectedIndicies;

                if (this.shiftSelectStart == I3CellPos.Empty)
                {
                    this.shiftSelectStart = new I3CellPos(0, 0);
                }

                bool changed = false;

                if (this.shiftSelectEnd != I3CellPos.Empty)
                {
                    changed = this.InternalRemoveCells(this.shiftSelectStart, this.shiftSelectEnd);
                    changed |= this.InternalAddCells(this.shiftSelectStart, new I3CellPos(row, column));
                }
                else
                {
                    changed = this.InternalAddCells(0, 0, row, column);
                }

                if (changed)
                {
                    this.owner.OnSelectionChanged(new I3SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
                }

                this.shiftSelectEnd = new I3CellPos(row, column);
            }


            /// <summary>
            /// Adds the Cells between the last selection start Cell and the Cell at the 
            /// specified CellPas to the current selection.  Any Cells that are 
            /// between the last start and end Cells that are not in the new area are 
            /// removed from the current selection
            /// </summary>
            /// <param name="cellPos">A CellPos that specifies the shift selected Cell</param>
            public void AddShiftSelectedCell(I3CellPos cellPos)
            {
                this.AddShiftSelectedCell(cellPos.Row, cellPos.Column);
            }


            /// <summary>
            /// 把a、b排序，小的放在前面
            /// Ensures that the first index is smaller than the second index, 
            /// performing a swap if necessary
            /// </summary>
            /// <param name="a">The first index</param>
            /// <param name="b">The second index</param>
            private void Normalise(ref int a, ref int b)
            {
                if (a < 0)
                {
                    a = 0;
                }

                if (b < 0)
                {
                    b = 0;
                }

                if (b < a)
                {
                    int temp = a;
                    a = b;
                    b = temp;
                }
            }

            #endregion

            #region Clear

            /// <summary>
            /// Removes all selected Rows and Cells from the selection
            /// </summary>
            public void Clear()
            {
                this.LastSelectRow = null;

                if (this.rows.Count > 0)
                {
                    int[] oldSelectedIndicies = this.SelectedIndicies;

                    this.InternalClear();

                    this.shiftSelectStart = I3CellPos.Empty;
                    this.shiftSelectEnd = I3CellPos.Empty;

                    this.owner.OnSelectionChanged(new I3SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
                }

            }


            /// <summary>
            /// Removes all selected Rows and Cells from the selection without raising an event
            /// </summary>
            private void InternalClear()
            {
                if (this.rows.Count > 0)
                {
                    for (int i = 0; i < this.rows.Count; i++)
                    {
                        ((I3Row)this.rows[i]).ClearSelection();
                    }

                    this.rows.Clear();
                    this.rows.Capacity = 0;
                }
            }

            #endregion

            #region Remove

            /// <summary>
            /// Removes the Cell at the specified row and column indicies from the current selection
            /// </summary>
            /// <param name="row">The row index of the Cell to remove from the selection</param>
            /// <param name="column">The column index of the Cell to remove from the selection</param>
            public void RemoveCell(int row, int column)
            {
                this.RemoveCells(row, column, row, column);
            }


            /// <summary>
            /// Removes the Cell at the specified row and column indicies from the current selection
            /// </summary>
            /// <param name="cellPos">A CellPos that specifies the Cell to remove from the selection</param>
            public void RemoveCell(I3CellPos cellPos)
            {
                this.RemoveCell(cellPos.Row, cellPos.Column);
            }


            /// <summary>
            /// Removes the Cells located between the specified start and end row/column indicies 
            /// from the current selection
            /// </summary>
            /// <param name="startRow">The row index of the start Cell</param>
            /// <param name="startColumn">The column index of the start Cell</param>
            /// <param name="endRow">The row index of the end Cell</param>
            /// <param name="endColumn">The column index of the end Cell</param>
            public void RemoveCells(int startRow, int startColumn, int endRow, int endColumn)
            {
                if (this.rows.Count > 0)
                {
                    int[] oldSelectedIndicies = this.SelectedIndicies;

                    if (this.InternalRemoveCells(startRow, startColumn, endRow, endColumn))
                    {
                        this.owner.OnSelectionChanged(new I3SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
                    }

                    this.shiftSelectStart = new I3CellPos(startRow, startColumn);
                    this.shiftSelectEnd = new I3CellPos(endRow, endColumn);
                }
            }


            /// <summary>
            /// Removes the Cells located between the specified start and end CellPos from the
            /// current selection
            /// </summary>
            /// <param name="start">A CellPos that specifies the start Cell</param>
            /// <param name="end">A CellPos that specifies the end Cell</param>
            public void RemoveCells(I3CellPos start, I3CellPos end)
            {
                this.RemoveCells(start.Row, start.Column, end.Row, end.Column);
            }


            /// <summary>
            /// Removes the Cells located between the specified start and end CellPos from the
            /// current selection without raising an event
            /// </summary>
            /// <param name="start">A CellPos that specifies the start Cell</param>
            /// <param name="end">A CellPos that specifies the end Cell</param>
            /// <returns>true if any Cells were added, false otherwise</returns>
            private bool InternalRemoveCells(I3CellPos start, I3CellPos end)
            {
                return this.InternalRemoveCells(start.Row, start.Column, end.Row, end.Column);
            }


            /// <summary>
            /// Removes the Cells located between the specified start and end row/column indicies 
            /// from the current selection without raising an event
            /// </summary>
            /// <param name="startRow">The row index of the start Cell</param>
            /// <param name="startColumn">The column index of the start Cell</param>
            /// <param name="endRow">The row index of the end Cell</param>
            /// <param name="endColumn">The column index of the end Cell</param>
            /// <returns>true if any Cells were added, false otherwise</returns>
            private bool InternalRemoveCells(int startRow, int startColumn, int endRow, int endColumn)
            {
                this.Normalise(ref startRow, ref endRow);
                this.Normalise(ref startColumn, ref endColumn);

                bool anyRemoved = false;

                for (int i = startRow; i <= endRow; i++)
                {
                    if (i >= this.owner.Rows.Count)
                    {
                        break;
                    }

                    I3Row r = this.owner.Rows[i];

                    for (int j = startColumn; j <= endColumn; j++)
                    {
                        if (j >= r.Cells.Count)
                        {
                            break;
                        }

                        if (r.Cells[j].Selected)
                        {
                            r.Cells[j].SetSelected(false);
                            r.InternalSelectedCellCount--;

                            anyRemoved = true;
                        }
                    }

                    if (!r.AnyCellsSelected)
                    {
                        if (this.rows.Contains(r))
                        {
                            this.rows.Remove(r);
                        }
                    }
                }

                //datamanager
                if (!this.IsRowSelected(this.owner.Rows.IndexOf(this.lastSelectRow)))
                {
                    int newRow = -1;
                    #region 查找endRow后面的第一个row
                    foreach (int row in this.SelectedIndicies)
                    {
                        if (row >= endRow)
                        {
                            if (newRow == -1)
                            {
                                newRow = row;
                            }
                            else
                            {
                                if (row < newRow)
                                {
                                    newRow = row;
                                }
                            }
                        }
                    }
                    #endregion
                    #region 没有则查找前面一个row
                    if (newRow == -1)
                    {
                        foreach (int row in this.SelectedIndicies)
                        {
                            if (row < endRow)
                            {
                                if (newRow == -1)
                                {
                                    newRow = row;
                                }
                                else
                                {
                                    if (row > newRow)
                                    {
                                        newRow = row;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region 定位新行
                    if (newRow != -1)
                    {
                        this.LastSelectRow = this.owner.Rows[newRow];
                    }
                    #endregion
                }

                return anyRemoved;
            }


            /// <summary>
            /// Removes the specified Row from the selection
            /// </summary>
            /// <param name="row">The Row to be removed from the selection</param>
            internal void RemoveRow(I3Row row)
            {
                if (this.rows.Contains(row))
                {
                    int[] oldSelectedIndicies = this.SelectedIndicies;

                    this.rows.Remove(row);

                    oldSelectedIndicies = this.SelectedIndicies;
                    if (oldSelectedIndicies.Length > 0)
                    {
                        this.LastSelectRow = this.owner.Rows[oldSelectedIndicies[0]];
                    }
                    else
                    {
                        this.LastSelectRow = null;
                    }

                    this.owner.OnSelectionChanged(new I3SelectionEventArgs(this.owner, oldSelectedIndicies, this.SelectedIndicies));
                }
            }

            #endregion

            #region Queries

            /// <summary>
            /// Returns whether the Cell at the specified row and column indicies is 
            /// currently selected
            /// </summary>
            /// <param name="row">The row index of the specified Cell</param>
            /// <param name="column">The column index of the specified Cell</param>
            /// <returns>true if the Cell at the specified row and column indicies is 
            /// selected, false otherwise</returns>
            public bool IsCellSelected(int row, int column)
            {
                if (row < 0 || row >= this.owner.Rows.Count)
                {
                    return false;
                }

                return this.owner.Rows[row].IsCellSelected(column);
            }


            /// <summary>
            /// Returns whether the Cell at the specified CellPos is currently selected
            /// </summary>
            /// <param name="cellPos">A CellPos the represents the row and column indicies 
            /// of the Cell to check</param>
            /// <returns>true if the Cell at the specified CellPos is currently selected, 
            /// false otherwise</returns>
            public bool IsCellSelected(I3CellPos cellPos)
            {
                return this.IsCellSelected(cellPos.Row, cellPos.Column);
            }


            /// <summary>
            /// Returns whether the Row at the specified index in th TableModel is 
            /// currently selected
            /// </summary>
            /// <param name="index">The index of the Row to check</param>
            /// <returns>true if the Row at the specified index is currently selected, 
            /// false otherwise</returns>
            public bool IsRowSelected(int index)
            {
                if (index < 0 || index >= this.owner.Rows.Count)
                {
                    return false;
                }

                return this.owner.Rows[index].AnyCellsSelected;
            }

            #endregion

            #endregion


            #region Properties

            /// <summary>
            /// 获取选择的Row的数组
            /// Gets an array that contains the currently selected Rows
            /// </summary>
            public I3Row[] SelectedItems
            {
                get
                {
                    if (this.rows.Count == 0)
                    {
                        return new I3Row[0];
                    }

                    this.rows.Sort(new I3RowComparer());

                    return (I3Row[])this.rows.ToArray(typeof(I3Row));
                }
            }


            /// <summary>
            /// 获取选择的Row.Index的数组
            /// Gets an array that contains the indexes of the currently selected Rows
            /// </summary>
            public int[] SelectedIndicies
            {
                get
                {
                    if (this.rows.Count == 0)
                    {
                        return new int[0];
                    }

                    this.rows.Sort(new I3RowComparer());

                    int[] indicies = new int[this.rows.Count];

                    for (int i = 0; i < this.rows.Count; i++)
                    {
                        indicies[i] = ((I3Row)this.rows[i]).InternalIndex;
                    }

                    return indicies;
                }
            }


            /// <summary>
            /// 获取选择的坐标
            /// Returns a Rectange that bounds the currently selected Rows
            /// </summary>
            public Rectangle SelectionBounds
            {
                get
                {
                    if (this.rows.Count == 0)
                    {
                        return Rectangle.Empty;
                    }

                    int[] indicies = this.SelectedIndicies;

                    return this.CalcSelectionBounds(indicies[0], indicies[indicies.Length - 1]);
                }
            }


            /// <summary>
            /// 计算选择的区域的坐标
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            /// <returns></returns>
            internal Rectangle CalcSelectionBounds(int start, int end)
            {
                this.Normalise(ref start, ref end);

                Rectangle bounds = new Rectangle();

                if (this.owner.Table != null && this.owner.Table.ColumnModel != null)
                {
                    bounds.Width = this.owner.Table.ColumnModel.VisibleColumnsWidth;
                }

                //bounds.Y = start * this.owner.RowHeight;
                bounds.Y = 0;
                for (int i = 0; i <= start - 1; i++)
                {
                    bounds.Y = bounds.Y + this.owner.Rows[i].Height;
                }

                if (start == end)
                {
                    //bounds.Height = this.owner.RowHeight;
                    bounds.Height = this.owner.Rows[start].Height;
                }
                else
                {
                    //bounds.Height = ((end + 1) * this.owner.RowHeight) - bounds.Y;
                    bounds.Height = 0;
                    for (int i = start; i <= end; i++)
                    {
                        bounds.Height = bounds.Height + this.owner.Rows[i].Height;
                    }
                }

                //expandLastCol
                if (bounds.Right < this.owner.Table.ClientRectWithOutBorder_ScrollBar_Header.Right && this.owner.Table.ExtendLastCol)
                {
                    bounds.Width = bounds.Width + this.owner.Table.ClientRectWithOutBorder_ScrollBar_Header.Right - bounds.Right;
                }

                return bounds;
            }

            private I3Row InnerLastSelectRow
            {
                get
                {
                    return this.lastSelectRow;
                }
                set
                {
                    this.lastSelectRow = value;
                    if (this.lastSelectRow != null)
                    {
                        this.OnDataManagerPositionChanged();
                    }
                }
            }
            private void OnDataManagerPositionChanged()
            {
                //刷新数据绑定信息
                if (this.owner.dataManager != null)
                {
                    int index = this.lastSelectRow == null ? -1 : this.lastSelectRow.Index;
                    if (index < 0)
                    {
                        this.owner.dataManager.Position = -1;
                    }
                    else
                    {
                        this.owner.dataManager.Position = this.owner.dataManager.List.IndexOf(this.lastSelectRow.UserData);
                    }
                    foreach (Binding binding in this.owner.dataManager.Bindings)
                    {
                        binding.ReadValue();
                    }
                }
            }
            public I3Row LastSelectRow
            {
                get
                {
                    return this.lastSelectRow;
                }
                set
                {
                    if (value == null)
                    {
                        this.InnerLastSelectRow = value;
                        return;
                    }
                    if (this.IsRowSelected(value.Index))
                    {
                        this.InnerLastSelectRow = value;
                    }
                    else
                    {
                        //this.InternalAddCells(value.Index, 0, value.Index, this.owner.Table.ColumnModel.Columns.Count - 1);
                        int firstColumn = this.owner.Table.ColumnModel.FirstVisibleColumnIndex;
                        this.InternalAddCells(value.Index, firstColumn, value.Index, firstColumn);
                    }
                    this.owner.Table.InvalidateRow(value);
                }
            }
            #endregion
        }

        #endregion


        #region DataSource

        private object dataSource;
        /// <summary>
        /// 获取或设置数据源
        /// 注意，如果只想显示一次数据，实现IList或IListSource即可 
        /// 但如果数据更改时，表格要动态更新，则必须使用BindingList，且实现INotifyPropertyChanged接口
        /// 注意，不能正确响应Insert方法，会以Add形式加到最后面
        /// </summary>
        [Category("DataSource")]
        [Browsable(false)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (((value != null) && !(value is IList)) && !(value is IListSource))
                {
                    throw new ArgumentException("DataSource必须实现IList或IListSource");
                }
                if (this.dataSource != value)
                {
                    //try
                    //{
                    this.SetDataConnection(value, false);
                    //}
                    //catch
                    //{
                    //this.bindingInfo = new I3TreeViewBindingMemberInfo();
                    //}
                    //if (value == null)
                    //{
                    //    this.bindingInfo = new I3TreeViewBindingMemberInfo();
                    //}
                }

            }
        }

        private CurrencyManager dataManager;
        protected CurrencyManager DataManager
        {
            get
            {
                return this.dataManager;
            }
        }

        private bool isDataSourceInitEventHooked;
        private void UnwireDataSource()
        {
            if (this.dataSource is IComponent)
            {
                ((IComponent)this.dataSource).Disposed -= new EventHandler(this.DataSourceDisposed);
            }
            ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
            if ((dataSource != null) && this.isDataSourceInitEventHooked)
            {
                dataSource.Initialized -= new EventHandler(this.DataSourceInitialized);
                this.isDataSourceInitEventHooked = false;
            }
        }

        private void DataSourceInitialized(object sender, EventArgs e)
        {
            this.SetDataConnection(this.dataSource, true);
        }

        private void DataSourceDisposed(object sender, EventArgs e)
        {
            this.SetDataConnection(null, true);
        }

        private bool isDataSourceInitialized;
        private void WireDataSource()
        {
            if (this.dataSource is IComponent)
            {
                ((IComponent)this.dataSource).Disposed += new EventHandler(this.DataSourceDisposed);
            }
            ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
            if ((dataSource != null) && !dataSource.IsInitialized)
            {
                dataSource.Initialized += new EventHandler(this.DataSourceInitialized);
                this.isDataSourceInitEventHooked = true;
                this.isDataSourceInitialized = false;
            }
            else
            {
                this.isDataSourceInitialized = true;
            }
        }

        private bool inSetDataConnection;
        private Dictionary<object, I3Row> rowList = new Dictionary<object, I3Row>();
        private void SetDataConnection(object newDataSource, bool force)
        {
            bool flag = this.dataSource != newDataSource;
            if (!this.inSetDataConnection)
            {
                try
                {
                    if (force || flag)
                    {
                        this.inSetDataConnection = true;

                        this.Selections.Clear();
                        this.Rows.Clear();
                        this.rowList.Clear();

                        IList list = (this.DataManager != null) ? this.DataManager.List : null;
                        bool flag3 = this.DataManager == null;
                        this.UnwireDataSource();
                        this.dataSource = newDataSource;
                        this.WireDataSource();
                        if (this.isDataSourceInitialized)
                        {
                            CurrencyManager manager = null;
                            if (((newDataSource != null) && this.Table !=null && (this.Table.BindingContext != null)) && (newDataSource != Convert.DBNull))
                            {
                                manager = (CurrencyManager)this.Table.BindingContext[newDataSource];
                            }
                            if (this.dataManager != manager)
                            {
                                if (this.dataManager != null)
                                {
                                    this.dataManager.PositionChanged -= new EventHandler(this.DataManager_PositionChanged);
                                    this.dataManager.ListChanged -= new ListChangedEventHandler(dataManager_ListChanged);
                                }
                                this.dataManager = manager;
                                if (this.dataManager != null)
                                {
                                    this.dataManager.PositionChanged += new EventHandler(this.DataManager_PositionChanged);
                                    this.dataManager.ListChanged += new ListChangedEventHandler(dataManager_ListChanged);
                                }
                            }
                        }
                        this.InitRows();
                        if (this.Rows.Count > 0)
                        {
                            this.Selections.LastSelectRow = this.Rows[0];
                        }
                    }
                }
                finally
                {
                    this.inSetDataConnection = false;
                }
            }
        }

        //代码设置位置时，重新刷新当前选择行， 但由table本身引起的定位呢？
        //导致fullrowselect=false时，点击到其他行也是全选
        //解决方法，不要全部清楚，通过判断，已经在的不要清除
        private void DataManager_PositionChanged(object sender, EventArgs e)
        {
            IList list = this.dataManager.List;
            if (this.dataManager.Position < 0 || this.dataManager.Position > list.Count - 1)
            {
                this.Selections.Clear();
                this.Selections.LastSelectRow = null;
                return;
            }

            object item = list[this.dataManager.Position];
            if (!this.rowList.ContainsKey(item) || this.rowList[item] == null)
            {
                this.Selections.Clear();
                this.Selections.LastSelectRow = null;
                return;
            }

            int rowIndex = this.Rows.IndexOf(this.rowList[item]);
            if (this.Table.MultiSelect)
            {
                if (!this.Selections.IsRowSelected(rowIndex))
                {
                    this.Selections.LastSelectRow = this.rowList[item];
                }
            }
            else
            {
                List<int> removeList = new List<int>();
                foreach (int i in this.Selections.SelectedIndicies)
                {
                    if (i != rowIndex)
                    {
                        removeList.Add(i);
                    }
                }
                foreach (int i in removeList)
                {
                    this.Selections.RemoveRow(this.Rows[i]);
                }
                if (!this.Selections.IsRowSelected(rowIndex))
                {
                    this.Selections.LastSelectRow = this.rowList[item];
                }
            }
        }

        private bool inDataManagerListChanging = false;
        private void dataManager_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (this.inDataManagerListChanging)
            {
                return;
            }

            this.inDataManagerListChanging = true;
            try
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        {
                            #region Reset
                            object oldItem = this.Selections.SelectedIndicies.Length == 0 ? null : this.Rows[this.Selections.SelectedIndicies[0]].UserData;
                            this.InitRows();
                            if (oldItem != null && this.rowList.ContainsKey(oldItem))
                            {
                                this.Selections.LastSelectRow = this.rowList[oldItem];
                            }
                            else
                            {
                                if (this.Rows.Count > 0)
                                {
                                    this.Selections.LastSelectRow = this.Rows[0];
                                }
                            }
                            #endregion
                            break;
                        }
                    case ListChangedType.ItemAdded:
                        {
                            #region ItemAdded
                            I3Row addRow = this.AddRow(this.dataManager.List[e.NewIndex]);
                            this.Selections.Clear();
                            this.Selections.LastSelectRow = addRow;
                            #endregion
                            break;
                        }
                    case ListChangedType.ItemDeleted:
                        {
                            #region ItemDeleted
                            object deleteItem = null;
                            if (this.dataManager.List.GetType().Name == "I3BindingList`1")
                            {
                                deleteItem = this.dataManager.List[e.NewIndex];
                            }
                            else
                            {
                                foreach (object item in this.rowList.Keys)
                                {
                                    if (!this.dataManager.List.Contains(item))
                                    {
                                        deleteItem = item;
                                        break;
                                    }
                                }
                            }
                            if (deleteItem != null)
                            {
                                I3Row deleteRow = this.rowList[deleteItem];
                                I3Row nextRow = this.GetNextRow(deleteRow);
                                this.Selections.RemoveRow(deleteRow);
                                this.DeleteRow(deleteRow.UserData);
                                if (this.Selections.LastSelectRow == null)
                                {
                                    this.Selections.LastSelectRow = nextRow;
                                }
                            }
                            #endregion
                            break;
                        }
                    case ListChangedType.ItemChanged:
                        {
                            if (this.inUpdatingValue)
                            {
                                break;
                            }
                            #region ItemChanged
                            object changedItem = this.dataManager.List[e.NewIndex];
                            this.UpdateRow(changedItem);
                            #endregion
                            break;
                        }
                    default:
                        break;
                }
            }
            finally
            {
                this.inDataManagerListChanging = false;
            }
        }

        /// <summary>
        /// 获取下一个选择行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public I3Row GetNextRow(I3Row row)
        {
            int index = this.Rows.IndexOf(row);
            if (index == this.Rows.Count - 1)
            {
                index--;
            }
            else
            {
                index++;
            }
            if (index < 0)
            {
                return null;
            }
            else
            {
                return this.Rows[index];
            }
        } 

        private Dictionary<string, PropertyDescriptor> descriptors = new Dictionary<string, PropertyDescriptor>();
        //查找属性描述器 
        private void PrepareDescriptor()
        {
            this.descriptors.Clear();
            if (this.dataManager == null)
            {
                return;
            }

            PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
            foreach (I3Column column in this.Table.ColumnModel.Columns)
            {
                if (this.descriptors.ContainsKey(column.DataMember))
                {
                    continue;
                }
                PropertyDescriptor descriptor = itemProperties.Find(column.DataMember, true);
                if (descriptor != null)
                {
                    this.descriptors.Add(column.DataMember, descriptor);
                }
            }
        }

        //初始化行
        private void InitRows()
        {
            this.Selections.Clear();
            this.Rows.Clear();
            this.rowList.Clear();
            this.PrepareDescriptor();

            if(this.dataManager ==null){return;}

            if (this.Table != null)
            {
                this.Table.BeginUpdate();
            }
            foreach (object item in this.dataManager.List)
            {
                this.AddRow(item);
            }
            if (this.Table != null)
            {
                this.Table.EndUpdate();
            }
        }
        
        //增加行
        private I3Row AddRow(object item)
        {
            I3Row row = new I3Row();
            foreach (I3Column column in this.Table.ColumnModel.Columns)
            {
                I3Cell cell = new I3Cell();
                if (this.descriptors.ContainsKey(column.DataMember))
                {
                    if (column is I3CheckBoxColumn)
                    {
                        cell.Checked = (bool)this.descriptors[column.DataMember].GetValue(item);
                    }
                    else
                    {
                        cell.Data = this.descriptors[column.DataMember].GetValue(item);
                    }
                }
                row.Cells.Add(cell);
                cell.UserData = item;
                cell.PropertyChanged += new Events.I3CellEventHandler(cell_PropertyChanged);
            }
            row.UserData = item;
            this.Rows.Add(row);
            this.rowList.Add(item, row);

            return row;
        }

        private bool inUpdatingValue = false;
        private void cell_PropertyChanged(object sender, Events.I3CellEventArgs e)
        {
            if (this.inItemChanging)
            {
                return;
            }

            I3Column column = e.Cell.Row.TableModel.Table.ColumnModel.Columns[e.Column];
            string dataMember = column.DataMember;
            if (!this.descriptors.ContainsKey(dataMember))
            {
                return;
            }

            if (e.EventType == I3CellEventType.CheckStateChanged)
            {
                if (column is I3CheckBoxColumn && e.Cell.UserData != null)
                {
                    object item = e.Cell.UserData;
                    this.inUpdatingValue = true;
                    try
                    {
                        this.descriptors[dataMember].SetValue(item, e.Cell.Checked);
                    }
                    finally
                    {
                        this.inUpdatingValue = false;
                        //有可能引起了其他属性值的改变
                        int index = this.dataManager.List.IndexOf(item);
                        ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.ItemChanged, index);
                        dataManager_ListChanged(null, args);
                    }
                }
                return;
            }

            if (e.EventType != I3CellEventType.ValueChanged)
            {
                return;
            }


            if (e.Cell.UserData != null)
            {
                object item = e.Cell.UserData;
                this.inUpdatingValue = true;
                try
                {
                    this.descriptors[dataMember].SetValue(item, e.Cell.Data);
                }
                finally
                {
                    this.inUpdatingValue = false;
                    //有可能引起了其他属性值的改变
                    int index = this.dataManager.List.IndexOf(item);
                    ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.ItemChanged, index);
                    dataManager_ListChanged(null, args);
                }
            }

        }

        //删除行
        private void DeleteRow(object item)
        {
            I3Row deleteRow = this.rowList[item];
            this.Rows.Remove(deleteRow);
            this.rowList.Remove(item);
        }

        private bool inItemChanging = false;
        private void UpdateRow(object item)
        {
            I3Row row = this.rowList[item];
            if (row == null)
            {
                return;
            }

            this.inItemChanging = true;
            try
            {
                for (int i = 0; i < this.Table.ColumnModel.Columns.Count; i++)
                {
                    I3Column column = this.Table.ColumnModel.Columns[i];
                    if (i < row.Cells.Count && this.descriptors.ContainsKey(column.DataMember))
                    {
                        if (this.Table.ColumnModel.Columns[i].GetType() == typeof(I3CheckBoxColumn))
                        {
                            row.Cells[i].Checked = (bool)this.descriptors[column.DataMember].GetValue(item);
                        }
                        else
                        {
                            row.Cells[i].Data = this.descriptors[column.DataMember].GetValue(item);
                        }
                    }
                }
            }
            finally
            {
                this.inItemChanging = false;
            }
        }

        #endregion
    }
}
