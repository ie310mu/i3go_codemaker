

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
	/// Represents a Column whose Cells are displayed as a CheckBox
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3CheckBoxColumn : I3Column
	{ 
		#region Class Data

		/// <summary>
		/// The size of the checkbox
		/// </summary>
		private Size checkSize;

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		/// <summary>
		/// The style of the checkboxes
		/// </summary>
		private I3CheckBoxColumnStyle checkBoxColumnStyle;

        /// <summary>
        /// 编辑方式
        /// </summary>
        private I3CheckBoxCheckStyle checkBoxCheckStyle;

        /// <summary>
        /// 选择模式为图片时，自定义的选择图片
        /// </summary>
        private Image customCheckImage;
        /// <summary>
        /// 选择模式为图片时，自定义的选择图片的强制大小
        /// </summary>
        private Size customCheckImageSize;

        /// <summary>
        /// 自定义图像时，是否填充整个区域
        /// </summary>
        private bool customCheckImageFillClient;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Creates a new CheckBoxColumn with default values
		/// </summary>
		public I3CheckBoxColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new CheckBoxColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3CheckBoxColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new CheckBoxColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3CheckBoxColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new CheckBoxColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3CheckBoxColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new CheckBoxColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3CheckBoxColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new CheckBoxColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3CheckBoxColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new CheckBoxColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3CheckBoxColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the CheckBoxColumn with default values
		/// </summary>
		private void Init()
		{
			this.checkSize = new Size(13, 13);
			this.drawText = true;
			this.checkBoxColumnStyle = I3CheckBoxColumnStyle.CheckBox;
            this.customCheckImage = null;
            this.customCheckImageSize = new Size(0, 0);
            this.checkBoxCheckStyle = I3CheckBoxCheckStyle.CheckInClientArea;
            this.customCheckImageFillClient = false;
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
			return "CHECKBOX";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3CheckBoxCellRenderer();
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
		/// Gets or sets the size of the checkboxes
		/// </summary>
		[Category("Appearance"),
		Description("Specifies the size of the checkboxes")]
		public Size CheckSize
		{
			get
			{
				return this.checkSize;
			}

			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					value = new Size(13, 13);
				}
				
				if (this.checkSize != value)
				{
					this.checkSize = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Specifies whether the CheckSize property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the CheckSize property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeCheckSize()
		{
			return (this.checkSize.Width != 13 || this.checkSize.Height != 13);
		}

		
		/// <summary>
		/// Gets or sets whether any text contained in the Cell should be drawn
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Specifies whether any text contained in the Cell should be drawn")]
		public bool DrawText
		{
			get
			{
				return this.drawText;
			}

			set
			{
				if (this.drawText != value)
				{
					this.drawText = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}

		
		/// <summary>
		/// Gets or sets whether any text contained in the Cell should be drawn
		/// </summary>
		[Category("Appearance"),
		DefaultValue(I3CheckBoxColumnStyle.CheckBox),
		Description("Specifies the style of the checkboxes")]
		public I3CheckBoxColumnStyle CheckBoxColumnStyle
		{
			get
			{
				return this.checkBoxColumnStyle;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3CheckBoxColumnStyle), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3CheckBoxColumnStyle));
				}
					
				if (this.checkBoxColumnStyle != value)
				{
					this.checkBoxColumnStyle = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


        [Category("Appearance"),
        DefaultValue(I3CheckBoxCheckStyle.CheckInClientArea)]
        public I3CheckBoxCheckStyle CheckBoxCheckStyle
        {
            get
            {
                return this.checkBoxCheckStyle;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3CheckBoxCheckStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3CheckBoxCheckStyle));
                }

                if (this.checkBoxCheckStyle != value)
                {
                    this.checkBoxCheckStyle = value;

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
				return typeof(I3CheckBoxComparer);
			}
		}

        /// <summary>
        /// 选择模式为图片时，自定义的选择图片
        /// </summary>
        [Category("Appearance")]
        public Image CustomCheckImage
        {
            get
            {
                return this.customCheckImage;
            }
            set
            {
                this.customCheckImage = value;
            }
        }

        /// <summary>
        /// 选择模式为图片时，自定义的选择图片的强制大小
        /// </summary>
        [Category("Appearance")]
        public Size CustomCheckImageSize
        {
            get
            {
                if (this.customCheckImageSize.Width == 0 || this.customCheckImageSize.Height == 0)
                {
                    if (this.CustomCheckImage == null)
                    {
                        this.customCheckImageSize = new Size(24, 18);
                    }
                    else
                    {
                        this.customCheckImageSize = new Size(this.customCheckImage.Width, this.customCheckImage.Height);
                    }
                }
                return this.customCheckImageSize;
            }
            set
            {
                this.customCheckImageSize = value;
            }
        }


        /// <summary>
        /// 自定义图像时，是否填充整个区域
        /// </summary>
        [Category("Appearance")]
        public bool CustomCheckImageFillClient
        {
            get
            {
                return this.customCheckImageFillClient;
            }
            set
            {
                this.customCheckImageFillClient = value;
            }
        }
		#endregion
	}
}
