using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Progressing
{
    public class I3ProgressingEventArgs : EventArgs
    {
        public I3ProgressingEventArgs(double min, double max, double position, string message, bool errorState, object userData)
        {
            this.min = min;
            this.max = max;
            this.position = position;
            this.message = message;
            this.errorState = errorState;
            this.userData = userData;
        }
        public I3ProgressingEventArgs(double min, double max, double position, string message)
            :this(min,max,position,message,false,null)
        {
        }
        public I3ProgressingEventArgs(double position, string message)
            : this(0, 100, position, message)
        {
        }
        public I3ProgressingEventArgs(double position)
            : this(position, null)
        {
        }

        private double min;
        /// <summary>
        /// 最小值
        /// </summary>
        public double Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }

        private double max;
        /// <summary>
        /// 最大值
        /// </summary>
        public double Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
            }
        }

        private double position;
        /// <summary>
        /// 当前值
        /// </summary>
        public double Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        private string message;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        private bool errorState = false;
        /// <summary>
        /// 是否处于错误状态
        /// </summary>
        public bool ErrorState
        {
            get
            {
                return errorState;
            }
            set
            {
                errorState = value;
            }
        }

        private bool cancel;
        /// <summary>
        /// 是否需要退出处理
        /// </summary>
        public bool Cancel
        {
            get
            {
                return cancel;
            }
            set
            {
                cancel = value;
            }
        }

        private object userData;
        /// <summary>
        /// 附加数据
        /// </summary>
        public object UserData
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
            }
        }
    }
}
