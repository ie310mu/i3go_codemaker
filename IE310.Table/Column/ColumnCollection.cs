


using System;
using System.Collections;

using IE310.Table.Events;
using System.ComponentModel;


namespace IE310.Table.Column
{
	/// <summary>
	/// Represents a collection of Column objects
	/// </summary>
	public class I3ColumnCollection : CollectionBase
	{
		#region Class Data

		/// <summary>
		/// The ColumnModel that owns the CollumnCollection
		/// </summary>
		private I3ColumnModel owner;

		/// <summary>
		/// A local cache of the combined width of all columns
		/// </summary>
		private int totalColumnWidth;

		/// <summary>
		/// A local cache of the combined width of all visible columns
		/// </summary>
		private int visibleColumnsWidth;

		/// <summary>
		/// A local cache of the number of visible columns
		/// </summary>
		private int visibleColumnCount;

		/// <summary>
		/// A local cache of the last visible column in the collection
		/// </summary>
        //private int lastVisibleColumn;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the ColumnModel.ColumnCollection class 
		/// that belongs to the specified ColumnModel
		/// </summary>
		/// <param name="owner">A ColumnModel representing the columnModel that owns 
		/// the Column collection</param>
		public I3ColumnCollection(I3ColumnModel owner) : base()
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
				
			this.owner = owner;
			this.totalColumnWidth = 0;
			this.visibleColumnsWidth = 0;
			this.visibleColumnCount = 0;
            //this.lastVisibleColumn = -1;
		}

		#endregion
		

		#region Methods
		
		/// <summary>
		/// Adds the specified Column to the end of the collection
		/// </summary>
		/// <param name="column">The Column to add</param>
		public int Add(I3Column column)
		{
			if (column == null) 
			{
				throw new System.ArgumentNullException("Column is null");
			}

			int index = this.List.Add(column);
			
			this.RecalcWidthCache();

			this.OnColumnAdded(new I3ColumnModelEventArgs(this.owner, column, index, index));

			return index;
		}


		/// <summary>
		/// Adds an array of Column objects to the collection
		/// </summary>
		/// <param name="columns">An array of Column objects to add 
		/// to the collection</param>
		public void AddRange(I3Column[] columns)
		{
			if (columns == null) 
			{
				throw new System.ArgumentNullException("Column[] is null");
			}

			for (int i=0; i<columns.Length; i++)
			{
				this.Add(columns[i]);
			}
		}


		/// <summary>
		/// Removes the specified Column from the model
		/// </summary>
		/// <param name="column">The Column to remove</param>
		public void Remove(I3Column column)
		{
			int columnIndex = this.IndexOf(column);

			if (columnIndex != -1) 
			{
				this.RemoveAt(columnIndex);
			}
		}


		/// <summary>
		/// Removes an array of Column objects from the collection
		/// </summary>
		/// <param name="columns">An array of Column objects to remove 
		/// from the collection</param>
		public void RemoveRange(I3Column[] columns)
		{
			if (columns == null) 
			{
				throw new System.ArgumentNullException("Column[] is null");
			}

			for (int i=0; i<columns.Length; i++)
			{
				this.Remove(columns[i]);
			}
		}


		/// <summary>
		/// Removes the Column at the specified index from the collection
		/// </summary>
		/// <param name="index">The index of the Column to remove</param>
		public new void RemoveAt(int index)
		{
			if (index >= 0 && index < this.Count) 
			{
				I3Column column = this[index];
				
				this.List.RemoveAt(index);

				this.RecalcWidthCache();

				this.OnColumnRemoved(new I3ColumnModelEventArgs(this.owner, column, index, index));
			}
		}


		/// <summary>
		/// Removes all Columns from the collection
		/// </summary>
		public new void Clear()
		{
			if (this.Count == 0)
			{
				return;
			}

			for (int i=0; i<this.Count; i++)
			{
				this[i].ColumnModel = null;
			}

			base.Clear();
			this.InnerList.Capacity = 0;

			this.RecalcWidthCache();

			this.OnColumnRemoved(new I3ColumnModelEventArgs(this.owner, null, -1, -1));
		}


        /// <summary>
        /// Inserts a Cell into the collection at the specified index
        /// </summary>
        /// <param name="index">The zero-based index at which the Cell 
        /// should be inserted</param>
        /// <param name="cell">The Cell to insert</param>
        public void Insert(int index, I3Column column)
        {
            if (column == null)
            {
                return;
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index >= this.Count)
            {
                this.Add(column);
            }
            else
            {
                base.List.Insert(index, column);
                this.RecalcWidthCache();

                this.OnColumnAdded(new I3ColumnModelEventArgs(this.owner, column, index, index));
            }
        }


        /// <summary>
        /// Inserts an array of Cells into the collection at the specified index
        /// </summary>
        /// <param name="index">The zero-based index at which the cells should be inserted</param>
        /// <param name="columns">An array of Cells to be inserted into the collection</param>
        public void InsertRange(int index, I3Column[] columns)
        {
            if (columns == null)
            {
                throw new System.ArgumentNullException("Column[] is null");
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index >= this.Count)
            {
                this.AddRange(columns);
            }
            else
            {
                for (int i = columns.Length - 1; i >= 0; i--)
                {
                    this.Insert(index, columns[i]);
                }
            }
        }


		/// <summary>
		///	Returns the index of the specified Column in the model
		/// </summary>
		/// <param name="column">The Column to look for</param>
		/// <returns>The index of the specified Column in the model</returns>
		public int IndexOf(I3Column column)
		{
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == column)
				{
					return i;
				}
			}

			return -1;
		}


		/// <summary>
		/// Recalculates the total combined width of all columns
		/// </summary>
		protected internal void RecalcWidthCache() 
		{
			int total = 0;
			int visibleWidth = 0;
			int visibleCount = 0;
			int lastVisible = -1;

			for (int i=0; i<this.Count; i++)
			{
				total += this[i].Width;
					
				if (this[i].Visible)
				{
					this[i].X = visibleWidth;
					visibleWidth += this[i].Width;
					visibleCount++;
					lastVisible = i;
				}
			}

			this.totalColumnWidth = total;
			this.visibleColumnsWidth = visibleWidth;
			this.visibleColumnCount = visibleCount;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Column at the specified index
		/// </summary>
		public I3Column this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					return null;
				}
					
				return this.List[index] as I3Column;
            }
            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new Exception("ÐòºÅ³¬³ö½çÏÞ");
                }
                this.List[index] = value;
            }
		}


		/// <summary>
		/// Gets the ColumnModel that owns this ColumnCollection
		/// </summary>
		public I3ColumnModel ColumnModel
		{
			get
			{
				return this.owner;
			}
		}


		/// <summary>
		/// Returns the total width of all the Columns in the model
		/// </summary>
		internal int TotalColumnWidth
		{
			get
			{
				return this.totalColumnWidth;
			}
		}


		/// <summary>
		/// Returns the total width of all the visible Columns in the model
		/// </summary>
		internal int VisibleColumnsWidth
		{
			get
			{
				return this.visibleColumnsWidth;
			}
		}


		/// <summary>
		/// Returns the number of visible Columns in the model
		/// </summary>
		internal int VisibleColumnCount
		{
			get
			{
				return this.visibleColumnCount;
			}
		}


		/// <summary>
		/// Returns the index of the last visible Column in the model
		/// </summary>
		internal int LastVisibleColumn
		{
			get
			{
                //return this.lastVisibleColumn;
                for (int i = this.Count - 1; i >= 0; i--)
                {
                    if (this[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
			}
		}



        /// <summary>
        /// Returns the index of the first visible Column in the model
        /// </summary>
        internal int FirstVisibleColumn
        {
            get
            {
                //return this.lastVisibleColumn;
                for (int i = 0; i <= this.Count - 1; i++)
                {
                    if (this[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

		#endregion


		#region Events

		/// <summary>
		/// Raises the ColumnAdded event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected virtual void OnColumnAdded(I3ColumnModelEventArgs e)
		{
			this.owner.OnColumnAdded(e);
		}


		/// <summary>
		/// Raises the ColumnRemoved event
		/// </summary>
		/// <param name="e">A ColumnModelEventArgs that contains the event data</param>
		protected virtual void OnColumnRemoved(I3ColumnModelEventArgs e)
		{
			this.owner.OnColumnRemoved(e);
		}

		#endregion
	}
}
