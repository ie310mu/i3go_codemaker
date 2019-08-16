using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Collections;
using System.Threading;

namespace IE310.Core.Utils
{
    public static class I3DirectoryUtil
    {
        /// <summary>
        /// 创建目录  目录已经存在则直接返回true
        /// 
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// 
        /// </summary>
        /// <param name="aDir"></param>
        /// <returns></returns>
        public static I3MsgInfo CreateDirctory(string aDir)
        {
            if (!Directory.Exists(aDir))
            {
                Directory.CreateDirectory(aDir);

                Thread.Sleep(50);
                if (!Directory.Exists(aDir))
                {
                    return new I3MsgInfo(false, "目录创建失败！目录：" + aDir);
                }

                return I3MsgInfo.Default;
            }
            else
            {
                return I3MsgInfo.Default;
            }
        }

        /// <summary>
        /// 通过指定的文件名，为它创建目录
        /// 
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// 
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static I3MsgInfo CreateDirectoryByFileName(string aFileName)
        {
            if (string.IsNullOrEmpty(aFileName))
            {
                return new I3MsgInfo(false, "未指定文件名");
            }

            string dir = Path.GetDirectoryName(aFileName);
            return I3DirectoryUtil.CreateDirctory(dir);
        }

        /// <summary>
        /// 删除目录(子目录和文件也删除)
        /// 
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// 
        /// 注意：如果此目录已经打开，最后检查时目录还存在，会报错，但随着代码的执行，目录仍然被删除
        /// 
        /// </summary>
        /// <param name="aDir"></param>
        /// <returns></returns>
        public static I3MsgInfo DeleteDirctory(string aDir)
        {
            if (!Directory.Exists(aDir))
            {
                return I3MsgInfo.Default;
            }
            else
            {
                //要先删除所有文件，因为如果有只读文件，删除会失败
                //目录只读时不影响删除
                //用IEFS_File.CheckFileNotExists速度会比较慢，因此只去除只读属性，再整个删除目录，这样速度快 
                ArrayList fileList = I3DirectoryUtil.ListFile(aDir, true);
                foreach (string s in fileList)
                {
                    FileInfo info = new FileInfo(s);
                    if (info.IsReadOnly)
                    {
                        info.IsReadOnly = false;
                    }
                }

                Directory.Delete(aDir, true);

                Thread.Sleep(50);
                if (Directory.Exists(aDir))
                {
                    return new I3MsgInfo(false, "目录删除失败，目录名：" + aDir);
                }

                return I3MsgInfo.Default;
            }
        }

        /// <summary>
        /// 检查并清空目录，最后有空的目录存在则返回true
        /// </summary>
        /// <param name="aDir"></param>
        /// <returns></returns>
        public static I3MsgInfo CheckAndClearDirctory(string aDir)
        {
            I3MsgInfo msg =I3DirectoryUtil.DeleteDirctory(aDir);
            if (!msg.State)
            {
                return msg;
            }

            return I3DirectoryUtil.CreateDirctory(aDir);
        }

        /// <summary>
        /// 检查目录名的最后是个字符是不是 \ ，不是则加上 
        /// </summary>
        /// <param name="aDirtoryName"></param>
        /// <returns></returns>
        public static string CheckDirctoryLastChar(string aDirtoryName)
        {
            string aNewDirtory = aDirtoryName;
            if (!I3StringUtil.SubString(aNewDirtory, aNewDirtory.Length - 1).Equals(@"\"))
            {
                aNewDirtory = aNewDirtory + @"\";
            }

            return aNewDirtory;
        }


        private static void ListDirEx(string aDir, bool hasSub, ArrayList list)
        {
            foreach (string subDir in Directory.GetDirectories(aDir))
            {
                list.Add(subDir);

                if (hasSub)
                {
                    ListDirEx(subDir, hasSub, list);
                }
            }
        }
        /// <summary>
        /// 以绝对路径列表一个目录的所有子目录
        /// hasSub指定是否包含子目录的子目录
        /// </summary>
        /// <param name="aDir"></param>
        /// <param name="hasSub"></param>
        /// <returns></returns>
        public static ArrayList ListDir(string aDir, bool hasSub)
        {
            ArrayList list = new ArrayList();
            ListDirEx(aDir, hasSub, list);
            return list;
        }

        private static void ListFileEx(string aDir, bool hasSub, ArrayList list)
        {
            foreach (string subFile in Directory.GetFiles(aDir))
            {
                list.Add(subFile);
            }

            if (hasSub)
            {
                foreach (string subDir in Directory.GetDirectories(aDir))
                {
                    ListFileEx(subDir, hasSub, list);
                }
            }
        }
        /// <summary>
        /// 以绝对路径列表一个目录的所有文件
        /// hasSub指定是否包含子目录的文件
        /// </summary>
        /// <param name="aDir"></param>
        /// <param name="hasSub"></param>
        /// <returns></returns>
        public static ArrayList ListFile(string aDir, bool hasSub)
        {
            ArrayList list = new ArrayList();
            ListFileEx(aDir, hasSub, list);
            return list;
        }

        /// <summary>
        /// 获取 我的文档 的路径
        /// </summary>
        /// <returns></returns>
        public static string GetMyDocumentsDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }


        /// <summary>
        /// 获取系统路径  返回如C:\Windows\System32
        /// </summary>
        /// <returns></returns>
        public static string GetSystemDir()
        {
            return Environment.SystemDirectory.ToString();
        }

        /// <summary>
        /// 获取ProgramFilesDir目录路径  返回如C:\Program Files
        /// </summary>
        /// <returns></returns>
        public static string GetProgramFilesDir()
        {
            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        /// <summary>
        /// 获取桌面路径 
        /// </summary>
        /// <returns></returns>
        public static string GetDesktopDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        /// <summary>
        /// 获取开始菜单目录路径  示例：C:\Documents and Settings\Administrator\「开始」菜单
        /// </summary>
        /// <returns></returns>
        public static string GetStartMenuDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
        }



        /// <summary>
        /// 获取用户程序组目录路径  示例： C:\Documents and Settings\Administrator\「开始」菜单\程序
        /// </summary>
        /// <returns></returns>
        public static string GetProgramsDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Programs);
        }


        /// <summary>
        /// 获取用户文档模板目录路径  示例： C:\Documents and Settings\Administrator\Templates
        /// </summary>
        /// <returns></returns>
        public static string GetTemplatesDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Templates);
        }


        /// <summary>
        /// 获取收藏夹目录路径  示例： G:\ie\bak\Favorites
        /// </summary>
        /// <returns></returns>
        public static string GetFavoritesDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
        }

        /// <summary>
        /// 获取共享组件目录路径  示例： C:\Program Files\Common Files
        /// </summary>
        /// <returns></returns>
        public static string GetCommonProgramFilesDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
        }



        /// <summary>
        /// 获取我的图片目录路径  示例：  G:\ie\bak\My Documents\My Pictures
        /// </summary>
        /// <returns></returns>
        public static string GetMyPicturesDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }


        /// <summary>
        /// 获取Internet历史记录目录路径  示例： C:\Documents and Settings\Administrator\Local Settings\History
        /// </summary>
        /// <returns></returns>
        public static string GetInternetHistoryDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.History);
        }


        /// <summary>
        /// 获取Internet临时目录路径  示例： C:\Documents and Settings\Administrator\Local Settings\Temporary Internet Files
        /// </summary>
        /// <returns></returns>
        public static string GetInternetTmpDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
        }

        /// <summary>
        /// 返回程序运行目录下的tmp临时目录的路径 
        /// </summary>
        /// <returns></returns>
        public static string GetAppTmpDir()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");
        }


        /// <summary>
        /// 在程序运行目录下的tmp临时目录下，获取一个临时目录的路径
        /// 注：路径名是一个guid，永远不重复
        /// 注：仅返回路径名，不生成目录
        /// 
        /// 这里不能用IEFS_DateTime.ConvertDateTimeToLongString，因为如果在短时间内执行此代码多次时，返回的目录名会重复，删除时会错误
        /// 因此用一个Guid作为临时目录名，肯定不会重复
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetAppTmpTmpDir()
        {
            return Path.Combine(I3DirectoryUtil.GetAppTmpDir(), I3StringUtil.GetAGuid());
        }
    }
}
