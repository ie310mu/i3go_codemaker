
using System;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace IE310.Table.Design
{
	/// <summary> 
	/// A CollectionEditor that displays the help and command areas of its PropertyGrid
	/// </summary>
	public class I3HelpfulCollectionEditor : CollectionEditor
	{
		/// <summary>
		/// Initializes a new instance of the HelpfulCollectionEditor class using 
		/// the specified collection type
		/// </summary>
		/// <param name="type">The type of the collection for this editor to edit</param>
		public I3HelpfulCollectionEditor(Type type) : base(type)
		{

		}


		/// <summary>
		/// Creates a new form to display and edit the current collection
		/// </summary>
		/// <returns>An instance of CollectionEditor.CollectionForm to provide as the 
		/// user interface for editing the collection</returns>
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm editor = base.CreateCollectionForm();

			foreach (Control control in editor.Controls)
			{
                if (control.Name.Equals("overArchingTableLayoutPanel"))
                {
                    foreach (Control con in control.Controls)
                    {
                        if (con is PropertyGrid)
                        {
                            PropertyGrid grid = (PropertyGrid)con;

                            grid.HelpVisible = true;
                            grid.CommandsVisibleIfAvailable = true;
                        }
                    }
                }
			}

			return editor;
		}

	}
}
