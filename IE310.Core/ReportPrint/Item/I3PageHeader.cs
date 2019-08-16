
using System.Drawing;

namespace IE310.Core.ReportPrint.Item
{
    public class I3PageHeader
    {
        public static I3PageHeader Default
        {
            get
            {
                I3PageHeader ph = new I3PageHeader();
                ph.Text = "PageHeader";
                ph.FontName = "宋体";
                ph.FontSize = 10;
                ph.FontStyle = FontStyle.Regular;
                ph.GraphicsUnit = GraphicsUnit.Pixel;

                StringFormat stringFormat = StringFormat.GenericDefault;
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
                stringFormat.Trimming = StringTrimming.None;
                stringFormat.FormatFlags = (StringFormatFlags)0;
                ph.StringFormat = stringFormat;

                ph.BrushColor = Color.Black;
                ph.MarginLeftMM = 2;
                ph.MarginTopMM = 2;
                ph.MarginRightMM = 2;

                return ph;
            }
        }

        public string Text { get; set; }
        public string FontName { get; set; }
        public float FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public GraphicsUnit GraphicsUnit { get; set; }
        public StringFormat StringFormat { get; set; }
        public Color BrushColor { get; set; }

        /// <summary>
        /// 单位：毫米
        /// </summary>
        public float MarginLeftMM { get; set; }

        /// <summary>
        /// 单位：毫米
        /// </summary>
        public float MarginTopMM { get; set; }
        /// <summary>
        /// 单位：毫米
        /// </summary>
        public float MarginBottomMM { get; set; }

        /// <summary>
        /// 单位：毫米
        /// </summary>
        public float MarginRightMM { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        private float marginLeft = float.MinValue;
        /// <summary>
        /// 单位：像素
        /// </summary>
        public float MarginLeft
        {
            get
            {
                if (marginLeft == float.MinValue)
                {
                    marginLeft = MarginLeftMM * (float)96 / (float)25.39999918;
                }
                return marginLeft;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        private float marginTop = float.MinValue;
        /// <summary>
        /// 单位：像素
        /// </summary>
        public float MarginTop
        {
            get
            {
                if (marginTop == float.MinValue)
                {
                    marginTop = MarginTopMM * (float)96 / (float)25.39999918;
                }
                return marginTop;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        private float marginBottom = float.MinValue;
        /// <summary>
        /// 单位：像素
        /// </summary>
        public float MarginBottom
        {
            get
            {
                if (marginBottom == float.MinValue)
                {
                    marginBottom = MarginBottomMM * (float)96 / (float)25.39999918;
                }
                return marginBottom;
            }
        }


        [Newtonsoft.Json.JsonIgnore]
        private float marginRight = float.MinValue;
        /// <summary>
        /// 单位：像素
        /// </summary>
        public float MarginRight
        {
            get
            {
                if (marginRight == float.MinValue)
                {
                    marginRight = MarginRightMM * (float)96 / (float)25.39999918;
                }
                return marginRight;
            }
        }
    }
}
