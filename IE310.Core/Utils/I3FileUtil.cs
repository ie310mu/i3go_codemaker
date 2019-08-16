using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Threading;

namespace IE310.Core.Utils
{
    public static class I3FileUtil
    {
        /// <summary>
        /// 检查文件是否存在，存在则删除，
        /// 最后，不存在时才返回true
        /// 
        /// 
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static bool CheckFileNotExists(string aFileName)
        {
            if (!File.Exists(aFileName))
            {
                return true;
            }


            //先清除只读属性，不然无法删除
            SetFileNotReadOnly(aFileName);
            File.Delete(aFileName);


            Thread.Sleep(50);
            return !File.Exists(aFileName);
        }

        /// <summary>
        /// 清除文件的只读属性 
        /// </summary>
        /// <param name="aFileName"></param>
        public static void SetFileNotReadOnly(string aFileName)
        {
            if (!File.Exists(aFileName))
            {
                return;
            }

            FileInfo info = new FileInfo(aFileName);
            info.IsReadOnly = false;
        }

        /// <summary>
        /// 获取文件的大小
        /// 
        /// 注意：无任何处理，需要保证文件的确存在且可读
        /// 
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static long GetFileSize(string aFileName)
        {
            using (FileStream fileStream = File.OpenRead(aFileName))
            {
                return fileStream.Length;
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="createSource"></param>
        /// <returns></returns>
        public static I3MsgInfo MoveFile(string sourceFileName, string destFileName, bool deleteSource)
        {
            if (!File.Exists(sourceFileName))
            {
                return new I3MsgInfo(false, "源文件不存在！文件名:" + sourceFileName);
            }

            //先删除目的文件
            if (!CheckFileNotExists(destFileName))
            {
                return new I3MsgInfo(false, "文件删除失败");
            }

            //移动
            if (deleteSource)
            {
                File.Move(sourceFileName, destFileName);
            }
            else
            {
                File.Copy(sourceFileName, destFileName);
            }

            Thread.Sleep(50);
            if (File.Exists(destFileName))
            {
                return I3MsgInfo.Default;
            }
            else
            {
                return new I3MsgInfo(false,"未知原因，文件复制失败！");
            }
        }


        /// <summary>
        /// 获取文件的创建时间
        /// 
        /// 注意：无任何处理，需要保证文件的确存在且可读
        /// 
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static DateTime GetFileCreateTime(string aFileName)
        {
            FileInfo info = new FileInfo(aFileName);
            return info.CreationTime;
        }

        /// <summary>
        /// 获取文件的最后修改时间
        /// 
        /// 注意：无任何处理，需要保证文件的确存在且可读
        /// 
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static DateTime GetFileLastWriteTime(string aFileName)
        {
            FileInfo info = new FileInfo(aFileName);
            return info.LastWriteTime;
        }

        /// <summary>
        /// 获取文件的最后访问时间
        /// 
        /// 注意：无任何处理，需要保证文件的确存在且可读
        /// 
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static DateTime GetFileLastAccessTime(string aFileName)
        {
            FileInfo info = new FileInfo(aFileName);
            return info.LastAccessTime;
        }
    }
}
