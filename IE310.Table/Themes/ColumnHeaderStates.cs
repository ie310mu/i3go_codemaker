using System;


namespace IE310.Table.Themes
{
	/// <summary>
	/// 列头状态
	/// </summary>
	public enum I3ColumnHeaderStates 
	{
		/// <summary>
		/// 普通状态
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// 焦点状态
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// 按下状态
		/// </summary>
		Pressed = 3
	}


    /// <summary>
    /// 行头状态
    /// </summary>
    public enum I3RowHeaderStates
    {
        /// <summary>
        /// 普通状态
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 焦点状态
        /// </summary>
        Hot = 2,

        /// <summary>
        /// 按下状态
        /// </summary>
        Pressed = 3
    }
}
