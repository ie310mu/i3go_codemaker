using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace IE310.Core.ReportPrint
{
    [Serializable]
    public class I3PageSetting
    {
        public static I3PageSetting Default
        {
            get
            {
                return new I3PageSetting();
            }
        }

        private PaperType paperType = PaperType.A4;
        /// <summary>
        /// 纸张类型
        /// </summary>
        [JsonProperty(PropertyName = "pt")]
        public PaperType PaperType
        {
            get
            {
                return paperType;
            }
            set
            {
                paperType = value;
            }
        }

        public int GetNPOIPaperType()
        {
            switch (PaperType)
            {
                case PaperType.Custom:
                    return 0;
                //case PaperType.A1:
                case PaperType.A2:
                    return 66;
                case PaperType.A3:
                    return 8;
                case PaperType.A4:
                    return 9;
                case PaperType.A5:
                    return 11;
                //case PaperType.B1:
                //case PaperType.B2:
                //case PaperType.B3:
                //case PaperType.B4:
                //case PaperType.B5:
                //case PaperType.A0:
                //case PaperType.B0:
                case PaperType.B4JIS:
                    return 12;
                case PaperType.B5JIS:
                    return 13;
                default:
                    return -1;
            }
        }

        private float customPaperWidthMM = 0;
        /// <summary>
        /// 自定义纸张宽度
        /// </summary>
        public float CustomPaperWidthMM
        {
            get
            {
                return customPaperWidthMM;
            }
            set
            {
                customPaperWidthMM = value;
            }
        }

        /// <summary>
        /// 自定义纸张宽度
        /// </summary>
        public float CustomPaperWidthPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(CustomPaperWidthMM);
            }
        }

        private float customPaperHeightMM = 0;
        /// <summary>
        /// 自定义纸张高度
        /// </summary>
        public float CustomPaperHeightMM
        {
            get
            {
                return customPaperHeightMM;
            }
            set
            {
                customPaperHeightMM = value;
            }
        }

        /// <summary>
        /// 自定义纸张高度
        /// </summary>
        public float CustomPaperHeightPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(CustomPaperHeightMM);
            }
        }

        private PaperOrientation paperOrientation = PaperOrientation.纵向;
        /// <summary>
        /// 纸张方向
        /// </summary>
        public PaperOrientation PaperOrientation
        {
            get
            {
                return paperOrientation;
            }
            set
            {
                paperOrientation = value;
            }
        }

        private float paperLeftMarginMM = 20;
        /// <summary>
        /// 左边距
        /// </summary>
        public float PaperLeftMarginMM
        {
            get
            {
                return paperLeftMarginMM;
            }
            set
            {
                paperLeftMarginMM = value;
            }
        }

        /// <summary>
        /// 左边距
        /// </summary>
        public float PaperLeftMarginPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(PaperLeftMarginMM);
            }
        }


        private float paperRightMarginMM = 20;
        /// <summary>
        /// 右边距
        /// </summary>
        public float PaperRightMarginMM
        {
            get
            {
                return paperRightMarginMM;
            }
            set
            {
                paperRightMarginMM = value;
            }
        }

        /// <summary>
        /// 右边距
        /// </summary>
        public float PaperRightMarginPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(PaperRightMarginMM);
            }
        }

        private float paperTopMarginMM = 20;
        /// <summary>
        /// 上边距
        /// </summary>
        public float PaperTopMarginMM
        {
            get
            {
                return paperTopMarginMM;
            }
            set
            {
                paperTopMarginMM = value;
            }
        }

        /// <summary>
        /// 上边距
        /// </summary>
        public float PaperTopMarginPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(PaperTopMarginMM);
            }
        }

        private float paperBottomMarginMM = 20;
        /// <summary>
        /// 下边距
        /// </summary>
        public float PaperBottomMarginMM
        {
            get
            {
                return paperBottomMarginMM;
            }
            set
            {
                paperBottomMarginMM = value;
            }
        }

        /// <summary>
        /// 下边距
        /// </summary>
        public float PaperBottomMarginPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(PaperBottomMarginMM);
            }
        }

        private float paperHeightAdjustPX = 0;
        /// <summary>
        /// 高度适应
        /// </summary>
        public float PaperHeightAdjustPX
        {
            get
            {
                return paperHeightAdjustPX;
            }
            set
            {
                paperHeightAdjustPX = value;
            }
        }

        private PagerStyle rowsPagerStyle = PagerStyle.按纸张尺寸分页;
        /// <summary>
        /// 行分页方式
        /// </summary>
        public PagerStyle RowsPagerStyle
        {
            get
            {
                return rowsPagerStyle;
            }
            set
            {
                rowsPagerStyle = value;
            }
        }

        private PagerStyle colsPagerStyle = PagerStyle.按纸张尺寸分页;
        /// <summary>
        /// 列分页方式
        /// </summary>
        public PagerStyle ColsPagerStyle
        {
            get
            {
                return colsPagerStyle;
            }
            set
            {
                colsPagerStyle = value;
            }
        }

        private int rowsPerPage = 10;
        /// <summary>
        /// 按数据行分页时每页的行数  (数据区的行数)
        /// </summary>
        public int RowsPerPage
        {
            get
            {
                if (rowsPerPage <= 0)
                {
                    return 1;
                }
                return rowsPerPage;
            }
            set
            {
                rowsPerPage = value;
            }
        }

        private int colsPerPage = 10;
        /// <summary>
        /// 按数据列分页时每页的列数  (数据区的列数)
        /// </summary>
        public int ColsPerPage
        {
            get
            {
                return colsPerPage;
            }
            set
            {
                colsPerPage = value;
            }
        }


        /// <summary>
        /// 纸张宽度 单位mm
        /// </summary>
        public float PaperWidthMM
        {
            get
            {
                PaperSizeF size = PaperSizeHelper.GetPaperSizeMM(this);
                return size.Width;
            }
        }

        /// <summary>
        /// 纸张高度 单位mm
        /// </summary>
        public float PaperHeightMM
        {
            get
            {
                PaperSizeF size = PaperSizeHelper.GetPaperSizeMM(this);
                return size.Height;
            }
        }

        /// <summary>
        /// 纸张宽度 单位px
        /// </summary>
        public float PaperWidthPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(PaperWidthMM);
            }
        }

        /// <summary>
        /// 纸张高度 单位px
        /// </summary>
        public float PaperHeightPX
        {
            get
            {
                return PaperSizeHelper.MM2PX(PaperHeightMM);
            }
        }

        /// <summary>
        /// 纸张上可绘制内容的区域，像素单位
        /// </summary>
        public RectangleF PaperContentRect
        {
            get
            {
                PaperSizeF size = PaperSizeHelper.GetPaperSizePX(this);

                //减去页边距
                size.Width -= PaperLeftMarginPX;
                size.Width -= PaperRightMarginPX;
                size.Height -= PaperTopMarginPX;
                size.Height -= PaperBottomMarginPX;

                //减去高度误差系数
                size.Height -= PaperHeightAdjustPX;

                RectangleF rect = new RectangleF(PaperLeftMarginPX, PaperTopMarginPX + PaperHeightAdjustPX / 2, size.Width, size.Height);

                return rect;
            }
        }


        /// <summary>
        /// 纸张的整个绘制区域，像素单位
        /// </summary>
        public RectangleF PaperRect
        {
            get
            {
                return new RectangleF(0, 0, PaperWidthPX, PaperHeightPX);
            }
        }
    }

    public enum PaperType
    {
        Custom = 13,
        A1 = 1,
        A2 = 2,
        A3 = 3,
        A4 = 4,
        A5 = 5,
        B1 = 6,
        B2 = 7,
        B3 = 8,
        B4 = 9,
        B5 = 10,
        A0 = 11,
        B0 = 12,
        B4JIS = 14,
        B5JIS = 15,
    }

    /// <summary>
    /// 纸张大小(像素单位)
    /// </summary>
    public class PaperSizeF
    {
        public PaperSizeF(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        private float width;
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }


        private float height;
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
    }

    public static class PaperSizeHelper
    {
        /// <summary>
        /// 计算纸张的大小  单位mm
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static PaperSizeF GetPaperSizeMM(I3PageSetting setting)
        {
            PaperSizeF size = null;
            #region 根据纸张类型获取大小
            switch (setting.PaperType)
            {
                case PaperType.Custom:
                    size = new PaperSizeF(setting.CustomPaperWidthMM, setting.CustomPaperHeightMM);
                    break;
                case PaperType.A1:
                    size = new PaperSizeF(594, 841);
                    break;
                case PaperType.A2:
                    size = new PaperSizeF(420, 594);
                    break;
                case PaperType.A3:
                    size = new PaperSizeF(297, 420);
                    break;
                case PaperType.A4:
                    size = new PaperSizeF(210, 297);
                    break;
                case PaperType.A5:
                    size = new PaperSizeF(148, 210);
                    break;
                case PaperType.B1:
                    size = new PaperSizeF(707, 1000);
                    break;
                case PaperType.B2:
                    size = new PaperSizeF(500, 707);
                    break;
                case PaperType.B3:
                    size = new PaperSizeF(353, 500);
                    break;
                case PaperType.B4:
                    size = new PaperSizeF(250, 353);
                    break;
                case PaperType.B5:
                    size = new PaperSizeF(176, 250);
                    break;
                case PaperType.A0:
                    size = new PaperSizeF(841, 1189);
                    break;
                case PaperType.B0:
                    size = new PaperSizeF(1000, 1414);
                    break;
                case PaperType.B4JIS:
                    size = new PaperSizeF(257, 364);
                    break;
                case PaperType.B5JIS:
                    size = new PaperSizeF(182, 257);
                    break;
                default:  //默认A4
                    size = new PaperSizeF(210, 297);
                    break;
            }
            #endregion

            //横向调换
            if (setting.PaperOrientation == PaperOrientation.横向)
            {
                float tmp = size.Height;
                size.Height = size.Width;
                size.Width = tmp;
            }

            return size;
        }

        /// <summary>
        /// 计算纸张的大小  单位像素
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static PaperSizeF GetPaperSizePX(I3PageSetting setting)
        {
            PaperSizeF size = GetPaperSizeMM(setting);
            size.Width = PaperSizeHelper.MM2PX(size.Width);
            size.Height = PaperSizeHelper.MM2PX(size.Height);
            return size;
        }

        /// <summary>
        /// mm转换为像素
        /// </summary>
        /// <returns></returns>
        public static float MM2PX(float value)
        {
            return value * 96F / 25.39999918F;
        }

        /// <summary>
        /// 像素转换为mm
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float PX2MM(float value)
        {
            return value * 25.39999918F / 96F;
        }
    }

    public enum PaperOrientation
    {
        横向 = 0,
        纵向 = 1,
    }

    /// <summary>
    /// 分布方式
    /// </summary>
    public enum PagerStyle
    {
        按纸张尺寸分页 = 1,
        按数据行列数分页 = 2,
    }
}
