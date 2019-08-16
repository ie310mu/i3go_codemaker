using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.IO;
using IE310.Core.Components;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace IE310.Core.Utils
{
    public static class I3DBUtil
    {
        /// <summary>
        /// 相当于delphi中的QuotedStr,用于给字符串两边加',并且原有的'换成两个'
        /// 如:  rt'yu    ----->      'rt''yu'
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string QuotedStr(string value)
        {
            if (value == null)
                return value;

            return "'" + value.Replace("'", "''") + "'";
        }

        /// <summary>
        /// 生成用OLEDB方式连接sqlserver时的连接字符串
        /// 获取连接字符串的示例，可以从  www.connectionstrings.com  获取
        /// </summary>
        /// <param name="aServerName"></param>
        /// <param name="aDataBaseName"></param>
        /// <param name="aUserName"></param>
        /// <param name="aPassWord"></param>
        /// <param name="aUseWindows"></param>
        /// <returns></returns>
        public static string CreateSQLOLEDBConnectionString(string aServerName, string aDataBaseName, string aUserName, string aPassWord, bool aUseWindows)
        {
            string aConnectionString = "";

            if (aUseWindows)
            {
                aConnectionString = "Provider=SQLOLEDB;Data Source="
                                  + aServerName
                                  + ";Integrated Security=SSPI;Initial Catalog="
                                  + aDataBaseName;
            }
            else
            {
                aConnectionString = "Provider=SQLOLEDB;Data Source="
                                  + aServerName
                                  + ";Persist Security Info=True;User ID="
                                  + aUserName
                                  + ";PassWord="
                                  + aPassWord
                                  + ";Initial Catalog="
                                  + aDataBaseName;
            }

            return aConnectionString;
        }

        /// <summary>
        /// 生成用OLEDB方式连接Access时的连接字符串
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aPassWord"></param>
        /// <returns></returns>
        public static string CreateAccessOLEDBConnectionString(string aFileName, string aPassWord)
        {
            string aConnectionString = "";

            if ((aPassWord == null) || (aPassWord.ToString().Equals("")))
            {
                aConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                  + aFileName
                                  + ";Persist Security Info=False";
            }
            else
            {
                aConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                  + aFileName
                                  + ";Persist Security Info=False;Jet OLEDB:Database Password="
                                  + aPassWord;
            }

            return aConnectionString;
        }

        /// <summary>
        /// 生成用OLEDB方式连接UDL文件的连接字符串
        /// </summary>
        /// <param name="aFileName"></param>
        /// <returns></returns>
        public static string CreateUDLOLEDBConnectionString(string aFileName)
        {
            string aConnectionString = "";

            aConnectionString = "FILE NAME=" + aFileName;

            return aConnectionString;
        }

        /// <summary>
        /// 生成用于通过SQLClient连接SQLServer的连接字符串，VS2010上添加数据源时说只能连接SQL2005以上版本，但连接SQL2000也可以，不知道会不会有其他影响
        /// </summary>
        /// <param name="aServerName"></param>
        /// <param name="aDataBaseName"></param>
        /// <param name="aUserName"></param>
        /// <param name="aPassWord"></param>
        /// <param name="aUseWindows"></param>
        /// <returns></returns>
        public static string CreateSQLConnectionString(string aServerName, string aDataBaseName, string aUserName, string aPassWord, bool aUseWindows)
        {
            string aConnectionString = "";

            if (aUseWindows)
            {
                aConnectionString = "Data Source="
                                  + aServerName
                                  + ";Initial Catalog="
                                  + aDataBaseName
                                  + ";Integrated Security=True";
            }
            else
            {
                aConnectionString = "Data Source="
                                  + aServerName
                                  + ";Initial Catalog="
                                  + aDataBaseName
                                  + ";User Id="
                                  + aUserName
                                  + ";Password="
                                  + aPassWord;
            }

            return aConnectionString;
        }


        /// <summary>
        /// 将文件经过rar压缩后，写入数据库字段中
        /// 写入数据库中的实际是一个rar压缩文件，里面只有一个文件，文件名为 IEFS_Rar.tmp
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static I3MsgInfo WriteFileToDataRowAfterRar(DataRow row, string fieldName, string sourceFileName, bool deleteSource)
        {
            if (!File.Exists(sourceFileName))
            {
                return new I3MsgInfo(false, "文件不存在！文件名：" + sourceFileName);
            }

            string tmpDir = I3DirectoryUtil.GetAppTmpDir();
            string tmpRarFileName = Path.Combine(tmpDir, I3DateTimeUtil.ConvertDateTimeToLongString(DateTime.Now) + ".rar");
            string tmpSourceFileName = Path.Combine(tmpDir, I3DateTimeUtil.ConvertDateTimeToLongString(DateTime.Now));
            tmpSourceFileName = Path.Combine(tmpSourceFileName, "IEFS_Rar.tmp");

            #region 生成临时目录并删除临时文件
            if (!I3FileUtil.CheckFileNotExists(tmpRarFileName))
            {
                return new I3MsgInfo(false, "临时文件删除失败");
            }
            I3MsgInfo msg = I3DirectoryUtil.CreateDirctory(tmpDir);
            if (!msg.State)
            {
                return msg;
            }
            if (!I3FileUtil.CheckFileNotExists(tmpSourceFileName))
            {
                return new I3MsgInfo(false, "临时源文件删除失败");
            }
            msg = I3DirectoryUtil.CreateDirectoryByFileName(tmpSourceFileName);
            if (!msg.State)
            {
                return msg;
            }
            msg = I3FileUtil.MoveFile(sourceFileName, tmpSourceFileName, deleteSource);
            if (!msg.State)
            {
                return msg;
            }
            #endregion
            try
            {
                #region 压缩
                using (I3Rar rar = new I3Rar())
                {
                    msg = rar.CompressASingleFile(tmpSourceFileName, tmpRarFileName);
                    if (!msg.State)
                    {
                        return msg;
                    }
                }
                if (!File.Exists(tmpRarFileName))
                {
                    return new I3MsgInfo(false, "未知原因，压缩失败！文件名：" + tmpRarFileName);
                }
                #endregion

                #region 写入
                using (FileStream stream = File.OpenRead(tmpRarFileName))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Position = 0;
                    int readSize = stream.Read(buffer, 0, buffer.Length);
                    if (readSize != buffer.Length)
                    {
                        return new I3MsgInfo(false, "数据缓冲读取失败！目标长度：" + buffer.Length.ToString() + "，读取长度：" + readSize.ToString());
                    }

                    try
                    {
                        row.BeginEdit();
                        row[fieldName] = buffer;
                        row.EndEdit();

                        stream.Close();
                        return I3MsgInfo.Default;
                    }
                    catch (Exception ex)
                    {
                        return new I3MsgInfo(false, ex.Message, ex);
                    }
                }
                #endregion
            }
            finally
            {
                #region 删除临时文件
                I3FileUtil.CheckFileNotExists(tmpRarFileName);
                I3DirectoryUtil.DeleteDirctory(Path.GetDirectoryName(tmpSourceFileName));
                #endregion
            }
        }

        /// <summary>
        /// 从数据库字段中读取文件并保存到目的文件，
        /// 注意，此字段必须是由WriteFileToDataRow写入！
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        public static I3MsgInfo ReadFileFromDataRowBeforeRar(DataRow row, string fieldName, string destFileName)
        {
            #region 初始化临时文件
            string tmpDir = I3DirectoryUtil.GetAppTmpDir();
            string tmpRarFileName = Path.Combine(tmpDir, I3DateTimeUtil.ConvertDateTimeToLongString(DateTime.Now) + ".rar");

            I3MsgInfo msg = I3DirectoryUtil.CreateDirctory(tmpDir);
            if (!msg.State)
            {
                return msg;
            }
            if (!I3FileUtil.CheckFileNotExists(tmpRarFileName))
            {
                return new I3MsgInfo(false, "");
            }
            #endregion

            try
            {
                #region 从数据库读取文件到临时文件
                try
                {
                    byte[] buffer = (byte[])row[fieldName];
                    if (buffer.Length == 0)
                    {
                        return new I3MsgInfo(false, "数据字段长度为0!");
                    }

                    using (FileStream stream = new FileStream(tmpRarFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        stream.Write(buffer, 0, buffer.Length);

                        stream.Flush();
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }
                #endregion

                #region 解压
                using (I3Rar rar = new I3Rar())
                {
                    msg = rar.UnCompressASingleFile(tmpRarFileName, "IEFS_Rar.tmp", destFileName);
                    if (!msg.State)
                    {
                        return msg;
                    }
                }
                #endregion

                return I3MsgInfo.Default;
            }
            finally
            {
                #region 删除临时Rar文件
                I3FileUtil.CheckFileNotExists(tmpRarFileName);
                #endregion
            }
        }



        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        public static void SetPreKey(DataTable dataTable, string fieldName)
        {
            DataColumn[] cols = new DataColumn[1];
            cols[0] = dataTable.Columns[fieldName];
            dataTable.PrimaryKey = cols;
        }

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        public static void CheckTable(DataSet dataSet, string tableName)
        {
            bool have;
            DataTable testdataTable = null;
            try
            {
                testdataTable = dataSet.Tables[tableName];
                have = testdataTable != null;
            }
            catch (Exception)
            {
                have = false;
            }

            if ((!have) || (testdataTable == null))
            {
                DataTable dataTable = new DataTable(tableName);
                dataSet.Tables.Add(dataTable);
            }
        }

        /// <summary>
        /// 检查字段是否存在 
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="fieldLength"></param>
        public static void CheckField(DataTable dataTable, string fieldName, string caption, I3DataTypeEnum dataType, int fieldLength)
        {
            bool have;
            DataColumn testdataColumn = null;
            try
            {
                testdataColumn = dataTable.Columns[fieldName];
                have = testdataColumn != null;
            }
            catch (Exception)
            {
                have = false;
            }


            if (!have)
            {
                DataColumn dataColumn = new DataColumn(fieldName);
                CheckFieldInfo(dataColumn, caption, dataType, fieldLength);
                dataTable.Columns.Add(dataColumn);
            }
            else
            {
                testdataColumn.Caption = caption;
                //主键不可修改
                foreach (DataColumn col in dataTable.PrimaryKey)
                {
                    if (col == testdataColumn)
                    {
                        return;
                    }
                }

                //数据类型和长度相同不用修改
                if (CheckFieldInfoEqual(testdataColumn, dataType, fieldLength))
                {
                    return;
                }

                DataColumn dataColumn = new DataColumn(fieldName + "2");
                CheckFieldInfo(dataColumn, caption, dataType, fieldLength);
                dataTable.Columns.Add(dataColumn);
                string errString = "";
                if (!CopyFieldValue(dataTable, testdataColumn, dataColumn, ref errString))
                {
                    dataTable.Columns.Remove(dataColumn);
                    if (errString.Length > 1000)
                    {
                        errString = I3StringUtil.SubString(errString, 0, 1000) + "\r\n.........";
                    }
                    errString = "修改字段" + fieldName + "时出现错误！错误信息：\r\n" + errString;
                    MessageBox.Show(errString, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dataTable.Columns.Remove(testdataColumn);
                    dataColumn.ColumnName = fieldName;
                }
            }
        }

        private static bool CopyFieldValue(DataTable dataTable, DataColumn sourceField, DataColumn destField, ref string errMessage)
        {
            bool result = true;
            errMessage = "";
            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    row.BeginEdit();
                    row[destField] = row[sourceField];
                    row.EndEdit();
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + ex.Message;
                    result = false;
                }
            }

            return result;
        }

        private static void CheckFieldInfo(DataColumn dataColumn, string caption, I3DataTypeEnum dataType, int fieldLength)
        {
            switch (dataType)
            {
                case I3DataTypeEnum.sdtString:
                    dataColumn.DataType = typeof(string);
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = caption;
                    dataColumn.MaxLength = fieldLength;
                    break;
                case I3DataTypeEnum.sdtInt:
                    dataColumn.DataType = typeof(int);
                    dataColumn.DefaultValue = 0;
                    dataColumn.Caption = caption;
                    break;
                case I3DataTypeEnum.sdtDateTime:
                    dataColumn.DataType = typeof(DateTime);
                    dataColumn.DefaultValue = new DateTime(2000, 1, 1);
                    dataColumn.Caption = caption;
                    break;
                case I3DataTypeEnum.sdtFloat:
                    dataColumn.DataType = typeof(double);
                    dataColumn.DefaultValue = 0;
                    dataColumn.Caption = caption;
                    break;
                case I3DataTypeEnum.sdtBoolean:
                    dataColumn.DataType = typeof(bool);
                    dataColumn.DefaultValue = false;
                    dataColumn.Caption = caption;
                    break;
                case I3DataTypeEnum.sdtStream:
                    dataColumn.DataType = typeof(byte[]);
                    dataColumn.DefaultValue = null;
                    dataColumn.Caption = caption;
                    break;
                default:
                    dataColumn.DataType = typeof(string);
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = caption;
                    dataColumn.MaxLength = 50;
                    break;
            }
        }



        ///检查DataColumn的类型的长度是否与目标类型、长度一致  一致时返回true
        private static bool CheckFieldInfoEqual(DataColumn column, I3DataTypeEnum dataType, int fieldLength)
        {
            switch (dataType)
            {
                case I3DataTypeEnum.sdtString:
                    if ((column.DataType == typeof(string)) && (column.MaxLength == fieldLength))
                    {
                        return true;
                    }
                    break;
                case I3DataTypeEnum.sdtInt:
                    if (column.DataType == typeof(int))
                    {
                        return true;
                    }
                    break;
                case I3DataTypeEnum.sdtDateTime:
                    if (column.DataType == typeof(DateTime))
                    {
                        return true;
                    }
                    break;
                case I3DataTypeEnum.sdtFloat:
                    if (column.DataType == typeof(double))
                    {
                        return true;
                    }
                    break;
                case I3DataTypeEnum.sdtBoolean:
                    if (column.DataType == typeof(bool))
                    {
                        return true;
                    }
                    break;
                default:
                    if ((column.DataType == typeof(string)) && (column.MaxLength == 50))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }



        /// <summary>
        /// 从udl文件获取连接字符串
        /// </summary>
        /// <param name="aUdlFileName"></param>
        /// <returns></returns>
        public static string GetConnectionStringByUDLFile(string aUdlFileName)
        {
            string tempStr, connectionString;
            try
            {
                TextReader fsRead = new StreamReader(aUdlFileName);
                while (true)
                {
                    tempStr = fsRead.ReadLine();
                    if (tempStr == "" || tempStr[0] != ';' && tempStr[0] != '[')
                    {
                        connectionString = tempStr;
                        break;
                    }
                }
                fsRead.Close();
            }
            catch (Exception ex)
            {
                connectionString = "连接字符串获取失败，错误信息：\r\n" + ex.Message;
            }

            return connectionString;
        }

        /// <summary>
        /// 从OLEDB连接字符串获取服务器名
        /// 失败返回 ""
        /// </summary>
        /// <param name="aConnectionString"></param>
        /// <returns></returns>
        public static string GetServerNameByOLEDBConnectionString(string aConnectionString)
        {
            OleDbConnectionStringBuilder ob;
            try
            {
                ob = new OleDbConnectionStringBuilder(aConnectionString);
            }
            catch
            {
                return "";
            }

            try
            {
                return ob.DataSource.ToUpper();
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 从SqlServer连接字符串获取服务器名
        /// 失败返回 ""
        /// </summary>
        /// <param name="aConnectionString"></param>
        /// <returns></returns>
        public static string GetServerNameBySqlServerConnectionString(string aConnectionString)
        {
            SqlConnectionStringBuilder sb;
            try
            {
                sb = new SqlConnectionStringBuilder(aConnectionString);
            }
            catch
            {
                return "";
            }

            try
            {
                return sb.DataSource.ToUpper();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 从OLEDB连接字符串获取数据库名
        /// 失败返回 ""
        /// </summary>
        /// <param name="aConnectionString"></param>
        /// <returns></returns>
        public static string GetDatabaseNameByOLEDBConnectionString(string aConnectionString)
        {
            OleDbConnectionStringBuilder ob;
            try
            {
                ob = new OleDbConnectionStringBuilder(aConnectionString);
            }
            catch
            {
                return "";
            }

            try
            {
                return ob["Initial Catalog"].ToString();
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 从SqlServer连接字符串获取数据库名
        /// 失败返回 ""
        /// </summary>
        /// <param name="aConnectionString"></param>
        /// <returns></returns>
        public static string GetDatabaseNameBySqlServerConnectionString(string aConnectionString)
        {
            SqlConnectionStringBuilder sb;
            try
            {
                sb = new SqlConnectionStringBuilder(aConnectionString);
            }
            catch
            {
                return "";
            }

            try
            {
                return sb["Initial Catalog"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }



    public enum I3DataTypeEnum
    {
        sdtBoolean = 1,
        sdtInt = 2,
        sdtFloat = 3,  //double
        sdtString = 4,
        sdtDateTime = 5,
        sdtStream = 6,
    }
}
