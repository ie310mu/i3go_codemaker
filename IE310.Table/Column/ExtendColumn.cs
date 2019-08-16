


using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Renderers;
using IE310.Table.Sorting;


namespace IE310.Table.Column
{
	/// <summary>
    /// 可弹出任意一个控件  注意，此类型的列在编辑时，不会自动对e.Cell.Data赋值，需要在ExtendEdit中进行操作
    /// 
	/// Represents a Column whose Cells are displayed as a DateTime
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3ExtendColumn : I3DropDownColumn
    {
        #region Event Handler
        /// <summary>
        /// in this event to do something like to edit some object
        /// </summary>
        public event I3CellEditEventHandler ExtendEdit;
        #endregion


        #region class data
        #endregion



        #region Constructor

        /// <summary>
		/// Creates a new DateTimeColumn with default values
		/// </summary>
		public I3ExtendColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3ExtendColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ExtendColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3ExtendColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3ExtendColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3ExtendColumn(string text, Image image, int width) : base(text, image, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
        public I3ExtendColumn(string text, Image image, int width, bool visible)
            : base(text, image, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Initializes the DateTimeColumn with default values
		/// </summary>
		internal void Init()
		{
		}

		#endregion


		#region Methods

		/// <summary>
        /// 获取默认的RendererName
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public override string GetDefaultRendererName()
		{
            return "EXTEND";
		}


		/// <summary>
        /// 创建默认的Renderer
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
        {
            return new I3ExtendCellRenderer();
		}


		/// <summary>
        /// 获取默认的EditorName
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
            return "EXTEND";
		}


		/// <summary>
        /// 创建默认的Editor
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return new I3ExtendCellEditor();
		}

        public void OnExtendEdit(I3CellEditEventArgs e)
        {
            if (this.ExtendEdit != null)
            {
                this.ExtendEdit(this, e);
            }
        }

		#endregion


		#region Properties



        /// <summary>
        /// 获取默认比较器
        /// Gets the Type of the Comparer used to compare the Column's Cells when 
        /// the Column is sorting
        /// </summary>
        public override Type DefaultComparerType
        {
            get
            {
                return typeof(I3TextComparer);
            }
        }

		#endregion

    }

}
