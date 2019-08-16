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
    /// �еĸ���
	/// Summary description for Column.
	/// </summary>
	[DesignTimeVisible(false)]
    [ToolboxItem(false)]
	public abstract class I3Column : Component
	{
		#region Event Handlers

		/// <summary>
        /// �����Ըı��¼�
		/// Occurs when one of the Column's properties changes
		/// </summary>
		public event I3ColumnEventHandler PropertyChanged;

		#endregion
         

		#region Class Data

        private string key = "";

        
		// Column state flags
        /// <summary>
        /// �ɱ༭��־
        /// </summary>
		private readonly static int STATE_EDITABLE_Const = 1;
        /// <summary>
        /// Enabled��־
        /// </summary>
		private readonly static int STATE_ENABLED_Const = 2;
        /// <summary>
        /// Visible��־
        /// </summary>
		private readonly static int STATE_VISIBLE_Const = 4;
        /// <summary>
        /// Selectable��־
        /// </summary>
		private readonly static int STATE_SELECTABLE_Const = 8;
        /// <summary>
        /// Sortable��־
        /// </summary>
		private readonly static int STATE_SORTABLE_Const = 16;
		
		/// <summary>
		/// The amount of space on each side of the Column that can 
		/// be used as a resizing handle
		/// </summary>
		public static readonly int ResizePadding_Const = 4;

		/// <summary>
        /// �е�Ĭ�Ͽ��
		/// The default width of a Column
		/// </summary>
		public static readonly int DefaultWidth_Const = 75;
		
		/// <summary>
        /// �е������
		/// The maximum width of a Column
		/// </summary>
		public static readonly int MaximumWidth_Const = 1024;
		
		/// <summary>
        /// �е���С���
		/// The minimum width of a Column
		/// </summary>
		public static readonly int MinimumWidth_Const = ResizePadding_Const * 4;

		/// <summary>
        /// ״̬
		/// Contains the current state of the the Column
		/// </summary>
		public byte state;

		/// <summary>
        /// ����
		/// The text displayed in the Column's header
		/// </summary>
		private string caption;

        private string dataMember;

        private IDictionary dictionary;

		
		/// <summary>
        /// ��ʽ���ַ���
		/// A string that specifies how a Column's Cell contents are formatted
		/// </summary>
		private string format;

		/// <summary>
        /// �б�����뷽ʽ
		/// The alignment of the text displayed in the Column's header
		/// </summary>
        private I3ColumnAlignment headerAlignment;

        
        /// <summary>
        /// ���ݶ��뷽ʽ
        /// </summary>
        private I3ColumnAlignment cellAlignment;

		/// <summary>
        /// �еĿ��
		/// The width of the Column
		/// </summary>
		private int width;

		/// <summary>
        /// ���б�������ʾ��ͼ��
		/// The Image displayed on the Column's header
		/// </summary>
		private Image image;

		/// <summary>
        /// ��ʶImage�Ƿ���ʾ���б�����ұ�
		/// Specifies whether the Image displayed on the Column's header should 
		/// be draw on the right hand side of the Column
		/// </summary>
		private bool imageOnRight;

		/// <summary>
        /// ��ʶ�е�״̬
		/// The current state of the Column
		/// </summary>
		private I3ColumnState columnState;

		/// <summary>
        /// �б�������ʾ����ʾ��Ϣ
		/// The text displayed when a ToolTip is shown for the Column's header
		/// </summary>
		private string tooltipText;

		/// <summary>
        /// ������ColumnModel
		/// The ColumnModel that the Column belongs to
		/// </summary>
		private I3ColumnModel columnModel;

		/// <summary>
        /// �е���ʼλ��
		/// The x-coordinate of the column's left edge in pixels
		/// </summary>
		private int x;

		/// <summary>
        /// �е�����ʽ
		/// The current SortOrder of the Column
		/// </summary>
		private SortOrder sortOrder;

		/// <summary>
        /// ���й�����ICellRenderer
		/// The CellRenderer used to draw the Column's Cells
		/// </summary>
		private II3CellRenderer renderer;

		/// <summary>
        /// ���й����ı༭��
		/// The CellEditor used to edit the Column's Cells
		/// </summary>
		private II3CellEditor editor;

		/// <summary>
        /// �Ƚ�������
		/// The Type of the IComparer used to compare the Column's Cells
		/// </summary>
		private Type comparer;


        /// <summary>
        /// ��ʶ���Ƿ���ѡ��״̬
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// �ı��Ƿ��Զ�����
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
        /// ��ȡColumn��Ĭ��CellRenderer������
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public abstract string GetDefaultRendererName();


		/// <summary>
        /// ����Column��Ĭ��CellRenderer
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public abstract II3CellRenderer CreateDefaultRenderer();


		/// <summary>
        /// ��ȡĬ�ϱ༭��������
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public abstract string GetDefaultEditorName();


		/// <summary>
        /// ����Ĭ�ϱ༭��
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public abstract II3CellEditor CreateDefaultEditor();


		/// <summary>
        /// ��ȡ״ֵ̬
		/// Returns the state represented by the specified state flag
		/// </summary>
		/// <param name="flag">A flag that represents the state to return</param>
		/// <returns>The state represented by the specified state flag</returns>
		internal bool GetState(int flag)
		{
			return ((this.state & flag) != 0);
		}


		/// <summary>
        /// ����״ֵ̬
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
        /// ��ȡ�������б���
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
        /// �е�����������Ψһ�ԣ���Ҫ���������������
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
        /// ��ȡ�����ø�ʽ���ַ���
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
        /// ��ȡ�������б�����뷽ʽ
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
        /// ��ȡ�������п��
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
        /// �������л�����ʱ�Ƿ���Ҫ���л��п�����ԣ�
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
        /// ��ȡ�������б���ͼ��
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
        /// ��ȡ�������б���ͼ���Ƿ���ʾ���ұ�
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
        /// ��ȡ��״̬
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
        /// ��ȡ��������״̬��������State�ı��¼�
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
        /// ��ȡ������Visible���ԣ�������Visible�ı��¼�
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
        /// ��ȡ������Sortable���ԣ�������Sortable�ı��¼�
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
        /// ��ȡ�����ù�����ICellRenderer���󣬲�����CellRenderer�ı��¼�
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
        /// ��ȡ�����ù�����ICellEditor���󣬲�����Editor�ı��¼�
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
        /// ��ȡ�����ñȽ�����������Comparer�ı��¼�
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
        /// ��ȡĬ�ϱȽ���
		/// Gets the Type of the default Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		[Browsable(false)]
		public abstract Type DefaultComparerType
		{
			get;
		}


		/// <summary>
        /// ��ȡ����ʽ
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
        /// ��ȡ����������ʽ��������SortOrder�ı��¼�
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
        /// ��ȡ������Editable���ԣ�������Editable�ı��¼�
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
        /// ��ȡ�Ƿ���Ҫ���л�Editable����
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
        /// ��ȡ������Enabled���ԣ�������Enabled�ı��¼�
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
        /// ��ȡ�Ƿ���Ҫ���л�Enabled����
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
        /// ��ȡ������Selectable���ԣ�������Selectable�ı��¼�
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
        /// ��ȡ������ToolTipText���ԣ�������ToolTipText�ı��¼�
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
        /// ��ȡ��������ʼλ��
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
        /// ��ȡColumn.Left
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
        /// ��ȡColumn.Right
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
        /// ��ȡ������ColumnModel����
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
        /// ��ȡParent����ColumnModel
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
        /// ��ȡ�Ƿ�������¼�
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
        /// ���ݻ�����ȫ����Ҫ�Ŀ��
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
        /// �����Ըı��¼�
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
