


using System; 
using System.ComponentModel;
using System.Drawing;


namespace IE310.Table.Cell
{
	/// <summary>
    /// Cell的外观设置
	/// Stores visual appearance related properties for a Cell
	/// </summary>
	public class I3CellStyle
	{
		#region Class Data

		/// <summary>
        /// 背景色
		/// The background color of the Cell
		/// </summary>
		private Color backColor;

		/// <summary>
        /// 前景色
		/// The foreground color of the Cell
		/// </summary>
		private Color foreColor;

		/// <summary>
        /// 字体
		/// The font used to draw the text in the Cell
		/// </summary>
		private Font font;

		/// <summary>
        /// 与边框的距离
		/// The amount of space between the Cells border and its contents
		/// </summary>
		private I3CellPadding padding;

		#endregion


		#region Constructor

		/// <summary>
        /// 空构造，背景色、前景色、字体、边距 都为空
		/// Initializes a new instance of the CellStyle class with default settings
		/// </summary>
		public I3CellStyle()
		{
			this.backColor = Color.Empty;
			this.foreColor = Color.Empty;
			this.font = null;
			this.padding = I3CellPadding.Empty;
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取或设置字体
		/// Gets or sets the Font used by the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The font used to display text in the cell")]
		public Font Font
		{
			get
			{
				return this.font;
			}

			set
			{
				this.font = value;
			}
		}


		/// <summary>
        /// 获取或设置背景色
		/// Gets or sets the background color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics in the cell")]
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}

			set
			{
				this.backColor = value;
			}
		}


		/// <summary>
        /// 获取或设置前景色
		/// Gets or sets the foreground color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics in the cell")]
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}

			set
			{
				this.foreColor = value;
			}
		}


		/// <summary>
        /// 获取或设置边距
		/// Gets or sets the amount of space between the Cells Border and its contents
		/// </summary>
		[Category("Appearance"),
		Description("The amount of space between the cells border and its contents")]
		public I3CellPadding Padding
		{
			get
			{
				return this.padding;
			}

			set
			{
				this.padding = value;
			}
		}

		#endregion
	}
}
