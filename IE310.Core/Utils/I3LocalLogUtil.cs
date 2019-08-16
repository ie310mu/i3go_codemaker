using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace IE310.Core.Utils
{
    /// <summary>
    /// �����־��Ϣʱ�Ĵ���
    /// </summary>
    /// <param name="sInfo"></param>
    public delegate void WriteLogEvenHandler(string sInfo);


    /// <summary>
    /// ������־��Ϣ������
    /// </summary>
    public class I3LocalLogUtil
    {
        private static readonly object _currentLockObj = new object();
        static I3LocalLogUtil _current;
        /// <summary>
        /// ��ȡ��־��������ʵ��
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
                            //ɾ��30����ǰ����־
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
        /// �Ƿ�д��һ����¼�ͱ���һ��
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
        /// AutoComplete=falseʱ�����������ֽ��Զ����棬Ĭ��10K
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
        /// ��¼һ���¼��Ϣ
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteInfoLog(string sInfo)
        {
            WriteExceptionLog(sInfo, null, null);
        }

        /// <summary>
        /// ��¼һ���¼��Ϣ
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteInfoLog(string sInfo, string sDataInfo)
        {
            WriteExceptionLog(sInfo, sDataInfo, null);
        }

        /// <summary>
        /// ��¼�쳣��Ϣ��־
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteExceptionLog(string sInfo, Exception ex)
        {
            WriteExceptionLog(sInfo, null, ex);
        }

        /// <summary>
        /// ��¼�쳣��Ϣ��־
        /// </summary>
        /// <param name="sInfo"></param>
        public void WriteExceptionLog(string sInfo, string sDataInfo, Exception ex)
        {
            StringBuilder sLog = new StringBuilder();
            sLog.Append("\r\n");
            sLog.Append("��").Append(DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss")).Append("�� ");
            sLog.Append("��Ϣ:");
            sLog.Append(sInfo);
            if (!string.IsNullOrEmpty(sDataInfo))
            {
                sLog.Append("\r\n***������Ϣ***\r\n");
                sLog.Append(sDataInfo);
            }
            if (ex != null)
            {
                sLog.Append("\r\n***�쳣��Ϣ***\r\n").Append(ex.Message);
                sLog.Append("\r\n\t").Append(ex.ToString());
            }
            if (OnWriteLogEventHandler != null)
            {
                this.OnWriteLogEventHandler(sLog.ToString());
            }

            //�޸�ʱ����
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
        /// ���ڴ��е���־�־û�������
        /// </summary>
        /// <param name="path">�ļ��Ĵ��Ŀ¼</param>
        private void Persist(System.IO.DirectoryInfo path)
        {
            //��ȡʱ����
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
        /// �����־�ĳ־û���¼��Ϣ
        /// </summary>
        public void CompleteLog()
        {
            try
            {
                string sRootPath = PreparePath();
                DirectoryInfo dir = new DirectoryInfo(sRootPath);
                //�������ε�¼���������־
                Persist(dir);
            }
            catch //(Exception ex)
            {
            }

        }

        /// <summary>
        /// ������־�ļ���
        /// </summary>
        /// <param name="path">��־�ļ�����Ŀ¼</param>
        /// <returns></returns>
        protected string PreparePath(System.IO.DirectoryInfo path)
        {
            string file = DateTime.Now.ToString("yyyy-MM-dd-HH") + ".log";
            return Path.Combine(path.FullName, file);
        }

        /// <summary>
        /// ��ȡ��־�洢Ŀ¼��Ϣ
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
        /// ɾ����30����ǰ��������־
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
        /// ��ȡ�Ƿ�����ɾ��
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
            //��ȡʱ����
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
