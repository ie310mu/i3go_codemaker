


using System;

namespace IE310.Table.Column
{
	/// <summary>
    /// �е�״̬
	/// Specifies the state of a Column
	/// </summary>
	public enum I3ColumnState
	{
		/// <summary>
        /// ��ͨ
		/// Column is in its normal state
		/// </summary>
		Normal = 1,

		/// <summary>
        /// ���н���
		/// Mouse is over the Column
		/// </summary>
		Hot = 2,

		/// <summary>
        /// ������
		/// Column is being pressed
		/// </summary>
		Pressed = 3
	}

    /// <summary>
    /// �е�״̬
    /// Specifies the state of a Column
    /// </summary>
    public enum I3RowState
    {
        /// <summary>
        /// ��ͨ
        /// Column is in its normal state
        /// </summary>
        Normal = 1,

        /// <summary>
        /// ���н���
        /// Mouse is over the Column
        /// </summary>
        Hot = 2,

        /// <summary>
        /// ������
        /// Column is being pressed
        /// </summary>
        Pressed = 3
    }
}
