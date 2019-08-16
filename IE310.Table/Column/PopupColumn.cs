

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
    /// �ɵ�������һ���ؼ�  ע�⣬�����͵����ڱ༭ʱ�������Զ���e.Cell.Data��ֵ����Ҫ��BeforePopup��EndPopup�н��в���
    /// �磺
    /// 
	/// Represents a Column whose Cells are displayed as a DateTime
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class I3PopupColumn : I3DropDownColumn
    {

        #region Event Handler
        /// <summary>
        /// before the PopupControl is show,you can set value to the PopupControl
        /// </summary>
        public event I3CellEditEventHandler BeforePopup;
        /// <summary>
        /// after the PopupControl is hide,you can get value and set it to your cell or object
        /// if the editor cancel the editing and hide the PopupControl,you can not get this event
        /// </summary>
        public event I3CellEditEventHandler EndPopup;
        #endregion

        #region class data
        private Control popupControl;
        #endregion



        #region Constructor

        /// <summary>
		/// Creates a new DateTimeColumn with default values
		/// </summary>
		public I3PopupColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3PopupColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3PopupColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3PopupColumn(string text, int width, bool visible) : base(text, width, visible)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3PopupColumn(string text, Image image) : base(text, image)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new DateTimeColumn with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3PopupColumn(string text, Image image, int width) : base(text, image, width)
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
        public I3PopupColumn(string text, Image image, int width, bool visible)
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
        /// ��ȡĬ�ϵ�RendererName
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public override string GetDefaultRendererName()
		{
            return "POPUP";
		}


		/// <summary>
        /// ����Ĭ�ϵ�Renderer
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override II3CellRenderer CreateDefaultRenderer()
        {
            return new I3PopupCellRenderer();
		}


		/// <summary>
        /// ��ȡĬ�ϵ�EditorName
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
            return "POPUP";
		}


		/// <summary>
        /// ����Ĭ�ϵ�Editor
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override II3CellEditor CreateDefaultEditor()
		{
			return new I3PopupCellEditor();
		}


        public void OnBeforePopup(I3CellEditEventArgs e)
        {
            if (this.BeforePopup != null)
            {
                this.BeforePopup(this, e);
            }
        }
        public void OnEndPopup(I3CellEditEventArgs e)
        {
            if (this.EndPopup != null)
            {
                this.EndPopup(this, e);
            }
        }

		#endregion


		#region Properties

        /// <summary>
        /// ��ȡ�����õ������ӿؼ�
        /// </summary>
        //[Browsable(false)]
        public Control PopupControl
        {
            get
            {
                return this.popupControl;
            }
            set
            {
                this.popupControl = value;
            }
        }


        /// <summary>
        /// ��ȡĬ�ϱȽ���
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
