using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using IE310.Core.IO.Compression;
using MySql.Data.MySqlClient;
using IE310.Core.Utils;
using System.IO;
using IE310.Core.Components;
using System.Data.OleDb;
using System.Reflection;

namespace IE310.Core.DB
{
    public enum DBServerType
    {
        None = 0,
        SqlServer = 1,
        Oracle = 2,
        MySql = 3,
    }
    public enum DBServerType2
    {
        MySql = 3,
    }

    /// <summary>
    /// 数据库操作实用类
    /// <para>增加对多数据源的支持，通过配置不同的数据源参数来获取不同的数据源连接串</para>
    /// </summary>
    /// <remarks>
    /// 数据库操作实用类
    /// </remarks>
    public sealed class I3DBUtil
    {
        private static Dictionary<string, object> dbTypeMapper = new Dictionary<string, object>();

        static I3DBUtil()
        {
            PrepareDBTypeMapper();
        }

        #region 参数类型转换

        private static void PrepareDBTypeMapper()
        {
            #region 2ORA
            dbTypeMapper.Add("DB2ORA:" + DbType.String, OracleType.VarChar);
            dbTypeMapper.Add("DB2ORA:" + DbType.DateTime, OracleType.DateTime);
            dbTypeMapper.Add("DB2ORA:" + DbType.Single, OracleType.Float);
            dbTypeMapper.Add("DB2ORA:" + DbType.Double, OracleType.Double);
            dbTypeMapper.Add("DB2ORA:" + DbType.Binary, OracleType.Blob);
            dbTypeMapper.Add("DB2ORA:" + DbType.Int32, OracleType.Int32);
            dbTypeMapper.Add("DB2ORA:" + DbType.Int16, OracleType.Int16);
            dbTypeMapper.Add("DB2ORA:" + DbType.SByte, OracleType.Byte);
            dbTypeMapper.Add("DB2ORA:" + DbType.StringFixedLength, OracleType.Char);
            dbTypeMapper.Add("DB2ORA:" + DbType.Decimal, OracleType.Number);
            dbTypeMapper.Add("DB2ORA:" + DbType.AnsiString, OracleType.Clob);

            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Char, OracleType.Char);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.VarChar, OracleType.VarChar);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.DateTime, OracleType.DateTime);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Float, OracleType.Float);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Real, OracleType.Double);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Binary, OracleType.Blob);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.SmallInt, OracleType.Int32);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Int, OracleType.Int16);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.TinyInt, OracleType.Byte);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Decimal, OracleType.Number);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Text, OracleType.Clob);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.NVarChar, OracleType.NVarChar);
            dbTypeMapper.Add("SQL2ORA:" + SqlDbType.Image, OracleType.Raw);

            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Decimal, OracleType.Number);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Byte, OracleType.Byte);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Int16, OracleType.Int16);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Int32, OracleType.Int32);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Float, OracleType.Float);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Double, OracleType.Double);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Timestamp, OracleType.Timestamp);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Int64, OracleType.);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Int24, OracleType.);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Date, OracleType.DateTime);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Time, OracleType.DateTime);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.DateTime, OracleType.DateTime);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Year, OracleType.);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Newdate, OracleType.VarChar);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.VarString, OracleType.VarChar);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Bit, OracleType.VarChar);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.NewDecimal, OracleType.Number);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Enum, OracleType.VarChar);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Set, OracleType.VarChar);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.TinyBlob, OracleType.Blob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.MediumBlob, OracleType.Blob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.LongBlob, OracleType.Blob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Blob, OracleType.Blob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.VarChar, OracleType.VarChar);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.String, OracleType.VarChar);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Geometry, OracleType.VarChar);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.UByte, OracleType.VarChar);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.UInt16, OracleType.UInt16);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.UInt32, OracleType.UInt32);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.UInt64, OracleType.VarChar);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.UInt24, OracleType.VarChar);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Binary, OracleType.Blob);
            //dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.VarBinary, OracleType.VarChar);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.TinyText, OracleType.Clob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.MediumText, OracleType.Clob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.LongText, OracleType.Clob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Text, OracleType.Clob);
            dbTypeMapper.Add("MYSQL2ORA:" + MySqlDbType.Guid, OracleType.Char);
            #endregion

            #region 2SQL
            dbTypeMapper.Add("DB2SQL:" + DbType.String, SqlDbType.VarChar);
            dbTypeMapper.Add("DB2SQL:" + DbType.DateTime, SqlDbType.DateTime);
            dbTypeMapper.Add("DB2SQL:" + DbType.Single, SqlDbType.Float);
            dbTypeMapper.Add("DB2SQL:" + DbType.Double, SqlDbType.Real);
            dbTypeMapper.Add("DB2SQL:" + DbType.Binary, SqlDbType.Binary);
            dbTypeMapper.Add("DB2SQL:" + DbType.Int32, SqlDbType.SmallInt);
            dbTypeMapper.Add("DB2SQL:" + DbType.Int16, SqlDbType.Int);
            dbTypeMapper.Add("DB2SQL:" + DbType.SByte, SqlDbType.TinyInt);
            dbTypeMapper.Add("DB2SQL:" + DbType.StringFixedLength, SqlDbType.Char);
            dbTypeMapper.Add("DB2SQL:" + DbType.Decimal, SqlDbType.Decimal);
            dbTypeMapper.Add("DB2SQL:" + DbType.AnsiString, SqlDbType.Text);

            dbTypeMapper.Add("ORA2SQL:" + OracleType.VarChar, SqlDbType.VarChar);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.DateTime, SqlDbType.DateTime);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Float, SqlDbType.Float);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Double, SqlDbType.Real);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Blob, SqlDbType.Binary);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Int32, SqlDbType.Int);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Int16, SqlDbType.SmallInt);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Byte, SqlDbType.TinyInt);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Char, SqlDbType.Char);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Number, SqlDbType.Decimal);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Clob, SqlDbType.Text);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.NVarChar, SqlDbType.NVarChar);
            dbTypeMapper.Add("ORA2SQL:" + OracleType.Raw, SqlDbType.Image);


            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Decimal, SqlDbType.Decimal);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Byte, SqlDbType.TinyInt);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Int16, SqlDbType.SmallInt);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Int32, SqlDbType.Int);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Float, SqlDbType.Float);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Double, SqlDbType.Real);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Timestamp, SqlDbType.Timestamp);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Int64, SqlDbType.BigInt);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Int24, SqlDbType.);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Date, SqlDbType.Date);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Time, SqlDbType.Time);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.DateTime, SqlDbType.DateTime);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Year, SqlDbType.);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Newdate, SqlDbType.VarChar);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.VarString, SqlDbType.VarChar);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Bit, SqlDbType.Bit);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.NewDecimal, SqlDbType.Decimal);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Enum, SqlDbType.VarChar);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Set, SqlDbType.VarChar);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.TinyBlob, SqlDbType.Binary);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.MediumBlob, SqlDbType.Binary);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.LongBlob, SqlDbType.Binary);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Blob, SqlDbType.Binary);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.VarChar, SqlDbType.VarChar);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.String, SqlDbType.VarChar);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Geometry, SqlDbType.VarChar);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.UByte, SqlDbType.VarChar);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.UInt16, SqlDbType.UInt16);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.UInt32, SqlDbType.UInt32);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.UInt64, SqlDbType.VarChar);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.UInt24, SqlDbType.VarChar);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Binary, SqlDbType.Blob);
            //dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.VarBinary, SqlDbType.VarChar);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.TinyText, SqlDbType.Text);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.MediumText, SqlDbType.Text);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.LongText, SqlDbType.Text);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Text, SqlDbType.Text);
            dbTypeMapper.Add("MYSQL2SQL:" + MySqlDbType.Guid, SqlDbType.Char);
            #endregion

            #region 2MYSQL
            dbTypeMapper.Add("DB2MYSQL:" + DbType.String, MySqlDbType.VarChar);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.DateTime, MySqlDbType.DateTime);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.Single, MySqlDbType.Float);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.Double, MySqlDbType.Double);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.Binary, MySqlDbType.LongBlob);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.Int32, MySqlDbType.Int32);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.Int16, MySqlDbType.Int16);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.SByte, MySqlDbType.Byte);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.StringFixedLength, MySqlDbType.String);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.Decimal, MySqlDbType.Decimal);
            dbTypeMapper.Add("DB2MYSQL:" + DbType.AnsiString, MySqlDbType.LongText);

            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.VarChar, MySqlDbType.VarChar);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.DateTime, MySqlDbType.DateTime);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Float, MySqlDbType.Float);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Double, MySqlDbType.Double);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Blob, MySqlDbType.LongBlob);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Int32, MySqlDbType.Int32);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Int16, MySqlDbType.Int16);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Byte, MySqlDbType.Byte);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Char, MySqlDbType.String);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Number, MySqlDbType.Decimal);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Clob, MySqlDbType.LongText);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.NVarChar, MySqlDbType.VarChar);
            dbTypeMapper.Add("ORA2MYSQL:" + OracleType.Raw, MySqlDbType.LongBlob);

            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Char, MySqlDbType.String);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.VarChar, MySqlDbType.VarChar);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.DateTime, MySqlDbType.DateTime);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Float, MySqlDbType.Float);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Real, MySqlDbType.Double);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Binary, MySqlDbType.LongBlob);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.SmallInt, MySqlDbType.Int16);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Int, MySqlDbType.Int32);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.TinyInt, MySqlDbType.Byte);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Decimal, MySqlDbType.Decimal);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Text, MySqlDbType.LongText);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.NVarChar, MySqlDbType.VarChar);
            dbTypeMapper.Add("SQL2MYSQL:" + SqlDbType.Image, MySqlDbType.LongBlob);
            #endregion
        }

        /// <summary>
        /// 将数据库类型转换为Oracle数据库类型 
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static OracleType Convert2OracleType(object objType)
        {
            if (objType is OracleType)
            {
                return (OracleType)objType;
            }
            if (objType is SqlDbType)
            {
                return (OracleType)dbTypeMapper["SQL2ORA:" + objType];
            }
            if (objType is MySqlDbType)
            {
                return (OracleType)dbTypeMapper["MYSQL2ORA:" + objType];
            }
            if (objType is DbType)
            {
                return (OracleType)dbTypeMapper["DB2ORA:" + objType];
            }
            return OracleType.VarChar;
        }

        /// <summary>
        /// 将数据库类型转为SQL数据类型
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static SqlDbType Convert2SQLType(object objType)
        {
            if (objType is SqlDbType)
            {
                return (SqlDbType)objType;
            }
            if (objType is OracleType)
            {
                return (SqlDbType)dbTypeMapper["ORA2SQL:" + objType];
            }
            if (objType is MySqlDbType)
            {
                return (SqlDbType)dbTypeMapper["MYSQL2SQL:" + objType];
            }
            if (objType is DbType)
            {
                return (SqlDbType)dbTypeMapper["DB2SQL:" + objType];
            }
            return SqlDbType.VarChar;
        }

        /// <summary>
        /// 将数据库类型转为SQL数据类型
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static MySqlDbType Convert2MySqlType(object objType)
        {
            if (objType is MySqlDbType)
            {
                return (MySqlDbType)objType;
            }
            if (objType is OracleType)
            {
                return (MySqlDbType)dbTypeMapper["ORA2MYSQL:" + objType];
            }
            if (objType is SqlDbType)
            {
                return (MySqlDbType)dbTypeMapper["SQL2MYSQL:" + objType];
            }
            if (objType is DbType)
            {
                return (MySqlDbType)dbTypeMapper["DB2MYSQL:" + objType];
            }
            return MySqlDbType.VarChar;
        }

        #endregion

        #region 无效值定义、值校验

        /// <summary>
        /// 表示无效时间
        /// </summary>
        public static DateTime INVALID_DATE = DateTime.MinValue;

        /// <summary>
        /// 表示无效整数
        /// </summary>
        public static int INVALID_ID = Int32.MinValue;

        /// <summary>
        /// 表示无效GUID的关键字（主键、外键等）
        /// </summary>
        public static string EMPTY_KEY = string.Empty;

        /// <summary>
        /// 表示为空GUID的关键字（主键、外键等）
        /// </summary>
        public static string NULL_KEY = "(null)";

        /// <summary>
        /// 表示为不为空的GUID关键字（主键、外键等）
        /// </summary>
        public static string NOTNULL_KEY = "(notnull)";

        /// <summary>
        /// 表示无效十进制数
        /// </summary>
        public static decimal INVALID_DECIMAL = Int32.MinValue;

        public static double INVALID_DOUBLE = Int32.MinValue;
        public static float INVALID_FLOAT = Int32.MinValue;

        public static bool IsValidPKey(string sKey)
        {
            return string.IsNullOrEmpty(sKey) == false;
        }
        public static bool IsValidPKey(int iKey)
        {
            return iKey > 0;
        }

        #endregion

        #region 创建连接字符串

        /// <summary>
        /// 生成用OLEDB方式连接sqlserver时的连接字符串
        /// 获取连接字符串的示例，可以从  www.ConnectionStrings.com  获取
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
        public static string CreateOLEDBConnectionStringByUDLFile(string aFileName)
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
        /// 从udl文件获取连接字符串
        /// </summary>
        /// <param name="aUdlFileName"></param>
        /// <returns></returns>
        public static string GetConnectionStringInUDLFile(string aUdlFileName)
        {
            string tempStr, ConnectionString;
            try
            {
                TextReader fsRead = new StreamReader(aUdlFileName);
                while (true)
                {
                    tempStr = fsRead.ReadLine();
                    if (tempStr == "" || tempStr[0] != ';' && tempStr[0] != '[')
                    {
                        ConnectionString = tempStr;
                        break;
                    }
                }
                fsRead.Close();
            }
            catch (Exception ex)
            {
                ConnectionString = "连接字符串获取失败，错误信息：\r\n" + ex.Message;
            }

            return ConnectionString;
        }

        /// <summary>
        /// 获取sqlserver数据库连接字符串
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static string CreateSqlServerConnectionString(string server, string userName, string password, string database)
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = server;
            sb.UserID = userName;
            sb.Password = password;
            sb.InitialCatalog = database;
            return sb.ConnectionString;
        }

        /// <summary>
        /// 获取Oracle数据库连接字符串
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static string CreateOracleConnectionString(string server, string userName, string password)
        {
            OracleConnectionStringBuilder sb = new OracleConnectionStringBuilder();
            sb.DataSource = server;
            sb.UserID = userName;
            sb.Password = password;
            return sb.ConnectionString;
        }

        /// <summary>
        /// 获取MySql数据库连接字符串
        /// </summary>
        /// <param name="server">服务器:端口</param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static string CreateMySqlConnectionString(string server, string userName, string password, string database)
        {
            string[] arr = server.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = arr[0];
            if (arr.Length > 1)
            {
                sb.Port = uint.Parse(arr[1]);
            }
            sb.UserID = userName;
            sb.Password = password;
            sb.Database = database;
            return sb.ConnectionString + ";charset=utf8";//todo..........这里需要根据变量设置
        }

        /// <summary>
        /// 根据数据库类型获取连接字符串
        /// </summary>
        /// <param name="dbServerType"></param>
        /// <param name="server"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static string CreateConnectionString(DBServerType dbServerType, string server, string userName, string password, string database)
        {
            string connectionString;
            switch (dbServerType)
            {
                case DBServerType.SqlServer:
                    connectionString = I3DBUtil.CreateSqlServerConnectionString(server, userName, password, database);
                    break;
                case DBServerType.Oracle:
                    connectionString = I3DBUtil.CreateOracleConnectionString(server, userName, password);
                    break;
                default:
                    connectionString = I3DBUtil.CreateMySqlConnectionString(server, userName, password, database);
                    break;///////////////////////////重构
            }
            return connectionString;
        }

        #endregion

        #region 连接字符串解析

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


        #endregion

        #region 连接字符串、数据库类型、数据库连接

        /// <summary>
        /// 表示链接字符串
        /// </summary>
        private static string connectionString;

        private static DBServerType dbServerType;
        /// <summary>
        /// 数据库的类型
        /// </summary>
        public static DBServerType DBServerType
        {
            get
            {
                return I3DBUtil.dbServerType;
            }
        }

        /// <summary>
        /// 获取或设置数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                    dbServerType = AnalysisConnectionString(ref connectionString);
                }
                return connectionString;
            }
            set
            {
                connectionString = value;
                dbServerType = AnalysisConnectionString(ref connectionString);
            }
        }

        /// <summary>
        /// 分析连接字符串，以
        /// 以 Encry: 为前缀表示密码经过了加密
        /// 以 DBServerType: 为前缀确定类型，默认为MySql
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DBServerType AnalysisConnectionString(ref string connectionString)
        {
            DBServerType result = DB.DBServerType.MySql;
            bool passWordIsEncry = false;
            if (connectionString.StartsWith("Encry:", StringComparison.CurrentCultureIgnoreCase))
            {
                passWordIsEncry = true;
                connectionString = connectionString.Replace("Encry:", "");
            }

            if (connectionString.StartsWith(DBServerType.SqlServer + ":"))
            {
                result = DBServerType.SqlServer;
                connectionString = connectionString.Replace(DBServerType.SqlServer + ":", "");
                if (passWordIsEncry)
                {
                    SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connectionString);
                    if (!string.IsNullOrEmpty(sb.Password))
                    {
                        sb.Password = I3Cry.UnCryString(sb.Password, "password");
                        connectionString = sb.ConnectionString;
                    }
                }
            }
            else if (connectionString.StartsWith(DBServerType.Oracle + ":"))
            {
                result = DBServerType.Oracle;
                connectionString = connectionString.Replace(DBServerType.Oracle + ":", "");
                if (passWordIsEncry)
                {
                    OracleConnectionStringBuilder sb = new OracleConnectionStringBuilder(connectionString);
                    if (!string.IsNullOrEmpty(sb.Password))
                    {
                        sb.Password = I3Cry.UnCryString(sb.Password, "password");
                        connectionString = sb.ConnectionString;
                    }
                }
            }
            else
            {
                result = DBServerType.MySql;
                connectionString = connectionString.Replace(DBServerType.MySql + ":", "");
                if (passWordIsEncry)
                {
                    MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder(connectionString);
                    if (!string.IsNullOrEmpty(sb.Password))
                    {
                        sb.Password = I3Cry.UnCryString(sb.Password, "password");
                        connectionString = sb.ConnectionString;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取一个数据库链接，一定要记得释放(Close)链接
        /// </summary>
        /// <returns><see cref="string"/>，返回默认的IDb数据库链接对象（链接已打开）</returns>
        public static IDbConnection CreateAndOpenDbConnection()
        {
            return CreateAndOpenDbConnection(DBServerType, ConnectionString);
        }


        public static IDbConnection CreateAndOpenDbConnection(string connectionString)
        {
            return CreateAndOpenDbConnection(DBServerType.None, connectionString);
        }

        public static IDbConnection CreateAndOpenDbConnection(DBServerType type, string connectionString)
        {
            IDbConnection sqlCon = CreateConnection(type, connectionString);
            sqlCon.Open();
            return sqlCon;
        }


        public static IDbConnection CreateConnection(string connectionString)
        {
            return CreateConnection(DBServerType.None, connectionString);
        }

        public static IDbConnection CreateConnection(DBServerType type, string connectionString)
        {
            if (type == DB.DBServerType.None)
            {
                type = AnalysisConnectionString(ref connectionString);
            }

            switch (type)
            {
                case DBServerType.SqlServer:
                    return new SqlConnection(connectionString);
                case DBServerType.Oracle:
                    return new OracleConnection(connectionString);
                default:
                    return new MySqlConnection(connectionString);
            }
        }

        /// <summary>
        /// 强制关闭数据库链接，屏蔽异常
        /// </summary>
        /// <param name="con"><see cref="string"/>，要关闭的数据库链接</param>
        public static void CloseConnection(IDbConnection con)
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region 参数准备

        /// <summary>
        /// 根据传入的参数获取构造（IDb格式的）分页查询SQL语句
        /// </summary>
        /// <param name="pageInfo"><see cref="IE310.CodeMaker.Core.Data.PageInfo"/>，分页数据对象</param>
        /// <param name="orderParams"><see cref="IE310.CodeMaker.Core.Data.OrderParams"/>，排序数据对象</param>
        /// <param name="sql"><see cref="string"/>，原始查询SQL语句</param>
        /// <example>
        /// <code>
        /// int iPageIndex = 1; //如：dataGrid.CurrentPageIndex;
        /// int iPageSize = 10; //如：dataGrid.PageSize;
        /// PageInfo pageInfo = new PageInfo(iPageIndex, iPageSize);
        /// pageInfo.TotalCount = 100;
        /// 
        /// OrderParams orderParams = new OrderParams("ID", false);
        /// 
        /// string sSql = "Select t.ID, t.NAME From TABLE_NAME t";
        /// sSql = DBUtil.PrepareSql(pageInfo, orderParams, sSql);
        /// </code>
        /// 结果：
        /// <para>"Select * From (Select * From (Select p.*, rownum rnum From(Select t.ID, t.NAME From TABLE_NAME t Order By t.ID Desc) p) q Where rownum &lt;= 20) Where rnum > 10"</para>
        /// </example>
        /// <returns><see cref="string"/>，返回构造好的（IDb格式的）分页查询SQL语句</returns>
        public static string PrepareSql(I3PageInfo pageInfo, I3OrderParams orderParams, string sql)
        {
            int startIndex = 0;
            int endIndex = 0;
            if (pageInfo.RowsInPage <= 0)
            {
                pageInfo.RowsInPage = 10;
            }
            int pageCount = (int)Math.Ceiling(1.0 * pageInfo.TotalCount / pageInfo.RowsInPage);
            pageInfo.PageCount = pageCount;
            if (pageInfo.PageIndex >= pageCount)
            {
                pageInfo.PageIndex = pageCount - 1;
            }
            if (pageCount == 0)
            {
                pageInfo.PageIndex = 0;
            }
            startIndex = pageInfo.PageIndex * pageInfo.RowsInPage;
            endIndex = startIndex + pageInfo.RowsInPage;
            if (endIndex >= pageInfo.TotalCount)
            {
                endIndex = pageInfo.TotalCount;
            }
            int curCount = endIndex - startIndex;

            StringBuilder strBuf = new StringBuilder();
            string sOrder = "";
            if (orderParams != null)
            {
                sOrder = orderParams.ToString();
            }
            if (string.IsNullOrEmpty(sOrder))
            {
                //sOrder = " Order By ID Desc";
                sOrder = " ";
            }
            switch (DBServerType)
            {
                case DBServerType.SqlServer:
                    strBuf.Append("select Top ").Append(pageInfo.RowsInPage).Append("  q.* from (select p.*, ROW_NUMBER() OVER (").Append(sOrder).Append(") As rnum from (").Append(sql)
                        .Append(") p) q where rnum > ").Append(startIndex).Append(sOrder);
                    return strBuf.ToString();
                case DBServerType.Oracle:
                    strBuf.Append("select * from ( select * from( select p.*, rownum rnum from (").Append(sql).Append(sOrder)
                        .Append(") p) q where rownum <= ").Append(endIndex).Append(") where rnum > ").Append(startIndex);
                    return strBuf.ToString();
                default:
                    strBuf.Append("select * from (").Append(sql).Append(") p").Append(sOrder).Append(" LIMIT ")
                        .Append(startIndex).Append(",").Append(pageInfo.RowsInPage);
                    return strBuf.ToString();
            }
        }

        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="length"><see cref="int"/>，参数数据大小</param>
        /// <param name="value"><see cref="object"/>，参数数据值</param>
        /// <example>
        /// <code>
        /// int iId = DBUtil.GetNextSeqValue("SEQ_NAME", trans);
        /// string sName = "InsCMS";
        /// IDbDataParameter[] parms = new IDbDataParameter[]
        /// {
        ///		DBUtil.PrepareParameter(":W_ID", OracleType.Number, 12, iId),
        ///		DBUtil.PrepareParameter(":W_NAME", DbType.NVarchar, 20, sName)
        /// };
        /// </code>
        /// </example>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, object type, int length, object value)
        {
            IDbDataParameter param = null;
            if (type is OracleType)
            {
                param = new OracleParameter(paramName, (OracleType)type, length);
            }
            else if (type is SqlDbType)
            {
                param = new SqlParameter(paramName, (SqlDbType)type, length);
            }
            else
            {
                param = new MySqlParameter(paramName, (MySqlDbType)type, length);
            }

            param.Value = InsureDBParam(value);
            return param;
        }

        public static string ToFrontLikeString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value + "%";
        }

        public static string ToBackLikeString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return "%" + value;
        }

        /// <summary>
        /// 转化为全匹配字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFullLikeString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return "%" + value + "%";
        }

        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="value"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, OracleType type, object value)
        {
            IDbDataParameter param = new OracleParameter(paramName, type);
            param.Value = InsureDBParam(value);
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="curValue"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, OracleType type)
        {
            IDbDataParameter param = new OracleParameter(paramName, type);
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="value"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, DbType type, object value)
        {
            IDbDataParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.DbType = type;
            param.Value = InsureDBParam(value);
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="curValue"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, DbType type)
        {
            IDbDataParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.DbType = type;
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="value"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, SqlDbType type, object value)
        {
            IDbDataParameter param = new SqlParameter(paramName, type);
            param.Value = InsureDBParam(value);
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="curValue"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, SqlDbType type)
        {
            IDbDataParameter param = new SqlParameter(paramName, type);
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="value"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, MySqlDbType type, object value)
        {
            IDbDataParameter param = new MySqlParameter(paramName, type);
            param.Value = InsureDBParam(value);
            return param;
        }
        /// <summary>
        /// 获取构造IDb参数对象
        /// </summary>
        /// <param name="paramName"><see cref="string"/>，参数名称</param>
        /// <param name="type"><see cref="System.Data.IDbClient.DbType"/>，参数数据类型</param>
        /// <param name="curValue"><see cref="object"/>，参数数据值</param>
        /// <returns><see cref="System.Data.IDbClient.IDbDataParameter"/>，返回构造好的IDb参数对象</returns>
        public static IDbDataParameter PrepareParameter(string paramName, MySqlDbType type)
        {
            IDbDataParameter param = new MySqlParameter(paramName, type);
            return param;
        }

        #endregion

        #region  值转换

        public static int ToInt(object obj)
        {
            if (obj is Decimal)
            {
                return (int)(decimal)(obj);
            }
            if (obj is Enum)
            {
                return (int)obj;
            }
            if (obj is int)
            {
                return (int)obj;
            }
            if (obj == null || obj == DBNull.Value)
            {
                return I3DBUtil.INVALID_ID;
            }
            int result = I3DBUtil.INVALID_ID;
            if (int.TryParse("" + obj, out result))
            {
                return result;
            }
            return I3DBUtil.INVALID_ID;
        }

        /// <summary>
        /// 获取十进制（decimal）数据的格式化字符串
        /// </summary>
        /// <param name="value"><see cref="decimal"/>，十进制数（decimal）</param>
        /// <param name="format"><see cref="string"/>，格式化字符串</param>
        /// <example>
        /// <code>
        ///	decimal v = 35.65m;
        ///	string format1 = "##";
        ///	string format2 = "##.#";
        ///	string s1 = DBUtil.ToString(v, format1);
        ///	string s2 = DBUtil.ToString(v, format2);
        /// </code>
        /// 结果
        /// <list>
        /// <item><description><c>s1</c>="36"</description></item>
        /// <item><description><c>s2</c>="35.7"</description></item>
        /// </list>
        /// </example>
        /// <returns><see cref="string"/>，返回十进制（decimal）数据的格式化字符串</returns>
        public static string ToString(decimal value, string format)
        {
            if (value == I3DBUtil.INVALID_DECIMAL)
            {
                return "";
            }
            else
            {
                return value.ToString(format);
            }
        }

        /// <summary>
        /// 获取整形（int）数据的格式化字符串
        /// </summary>
        /// <param name="value"><see cref="int"/>，整数（int）</param>
        /// <param name="format"><see cref="string"/>，格式化字符串</param>
        /// <returns><see cref="string"/>，返回整数（int）数据的格式化字符串</returns>
        public static string ToString(int value, string format)
        {
            if (value == I3DBUtil.INVALID_ID)
            {
                return "";
            }
            else
            {
                return value.ToString(format);
            }

        }

        /// <summary>
        /// 获取数据库参数数据值
        /// </summary>
        /// <param name="paramData"><see cref="object"/>，参数数据值</param>
        /// <example>
        /// 例1：
        /// <code>
        /// int i = DBUtil.INVALID_ID;
        /// IDbDataParameter p = DBUtil.PrepareParameter(":W_ID", OracleType.Number, 12);
        /// p.Value = DBUtil.InsureDBParam(i);
        /// </code>
        /// </example>
        /// <example>
        /// 例2：
        /// <code>
        /// int i = DBUtil.INVALID_ID;
        ///	object obj1 = InsureDBParam(i);
        /// </code>
        /// 结果
        /// i：DBNull.Value
        /// </example>
        /// <returns><see cref="object"/>，返回数据库参数数据值</returns>
        public static object InsureDBParam(object paramData)
        {
            if (paramData == null)
            {
                return DBNull.Value;
            }
            else if (paramData is int && ((int)paramData) == INVALID_ID)
            {
                return DBNull.Value;
            }
            else if (paramData is float && float.IsNaN((float)paramData))
            {
                return DBNull.Value;
            }
            else if (paramData is decimal && (decimal)paramData == INVALID_DECIMAL)
            {
                return DBNull.Value;
            }
            else if (paramData is DateTime && ((DateTime)paramData) == INVALID_DATE)
            {
                return DBNull.Value;
            }
            return paramData;
        }

        /// <summary>
        /// 返回数据列表的表示的SQL语句片断
        /// </summary>
        /// <param name="list"><see cref="System.Collections.ArrayList"/>，数据列表</param>
        /// <example>
        /// <code>
        /// ArrayList aryId = new ArrayList();
        /// aryDept.Add(1);
        /// aryDept.Add(2);
        /// aryDept.Add(3);
        /// string sId = DBUtil.ArrayToSQLIn(aryId);
        /// string sSql = "Select t.* From TABLE_NAME t Where t.ID In (" + sId + ")";
        /// </code>
        /// 结果sSql："Select t.* From TABLE_NAME t Where t.ID In (1, 2, 3)"
        /// </example>
        /// <returns><see cref="string"/>，返回SQL语句片断</returns>
        public static string ArrayToSQLIn(IList list)
        {
            if (list.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            foreach (object obj in list)
            {
                sb.Append(",");
                if (obj is string)
                {
                    sb.Append("'").Append(obj.ToString()).Append("'");
                }
                else
                {
                    sb.Append(obj.ToString());
                }
            }
            return sb.ToString(1, sb.Length - 1);
        }

        /// <summary>
        /// 返回字符串数组表示的SQL语句片断
        /// </summary>
        /// <param name="arr"><see cref="string"/>[]，字符串数组</param>
        /// <returns><see cref="string"/>，返回SQL语句片断</returns>
        public static string ArrayToSQLIn(string[] arr)
        {
            return ArrayToSQLIn(arr, false);
        }

        /// <summary>
        /// 返回字符串数组表示的SQL语句片断
        /// </summary>
        /// <param name="arr"><see cref="string"/>[]，整数数组</param>
        /// <returns><see cref="string"/>，返回SQL语句片断</returns>
        public static string ArrayToSQLIn(int[] arr)
        {
            if (arr.Length == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            foreach (int obj in arr)
            {
                sb.Append(",");
                sb.Append(obj);
            }
            return sb.ToString(1, sb.Length - 1);
        }

        /// <summary>
        /// 返回字符串数组表示的SQL语句片断
        /// </summary>
        /// <param name="arr"><see cref="string"/>[]，字符串数组</param>
        /// <param name="bAsNum"><see cref="bool"/>，是否当成数字格式处理，如果是 不加引号</param>
        /// <returns><see cref="string"/>，返回SQL语句片断</returns>
        public static string ArrayToSQLIn(string[] arr, bool bAsNum)
        {
            if (arr.Length == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            foreach (string obj in arr)
            {
                sb.Append(",");
                if (!bAsNum)
                {
                    sb.Append("'");
                }
                sb.Append(obj);
                if (!bAsNum)
                {
                    sb.Append("'");
                }
            }
            return sb.ToString(1, sb.Length - 1);
        }

        /// <summary>
        /// 处理一个字符串为空的情形
        /// </summary>
        /// <param name="value"><see cref="string"/>，要进行处理的字符串。</param>
        /// <param name="defaultValue"><see cref="string"/>，当字符串为空时要返回的默认值</param>
        /// <returns>当字符串为空时返回指定的默认值，否则返回字符串本身</returns>
        public static string IsNull(string value, string defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }
            return value;
        }

        #endregion

        #region 值读取、值写入

        public static byte[] GetBytes(DbDataReader reader, int colIndex)
        {
            int iCount = (int)reader.GetBytes(colIndex, 0, null, 0, 0);
            byte[] tempData = new byte[iCount];
            reader.GetBytes(colIndex, 0, tempData, 0, iCount);
            return tempData;
        }

        /// <summary>
        /// 返回obj所代表的整数。
        /// </summary>
        /// <param name="obj">要返回其所代表的整数的object。</param>
        /// <param name="defaultValue">当obj不能对应一个整数时返回的值。</param>
        /// <returns>返回obj所代表的整数。</returns>
        /// <example>
        /// <code>
        /// object obj = 6;
        /// int i = DBUtil.GetDbIntValue(obj, DBUtil.INVALID_ID);
        /// </code>
        /// 运行结果：i = 6;
        /// </example>
        public static int GetDbIntValue(object obj, int defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else if (obj is int)
            {
                return (int)obj;
            }
            else if (obj is decimal)
            {
                return (int)(decimal)(obj);
            }
            else
            {
                try
                {
                    return int.Parse(obj.ToString());
                }
                catch
                {
                    return defaultValue;
                }
            }
        }


        /// <summary>
        /// 返回obj所代表的整数。
        /// </summary>
        /// <param name="obj">要返回其所代表的整数的object。</param>
        /// <param name="defaultValue">当obj不能对应一个整数时返回的值。</param>
        /// <returns>返回obj所代表的整数。</returns>
        /// <example>
        /// <code>
        /// object obj = 6;
        /// int i = DBUtil.GetDbIntValue(obj, DBUtil.INVALID_ID);
        /// </code>
        /// 运行结果：i = 6;
        /// </example>
        public static long GetDbLongValue(object obj, long defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else if (obj is int)
            {
                return (long)obj;
            }
            else if (obj is long)
            {
                return (long)obj;
            }
            else if (obj is decimal)
            {
                return (long)(decimal)(obj);
            }
            else
            {
                try
                {
                    return long.Parse(obj.ToString());
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 返回obj所代表的decimal。
        /// </summary>
        /// <param name="obj">要返回其所代表的decimal的object。</param>
        /// <param name="defaultValue">当obj不能对应一个decimal时返回的值。</param>
        /// <returns>返回obj所代表的decimal。</returns>
        public static decimal GetDbDecimalValue(object obj, decimal defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else if (obj is decimal)
            {
                return (decimal)obj;
            }
            else if (obj is int)
            {
                return (decimal)(int)obj;
            }
            else
            {
                try
                {
                    return decimal.Parse(obj.ToString());
                }
                catch
                {
                    return defaultValue;
                }
            }
        }


        /// <summary>
        /// 返回obj所代表的double。
        /// </summary>
        /// <param name="obj">要返回其所代表的decimal的object。</param>
        /// <param name="defaultValue">当obj不能对应一个decimal时返回的值。</param>
        /// <returns>返回obj所代表的decimal。</returns>
        public static double GetDbDoubleValue(object obj, double defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else if (obj is double)
            {
                return (double)obj;
            }
            else if (obj is int)
            {
                return (double)(int)obj;
            }
            else
            {
                try
                {
                    return double.Parse(obj.ToString());
                }
                catch
                {
                    return defaultValue;
                }
            }
        }




        /// <summary>
        /// 返回obj所代表的float。
        /// </summary>
        /// <param name="obj">要返回其所代表的decimal的object。</param>
        /// <param name="defaultValue">当obj不能对应一个decimal时返回的值。</param>
        /// <returns>返回obj所代表的decimal。</returns>
        public static float GetDbFloatValue(object obj, float defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else if (obj is float)
            {
                return (float)obj;
            }
            else if (obj is int)
            {
                return (float)(int)obj;
            }
            else
            {
                try
                {
                    return float.Parse(obj.ToString());
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 返回obj所代表的字符串。
        /// </summary>
        /// <param name="obj">要返回其所代表的字符串的object。</param>
        /// <param name="fDefaultValue">当obj不能对应一个字符串（如为空，为DBNull.Value）时返回的值。</param>
        /// <returns>返回obj所代表的字符串。</returns>
        public static string GetDbStringValue(object obj, string defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 返回obj所代表的DateTime。
        /// </summary>
        /// <param name="obj">要返回其所代表的DateTime的object。</param>
        /// <param name="fDefaultValue">当obj不能对应一个DateTime时返回的值。</param>
        /// <returns>返回obj所代表的DateTime。</returns>
        public static DateTime GetDbDateTimeValue(object obj, DateTime defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else if (obj is DBNull)
            {
                return defaultValue;
            }
            else if (obj is DateTime)
            {
                return (DateTime)obj;
            }
            else if (obj is string)
            {
                try
                {
                    return DateTime.Parse((string)obj);
                }
                catch
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
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

        #endregion

        #region 动态创建DataTable

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
                    throw new Exception(errString);
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

        #endregion

        #region 对象转数据集

        public static DataTable ObjectOrListToDataTable(Type type, object dataSource)
        {
            return ObjectOrListToDataTable(type, dataSource, null);
        }


        public static DataTable ObjectOrListToDataTable(Type type, object dataSource, DefaultValue defaultValue)
        {
            return ObjectOrListToDataTable(type, dataSource, defaultValue, 1);
        }

        public static DataTable ObjectOrListToDataTable(Type type, object dataSource, DefaultValue defaultValue, int pageSize)
        {
            return ObjectOrListToDataTable(type, dataSource, defaultValue, pageSize, null);
        }

        /// <summary>
        /// 将对象(或IList实现)转换为数据集（只支持Instance、Public属性，类型支持值类型、byte[]）
        /// dataSource可能为空列表，无法自动获取type，因此需要传入
        /// 注意，如果原始数据为4条，每页3条，则第2页的5、6条数据的位置上，并不会有默认值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DataTable ObjectOrListToDataTable(Type type, object dataSource, DefaultValue defaultValue, int pageSize, Dictionary<string, Type> enumTypeDic)
        {
            if (dataSource == null)
            {
                return new DataTable();
            }

            #region 准备数据集
            DataTable table = new DataTable();
            PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo pi in pis)
            {
                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string) || pi.PropertyType == typeof(byte[]))
                {
                    if (pageSize == 1)
                    {
                        DataColumn column = new DataColumn(pi.Name, pi.PropertyType);
                        table.Columns.Add(column);
                    }
                    else  //如果每页数据数大于1,则在每个字段后面附加1、2、3、4这样的后缀
                    {
                        for (int i = 1; i <= pageSize; i++)
                        {
                            DataColumn column = new DataColumn(pi.Name + i.ToString(), pi.PropertyType);
                            table.Columns.Add(column);
                        }
                    }
                    #region 枚举
                    if (enumTypeDic != null && enumTypeDic.ContainsKey(pi.Name))
                    {
                        if (pageSize == 1)
                        {
                            DataColumn column = new DataColumn(pi.Name + "Str", typeof(string));
                            table.Columns.Add(column);
                        }
                        else  //如果每页数据数大于1,则在每个字段后面附加1、2、3、4这样的后缀
                        {
                            for (int i = 1; i <= pageSize; i++)
                            {
                                DataColumn column = new DataColumn(pi.Name + "Str" + i.ToString(), typeof(string));
                                table.Columns.Add(column);
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion


            #region 准备数据源
            List<Object> list = new List<object>();
            if (dataSource.GetType().GetInterface("IList") != null)
            {
                IList sourceList = dataSource as IList;
                foreach (object item in sourceList)
                {
                    list.Add(item);
                }
            }
            else
            {
                list.Add(dataSource);
            }
            #endregion

            #region 创建DataRow
            DataRow lastRow = null;
            int totalIndex = 0;
            foreach (object item in list)
            {
                #region 创建或获取row对象
                DataRow row = null;
                totalIndex++;
                int indexInPage = 0;
                if (pageSize > 1)
                {
                    indexInPage = totalIndex % pageSize;
                    indexInPage = indexInPage == 0 ? pageSize : indexInPage;
                    if (indexInPage == 1)
                    {
                        row = table.NewRow();
                        table.Rows.Add(row);
                        lastRow = row;
                    }
                    else
                    {
                        row = lastRow;
                    }
                }
                else
                {
                    row = table.NewRow();
                    table.Rows.Add(row);
                }
                #endregion

                #region 赋值
                row.BeginEdit();
                foreach (PropertyInfo pi in pis)
                {
                    string fieldName = pi.Name;
                    if (pageSize > 1)//如果pageSize>1，获取实际的字段名
                    {
                        fieldName = fieldName + indexInPage.ToString();
                    }
                    if (table.Columns.Contains(fieldName))
                    {
                        object value = pi.GetValue(item, null);
                        if (defaultValue != null)
                        {
                            value = defaultValue.CheckValue(value, pi.PropertyType);
                        }
                        row[fieldName] = value;
                    }
                    #region 枚举
                    if (enumTypeDic != null && enumTypeDic.ContainsKey(pi.Name))
                    {
                        string fieldName2 = pi.Name + "Str";
                        if (pageSize > 1)//如果pageSize>1，获取实际的字段名
                        {
                            fieldName2 = fieldName2 + indexInPage.ToString();
                        }
                        if (table.Columns.Contains(fieldName2))
                        {
                            object value = pi.GetValue(item, null);
                            value = EnumToStr(value, enumTypeDic[pi.Name]);
                            if (defaultValue != null)
                            {
                                value = defaultValue.CheckValue(value, pi.PropertyType);
                            }
                            row[fieldName2] = value;
                        }
                    }
                    #endregion
                }
                row.EndEdit();
                #endregion
            }
            #endregion

            return table;
        }

        public static DataTable StringDicToDataTable(Dictionary<string, string> dic, DefaultValue defaultValue)
        {
            if (dic == null)
            {
                return new DataTable();
            }

            #region 准备数据集
            DataTable table = new DataTable();
            foreach (string key in dic.Keys)
            {
                DataColumn column = new DataColumn(key, typeof(string));
                table.Columns.Add(column);
            }
            if (!table.Columns.Contains("gv_rowindex"))
            {
                DataColumn column = new DataColumn("gv_rowindex", typeof(int));
                table.Columns.Add(column);
            }
            #endregion


            #region 创建或获取row对象
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            row["gv_rowindex"] = 1;
            #endregion

            #region 赋值
            row.BeginEdit();
            foreach (string key in dic.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }
                object value = dic[key];
                if (defaultValue != null)
                {
                    value = defaultValue.CheckValue(value, typeof(string));
                }
                row[key] = value;
            }
            row.EndEdit();
            #endregion

            return table;
        }



        /// <summary>
        /// codeStarter:根据它进行分行处理
        /// </summary>
        /// <returns></returns>
        public static DataTable StringDicToDataTable(Dictionary<string, string> dic, DefaultValue defaultValue, string codeStarter, int maxRowCount, int copysInPage, int forcePageCount)
        {
            if (string.IsNullOrEmpty(codeStarter))
            {
                return StringDicToDataTable(dic, defaultValue);
            }
            if (dic == null)
            {
                return new DataTable();
            }

            #region 计算最大页数
            int maxPageCount = maxRowCount / copysInPage;
            if (maxRowCount % copysInPage > 0)
            {
                maxPageCount++;
            }
            #endregion

            #region 字段所在的行数计算、字段计算
            Dictionary<string, CodePageInfo> dic2 = new Dictionary<string, CodePageInfo>();
            foreach (string key in dic.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }
                CodePageInfo cpi = GetPageIndexWhenStringDicToDataTable(codeStarter, copysInPage, key);
                if (cpi.error)//数据问题，有可能转换错误
                {
                    continue;
                }
                dic2.Add(key, cpi);
            }
            #endregion

            #region 准备数据集
            DataTable table = new DataTable();
            string codeStarterCopy = codeStarter + "copy";
            foreach (string key in dic2.Keys)
            {
                CodePageInfo cpi = dic2[key];
                if (cpi.pageIndex > 1)  //不是第1页，不处理
                {
                    continue;
                }
                if (cpi.pageCopyIndex > 1)//不是第1页的第1行，不处理（其他行在处理第1行时强制添加，因为如果需要3行，可能实际只有2行）
                {
                    continue;
                }
                //添加第1页第1行
                DataColumn column = new DataColumn(cpi.nowField, typeof(string));
                table.Columns.Add(column);
                //添加第1页其他行
                //2018.07.24.由于历史数据原因，d_root_htsj_l3copy2_pjz的原始项目d_root_htsj_l3_pjz不存在，字段会无法生成
                if (cpi.nowField.IndexOf(codeStarter) == 0)
                {
                    for (int i = 2; i <= copysInPage; i++)
                    {
                        string copyedKey = GetCopyedKey(codeStarter, cpi.nowField, i);
                        DataColumn column2 = new DataColumn(copyedKey, typeof(string));
                        table.Columns.Add(column2);
                    }
                }
            }
            if (!table.Columns.Contains("gv_rowindex"))
            {
                DataColumn column = new DataColumn("gv_rowindex", typeof(int));
                table.Columns.Add(column);
            }
            #endregion

            #region 准备数据行、对通用字段赋值
            int rowCount = 1;
            foreach (string key in dic2.Keys)
            {
                CodePageInfo cpi = dic2[key];
                rowCount = cpi.pageIndex > rowCount ? cpi.pageIndex : rowCount;
            }
            rowCount = maxPageCount < rowCount ? maxPageCount : rowCount;  //行数限制(放弃多余数据)
            if (forcePageCount != -1)
            {
                rowCount = forcePageCount;  //指定了强制行数，按此行数处理
            }
            for (int i = 1; i <= rowCount; i++)
            {
                DataRow row = table.NewRow();
                row.BeginEdit();
                row["gv_rowindex"] = i;
                foreach (string key in dic2.Keys)
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }
                    if (key.IndexOf(codeStarter) == 0)
                    {
                        continue;//只处理通用字段
                    }
                    object value = dic[key];
                    if (defaultValue != null)
                    {
                        value = defaultValue.CheckValue(value, typeof(string));
                    }
                    row[key] = value;
                }
                row.EndEdit();
                table.Rows.Add(row);
            }
            #endregion

            #region 对行的特有字段赋值
            foreach (string key in dic.Keys)
            {
                if (key.IndexOf(codeStarter) < 0)
                {
                    continue;//通用字段不用处理了
                }
                if (!dic2.ContainsKey(key))//
                {
                    continue;
                }
                CodePageInfo cpi = dic2[key];
                if (cpi.pageIndex > rowCount)
                {
                    continue;//多余数据不处理
                }
                if (cpi.realCopyIndex > maxRowCount)
                {
                    continue;//多余数据不处理
                }
                DataRow row = table.Rows[cpi.pageIndex - 1];
                //row.BeginEdit();
                object value = dic[key];
                if (defaultValue != null)
                {
                    value = defaultValue.CheckValue(value, typeof(string));
                }
                //2018.07.24.由于历史数据原因，d_root_htsj_l3copy2_pjz的原始项目d_root_htsj_l3_pjz不存在，字段会无法生成
                if (row.Table.Columns.Contains(cpi.nowField))
                {
                    row[cpi.nowField] = value;
                }
                //row.EndEdit();
            }
            #endregion

            #region 检查默认值
            if (defaultValue != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        object value = row[col];
                        if (value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
                        {
                            value = defaultValue.CheckValue(value, typeof(string));
                            row[col] = value;
                        }
                    }
                }
            }
            #endregion

            return table;
        }


        public static DataTable DataTableRowToColumn(DataTable dt, int rowsInPage, DefaultValue defaultValue)
        {
            if (rowsInPage <= 1)
            {
                return dt;
            }

            //计算需要的页数
            int pageCount = dt.Rows.Count / rowsInPage;
            if (dt.Rows.Count % rowsInPage > 0)
            {
                pageCount++;
            }

            //添加列
            DataTable newDt = new DataTable();
            for (int i = 0; i < rowsInPage; i++)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    string colName = col.ColumnName + (i + 1).ToString();
                    newDt.Columns.Add(colName, col.DataType);
                }
            }

            //设置值
            for (int pi = 0; pi < pageCount; pi++)
            {
                DataRow newRow = newDt.NewRow();
                for (int i = 0; i < rowsInPage; i++)
                {
                    int rowIndex = i + pi * rowsInPage;
                    DataRow row = dt.Rows.Count > rowIndex ? dt.Rows[rowIndex] : null;
                    foreach (DataColumn col in dt.Columns)
                    {
                        string colName = col.ColumnName + (i + 1).ToString();
                        if (row != null)
                        {
                            newRow[colName] = row[col];
                        }
                        if (newRow[colName] == DBNull.Value && defaultValue != null)  //默认值
                        {
                            if (col.DataType == typeof(string))
                            {
                                newRow[colName] = defaultValue.DefaultString;
                            }
                            else if (col.DataType == typeof(int) || col.DataType == typeof(Int32) || col.DataType == typeof(Int64))
                            {
                                newRow[colName] = defaultValue.DefaultInt;
                            }
                            else if (col.DataType == typeof(float))
                            {
                                newRow[colName] = defaultValue.DefaultFloat;
                            }
                            else if (col.DataType == typeof(double))
                            {
                                newRow[colName] = defaultValue.DefaultDouble;
                            }
                        }
                    }
                }
                newDt.Rows.Add(newRow);
            }

            return newDt;
        }

        /// <summary>
        /// 获取指定的复制份数的编码
        /// </summary>
        /// <param name="codeStarter"></param>
        /// <param name="key"></param>
        /// <param name="copyIndex"></param>
        /// <returns></returns>
        private static string GetCopyedKey(string codeStarter, string key, int copyIndex)
        {
            string s = key.Substring(codeStarter.Length);
            return codeStarter + "copy" + copyIndex + s;
        }

        class CodePageInfo
        {
            public string nowField;
            public int pageIndex;
            public int realCopyIndex = 1;
            public int pageCopyIndex = 1;
            public bool error { get; set; }
        }

        //从1开始
        private static CodePageInfo GetPageIndexWhenStringDicToDataTable(string codeStarter, int copysInPage, string nowField)
        {
            CodePageInfo rpi = new CodePageInfo();
            rpi.nowField = nowField;

            //计算rowIndex
            int realCopyIndex = 1;
            int pageCopyIndex = 1;
            int pageIndex = 1;

            string codeStarterCopy = codeStarter + "copy";
            if (nowField.IndexOf(codeStarterCopy) == 0)
            {
                string s = nowField.Substring(codeStarterCopy.Length);
                int index = s.IndexOf("_");
                if (index <= 0)
                {
                    try
                    {
                        realCopyIndex = Convert.ToInt32(s);
                        pageCopyIndex = GetPageCopyIndex(realCopyIndex, copysInPage, ref pageIndex);
                        rpi.nowField = codeStarter + (pageCopyIndex == 1 ? "" : "copy" + pageCopyIndex);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    string s2 = s.Substring(0, index);
                    try
                    {
                        realCopyIndex = Convert.ToInt32(s2);//数据问题，有可能转换错误
                    }
                    catch
                    {
                        rpi.error = true;
                        return rpi;
                    }
                    pageCopyIndex = GetPageCopyIndex(realCopyIndex, copysInPage, ref pageIndex);
                    rpi.nowField = codeStarter + (pageCopyIndex == 1 ? "" : "copy" + pageCopyIndex) + s.Substring(index);
                }
            }

            rpi.pageIndex = pageIndex;
            rpi.realCopyIndex = realCopyIndex;
            rpi.pageCopyIndex = pageCopyIndex;
            if (rpi.pageIndex <= 0)
            {
                rpi.error = true;
            }
            return rpi;
        }

        /// <summary>
        /// 根据实际的复制序号、每页行数，计算数据在分页后的页中的复制序号
        /// </summary>
        /// <param name="realCopyIndex"></param>
        /// <param name="copysInPage"></param>
        /// <returns></returns>
        private static int GetPageCopyIndex(int realCopyIndex, int copysInPage, ref int pageIndex)
        {
            if (copysInPage == 1)
            {
                pageIndex = realCopyIndex;
                return 1; //1页1行时，返回1
            }

            int ys = realCopyIndex % copysInPage;
            if (ys == 0)
            {
                pageIndex = (realCopyIndex / copysInPage);
                return copysInPage;
            }
            else
            {
                pageIndex = (realCopyIndex / copysInPage) + 1;
                return ys;
            }
        }

        private static string EnumToStr(object value, Type enumType)
        {
            if (value == null)
            {
                return "";
            }

            if (Enum.IsDefined(enumType, value))
            {
                value = Enum.GetName(enumType, value);
                return value.ToString();
            }

            return "";
            //return value.ToString();
        }


        #endregion

        #region 其他


        /// <summary>
        /// 新的GUID值
        /// </summary>
        public static string NewGuid
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

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
        /// 返回数据库下所有的表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTableNameList()
        {
            DataTable dataTable = null;
            using (IDbConnection connection = I3DBUtil.CreateAndOpenDbConnection())
            {
                string sql = "";
                switch (DBServerType)
                {
                    case DBServerType.SqlServer:
                        sql = "SELECT name FROM sysobjects WHERE xtype='u'";
                        break;
                    case DBServerType.Oracle:
                        throw new Exception("未实现");
                    default:
                        sql = "show tables";
                        break;
                }
                dataTable = I3DbHelper.ExecuteDataset(connection, CommandType.Text, sql).Tables[0];
            }

            List<string> result = new List<string>();
            DataColumn column = dataTable.Columns[0];
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(row[column].ToString());
            }
            return result;
        }

        /// <summary>
        /// 获取当前默认数据库中当前登陆用户某个数据表的列信息数组
        /// </summary>
        /// <param name="sTblName"><see cref="string"/>，数据表名</param>
        /// <returns><see cref="IE310.CodeMaker.Core.Data.ColumnInfo"/>[]，返回当前默认数据库中当前登陆用户指定数据表的列信息数组</returns>
        public static I3TableInfo GetTableInfo(string databaseName, string tableName, bool tableNeedUnderLine, bool fieldNeedUnderLine)
        {
            I3TableInfo result = new I3TableInfo(databaseName, tableName, tableNeedUnderLine, fieldNeedUnderLine);
            return result;
        }

        #endregion
    }


    /// <summary>
    /// 时间段参数，用来处理基于时间的参数方法，在自动生成查询方案时使用。
    /// 实际处理中，使用大于等于最小日期，小于最大日期的模式。
    /// </summary>
    [Serializable]
    public class I3DateSpanParam
    {
        private DateTime startDate;
        private DateTime endDate;
        /// <summary>
        /// 获取或设置起始时间
        /// </summary>
        public DateTime Start
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }
        /// <summary>
        /// 获取或设置截止时间
        /// </summary>
        public DateTime End
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }
        public I3DateSpanParam(DateTime start, DateTime end)
        {
            startDate = start;
            endDate = end;
        }
        public I3DateSpanParam(DateTime start, TimeSpan span)
        {
            startDate = start;
            endDate = start.Add(span);
        }
        public I3DateSpanParam(DateTime start, int iMonth)
        {
            startDate = start;
            endDate = start.AddMonths(iMonth);
        }
        public IDbDataParameter[] PrepareParams(string sCol, StringBuilder sqlBuf)
        {
            return PrepareParams(sCol, sCol, sqlBuf);


        }
        public IDbDataParameter[] PrepareParams(string sStartCol, string sEndCol, StringBuilder sqlBuf)
        {
            string sParam1 = ":W_START_DATE" + sqlBuf.Length;
            string sParam2 = ":W_END_DATE" + sqlBuf.Length;
            ArrayList paramList = new ArrayList();
            IDbDataParameter param = null;
            if (startDate != I3DBUtil.INVALID_DATE)
            {
                sqlBuf.Append(" And ").Append(sStartCol).Append(">=").Append(sParam1);
                paramList.Add(I3DBUtil.PrepareParameter(sParam1, DbType.DateTime, startDate));
            }
            if (endDate != I3DBUtil.INVALID_DATE)
            {
                sqlBuf.Append(" And ").Append(sEndCol).Append("<").Append(sParam2).Append(" ");
                paramList.Add(I3DBUtil.PrepareParameter(sParam2, DbType.DateTime, endDate));
            }
            return (IDbDataParameter[])paramList.ToArray(typeof(IDbDataParameter));
        }
    }


    /// <summary>
    /// 查询参数定义,包括默认的查询参数，分页参数及排序参数
    /// 支持序列化
    /// </summary>
    [Serializable]
    public abstract class QueryParamInfo
    {
        private I3OrderParams orderParam;
        private I3PageInfo pageInfo;
        public QueryParamInfo()
        {


        }
        /// <summary>
        /// 排序参数
        /// </summary>
        public I3OrderParams OrderParams
        {
            get
            {
                return orderParam;
            }
            set
            {
                orderParam = value;
            }
        }
        /// <summary>
        /// 分页参数
        /// </summary>
        public I3PageInfo PagingInfo
        {
            get
            {
                return pageInfo;
            }
            set
            {
                pageInfo = value;
            }
        }
    }


    /// <summary>
    /// 用来处理分页的帮助类，用来传入和输出参数
    /// 从第0行开始，第0页开始
    /// </summary>
    [Serializable]
    public class I3PageInfo
    {
        /// <summary>
        /// 私有变量 <c>iTotalCount</c>表示总记录数
        /// </summary>
        private int iTotalCount;

        /// <summary>
        /// 私有变量 <c>iPageCount</c>表示页面总数
        /// </summary>
        private int iPageCount;

        /// <summary>
        /// 私有变量 <c>iPageCount</c>表示当前页面序号
        /// </summary>
        private int iPageIndex;

        /// <summary>
        /// 私有变量 <c>iRowsInPage</c>表示每页数据行数
        /// </summary>
        private int iRowsInPage;

        /// <summary>
        /// 获取或设置总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return iTotalCount;
            }
            set
            {
                iTotalCount = value;
            }
        }

        /// <summary>
        /// 获取或设置每页数据行数
        /// </summary>
        public int RowsInPage
        {
            get
            {
                return iRowsInPage;
            }
            set
            {
                iRowsInPage = value;
            }
        }

        /// <summary>
        /// 获取或设置页面总数
        /// </summary>
        public int PageCount
        {
            get
            {
                return iPageCount;
            }
            set
            {
                iPageCount = value;
            }
        }

        /// <summary>
        /// 获取或设置当前页面序号
        /// </summary>
        public int PageIndex
        {
            get
            {
                return iPageIndex;
            }
            set
            {
                iPageIndex = value;
            }
        }

        /// <summary>
        /// 初始化<b>PageInfo</b>类的新实例
        /// </summary>
        /// <param name="iPageIndex"><see cref="int"/>，页面序号</param>
        /// <param name="iRowsInPage"><see cref="int"/>，每页数据行数</param>
        public I3PageInfo(int iPageIndex, int iRowsInPage)
            : this(iPageIndex, iRowsInPage, 0)
        {
        }
        /// <summary>
        /// 初始化<b>PageInfo</b>类的新实例
        /// </summary>
        /// <param name="iPageIndex"><see cref="int"/>，页面序号</param>
        /// <param name="iRowsInPage"><see cref="int"/>，每页数据行数</param>
        public I3PageInfo(int iPageIndex, int iRowsInPage, int iTotalRows)
        {
            this.iPageIndex = iPageIndex;
            this.iRowsInPage = iRowsInPage;
            this.PageCount = (int)Math.Ceiling(1.0 * iTotalRows / this.iRowsInPage);
        }
        /// <summary>
        /// 初始化<b>PageInfo</b>类的新实例
        /// </summary>
        /// <param name="iRowsInPage"><see cref="int"/>，每页显示的行数</param>
        public I3PageInfo(int iRowsInPage)
        {
            this.iRowsInPage = iRowsInPage;
        }
        /// <summary>
        /// 初始化<b>PageInfo</b>类的新实例
        /// </summary>
        /// <remarks>缺省每页显示10行</remarks>
        public I3PageInfo()
        {
            this.iRowsInPage = 10;
        }
    }

    /// <summary>
    /// 用来构造SQL语句中的排序（Order By）部分的类
    /// </summary>
    [Serializable]
    public class I3OrderParams
    {
        /// <summary>
        /// 用于构造排序语句的可变字符字符串
        /// </summary>
        private StringBuilder strBuf = new StringBuilder();

        /// <summary>
        /// 初始化<b>OrderParams</b>类的新实例
        /// </summary>
        public I3OrderParams()
        {
        }

        /// <summary>
        /// 初始化<b>OrderParams</b>类的新实例
        /// </summary>
        /// <param name="colName"><see cref="string"/>，字段名称</param>
        /// <param name="bAsc"><see cref="bool"/>，排序方式；true表示Asc升序（从小到大排序），false表示Desc降序（从大到小排序）
        /// </param>
        public I3OrderParams(string colName, bool bAsc)
        {
            Append(colName, bAsc);
        }

        /// <summary>
        /// 初始化<b>OrderParams</b>类的新实例
        /// </summary>
        /// <param name="colName"><see cref="string"/>，字段名称</param>
        /// <remarks>字段排序方式为从小到大排序（ASC 升序）</remarks>
        public I3OrderParams(string colName)
        {
            Append(colName, true);
        }

        public I3OrderParams Append(string orderString)
        {
            if (strBuf.Length > 0)
                strBuf.Append(",");
            strBuf.Append(orderString.ToUpper());
            return this;
        }

        /// <summary>
        /// 增加一个排序字段
        /// </summary>
        /// <param name="colName"><see cref="string"/>，字段名称</param>
        /// <param name="bAsc"><see cref="bool"/>，排序方式；true表示Asc升序（从小到大排序），false表示Desc降序（从大到小排序）
        /// </param>
        /// <returns><see cref="IE310.CodeMaker.Core.Data.OrderParams"/>，返回当前排序语句构造对象</returns>
        public I3OrderParams Append(string colName, bool bAsc)
        {
            if (colName == null || colName.Length == 0)
                return this;
            if (strBuf.Length > 0)
                strBuf.Append(",");
            strBuf.Append(colName.ToUpper());
            strBuf.Append(bAsc ? " " : " Desc ");
            return this;
        }

        /// <summary>
        /// 返回当前构造的SQL语句排序部分
        /// </summary>
        /// <returns><see cref="string"/>，返回当前构造的排序SQL语句排序部分</returns>
        public override string ToString()
        {
            if (strBuf.Length == 0)
                return "";
            return " Order By " + strBuf.ToString();
        }


        public static I3OrderParams Build(string orderByString, string defaultColName, bool defaultAsc)
        {
            if (string.IsNullOrEmpty(orderByString))
            {
                I3OrderParams result = new I3OrderParams(defaultColName, defaultAsc);
                return result;
            }
            else
            {
                I3OrderParams result = new I3OrderParams();
                result.Append(orderByString);
                return result;
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

    public class I3TableInfo : List<I3ColumnInfo>
    {
        private bool tableNeedUnderLine;
        private bool fieldNeedUnderLine;
        public I3TableInfo(string databaseName, string tableName, bool tableNeedUnderLine, bool fieldNeedUnderLine)
        {
            this.tableNeedUnderLine = tableNeedUnderLine;
            this.fieldNeedUnderLine = fieldNeedUnderLine;
            this.tableName = tableName;
            DbDataReader tableInfoReader;

            string sql = "";
            switch (I3DBUtil.DBServerType)
            {
                case DBServerType.SqlServer:
                    sql = @"select sys.columns.name CNAME,
                   sys.types.name COLTYPE,
                   sys.columns.max_length WIDTH,
                   sys.columns.precision PRECISION,
                   sys.columns.scale SCALE,
                   (select value
                      from sys.extended_properties
                     where sys.extended_properties.major_id = sys.columns.object_id
                       and sys.extended_properties.minor_id = sys.columns.column_id) as comments
              from sys.columns, sys.tables, sys.types
             where sys.tables.object_id = sys.columns.object_id
               and sys.types.user_type_id = sys.columns.user_type_id
               and sys.tables.name = '" + tableName + "'";
                    break;
                case DBServerType.Oracle:
                    sql = @"select CNAME,COLTYPE, WIDTH,PRECISION,SCALE,com.comments from col,dba_col_Comments com
				Where com.table_name(+) = col.tname and col.cname = com.column_name(+) And  com.OWNER=(select username from v$session where sid in (select distinct sid from v$mystat)) And TNAME='" + tableName + "' Order By COLNO";
                    break;
                default:
                    sql = @"select column_name,data_type,
(case when character_maximum_length is null then NUMERIC_precision else character_maximum_length end ) MaxCount,
(case when character_maximum_length is null then NUMERIC_precision else character_maximum_length end ) DataPrecision,
(case when numeric_scale is null then 0 else numeric_scale end ) DataScale,
COLUMN_comment, EXTRA 
from information_schema.columns where table_name = '" + tableName + "' AND table_schema='" + databaseName + "' order by ordinal_position";
                    break;
            }
            using (tableInfoReader = I3DbHelper.ExecuteReader(I3DBUtil.DBServerType, I3DBUtil.ConnectionString, CommandType.Text, sql))
            {
                while (tableInfoReader.Read())
                {
                    I3ColumnInfo info = new I3ColumnInfo(this.fieldNeedUnderLine);
                    info.Name = tableInfoReader.GetString(0);
                    info.TypeName = tableInfoReader.GetString(1);
                    info.Width = 0;
                    if (!tableInfoReader.IsDBNull(2))
                    {
                        info.Width = Convert.ToInt64(tableInfoReader.GetValue(2).ToString());
                    }
                    info.Precision = 0;
                    if (!tableInfoReader.IsDBNull(3))
                    {
                        info.Precision = Convert.ToInt64(tableInfoReader.GetValue(3).ToString());
                    }
                    info.Scale = 0;
                    if (!tableInfoReader.IsDBNull(4))
                    {
                        info.Scale = Convert.ToInt32(tableInfoReader.GetValue(4).ToString());
                    }
                    info.Comment = tableInfoReader.GetValue(5).ToString();

                    //2017.04.20   字段是否自增   目前只实现了mysql，修改了IdColumnIsAutoInt的逻辑（增加AutoInc判断），可能会影响之前的代码
                    try
                    {
                        object obj = tableInfoReader.GetValue(6);
                        if (obj != null && obj != DBNull.Value && obj.ToString().IndexOf("auto_increment") >= 0)
                        {
                            info.AutoInc = true;
                        }
                    }
                    catch
                    {
                    }

                    this.Add(info);
                }
            }
        }

        private string tableName;
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        /// <summary>
        /// 不带下划线
        /// </summary>
        public string TableNameEx
        {
            get
            {
                if (this.tableNeedUnderLine)
                {
                    return this.tableName.Substring(0, 1).ToUpper() + this.tableName.Substring(1);
                }
                else
                {
                    string[] arr = TableName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sb = new StringBuilder();
                    foreach (string str in arr)
                    {
                        string tmp = str.Substring(0, 1).ToUpper() + str.Substring(1);
                        sb.Append(tmp);
                    }
                    return sb.ToString();
                }
            }
        }


        /// <summary>
        /// 不带下划线  且第一个字母小写
        /// </summary>
        public string TableNameEx2
        {
            get
            {
                string result = TableNameEx;
                return result.Substring(0, 1).ToLower() + result.Substring(1);
            }
        }

        public string EntityClassName
        {
            get
            {
                return TableNameEx + "Entity";
            }
        }

        public string EntityInstanceName
        {
            get
            {
                //return "_" + EntityClassName;
                return "entity";
            }
        }

        public string EntitiesClassName
        {
            get
            {
                return TableNameEx + "Entities";
            }
        }

        public string ParamsClassName
        {
            get
            {
                return TableNameEx + "Params";
            }
        }

        public string ParamsInstanceName
        {
            get
            {
                //return "_" + ParamsClassName;
                return "paramObj";
            }
        }

        public string HelperClassName
        {
            get
            {
                return TableNameEx + "Helper";
            }
        }

        public string ManagerClassName
        {
            get
            {
                return TableNameEx + "Manager";
            }
        }

        public string ServiceClassName
        {
            get
            {
                return TableNameEx + "Service";
            }
        }

        /// <summary>
        /// 全部设置为值参数
        /// </summary>
        public void ResetToValueParams()
        {
            foreach (I3ColumnInfo columnInfo in this)
            {
                columnInfo.IsCauseParam = false;
                columnInfo.IsValueParam = true;
                columnInfo.OrderBy = I3OrderByType.None;
            }
        }

        /// <summary>
        /// 全部设置为条件参数
        /// </summary>
        public void ResetToCauseParams()
        {
            foreach (I3ColumnInfo columnInfo in this)
            {
                columnInfo.IsCauseParam = true;
                columnInfo.IsValueParam = false;
                columnInfo.OrderBy = I3OrderByType.None;
            }
        }

        /// <summary>
        /// 全部设置为既不是值参数，也不是属性参数
        /// </summary>
        public void ResetParams()
        {
            foreach (I3ColumnInfo columnInfo in this)
            {
                columnInfo.IsCauseParam = false;
                columnInfo.IsValueParam = false;
                columnInfo.OrderBy = I3OrderByType.None;
            }
        }



        /// <summary>
        /// 是否有值参数
        /// </summary>
        public bool HasValueParam
        {
            get
            {
                foreach (I3ColumnInfo columnInfo in this)
                {
                    if (columnInfo.IsValueParam)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 是否有查询条件
        /// </summary>
        public bool HasCauseParam
        {
            get
            {
                foreach (I3ColumnInfo columnInfo in this)
                {
                    if (columnInfo.IsCauseParam)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 是不是有主键
        /// </summary>
        public bool HasIdKey
        {
            get
            {
                foreach (I3ColumnInfo columnInfo in this)
                {
                    if (columnInfo.Name.ToUpper().Equals("ID"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public I3ColumnInfo IdColumnInfo
        {
            get
            {
                foreach (I3ColumnInfo columnInfo in this)
                {
                    if (columnInfo.Name.ToUpper().Equals("ID"))
                    {
                        return columnInfo;
                    }
                }
                //throw new Exception(string.Format("表{0}没有id字段", this.tableName));
                //2017.12.29 为了兼容其他项目的数据库，没有id字段时，不抛异常，只是返回null，相关代码不生成
                return null;
            }
        }


        public string IdColumnDataType(bool forJava)
        {
            if (!this.HasIdKey)
            {
                return "String";
            }
            I3ColumnInfo idCol = this.IdColumnInfo;
            string idNetType = forJava ? DBTypeMapUtil.GetJavaType(I3DBUtil.DBServerType.ToString(), idCol.TypeName)
                                                         : DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
            return idNetType;
        }

        public string IdColumnGetMethodForGoService()
        {
            if (!this.HasIdKey)
            {
                return "string";
            }
            I3ColumnInfo idCol = this.IdColumnInfo;
            string idType = DBTypeMapUtil.GetGoType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
            return idType == "int" ? "args.GetIntParam(\"id\")" : "args.GetStringParamWithCheck(\"id\", false)";
        }
        public string IdColumnTypeForGo()
        {
            if (!this.HasIdKey)
            {
                return "string";
            }
            I3ColumnInfo idCol = this.IdColumnInfo;
            string idType = DBTypeMapUtil.GetGoType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
            return idType;
        }


        public bool IdColumnIsAutoInt
        {
            get
            {
                I3ColumnInfo idCol = this.IdColumnInfo;
                if (idCol == null)
                {
                    return false;
                }

                string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
                return idCol.AutoInc && idNetType == "int";
            }
        }
        public bool IdColumnIsInt
        {
            get
            {
                I3ColumnInfo idCol = this.IdColumnInfo;
                if (idCol == null)
                {
                    return false;
                }

                string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
                return idNetType == "int";
            }
        }

        public bool IdColumnIsGuid
        {
            get
            {
                I3ColumnInfo idCol = this.IdColumnInfo;
                if (idCol == null)
                {
                    return false;
                }

                string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
                return idNetType == "string" && idCol.Width == 36;
            }
        }

        /// <summary>
        /// 是不是自增id字段
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsAutoIntIdColumn(I3ColumnInfo col)
        {
            if (col == null)
            {
                return false;
            }
            string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), col.TypeName);
            return col.AutoInc && idNetType == "int" && col.Name.Equals(this.IdColumnInfo.Name);
        }

        /// <summary>
        /// 是不是自增字段
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsAutoIntColumn(I3ColumnInfo col)
        {
            if (col == null)
            {
                return false;
            }
            string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), col.TypeName);
            return col.AutoInc && idNetType == "int";
        }

        public void SetOrderByParam(string sColName, I3OrderByType orderByType)
        {
            sColName = sColName.ToUpper();
            foreach (I3ColumnInfo columnInfo in this)
            {
                if (columnInfo.Name.ToUpper() == sColName)
                {
                    columnInfo.OrderBy = orderByType;
                    return;
                }
            }
        }

        public void SetParam(string sColName, bool bCauseParam, bool bValueParam, I3OrderByType orderByType)
        {
            SetParam(sColName, bCauseParam, bValueParam);
            SetOrderByParam(sColName, orderByType);
        }

        public void SetParam(string sColName, bool bCauseParam, bool bValueParam)
        {
            foreach (I3ColumnInfo columnInfo in this)
            {
                if (columnInfo.Name.Equals(sColName, StringComparison.OrdinalIgnoreCase))
                {
                    columnInfo.IsCauseParam = bCauseParam;
                    columnInfo.IsValueParam = bValueParam;
                    return;
                }
            }
        }

        /// <summary>
        /// 获取字段名数组
        /// </summary>
        /// <param name="hasId"></param>
        /// <returns></returns>
        public string[] GetFields(bool hasId)
        {
            List<string> list = new List<string>();
            foreach (I3ColumnInfo col in this)
            {
                if (col.Name.ToUpper().Equals("ID") && !hasId)
                {
                    continue;
                }
                list.Add(col.Name);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取字段名排列，用指定符号分隔
        /// </summary>
        /// <param name="hasId"></param>
        /// <returns></returns>
        public string GetFieldsStr(bool hasId, string split)
        {
            string[] fields = GetFields(hasId);
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string field in fields)
            {
                sb.Append(field);
                if (i < fields.Length - 1)
                {
                    sb.Append(split);
                }
                i++;
            }
            return sb.ToString();
        }
    }


    /// <summary>
    /// 数据表的列信息类
    /// </summary>
    public class I3ColumnInfo
    {
        private bool needUnderline;
        public I3ColumnInfo(bool needUnderline)
        {
            this.needUnderline = needUnderline;
        }

        private bool autoInc = false;

        public bool AutoInc
        {
            get
            {
                return autoInc;
            }
            set
            {
                autoInc = value;
            }
        }


        public string PropertyName
        {
            get
            {
                if (needUnderline)
                {
                    return this.sname;
                }
                else
                {
                    return this.sname.Replace("_", "");
                }
            }
        }
        public string FieldName
        {
            get
            {
                return "_" + PropertyName.Substring(0, 1).ToLower() + PropertyName.Substring(1);
            }
        }
        public string ParamName
        {
            get
            {
                return "_" + PropertyName.Substring(0, 1).ToLower() + PropertyName.Substring(1);
            }
        }
        public string ValueParamterName
        {
            get
            {
                return "@V_" + PropertyName;
            }
        }
        public string WhereParamterName
        {
            get
            {
                return "@W_" + PropertyName;
            }
        }

        /// <summary>
        /// 列名称
        /// </summary>
        private string sname;

        /// <summary>
        /// 列数据长度
        /// </summary>
        private long width;
        //private int type;

        /// <summary>
        /// 列数据类型名称
        /// </summary>
        private string typename;

        /// <summary>
        /// 用于表示列值的最大位数
        /// </summary>
        private long precision;

        /// <summary>
        /// 用于将列值解析为的小数位数
        /// </summary>
        private int scale;

        /// <summary>
        /// 列的备注
        /// </summary>
        private string comment;

        /// <summary>
        /// 获取或设置列名称
        /// </summary>
        public string Name
        {
            set
            {
                sname = value;
            }
            get
            {
                return sname;
            }
        }

        /// <summary>
        /// 获取或设置列数据长度
        /// </summary>
        public long Width
        {
            set
            {
                width = value;
            }
            get
            {
                return width;
            }
        }

        /// <summary>
        /// 获取或设置列备注
        /// </summary>
        public string Comment
        {
            set
            {
                comment = value;
            }
            get
            {
                return comment;
            }
        }

        /// <summary>
        /// 获取或设置列数据类型名称
        /// </summary>
        public string TypeName
        {
            set
            {
                typename = value;
            }
            get
            {
                if (string.IsNullOrEmpty(typename))
                    return null;
                return typename.ToUpper();
            }
        }

        /// <summary>
        /// 获取或设置列值的最大长度
        /// </summary>
        public long Precision
        {
            set
            {
                precision = value;
            }
            get
            {
                return precision;
            }
        }

        /// <summary>
        /// 获取或设置用于将列值解析为的小数位数
        /// </summary>
        public int Scale
        {
            set
            {
                scale = value;
            }
            get
            {
                return scale;
            }
        }

        private bool isCauseParam;
        public bool IsCauseParam
        {
            get
            {
                return isCauseParam;
            }
            set
            {
                isCauseParam = value;
            }
        }

        private bool isValueParam;
        public bool IsValueParam
        {
            get
            {
                return isValueParam;
            }
            set
            {
                isValueParam = value;
            }
        }

        private I3OrderByType orderBy = I3OrderByType.None;
        public I3OrderByType OrderBy
        {
            get
            {
                return orderBy;
            }
            set
            {
                orderBy = value;
            }
        }
    }


    public enum I3OrderByType
    {
        None,
        Asc,
        Desc
    }
}
