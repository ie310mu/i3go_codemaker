using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using IE310.Core.Utils;
using IE310.Core.Progressing;

namespace IE310.Core.Components
{
    public partial class I3SharpZip : Component
    {
        public I3SharpZip()
        {
            InitializeComponent();
        }

        private string fileName;
        /// <summary>
        /// zip压缩包的文件名
        /// 设置时将对文件进行检查，
        /// 如果文件已经存在，则检查其格式，并设置fileOK
        /// 如果文件不存在，则生成文件，并设置fileOK
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                CheckFile();
            }
        }

        private string passWord = null;
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return passWord;
            }
            set
            {
                passWord = value;
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

        /// <summary>
        /// 以遍历所有ZipEntry的方式，检查文件格式是否正确
        /// 
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// 
        /// </summary>
        /// <returns></returns>
        private void CheckFile()
        {
            fileInfoList.Clear();

            #region 文件不存在
            if (!File.Exists(fileName))
            {
                fileOK = true;
                fileExists = false;
                return;
            }
            #endregion

            #region 文件存在，打开
            fileExists = true;
            ZipInputStream inputStream = null;
            try
            {
                inputStream = new ZipInputStream(File.OpenRead(fileName));
                if (!string.IsNullOrEmpty(passWord))
                {
                    inputStream.Password = passWord;
                }
            }
            catch (Exception ex)
            {
                this.errorMessage = ex.Message ;
                fileOK = false;
                return;
            }
            #endregion

            #region 开始获取ZipEntry
            try
            {
                ZipEntry theEntry;
                try
                {
                    while ((theEntry = inputStream.GetNextEntry()) != null)
                    {
                        fileInfoList.Add(new I3SharpZipFileInfo(theEntry.Name, null, theEntry.Size, I3SZFileInfoMode.szimNormal));
                    }
                }
                catch (Exception ex)
                {
                    this.errorMessage = ex.Message ;
                    fileOK = false;
                    return;
                }
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            #endregion

            fileOK = true;
        }

        /// <summary>
        /// 内部使用，记录文件是否存在
        /// </summary>
        private bool fileExists = false;
        private bool fileOK = false;
        /// <summary>
        /// 文件已经存在时，返回文件格式是否正确
        /// </summary>
        public bool FileOK
        {
            get
            {
                return fileOK;
            }
        }

        private int blockSize = 4096;
        /// <summary>
        /// 压缩或者解压缩时的缓冲大小，默认值为4096
        /// </summary>
        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize = value;
            }
        }

        private int zipLevel = 9;
        /// <summary>
        /// 压缩级别，0-9，9压缩比率最高，但速度最慢
        /// </summary>
        public int ZipLevel
        {
            get
            {
                return zipLevel;
            }
            set
            {
                zipLevel = value;
            }
        }


        private IProgressReporter singleFileReporter;
        /// <summary>
        /// 解压或者压缩单个文件时的事件
        /// 事件的参数中，positoin表示当前位置，max表示文件大小，message表示当前信息，如：“正在解压xxx到xxx”或“正在压缩xxx”
        /// </summary>
        public IProgressReporter SingleFileReporter
        {
            get
            {
                return singleFileReporter;
            }
            set
            {
                singleFileReporter = value;
            }
        }
        private void OnSingleFileProcessReport(long max, long position, string message)
        {
            if (!this.DesignMode)
            {
                if (SingleFileReporter != null)
                {
                    SingleFileReporter.ChangeProgress(new I3ProgressingEventArgs(0, max, position, message));
                }
            }
        }

        /// <summary>
        /// 根据文件信息从压缩包中获取单个文件，需要指定FullName  
        /// </summary>
        /// <param name="aFileInfo"></param>
        /// <returns></returns>
        public I3MsgInfo UnCompressSingleFile(I3SharpZipFileInfo aFileInfo)
        {
            #region 检查文件名与上级目录
            if (string.IsNullOrEmpty(aFileInfo.FullName))
            {
                return new I3MsgInfo(false, "未设置目的路径！");
            }
            I3MsgInfo msg=I3DirectoryUtil.CreateDirectoryByFileName(aFileInfo.FullName);
            if (!msg.State)
            {
                return msg;
            }
            string message = "正在解压文件" + aFileInfo.FileName + "到" + aFileInfo.FullName;
            #endregion
            #region 检查是否是目录  文件大小为0且文件名最后一个字符是/表示是目录            
            if ((aFileInfo.FileSize.Equals(0) && (I3StringUtil.SubString(aFileInfo.FileName, aFileInfo.FileName.Length - 1).Equals("/"))))
            {
                return I3MsgInfo.Default;
            }
            #endregion

            bool result = false;
            ZipInputStream inputStream = null;
            try
            {
                inputStream = new ZipInputStream(File.OpenRead(fileName));
                if (!string.IsNullOrEmpty(passWord))
                {
                    inputStream.Password = passWord;
                }
                ZipEntry theEntry;

                while ((theEntry = inputStream.GetNextEntry()) != null)
                {
                    #region 判断文件名是否为要解压的文件 
                    if (!theEntry.Name.Equals(aFileInfo.FileName))
                    {
                        continue;
                    }
                    #endregion

                    #region 定义单个文件的变量
                    FileStream streamWriter = null;
                    #endregion
                    try
                    {
                        #region 解压
                        streamWriter = File.Create(aFileInfo.FullName);

                        byte[] data = new byte[blockSize];
                        long longsize = 0;
                        while (true)
                        {
                            int size = inputStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                                longsize += size;
                                OnSingleFileProcessReport(aFileInfo.FileSize, longsize, message);
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
                    result = true;
                }
            }
            #region 错误处理
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
            #endregion
            #region 释放inputStream
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            #endregion

            return new I3MsgInfo(result, "");
        }

        private IProgressReporter mutiFileReporter;
        /// <summary>
        /// 解压或者压缩多个文件时的事件
        /// 事件的参数中，positoin当前操作第几个文件，max表示文件个数，message表示当前信息，如：“正在解压xxx到xxx”或“正在压缩xxx”
        /// </summary>
        public IProgressReporter MutiFileReporter
        {
            get
            {
                return mutiFileReporter;
            }
            set
            {
                mutiFileReporter = value;
            }
        }
        private void OnMutiFileProcessReport(long max, long position, string message)
        {
            if (!this.DesignMode)
            {
                if (MutiFileReporter != null)
                {
                    MutiFileReporter.ChangeProgress(new I3ProgressingEventArgs(0, max, position, message));
                }
            }
        }

        private IProgressReporter totalBytesReporter;
        /// <summary>
        /// 解压或者压缩多个文件时的事件
        /// 事件的参数中，positoin当前总数据大小，max表示总数据大小，message表示当前信息，如：“正在解压xxx到xxx”或“正在压缩xxx”
        /// </summary>
        public IProgressReporter TotalBytesReporter
        {
            get
            {
                return totalBytesReporter;
            }
            set
            {
                totalBytesReporter = value;
            }
        }
        private void OnTotalBytesProcessReport(long max, long position, string message)
        {
            if (!this.DesignMode)
            {
                if (TotalBytesReporter != null)
                {
                    TotalBytesReporter.ChangeProgress(new I3ProgressingEventArgs(0, max, position, message));
                }
            }
        }

        /// <summary>
        /// 解压所有文件到指定的目录
        /// 
        /// checkFileIsNormal:解压时，是否在fileInfoList中检查其状态为normal，为normal时才解压
        /// 此功能用于更换或者删除文件时，同时，为normal状态的fileinfo.fullName将被更新，指示现在被解压到了哪里
        /// 
        /// </summary>
        /// <param name="aNewPath"></param>
        /// <returns></returns>
        public I3MsgInfo UnCompressAllFile(string aNewPath, bool checkFileIsNormal)
        {
            if (!fileExists)
            {
                return I3MsgInfo.Default;
            }

            I3MsgInfo msg;
            #region 定义变量
            string message = "";
            ZipInputStream inputStream = null;
            int normalFileCount = GetNormalFileCount();
            long totalNormalFileSize = GetTotalNormalFileSize();//所有需要解压的文件的字节数
            long passFileSize = 0;//已经解压掉的文件的字节数
            long totalPosition = 0;//总共解压到的字节数
            #endregion

            try
            {
                inputStream = new ZipInputStream(File.OpenRead(fileName));
                if (!string.IsNullOrEmpty(passWord))
                {
                    inputStream.Password = passWord;
                }
                ZipEntry theEntry;

                int fileindex = 0;
                while ((theEntry = inputStream.GetNextEntry()) != null)
                {
                    I3SharpZipFileInfo normalInfo = null;
                    #region 检查文件是否为normal状态
                    if (checkFileIsNormal)
                    {
                        bool isNormal = false;
                        foreach (I3SharpZipFileInfo info in fileInfoList)
                        {
                            if (info.FileName.Equals(theEntry.Name))
                            {
                                isNormal = info.Mode == I3SZFileInfoMode.szimNormal;
                                normalInfo = info;
                                break;
                            }
                        }

                        if (!isNormal)
                        {
                            continue;
                        }
                    }
                    #endregion
                    #region 获取目的文件名，并生成目录 normalInfo不为空时，更新FullName
                    string aNewFileName = Path.Combine(aNewPath, theEntry.Name);
                    if (normalInfo != null)
                    {
                        normalInfo.FullName = aNewFileName;
                    }
                    msg = I3DirectoryUtil.CreateDirectoryByFileName(aNewFileName);
                    if (!msg.State)
                    {
                        return msg;
                    }
                    #endregion
                    #region 发送文件个数进度信息
                    message = "正在解压文件" + theEntry.Name + "到" + aNewFileName;
                    fileindex++;
                    OnMutiFileProcessReport(normalFileCount, fileindex, message);
                    #endregion
                    #region 如果是目录则不需要解压  文件大小为0且文件名最后一个字符是/表示是目录                    
                    if ((theEntry.Size.Equals(0) && (I3StringUtil.SubString(theEntry.Name, theEntry.Name.Length - 1).Equals("/"))))
                    {
                        continue;
                    }
                    #endregion

                    #region 定义单个文件的变量
                    FileStream streamWriter = null;
                    #endregion
                    try
                    {
                        #region 解压
                        streamWriter = File.Create(aNewFileName);

                        byte[] data = new byte[blockSize];
                        long longsize = 0;
                        while (true)
                        {
                            int size = inputStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                                longsize += size;
                                OnSingleFileProcessReport(theEntry.Size, longsize, message);
                                totalPosition = passFileSize + longsize;
                                OnTotalBytesProcessReport(totalNormalFileSize, totalPosition, message);
                            }
                            else
                            {
                                break;
                            }
                        }
                        passFileSize += longsize;
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

                return I3MsgInfo.Default;
            }
            #region 错误处理
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
            #endregion
            #region 释放inputStream
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            #endregion
        }

        private List<I3SharpZipFileInfo> fileInfoList = new List<I3SharpZipFileInfo>();
        /// <summary>
        /// 文件列表信息
        /// </summary>
        public List<I3SharpZipFileInfo> FileInfoList
        {
            get
            {
                return fileInfoList;
            }
        }

        /// <summary>
        /// 返回fileInfoList中，记录类型为normal的所有文件的总大小
        /// </summary>
        public long GetTotalNormalFileSize()
        {
            long totalSize = 0;
            foreach (I3SharpZipFileInfo info in fileInfoList)
            {
                if (info.Mode == I3SZFileInfoMode.szimNormal)
                {
                    totalSize += info.FileSize;
                }
            }

            return totalSize;
        }

        /// <summary>
        /// 返回fileInfoList中，记录类型为normal的所有文件的总个数
        /// </summary>
        public int GetNormalFileCount()
        {
            int count = 0;
            foreach (I3SharpZipFileInfo info in fileInfoList)
            {
                if (info.Mode == I3SZFileInfoMode.szimNormal)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 返回fileInfoList中，记录类型为Change和New的所有文件的总大小
        /// </summary>
        public long GetTotalAddFileSize()
        {
            long totalSize = 0;
            foreach (I3SharpZipFileInfo info in fileInfoList)
            {
                if ((info.Mode == I3SZFileInfoMode.szimNew) || (info.Mode == I3SZFileInfoMode.szimChange))
                {
                    totalSize += info.FileSize;
                }
            }

            return totalSize;
        }


        /// <summary>
        /// 返回fileInfoList中，记录类型为normal的所有文件的总个数
        /// </summary>
        public int GetAddFileCount()
        {
            int count = 0;
            foreach (I3SharpZipFileInfo info in fileInfoList)
            {
                if ((info.Mode == I3SZFileInfoMode.szimNew) || (info.Mode == I3SZFileInfoMode.szimChange))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 从文件名和文件流得到一个Entry
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private ZipEntry GetEntry(string aFileName, string aFullName)
        {
            ZipEntry zipEntry = new ZipEntry(aFileName);
            zipEntry.DateTime = DateTime.Now;

            //目录            
            if (I3StringUtil.SubString(aFileName, aFileName.Length - 1).Equals("/"))
            {
                zipEntry.Size = 0;
                //zipEntry.Crc = 0;

                return zipEntry;
            }

            using (FileStream fileStream = File.Open(aFullName, FileMode.Open))
            {
                
                zipEntry.Size = fileStream.Length;

                /*Crc32 crc = new Crc32();
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
                    Application.DoEvents();
                }

                zipEntry.Crc = crc.Value;*/
                return zipEntry;
            }
        }
        

        /// <summary>
        /// 根据fileInfoList的设置，生成新的压缩包文件
        /// 
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// 
        /// </summary>
        /// <returns></returns>
        public I3MsgInfo Flush()
        {
            #region 定义变量
            string message;
            ZipOutputStream outputStream = null;
            int addFileCount = GetAddFileCount() + GetNormalFileCount();
            long totalAddFileSize = GetTotalAddFileSize() + GetTotalNormalFileSize();//所有需要压缩的文件的字节数
            long passFileSize = 0;//已经压缩的文件的字节数
            long totalPosition = 0;//总共压缩到的字节数
            #endregion

            #region 检查临时文件与临时目录
            string tmpFile = fileName + ".tmpsharpzip";
            if (!I3FileUtil.CheckFileNotExists(tmpFile))
            {
                return new I3MsgInfo(false, "");
            }
            string tmpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, I3DateTimeUtil.ConvertDateTimeToLongString(DateTime.Now));
            I3MsgInfo msg=I3DirectoryUtil.CheckAndClearDirctory(tmpDir);
            if (!msg.State)
            {
                return msg;
            }
            #endregion

            try
            {
                #region 解压所有不需要替换或者删除的文件到临时目录
                msg = UnCompressAllFile(tmpDir, true);
                if (!msg.State)
                {
                    return msg;
                }
                #endregion

                #region 开始压缩状态为normal、New、Change三种状态的文件到临时文件中
                try
                {
                    #region 压缩
                    outputStream = new ZipOutputStream(File.Create(tmpFile));
                    outputStream.SetLevel(zipLevel);
                    if (!string.IsNullOrEmpty(passWord))
                    {
                        outputStream.Password = passWord;
                    }

                    int fileindex = 0;
                    foreach (I3SharpZipFileInfo info in fileInfoList)
                    {
                        #region 判断文件是否要参与压缩
                        if (info.Mode == I3SZFileInfoMode.szimDelete)
                        {
                            continue;
                        }
                        #endregion

                        #region 发送文件个数进度信息
                        message = "正在压缩文件\"" + info.FileName + "\"";
                        fileindex++;
                        OnMutiFileProcessReport(addFileCount, fileindex, message);
                        #endregion

                        #region 写入ZipEntry
                        //outputStream.PutNextEntry(GetEntry(info.FileName, info.FullName));
                        //自己在ZipEntry中写入CRC信息，读取时会报Eof of Header的错误
                        //在网上查找资料后发现，新版SharpZip.dll已经更改，不再需要自己写入CRC等信息
                        outputStream.PutNextEntry(new ZipEntry(info.FileName));
                        info.Mode = I3SZFileInfoMode.szimNormal;
                        if (info.FileSize == 0)
                        {
                            continue;
                        }
                        #endregion

                        FileStream sourceFileStream = null;
                        try
                        {
                            #region 将文件写入压缩流
                            sourceFileStream = File.OpenRead(info.FullName);
                            sourceFileStream.Position = 0;
                            //IEFS_Stream.WriteStreamToStream(null, sourceFileStream, outputStream, blockSize, null);
                            byte[] data = new byte[blockSize];
                            long longsize = 0;
                            while (true)
                            {
                                int size = sourceFileStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    outputStream.Write(data, 0, size);
                                    longsize += size;
                                    OnSingleFileProcessReport(info.FileSize, longsize, message);
                                    totalPosition = passFileSize + longsize;
                                    OnTotalBytesProcessReport(totalAddFileSize, totalPosition, message);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            passFileSize += longsize;
                            #endregion
                        }
                        finally
                        {
                            #region 释放单个文件的文件流
                            if (sourceFileStream != null)
                                sourceFileStream.Close();
                            #endregion
                        }
                    }
                    #endregion
                }
                #region 错误处理
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }
                #endregion
                #region 释放变量  删除临时目录
                finally
                {
                    if (outputStream != null)
                    {
                        outputStream.Finish();
                        outputStream.Close();
                    }
                    I3DirectoryUtil.DeleteDirctory(tmpDir);
                }
                #endregion
                #endregion

                #region 替换原始文件
                if (!I3FileUtil.CheckFileNotExists(fileName))
                {
                    return new I3MsgInfo(false, "");
                }
                return I3FileUtil.MoveFile(tmpFile, fileName, true);
                #endregion
            }
            finally
            {
                I3DirectoryUtil.DeleteDirctory(tmpDir);
                I3FileUtil.CheckFileNotExists(tmpFile);
            }
        }

        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aDestFileName"></param>
        /// <returns></returns>
        public I3MsgInfo CompressASingleFile(string aFileName, string aDestFileName)
        {
            if (!I3FileUtil.CheckFileNotExists(aDestFileName))
            {
                return new I3MsgInfo(false, "");
            }

            FileName = aDestFileName;
            I3SharpZipFileInfo fileInfo = new I3SharpZipFileInfo(Path.GetFileName(aFileName), 
                aFileName, I3FileUtil.GetFileSize(aFileName), I3SZFileInfoMode.szimNew);
            fileInfoList.Add(fileInfo);

            I3MsgInfo msg = Flush();
            if (!msg.State)
            {
                return msg;
            }

            FileName = aDestFileName;
            return new I3MsgInfo(fileExists && fileOK, "");
        }

        /// <summary>
        /// 内部使用，递归添加一个目录中的文件
        /// </summary>
        private void AddDirFileToFileList(string aDir, string aBaseDir)
        {
            //增加目录本身
            if (!aDir.Equals(aBaseDir))
            {
                I3SharpZipFileInfo fileInfo = new I3SharpZipFileInfo(aDir, aBaseDir, true, I3SZFileInfoMode.szimNew);
                fileInfoList.Add(fileInfo);
            }

            //增加子文件
            foreach (string aFileName in Directory.GetFiles(aDir))
            {
                I3SharpZipFileInfo fileInfo = new I3SharpZipFileInfo(aFileName, aBaseDir, false, I3SZFileInfoMode.szimNew);
                fileInfoList.Add(fileInfo);
            }

            //增加子目录
            foreach (string aDirName in Directory.GetDirectories(aDir))
            {
                AddDirFileToFileList(aDirName, aBaseDir);
            }
        }

        /// <summary>
        /// 压缩一个目录下的所有文件，包含子目录中的文件
        /// aFilter为空时，压缩所有文件  指定文件时，其值类似于  .txt
        /// </summary>
        /// <param name="aDir"></param>
        /// <param name="aDestFileName"></param>
        /// <returns></returns>
        public I3MsgInfo CompressADir(string aDir, string aDestFileName)
        {
            if (!I3FileUtil.CheckFileNotExists(aDestFileName))
            {
                return new I3MsgInfo(false, "");
            }

            FileName = aDestFileName;
            AddDirFileToFileList(aDir, aDir);

            I3MsgInfo msg = Flush();
            if (!msg.State)
            {
                return msg;
            }

            FileName = aDestFileName;
            return new I3MsgInfo(fileExists && fileOK, "");
        }
    }




    /// <summary>
    /// 用于记录文件信息
    /// </summary>
    public class I3SharpZipFileInfo
    {
        private string fileName;
        /// <summary>
        /// 短文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        private string fullName;
        /// <summary>
        /// 完整文件名
        /// </summary>
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }

        private long fileSize;
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize
        {
            get
            {
                return fileSize;
            }
            set
            {
                fileSize = value;
            }
        }

        private I3SZFileInfoMode mode;
        /// <summary>
        /// 文件信息中，文件的模式
        /// szimNormal：zip压缩包中已经有的文件
        /// szimNew:需要新增加的文件
        /// szimChange:需要修改的文件
        /// szimDelete:需要删除的文件 
        /// </summary>
        public I3SZFileInfoMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public I3SharpZipFileInfo(string aFileName, string aFullName, long aFileSize, I3SZFileInfoMode aMode)
        {
            fileName = aFileName;
            fullName = aFullName;
            fileSize = aFileSize;
            mode = aMode;
        }

        /// <summary>
        /// 构造函数，根据完整文件名生成适应于ZipEntry的文件名
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aBasePath"></param>
        /// <param name="isDirctory"></param>
        public I3SharpZipFileInfo(string aFullName, string aBaseDir, bool isDirctory, I3SZFileInfoMode aMode)
        {
            string aFileName;
            if (string.IsNullOrEmpty(aBaseDir))
            {
                aFileName = Path.GetFileName(aFullName);
            }
            else
            {
                aBaseDir = I3DirectoryUtil.CheckDirctoryLastChar(aBaseDir);
                aFileName = aFullName.Replace(aBaseDir, string.Empty);
            }

            if (isDirctory)
            {
                aFileName = I3DirectoryUtil.CheckDirctoryLastChar(aFileName);
            }

            aFileName = aFileName.Replace(@"\", "/");

            if (isDirctory)
            {
                fileName = aFileName;
                fullName = aFullName;
                fileSize = 0;
                mode = aMode;
            }
            else
            {
                using (FileStream fileStream = new FileStream(aFullName, FileMode.Open, FileAccess.Read))
                {
                    fileName = aFileName;
                    fullName = aFullName;
                    fileSize = fileStream.Length;
                    mode = aMode;
                }
            }
        }

        public override string ToString()
        {
            string aStr = "";
            aStr = "文 件 名：" + fileName + "\r\n"
                 + "文件全名：" + fullName + "\r\n"
                 + "文件大小：" + fileSize.ToString() + "\r\n"
                 + "模    式：" + mode.ToString();
            return aStr;
        }
    }

    /// <summary>
    /// 文件信息中，文件的模式
    /// szimNormal：zip压缩包中已经有的文件
    /// szimNew:需要新增加的文件
    /// szimChange:需要修改的文件
    /// szimDelete:需要删除的文件 
    /// </summary>
    public enum I3SZFileInfoMode
    {
        szimNormal = 0,
        szimNew = 1,
        szimChange = 2,
        szimDelete = 3
    }
}

