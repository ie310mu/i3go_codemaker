

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
    /// ����ʱ�����͵���
    /// Represents a Column whose Cells are displayed as a DateTime
    /// </summary>
    [DesignTimeVisible(false),
    ToolboxItem(false)]
    public class I3DateTimeColumn : I3DropDownColumn
    {
        #region Class Data

        /// <summary>
        /// ���򻯵ĳ����ڸ�ʽ�ַ���
        /// Default long date format
        /// </summary>
        public static readonly string LongDateFormat = DateTimeFormatInfo.CurrentInfo.LongDatePattern;

        /// <summary>
        /// ���򻯵Ķ����ڸ�ʽ�ַ���
        /// Default short date format
        /// </summary>
        public static readonly string ShortDateFormat = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

        /// <summary>
        /// ���򻯵ĳ�ʱ���ʽ�ַ���
        /// Default time format
        /// </summary>
        public static readonly string LongTimeFormat = DateTimeFormatInfo.CurrentInfo.LongTimePattern;

        /// <summary>
        /// ���򻯵Ķ�ʱ���ʽ�ַ���
        /// </summary>
        public static readonly string ShortTimeFormat = DateTimeFormatInfo.CurrentInfo.ShortTimePattern;

        /// <summary>
        /// ʹ�����ָ�ʽ���ַ���
        /// The format of the date and time displayed in the Cells
        /// </summary>
        private I3DateTimeColumnType dateTimeColumnType;

        /// <summary>
        /// �Զ���ĸ�ʽ���ַ���
        /// The custom date/time format string
        /// </summary>
        //private string customFormat;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new DateTimeColumn with default values
        /// </summary>
        public I3DateTimeColumn()
            : base()
        {
            this.Init();
        }


        /// <summary>
        /// Creates a new DateTimeColumn with the specified header text
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        public I3DateTimeColumn(string text)
            : base(text)
        {
            this.Init();
        }


        /// <summary>
        /// Creates a new DateTimeColumn with the specified header text and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        public I3DateTimeColumn(string text, int width)
            : base(text, width)
        {
            this.Init();
        }


        /// <summary>
        /// Creates a new DateTimeColumn with the specified header text, width and visibility
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        /// <param name="visible">Specifies whether the column is visible</param>
        public I3DateTimeColumn(string text, int width, bool visible)
            : base(text, width, visible)
        {
            this.Init();
        }


        /// <summary>
        /// Creates a new DateTimeColumn with the specified header text and image
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        public I3DateTimeColumn(string text, Image image)
            : base(text, image)
        {
            this.Init();
        }


        /// <summary>
        /// Creates a new DateTimeColumn with the specified header text, image and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        /// <param name="width">The column's width</param>
        public I3DateTimeColumn(string text, Image image, int width)
            : base(text, image, width)
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
        public I3DateTimeColumn(string text, Image image, int width, bool visible)
            : base(text, image, width, visible)
        {
            this.Init();
        }


        /// <summary>
        /// Initializes the DateTimeColumn with default values
        /// </summary>
        internal void Init()
        {
            this.dateTimeColumnType = I3DateTimeColumnType.Date;
            this.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
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
            return "DATETIME";
        }


        /// <summary>
        /// ����Ĭ�ϵ�Renderer
        /// Gets the Column's default CellRenderer
        /// </summary>
        /// <returns>The Column's default CellRenderer</returns>
        public override II3CellRenderer CreateDefaultRenderer()
        {
            return new I3DateTimeCellRenderer();
        }


        /// <summary>
        /// ��ȡĬ�ϵ�EditorName
        /// Gets a string that specifies the name of the Column's default CellEditor
        /// </summary>
        /// <returns>A string that specifies the name of the Column's default 
        /// CellEditor</returns>
        public override string GetDefaultEditorName()
        {
            return "DATETIME";
        }


        /// <summary>
        /// ����Ĭ�ϵ�Editor
        /// Gets the Column's default CellEditor
        /// </summary>
        /// <returns>The Column's default CellEditor</returns>
        public override II3CellEditor CreateDefaultEditor()
        {
            return new I3DateTimeCellEditor();
        }


        public override string DataToString(object data)
        {
            if (data == null)
            {
                return "";
            }

            if (this.Dictionary != null && this.Dictionary.Contains(data))
            {
                object value = this.Dictionary[data];
                if (value == null)
                {
                    return "";
                }
                return value.ToString();
            }

            if (data is DateTime)
            {
                return ((DateTime)data).ToString(this.Format);
            }

            return data.ToString();
        }

        #endregion


        #region Properties

        /// <summary>
        /// ��ȡ�����ø�ʽ��������������RendererChanged�¼�
        /// Gets or sets the format of the date and time displayed in the Column's Cells
        /// </summary>
        [Category("Appearance"),
        DefaultValue(DateTimePickerFormat.Long),
        Description("The format of the date and time displayed in the Column's Cells")]
        public I3DateTimeColumnType DateTimeColumnType
        {
            get
            {
                return this.dateTimeColumnType;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3DateTimeColumnType), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3DateTimeColumnType));
                }

                if (this.dateTimeColumnType != value)
                {
                    this.dateTimeColumnType = value;
                    if (this.dateTimeColumnType == I3DateTimeColumnType.Date)
                    {
                        this.Format = I3DateTimeColumn.ShortDateFormat;
                    }
                    else
                    {
                        this.Format = I3DateTimeColumn.ShortTimeFormat;
                    }

                    this.OnPropertyChanged(new I3ColumnEventArgs(this, I3ColumnEventType.RendererChanged, null));
                }
            }
        }


        /// <summary>
        /// ��ȡ�������Զ����ʽ���ַ�����������RendererChanged�¼�
        /// Gets or sets the custom date/time format string
        /// </summary>
        //[Category("Appearance"),
        //Description("The custom date/time format string")]
        //public string CustomDateTimeFormat
        //{
        //    get
        //    {
        //        return this.customFormat;
        //    }

        //    set
        //    {
        //        if (value == null)
        //        {
        //            throw new ArgumentNullException("CustomFormat cannot be null");
        //        }

        //        if (!this.customFormat.Equals(value))
        //        {
        //            this.customFormat = value;

        //            this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
        //        }

        //        DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
        //    }
        //}


        /// <summary>
        /// ��ʶ�Ƿ���Ҫ���л�CustomDateTimeFormat����
        /// Specifies whether the CustomDateTimeFormat property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the CustomDateTimeFormat property should be serialized, 
        /// false otherwise</returns>
        //private bool ShouldSerializeCustomDateTimeFormat()
        //{
        //    return !this.customFormat.Equals(DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.LongTimePattern);
        //}


        /// <summary>
        /// ��ȡ������Format����CustomDateTimeFormat
        /// Gets or sets the string that specifies how the Column's Cell contents 
        /// are formatted
        /// </summary>
        //[Browsable(false),
        //DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new string Format
        //{
        //    get
        //    {
        //        return this.CustomDateTimeFormat;
        //    }

        //    set
        //    {
        //        this.CustomDateTimeFormat = value;
        //    }
        //}


        /// <summary>
        /// ��ȡĬ�ϱȽ���
        /// Gets the Type of the Comparer used to compare the Column's Cells when 
        /// the Column is sorting
        /// </summary>
        public override Type DefaultComparerType
        {
            get
            {
                return typeof(I3DateTimeComparer);
            }
        }

        #endregion
    }

    public enum I3DateTimeColumnType
    {
        Date = 1,
        Time = 2,
    }
}
