


using System;
using System.Collections;

using IE310.Table.Events;
using IE310.Table.Row;


namespace IE310.Table.Cell
{
	/// <summary>
	/// Represents a collection of Cell objects
	/// </summary>
	public class I3CellCollection : CollectionBase
	{
		#region Class Data

		/// <summary>
		/// The Row that owns the CellCollection
		/// </summary>
		private I3Row owner;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellCollection class 
		/// that belongs to the specified Row
		/// </summary>
		/// <param name="owner">A Row representing the row that owns 
		/// the Cell collection</param>
		public I3CellCollection(I3Row owner) : base()
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
				
			this.owner = owner;
		}

		#endregion
		

		#region Methods

        public I3Cell Add()
        {
            I3Cell cell = new I3Cell();
            Add(cell);
            return cell;
        }

		/// <summary>
		/// Adds the specified Cell to the end of the collection
		/// </summary>
		/// <param name="cell">The Cell to add</param>
		public int Add(I3Cell cell)
		{
			if (cell == null) 
			{
				throw new System.ArgumentNullException("Cell is null");
			}

			int index = this.List.Add(cell);

			this.OnCellAdded(new I3RowEventArgs(this.owner, cell, index, index));

			return index;
		}

		/// <summary>
		/// Adds an array of Cell objects to the collection
		/// </summary>
		/// <param name="cells">An array of Cell objects to add 
		/// to the collection</param>
		public void AddRange(I3Cell[] cells)
		{
			if (cells == null) 
			{
				throw new System.ArgumentNullException("Cell[] is null");
			}

			for (int i=0; i<cells.Length; i++)
			{
				this.Add(cells[i]);
			}
		}


		/// <summary>
		/// Removes the specified Cell from the model
		/// </summary>
		/// <param name="cell">The Cell to remove</param>
		public void Remove(I3Cell cell)
		{
			int cellIndex = this.IndexOf(cell);

			if (cellIndex != -1) 
			{
				this.RemoveAt(cellIndex);
			}
		}


		/// <summary>
		/// Removes an array of Cell objects from the collection
		/// </summary>
		/// <param name="cells">An array of Cell objects to remove 
		/// from the collection</param>
		public void RemoveRange(I3Cell[] cells)
		{
			if (cells == null) 
			{
				throw new System.ArgumentNullException("Cell[] is null");
			}

			for (int i=0; i<cells.Length; i++)
			{
				this.Remove(cells[i]);
			}
		}


		/// <summary>
		/// Removes the Cell at the specified index from the collection
		/// </summary>
		/// <param name="index">The index of the Cell to remove</param>
		public new void RemoveAt(int index)
		{
			if (index >= 0 && index < this.Count) 
			{
				I3Cell cell = this[index];
			
				this.List.RemoveAt(index);

				this.OnCellRemoved(new I3RowEventArgs(this.owner, cell, index, index));
			}
		}


		/// <summary>
		/// Removes all Cells from the collection
		/// </summary>
		public new void Clear()
		{
			if (this.Count == 0)
			{
				return;
			}

			for (int i=0; i<this.Count; i++)
			{
				this[i].InternalRow = null;
			}

			base.Clear();
			this.InnerList.Capacity = 0;

			this.OnCellRemoved(new I3RowEventArgs(this.owner, null, -1, -1));
		}


		/// <summary>
		/// Inserts a Cell into the collection at the specified index
		/// </summary>
		/// <param name="index">The zero-based index at which the Cell 
		/// should be inserted</param>
		/// <param name="cell">The Cell to insert</param>
		public void Insert(int index, I3Cell cell)
		{
			if (cell == null)
			{
				return;
			}

			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}
			
			if (index >= this.Count)
			{
				this.Add(cell);
			}
			else
			{
				base.List.Insert(index, cell);

				this.OnCellAdded(new I3RowEventArgs(this.owner, cell, index, index));
			}
		}


		/// <summary>
		/// Inserts an array of Cells into the collection at the specified index
		/// </summary>
		/// <param name="index">The zero-based index at which the cells should be inserted</param>
		/// <param name="cells">An array of Cells to be inserted into the collection</param>
		public void InsertRange(int index, I3Cell[] cells)
		{
			if (cells == null) 
			{
				throw new System.ArgumentNullException("Cell[] is null");
			}

			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}

			if (index >= this.Count)
			{
				this.AddRange(cells);
			}
			else
			{
				for (int i=cells.Length-1; i>=0; i--)
				{
					this.Insert(index, cells[i]);
				}
			}
		}


		/// <summary>
		///	Returns the index of the specified Cell in the model
		/// </summary>
		/// <param name="cell">The Cell to look for</param>
		/// <returns>The index of the specified Cell in the model</returns>
		public int IndexOf(I3Cell cell)
		{
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == cell)
				{
					return i;
				}
			}

			return -1;
		}

        /// <summary>
        /// 检查cell的数量，少了则添加，多了则删除
        /// </summary>
        /// <param name="count"></param>
        public void CheckCellsCount(int count)
        {
            while (this.List.Count > count)
            {
                this.List.RemoveAt(this.List.Count - 1);
            }
            while (this.List.Count < count)
            {
                I3Cell cell = new I3Cell();
                cell.Enabled = false;
                this.Add(cell);
            }
        }

        /// <summary>
        /// 替换两个单元格的位置
        /// </summary>
        /// <param name="oldColumn"></param>
        /// <param name="newColumn"></param>
        public void MoveCell(I3Cell moveCell, I3Cell cellAfterAfterNewIndex)
        {
            MoveCell(this.IndexOf(moveCell), this.IndexOf(cellAfterAfterNewIndex));
        }

        /// <summary>
        /// 替换两个单元格的位置
        /// </summary>
        /// <param name="moveCellIndex"></param>
        /// <param name="newIndex"></param>
        public void MoveCell(int moveCellIndex, int newIndex)
        {
            if (moveCellIndex < 0 || moveCellIndex > this.Count - 1)
            {
                throw new Exception("无法移动单元格，moveCellIndex:" + moveCellIndex.ToString() + "错误");
            }
            if (newIndex < 0 /*|| newIndex > this.Count - 1*/)
            {
                throw new Exception("无法移动单元格，newIndex:" + newIndex.ToString() + "错误");
            }
            if (moveCellIndex == newIndex)
            {
                throw new Exception("无法移动单元格，newIndex:" + newIndex.ToString() + "与moveCellIndex相等");
            }

            I3Cell moveCell = this[moveCellIndex];
            this.Remove(moveCell);
            this.Insert(newIndex, moveCell);
        }

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Cell at the specified index
		/// </summary>
		public I3Cell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					return null;
				}
					
				return this.List[index] as I3Cell;
            }
            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new Exception("序号错误");
                }

                this.List[index] = value;
            }
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the CellAdded event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected virtual void OnCellAdded(I3RowEventArgs e)
		{
			this.owner.OnCellAdded(e);
		}


		/// <summary>
		/// Raises the CellRemoved event
		/// </summary>
		/// <param name="e">A RowEventArgs that contains the event data</param>
		protected virtual void OnCellRemoved(I3RowEventArgs e)
		{
			this.owner.OnCellRemoved(e);
		}

		#endregion
	}
}
