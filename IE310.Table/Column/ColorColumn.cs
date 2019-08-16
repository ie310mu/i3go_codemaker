


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
	/// Represents a Column whose Cells are displayed as a Color
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3ColorColumn : I3DropDownColumn
	{
		#region Class Data
		
		/// <summary>
		/// Specifies whether the Cells should draw their Color value
		/// </summary>
		private bool showColor;

		/// <summary>
		/// Specifies whether the Cells should draw their Color name
		/// </summary>
		private bool showColorName;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Creates a new ColorColumn with default values
		/// </summary>
		public I3ColorColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ColorColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3ColorColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ColorColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ColorColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ColorColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ColorColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ColorColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3ColorColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ColorColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ColorColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ColorColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ColorColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the ColorColumn with default values
		/// </summary>
		private void Init()
		{
			this.showColor = true;
			this.showColorName = true;
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
			return "COLOR";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3ColorCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return "COLOR";
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return new I3ColorCellEditor();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether the Column's Cells should draw their Color value
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Specifies whether the Column's Cells should draw their Color value")]
		public bool ShowColor
		{
			get
			{
				return this.showColor;
			}

			set
			{
				if (this.showColor != value)
				{
					this.showColor = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Gets or sets whether the Column's Cells should draw their Color name
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Specifies whether the Column's Cells should draw their Color name")]
		public bool ShowColorName
		{
			get
			{
				return this.showColorName;
			}

			set
			{
				if (this.showColorName != value)
				{
					this.showColorName = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return typeof(I3ColorComparer);
			}
		}

		#endregion
	}
}
