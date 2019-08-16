using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace IE310.Core.Utils
{
    /// <summary>
    /// 添加日志信息时的处理
    /// </summary>
    /// <param name="sInfo"></param>
    public delegate void WriteLogEvenHandler(string sInfo);


    /// <summary>
    /// 本地日志信息管理类
    /// </summary>
    public class I3LocalLogUtil
    {
        private static readonly object _currentLockObj = new object();
        static I3LocalLogUtil _current;
        /// <summary>
        /// 获取日志管理器的实例
        /// </summary>
        public static I3LocalLogUtil Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_currentLockObj)
                    {
                        if (_current == null)
                        {
                            I3LocalLogUtil tmp = new I3LocalLogUtil();
                            //删除30天以前的日志
                            string sRootPath = tmp.PreparePath();
                            DirectoryInfo dir = new DirectoryInfo(sRootPath);
                            OptimizePersistence(dir);

                            _current = tmp;
                        }
                    }
                }

                return _current;
            }
        }

        private bool autoComplete = true;
        /// <summary>
        /// 是否写入一条记录就保存一条
        /// </summary>
        public bool AutoComplete
        {
            get
            {
                return autoComplete;
            }
            set
            {
                autoComplete = value;
            }
        }

        private int autoCompleteLength = 10240;
        /// <summary>
        /// AutoComplete=false时，超过多少字节自动保存，默认10K
        /// </summary>
        public int AutoCompleteLength
        {
            get
            {
                return autoCompleteLength;
            }
            set
            {
                autoCompleteLength = value;
            }
        }

        private string logDir = "log";
        public string LogDir
        {
            get
            {
                return logDir;
            }
            set
            {
                logDir = value;
            }
        }

        
        private I3LocalLogUtil()
        {
        }

        public event WriteLogEvenHandler OnWriteLogEventHandler;

        private readonly object contentsLockObj = new object();
        System.Text.StringBuilder contents = new StringBuilder();

        /// <summary>
        /// 记录一般记录信息
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteInfoLog(string sInfo)
        {
            WriteExceptionLog(sInfo, null, null);
        }

        /// <summary>
        /// 记录一般记录信息
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteInfoLog(string sInfo, string sDataInfo)
        {
            WriteExceptionLog(sInfo, sDataInfo, null);
        }

        /// <summary>
        /// 记录异常信息日志
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteExceptionLog(string sInfo, Exception ex)
        {
            WriteExceptionLog(sInfo, null, ex);
        }

        /// <summary>
        /// 记录异常信息日志
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteExceptionLog(string sInfo, string sDataInfo, Exception ex)
        {
            StringBuilder sLog = new StringBuilder();
            sLog.Append("\r\n");
            sLog.Append("【").Append(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")).Append("】 ");
            sLog.Append("信息:");
            sLog.Append(sInfo);
            if (!string.IsNullOrEmpty(sDataInfo))
            {
                sLog.Append("\r\n***数据信息***\r\n");
                sLog.Append(sDataInfo);
            }
            if (ex != null)
            {
                sLog.Append("\r\n***异常信息***\r\n").Append(ex.Message);
                sLog.Append("\r\n\t").Append(ex.ToString());
            }
            if (OnWriteLogEventHandler != null)
            {
                this.OnWriteLogEventHandler(sLog.ToString());
            }

            //修改时加锁
            lock (contentsLockObj)
            {
                contents.Append(sLog.ToString());
            }

            if (autoComplete)
            {
                CompleteLog();
            }
            else
            {
                if (contents.Length > autoCompleteLength)
                {
                    CompleteLog();
                }
            }
        }

        /// <summary>
        /// 将内存中的日志持久化到磁盘
        /// </summary>
        /// <param name="path">文件的存放目录</param>
        private void Persist(System.IO.DirectoryInfo path)
        {
            //读取时加锁
            lock (contentsLockObj)
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(PreparePath(path), true, Encoding.UTF8))
                {
                    writer.WriteLine(contents.ToString());
                    writer.Flush();
                    writer.Close();
                    contents.Remove(0, contents.Length);
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// 完成日志的持久化记录信息
        /// </summary>
        public void CompleteLog()
        {
            try
            {
                string sRootPath = PreparePath();
                DirectoryInfo dir = new DirectoryInfo(sRootPath);
                //创建本次登录后的所有日志
                Persist(dir);
            }
            catch //(Exception ex)
            {
            }

        }

        /// <summary>
        /// 构造日志文件名
        /// </summary>
        /// <param name="path">日志文件所在目录</param>
        /// <returns></returns>
        protected string PreparePath(System.IO.DirectoryInfo path)
        {
            string file = DateTime.Now.ToString("yyyy-MM-dd-HH") + ".log";
            return Path.Combine(path.FullName, file);
        }

        /// <summary>
        /// 获取日志存储目录信息
        /// </summary>
        /// <returns></returns>
        private string PreparePath()
        {
            try
            {
                string sPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logDir);
                System.IO.DirectoryInfo DirInfo = new DirectoryInfo(sPath);
                if (!DirInfo.Exists)
                {
                    DirInfo.Create();
                }
                DirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                return DirInfo.FullName;
            }
            catch//(Exception ex)
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 删除非30天以前的所有日志
        /// </summary>
        /// <param name="path"></param>
        private static void OptimizePersistence(System.IO.DirectoryInfo path)
        {
            foreach (System.IO.FileInfo file in path.GetFiles())
            {
                try
                {
                    if (Allow(file))
                    {
                        if (!file.IsReadOnly)
                        {
                            file.Delete();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 获取是否允许删除
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected static bool Allow(System.IO.FileInfo file)
        {
            if (file.Extension == "log" || file.Extension == ".log")
            {
                string[] sParam = file.Name.Split('.')[0].Split('-');
                if (sParam.Length != 4)
                {
                    return false;
                }
                int iYear = 2046;
                int iMonth = 1;
                int iDay = 1;
                int iHour = 1;
                int.TryParse(sParam[0], out iYear);
                int.TryParse(sParam[1], out iMonth);
                int.TryParse(sParam[2], out iDay);
                int.TryParse(sParam[3], out iHour);
                TimeSpan t = System.DateTime.Now - new DateTime(iYear, iMonth, iDay, iHour, 0, 0);
                if (t.Days > 30)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetLogString()
        {
            //读取时加锁
            lock (contentsLockObj)
            {
                string result = contents.ToString();
                contents.Remove(0, contents.Length);
                GC.Collect();
                return result;
            }
        }
    }
}
