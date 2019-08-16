

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Design;
using IE310.Table.Renderers;
using IE310.Table.Models;
using IE310.Table.Row;
using IE310.Table.Column;


namespace IE310.Table.Cell
{
    /// <summary>
    /// Represents a Cell that is displayed in a Table
    /// </summary>
    [DesignTimeVisible(true),
    TypeConverter(typeof(I3CellConverter))]
    public class I3Cell : IDisposable
    {
        #region EventHandlers

        /// <summary>
        /// 属性改变事件
        /// Occurs when the value of a Cells property changes
        /// </summary>
        public event I3CellEventHandler PropertyChanged;

        #endregion


        #region Class Data

        //Cell 状态标志
        // Cell state flags
        //可编辑标志
        private static readonly int STATE_EDITABLE = 1;
        //Enabled标志
        private static readonly int STATE_ENABLED = 2;
        //选中标志
        private static readonly int STATE_SELECTED = 4;

        /// <summary>
        /// Cell中显示的文本
        /// The text displayed in the Cell
        /// </summary>
        //private string text;


        /// <summary>
        /// 关联的数据
        /// An object that contains data to be displayed in the Cell
        /// </summary>
        private object data;

        /// <summary>
        /// tag
        /// An object that contains data about the Cell
        /// </summary>
        private object tag;

        private object userData;

        /// <summary>
        /// 用于记录Cell的当前状态
        /// Stores information used by CellRenderers to record the current 
        /// state of the Cell
        /// </summary>
        private object rendererData;

        /// <summary>
        /// 行号
        /// The Row that the Cell belongs to
        /// </summary>
        private I3Row row;

        /// <summary>
        /// 在行中的序号，其实就是列号
        /// The index of the Cell
        /// </summary>
        private int index;

        /// <summary>
        /// 记录了可编辑标志、Enabled标志、选中标志的值
        /// Contains the current state of the the Cell
        /// </summary>
        private byte state;

        /// <summary>
        /// Cell外观设置
        /// The Cells CellStyle settings
        /// </summary>
        private I3CellStyle cellStyle;

        /// <summary>
        /// Cell的Check状态
        /// The Cells CellCheckStyle settings
        /// </summary>
        private I3CellCheckStyle checkStyle;

        /// <summary>
        /// Cell的图像属性
        /// The Cells CellImageStyle settings
        /// </summary>
        private I3CellImageStyle imageStyle;

        /// <summary>
        /// 说明文本
        /// The text displayed in the Cells tooltip
        /// </summary>
        private string tooltipText;

        /// <summary>
        /// 标志Cell对象是否已经被释放
        /// Specifies whether the Cell has been disposed
        /// </summary>
        private bool disposed = false;

        private float needWidth = 0;
        private float needHeight = 0;

        #endregion


        #region Constructor

        /// <summary>
        /// 空构造函数
        /// Initializes a new instance of the Cell class with default settings
        /// </summary>
        public I3Cell()
            : base()
        {
            this.Init();
        }


        /// <summary>
        /// 根据文本构造Cell
        /// Initializes a new instance of the Cell class with the specified text
        /// </summary>
        /// <param name="text">The text displayed in the Cell</param>
        //public I3Cell(string text)
        //{	
        //    this.Init();

        //    this.text = text;
        //}


        /// <summary>
        /// 根据关联的数据（data对象）构造Cell
        /// Initializes a new instance of the Cell class with the specified object
        /// </summary>
        /// <param name="value">The object displayed in the Cell</param>
        public I3Cell(object data)
        {
            this.Init();

            this.data = data;
        }


        /// <summary>
        /// 根据文本、关联的数据（data对象）构造Cell
        /// Initializes a new instance of the Cell class with the specified text 
        /// and object
        /// </summary>
        /// <param name="text">The text displayed in the Cell</param>
        /// <param name="value">The object displayed in the Cell</param>
        public I3Cell(object data, object value)
        {
            this.Init();

            this.data = data;
            this.data = value;
        }


        /// <summary>
        /// 根据文本、check构造Cell
        /// Initializes a new instance of the Cell class with the specified text 
        /// and check value
        /// </summary>
        /// <param name="text">The text displayed in the Cell</param>
        /// <param name="check">Specifies whether the Cell is Checked</param>
        public I3Cell(object data, bool check)
        {
            this.Init();

            this.data = data;
            this.Checked = check;
        }


        /// <summary>
        /// 根据Cell、Image构造Cell
        /// Initializes a new instance of the Cell class with the specified text 
        /// and Image value
        /// </summary>
        /// <param name="text">The text displayed in the Cell</param>
        /// <param name="image">The Image displayed in the Cell</param>
        public I3Cell(object data, Image image)
        {
            this.Init();

            this.data = data;
            this.Image = image;
        }


        /// <summary>
        /// 根据文本、前景色、背景色、字体构造Cell
        /// Initializes a new instance of the Cell class with the specified text, 
        /// fore Color, back Color and Font
        /// </summary>
        /// <param name="text">The text displayed in the Cell</param>
        /// <param name="foreColor">The foreground Color of the Cell</param>
        /// <param name="backColor">The background Color of the Cell</param>
        /// <param name="font">The Font used to draw the text in the Cell</param>
        public I3Cell(object data, Color foreColor, Color backColor, Font font)
        {
            this.Init();

            this.data = data;
            this.ForeColor = foreColor;
            this.BackColor = backColor;
            this.Font = font;
        }


        /// <summary>
        /// 根据文本、CellStyle构造Cell
        /// Initializes a new instance of the Cell class with the specified text 
        /// and CellStyle
        /// </summary>
        /// <param name="text">The text displayed in the Cell</param>
        /// <param name="cellStyle">A CellStyle that specifies the visual appearance 
        /// of the Cell</param>
        public I3Cell(object data, I3CellStyle cellStyle)
        {
            this.Init();

            this.data = data;
            this.cellStyle = cellStyle;
        }



        /// <summary>
        /// 对属性赋初始值
        /// Initialise default values
        /// </summary>
        private void Init()
        {
            this.data = null;
            this.rendererData = null;
            this.tag = null;
            this.userData = null;
            this.row = null;
            this.index = -1;
            this.cellStyle = null;
            this.checkStyle = null;
            this.imageStyle = null;
            this.tooltipText = null;

            this.state = (byte)(STATE_EDITABLE | STATE_ENABLED);
        }

        #endregion


        #region Methods

        /// <summary>
        /// 释放
        /// Releases all resources used by the Cell
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.data = null;
                this.tag = null;
                this.rendererData = null;

                if (this.row != null)
                {
                    this.row.Cells.Remove(this);
                }

                this.row = null;
                this.index = -1;
                this.cellStyle = null;
                this.checkStyle = null;
                this.imageStyle = null;
                this.tooltipText = null;

                this.state = (byte)0;

                this.disposed = true;
            }
        }


        /// <summary>
        /// 获取 可编辑标志、Enabled标志、选中标志的值 
        /// Returns the state represented by the specified state flag
        /// </summary>
        /// <param name="flag">A flag that represents the state to return</param>
        /// <returns>The state represented by the specified state flag</returns>
        internal bool GetState(int flag)
        {
            return ((this.state & flag) != 0);
        }


        /// <summary>
        /// 设置 可编辑标志、Enabled标志、选中标志的值 的值
        /// Sets the state represented by the specified state flag to the specified value
        /// </summary>
        /// <param name="flag">A flag that represents the state to be set</param>
        /// <param name="value">The new value of the state</param>
        internal void SetState(int flag, bool value)
        {
            this.state = (byte)(value ? (this.state | flag) : (this.state & ~flag));
        }

        #endregion


        #region Properties



        /// <summary>
        /// 获取或设置关联的数据
        /// Gets or sets the Cells non-text data
        /// </summary>
        //[Category("Appearance"),
        //DefaultValue(null),
        //Description("The non-text data displayed by the cell"),
        //TypeConverter(typeof(StringConverter))]
        [Browsable(false)]
        public object Data
        {
            get
            {
                return this.data;
            }

            set
            {

                if (this.data != value)
                {
                    object oldData = this.data;
                    this.data = value;

                    ////强制转换为Data的源类型，不能转换时会抛出异常
                    //if (oldData == null || value == null || oldData.GetType() == value.GetType())
                    //{
                    //    this.data = value;
                    //    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ValueChanged, oldData));
                    //    return;
                    //}
                    //if (oldData.GetType() == typeof(string))
                    //{
                    //    this.data = value.ToString();
                    //    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ValueChanged, oldData));
                    //    return;
                    //}

                    //if (oldData.GetType().IsClass)  //string也是一个class
                    //{
                    //    this.data = Activator.CreateInstance(oldData.GetType(), value);
                    //}
                    //else
                    //{
                    //    TypeCode typeCode = Convert.GetTypeCode(oldData);
                    //    if (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object)
                    //    {
                    //        this.data = value;
                    //    }
                    //    else
                    //    {
                    //        this.data = Convert.ChangeType(value, typeCode);
                    //    }
                    //}

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ValueChanged, oldData));
                }
            }
        }


        /// <summary>
        /// 获取或设置Tag
        /// Gets or sets the object that contains data about the Cell
        /// </summary>
        //[Category("Appearance"),
        //DefaultValue(null),
        //Description("User defined data associated with the cell"),
        //TypeConverter(typeof(StringConverter))]
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

        [Browsable(false)]
        public object UserData
        {
            get
            {
                return this.userData;
            }
            set
            {
                this.userData = value;
            }
        }


        /// <summary>
        /// 获取或设置CellStyle
        /// Gets or sets the CellStyle used by the Cell
        /// </summary>
        [Browsable(false),
        DefaultValue(null)]
        public I3CellStyle CellStyle
        {
            get
            {
                return this.cellStyle;
            }

            set
            {
                if (this.cellStyle != value)
                {
                    I3CellStyle oldStyle = this.CellStyle;

                    this.cellStyle = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.StyleChanged, oldStyle));
                }
            }
        }


        /// <summary>
        /// 获取Cell是否被选择
        /// Gets or sets whether the Cell is selected
        /// </summary>
        [Browsable(false)]
        public bool Selected
        {
            get
            {
                return this.GetState(STATE_SELECTED);
            }
        }


        /// <summary>
        /// 设置Cell当前为选择状态
        /// Sets whether the Cell is selected
        /// </summary>
        /// <param name="selected">A boolean value that specifies whether the 
        /// cell is selected</param>
        internal void SetSelected(bool selected)
        {
            this.SetState(STATE_SELECTED, selected);
        }


        /// <summary>
        /// 获取或设置背景色
        /// Gets or sets the background Color for the Cell
        /// </summary>
        [Category("Appearance"),
        Description("The background color used to display text and graphics in the cell")]
        public Color BackColor
        {
            get
            {
                if (this.CellStyle == null)
                {
                    if (this.Row != null)
                    {
                        return this.Row.BackColor;
                    }

                    return Color.Transparent;
                }

                return this.CellStyle.BackColor;
            }

            set
            {
                if (this.CellStyle == null)
                {
                    this.CellStyle = new I3CellStyle();
                }

                if (this.CellStyle.BackColor != value)
                {
                    Color oldBackColor = this.BackColor;

                    this.CellStyle.BackColor = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.BackColorChanged, oldBackColor));
                }
            }
        }


        /// <summary>
        /// 获取是否需要序列化背景色（是否使用了自己的背景色设置）
        /// Specifies whether the BackColor property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the BackColor property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeBackColor()
        {
            return (this.cellStyle != null && this.cellStyle.BackColor != Color.Empty);
        }


        /// <summary>
        /// 获取或设置前景色
        /// Gets or sets the foreground Color for the Cell
        /// </summary>
        [Category("Appearance"),
        Description("The foreground color used to display text and graphics in the cell")]
        public Color ForeColor
        {
            get
            {
                if (this.CellStyle == null)
                {
                    if (this.Row != null)
                    {
                        return this.Row.ForeColor;
                    }

                    return Color.Black;
                }
                else
                {
                    if (this.CellStyle.ForeColor == Color.Empty || this.CellStyle.ForeColor == Color.Transparent)
                    {
                        if (this.Row != null)
                        {
                            return this.Row.ForeColor;
                        }
                    }

                    return this.CellStyle.ForeColor;
                }
            }

            set
            {
                if (this.CellStyle == null)
                {
                    this.CellStyle = new I3CellStyle();
                }

                if (this.CellStyle.ForeColor != value)
                {
                    Color oldForeColor = this.ForeColor;

                    this.CellStyle.ForeColor = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ForeColorChanged, oldForeColor));
                }
            }
        }


        /// <summary>
        /// 获取是否需要序列化前景色（是否使用了自己的前景色设置）
        /// Specifies whether the ForeColor property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the ForeColor property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeForeColor()
        {
            return (this.cellStyle != null && this.cellStyle.ForeColor != Color.Empty);
        }


        /// <summary>
        /// 获取或设置字体
        /// Gets or sets the Font used by the Cell
        /// </summary>
        [Category("Appearance"),
        Description("The font used to display text in the cell")]
        public Font Font
        {
            get
            {
                if (this.CellStyle == null)
                {
                    if (this.Row != null)
                    {
                        return this.Row.Font;
                    }

                    return null;
                }
                else
                {
                    if (this.CellStyle.Font == null)
                    {
                        if (this.Row != null)
                        {
                            return this.Row.Font;
                        }
                    }

                    return this.CellStyle.Font;
                }
            }

            set
            {
                if (this.CellStyle == null)
                {
                    this.CellStyle = new I3CellStyle();
                }

                if (this.CellStyle.Font != value)
                {
                    Font oldFont = this.Font;

                    this.CellStyle.Font = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.FontChanged, oldFont));
                }
            }
        }


        /// <summary>
        /// 获取是否需要序列化字体 （是否使用了自己的字体设置）
        /// Specifies whether the Font property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Font property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeFont()
        {
            return (this.cellStyle != null && this.cellStyle.Font != null);
        }


        /// <summary>
        /// 获取或设置边距
        /// Gets or sets the amount of space between the Cells Border and its contents
        /// </summary>
        [Category("Appearance"),
        Description("The amount of space between the cells border and its contents")]
        public I3CellPadding Padding
        {
            get
            {
                if (this.CellStyle == null)
                {
                    return I3CellPadding.Empty;
                }

                return this.CellStyle.Padding;
            }

            set
            {
                if (this.CellStyle == null)
                {
                    this.CellStyle = new I3CellStyle();
                }

                if (this.CellStyle.Padding != value)
                {
                    I3CellPadding oldPadding = this.Padding;

                    this.CellStyle.Padding = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.PaddingChanged, oldPadding));
                }
            }
        }


        /// <summary>
        /// 获取是否需要序列化边距
        /// Specifies whether the Padding property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Padding property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializePadding()
        {
            return this.Padding != I3CellPadding.Empty;
        }


        /// <summary>
        /// 获取或设置Checked
        /// Gets or sets whether the Cell is in the checked state
        /// </summary>
        [Category("Appearance"),
        DefaultValue(false),
        Description("Indicates whether the cell is checked or unchecked"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        RefreshProperties(RefreshProperties.Repaint)]
        public bool Checked
        {
            get
            {
                if (this.checkStyle == null)
                {
                    return false;
                }

                return this.checkStyle.Checked;
            }
            set
            {
                if (this.checkStyle == null)
                {
                    this.checkStyle = new I3CellCheckStyle();
                }

                if (this.checkStyle.Checked != value)
                {
                    bool oldCheck = this.Checked;

                    this.checkStyle.Checked = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.CheckStateChanged, oldCheck));
                }
            }
        }


        /// <summary>
        /// 获取或设置CheckState
        /// Gets or sets the state of the Cells check box
        /// </summary>
        [Category("Appearance"),
        DefaultValue(CheckState.Unchecked),
        Description("Indicates the state of the cells check box"),
        RefreshProperties(RefreshProperties.Repaint)]
        public CheckState CheckState
        {
            get
            {
                if (this.checkStyle == null)
                {
                    return CheckState.Unchecked;
                }

                return this.checkStyle.CheckState;
            }

            set
            {
                if (!Enum.IsDefined(typeof(CheckState), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
                }

                if (this.checkStyle == null)
                {
                    this.checkStyle = new I3CellCheckStyle();
                }

                if (this.checkStyle.CheckState != value)
                {
                    CheckState oldCheckState = this.CheckState;

                    this.checkStyle.CheckState = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.CheckStateChanged, oldCheckState));
                }
            }
        }


        /// <summary>
        /// 获取或设置是否支持不确定状态
        /// Gets or sets a value indicating whether the Cells check box 
        /// will allow three check states rather than two
        /// </summary>
        [Category("Appearance"),
        DefaultValue(false),
        Description("Controls whether or not the user can select the indeterminate state of the cells check box"),
        RefreshProperties(RefreshProperties.Repaint)]
        public bool ThreeState
        {
            get
            {
                if (this.checkStyle == null)
                {
                    return false;
                }

                return this.checkStyle.ThreeState;
            }

            set
            {
                if (this.checkStyle == null)
                {
                    this.checkStyle = new I3CellCheckStyle();
                }

                if (this.checkStyle.ThreeState != value)
                {
                    bool oldThreeState = this.ThreeState;

                    this.checkStyle.ThreeState = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ThreeStateChanged, oldThreeState));
                }
            }
        }


        /// <summary>
        /// 获取或设置图像
        /// Gets or sets the image that is displayed in the Cell
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The image that will be displayed in the cell")]
        public Image Image
        {
            get
            {
                if (this.imageStyle == null)
                {
                    return null;
                }

                return this.imageStyle.Image;
            }

            set
            {
                if (this.imageStyle == null)
                {
                    this.imageStyle = new I3CellImageStyle();
                }

                if (this.imageStyle.Image != value)
                {
                    Image oldImage = this.Image;

                    this.imageStyle.Image = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ImageChanged, oldImage));
                }
            }
        }


        /// <summary>
        /// 获取或设置图像显示模式
        /// Gets or sets how the Cells image is sized within the Cell
        /// </summary>
        [Category("Appearance"),
        DefaultValue(I3ImageSizeMode.Normal),
        Description("Controls how the image is sized within the cell")]
        public I3ImageSizeMode ImageSizeMode
        {
            get
            {
                if (this.imageStyle == null)
                {
                    return I3ImageSizeMode.Normal;
                }

                return this.imageStyle.ImageSizeMode;
            }

            set
            {
                if (this.imageStyle == null)
                {
                    this.imageStyle = new I3CellImageStyle();
                }

                if (this.imageStyle.ImageSizeMode != value)
                {
                    I3ImageSizeMode oldSizeMode = this.ImageSizeMode;

                    this.imageStyle.ImageSizeMode = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ImageSizeModeChanged, oldSizeMode));
                }
            }
        }


        /// <summary>
        /// 获取或设置是否可编辑
        /// Gets or sets a value indicating whether the Cells contents are able 
        /// to be edited
        /// </summary>
        [Category("Appearance"),
        Description("Controls whether the cells contents are able to be changed by the user")]
        public bool Editable
        {
            get
            {
                if (!this.GetState(STATE_EDITABLE))
                {
                    return false;
                }

                if (this.Row == null)
                {
                    return this.Enabled;
                }

                return this.Enabled && this.Row.Editable;
            }

            set
            {
                bool editable = this.Editable;

                this.SetState(STATE_EDITABLE, value);

                if (editable != value)
                {
                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.EditableChanged, editable));
                }
            }
        }


        /// <summary>
        /// 获取是否需要序列可编辑属性
        /// Specifies whether the Editable property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Editable property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeEditable()
        {
            return !this.GetState(STATE_EDITABLE);
        }


        /// <summary>
        /// 获取或设置Enabled
        /// Gets or sets a value indicating whether the Cell 
        /// can respond to user interaction
        /// </summary>
        [Category("Appearance"),
        Description("Indicates whether the cell is enabled")]
        public bool Enabled
        {
            get
            {
                if (!this.GetState(STATE_ENABLED))
                {
                    return false;
                }

                if (this.Row == null)
                {
                    return true;
                }

                return this.Row.Enabled;
            }

            set
            {
                bool enabled = this.Enabled;

                this.SetState(STATE_ENABLED, value);

                if (enabled != value)
                {
                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.EnabledChanged, enabled));
                }
            }
        }


        /// <summary>
        /// 获取是否需要序列化Enabled
        /// Specifies whether the Enabled property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Enabled property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeEnabled()
        {
            return !this.GetState(STATE_ENABLED);
        }


        /// <summary>
        /// 获取或设置提示文本
        /// Gets or sets the text displayed in the Cells tooltip
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The text displayed in the cells tooltip")]
        public string ToolTipText
        {
            get
            {
                return this.tooltipText;
            }

            set
            {
                if (this.tooltipText != value)
                {
                    string oldToolTip = this.tooltipText;

                    this.tooltipText = value;

                    this.OnPropertyChanged(new I3CellEventArgs(this, I3CellEventType.ToolTipTextChanged, oldToolTip));
                }
            }
        }


        /// <summary>
        /// 获取或设置RendererData
        /// Gets or sets the information used by CellRenderers to record the current 
        /// state of the Cell
        /// </summary>
        protected internal object RendererData
        {
            get
            {
                return this.rendererData;
            }

            set
            {
                this.rendererData = value;
            }
        }


        /// <summary>
        /// 获取行号
        /// Gets the Row that the Cell belongs to
        /// </summary>
        [Browsable(false)]
        public I3Row Row
        {
            get
            {
                return this.row;
            }
        }


        /// <summary>
        /// 获取或设置行号？与属性Row的关系？
        /// Gets or sets the Row that the Cell belongs to
        /// </summary>
        internal I3Row InternalRow
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
        /// 获取序号
        /// Gets the index of the Cell within its Row
        /// </summary>
        [Browsable(false)]
        public int Index
        {
            get
            {
                return this.index;
            }
        }


        /// <summary>
        /// 获取或设置序号？与属性Index的关系？
        /// Gets or sets the index of the Cell within its Row
        /// </summary>
        internal int InternalIndex
        {
            get
            {
                return this.index;
            }

            set
            {
                this.index = value;
            }
        }


        /// <summary>
        /// 获取或设置是否可以引发事件
        /// Gets whether the Cell is able to raise events
        /// </summary>
        protected internal bool CanRaiseEvents
        {
            get
            {
                // check if the Row that the Cell belongs to is able to 
                // raise events (if it can't, the Cell shouldn't raise 
                // events either)
                if (this.Row != null)
                {
                    return this.Row.CanRaiseEvents;
                }

                return true;
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
                ////else if (this.row != null && this.row.TableModel != null && this.row.TableModel.Table != null && this.row.TableModel.Table.ColumnModel != null)
                ////{
                ////    I3ColumnModel cm = this.row.TableModel.Table.ColumnModel;
                ////    result = cm.Columns[this.index].Width;
                ////}
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

        /// <summary>
        /// 内容绘制完全所需要的高度
        /// </summary>
        [Browsable(false)]
        public float NeedHeight
        {
            get
            {
                //float result = 0;
                //if (needHeight > 0)
                //{
                //    result = needHeight;
                //}
                ////else if (this.row != null)
                ////{
                ////    result = this.row.Height;
                ////}
                //else
                //{
                //    result = I3TableModel.DefaultRowHeight_Const;
                //}

                float result = needHeight;
                if (result < I3TableModel.MinRowHeight_Const)
                {
                    result = I3TableModel.MinRowHeight_Const;
                }
                if (result > I3TableModel.MaxAutoRowHeight_Const)  
                {
                    result = I3TableModel.MaxAutoRowHeight_Const;
                }

                return result;
            }
            set
            {
                needHeight = value;
            }
        }


        #endregion


        #region Events

        /// <summary>
        /// 引发属性改变事件
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        protected virtual void OnPropertyChanged(I3CellEventArgs e)
        {
            e.SetColumn(this.Index);

            if (this.Row != null)
            {
                e.SetRow(this.Row.Index);
            }

            if (this.CanRaiseEvents)
            {
                if (this.Row != null)
                {
                    this.Row.OnCellPropertyChanged(e);
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
