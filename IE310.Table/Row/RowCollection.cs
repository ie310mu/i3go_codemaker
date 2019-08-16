using System;
using System.Collections;

using IE310.Table.Events;

namespace IE310.Table.Row
{
	/// <summary>
    /// ÐÐ¼¯ºÏ
	/// Represents a collection of Row objects
	/// </summary>
	public class I3RowCollection : CollectionBase 
	{
		#region Class Data

		/// <summary>
		/// The TableModel that owns the RowCollection
		/// </summary>
		private I3TableModel owner;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the RowCollection class 
		/// that belongs to the specified TableModel
		/// </summary>
		/// <param name="owner">A TableModel representing the tableModel that owns 
		/// the RowCollection</param>
		public I3RowCollection(I3TableModel owner) : base()
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
				
			this.owner = owner;
		}

		#endregion
		

		#region Methods

        public I3Row Add()
        {
            I3Row row = new I3Row();
            Add(row);
            return row;
        }

		/// <summary>
		/// Adds the specified Row to the end of the collection
		/// </summary>
		/// <param name="row">The Row to add</param>
		public int Add(I3Row row)
		{
			if (row == null) 
			{
				throw new System.ArgumentNullException("Row is null");
			}

			int index = this.List.Add(row);

			this.OnRowAdded(new I3TableModelEventArgs(this.owner, row, index, index));

			return index;
		}


		/// <summary>
		/// Adds an array of Row objects to the collection
		/// </summary>
		/// <param name="rows">An array of Row objects to add 
		/// to the collection</param>
		public void AddRange(I3Row[] rows)
		{
			if (rows == null) 
			{
				throw new System.ArgumentNullException("Row[] is null");
			}

			for (int i=0; i<rows.Length; i++)
			{
				this.Add(rows[i]);
			}
		}


		/// <summary>
		/// Removes the specified Row from the model
		/// </summary>
		/// <param name="row">The Row to remove</param>
		public void Remove(I3Row row)
		{
			int rowIndex = this.IndexOf(row);

			if (rowIndex != -1) 
			{
				this.RemoveAt(rowIndex);
			}
		}


		/// <summary>
		/// Removes an array of Row objects from the collection
		/// </summary>
		/// <param name="rows">An array of Row objects to remove 
		/// from the collection</param>
		public void RemoveRange(I3Row[] rows)
		{
			if (rows == null) 
			{
				throw new System.ArgumentNullException("Row[] is null");
			}

			for (int i=0; i<rows.Length; i++)
			{
				this.Remove(rows[i]);
			}
		}


		/// <summary>
		/// Removes the Row at the specified index from the collection
		/// </summary>
		/// <param name="index">The index of the Row to remove</param>
		public new void RemoveAt(int index)
		{
			if (index >= 0 && index < this.Count) 
			{
				I3Row row = this[index];
			
				this.List.RemoveAt(index);

				this.OnRowRemoved(new I3TableModelEventArgs(this.owner, row, index, index));
			}
		}


		/// <summary>
		/// Removes all Rows from the collection
		/// </summary>
		public new void Clear()
		{
			if (this.Count == 0)
			{
				return;
			}

			for (int i=0; i<this.Count; i++)
			{
				this[i].InternalTableModel = null;
			}

			base.Clear();
			this.InnerList.Capacity = 0;

			this.owner.OnRowRemoved(new I3TableModelEventArgs(this.owner, null, -1, -1));
		}


		/// <summary>
		/// Inserts a Row into the collection at the specified index
		/// </summary>
		/// <param name="index">The zero-based index at which the Row 
		/// should be inserted</param>
		/// <param name="row">The Row to insert</param>
		public void Insert(int index, I3Row row)
		{
			if (row == null)
			{
				return;
			}

			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}
			
			if (index >= this.Count)
			{
				this.Add(row);
			}
			else
			{
				base.List.Insert(index, row);

				this.owner.OnRowAdded(new I3TableModelEventArgs(this.owner, row, index, index));
			}
		}


		/// <summary>
		/// Inserts an array of Rows into the collection at the specified 
		/// index
		/// </summary>
		/// <param name="index">The zero-based index at which the rows 
		/// should be inserted</param>
		/// <param name="rows">The array of Rows to be inserted into 
		/// the collection</param>
		public void InsertRange(int index, I3Row[] rows)
		{
			if (rows == null) 
			{
				throw new System.ArgumentNullException("Row[] is null");
			}

			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}

			if (index >= this.Count)
			{
				this.AddRange(rows);
			}
			else
			{
				for (int i=rows.Length-1; i>=0; i--)
				{
					this.Insert(index, rows[i]);
				}
			}
		}


		/// <summary>
		///	Returns the index of the specified Row in the model
		/// </summary>
		/// <param name="row">The Row to look for</param>
		/// <returns>The index of the specified Row in the model</returns>
		public int IndexOf(I3Row row)
		{
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == row)
				{
					return i;
				}
			}

			return -1;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Row at the specified index
		/// </summary>
		public I3Row this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					return null;
				}
					
				return this.List[index] as I3Row;
			}
		}


		/// <summary>
		/// Replaces the Row at the specified index to the specified Row
		/// </summary>
		/// <param name="index">The index of the Row to be replaced</param>
		/// <param name="row">The Row to be placed at the specified index</param>
		internal void SetRow(int index, I3Row row)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("value");
			}

			if (row == null)
			{
				throw new ArgumentNullException("row cannot be null");
			}
					
			this.List[index] = row;

			row.InternalIndex = index;
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the RowAdded event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected virtual void OnRowAdded(I3TableModelEventArgs e)
		{
			this.owner.OnRowAdded(e);
		}


		/// <summary>
		/// Raises the RowRemoved event
		/// </summary>
		/// <param name="e">A TableModelEventArgs that contains the event data</param>
		protected virtual void OnRowRemoved(I3TableModelEventArgs e)
		{
			this.owner.OnRowRemoved(e);
		}

		#endregion
	}
}
