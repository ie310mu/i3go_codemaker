using System;
using System.Collections.Generic;
using System.Text;
using IE310.Core.DB;
using System.Data;
using MySql.Data.MySqlClient;

namespace IE310.Core.DB
{
    public static class DBTypeMapUtil
    {
        private static Dictionary<string, Dictionary<string, string>> dbTypeMap;
        /// <summary>
        /// 数据库字段类型到.net类型的映射
        /// Dic<数据库类型大写,Dic<数据库字段类型大写,.net类型字符串>>
        /// 如:"MYSQL","INT","int"
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> DbTypeMap
        {
            get
            {
                if (dbTypeMap == null)
                {
                    dbTypeMap = new Dictionary<string, Dictionary<string, string>>();
                    string[] arr = Resource.DBTypeMap.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Dictionary<string, string> dic = null;
                    foreach (string str in arr)
                    {
                        if (str.IndexOf("[") == 0)
                        {
                            dic = new Dictionary<string, string>();
                            dbTypeMap.Add(str.Replace("[", "").Replace("]", "").ToUpper(), dic);
                        }
                        else
                        {
                            string[] arr2 = str.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            dic.Add(arr2[0].ToUpper(), arr2[1]);
                        }
                    }
                }
                return dbTypeMap;
            }
        }


        private static Dictionary<string, Dictionary<string, string>> dbTypeMapJava;
        /// <summary>
        /// 数据库字段类型到.java类型的映射
        /// Dic<数据库类型大写,Dic<数据库字段类型大写,.net类型字符串>>
        /// 如:"MYSQL","INT","int"
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> DbTypeMapJava
        {
            get
            {
                if (dbTypeMapJava == null)
                {
                    dbTypeMapJava = new Dictionary<string, Dictionary<string, string>>();
                    string[] arr = Resource.DBTypeMapJava.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Dictionary<string, string> dic = null;
                    foreach (string str in arr)
                    {
                        if (str.IndexOf("[") == 0)
                        {
                            dic = new Dictionary<string, string>();
                            dbTypeMapJava.Add(str.Replace("[", "").Replace("]", "").ToUpper(), dic);
                        }
                        else
                        {
                            string[] arr2 = str.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            dic.Add(arr2[0].ToUpper(), arr2[1]);
                        }
                    }
                }
                return dbTypeMapJava;
            }
        }

        private static Dictionary<string, Dictionary<string, string>> dbTypeMapGo;
        /// <summary>
        /// 数据库字段类型到.go类型的映射
        /// Dic<数据库类型大写,Dic<数据库字段类型大写,.net类型字符串>>
        /// 如:"MYSQL","INT","int"
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> DbTypeMapGo
        {
            get
            {
                if (dbTypeMapGo == null)
                {
                    dbTypeMapGo = new Dictionary<string, Dictionary<string, string>>();
                    string[] arr = Resource.DBTypeMapGo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Dictionary<string, string> dic = null;
                    foreach (string str in arr)
                    {
                        if (str.IndexOf("[") == 0)
                        {
                            dic = new Dictionary<string, string>();
                            dbTypeMapGo.Add(str.Replace("[", "").Replace("]", "").ToUpper(), dic);
                        }
                        else
                        {
                            string[] arr2 = str.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            dic.Add(arr2[0].ToUpper(), arr2[1]);
                        }
                    }
                }
                return dbTypeMapGo;
            }
        }

        public static string GetNetType(string dbServerType, string columnTypeName)
        {
            dbServerType = dbServerType.ToUpper();
            columnTypeName = columnTypeName.ToUpper();

            if (DbTypeMap.ContainsKey(dbServerType) && DbTypeMap[dbServerType].ContainsKey(columnTypeName))
            {
                return DbTypeMap[dbServerType][columnTypeName];
            }
            else
            {
                return null;
            }
        }


        public static string GetJavaType(string dbServerType, string columnTypeName)
        {
            dbServerType = dbServerType.ToUpper();
            columnTypeName = columnTypeName.ToUpper();

            if (DbTypeMapJava.ContainsKey(dbServerType) && DbTypeMapJava[dbServerType].ContainsKey(columnTypeName))
            {
                return DbTypeMapJava[dbServerType][columnTypeName];
            }
            else
            {
                return null;
            }
        }

        public static string GetGoType(string dbServerType, string columnTypeName)
        {
            dbServerType = dbServerType.ToUpper();
            columnTypeName = columnTypeName.ToUpper();

            if (DbTypeMapGo.ContainsKey(dbServerType) && DbTypeMapGo[dbServerType].ContainsKey(columnTypeName))
            {
                return DbTypeMapGo[dbServerType][columnTypeName];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 返回类型于MySqlDbType.Int32的数据库类型
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnTypeName"></param>
        /// <returns></returns>
        public static string GetDBParamterType(string columnName, string columnTypeName)
        {
            string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnTypeName);
            string paramType = "";
            switch (netType)
            {
                case "int":
                    paramType = "MySqlDbType.Int32";
                    break;
                case "long":
                    paramType = "MySqlDbType.Int64";
                    break;
                case "decimal":
                    paramType = "MySqlDbType.Decimal";
                    break;
                case "double":
                    paramType = "MySqlDbType.Double";
                    break;
                case "float":
                    paramType = "MySqlDbType.Float";
                    break;
                case "string":
                    if (columnName.ToUpper() == "ID")
                    {
                        paramType = "MySqlDbType.String";  //对应char
                    }
                    else
                    {
                        paramType = "MySqlDbType.VarString";
                    }
                    break;
                case "bool":
                    paramType = "MySqlDbType.Bit";
                    break;
                case "DateTime":
                    paramType = "MySqlDbType.DateTime";
                    break;
                case "byte[]":
                    paramType = "MySqlDbType.LongBlob";
                    break;
                default:
                    paramType = "未实现的类型" + columnTypeName;
                    break;
            }

            return paramType;
        }

        /// <summary>
        /// 返回类型于MySqlDbType.Int32的数据库类型
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnTypeName"></param>
        /// <returns></returns>
        public static string GetNetDBParamterType(string columnName, string columnTypeName)
        {
            string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnTypeName);
            string paramType = "";
            switch (netType)
            {
                case "int":
                    paramType = "DbType.Int32";
                    break;
                case "long":
                    paramType = "DbType.Int64";
                    break;
                case "decimal":
                    paramType = "DbType.Decimal";
                    break;
                case "double":
                    paramType = "DbType.Double";
                    break;
                //case "float":
                //    paramType = "DbType.Float";
                //    break;
                case "string":
                    if (columnName.ToUpper() == "ID")
                    {
                        paramType = "DbType.StringFixedLength";  //对应char
                    }
                    else
                    {
                        paramType = "DbType.String";
                    }
                    break;
                case "bool":
                    paramType = "DbType.Boolean";
                    break;
                case "DateTime":
                    paramType = "DbType.DateTime";
                    break;
                case "byte[]":
                    paramType = "DbType.Binary";
                    break;
                default:
                    paramType = "未实现的类型" + columnTypeName;
                    break;
            }

            return paramType;
        }
    }
}
