using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// ��Ԫ����ʽ������
    /// </summary>
    [Serializable]
    public class I3ReportCellStyle
    {
        private string name;
        /// <summary>
        /// ��ʽ����
        /// </summary>
        [JsonProperty(PropertyName = "n")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int fontSize;
        /// <summary>
        /// �����С
        /// </summary>
        [JsonProperty(PropertyName = "fs")]
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        private string fontName;
        /// <summary>
        /// ��������
        /// </summary>
        [JsonProperty(PropertyName = "fn")]
        public string FontName
        {
            get { return fontName; }
            set { fontName = value; }
        }

        private Color fontColor = Color.Black;
        /// <summary>
        /// ������ɫ
        /// </summary>
        [JsonProperty(PropertyName = "fc")]
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        private FontStyle fontStyle;
        /// <summary>
        /// ������ʽ
        /// </summary>
        [JsonProperty(PropertyName = "fsty")]
        public FontStyle FontStyle
        {
            get { return fontStyle; }
            set { fontStyle = value; }
        }


        private StringAlignment alignment = StringAlignment.Near;
        [JsonProperty(PropertyName = "al")]
        public StringAlignment Alignment
        {
            get
            {
                return alignment;
            }
            set
            {
                alignment = value;
            }
        }

        private StringAlignment lineAlignment = StringAlignment.Center;
        [JsonProperty(PropertyName = "lal")]
        public StringAlignment LineAlignment
        {
            get
            {
                return lineAlignment;
            }
            set
            {
                lineAlignment = value;
            }
        }

        private bool wordWrap = false;
        [JsonProperty(PropertyName = "ww")]
        public bool WordWrap
        {
            get
            {
                return wordWrap;
            }
            set
            {
                wordWrap = value;
            }
        }

        private I3BorderInfo leftBorder = null;
        [JsonProperty(PropertyName = "lb")]
        public I3BorderInfo LeftBorder
        {
            get
            {
                return leftBorder;
            }
            set
            {
                leftBorder = value;
            }
        }

        private I3BorderInfo topBorder = null;
        [JsonProperty(PropertyName = "tb")]
        public I3BorderInfo TopBorder
        {
            get
            {
                return topBorder;
            }
            set
            {
                topBorder = value;
            }
        }

        private I3BorderInfo rightBorder = null;
        [JsonProperty(PropertyName = "rb")]
        public I3BorderInfo RightBorder
        {
            get
            {
                return rightBorder;
            }
            set
            {
                rightBorder = value;
            }
        }

        private I3BorderInfo bottomBorder = null;
        [JsonProperty(PropertyName = "bb")]
        public I3BorderInfo BottomBorder
        {
            get
            {
                return bottomBorder;
            }
            set
            {
                bottomBorder = value;
            }
        }

        private Color backColor = Color.White;
        /// <summary>
        /// ��Ԫ�񱳾�
        /// </summary>
        [JsonProperty(PropertyName = "bc")]
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        private I3AdjustSize adjustSize = I3AdjustSize.������;
        /// <summary>
        /// �ı�������Ԫ��ʱ������ʽ
        /// </summary>
        [JsonProperty(PropertyName = "as")]
        public I3AdjustSize AdjustSize
        {
            get
            {
                return adjustSize;
            }
            set
            {
                adjustSize = value;
            }
        }

        [NonSerialized]
        private bool pageBreakRow = false;
        /// <summary>
        /// �к��ҳ  ת����������ʱʹ�ã������л�
        /// </summary>
        [JsonProperty(PropertyName = "pbr")]
        public bool PageBreakRow
        {
            get
            {
                return pageBreakRow;
            }
            set
            {
                pageBreakRow = value;
            }
        }

        [NonSerialized]
        private bool pageBreakCol = false;
        /// <summary>
        /// �к��ҳ  ת����������ʱʹ�ã������л�
        /// </summary>
        [JsonProperty(PropertyName = "pbc")]
        public bool PageBreakCol
        {
            get
            {
                return pageBreakCol;
            }
            set
            {
                pageBreakCol = value;
            }
        }
        
        public I3ReportCellStyle()
        {
        }
        public I3ReportCellStyle(string sName)
        {
            this.name = sName;
        }
    }
}
