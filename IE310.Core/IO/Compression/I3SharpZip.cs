/*                     IEFS_SharpZip.IEFS_SharpZip
 * 
 * 
 *          类型: 类　压缩
 * 
 *          说明: 要使用SharpZip开源dll
 * 
 *      使用方法: 1.生成类ieSharpZip的对象 zip
 *                2.ZipFile　压缩文件
 *                  ZipPath  压缩目录(不包含子目录的文件)
 *                  UnZipFile 解压缩文件
 *                  （在进行操作前，最好将 窗口.enabled = false;）
 *                  UpZipFileByIndex根据index解压文件 
 *                3.CompressionLevel // 0 - store only to 9 - means best compression
 *                  BlockSize 缓存大小 一般设置为4096即可
 * 
 *      注意事项: 1.使用本单元压缩得到的文件，用winrar软件解压缩时可能会产生错误
 * 
 *      修改记录: 1.
 *  
 *                                                  Created by ie
 *                                                            2008-04-04     
 * 
 * */


using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using IE310.Core.Utils;
using IE310.Core.Progressing;

namespace IE310.Core.IO.Compression
{
    public class I3SharpZip
    {
        //public bool ffshowProcess = true;



        /// <summary>
        /// 将文件流写入到压缩流中
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="zipOutPutStream"></param>
        /// <param name="sourceFileStream">非完整文件名</param>
        /// <param name="BlockSize"></param>
        /// <param name="form"></param>
        private static void WriteFile(string sourceFileName, ZipOutputStream zipOutPutStream, FileStream sourceFileStream, int BlockSize, IProgressReporter progressReporter)
        {
            string messageInfo = "正在压缩文件\"" + sourceFileName + "\"";
            sourceFileStream.Position = 0;
            I3Stream.WriteStreamToStream(messageInfo, sourceFileStream, zipOutPutStream, BlockSize, progressReporter);
        }

        /// <summary>
        /// 从文件名和文件流得到一个Entry
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private static ZipEntry GetEntry(string fileName, FileStream fileStream)
        {
            ZipEntry ZipEntry = new ZipEntry(Path.GetFileName(fileName));
            ZipEntry.DateTime = DateTime.Now;
            ZipEntry.Size = fileStream.Length;

            Crc32 crc = new Crc32();

            byte[] buffer = new byte[4096];
            crc.Reset();
            fileStream.Position = 0;

            while (true)
            {
                long pos = fileStream.Position;
                int size = fileStream.Read(buffer, 0, buffer.Length);
                if (size == 0)
                    break;
                if (size < buffer.Length)
                {
                    buffer = new byte[size];
                    fileStream.Position = pos;
                    fileStream.Read(buffer, 0, buffer.Length);
                }
                crc.Update(buffer);
            }

            ZipEntry.Crc = crc.Value;
            return ZipEntry;
        }


        /// <summary>
        /// 将一个文件压缩成另一个文件
        /// CompressionLevel:0-9,9为最佳压缩，值越大速度越慢
        /// BlockSize:每次写入的大小，字节数，一般写4096即可
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="CompressionLevel"></param>
        /// <param name="BlockSize"></param>
        public static void ZipFile(string sourceFileName, string destFileName, int CompressionLevel, int BlockSize)
        {
            #region 如果文件没有找到，则报错
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new System.IO.FileNotFoundException("文件 " + sourceFileName + " 不存在");
            }
            #endregion

            #region 定义流变量
            FileStream sourceFileStream = null;
            FileStream destFileStream = null;
            ZipOutputStream outputStream = null;
            #endregion

            try
            {
                sourceFileStream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read);
                destFileStream = File.Create(destFileName);
                outputStream = new ZipOutputStream(destFileStream);
                outputStream.SetLevel(CompressionLevel);

                outputStream.PutNextEntry(GetEntry(sourceFileName, sourceFileStream));
                WriteFile(sourceFileName, outputStream, sourceFileStream, BlockSize, null);
            }
            finally
            {
                #region 释放流变量
                if (outputStream != null)
                {
                    outputStream.Finish();
                    outputStream.Close();
                }
                if (destFileStream != null)
                    destFileStream.Close();
                if (sourceFileStream != null)
                    sourceFileStream.Close();
                #endregion
            }
        }


        /// <summary>
        /// 将一个目录压缩成为一个文件，不包含子目录中的文件
        /// sourcePath要带 \
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destFileName"></param>
        /// <param name="CompressionLevel"></param>
        /// <param name="BlockSize"></param>
        public static void ZipPath(string sourcePath, string destFileName, int CompressionLevel, int BlockSize)
        {
            #region 定义变量　　得到文件名列表
            string[] filenames = Directory.GetFiles(sourcePath);
            ZipOutputStream outputStream = null;
            #endregion

            try
            {
                outputStream = new ZipOutputStream(File.Create(destFileName));
                outputStream.SetLevel(CompressionLevel); // 0 - store only to 9 - means best compression

                foreach (string fileName in filenames)
                {
                    #region 定义单个文件的文件流
                    FileStream sourceFileStream = null;
                    #endregion

                    try
                    {
                        sourceFileStream = File.OpenRead(fileName);

                        outputStream.PutNextEntry(GetEntry(fileName, sourceFileStream));
                        WriteFile(fileName, outputStream, sourceFileStream, BlockSize, null);
                    }
                    finally
                    {
                        #region 释放单个文件的文件流
                        if (sourceFileStream != null)
                            sourceFileStream.Close();
                        #endregion
                    }
                }
            }
            finally
            {
                #region 释放变量
                if (outputStream != null)
                {
                    outputStream.Finish();
                    outputStream.Close();
                }
                #endregion
            }
        }

        /// <summary>
        /// 解压一个文件到一个目录中
        /// destPath要带 \
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destDirectory"></param>
        /// <param name="BlockSize"></param>
        public static void UnZip(string sourceFileName, string destDirectory, int BlockSize)
        {
            #region 定义输入流
            ZipInputStream inputStream = null;
            #endregion

            try
            {
                Directory.CreateDirectory(destDirectory);         //生成解压目录

                inputStream = new ZipInputStream(File.OpenRead(sourceFileName));

                ZipEntry theEntry;

                #region while循环，处理每个Entry
                while ((theEntry = inputStream.GetNextEntry()) != null)
                {
                    string fileName = destDirectory + theEntry.Name;

                    if (fileName != String.Empty)
                    {
                        #region 定义单个文件的变量
                        FileStream streamWriter = null;
                        #endregion
                        try
                        {
                            #region 解压
                            streamWriter = File.Create(fileName);

                            byte[] data = new byte[BlockSize];
                            while (true)
                            {
                                int size = inputStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                        finally
                        {
                            #region 释放单个文件的变量
                            if (streamWriter != null)
                                streamWriter.Close();
                            #endregion
                        }
                    }
                }
                #endregion

            }
            finally
            {
                #region 释放输入流
                if (inputStream != null)
                    inputStream.Close();
                #endregion
            }
        }

        /// <summary>
        /// 将压缩包中指定的第index个文件解压到一个文件中
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="BlockSize"></param>
        /// <param name="index"></param>
        public static void UpZipFileByIndex(string sourceFileName, string destFileName, int BlockSize, int index)
        {
            #region 定义输入流
            ZipInputStream inputStream = null;
            #endregion

            try
            {
                inputStream = new ZipInputStream(File.OpenRead(sourceFileName));

                ZipEntry theEntry;

                #region while循环，处理每个Entry
                int i = 0;
                while ((theEntry = inputStream.GetNextEntry()) != null)
                {
                    i++;
                    if (i != index)
                        continue;

                    #region 定义单个文件的变量
                    FileStream streamWriter = null;
                    #endregion
                    try
                    {
                        #region 解压
                        streamWriter = File.Create(destFileName);

                        byte[] data = new byte[BlockSize];
                        while (true)
                        {
                            int size = inputStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        #endregion
                    }
                    finally
                    {
                        #region 释放单个文件的变量
                        if (streamWriter != null)
                            streamWriter.Close();
                        #endregion
                    }
                }
                #endregion
            }
            finally
            {
                #region 释放输入流
                if (inputStream != null)
                    inputStream.Close();
                #endregion
            }
        }

    }
}
