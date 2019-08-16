


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
        /// 选择状态，Windows标准选择状态，有三个值：选中，未选中，不确定
		/// The CheckState of the Cells check box
		/// </summary>
		private CheckState checkState;

		/// <summary>
        /// 是否支持不确定状态
		/// Specifies whether the Cells check box supports an indeterminate state
		/// </summary>
		private bool threeState;

		#endregion


		#region Constructor

		/// <summary>
        /// 构造函数
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
        /// 获取或设置是否选择  this.checkState != CheckState.Unchecked 时，返回true
        /// 设置时，value=true对应CheckState.Checked，value=false对应CheckState.Unchecked
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
        /// 获取或设置选择状态
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
        /// 获取或设置 是否支持不确定状态
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
