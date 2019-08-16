/*
 * 此类中的代码有待查找资料并进一步完善
*/

using System;
using System.Collections.Generic;

using System.Text;
using System.Management;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.ServiceProcess;

namespace IE310.Core.Utils
{
    public static class I3PCUtil
    {
        public static bool CheckServiceExists(string serviceName)
        {
            try
            {
                ServiceController sc2 = new ServiceController(serviceName);
                ServiceControllerStatus status = sc2.Status;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void RunService(string serviceName, int waitTime)
        {
            try
            {
                ServiceController sc2 = new ServiceController(serviceName);
                if (sc2.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    sc2.Start();
                    Thread.Sleep(waitTime);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void StopService(string serviceName, int waitTime)
        {
            try
            {
                ServiceController sc2 = new ServiceController(serviceName);
                if (sc2.Status.Equals(ServiceControllerStatus.Stopped))
                {
                }
                else
                {
                    sc2.Stop();
                    Thread.Sleep(waitTime);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查服务器名是否本机
        /// </summary>
        /// <returns></returns>
        public static bool CheckServerNameIsLocal(string aServerName)
        {
            aServerName = I3StringUtil.SplitStringToList(aServerName.ToUpper(), "\\")[0];

            if (string.IsNullOrEmpty(aServerName))
            {
                return true;
            }
            if (string.Equals(aServerName, "."))
            {
                return true;
            }
            if (string.Equals(aServerName, "127.0.0.1"))
            {
                return true;
            }
            if (string.Equals(aServerName, "(LOCAL)"))
            {
                return true;
            }
            if (string.Equals(aServerName, GetMachineName().ToUpper()))
            {
                return true;
            }
            foreach (string s in GetLocalIP())
            {
                if (string.Equals(aServerName, s.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<string> GetLocalIP()
        {
            IPHostEntry ihe = Dns.GetHostEntry(Dns.GetHostName());
            List<string> list = new List<string>();
            for (int i = 0; i <= ihe.AddressList.Length - 1; i++)
            {
                if (ihe.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    list.Add(ihe.AddressList[i].ToString());
                }
            }

            return list;
        }

        /// <summary>
        /// 获取计算机名称
        /// </summary>
        /// <returns></returns>
        public static string GetMachineName()
        {
            return Environment.MachineName.ToString();
        }

        /// <summary>
        /// 获取操作系统版本号
        /// </summary>
        /// <returns></returns>
        public static string GetSystemVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        /// <summary>
        /// 获取CPU编号
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetCPUNO()
        {
            ArrayList list = new ArrayList();

            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                list.Add(mo["processorid"].ToString());
            }

            return list;
        }

        /// <summary>
        /// 获取CPU版本信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetCPUVersion()
        {
            ArrayList list = new ArrayList();

            ManagementObjectSearcher driveID = new ManagementObjectSearcher("select * from Win32_Processor");

            foreach (ManagementObject mo in driveID.Get())
            {
                list.Add(mo["Version"].ToString());
            }

            return list;
        }

        /// <summary>
        /// 获取CPU产品名称信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetCPUName()
        {
            ArrayList list = new ArrayList();

            ManagementObjectSearcher driveID = new ManagementObjectSearcher("select * from Win32_Processor");

            foreach (ManagementObject mo in driveID.Get())
            {
                list.Add(mo["Name"].ToString());
            }

            return list;
        }


        /// <summary>
        /// 获取CPU制造商
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetCPUMaker()
        {
            ArrayList list = new ArrayList();

            ManagementObjectSearcher driveID = new ManagementObjectSearcher("select * from Win32_Processor");

            foreach (ManagementObject mo in driveID.Get())
            {
                list.Add(mo["Manufacturer"].ToString());
            }

            return list;
        }

        /// <summary>
        /// 获取显示设备的PNPDeviceID
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetVideoPNP()
        {
            ArrayList list = new ArrayList();

            ManagementObjectSearcher driveID = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject mo in driveID.Get())
            {
                list.Add(mo["PNPDeviceID"].ToString());
            }

            return list;
        }

        /// <summary>
        /// 获取声音设备的PNPDeviceID
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetSoundPNP()
        {
            ArrayList list = new ArrayList();

            ManagementObjectSearcher driveID = new ManagementObjectSearcher("select * from Win32_SoundDevice");

            foreach (ManagementObject mo in driveID.Get())
            {
                list.Add(mo["PNPDeviceID"].ToString());
            }

            return list;
        }


        /// <summary>
        /// 获取主板编号  注意，有时候返回 to be filled by O.E.M
        /// </summary>
        /// <returns></returns>
        public static string GetBaseBoardNO()
        {
            SelectQuery query = new SelectQuery("select * from Win32_BaseBoard");
            ManagementObjectSearcher driveID = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator data = driveID.Get().GetEnumerator();
            data.MoveNext();
            ManagementBaseObject board = data.Current;
            return board.GetPropertyValue("SerialNumber").ToString();
        }


        /// <summary>
        /// 获取主板制造商
        /// </summary>
        /// <returns></returns>
        public static string GetBaseBoardMaker()
        {
            SelectQuery query = new SelectQuery("select * from Win32_BaseBoard");
            ManagementObjectSearcher driveID = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator data = driveID.Get().GetEnumerator();
            data.MoveNext();
            ManagementBaseObject board = data.Current;
            return board.GetPropertyValue("Manufacturer").ToString();
        }

        /// <summary>
        /// 获取主板制型号
        /// </summary>
        /// <returns></returns>
        public static string GetBaseBoardVersion()
        {
            SelectQuery query = new SelectQuery("select * from Win32_BaseBoard");
            ManagementObjectSearcher driveID = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator data = driveID.Get().GetEnumerator();
            data.MoveNext();
            ManagementBaseObject board = data.Current;
            return board.GetPropertyValue("Product").ToString();
        }


        /// <summary>
        /// 获取当前Windows用户名称
        /// </summary>
        /// <returns></returns>
        public static string GetNowUserName()
        {
            return Environment.UserName;
        }


        /// <summary>
        /// 根据exe名称查找进程是否存在，不区分大小写  exe名称示例：QQ.exe
        /// </summary>
        /// <param name="exeFileName"></param>
        /// <returns></returns>
        public static bool FindProcess(string exeFileName)
        {
            exeFileName = exeFileName.ToUpper();

            foreach (Process process in Process.GetProcesses())
            {
                string processName = process.ProcessName + ".Exe";
                processName = processName.ToUpper();
                if (string.Equals(exeFileName, processName))
                {
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// 根据exe名称查找进程是否存在，不区分大小写  exe名称示例：QQ.exe
        /// </summary>
        /// <param name="exeFileName"></param>
        /// <returns></returns>
        public static void KillProcess(string exeFileName)
        {
            exeFileName = exeFileName.ToUpper();

            foreach (Process process in Process.GetProcesses())
            {
                string processName = process.ProcessName + ".Exe";
                processName = processName.ToUpper();
                if (string.Equals(exeFileName, processName))
                {
                    process.Kill();
                }
            }
        }


        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);


        /// <summary>
        /// 根据窗口标题查找窗口  不区分大小写
        /// </summary>
        /// <param name="aCaption"></param>
        /// <returns></returns>
        public static bool FindWindowByCaption(string aCaption)
        {
            IntPtr hWnd = FindWindow(null, aCaption);

            return (int)hWnd != 0;
        }


        /// <summary>
        /// 根据exe文件名和命令行参数，以阻塞形式运行程序
        /// 
        /// 2011-02-14:发现一个问题，以前在delphi中时，这样直接打开一个文本文件的话，将不是阻塞模式，
        /// 而在.net里面则是阻塞模式！
        /// 
        /// </summary>
        public static I3MsgInfo CreateAndWaitProcess(string workingDirectory, string fileName, string args, bool wait, bool useShellExecute, bool createNoWindow)
        {
            using (Process process = new Process())
            {
                try
                {
                    process.StartInfo.WorkingDirectory = workingDirectory;
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Arguments = args;
                    process.StartInfo.UseShellExecute = useShellExecute;
                    process.StartInfo.CreateNoWindow = createNoWindow;
                    process.Start();
                    if (wait)
                    {
                        process.WaitForExit();
                    }
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }
            }
        }
        public static I3MsgInfo CreateAndWaitProcess(string workingDirectory, string fileName, string args, bool wait)
        {
            return CreateAndWaitProcess(workingDirectory, fileName, args, wait, true, false);
        }



        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
               IntPtr hWnd,　　　// handle to destination window 
               int Msg,　　　 // message 
               int wParam,　// first message parameter 
               int lParam // second message parameter 
         );


        private class IEFS_RunExeInThread
        {
            private string WorkingDirectory;
            private string FileName;
            private string Args;
            private IntPtr Handle;
            private int UserMessage;
            private int Flag;

            public IEFS_RunExeInThread(string workingDirectory, string aFileName, string aArgs, IntPtr aHandle, int aUserMessage, int aFlag)
            {
                WorkingDirectory = workingDirectory;
                FileName = aFileName;
                Args = aArgs;
                Handle = aHandle;
                UserMessage = aUserMessage;
                Flag = aFlag;
            }

            public void Run()
            {
                if (CreateAndWaitProcess(WorkingDirectory, FileName, Args, true).State)
                {
                    if (Handle != IntPtr.Zero)
                    {
                        SendMessage(Handle, UserMessage, 1, Flag);
                    }
                }
                else
                {
                    if (Handle != IntPtr.Zero)
                    {
                        SendMessage(Handle, UserMessage, 0, Flag);
                    }
                }
            }
        }

        /// <summary>
        /// 在线程中运行程序，然后向aHandle发送消息
        /// wParam=1表示运行成功，wParam=0表示运行失败  lParam=aFlag
        /// 
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <param name="handle"></param>
        /// <param name="userMessage"></param>
        /// <param name="flag"></param>
        public static void CreateAndWaitProcessByEvent(string workingDirectory, string fileName, string args, IntPtr handle, int userMessage, int flag)
        {
            IEFS_RunExeInThread run = new IEFS_RunExeInThread(workingDirectory, fileName, args, handle, userMessage, flag);
            Thread thread = new Thread(new ThreadStart(run.Run));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
