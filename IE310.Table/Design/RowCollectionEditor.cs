using System;
using System.ComponentModel;
using System.ComponentModel.Design;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Row;


namespace IE310.Table.Design
{
	/// <summary>
	/// Provides a user interface that can edit collections of Rows 
	/// at design time
	/// </summary>
	public class I3RowCollectionEditor : I3HelpfulCollectionEditor
	{
		/// <summary>
		///	The RowCollection being edited
		/// </summary>
		private I3RowCollection rows;

		
		/// <summary>
		/// Initializes a new instance of the RowCollectionEditor class 
		/// using the specified collection type
		/// </summary>
		/// <param name="type">The type of the collection for this editor to edit</param>
		public I3RowCollectionEditor(Type type) : base(type)
		{
			this.rows = null;
		}


		/// <summary>
		/// Edits the value of the specified object using the specified 
		/// service provider and context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that can be 
		/// used to gain additional context information</param>
		/// <param name="isp">A service provider object through which 
		/// editing services can be obtained</param>
		/// <param name="value">The object to edit the value of</param>
		/// <returns>The new value of the object. If the value of the 
		/// object has not changed, this should return the same object 
		/// it was passed</returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.rows = (I3RowCollection) value;

			object returnObject = base.EditValue(context, isp, value);

			I3TableModel model = (I3TableModel) context.Instance;

			// make sure the TableModel's Table redraws any additions/deletions
			if (model.Table != null)
			{
				model.Table.PerformLayout();
				model.Table.Refresh();
			}

			return returnObject;
		}


		/// <summary>
		/// Creates a new instance of the specified collection item type
		/// </summary>
		/// <param name="itemType">The type of item to create</param>
		/// <returns>A new instance of the specified object</returns>
		protected override object CreateInstance(Type itemType)
		{
			I3Row row = (I3Row) base.CreateInstance(itemType);

			// newly created items aren't added to the collection 
			// until editing has finished.  we'd like the newly 
			// created row to show up in the table immediately
			// so we'll add it to the RowCollection now
			this.rows.Add(row);
			
			return row;
		}


		/// <summary>
		/// Destroys the specified instance of the object
		/// </summary>
		/// <param name="instance">The object to destroy</param>
		protected override void DestroyInstance(object instance)
		{
			if (instance != null && instance is I3Row)
			{
				I3Row row = (I3Row) instance;

				// the specified row is about to be destroyed so 
				// we need to remove it from the RowCollection first
				this.rows.Remove(row);
			}
			
			base.DestroyInstance(instance);
		}
	}
}
