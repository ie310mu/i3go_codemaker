using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using IE310.Core.Utils;

namespace IE310.Core.Controls
{

    [DefaultEvent("DateChange")]
    public partial class I3DatePanel : UserControl
    {
        public I3DatePanel()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get
            {
                return gp.Text;
            }
            set
            {
                gp.Text = value;
            }
        }

        /// <summary>
        /// 记录是否需要响应控件的事件
        /// </summary>
        private bool useEvent = true;

        private bool canSendMessage = true;
        /// <summary>
        /// 指示是否会发送日期改变事件
        /// </summary>
        public bool CanSendMessage
        {
            get
            {
                return canSendMessage;
            }
            set
            {
                canSendMessage = value;
            }
        }

        /// <summary>
        /// 返回开始日期是否选择
        /// </summary>
        public bool BeginChecked
        {
            get
            {
                return dtBegin.Checked;
            }
        }

        /// <summary>
        /// 返回结束日期是否选择
        /// </summary>
        public bool EndChecked
        {
            get
            {
                return dtEnd.Checked;
            }
        }


        /// <summary>
        /// 返回或设置开始日期，未选择时返回 1754-01-01
        /// 注：不会引发日期改变事件  如果需要引发事件，需要主动调用 SendDateChangeMessage();
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                if (dtBegin.Checked)
                {
                    return dtBegin.Value.Date;
                }
                else
                {
                    return new DateTime(1754, 1, 1);
                }
            }
            set
            {
                bool nowValue = canSendMessage;
                bool nowUseEvent = useEvent;
                canSendMessage = false;
                useEvent = false;
                try
                {
                    dtBegin.Value = value;
                }
                finally
                {
                    canSendMessage = nowValue;
                    useEvent = nowUseEvent;
                }
            }
        }

        /// <summary>
        /// 返回或设置结束日期，未选择时返回DateTime.MaxValue
        /// 注：不会引发日期改变事件  如果需要引发事件，需要主动调用 SendDateChangeMessage();
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (dtEnd.Checked)
                {
                    return dtEnd.Value.Date;
                }
                else
                {
                    return DateTime.MaxValue;
                }
            }
            set
            {
                bool nowValue = canSendMessage;
                bool nowUseEvent = useEvent;
                canSendMessage = false;
                useEvent = false;
                try
                {
                    dtEnd.Value = value;
                }
                finally
                {
                    canSendMessage = nowValue;
                    useEvent = nowUseEvent;
                }
            }
        }

        /// <summary>
        /// 获取或设置当前的选择模式  对除other外的模式，会引起开始结束日期的改变
        /// 注：不会引发日期改变事件  如果需要引发事件，需要主动调用 SendDateChangeMessage();
        /// </summary>
        public I3DatePanelSelMode Mode
        {
            get
            {
                if (rbAll.Checked)
                {
                    return I3DatePanelSelMode.dpsmAll;
                }
                else if (rbYear.Checked)
                {
                    return I3DatePanelSelMode.dpsmYear;
                }
                else if (rbMonth.Checked)
                {
                    return I3DatePanelSelMode.dpsmMonth;
                }
                else if (rbWeek.Checked)
                {
                    return I3DatePanelSelMode.dpsmWeek;
                }
                else if (rbDay.Checked)
                {
                    return I3DatePanelSelMode.dpsmDay;
                }
                else if (rbOther.Checked)
                {
                    return I3DatePanelSelMode.dpsmOther;
                }
                else
                {
                    return I3DatePanelSelMode.dpsmMonth;
                }
            }
            set
            {
                bool nowValue = canSendMessage;
                bool nowUseEvent = useEvent;
                canSendMessage = false;
                useEvent = false;
                try
                {
                    DateTime now = DateTime.Now.Date;
                    switch (value)
                    {
                        case I3DatePanelSelMode.dpsmAll:
                            rbAll.Checked = true;
                            dtBegin.Checked = false;
                            dtEnd.Checked = false;
                            break;
                        case I3DatePanelSelMode.dpsmYear:
                            rbYear.Checked = true;
                            dtBegin.Checked = true;
                            dtEnd.Checked = true;
                            dtBegin.Value = new DateTime(now.Year, 1, 1);
                            dtEnd.Value = dtBegin.Value.AddYears(1).AddDays(-1);
                            break;
                        case I3DatePanelSelMode.dpsmMonth:
                            rbMonth.Checked = true;
                            dtBegin.Checked = true;
                            dtEnd.Checked = true;
                            dtBegin.Value = new DateTime(now.Year, now.Month, 1);
                            dtEnd.Value = dtBegin.Value.AddMonths(1).AddDays(-1);
                            break;
                        case I3DatePanelSelMode.dpsmWeek:
                            rbWeek.Checked = true;
                            dtBegin.Checked = true;
                            dtEnd.Checked = true;
                            int delDay = (int)now.DayOfWeek;
                            if (delDay == 0)  //星期天
                            {
                                delDay = 7;
                            }
                            delDay = delDay - 1;
                            delDay = 0 - delDay;
                            dtBegin.Value = now.AddDays(delDay);
                            dtEnd.Value = dtBegin.Value.AddDays(6);
                            break;
                        case I3DatePanelSelMode.dpsmDay:
                            rbDay.Checked = true;
                            dtBegin.Checked = true;
                            dtEnd.Checked = true;
                            dtBegin.Value = now;
                            dtEnd.Value = now;
                            break;
                        case I3DatePanelSelMode.dpsmOther:
                            rbOther.Checked = true;
                            break;
                        default:
                            rbMonth.Checked = true;
                            dtBegin.Checked = true;
                            dtEnd.Checked = true;
                            dtBegin.Value = new DateTime(now.Year, now.Month, 0);
                            dtEnd.Value = dtBegin.Value.AddMonths(1).AddDays(-1);
                            break;
                    }
                }
                finally
                {
                    canSendMessage = nowValue;
                    useEvent = nowUseEvent;
                }
            }
        }

        /// <summary>
        /// 对控件进行初始化
        /// 注：不会引发日期改变事件  如果需要引发事件，需要主动调用 SendDateChangeMessage();
        /// </summary>
        /// <param name="aMode"></param>
        /// <param name="aBegin"></param>
        /// <param name="aEnd"></param>
        public void Init(I3DatePanelSelMode aMode, DateTime aBegin, DateTime aEnd)
        {
            BeginDate = aBegin;
            EndDate = aEnd;

            Mode = aMode;
        }

        private DateTime lastSendBegin, lastSendEnd = DateTime.MinValue;

        /// <summary>
        /// 日期改变事件
        /// </summary>
        [Description("日期改变事件"), Category("杂项")]
        public event I3DatePanelChangeEvent DateChange;

        /// <summary>
        /// 调用此方法，当canSendMessage=true时可引用日期改变事件
        /// checkLastSend时会检查上次发送的日期是否与现在相同，相同则不发送事件
        /// </summary>
        public void SendDateChangeMessage(bool checkLastSend)
        {
            if (!canSendMessage)
            {
                return;
            }

            if (checkLastSend)
            {
                if ((lastSendBegin == dtBegin.Value.Date) && (lastSendEnd == dtEnd.Value.Date))
                {
                    return;
                }
                else
                {
                    lastSendBegin = dtBegin.Value.Date;
                    lastSendEnd = dtEnd.Value.Date;
                }
            }

            if (!this.DesignMode)
            {
                if (DateChange != null)
                {
                    IECT_DatePanelChangeEventArgs args = new IECT_DatePanelChangeEventArgs();
                    args.Mode = Mode;
                    args.BeginChecked = BeginChecked;
                    args.EndChecked = EndChecked;
                    args.Begin = BeginDate;
                    args.End = EndDate;

                    DateChange(this, args);
                }
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((RadioButton)sender).Checked))
            {
                return;
            }

            Mode = I3DatePanelSelMode.dpsmAll;
            SendDateChangeMessage(false);
        }

        private void rbYear_CheckedChanged(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((RadioButton)sender).Checked))
            {
                return;
            }


            Mode = I3DatePanelSelMode.dpsmYear;
            SendDateChangeMessage(true);
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((RadioButton)sender).Checked))
            {
                return;
            }


            Mode = I3DatePanelSelMode.dpsmMonth;
            SendDateChangeMessage(true);
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((RadioButton)sender).Checked))
            {
                return;
            }


            Mode = I3DatePanelSelMode.dpsmWeek;
            SendDateChangeMessage(true);
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((RadioButton)sender).Checked))
            {
                return;
            }


            Mode = I3DatePanelSelMode.dpsmDay;
            SendDateChangeMessage(true);
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((RadioButton)sender).Checked))
            {
                return;
            }


            Mode = I3DatePanelSelMode.dpsmOther;
            SendDateChangeMessage(false);
        }

        private void btPrvMonth_Click(object sender, EventArgs e)
        {
            if (!useEvent)
            {
                return;
            }

            DateTime aBegin = new DateTime(dtBegin.Value.Year, dtBegin.Value.Month, 1).AddMonths(-1);
            DateTime aEnd = aBegin.AddMonths(1).AddDays(-1);

            Init(I3DatePanelSelMode.dpsmOther, aBegin, aEnd);

            SendDateChangeMessage(true);
        }

        private void btNextMonth_Click(object sender, EventArgs e)
        {
            if (!useEvent)
            {
                return;
            }

            DateTime aBegin = new DateTime(dtEnd.Value.Year, dtEnd.Value.Month, 1).AddMonths(1);
            DateTime aEnd = aBegin.AddMonths(1).AddDays(-1);

            Init(I3DatePanelSelMode.dpsmOther, aBegin, aEnd);

            SendDateChangeMessage(true);
        }

        private void dtBegin_Leave(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((DateTimePicker)sender).Checked))
            {
                return;
            }

            Mode = I3DatePanelSelMode.dpsmOther;

            SendDateChangeMessage(true);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
        }






        /*
        private void dtBegin_Validated(object sender, EventArgs e)
        {
            if ((!useEvent) || (!((DateTimePicker)sender).Checked))
            {
                return;
            }

            Mode = IECT_EnumDatePanelSelMode.dpsmOther;

            SendDateChangeMessage();
        }*/
    }

    public enum I3DatePanelSelMode
    {
        dpsmAll = 0,
        dpsmYear = 1,
        dpsmMonth = 2,
        dpsmWeek = 3,
        dpsmDay = 4,
        dpsmOther = 5
    }

    public class IECT_DatePanelChangeEventArgs : EventArgs
    {
        public I3DatePanelSelMode Mode;
        public bool BeginChecked;
        public bool EndChecked;
        public DateTime Begin;
        public DateTime End;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Mode.ToString());
            sb.Append("\r\n");
            sb.Append(BeginChecked.ToString());
            sb.Append("\r\n");
            sb.Append(EndChecked.ToString());
            sb.Append("\r\n");
            sb.Append(I3DateTimeUtil.ConvertDateTimeToDateString(Begin));
            sb.Append("\r\n");
            sb.Append(I3DateTimeUtil.ConvertDateTimeToDateString(End));

            return sb.ToString();
        }
    }

    public delegate void I3DatePanelChangeEvent(Object Sender, IECT_DatePanelChangeEventArgs e);
}
