


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Renderers;
using IE310.Table.Sorting;
using IE310.Table.Design;


namespace IE310.Table.Column
{
	/// <summary>
	/// Represents a Column whose Cells are displayed as a numbers
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3NumberColumn : I3Column
	{
		#region Class Data

		/// <summary>
        /// ÔöÁ¿
		/// The value to increment or decrement a Cell when its up or down buttons are clicked
		/// </summary>
		private object increment;

		/// <summary>
		/// The maximum value for a Cell
        /// </summary>
        private object maximum;

		/// <summary>
		/// The minimum value for a Cell
        /// </summary>
        private object minimum;

		/// <summary>
		/// The alignment of the up and down buttons in the Column
		/// </summary>
		private LeftRightAlignment upDownAlignment;

		/// <summary>
		/// Specifies whether the up and down buttons should be drawn
		/// </summary>
		private bool showUpDownButtons;

        private NumberColumnType numberColumnType;

		#endregion
		
		
		#region Constructor
		
		/// <summary>
		/// Creates a new NumberColumn with default values
		/// </summary>
		public I3NumberColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new NumberColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3NumberColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new NumberColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3NumberColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new NumberColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3NumberColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3NumberColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3NumberColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text, image, width 
		/// and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3NumberColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the NumberColumn with default values
		/// </summary>
		private void Init()
		{
			this.Format = "G";

            this.numberColumnType = NumberColumnType.DECIMAL;
			this.maximum = (decimal) 100;
			this.minimum = (decimal) 0;
			this.increment = (decimal) 1;

			this.showUpDownButtons = false;
            this.upDownAlignment = LeftRightAlignment.Right;
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
			return "NUMBER";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
		{
			return new I3NumberCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return "NUMBER";
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return new I3NumberCellEditor();
		}

        public override string DataToString(object data)
        {
            if (data == null)
            {
                return "";
            }

            if (this.Dictionary != null && this.Dictionary.Contains(data))
            {
                object value = this.Dictionary[data];
                if (value == null)
                {
                    return "";
                }
                return value.ToString();
            }

            switch (this.numberColumnType)
            {
                case NumberColumnType.SBYTE:
                    return Convert.ToSByte(data).ToString(this.Format);
                case NumberColumnType.BYTE:
                    return Convert.ToByte(data).ToString(this.Format);
                case NumberColumnType.SHORT:
                    return Convert.ToInt16(data).ToString(this.Format);
                case NumberColumnType.USHORT:
                    return Convert.ToUInt16(data).ToString(this.Format);
                case NumberColumnType.INT:
                    return Convert.ToInt32(data).ToString(this.Format);
                case NumberColumnType.UINT:
                    return Convert.ToUInt32(data).ToString(this.Format);
                case NumberColumnType.LONG:
                    return Convert.ToInt64(data).ToString(this.Format);
                case NumberColumnType.ULONG:
                    return Convert.ToUInt64(data).ToString(this.Format);
                case NumberColumnType.FLOAT:
                    return Convert.ToSingle(data).ToString(this.Format);
                case NumberColumnType.DOUBLE:
                    return Convert.ToDouble(data).ToString(this.Format);
                case NumberColumnType.DECIMAL:
                    return Convert.ToDecimal(data).ToString(this.Format);
                default:
                    return "";
            }
        }

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the maximum value for Column's Cells
		/// </summary>
		[Category("Appearance"),
        Description("The maximum value for Column's Cells")]
        [TypeConverter(typeof(NumberConvert))]
		public object Maximum
		{
			get
			{
				return this.maximum;
			}

			set
			{
				this.maximum = value;

                if (Convert.ToDecimal(this.minimum) > Convert.ToDecimal(this.maximum))
				{
					this.minimum = this.maximum;
				}

				this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
			}
		}


		/// <summary>
		/// Specifies whether the Maximum property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Maximum property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeMaximum()
        {
            return true;
            //return this.maximum != (decimal) 100;
		}


		/// <summary>
		/// Gets or sets the minimum value for Column's Cells
		/// </summary>
		[Category("Appearance"),
        Description("The minimum value for Column's Cells")]
        [TypeConverter(typeof(NumberConvert))]
        public object Minimum
		{
			get
			{
				return this.minimum;
			}

			set
			{
				this.minimum = value;

                if (Convert.ToDecimal(this.minimum) > Convert.ToDecimal(this.maximum))
				{
					this.maximum = value;
				}

				this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
			}
		}


		/// <summary>
		/// Specifies whether the Minimum property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Minimum property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeMinimum()
        {
            return true;
            //return this.minimum != (decimal) 0;
		}


		/// <summary>
		/// Gets or sets the value to increment or decrement a Cell when its up or down 
		/// buttons are clicked
		/// </summary>
		[Category("Appearance"),
        Description("The value to increment or decrement a Cell when its up or down buttons are clicked")]
        [TypeConverter(typeof(NumberConvert))]
        public object Increment
		{
			get
			{
				return this.increment;
			}

			set
			{
                if (Convert.ToDecimal(value) < new decimal(0))
				{
					throw new ArgumentException("value must be greater than zero");
				}

				this.increment = value;
			}
		}


		/// <summary>
		/// Specifies whether the Increment property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Increment property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeIncrement()
        {
            return true;
            //return this.increment != (decimal) 1;
		}


		/// <summary>
		/// Gets or sets whether the Column's Cells should draw up and down buttons
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Determines whether the Column's Cells draw up and down buttons")]
		public bool ShowUpDownButtons
		{
			get
			{
				return this.showUpDownButtons;
			}

			set
			{
				if (this.showUpDownButtons != value)
				{
					this.showUpDownButtons = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Gets or sets the alignment of the up and down buttons in the Column
		/// </summary>
		[Category("Appearance"),
		DefaultValue(LeftRightAlignment.Right),
		Description("The alignment of the up and down buttons in the Column")]
		public LeftRightAlignment UpDownAlign
		{
			get
			{
				return this.upDownAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(LeftRightAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(LeftRightAlignment));
				}
					
				if (this.upDownAlignment != value)
				{
					this.upDownAlignment = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Gets or sets the string that specifies how a Column's Cell contents 
		/// are formatted
		/// </summary>
		[Category("Appearance"),
		DefaultValue("G"),
		Description("A string that specifies how a column's cell contents are formatted.")]
		public new string Format
		{
			get
			{
				return base.Format;
			}

			set
			{
				base.Format = value;
			}
		}

        public NumberColumnType NumberColumnType
        {
            get
            {
                return this.numberColumnType;
            }
            set
            {
                this.numberColumnType = value;
                switch (this.numberColumnType)
                {
                    case NumberColumnType.SBYTE:
                        this.maximum = Convert.ToSByte(this.maximum);
                        this.minimum = Convert.ToSByte(this.minimum);
                        this.increment = Convert.ToSByte(this.increment);
                        break;
                    case NumberColumnType.BYTE:
                        this.maximum = Convert.ToByte(this.maximum);
                        this.minimum = Convert.ToByte(this.minimum);
                        this.increment = Convert.ToByte(this.increment);
                        break;
                    case NumberColumnType.SHORT:
                        this.maximum = Convert.ToInt16(this.maximum);
                        this.minimum = Convert.ToInt16(this.minimum);
                        this.increment = Convert.ToInt16(this.increment);
                        break;
                    case NumberColumnType.USHORT:
                        this.maximum = Convert.ToUInt16(this.maximum);
                        this.minimum = Convert.ToUInt16(this.minimum);
                        this.increment = Convert.ToUInt16(this.increment);
                        break;
                    case NumberColumnType.INT:
                        this.maximum = Convert.ToInt32(this.maximum);
                        this.minimum = Convert.ToInt32(this.minimum);
                        this.increment = Convert.ToInt32(this.increment);
                        break;
                    case NumberColumnType.UINT:
                        this.maximum = Convert.ToUInt32(this.maximum);
                        this.minimum = Convert.ToUInt32(this.minimum);
                        this.increment = Convert.ToUInt32(this.increment);
                        break;
                    case NumberColumnType.LONG:
                        this.maximum = Convert.ToInt64(this.maximum);
                        this.minimum = Convert.ToInt64(this.minimum);
                        this.increment = Convert.ToInt64(this.increment);
                        break;
                    case NumberColumnType.ULONG:
                        this.maximum = Convert.ToUInt64(this.maximum);
                        this.minimum = Convert.ToUInt64(this.minimum);
                        this.increment = Convert.ToUInt64(this.increment);
                        break;
                    case NumberColumnType.FLOAT:
                        this.maximum = Convert.ToSingle(this.maximum);
                        this.minimum = Convert.ToSingle(this.minimum);
                        this.increment = Convert.ToSingle(this.increment);
                        break;
                    case NumberColumnType.DOUBLE:
                        this.maximum = Convert.ToDouble(this.maximum);
                        this.minimum = Convert.ToDouble(this.minimum);
                        this.increment = Convert.ToDouble(this.increment);
                        break;
                    case NumberColumnType.DECIMAL:
                        this.maximum = Convert.ToDecimal(this.maximum);
                        this.minimum = Convert.ToDecimal(this.minimum);
                        this.increment = Convert.ToDecimal(this.increment);
                        break;
                    default:
                        this.maximum = Convert.ToDecimal(this.maximum);
                        this.minimum = Convert.ToDecimal(this.minimum);
                        this.increment = Convert.ToDecimal(this.increment);
                        break;
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

		#endregion
	}
}
