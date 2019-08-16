
using System;
using System.Xml.Serialization;

namespace IE310.Core.Utils
{

    [Serializable]
    public class I3MsgInfo
    {
        private bool bState;
        public static I3MsgInfo Default = new I3MsgInfo();
        private Exception exception;
        private string sMsg;
        private object userData;

        public I3MsgInfo()
        {
            this.bState = true;
            this.sMsg = null;
            this.userData = null;
            this.exception = null;
        }

        public I3MsgInfo(bool state, string msg)
        {
            this.bState = state;
            this.sMsg = msg;
            this.userData = null;
            this.exception = null;
        }

        public I3MsgInfo(bool state, string msg, Exception e)
        {
            this.bState = state;
            this.sMsg = msg;
            this.userData = null;
            this.exception = e;
        }

        public I3MsgInfo(bool state, string msg, object uData)
        {
            this.bState = state;
            this.sMsg = msg;
            this.userData = uData;
        }

        [XmlIgnore]
        public Exception ExpMsg
        {
            get
            {
                return this.exception;
            }
            set
            {
                this.exception = value;
            }
        }

        public string Message
        {
            get
            {
                return this.sMsg;
            }
            set
            {
                this.sMsg = value;
            }
        }

        public bool State
        {
            get
            {
                return this.bState;
            }
            set
            {
                this.bState = value;
            }
        }

        public object UserData
        {
            get
            {
                return this.userData;
            }
            set
            {
                this.userData = value;
            }
        }
    }
}

