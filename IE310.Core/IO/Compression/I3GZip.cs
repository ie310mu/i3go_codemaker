using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.IO;
using IE310.Core.Utils;

namespace IE310.Core.IO.Compression
{
    public class I3GZip
    {

        /// <summary>
        /// 使用GZip方式压缩文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <param name="maxFileMBs"></param>
        /// <returns></returns>
        public static I3MsgInfo ZipFile(string sourceFile, string destFile, int maxFileMBs)
        {
            if (!File.Exists(sourceFile))
            {
                return new I3MsgInfo(false, "错误：源文件不存在！文件名：" + sourceFile);
            }

            FileStream sourceStream = null;
            FileStream destStream = null;
            GZipStream zipStream = null;
            try
            {
                try
                {
                    sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

                    if (sourceStream.Length > maxFileMBs * 1024 * 1024)
                    {
                        return new I3MsgInfo(false, "要压缩的文件大小超过指定的最大大小" + maxFileMBs.ToString() + "M");
                    }

                    destStream = new FileStream(destFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    zipStream = new GZipStream(destStream, CompressionMode.Compress);
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }

                //sourceStream刚创建，从0开始
                return I3Stream.WriteStreamToStream(null, sourceStream, zipStream, 4096, null);

            }
            finally
            {
                if (zipStream != null)
                    zipStream.Close();
                if (destStream != null)
                    destStream.Close();
                if (sourceStream != null)
                    sourceStream.Close();
            }
        }

        /// <summary>
        /// 使用GZip方式解压缩文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <param name="maxFileMBs"></param>
        /// <returns></returns>
        public static I3MsgInfo UnZipFile(string sourceFile, string destFile, int maxFileMBs)
        {
            if (!File.Exists(sourceFile))
            {
                return new I3MsgInfo(false, "错误：源文件不存在！文件名：" + sourceFile);
            }

            FileStream sourceStream = null;
            FileStream destStream = null;
            GZipStream zipStrem = null;
            try
            {
                try
                {
                    sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                    if (sourceStream.Length > maxFileMBs * 1024 * 1024)
                    {
                        return new I3MsgInfo(false, "要解压缩的文件大小超过指定的最大大小" + maxFileMBs.ToString() + "M");
                    }
                    destStream = new FileStream(destFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    zipStrem = new GZipStream(sourceStream, CompressionMode.Decompress);
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }

                //压缩流不支持Position.DataLength等方法，所以不能用WriteStreamToStream函数
                //return IEFS_Stream.WriteStreamToStream(null, zipStrem, destStream, 4096, null)
                try
                {
                    byte[] data = new byte[4096];
                    while (true)
                    {
                        int size = zipStrem.Read(data, 0, data.Length);

                        if (size <= 0)
                            break;

                        destStream.Write(data, 0, size);
                    }
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }
            }
            finally
            {
                if (zipStrem != null)
                    zipStrem.Close();
                if (destStream != null)
                    destStream.Close();
                if (sourceStream != null)
                    sourceStream.Close();
            }
        }
    }
}