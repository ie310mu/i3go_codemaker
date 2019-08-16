


using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace IE310.Table.Cell
{
	/// <summary>
	/// Stores CheckBox related properties for a Cell
	/// </summary>
	internal class I3CellCheckStyle 
	{
		#region Class Data

		/// <summary>
        /// ѡ��״̬��Windows��׼ѡ��״̬��������ֵ��ѡ�У�δѡ�У���ȷ��
		/// The CheckState of the Cells check box
		/// </summary>
		private CheckState checkState;

		/// <summary>
        /// �Ƿ�֧�ֲ�ȷ��״̬
		/// Specifies whether the Cells check box supports an indeterminate state
		/// </summary>
		private bool threeState;

		#endregion


		#region Constructor

		/// <summary>
        /// ���캯��
		/// Initializes a new instance of the CellCheckStyle class with default settings
		/// </summary>
		public I3CellCheckStyle()
		{
			this.checkState = CheckState.Unchecked;
			this.threeState = false;
		}

		#endregion


		#region Properties

		/// <summary>
        /// ��ȡ�������Ƿ�ѡ��  this.checkState != CheckState.Unchecked ʱ������true
        /// ����ʱ��value=true��ӦCheckState.Checked��value=false��ӦCheckState.Unchecked
		/// Gets or sets whether the Cell is in the checked state
		/// </summary>
		public bool Checked
		{
			get
			{
				return (this.checkState != CheckState.Unchecked);
			}

			set
			{
				if (value != this.Checked)
				{
					this.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
				}
			}
		}


		/// <summary>
        /// ��ȡ������ѡ��״̬
		/// Gets or sets the state of the Cells check box
		/// </summary>
		public CheckState CheckState
		{
			get
			{
				return this.checkState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(CheckState));
				}
				
				if (this.checkState != value)
				{
					this.checkState = value;
				}
			}
		}
		

		/// <summary>
        /// ��ȡ������ �Ƿ�֧�ֲ�ȷ��״̬
		/// Gets or sets a value indicating whether the Cells check box 
		/// will allow three check states rather than two
		/// </summary>
		public bool ThreeState
		{
			get
			{
				return this.threeState;
			}

			set
			{
				this.threeState = value;
			}
		}

		#endregion
	}
}
