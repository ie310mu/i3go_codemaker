/*                     ieDataFile.IEFS_FileInfo
 * 
 * 
 *          类型: 类　
 * 
 *          说明: 记录文件信息，并将其与byte[]互相转换
 * 
 *      使用方法: 1.使用构造函数生成对象
 *                  构造函数有四种
 *                  a.IEFS_FileInfo(string fileName, string fileID, long fileLength)   根据基础文件信息
 *                  b.IEFS_FileInfo(byte[] data)                                       根据byte[]
 *                  c.IEFS_FileInfo(string fileFullName, string fileID)                根据完整文件名和文件ID
 *                  d.IEFS_FileInfo(Stream s, long pos)                                根据流和在流中的起始位置
 *                2.ToBytes()将文件信息转换成byte[]
 *                3.BytesLength返回ToBytes()转换成byte[]后的长度
 *                4.ToString()将文件信息转换成字符串
 * 
 * 
 *      修改记录: 1.
 *  
 *                                                  Created by ie
 *                                                            2008-04-04      
 * 
 * */


/*                     ieDataFile.IEFS_DataFile
 * 
 * 
 *          类型: 类　
 * 
 *          说明: 档案包，以流的形式存储一个个的压缩文件
 * 
 *      使用方法: 1.使用构造函数生成对象
 *                2.AddFile(string sourceFileName, string fileID) 增加文件
 *                3.GetFile(string destFileName, string fileID)   下载文件
 *                4.DeleteFile(string deleteFileID)               删除文件
 *                5.DataTable ListFiles()                         列表所有的文件信息
 * 
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
using System.Windows.Forms;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System.Data;

namespace IE310.Core.IO.Compression
{
    public class IEFS_FileInfo
    {
        #region 变量

        private int _fileNameLength;
        private string _fileName;
        private string _fileID;//_fileID must be a Guid to String.ToUpper()
        private long _fileLength;

        private byte[] _bytes_fileNameLength
        {
            get
            {
                return BitConverter.GetBytes(_fileNameLength);
            }
            set
            {
                _fileNameLength = BitConverter.ToInt32(value, 0);
            }
        }
        private byte[] _bytes_fileName
        {
            get
            {
                return Encoding.Unicode.GetBytes(_fileName);
            }
            set
            {
                _fileName = Encoding.Unicode.GetString(value);
            }
        }
        private byte[] _bytes_fileID
        {
            get
            {
                return Encoding.ASCII.GetBytes(_fileID);
            }
            set
            {
                _fileID = Encoding.ASCII.GetString(value);
            }
        }
        private byte[] _bytes_fileLength
        {
            get
            {
                return BitConverter.GetBytes(_fileLength);
            }
            set
            {
                _fileLength = BitConverter.ToInt64(value, 0);
            }
        }

        public int fileNameLength
        {
            get
            {
                return _fileNameLength;
            }
        }
        public string fileName
        {
            get
            {
                return _fileName;
            }
        }
        public string fielID
        {
            get
            {
                return _fileID;
            }
        }
        public long fileLength
        {
            get
            {
                return _fileLength;
            }
        }


        #endregion


        #region 构造函数

        private void SetInfo(string fileName, string fileID, long fileLength)
        {
            if (fileName == null)
                throw new Exception("文件名不能为空");

            if (fileID == null)
                throw new Exception("文件ID不能为空");

            if (fileID.Length != Guid.NewGuid().ToString().Length)
                throw new Exception("文件ID长度不正确!");

            if (fileLength > int.MaxValue)
                throw new Exception("文件大小不能超过2G");

            _fileNameLength = fileName.Length * 2;  //string is unicode,when use Encoding.Unicode.GetBytes() to byte[], the byte[]'s length = string.length * 2
            _fileName = fileName;
            _fileID = fileID;
            _fileLength = fileLength;
        }
        private void SetInfo(byte[] data)
        {
            if (data == null)
                throw new Exception("empty bytes data");

            if (data.Length < 49)
                throw new Exception("error bytes's Length");


            byte[] tempData = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                tempData[i] = data[i];
            }
            _bytes_fileNameLength = tempData;



            tempData = new byte[_fileNameLength];
            for (int i = 4; i < 4 + _fileNameLength; i++)
            {
                tempData[i - 4] = data[i];
            }
            _bytes_fileName = tempData;



            tempData = new byte[36];
            for (int i = 4 + _fileNameLength; i < 40 + _fileNameLength; i++)
            {
                tempData[i - 4 - _fileNameLength] = data[i];
            }
            _bytes_fileID = tempData;


            tempData = new byte[8];
            for (int i = 40 + _fileNameLength; i < 48 + _fileNameLength; i++)
            {
                tempData[i - 40 - _fileNameLength] = data[i];
            }
            _bytes_fileLength = tempData;
        }

        public IEFS_FileInfo(string fileName, string fileID, long fileLength)
        {
            SetInfo(fileName, fileID, fileLength);
        }

        public IEFS_FileInfo(byte[] data)
        {
            SetInfo(data);
        }

        public IEFS_FileInfo(string fileFullName, string fileID)
        {
            if (!File.Exists(fileFullName))
                throw new Exception("文件" + fileFullName + "不存在");

            long fileLength;
            FileStream s = null;
            try
            {
                s = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                fileLength = s.Length;
            }
            catch (Exception ex)
            {
                throw new Exception("error to get file info\r\n" + ex.Message);
            }
            finally
            {
                if (s != null)
                    s.Close();
            }


            SetInfo(Path.GetFileName(fileFullName), fileID, fileLength);
        }

        public IEFS_FileInfo(Stream s, long pos)
        {
            try
            {
                s.Seek(pos, SeekOrigin.Begin);
                byte[] data = new byte[4];
                s.Read(data, 0, data.Length);
                _bytes_fileNameLength = data;

                s.Seek(pos, SeekOrigin.Begin);
                data = new byte[48 + _fileNameLength];
                s.Read(data, 0, data.Length);
                SetInfo(data);
            }
            catch (Exception ex)
            {
                throw new Exception("error to get file info from stream\r\n" + ex.Message);
            }
        }

        #endregion


        #region 公共方法

        public byte[] ToBytes()
        {
            byte[] data = new byte[48 + _fileNameLength];

            _bytes_fileNameLength.CopyTo(data, 0);
            _bytes_fileName.CopyTo(data, 4);
            _bytes_fileID.CopyTo(data, 4 + _fileNameLength);
            _bytes_fileLength.CopyTo(data, 40 + _fileNameLength);

            return data;
        }

        public int BytesLength
        {
            get
            {
                return 48 + _fileNameLength;
            }
        }

        /// <returns><see cref="T:System.String"></see>，表示当前的 <see cref="T:System.Object"></see>。</returns>
        public override string ToString()
        {
            return _fileNameLength.ToString() + "|" + _fileName + "|" + _fileID + "|" + _fileLength.ToString();
        }

        #endregion
    }





    public class I3DataFile
    {
        private string _fileName;

        

        public I3DataFile(string file)
        {
            _fileName = file;
            CheckFile();
        }

        private void CheckFile()
        {
            if (!File.Exists(_fileName))
            {
                try
                {
                    FileStream destFileStream = new FileStream(_fileName, FileMode.Create);
                    destFileStream.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void CheckTempPath(string tempPath)
        {
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
        }


        private void PutNextEntry(FileStream s, string fileID, string fileName, long fileLength)
        {
            IEFS_FileInfo fileInfo = new IEFS_FileInfo(fileName, fileID, fileLength);
            byte[] data = fileInfo.ToBytes();

            s.Seek(0, SeekOrigin.End);
            s.Write(data, 0, data.Length);
        }
        //fileID是一个GUID
        public bool AddFile(string sourceFileName, string fileID)
        {
            string tempPath = Application.StartupPath + @"\tempzip";
            CheckTempPath(tempPath);

            string destFileName = tempPath + @"\" + Path.GetFileName(sourceFileName) + ".zip";
            I3SharpZip.ZipFile(sourceFileName, destFileName, 9, 4096);

            FileStream dataFileStream = null;
            FileStream destFileStream = null;
            try
            {
                dataFileStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                destFileStream = new FileStream(destFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (dataFileStream.Length + destFileStream.Length > int.MaxValue)
                {
                    //MessageBox.Show("增加此文件后，数据文件大小将超过2G，取消增加此文件!");
                    IEFS_Error.LastErrorMessage = "增加此文件后，数据文件大小将超过2G，取消增加此文件!";
                    return false;
                }
                PutNextEntry(dataFileStream, fileID, Path.GetFileName(sourceFileName), destFileStream.Length);
                byte[] buffer = new byte[4096];
                while (true)
                {
                    int size = destFileStream.Read(buffer, 0, buffer.Length);
                    if (size <= 0)
                        break;
                    dataFileStream.Write(buffer, 0, size);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (dataFileStream != null)
                    dataFileStream.Close();
                if (destFileStream != null)
                    destFileStream.Close();
                File.Delete(destFileName);
            }
        }

        public bool GetFile(string destFileName, string fileID)
        {
            #region 获取文件信息
            long pos = 0;
            IEFS_FileInfo fileInfo = GetFileInfo(fileID, out pos);
            if (fileInfo == null)
                return false;
            #endregion

            #region 得到临时文件名
            string tempPath = Application.StartupPath + @"\tempzip";
            CheckTempPath(tempPath);
            string tempFileName = tempPath + @"\" + fileID + ".zip";
            #endregion

            #region 从数据文件中提取相应文件的压缩包
            FileStream dataFileStream = null;
            FileStream tempFileStream = null;
            try
            {
                dataFileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                tempFileStream = new FileStream(tempFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                byte[] buffer = new byte[4096];
                dataFileStream.Seek(pos + fileInfo.BytesLength, SeekOrigin.Begin);
                while (true)
                {
                    int size = dataFileStream.Read(buffer, 0, buffer.Length);
                    if (size <= 0)
                        break;
                    if (dataFileStream.Position > (pos + fileInfo.BytesLength + fileInfo.fileLength))
                    {
                        int xsize = size;
                        size -= Convert.ToInt32(dataFileStream.Position - pos - fileInfo.BytesLength - fileInfo.fileLength);
                        if (size == 0)
                            break;
                        buffer = new byte[size];
                        dataFileStream.Seek(dataFileStream.Position - xsize, SeekOrigin.Begin);
                        dataFileStream.Read(buffer, 0, buffer.Length);
                        tempFileStream.Write(buffer, 0, buffer.Length);
                        break;
                    }
                    else
                    {
                        tempFileStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                #region 出错处理
                MessageBox.Show(ex.Message);
                return false;
                #endregion
            }
            finally
            {
                #region 释放
                if (dataFileStream != null)
                    dataFileStream.Close();
                if (tempFileStream != null)
                    tempFileStream.Close();
                #endregion
            }
            #endregion

            #region 从压缩包中解压文件
            I3SharpZip.UpZipFileByIndex(tempFileName, destFileName, 4096, 1);
            return true;
            #endregion
        }

        private void CopyDeleteStream(Stream sourceStream, Stream destStream, long pos, long len)
        {
            sourceStream.Position = 0;

            #region 定义缓冲区与变量
            bool delete = false;
            byte[] buffer = new byte[4096];
            #endregion

            #region 生成进度显示窗口
            IEFS_ProcessForm form = IEFS_ProcessForm.CreateMessageProcessDialog();
            form.max = Convert.ToInt32(sourceStream.Length);
            form.message = "正在删除，请稍候....";
            form.SetPosition(0);
            #endregion
            while (true)
            {
                int size = sourceStream.Read(buffer, 0, buffer.Length);
                if ((sourceStream.Position > pos) && (!delete))
                {
                    #region 超过要删除的文件的起始点时的处理
                    int sizex = size;
                    size -= Convert.ToInt32(sourceStream.Position - pos);
                    if (size > 0)
                    {
                        sourceStream.Seek(0 - sizex, SeekOrigin.Current);
                        byte[] tempbuffer = new byte[size];
                        size = sourceStream.Read(tempbuffer, 0, tempbuffer.Length);
                        destStream.Write(tempbuffer, 0, size);
                    }
                    sourceStream.Seek(pos + len, SeekOrigin.Begin);
                    delete = true;
                    #endregion
                }
                else
                {
                    destStream.Write(buffer, 0, size);
                }
                form.SetPosition(Convert.ToInt32(sourceStream.Position));

                if (sourceStream.Position == sourceStream.Length)
                    break;
            }
            form.Dispose();
        }

        public bool DeleteFile(string deleteFileID)
        {
            #region 获取文件信息
            long pos = 0;
            IEFS_FileInfo fileInfo = GetFileInfo(deleteFileID, out pos);
            if (fileInfo == null)
                return false;
            #endregion

            FileStream dataFileStream = null;
            FileStream destStream = null;
            try
            {
                dataFileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                destStream = new FileStream(_fileName + ".move", FileMode.Create, FileAccess.ReadWrite, FileShare.None);

                CopyDeleteStream(dataFileStream, destStream, pos, fileInfo.BytesLength + fileInfo.fileLength);

                return true;
            }
            catch (Exception ex)
            {
                #region 出错处理
                MessageBox.Show(ex.Message);
                return false;
                #endregion
            }
            finally
            {
                #region 释放
                if (destStream != null)
                    destStream.Close();
                if (dataFileStream != null)
                    dataFileStream.Close();
                #endregion

                #region 删除与重命名文件
                File.Delete(_fileName);
                File.Move(_fileName + ".move", _fileName);
                #endregion
            }
        }

        public DataTable ListFiles()
        {
            #region 生成DataTable与其架构
            DataTable dataTable = new DataTable();

            #region FileID
            DataColumn col1 = new DataColumn("FileID");
            col1.DataType = typeof(string);
            col1.MaxLength = 50;
            dataTable.Columns.Add(col1);
            #endregion

            #region FileName
            DataColumn col2 = new DataColumn("FileName");
            col2.DataType = typeof(string);
            col2.MaxLength = 255;
            dataTable.Columns.Add(col2);
            #endregion

            #region FileLength
            DataColumn col3 = new DataColumn("FileLength");
            col3.DataType = typeof(int);
            dataTable.Columns.Add(col3);
            #endregion

            #endregion

            FileStream dataFileStream = null;
            try
            {
                dataFileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                if (dataFileStream.Length == 0)
                    return null;

                long total = 0;
                while (true)
                {
                    IEFS_FileInfo fileInfo = new IEFS_FileInfo(dataFileStream, total);
                    total = total + fileInfo.BytesLength + fileInfo.fileLength;

                    DataRow row = dataTable.NewRow();
                    row.BeginEdit();
                    row["FileID"] = fileInfo.fielID;
                    row["FileName"] = fileInfo.fileName;
                    row["FileLength"] = fileInfo.fileLength;
                    row.EndEdit();
                    dataTable.Rows.Add(row);

                    if (total == dataFileStream.Length)
                        break;
                }
            }
            catch (Exception ex)
            {
                #region 出错处理
                MessageBox.Show(ex.Message);
                return null;
                #endregion
            }
            finally
            {
                #region 释放
                if (dataFileStream != null)
                    dataFileStream.Close();
                #endregion
            }

            return dataTable;
        }

        //pos在数据文件中的起始位置（包含文件头信息)  len 文件头长度+文件长度
        private IEFS_FileInfo GetFileInfo(string searchFileID, out long pos)
        {
            FileStream dataFileStream = null;
            try
            {
                dataFileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                if (dataFileStream.Length == 0)
                {
                    pos = 0;
                    return null;
                }

                long total = 0;
                while (true)
                {
                    IEFS_FileInfo fileInfo = new IEFS_FileInfo(dataFileStream, total);

                    if (fileInfo.fielID == searchFileID)
                    {
                        pos = total;
                        return fileInfo;
                    }

                    total = total + fileInfo.BytesLength + fileInfo.fileLength;

                    if (total == dataFileStream.Length)
                        break;
                }

                pos = 0;
                return null;
            }
            catch (Exception ex)
            {
                #region 出错处理
                MessageBox.Show(ex.Message);
                pos = 0;
                return null;
                #endregion
            }
            finally
            {
                #region 释放
                if (dataFileStream != null)
                    dataFileStream.Close();
                #endregion
            }
        }
    }
}
