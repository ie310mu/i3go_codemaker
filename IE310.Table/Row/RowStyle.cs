using System;
using System.ComponentModel;
using System.Drawing;


namespace IE310.Table.Row
{
	/// <summary>
	/// Stores visual appearance related properties for a Row
	/// </summary>
	public class I3RowStyle
	{
		#region Class Data

		/// <summary>
		/// The background color of the Row
		/// </summary>
		private Color backColor;

		/// <summary>
		/// The foreground color of the Row
		/// </summary>
		private Color foreColor;

		/// <summary>
		/// The font used to draw the text in the Row
		/// </summary>
		private Font font;

		/// <summary>
		/// The alignment of the text in the Row
		/// </summary>
		private I3RowAlignment alignment;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the RowStyle class with default settings
		/// </summary>
		public I3RowStyle()
		{
			this.backColor = Color.Empty;
			this.foreColor = Color.Empty;
			this.font = null;
			this.alignment = I3RowAlignment.Center;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the Font used by the Row
		/// </summary>
		[Category("Appearance"),
		Description("The font used to display text in the row")]
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
		/// Gets or sets the background color for the Row
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics in the row")]
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
		/// Gets or sets the foreground color for the Row
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics in the row")]
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
		/// Gets or sets the vertical alignment of the text displayed in the Row
		/// </summary>
		[Category("Appearance"),
		DefaultValue(I3RowAlignment.Center),
		Description("The vertical alignment of the objects displayed in the row")]
		public I3RowAlignment Alignment
		{
			get
			{
				return this.alignment;
			}

			set
			{
				this.alignment = value;
			}
		}

		#endregion
	}
}
