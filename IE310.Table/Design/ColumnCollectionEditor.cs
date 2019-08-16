
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Column;
using IE310.Table.Row;
using IE310.Table.Cell;
using System.Reflection;
using System.Collections.Generic;


namespace IE310.Table.Design
{
	/// <summary>
	/// Provides a user interface that can edit collections of Columns 
	/// at design time
	/// </summary>
	public class I3ColumnCollectionEditor : I3HelpfulCollectionEditor
	{
		#region Class Data
		
		/// <summary>
		/// The ColumnCollection being edited
		/// </summary>
		private I3ColumnCollection columns;

		/// <summary>
		/// Preview table
		/// </summary>
		private I3Table previewTable;

		/// <summary>
		/// ColumnModel for the preview table
		/// </summary>
		private I3ColumnModel previewColumnModel;

		/// <summary>
		/// TableModel for the preview table
		/// </summary>
		private I3TableModel previewTableModel;

		/// <summary>
		/// 
		/// </summary>
		private Label previewLabel;

		#endregion

		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColumnCollectionEditor class 
		/// using the specified collection type
		/// </summary>
		/// <param name="type">The type of the collection for this editor to edit</param>
		public I3ColumnCollectionEditor(Type type) : base(type)
		{
			this.columns = null;

			this.previewColumnModel = new I3ColumnModel();
			this.previewColumnModel.Columns.Add(new I3TextColumn("Column", 116));
			
			this.previewTableModel = new I3TableModel();
			this.previewTableModel.Rows.Add(new I3Row());
			
            //I3Cell cell = new I3Cell();
            //cell.Editable = false;
            //cell.ToolTipText = "This is a Cell ToolTip";
			
            //this.previewTableModel.Rows[0].Cells.Add(cell);
			this.previewTableModel.DefaultRowHeight = 20;
        

			this.previewTable = new I3Table();
            this.previewTable.EnableColumnHeaderContextMenu = false;
			this.previewTable.Preview = true;
			this.previewTable.Size = new Size(120, 274);
			this.previewTable.Location = new Point(0, 24);
			this.previewTable.GridLines = I3GridLines.Both;
			this.previewTable.TabStop = false;
			this.previewTable.EnableToolTips = true;
			this.previewTable.ColumnModel = this.previewColumnModel;
			this.previewTable.TableModel = this.previewTableModel;

			this.previewLabel = new Label();
            this.previewLabel.Name = "previewLabel";
			this.previewLabel.Text = "‘§¿¿:";
			this.previewLabel.Size = new Size(140, 16);
			this.previewLabel.Location = new Point(0, 8);
		}

		#endregion
		
		
		#region Methods

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
			this.columns = (I3ColumnCollection) value;

			// for some reason (might be beacause Column is an 
			// abstract class) the table doesn't get redrawn 
			// when a columns property changes, but we can get 
			// around that by subscribing to the columns 
			// PropertyChange event and passing the message on 
			// to the table ourselves.  we need to do this for 
			// all the existing columns in the collection
			for (int i=0; i<this.columns.Count; i++)
			{
				this.columns[i].PropertyChanged += new I3ColumnEventHandler(column_PropertyChanged);
			}

            I3ColumnModel model = (I3ColumnModel)context.Instance;
            I3Table oldTable = model.Table;
            if (oldTable != null)
            {
                oldTable.ColumnModel = null;
            }
            this.previewTable.ColumnModel = model;
            this.CheckCells();

			object returnObject = base.EditValue(context, isp, value);

            if (oldTable != null)
            {
                oldTable.ColumnModel = model;
            }
            this.previewTable.ColumnModel = null;
			if (model.Table != null)
			{
				model.Table.PerformLayout();
				model.Table.Refresh();
			}
			
			return returnObject;
		}

        private void CheckCells()
        {
            I3Row row = this.previewTableModel.Rows[0];
            row.Cells.Clear();
            for (int i = 0; i <= this.columns.Count - 1; i++)
            {
                    I3Cell cell = new I3Cell();
                    SetCellData(cell, this.columns[i]);
                    row.Cells.Add(cell);
            }
        }



        private List<Type> list;
		/// <summary>
		/// Gets the data types that this collection editor can contain
		/// </summary>
		/// <returns>An array of data types that this collection can contain</returns>
		protected override Type[] CreateNewItemTypes()
		{
            if (list == null)
            {
                list = new List<Type>();
                Type[] types = this.GetType().Assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(I3Column)))
                    {
                        list.Add(type);
                    }
                }
            }

            return list.ToArray();

            //return new Type[] {typeof(I3TextColumn),
            //                      typeof(I3ButtonColumn),
            //                      typeof(I3CheckBoxColumn),
            //                      typeof(I3ColorColumn),
            //                      typeof(I3ComboBoxColumn),
            //                      typeof(I3DateTimeColumn),
            //                      typeof(I3ImageColumn),
            //                      typeof(I3NumberColumn),
            //                      typeof(I3ExtendColumn),
            //                      typeof(I3PopupColumn),
            //                      typeof(I3ProgressBarColumn)};
		}


		/// <summary>
		/// Creates a new instance of the specified collection item type
		/// </summary>
		/// <param name="itemType">The type of item to create</param>
		/// <returns>A new instance of the specified object</returns>
		protected override object CreateInstance(Type itemType)
		{
			I3Column column = (I3Column) base.CreateInstance(itemType);

			// newly created items aren't added to the collection 
			// until editing has finished.  we'd like the newly 
			// created column to show up in the table immediately
			// so we'll add it to the ColumnCollection now
			this.columns.Add(column);

			// for some reason (might be beacause Column is an 
			// abstract class) the table doesn't get redrawn 
			// when a columns property changes, but we can get 
			// around that by subscribing to the columns 
			// PropertyChange event and passing the message on 
			// to the table ourselves
            column.PropertyChanged += new IE310.Table.Events.I3ColumnEventHandler(column_PropertyChanged);

            this.CheckCells();
			return column;
		}


		/// <summary>
		/// Destroys the specified instance of the object
		/// </summary>
		/// <param name="instance">The object to destroy</param>
		protected override void DestroyInstance(object instance)
		{
			if (instance != null && instance is I3Column)
			{
				I3Column column = (I3Column) instance;

				// the specified column is about to be destroyed 
				// so we need to remove it from the ColumnCollection first
				this.columns.Remove(column);
                column.PropertyChanged -= new IE310.Table.Events.I3ColumnEventHandler(column_PropertyChanged);
			}

            base.DestroyInstance(instance);
            this.CheckCells();
		}


		/// <summary>
		/// Creates a new form to display and edit the current collection
		/// </summary>
		/// <returns>An instance of CollectionEditor.CollectionForm to provide 
		/// as the user interface for editing the collection</returns>
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm editor = base.CreateCollectionForm();
            
            editor.Height += 180;
            editor.Width += 200;

            int left = 0;
            int width = 0;
			foreach (Control control in editor.Controls)
            {
                if (control.Name.Equals("overArchingTableLayoutPanel"))
				{
                    left = control.Left;
                    width = control.Width;
                    control.Top += 140;
                    control.Height -= 140;

                    foreach (Control con in control.Controls)
                    {
                        if (con is PropertyGrid)
                        {
                            PropertyGrid grid = (PropertyGrid)con;
                            this.propertyGrid = grid;

                            //grid.SelectedObjectsChanged += new EventHandler(this.PropertyGrid_SelectedObjectsChanged);
                        }
                    }
				}
				
			}


            this.previewTable.Left = left + 2;
            this.previewTable.Top = 30;
            this.previewTable.Height = 100;
            this.previewTable.Width = width - 4;
            this.previewTable.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            editor.Controls.Add(this.previewTable);
            this.previewTable.BringToFront();

            this.previewLabel.Left = left + 2;
            this.previewLabel.Top = this.previewTable.Top - 20;
            this.previewLabel.Height = 14;
            editor.Controls.Add(this.previewLabel);

            editor.MaximizeBox = true;
            editor.FormBorderStyle = FormBorderStyle.Sizable;
            editor.FormClosed += new FormClosedEventHandler(editor_FormClosed);

			return editor;
		}

        void editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form editor = sender as Form;
            editor.Controls.Remove(this.previewLabel);
            editor.Controls.Remove(this.previewTable);
        }
        PropertyGrid propertyGrid;

		#endregion


        private void SetCellData(I3Cell cell, I3Column column)
        {
            if (column is I3ButtonColumn)
            {
                cell.Data = null;
                cell.Data = "Button";
            }
            else if (column is I3CheckBoxColumn)
            {
                cell.Data = null;
                cell.Data = "Checkbox";
                cell.Checked = true;
            }
            else if (column is I3ColorColumn)
            {
                cell.Data = null;
                cell.Data = Color.Red;
            }
            else if (column is I3ComboBoxColumn)
            {
                cell.Data = null;
                cell.Data = "ComboBox";
            }
            else if (column is I3DateTimeColumn)
            {
                cell.Data = null;
                cell.Data = DateTime.Now;
            }
            else if (column is I3ImageColumn)
            {
                cell.Data = null;
                cell.Data = "Image";
            }
            else if (column is I3NumberColumn || column is I3ProgressBarColumn)
            {
                cell.Data = null;
                cell.Data = 50;
            }
            else if (column is I3TextColumn)
            {
                cell.Data = null;
                cell.Data = "Text";
            }
            else
            {
                cell.Data = null;
            }
        }

		#region Events

		/// <summary>
		/// Handler for the PropertyGrid's SelectedObjectsChanged event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected void PropertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
		{
            object[] objects = ((PropertyGrid)sender).SelectedObjects;

            this.previewColumnModel.Columns.Clear();

            if (objects.Length == 1)
            {
                I3Column column = (I3Column)objects[0];
                I3Cell cell = this.previewTableModel[0, 0];

                SetCellData(cell, column);

                I3Column newColumn = (I3Column)Activator.CreateInstance(column.GetType());
                PropertyInfo[] properties = column.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetGetMethod() != null && property.GetSetMethod() != null)
                    {
                        object value = property.GetValue(column, null);
                        property.SetValue(newColumn, value, null);
                    }
                }
                newColumn.Tag = column;
                newColumn.PropertyChanged += new I3ColumnEventHandler(newColumn_PropertyChanged);
                this.previewColumnModel.Columns.Add(newColumn);
            }

            this.previewTable.Invalidate();
		}

        void newColumn_PropertyChanged(object sender, I3ColumnEventArgs e)
        {
            if (e.EventType == I3ColumnEventType.WidthChanged)
            {
                I3Column newColumn = sender as I3Column;
                if (newColumn == null || newColumn.Tag == null)
                {
                    return;
                }
                I3Column column = newColumn.Tag as I3Column;
                if (column == null)
                {
                    return;
                }
                column.Width = newColumn.Width;
            }
        }


		/// <summary>
		/// Handler for a Column's PropertyChanged event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		private void column_PropertyChanged(object sender, I3ColumnEventArgs e)
		{
			this.columns.ColumnModel.OnColumnPropertyChanged(e);
            if (this.propertyGrid != null)
            {
                this.propertyGrid.Refresh();
            }
            
		}

		#endregion
	}
}
