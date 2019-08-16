
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Models;
using IE310.Table.Themes;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Base class for Renderers 
	/// </summary>
	public abstract class I3Renderer : II3Renderer, IDisposable
	{
		#region Class Data

		/// <summary>
		/// A StringFormat object that specifies how the Renderers 
		/// contents are drawn
		/// </summary>
		private StringFormat stringFormat;

		/// <summary>
		/// The brush used to draw the Renderers background
		/// </summary>
		private SolidBrush backBrush;

		/// <summary>
		/// The brush used to draw the Renderers foreground
		/// </summary>
		private SolidBrush foreBrush;

		/// <summary>
		/// A Rectangle that specifies the size and location of the Renderer
		/// </summary>
		private Rectangle bounds;

		/// <summary>
		/// The Font of the text displayed by the Renderer
		/// </summary>
		private Font font;

		/// <summary>
		/// The width of a Cells border
		/// </summary>
		protected static int BorderWidth = 1;

        /// <summary>
        /// 文本是否自动换行
        /// </summary>
        private bool autoWarp;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Renderer class with default settings
		/// </summary>
		protected I3Renderer()
		{
			this.bounds = Rectangle.Empty;
			this.font = null;
			
			this.stringFormat = new StringFormat();
			this.stringFormat.LineAlignment = StringAlignment.Center;
			this.stringFormat.Alignment = StringAlignment.Near;
			this.stringFormat.FormatFlags = StringFormatFlags.NoWrap;
			this.stringFormat.Trimming = StringTrimming.EllipsisCharacter;

			this.backBrush = new SolidBrush(Color.Transparent);
			this.foreBrush = new SolidBrush(Color.Black);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Releases the unmanaged resources used by the Renderer and 
		/// optionally releases the managed resources
		/// </summary>
		public virtual void Dispose()
		{
			if (this.backBrush != null)
			{
				this.backBrush.Dispose();
				this.backBrush = null;
			}
				
			if (this.foreBrush != null)
			{
				this.foreBrush.Dispose();
				this.foreBrush = null;
			}
		}


		/// <summary>
		/// Sets the color of the brush used to draw the background
		/// </summary>
		/// <param name="color">The color of the brush</param>
		protected void SetBackBrushColor(Color color)
		{
			if (this.BackBrush.Color != color)
			{
				this.BackBrush.Color = color;
			}
		}


		/// <summary>
		/// Sets the color of the brush used to draw the foreground
		/// </summary>
		/// <param name="color">The color of the brush</param>
		protected void SetForeBrushColor(Color color)
		{
			if (this.ForeBrush.Color != color)
			{
				this.ForeBrush.Color = color;
			}
		}



		#endregion


		#region Properties

		/// <summary>
		/// Gets the rectangle that represents the client area of the Renderer
		/// </summary>
		[Browsable(false)]
		public abstract Rectangle ClientRectangle
		{
			get;
		}


		/// <summary>
		/// Gets or sets the size and location of the Renderer
		/// </summary>
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}

			set
			{
				this.bounds = value;
			}
		}


		/// <summary>
		/// Gets or sets the font of the text displayed by the Renderer
		/// </summary>
		[Category("Appearance"),
		Description("The font used to draw the text")]
		public Font Font
		{
			get
			{
				return this.font;
			}

			set
			{
				if (value == null)
				{
					value = Control.DefaultFont;
				}
				
				if (this.font != value)
				{
					this.font = value;
				}
			}
		}

		
		/// <summary>
		/// Gets the brush used to draw the Renderers background
		/// </summary>
		protected SolidBrush BackBrush
		{
			get
			{
				return this.backBrush;
			}
		}


		/// <summary>
		/// Gets the brush used to draw the Renderers foreground
		/// </summary>
		protected SolidBrush ForeBrush
		{
			get
			{
				return this.foreBrush;
			}
		}


		/// <summary>
		/// Gets or sets the foreground Color of the Renderer
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics")]
		public Color ForeColor
		{
			get
			{
				return this.ForeBrush.Color;
			}

			set
			{
				this.SetForeBrushColor(value);
			}
		}


		/// <summary>
		/// Gets or sets the background Color of the Renderer
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics")]
		public Color BackColor
		{
			get
			{
				return this.BackBrush.Color;
			}

			set
			{
				this.SetBackBrushColor(value);
			}
		}


		/// <summary>
		/// Gets or sets a StringFormat object that specifies how the Renderers 
		/// contents are drawn
		/// </summary>
		protected StringFormat StringFormat
		{
			get
			{
				return this.stringFormat;
			}

			set
			{
				this.stringFormat = value;
			}
		}


		/// <summary>
		/// Gets or sets a StringTrimming enumeration that indicates how text that 
		/// is drawn by the Renderer is trimmed when it exceeds the edges of the 
		/// layout rectangle
		/// </summary>
		[Browsable(false)]
		public StringTrimming Trimming
		{
			get
			{
				return this.stringFormat.Trimming;
			}

			set
			{
				this.stringFormat.Trimming = value;
			}
		}


		/// <summary>
		/// Gets or sets how the Renderers contents are aligned horizontally
		/// </summary>
		[Browsable(false)]
		public I3ColumnAlignment Alignment
		{
			get
			{
				switch (this.stringFormat.Alignment)
				{
					case StringAlignment.Near:
						return I3ColumnAlignment.Left;
					
					case StringAlignment.Center:
						return I3ColumnAlignment.Center;

					case StringAlignment.Far:
						return I3ColumnAlignment.Right;
				}

				return I3ColumnAlignment.Left;
			}
			set
			{
				switch (value)
				{
					case I3ColumnAlignment.Left:
						this.stringFormat.Alignment = StringAlignment.Near;
						break;
					
					case I3ColumnAlignment.Center:
						this.stringFormat.Alignment = StringAlignment.Center;
						break;

					case I3ColumnAlignment.Right:
						this.stringFormat.Alignment = StringAlignment.Far;
						break;
				}
			}
		}

        public bool AutoWarp
        {
            get
            {
                return this.autoWarp;
            }
            set
            {
                this.autoWarp = value;
                if (this.autoWarp)
                {
                    if ((this.StringFormat.FormatFlags & StringFormatFlags.NoWrap) == StringFormatFlags.NoWrap)
                    {
                        this.StringFormat.FormatFlags = this.StringFormat.FormatFlags ^ StringFormatFlags.NoWrap;
                    }
                }
                else
                {
                    if ((this.StringFormat.FormatFlags & StringFormatFlags.NoWrap) != StringFormatFlags.NoWrap)
                    {
                        this.StringFormat.FormatFlags = this.StringFormat.FormatFlags ^ StringFormatFlags.NoWrap;
                    }
                }
            }
        }


		/// <summary>
		/// Gets or sets how the Renderers contents are aligned vertically
		/// </summary>
		[Browsable(false)]
		public I3RowAlignment LineAlignment
		{
			get
			{
				switch (this.stringFormat.LineAlignment)
				{
					case StringAlignment.Near:
						return I3RowAlignment.Top;
					
					case StringAlignment.Center:
						return I3RowAlignment.Center;

					case StringAlignment.Far:
						return I3RowAlignment.Bottom;
				}

				return I3RowAlignment.Center;
			}

			set
			{
				switch (value)
				{
					case I3RowAlignment.Top:
						this.stringFormat.LineAlignment = StringAlignment.Near;
						break;
					
					case I3RowAlignment.Center:
						this.stringFormat.LineAlignment = StringAlignment.Center;
						break;

					case I3RowAlignment.Bottom:
						this.stringFormat.LineAlignment = StringAlignment.Far;
						break;
				}
			}
		}


		/// <summary>
		/// Gets whether Visual Styles are enabled for the application
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				return I3ThemeManager.VisualStylesEnabled;
			}
		}

		#endregion
	}
}
