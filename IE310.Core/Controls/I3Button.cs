/*                     ieButton.ieButton
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 可以自绘画，有很多风格的按钮
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                2.设置下列属性:
 *                  FrontColor:设定按钮渐变的前景色
 *                  BackGroundColor:设定按钮渐变的背景色
 *                  UseFloat:设定是否人工设定角度
 *                  UseStyle:设定是否使用图案填充文本
 *                  Angle:定义渐变方向的角度，以度为单位从 X 轴顺时针测量
 *                  Mode:当UseFloat设为false时，设定渐变方向
 *                  FillStyle:设定文本要填充的图案
 * 
 *          附注: 从这里可以学习如何为控件定义属性
 * 
 *      修改记录: 1.
 *  
 *                                                  Created by ie
 *                                                            2008-01-01      
 * 
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IE310.Core.Controls
{
    public partial class I3Button : System.Windows.Forms.Button
    {
        private Color froColor; //渐变前景色

        private Color backColor;//渐变背景色

        private bool isUseFloat;//是否使用角度转变

        private float angle; //放置角度

        private LinearGradientMode mode;//设定渐变的角度

        private HatchStyle hatchStyle; //设定文本的填充图案

        private bool isUseStyle;//设定是否用图案填充图案 




        public I3Button()
        {
            InitializeComponent();
        }




        [Description("设定按钮渐变的前景色"), Category("Appearance")]

        public Color FrontColor
        {

            get
            {

                return froColor;

            }

            set
            {

                froColor = value;

            }

        }

        [Description("设定按钮渐变的背景色"), Category("Appearance")]

        public Color BackGroundColor
        {

            get
            {

                return backColor;

            }

            set
            {

                backColor = value;

            }

        }

        [DefaultValue(false), Description("设定是否人工设定角度")]

        public bool UseFloat
        {

            get
            {

                return isUseFloat;

            }

            set
            {

                isUseFloat = value;

            }

        }

        [DefaultValue(false), Description("设定是否使用图案填充文本")]

        public bool UseStyle
        {

            get
            {

                return isUseStyle;

            }

            set
            {

                isUseStyle = value;

            }

        }

        [DefaultValue(0), Description("定义渐变方向的角度，以度为单位从 X 轴顺时针测量。 "), Category("Appearance")]

        public float Angle
        {

            get
            {

                return angle;

            }

            set
            {

                angle = value;

            }

        }

        [DefaultValue(0), Description("当UseFloat设为false时，设定渐变方向。 "), Category("Appearance")]

        public LinearGradientMode Mode
        {

            get
            {

                return mode;

            }

            set
            {

                mode = value;

            }

        }

        [DefaultValue(false), Description("设定文本要填充的图案"), Category("Appearance")]

        public HatchStyle FillStyle
        {

            get
            {

                return hatchStyle;

            }

            set
            {

                hatchStyle = value;

            }

        }




        //使用角度的方法渐近重画Button

        private void DrawButtonWithAngle(Graphics dbg)
        {

            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), froColor, backColor, angle);

            dbg.FillRectangle(brush, 0, 0, this.Width, this.Height);

            brush.Dispose();

        }

        ////使用模式的方法渐近重画Button

        private void DrawButtonWithMode(Graphics dbg, LinearGradientMode Mode)
        {

            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), froColor, backColor, Mode);

            dbg.FillRectangle(brush, 0, 0, this.Width, this.Height);

            brush.Dispose();

        }

        //重画Button的文本(Text),不使用图案填充

        private void DrawButtonText(Graphics dbg)
        {

            StringFormat format = new StringFormat();

            format.LineAlignment = StringAlignment.Center;

            format.Alignment = StringAlignment.Center;

            dbg.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Rectangle(0, 0, this.Width, this.Height), format);

        }

        //override DrawButtonText函数，使之可以用图案填充文本

        private void DrawButtonText(Graphics dbg, HatchStyle hs)
        {

            StringFormat format = new StringFormat();

            format.LineAlignment = StringAlignment.Center;

            format.Alignment = StringAlignment.Center;

            dbg.DrawString(this.Text, this.Font, new HatchBrush(hs, this.ForeColor, Color.Aquamarine), new Rectangle(0, 0, this.Width, this.Height), format);

        }






        protected override void OnPaint(PaintEventArgs pe)
        {



            Graphics g = pe.Graphics;

            base.OnPaint(pe); //调用父控件的方法

            if (isUseFloat == true) //假如使用角度控制渐变的角度

                DrawButtonWithAngle(g);

            if (isUseFloat == false)

                DrawButtonWithMode(g, mode);

            if (isUseStyle == true)//假如使用图案填充文字

                DrawButtonText(g, hatchStyle);

            else

                DrawButtonText(g);

        }
    }
}
