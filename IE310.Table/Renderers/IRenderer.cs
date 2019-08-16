
using System;
using System.Drawing;

using IE310.Table.Models;
using IE310.Table.Column;
using IE310.Table.Row;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Exposes common methods provided by renderers
	/// </summary>
	public interface II3Renderer
	{ 
		/// <summary>
		/// Gets a Rectangle that represents the client area of the object 
		/// being rendered
		/// </summary>
		Rectangle ClientRectangle
		{
			get;
		}


		/// <summary>
		/// Gets or sets a Rectangle that represents the size and location 
		/// of the object being rendered
		/// </summary>
		Rectangle Bounds
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the font of the text displayed by the object being 
		/// rendered
		/// </summary>
		Font Font
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the foreground color of the object being rendered
		/// </summary>
		Color ForeColor
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the background color for the object being rendered
		/// </summary>
		Color BackColor
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets how the Renderers contents are aligned horizontally
		/// </summary>
		I3ColumnAlignment Alignment
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets how the Renderers contents are aligned vertically
		/// </summary>
		I3RowAlignment LineAlignment
		{
			get;
			set;
		}
	}
}
