


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IE310.Table.Models;


namespace IE310.Table.Cell
{
	/// <summary>
	/// Stores Image related properties for a Cell
	/// </summary>
	internal class I3CellImageStyle 
	{
		#region Class Data

		/// <summary>
        /// Cell����ʾ��ͼ��
		/// The Image displayed in the Cell
		/// </summary>
		private Image image;

		/// <summary>
        /// Cell��ͼ�����ʾģʽ
		/// Determines how Images are sized in the Cell
		/// </summary>
		private I3ImageSizeMode imageSizeMode;

		#endregion


		#region Constructor

		/// <summary>
        /// ���캯��
		/// Initializes a new instance of the CellImageStyle class with default settings
		/// </summary>
		public I3CellImageStyle()
		{
			this.image = null;
			this.imageSizeMode = I3ImageSizeMode.Normal;
		}

		#endregion


		#region Properties

		/// <summary>
        /// ��ȡ������Cell����ʾ��ͼ��
		/// Gets or sets the image that is displayed in the Cell
		/// </summary>
		public Image Image
		{
			get
			{
				return this.image;
			}

			set
			{
				this.image = value;
			}
		}


		/// <summary>
        /// ��ȡ������Cell��ͼ�����ʾģʽ
		/// Gets or sets how the Cells image is sized within the Cell
		/// </summary>
		public I3ImageSizeMode ImageSizeMode
		{
			get
			{
				return this.imageSizeMode;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3ImageSizeMode), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3ImageSizeMode));
				}
				
				if (this.imageSizeMode != value)
				{
					this.imageSizeMode = value;
				}
			}
		}

		#endregion
	}
}
