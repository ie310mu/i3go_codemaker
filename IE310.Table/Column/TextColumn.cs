

using System;
using System.ComponentModel;
using System.Drawing;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Renderers;
using IE310.Table.Sorting;


namespace IE310.Table.Column
{
	/// <summary>
	/// Summary description for TextColumn.
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3TextColumn : I3Column 
	{
		#region Constructor
		
		/// <summary>
		/// Creates a new TextColumn with default values
		/// </summary>
		public I3TextColumn() : base()
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3TextColumn(string text) : base(text)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3TextColumn(string text, int width) : base(text, width)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3TextColumn(string text, int width, bool visible) : base(text, width, visible)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3TextColumn(string text, Image image) : base(text, image)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3TextColumn(string text, Image image, int width) : base(text, image, width)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3TextColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{

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
			return "TEXT";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3TextCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return "TEXT";
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return new I3TextCellEditor();
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

		#endregion
	}
}
