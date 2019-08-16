
using System;


namespace IE310.Table.Models 
{
	/// <summary>
    /// ͼ����Cell�е���ʾģʽ
	/// Specifies how Images are sized within a Cell
	/// </summary>
	public enum I3ImageSizeMode
	{
		/// <summary>
        /// ��ͨģʽ
		/// The Image will be displayed normally
		/// </summary>
		Normal = 0,

		/// <summary>
        /// ���ݳ������������ģʽ
		/// The Image will be stretched/shrunken to fit the Cell
		/// </summary>
		SizedToFit = 1,

		/// <summary>
        /// ��������ģʽ
		/// The Image will be scaled to fit the Cell
		/// </summary>
		ScaledToFit = 2
	}
}
