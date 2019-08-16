


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
	/// Represents a Column whose Cells are displayed as a Button
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3ButtonColumn : I3Column
	{

        /// <summary>
        /// Cell附带的按钮点击事件
        /// Occurs when a Cell's button is clicked
        /// </summary>
        public event I3CellButtonEventHandler ButtonClicked;


		#region Class Data

		/// <summary>
		/// Specifies the alignment of the Image displayed on the button
		/// </summary>
		private ContentAlignment imageAlignment;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Creates a new ButtonColumn with default values
		/// </summary>
		public I3ButtonColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ButtonColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3ButtonColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ButtonColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ButtonColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ButtonColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ButtonColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ButtonColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3ButtonColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ButtonColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ButtonColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new ButtonColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ButtonColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the ButtonColumn with default values
		/// </summary>
		private void Init()
		{
			this.HeaderAlignment = I3ColumnAlignment.Center;
            this.CellAlignment = I3ColumnAlignment.Center;
			this.imageAlignment = ContentAlignment.MiddleCenter;
			this.Editable = false;
			this.Selectable = false;
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
			return "BUTTON";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3ButtonCellRenderer();
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


        /// <summary>
        /// 调用此函数，抛出CellButtonClicked事件
        /// Raises the CellButtonClicked event
        /// </summary>
        /// <param name="e">A CellButtonEventArgs that contains the event data</param>
        protected internal virtual void OnButtonClicked(I3CellButtonEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (ButtonClicked != null)
                {
                    ButtonClicked(this, e);
                }
            }
        }

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the horizontal alignment of the Column's Cell contents
		/// </summary>
		[Category("Appearance"),
		DefaultValue(I3ColumnAlignment.Center),
		Description("The horizontal alignment of the column's cell contents.")]
		public override I3ColumnAlignment HeaderAlignment
		{
			get
			{
				return base.HeaderAlignment;
			}

			set
			{
				base.HeaderAlignment = value;
			}
		}


		/// <summary>
		/// Gets or sets the alignment of the Image displayed on the buttons
		/// </summary>
		[Category("Appearance"),
        DefaultValue(ContentAlignment.MiddleCenter),
		Description("The alignment of the Image displayed on the buttons")]
		public ContentAlignment ImageAlignment
		{
			get
			{
				return this.imageAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}
					
				if (this.imageAlignment != value)
				{
					this.imageAlignment = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells contents 
		/// are able to be edited
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Controls whether the column's cell contents are able to be changed by the user")]
		public override bool Editable
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


		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells can be selected
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Indicates whether the column's cells can be selected")]
		public override bool Selectable
		{
			get
			{
				return base.Selectable;
			}

			set
			{
				base.Selectable = value;
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
				return typeof(I3TextComparer);
			}
		}

		#endregion
	}
}
