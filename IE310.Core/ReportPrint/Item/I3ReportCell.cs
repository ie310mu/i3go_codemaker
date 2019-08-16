using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;


namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// ����Ԫ��
    /// </summary>
    [Serializable]
    [I3CellRenderer(typeof(I3ReportCellRenderer))]
    public class I3ReportCell
    {
        public bool Lock { get; set; }

        public string ExtandSettingData { get; set; }
         
        public I3ReportCell()
            : this(-1, -1)
        {
        }

        public I3ReportCell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }


        private int row;
        [JsonProperty(PropertyName = "r")]
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }

        private int col;
        [JsonProperty(PropertyName = "c")]
        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }

        private I3MergeState mergState = I3MergeState.None;
        /// <summary>
        /// �ϲ�״̬
        /// </summary>
        [JsonProperty(PropertyName = "ms")]
        public I3MergeState MergState
        {
            get
            {
                return mergState;
            }
            set
            {
                mergState = value;
            }
        }

        private string text = "";
        /// <summary>
        /// �ı�ֵ
        /// </summary>
        [JsonProperty(PropertyName = "t")]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        private string styleName = "";
        /// <summary>
        /// ��ʽ����
        /// </summary>
        [JsonProperty(PropertyName = "sn")]
        public string StyleName
        {
            get
            {
                return styleName;
            }
            set
            {
                styleName = value;
            }
        }

        [NonSerialized]
        private float calFontSize = -1F;
        /// <summary>
        /// ���ݳ�����Ԫ����С����ʱ������������С������Ҫ���л�
        /// </summary>
        [JsonProperty(PropertyName = "cfs")]
        public float CalFontSize
        {
            get
            {
                return calFontSize;
            }
            set
            {
                calFontSize = value;
            }
        }


        private byte[] imageData;
        /// <summary>
        /// ͼ�ζ���
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public byte[] ImageData
        {
            get
            {
                return imageData;
            }
            set
            {
                imageData = value;
            }
        }

        private int imageWidth = 0;
        [JsonProperty(PropertyName = "w")]
        public int Width
        {
            get
            {
                return imageWidth;
            }
            set
            {
                imageWidth = value;
            }
        }

        private int imageHeight = 0;
        [JsonProperty(PropertyName = "h")]
        public int Height
        {
            get
            {
                return imageHeight;
            }
            set
            {
                imageHeight = value;
            }
        }

        public bool HasCalFontSize { get; set; }

        [NonSerialized]
        private StringTrimming stringTrimming = StringTrimming.EllipsisCharacter;
        /// <summary>
        /// �������С���Ԫ���С�������StringTrimming������Ҫ���л�
        /// </summary>
        [JsonProperty(PropertyName = "st")]
        public StringTrimming StringTrimming
        {
            get
            {
                return stringTrimming;
            }
            set
            {
                stringTrimming = value;
            }
        }

        /// <summary>
        /// ���˵�Ԫ���λ����I3MergeRange��ʾ
        /// Ϊ�ϲ���Ԫ��ĵ�һ����Ԫ��ʱ�����������ϲ�����
        /// Ϊ�ϲ���Ԫ���������Ԫ��ʱ��ֻ���ص�Ԫ����
        /// </summary>
        public I3MergeRange GetRange_Mode2(I3ReportData reportData)
        {
            I3MergeRange range = this.MergState == I3MergeState.FirstCell ? reportData.GetMergeRange(row, col) : null;
            if (range == null)
            {
                range = new I3MergeRange((short)row, (short)col, (short)row, (short)col);
            }
            return range;
        }


        /// <summary>
        /// ���˵�Ԫ���λ����I3MergeRange��ʾ
        /// Ϊ�ϲ���Ԫ��ʱ�����غϲ�����
        /// </summary>
        public I3MergeRange GetRange_Mode1(I3ReportData reportData)
        {
            I3MergeRange range = null;
            switch (this.MergState)
            {
                case I3MergeState.Merged:
                    I3ReportCell startCell = reportData.GetMergedStartedCell(this.Row, this.Col);
                    range = reportData.GetMergeRange(startCell.Row, startCell.Col);
                    break;
                case I3MergeState.FirstCell:
                    range = reportData.GetMergeRange(this.Row, this.Col);
                    break;
                default:
                    range = new I3MergeRange((short)this.Row, (short)this.Col, (short)this.Row, (short)this.Col);
                    break;
            }

            return range;
        }
    }


}
