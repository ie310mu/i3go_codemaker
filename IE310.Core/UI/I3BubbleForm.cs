/*
使用方法:
                  1。从本窗口继承一个新的类
                  2。生成新窗口类的对象
                  3。调用Init(StepCount,AStepTime:integer;CloseByClick,CloseByTime:Boolean;CloseTime:integer)方法设置初始化参数
                     StepCount : 窗口经过几步之后完全显示
                     AStepTime : 每一步之间间隔的毫秒数
                     CloseByClick : 在窗口上点击时是否关闭窗口
                     CloseByTime  : 是否经过一定的时间关闭窗口
                     CloseTime    : 窗口完全显示后经过多少毫秒关闭窗口 ，需要CloseByTime=True
                    (注意，这一步可以省略，因为在Create事件中已经进行了初始化:Init(5,100,true,true,3000);)
                  4。调用Show或ShowModal显示窗口
                  5。如果没有自动关闭，子窗口可以打开关闭按钮，或者自行用代码控制
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using IE310.Core.Utils;

namespace IE310.Core.UI
{
    public partial class I3BubbleForm : Form
    {
        /// <summary>
        /// 每步增加的Top是多少
        /// </summary>
        private int FAStepHeight;
        /// <summary>
        /// 是否通过点击窗口来关闭
        /// </summary>
        private bool FCloseByClick;
        /// <summary>
        /// 是否通过时间来自动关闭
        /// </summary>
        private bool FCloseByTime;

        /// <summary>
        /// 记录最终的Top
        /// </summary>
        private int FTop;

        public I3BubbleForm()
        {
            InitializeComponent();

            if (this.DesignMode)
            {
                return;
            }

            Init(5, 100, true, true, 3000);
        }

        private void IEFS_BubbleForm_Load(object sender, EventArgs e)
        {
        }

        private void IEFS_BubbleForm_Shown(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            FTop = SystemInformation.WorkingArea.Height - this.Height - 2;

            this.Left = SystemInformation.WorkingArea.Width - this.Width - 2;
            this.Top = SystemInformation.WorkingArea.Height;

            tmHeight.Enabled = true;
        }


        public void Init(int StepCount, int AStepTime, bool CloseByClick, bool CloseByTime, int CloseTime)
        {
            if (this.DesignMode)
            {
                return;
            }

            FAStepHeight = (int)I3MathUtil.Round(this.Height / StepCount, 0);
            tmHeight.Interval = AStepTime;
            FCloseByClick = CloseByClick;
            FCloseByTime = CloseByTime;
            tmClose.Interval = CloseTime;
        }

        private void tmHeight_Tick(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            int wantTop = this.Top - FAStepHeight;
            if (wantTop < FTop)
            {
                wantTop = FTop;
            }

            this.Top = wantTop;

            if (wantTop == FTop)
            {
                tmHeight.Enabled = false;
                tmClose.Enabled = FCloseByTime;
            }
        }

        private void tmClose_Tick(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            Close();
        }

        private void IEFS_BubbleForm_Click(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            if (FCloseByClick)
            {
                if (this.Top != FTop)
                {
                    return;
                }

                Close();
            }
        }

        private void IEFS_BubbleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

        }


    }
}
