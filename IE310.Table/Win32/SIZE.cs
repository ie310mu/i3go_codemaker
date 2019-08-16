using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace IE310.Table.Win32
{
	/// <summary>
	/// The SIZE structure specifies the width and height of a rectangle
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
    public struct I3SIZE
	{
		/// <summary>
		/// Specifies the x-coordinate of the point
		/// </summary>
		public int cx;
			
		/// <summary>
		/// Specifies the y-coordinate of the point
		/// </summary>
		public int cy;


		/// <summary>
		/// Creates a new SIZE struct with the specified width and height
		/// </summary>
		/// <param name="cx">The width component of the new SIZE</param>
		/// <param name="cy">The height component of the new SIZE</param>
		public I3SIZE(int cx, int cy)
		{
			this.cx = cx;
			this.cy = cy;
		}


		/// <summary>
		/// Creates a new SIZE struct from the specified Size
		/// </summary>
		/// <param name="s">The Size to create the SIZE from</param>
		/// <returns>A SIZE struct with the same width and height values as 
		/// the specified Point</returns>
		public static I3SIZE FromSize(Size s)
		{
			return new I3SIZE(s.Width, s.Height);
		}


		/// <summary>
		/// Returns a Point with the same width and height values as the SIZE
		/// </summary>
		/// <returns>A Point with the same width and height values as the SIZE</returns>
		public Size ToSize()
		{
			return new Size(this.cx, this.cy);
		}
	}
}
