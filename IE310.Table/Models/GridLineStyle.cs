
using System;


namespace IE310.Table.Models
{
	/// <summary>
    /// 网格线的风格
	/// Specifies the style of the lines drawn when a Table draws its grid lines
	/// </summary>
	public enum I3GridLineStyle
	{
		/// <summary>
        /// 实线
		/// Specifies a solid line
		/// </summary>
		Solid = 0,

		/// <summary>
        /// 破折线
		/// Specifies a line consisting of dashes
		/// </summary>
		Dash = 1,

		/// <summary>
        /// 点线
		/// Specifies a line consisting of dots
		/// </summary>
		Dot = 2,

		/// <summary>
        /// 破折点线
		/// Specifies a line consisting of a repeating pattern of dash-dot
		/// </summary>
		DashDot = 3,

		/// <summary>
        /// 破折点点线
		/// Specifies a line consisting of a repeating pattern of dash-dot-dot
		/// </summary>
		DashDotDot = 4
	}
}
