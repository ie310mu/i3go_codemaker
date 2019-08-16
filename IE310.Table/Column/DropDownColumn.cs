

using System;
using System.ComponentModel;
using System.Drawing;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Renderers;


namespace IE310.Table.Column
{
	/// <summary>
    /// 下拉类型的列
	/// Represents a Column whose Cells are displayed with a drop down 
	/// button for editing
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public abstract class I3DropDownColumn : I3Column 
	{
		#region Class Data

		/// <summary>
        /// 是否显示下拉按钮
		/// Specifies whether the Cells should draw a drop down button
		/// </summary>
		private bool showButton;


        private I3DropDownStyle dropDownStyle;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Creates a new DropDownColumn with default values
		/// </summary>
		public I3DropDownColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DropDownColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3DropDownColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DropDownColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3DropDownColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DropDownColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3DropDownColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DropDownColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3DropDownColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DropDownColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3DropDownColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DropDownColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3DropDownColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the DropDownColumn with default values
		/// </summary>
		protected virtual void Init()
		{
			this.showButton = true;
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取或设置是否显示下拉按钮，并引发Renderer改变事件
		/// Gets or sets whether the Column's Cells should draw a drop down button
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Determines whether the Column's Cells should draw a drop down button")]
		public bool ShowDropDownButton
		{
			get
			{
				return this.showButton;
			}

			set
			{
				if(this.showButton != value)
				{
					this.showButton = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}

        [DefaultValue(I3DropDownStyle.DropDownList)]
        public I3DropDownStyle DropDownStyle
        {
            get
            {
                return this.dropDownStyle;
            }
            set
            {
                if (!Enum.IsDefined(typeof(I3DropDownStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3DropDownStyle));
                }

                if (this.dropDownStyle != value)
                {
                    this.dropDownStyle = value;
                }
            }
        }

		#endregion
	}
}
