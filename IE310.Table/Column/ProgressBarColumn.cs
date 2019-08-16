


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
	/// Represents a Column whose Cells are displayed as a ProgressBar
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3ProgressBarColumn : I3Column
	{
		#region Class Data

		/// <summary>
		/// Specifies whether the ProgressBar's value as a string 
		/// should be displayed
		/// </summary>
		private bool drawPercentageText;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Creates a new ProgressBarColumn with default values
		/// </summary>
		public I3ProgressBarColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ProgressBarColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3ProgressBarColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ProgressBarColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ProgressBarColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ProgressBarColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ProgressBarColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ProgressBarColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3ProgressBarColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ProgressBarColumn with the specified header text, image 
		/// and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ProgressBarColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ProgressBarColumn with the specified header text, image, 
		/// width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ProgressBarColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the ProgressBarColumn with default values
		/// </summary>
		private void Init()
		{
			this.drawPercentageText = true;
			this.Editable = false;
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
			return "PROGRESSBAR";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3ProgressBarCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return null;
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return null;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether a Cell's percantage value should be drawn as a string
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Indicates whether a Cell's percantage value is drawn as a string")]
		public bool DrawPercentageText
		{
			get
			{
				return this.drawPercentageText;
			}

			set
			{
				if(this.drawPercentageText != value)
				{
					this.drawPercentageText = value;

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
				return typeof(I3NumberComparer);
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells contents 
		/// are able to be edited
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Controls whether the column's cell contents are able to be changed by the user")]
		public new bool Editable
		{
			get
			{
				return base.Editable;
			}

			set
			{
				base.Editable = value;
			}
		}

		#endregion
	}
}
