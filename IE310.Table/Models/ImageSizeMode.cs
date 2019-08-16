
using System;


namespace IE310.Table.Models 
{
	/// <summary>
    /// 图像在Cell中的显示模式
	/// Specifies how Images are sized within a Cell
	/// </summary>
	public enum I3ImageSizeMode
	{
		/// <summary>
        /// 普通模式
		/// The Image will be displayed normally
		/// </summary>
		Normal = 0,

		/// <summary>
        /// 根据长或宽进行拉伸的模式
		/// The Image will be stretched/shrunken to fit the Cell
		/// </summary>
		SizedToFit = 1,

		/// <summary>
        /// 整体拉伸模式
		/// The Image will be scaled to fit the Cell
		/// </summary>
		ScaledToFit = 2
	}
}
