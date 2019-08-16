using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// 调用结果
    /// </summary>
    [Serializable]
    public class ServiceResult
    {
        private int _state;
        private string _message;
        private object _data;

        /// <summary>
        /// 状态
        /// </summary>
        public int state
        {
            get { return this._state; }
            set { this._state = value; }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public string message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public object data
        {
            get { return this._data; }
            set { this._data = value; }
        }
    }
}
