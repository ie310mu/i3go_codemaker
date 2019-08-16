

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace IE310.Table.Cell
{
	/// <summary>
    /// 记录了Cell的行列信息
	/// Represents the position of a Cell in a Table
	/// </summary>
	[Serializable(),  
	StructLayout(LayoutKind.Sequential)]
	public struct I3CellPos
	{
		#region Class Data

		/// <summary>
        /// 静态变量，返回一个空的CellPos，其row、column都为-1
		/// Repsesents a null CellPos
		/// </summary>
		public static readonly I3CellPos Empty = new I3CellPos(-1, -1);
		
		/// <summary>
        /// 行号，从0开始
		/// The Row index of this CellPos
		/// </summary>
		private int row;

		/// <summary>
        /// 列号，从0开始
		/// The Column index of this CellPos
		/// </summary>
		private int column;

		#endregion


		#region Constructor

		/// <summary>
        /// 初始化CellPos
		/// Initializes a new instance of the CellPos class with the specified 
		/// row index and column index
		/// </summary>
		/// <param name="row">The Row index of the CellPos</param>
		/// <param name="column">The Column index of the CellPos</param>
		public I3CellPos(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		#endregion


		#region Methods

		/// <summary>
        /// 进行指定的偏移
		/// Translates this CellPos by the specified amount
		/// </summary>
		/// <param name="rows">The amount to offset the row index</param>
		/// <param name="columns">The amount to offset the column index</param>
		public void Offset(int rows, int columns)
		{
			this.row += rows;
			this.column += columns;
		}


		/// <summary>
        /// 与另一个CellPos比较，看行列值是否相等，即判断是否同一个Cell
		/// Tests whether obj is a CellPos structure with the same values as 
		/// this CellPos structure
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns>This method returns true if obj is a CellPos structure 
		/// and its Row and Column properties are equal to the corresponding 
		/// properties of this CellPos structure; otherwise, false</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is I3CellPos))
			{
				return false;
			}

			I3CellPos cellPos = (I3CellPos) obj;

			if (cellPos.Row == this.Row)
			{
				return (cellPos.Column == this.Column);
			}

			return false;
		}


		/// <summary>
        /// 通过位运算，返回HashCode  
        /// 注意 类属性 StructLayout(LayoutKind.Sequential)，能够这样计算可能与它有关
		/// Returns the hash code for this CellPos structure
		/// </summary>
		/// <returns>An integer that represents the hashcode for this 
		/// CellPos</returns>
		public override int GetHashCode()
		{
			return (this.Row ^ ((this.Column << 13) | (this.Column >> 0x13)));
		}


		/// <summary>
        /// 转换成 CellPos:3,1 的字符串形式
		/// Converts the attributes of this CellPos to a human-readable string
		/// </summary>
		/// <returns>A string that contains the row and column indexes of this 
		/// CellPos structure </returns>
		public override string ToString()
		{
			return "CellPos: (" + this.Row + "," + this.Column + ")";
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取或设置行号
		/// Gets or sets the Row index of this CellPos
		/// </summary>
		public int Row
		{
			get
			{
				return this.row;
			}

			set
			{
				this.row = value;
			}
		}


		/// <summary>
        /// 获取或设置列号
		/// Gets or sets the Column index of this CellPos
		/// </summary>
		public int Column
		{
			get
			{
				return this.column;
			}

			set
			{
				this.column = value;
			}
		}


		/// <summary>
        /// 测试是否指向空Cell，通过行=-1或列=-1来得知。
        /// 注：如果直接对行号赋值=-2，则判断不会生效
		/// Tests whether any numeric properties of this CellPos have 
		/// values of -1
		/// </summary>
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return (this.Row == -1 || this.Column == -1);
			}
		}

		#endregion


		#region Operators

		/// <summary>
        /// 判断两个CellPos是否指定同一个Cell  相同时为true
		/// Tests whether two CellPos structures have equal Row and Column 
		/// properties
		/// </summary>
		/// <param name="left">The CellPos structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The CellPos structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if the two CellPos structures 
		/// have equal Row and Column properties</returns>
		public static bool operator ==(I3CellPos left, I3CellPos right)
		{
			if (left.Row == right.Row)
			{
				return (left.Column == right.Column);
			}

			return false;
		}


		/// <summary>
        /// 判断两个CellPos是否指定同一个Cell  相同时为false
		/// Tests whether two CellPos structures differ in their Row and 
		/// Column properties
		/// </summary>
		/// <param name="left">The CellPos structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The CellPos structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if any of the Row and Column 
		/// properties of the two CellPos structures are unequal; otherwise 
		/// false</returns>
		public static bool operator !=(I3CellPos left, I3CellPos right)
		{
			return !(left == right);
		}

		#endregion
	}
}
