using System;
using System.Drawing;  
using System.Runtime.InteropServices;


namespace IE310.Core.Win32
{
	/// <summary>
	/// The POINT structure defines the x- and y- coordinates of a point
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
    public struct I3POINT
	{
		/// <summary>
		/// Specifies the x-coordinate of the point
		/// </summary>
		public int x;
			
		/// <summary>
		/// Specifies the y-coordinate of the point
		/// </summary>
		public int y;


		/// <summary>
		/// Creates a new RECT struct with the specified x and y coordinates
		/// </summary>
		/// <param name="x">The x-coordinate of the point</param>
		/// <param name="y">The y-coordinate of the point</param>
		public I3POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


		/// <summary>
		/// Creates a new POINT struct from the specified Point
		/// </summary>
		/// <param name="p">The Point to create the POINT from</param>
		/// <returns>A POINT struct with the same x and y coordinates as 
		/// the specified Point</returns>
		public static I3POINT FromPoint(Point p)
		{
			return new I3POINT(p.X, p.Y);
		}


		/// <summary>
		/// Returns a Point with the same x and y coordinates as the POINT
		/// </summary>
		/// <returns>A Point with the same x and y coordinates as the POINT</returns>
		public Point ToPoint()
		{
			return new Point(this.x, this.y);
		}
	}
}
