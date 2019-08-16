

using System;


namespace IE310.Table.Editors
{
	/// <summary>
    /// ��ʼ�༭��ö��
	/// Specifies the action that causes a Cell to start editing
	/// </summary>
	public enum I3EditStartAction
	{
		/// <summary>
        /// ˫��
		/// A double click will start cell editing
		/// </summary>
		DoubleClick = 1,

		/// <summary>
        /// ����
		/// A single click will start cell editing
		/// </summary>
		SingleClick = 2,

		/// <summary>
        /// �Զ����ȼ�
		/// A user defined key press will start cell editing
		/// </summary>
		CustomKey = 3,

        DataInputKey = 4,

        DoubleClick_CustomKey = 5,
        DoubleClick_DataInputKey = 6,
	}
}
