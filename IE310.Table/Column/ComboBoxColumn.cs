

using System;
using System.ComponentModel;
using System.Drawing;

using IE310.Table.Editors;
using IE310.Table.Design;
using IE310.Table.Renderers;
using IE310.Table.Sorting;
using System.Windows.Forms;


namespace IE310.Table.Column
{
	/// <summary>
	/// Represents a Column whose Cells are displayed as a ComboBox
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3ComboBoxColumn : I3DropDownColumn
	{
        private ListBox.ObjectCollection items;

		#region Constructor
		
		/// <summary>
		/// Creates a new ComboBoxColumn with default values
		/// </summary>
		public I3ComboBoxColumn() : base()
		{
		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3ComboBoxColumn(string text) : base(text)
		{
		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ComboBoxColumn(string text, int width) : base(text, width)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ComboBoxColumn(string text, int width, bool visible) : base(text, width, visible)
		{
		
		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3ComboBoxColumn(string text, Image image) : base(text, image)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ComboBoxColumn(string text, Image image, int width) : base(text, image, width)
		{

		}


		/// <summary>
		/// Creates a new ComboBoxColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ComboBoxColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{

		}

        protected override void Init()
        {
            this.items = new ListBox.ObjectCollection(new ListBox());
            base.Init();
        }

		#endregion


		#region Methods

		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public override string GetDefaultRendererName()
		{
			return "COMBOBOX";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3ComboBoxCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return "COMBOBOX";
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return new I3ComboBoxCellEditor();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return typeof(I3TextComparer);
			}
		}

        public ListBox.ObjectCollection Items
        {
            get
            {
                return this.items;
            }
        }

		#endregion
	}
}
