


using System; 
using System.ComponentModel;
using System.Drawing;


namespace IE310.Table.Cell
{
	/// <summary>
    /// Cell���������
	/// Stores visual appearance related properties for a Cell
	/// </summary>
	public class I3CellStyle
	{
		#region Class Data

		/// <summary>
        /// ����ɫ
		/// The background color of the Cell
		/// </summary>
		private Color backColor;

		/// <summary>
        /// ǰ��ɫ
		/// The foreground color of the Cell
		/// </summary>
		private Color foreColor;

		/// <summary>
        /// ����
		/// The font used to draw the text in the Cell
		/// </summary>
		private Font font;

		/// <summary>
        /// ��߿�ľ���
		/// The amount of space between the Cells border and its contents
		/// </summary>
		private I3CellPadding padding;

		#endregion


		#region Constructor

		/// <summary>
        /// �չ��죬����ɫ��ǰ��ɫ�����塢�߾� ��Ϊ��
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
        /// ��ȡ����������
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
        /// ��ȡ�����ñ���ɫ
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
        /// ��ȡ������ǰ��ɫ
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
        /// ��ȡ�����ñ߾�
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
