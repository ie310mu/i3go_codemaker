/*
 * Copyright ?2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Renderers;
using System.Collections;
using IE310.Table.Design;


namespace IE310.Table.Column
{
	/// <summary>
    /// 列的父类
	/// Summary description for Column.
	/// </summary>
	[DesignTimeVisible(false)]
    [ToolboxItem(false)]
	public abstract class I3Column : Component
	{
		#region Event Handlers

		/// <summary>
        /// 列属性改变事件
		/// Occurs when one of the Column's properties changes
		/// </summary>
		public event I3ColumnEventHandler PropertyChanged;

		#endregion
         

		#region Class Data

        private string key = "";

        
		// Column state flags
        /// <summary>
        /// 可编辑标志
        /// </summary>
		private readonly static int STATE_EDITABLE_Const = 1;
        /// <summary>
        /// Enabled标志
        /// </summary>
		private readonly static int STATE_ENABLED_Const = 2;
        /// <summary>
        /// Visible标志
        /// </summary>
		private readonly static int STATE_VISIBLE_Const = 4;
        /// <summary>
        /// Selectable标志
        /// </summary>
		private readonly static int STATE_SELECTABLE_Const = 8;
        /// <summary>
        /// Sortable标志
        /// </summary>
		private readonly static int STATE_SORTABLE_Const = 16;
		
		/// <summary>
		/// The amount of space on each side of the Column that can 
		/// be used as a resizing handle
		/// </summary>
		public static readonly int ResizePadding_Const = 4;

		/// <summary>
        /// 列的默认宽度
		/// The default width of a Column
		/// </summary>
		public static readonly int DefaultWidth_Const = 75;
		
		/// <summary>
        /// 列的最大宽度
		/// The maximum width of a Column
		/// </summary>
		public static readonly int MaximumWidth_Const = 1024;
		
		/// <summary>
        /// 列的最小宽度
		/// The minimum width of a Column
		/// </summary>
		public static readonly int MinimumWidth_Const = ResizePadding_Const * 4;

		/// <summary>
        /// 状态
		/// Contains the current state of the the Column
		/// </summary>
		public byte state;

		/// <summary>
        /// 标题
		/// The text displayed in the Column's header
		/// </summary>
		private string caption;

        private string dataMember;

        private IDictionary dictionary;

		
		/// <summary>
        /// 格式化字符串
		/// A string that specifies how a Column's Cell contents are formatted
		/// </summary>
		private string format;

		/// <summary>
        /// 列标题对齐方式
		/// The alignment of the text displayed in the Column's header
		/// </summary>
        private I3ColumnAlignment headerAlignment;

        
        /// <summary>
        /// 内容对齐方式
        /// </summary>
        private I3ColumnAlignment cellAlignment;

		/// <summary>
        /// 列的宽度
		/// The width of the Column
		/// </summary>
		private int width;

		/// <summary>
        /// 在列标题中显示的图像
		/// The Image displayed on the Column's header
		/// </summary>
		private Image image;

		/// <summary>
        /// 标识Image是否显示在列标题的右边
		/// Specifies whether the Image displayed on the Column's header should 
		/// be draw on the right hand side of the Column
		/// </summary>
		private bool imageOnRight;

		/// <summary>
        /// 标识列的状态
		/// The current state of the Column
		/// </summary>
		private I3ColumnState columnState;

		/// <summary>
        /// 列标题上显示的提示信息
		/// The text displayed when a ToolTip is shown for the Column's header
		/// </summary>
		private string tooltipText;

		/// <summary>
        /// 所属的ColumnModel
		/// The ColumnModel that the Column belongs to
		/// </summary>
		private I3ColumnModel columnModel;

		/// <summary>
        /// 列的起始位置
		/// The x-coordinate of the column's left edge in pixels
		/// </summary>
		private int x;

		/// <summary>
        /// 列的排序方式
		/// The current SortOrder of the Column
		/// </summary>
		private SortOrder sortOrder;

		/// <summary>
        /// 与列关联的ICellRenderer
		/// The CellRenderer used to draw the Column's Cells
		/// </summary>
		private II3CellRenderer renderer;

		/// <summary>
        /// 与列关联的编辑器
		/// The CellEditor used to edit the Column's Cells
		/// </summary>
		private II3CellEditor editor;

		/// <summary>
        /// 比较器的类
		/// The Type of the IComparer used to compare the Column's Cells
		/// </summary>
		private Type comparer;


        /// <summary>
        /// 标识行是否是选中状态
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// 文本是否自动换行
        /// </summary>
        private bool cellTextAutoWarp;

        private object tag;

        private float needWidth = 0;

		#endregion
		

		#region Constructor
		
		/// <summary>
		/// Creates a new Column with default values
		/// </summary>
		public I3Column() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new Column with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public I3Column(string text) : base()
		{
			this.Init();

			this.caption = text;
		}


		/// <summary>
		/// Creates a new Column with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public I3Column(string text, int width) : base()
		{
			this.Init();

			this.caption = text;
			this.width = width;
		}


		/// <summary>
		/// Creates a new Column with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3Column(string text, int width, bool visible) : base()
		{
			this.Init();

			this.caption = text;
			this.width = width;
			this.Visible = visible;
		}


		/// <summary>
		/// Creates a new Column with the specified header text and image
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		public I3Column(string text, Image image) : base()
		{
			this.Init();

			this.caption = text;
			this.image = image;
		}


		/// <summary>
		/// Creates a new Column with the specified header text, image and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		public I3Column(string text, Image image, int width) : base()
		{
			this.Init();

			this.caption = text;
			this.image = image;
			this.width = width;
		}


		/// <summary>
		/// Creates a new Column with the specified header text, image, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="image">The image displayed on the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public I3Column(string text, Image image, int width, bool visible) : base()
		{
			this.Init();

			this.caption = text;
			this.image = image;
			this.width = width;
			this.Visible = visible;
		}


		/// <summary>
		/// Initialise default values
		/// </summary>
		private void Init()
        {
            this.key = "";
            this.dataMember = "";
            this.dictionary = null;
			this.caption = "";
			this.width = I3Column.DefaultWidth_Const;
			this.columnState = I3ColumnState.Normal;
            this.headerAlignment = I3ColumnAlignment.Center;
            this.cellAlignment = I3ColumnAlignment.Left;
			this.image = null;
			this.imageOnRight = false;
			this.columnModel = null;
			this.x = 0;
			this.tooltipText = null;
			this.format = "";
			this.sortOrder = SortOrder.None;
			this.renderer = null;
			this.editor = null;
            this.comparer = null;
            this.isSelected = false;
            this.cellTextAutoWarp = false;

			this.state = (byte) (STATE_ENABLED_Const | STATE_EDITABLE_Const | STATE_VISIBLE_Const | STATE_SELECTABLE_Const | STATE_SORTABLE_Const);
		}

		#endregion


		#region Methods

		/// <summary>
        /// 获取Column的默认CellRenderer的名称
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public abstract string GetDefaultRendererName();


		/// <summary>
        /// 创建Column的默认CellRenderer
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public abstract II3CellRenderer CreateDefaultRenderer();


		/// <summary>
        /// 获取默认编辑器的名称
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public abstract string GetDefaultEditorName();


		/// <summary>
        /// 创建默认编辑器
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public abstract II3CellEditor CreateDefaultEditor();


		/// <summary>
        /// 获取状态值
		/// Returns the state represented by the specified state flag
		/// </summary>
		/// <param name="flag">A flag that represents the state to return</param>
		/// <returns>The state represented by the specified state flag</returns>
		internal bool GetState(int flag)
		{
			return ((this.state & flag) != 0);
		}


		/// <summary>
        /// 设置状态值
		/// Sets the state represented by the specified state flag to the specified value
		/// </summary>
		/// <param name="flag">A flag that represents the state to be set</param>
		/// <param name="value">The new value of the state</param>
		internal void SetState(int flag, bool value)
		{
			this.state = (byte) (value ? (this.state | flag) : (this.state & ~flag));
		}

        public virtual string DataToString(object data)
        {
            if (data == null)
            {
                return "";
            }

            if (this.dictionary != null && this.dictionary.Contains(data))
            {
                object value = this.dictionary[data];
                if (value == null)
                {
                    return "";
                }
                return value.ToString();
            }

            return data.ToString();
        }

		#endregion


		#region Properties

		/// <summary>
        /// 获取或设置列标题
		/// Gets or sets the text displayed on the Column header
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The text displayed in the column's header.")]
		public string Caption
		{
			get
			{
				return this.caption;
			}

			set
			{
				if (value == null)
				{
					value = "";
				}
				
				if (!value.Equals(this.caption))
				{
					string oldText = this.caption;
					
					this.caption = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.TextChanged, oldText));
				}
			}
		}


        /// <summary>
        /// 列的主键，具有唯一性，主要用于排序帮助类中
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }


        public string DataMember
        {
            get
            {
                return this.dataMember;
            }
            set
            {
                this.dataMember = value;
            }
        }

        [Browsable(false)]
        public IDictionary Dictionary
        {
            get
            {
                return this.dictionary;
            }
            set
            {
                this.dictionary = value;
            }
        }


		/// <summary>
        /// 获取或设置格式化字符串
		/// Gets or sets the string that specifies how a Column's Cell contents 
		/// are formatted
		/// </summary>
		[Category("Appearance"),
		DefaultValue(""),
		Description("A string that specifies how a column's cell contents are formatted.")]
		public string Format
		{
			get
			{
				return this.format;
			}

			set
			{
				if (value == null)
				{
					value = "";
				}

				if (!value.Equals(this.format))
				{
					string oldFormat = this.format;
					
					this.format = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.FormatChanged, oldFormat));
				}
			}
		}


		/// <summary>
        /// 获取或设置列标题对齐方式
		/// Gets or sets the horizontal alignment of the Column's Cell contents
		/// </summary>
		[Category("Appearance"),
		DefaultValue(I3ColumnAlignment.Center),
		Description("The horizontal alignment of the column's header")]
		public virtual I3ColumnAlignment HeaderAlignment
		{
			get
			{
				return this.headerAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3ColumnAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3ColumnAlignment));
				}
					
				if (this.headerAlignment != value)
				{
					I3ColumnAlignment oldAlignment = this.headerAlignment;
					
					this.headerAlignment = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.AlignmentChanged, oldAlignment));
				}
			}
		}


        [Category("Appearance"),
        DefaultValue(I3ColumnAlignment.Left),
        Description("The horizontal alignment of the column's content")]
        public virtual I3ColumnAlignment CellAlignment
        {
            get
            {
                return this.cellAlignment;
            }
            set
            {
                if (!Enum.IsDefined(typeof(I3ColumnAlignment), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3ColumnAlignment));
                }

                if (this.cellAlignment != value)
                {
                    I3ColumnAlignment oldAlignment = this.cellAlignment;

                    this.cellAlignment = value;

                    this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.AlignmentChanged, oldAlignment));
                }
            }
        }


		/// <summary>
        /// 获取或设置列宽度
		/// Gets or sets the width of the Column
		/// </summary>
		[Category("Appearance"),
		Description("The width of the column.")]
		public int Width
		{
			get
			{
				return this.width;
			}

			set
			{
				if (this.width != value)
				{
					int oldWidth = this.Width;
					
					// Set the width, and check min & max
					this.width = Math.Min(Math.Max(value, MinimumWidth_Const), MaximumWidth_Const);
					
					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.WidthChanged, oldWidth));
				}
			}
		}


		/// <summary>
        /// 返回序列化对象时是否需要序列化列宽度属性，
		/// Specifies whether the Width property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Width property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeWidth()
		{
			return this.Width != I3Column.DefaultWidth_Const;
		}


		/// <summary>
        /// 获取或设置列标题图像
		/// Gets or sets the Image displayed in the Column's header
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("Ihe image displayed in the column's header")]
		public Image HeaderImage
		{
			get
			{
				return this.image;
			}

			set
			{
				if (this.image != value)
				{
					Image oldImage = this.HeaderImage;
					
					this.image = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.ImageChanged, oldImage));
				}
			}
		}


		/// <summary>
        /// 获取或设置列标题图像是否显示在右边
		/// Gets or sets whether the Image displayed on the Column's header should 
		/// be draw on the right hand side of the Column
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Specifies whether the image displayed on the column's header should be drawn on the right hand side of the column")]
		public bool HeaderImageOnRight
		{
			get
			{
				return this.imageOnRight;
			}

			set
			{
				if (this.imageOnRight != value)
				{
					this.imageOnRight = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.ImageChanged, null));
				}
			}
		}


		/// <summary>
        /// 获取列状态
		/// Gets the state of the Column
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public I3ColumnState ColumnState
		{
			get
			{
				return this.columnState;
			}
		}


		/// <summary>
        /// 获取或设置列状态，并引发State改变事件
		/// Gets or sets the state of the Column
		/// </summary>
		internal I3ColumnState InternalColumnState
		{
			get
			{
				return this.ColumnState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3ColumnState), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3ColumnState));
				}

				if (this.columnState != value)
				{
					I3ColumnState oldState = this.columnState;
					
					this.columnState = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.StateChanged, oldState));
				}
			}
		}


		/// <summary>
        /// 获取或设置Visible属性，并引发Visible改变事件
		/// Gets or sets the whether the Column is displayed
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Determines whether the column is visible or hidden.")]
		public bool Visible
		{
			get
			{
				return this.GetState(STATE_VISIBLE_Const);
			}

			set
			{
				bool visible = this.Visible;
				
				this.SetState(STATE_VISIBLE_Const, value);
				
				if (visible != value)
				{
					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.VisibleChanged, visible));
				}
			}
		}


		/// <summary>
        /// 获取或设置Sortable属性，并引发Sortable改变事件
		/// Gets or sets whether the Column is able to be sorted
		/// </summary>
        [Category("Behavior"),
		DefaultValue(true),
		Description("Determines whether the column is able to be sorted.")]
		public virtual bool Sortable
		{
			get
			{
				return this.GetState(STATE_SORTABLE_Const);
			}

			set
			{
				bool sortable = this.Sortable;
				
				this.SetState(STATE_SORTABLE_Const, value);
				
				if (sortable != value)
				{
					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.SortableChanged, sortable));
				}
			}
		}


		/// <summary>
        /// 获取或设置关联的ICellRenderer对象，并引发CellRenderer改变事件
		/// Gets or sets the user specified ICellRenderer that is used to draw the 
		/// Column's Cells
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public II3CellRenderer Renderer
		{
			get
			{
				return this.renderer;
			}

			set
			{
				if (this.renderer != value)
				{
					this.renderer = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
        /// 获取或设置关联的ICellEditor对象，并引发Editor改变事件
		/// Gets or sets the user specified ICellEditor that is used to edit the 
		/// Column's Cells
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public II3CellEditor Editor
		{
			get
			{
				return this.editor;
			}

			set
			{
				if (this.editor != value)
				{
					this.editor = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.EditorChanged, null));
				}
			}
		}
		
		
		/// <summary>
        /// 获取或设置比较器，并引发Comparer改变事件
		/// Gets or sets the user specified Comparer type that is used to edit the 
		/// Column's Cells
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type Comparer
		 {
			 get
			 {
				 return this.comparer;
			 }

			 set
			 {
				 if (this.comparer != value)
				 {
					 this.comparer = value;

					 this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.ComparerChanged, null));
				 }
			 }
		 }


		/// <summary>
        /// 获取默认比较器
		/// Gets the Type of the default Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		[Browsable(false)]
		public abstract Type DefaultComparerType
		{
			get;
		}


		/// <summary>
        /// 获取排序方式
		/// Gets the current SortOrder of the Column
		/// </summary>
		[Browsable(false)]
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}


		/// <summary>
        /// 获取或设置排序方式，并引发SortOrder改变事件
		/// Gets or sets the current SortOrder of the Column
		/// </summary>
		internal SortOrder InternalSortOrder
		{
			get
			{
				return this.SortOrder;
			}

			set
			{
				if (!Enum.IsDefined(typeof(SortOrder), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(SortOrder));
				}

				if (this.sortOrder != value)
				{
					SortOrder oldOrder = this.sortOrder;
					
					this.sortOrder = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.SortOrderChanged, oldOrder));
				}
			}
		}


		/// <summary>
        /// 获取或设置Editable属性，并引发Editable改变事件
		/// Gets or sets a value indicating whether the Column's Cells contents 
		/// are able to be edited
		/// </summary>
        [Category("Behavior"),
		Description("Controls whether the column's cell contents are able to be changed by the user")]
		public virtual bool Editable
		{
			get
			{
				if (!this.GetState(STATE_EDITABLE_Const))
				{
					return false;
				}

				return this.Visible && this.Enabled;
			}

			set
			{
				bool editable = this.GetState(STATE_EDITABLE_Const);
				
				this.SetState(STATE_EDITABLE_Const, value);
				
				if (editable != value)
				{
					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.EditableChanged, editable));
				}
			}
		}


		/// <summary>
        /// 获取是否需要序列化Editable属性
		/// Specifies whether the Editable property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Editable property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeEditable()
		{
			return !this.GetState(STATE_EDITABLE_Const);
		}


		/// <summary>
        /// 获取或设置Enabled属性，并引发Enabled改变事件
		/// Gets or sets a value indicating whether the Column's Cells can respond to 
		/// user interaction
		/// </summary>
        [Category("Behavior"),
		Description("Indicates whether the column's cells can respond to user interaction")]
		public bool Enabled
		{
			get
			{
				if (!this.GetState(STATE_ENABLED_Const))
				{
					return false;
				}

				if (this.ColumnModel == null)
				{
					return true;
				}

				return this.ColumnModel.Enabled;
			}

			set
			{
				bool enabled = this.GetState(STATE_ENABLED_Const);
				
				this.SetState(STATE_ENABLED_Const, value);
				
				if (enabled != value)
				{
					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.EnabledChanged, enabled));
				}
			}
		}


		/// <summary>
        /// 获取是否需要序列化Enabled属性
		/// Specifies whether the Enabled property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Enabled property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeEnabled()
		{
			return !this.GetState(STATE_ENABLED_Const);
		}


		/// <summary>
        /// 获取或设置Selectable属性，并引发Selectable改变事件
		/// Gets or sets a value indicating whether the Column's Cells can be selected
		/// </summary>
        [Category("Behavior"),
		DefaultValue(true),
		Description("Indicates whether the column's cells can be selected")]
		public virtual bool Selectable
		{
			get
			{
				return this.GetState(STATE_SELECTABLE_Const);
			}

			set
			{
				bool selectable = this.Selectable;
				
				this.SetState(STATE_SELECTABLE_Const, value);
				
				if (selectable != value)
				{
					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.SelectableChanged, selectable));
				}
			}
		}


		/// <summary>
        /// 获取或设置ToolTipText属性，并引发ToolTipText改变事件
		/// Gets or sets the ToolTip text associated with the Column
		/// </summary>
        [Category("Behavior"),
		DefaultValue(null),
		Description("The ToolTip text associated with the Column")]
		public string ToolTipText
		{
			get
			{
				return this.tooltipText;
			}

			set
			{
				if (value == null)
				{
					value = "";
				}
				
				if (!value.Equals(this.tooltipText))
				{
					string oldTip = this.tooltipText;
					
					this.tooltipText = value;

					this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.ToolTipTextChanged, oldTip));
				}
			}
		}


		/// <summary>
        /// 获取或设置起始位置
		/// Gets the x-coordinate of the column's left edge in pixels
		/// </summary>
		internal int X
		{
			get
			{
				return this.x;
			}
			
			set
			{
				this.x = value;
			}
		}


		/// <summary>
        /// 获取Column.Left
		/// Gets the x-coordinate of the column's left edge in pixels
		/// </summary>
		[Browsable(false)]
		public int Left
		{
			get
			{
				return this.X;
			}
		}


		/// <summary>
        /// 获取Column.Right
		/// Gets the x-coordinate of the column's right edge in pixels
		/// </summary>
		[Browsable(false)]
		public int Right
		{
			get
			{
				return this.Left + this.Width;
			}
		}


		/// <summary>
        /// 获取或设置ColumnModel对象
		/// Gets or sets the ColumnModel the Column belongs to
		/// </summary>
		protected internal I3ColumnModel ColumnModel
		{
			get
			{
				return this.columnModel;
			}

			set
			{
				this.columnModel = value;
			}
		}


		/// <summary>
        /// 获取Parent，即ColumnModel
		/// Gets the ColumnModel the Column belongs to.  This member is not 
		/// intended to be used directly from your code
		/// </summary>
		[Browsable(false)]
		public I3ColumnModel Parent
		{
			get
			{
				return this.ColumnModel;
			}
		}


		/// <summary>
        /// 获取是否可引发事件
		/// Gets whether the Column is able to raise events
		/// </summary>
		protected bool CanRaiseEvents
		{
			get
			{
				// check if the ColumnModel that the Colum belongs to is able to 
				// raise events (if it can't, the Colum shouldn't raise events either)
				if (this.ColumnModel != null)
				{
					return this.ColumnModel.CanRaiseEvents;
				}

				return true;
			}
		}

        [Browsable(false)]
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;

                    this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.StateChanged, this.ColumnState));
                }
            }
        }

        [Category("Appearance"),
        DefaultValue(false),
        Description("Determines whether the cell's is AutoWarp")]
        public bool CellTextAutoWarp
        {
            get
            {
                return this.cellTextAutoWarp;
            }
            set
            {
                if (this.cellTextAutoWarp != value)
                {
                    bool oldValue = this.cellTextAutoWarp;

                    this.cellTextAutoWarp = value;

                    this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.AlignmentChanged, oldValue));
                }
            }
        }

        [Browsable(false)]
        public object Tag
        {
            get
            {
                return this.tag;
            }
            set
            {
                this.tag = value;
            }
        }

        /// <summary>
        /// 内容绘制完全所需要的宽度
        /// </summary>
        [Browsable(false)]
        public float NeedWidth
        {
            get
            {
                //float result = 0;
                //if (needWidth > 0)
                //{
                //    result = needWidth;
                //}
                //else 
                //{
                //    result = I3ColumnModel.DefaultColumnWidth_Const;
                //}

                float result = needWidth;
                if (result < I3ColumnModel.MinColumnWidth_Const)
                {
                    result = I3ColumnModel.MinColumnWidth_Const;
                }
                if (result > I3ColumnModel.MaxAutoColumnWidth_Const)
                {
                    result = I3ColumnModel.MaxAutoColumnWidth_Const;
                }

                return result;
            }
            set
            {
                needWidth = value;
            }
        }

		#endregion


		#region Events

		/// <summary>
        /// 列属性改变事件
		/// Raises the PropertyChanged event
		/// </summary>
		/// <param name="e">A ColumnEventArgs that contains the event data</param>
		protected virtual void OnPropertyChanged(I3ColumnEventArgs e)
		{
			if (this.ColumnModel != null)
			{
				e.SetIndex(this.ColumnModel.Columns.IndexOf(this));	
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.ColumnModel != null)
				{
					this.ColumnModel.OnColumnPropertyChanged(e);
				}
				
				if (PropertyChanged != null)
				{
					PropertyChanged(this, e);
				}
			}
		}
		#endregion
	}
}
