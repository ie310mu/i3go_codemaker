using System;
using System.Text;
using System.Collections;
using System.Data.OracleClient;
using System.Data;
using IE310.Core.DB;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace IE310.Tools.CodeMaker
{
    public static class HelperBuildHelper
    {
        public static string BuildCode(I3TableInfo tableInfo)
        {
            return BuildGetDataBody(tableInfo) +
                BuildSelectBody(tableInfo) +
                BuildInsertBody(tableInfo) +
                BuildUpdateBody(tableInfo) +
                BuildDeleteBody(tableInfo) +
                BuildDeleteByParamBody(tableInfo) +
                BuildParams(tableInfo) +
                BuildParameters(tableInfo) +
                BuildBatchSave(tableInfo) +
                BuildBatchUpdate(tableInfo) +
                BuildBatchDelete(tableInfo) +
                BuildQueryObjsAndFind(tableInfo) +
                BuildQueryObjsInHash(tableInfo) +
                BuildGetCountInfo(tableInfo) +
                BuildIsExists(tableInfo) +
                BuildSaveOrUpdateBody(tableInfo);
        }


        /// <summary>
        /// 生成获取数据对象函数
        /// </summary>
        /// <returns></returns>
        private static string BuildGetDataBody(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\tpublic static ").Append(tableInfo.EntityClassName).Append(" GetData(DbDataReader sqlReader){\r\n");
            strBuf.Append(tableInfo.EntityClassName).Append(" ").Append(BuildData2Obj(tableInfo));
            strBuf.Append("\treturn ").Append(tableInfo.EntityInstanceName).Append(";\r\n}\r\n");
            return strBuf.ToString();
        }


        /// <summary>
        /// 构建Sql命令参数工厂方法查询
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        private static string BuildParameters(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder(2000);
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string propertyName = columnInfo.Name.Replace("_", "");
                string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                string paramType = DBTypeMapUtil.GetDBParamterType(columnInfo.Name, columnInfo.TypeName);

                //构造方法
                strBuf.Append("\tpublic static IDbDataParameter Create").Append(propertyName)
                    .Append("Parameter(string parameterName,").Append(netType)
                    .Append(" parameterValue){return I3DBUtil.PrepareParameter(parameterName,")
                    .Append(paramType).Append(",parameterValue);}\r\n");
            }

            return strBuf.ToString();
        }

        /// <summary>
        /// 生成查询函数
        /// </summary>
        /// <returns>查询还数的源码</returns>
        private static string BuildSelectBody(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成　Get()函数\r\n";
            }

            StringBuilder strBuf = new StringBuilder();
            tableInfo.ResetToValueParams();
            tableInfo.SetParam("ID", true, true);
            strBuf.Append("\t public static ").Append(tableInfo.EntityClassName).Append(" Get").Append("(").Append(GetFuncParams(tableInfo, false)).Append(",IDbTransaction trans)\r\n\t{\r\n");
            strBuf.Append("\tconst string sSelectSql = @\"").Append(BuildSelectSql(tableInfo)).Append("\";\r\n");
            strBuf.Append(BuildSqlPrams(tableInfo, true, false));
            ArrayList updateParams = new ArrayList();
            strBuf.Append("\t").Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(" = null;\r\n");
            strBuf.Append("\tDbDataReader sqlReader = null;");
            strBuf.Append("\r\n\tif(trans==null)\r\n\t{\r\n");
            strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\tsqlReader = I3DbHelper.ExecuteReader(I3DBUtil.DBServerType,conStr,CommandType.Text,sSelectSql,parms);\r\n");
            strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
            strBuf.Append("\t\tsqlReader = I3DbHelper.ExecuteReader(trans,CommandType.Text,sSelectSql,parms);\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("\t if(sqlReader.Read())\r\n{\r\n");
            strBuf.Append(BuildData2Obj(tableInfo));
            strBuf.Append("\t}\r\n\t sqlReader.Close();\r\n");
            strBuf.Append("\treturn ").Append(tableInfo.EntityInstanceName).Append(";\r\n}\r\n");
            return strBuf.ToString();
        }

        //        /// <summary>
        //        /// 生成插入数据的函数
        //        /// </summary>
        //        /// <returns>源码</returns>
        private static string BuildInsertBody(I3TableInfo tableInfo)
        {
            tableInfo.ResetToValueParams();
            tableInfo.SetParam("ID", true, true);

            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\tpublic static bool").Append(" Save").Append("(").Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(",IDbTransaction trans)\r\n\t{\r\n");
            strBuf.Append("\tconst string sSql = @\"").Append(BuildInsertSql(tableInfo)).Append("\";\r\n");


            //取得主键
            if (tableInfo.HasIdKey)
            {
                I3ColumnInfo idCol = tableInfo.IdColumnInfo;
                if (idCol != null)
                {
                    string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
                    //if (idNetType == "string" && idCol.Width == 36)//guid要赋值
                    if (tableInfo.IdColumnIsGuid)//guid要赋值
                    {
                        strBuf.Append("\tif(I3DBUtil.IsValidPKey(").Append(tableInfo.EntityInstanceName).Append(".").Append(idCol.PropertyName).Append(")==false)\r\n\t{\r\n");
                        strBuf.Append("\t").Append(tableInfo.EntityInstanceName).Append("." + idCol.PropertyName + " = I3DBUtil.NewGuid;\r\n");
                        strBuf.Append("\t}\r\n");
                    }
                    //else if (idNetType == "int")//自增型int不用赋值
                    else if (tableInfo.IdColumnIsAutoInt)//自增型int不用赋值
                    {
                    }
                    else
                    {
                        //strBuf.Append("\t//只支持char(36)、自增型int为主键,保存时请自行确认所有参数\r\n");
                        //strBuf.Append("\treturn false;");
                    }
                }
            }
            else
            {
                //strBuf.Append("\t//未找到主键,保存时请自行确认所有参数\r\n");
                //strBuf.Append("\treturn false;");
            }


            strBuf.Append(BuildSqlPrams(tableInfo, false, true));
            strBuf.Append("\tif(trans==null)\r\n\t{\r\n");
            strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(I3DBUtil.DBServerType, conStr,CommandType.Text,sSql,parms)==1;\r\n");
            strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
            strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(trans,CommandType.Text,sSql,parms)==1;\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("}\r\n");
            return strBuf.ToString();
        }



        //        /// <summary>
        //        /// 生成插入数据的函数
        //        /// </summary>
        //        /// <returns>源码</returns>
        private static string BuildSaveOrUpdateBody(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成SaveOrUpdate函数\r\n";
            }


            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\tpublic static bool").Append(" SaveOrUpdate").Append("(").Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(",IDbTransaction trans)\r\n\t{\r\n");


            //取得主键
            if (tableInfo.HasIdKey)
            {
                I3ColumnInfo idCol = tableInfo.IdColumnInfo;
                if (idCol != null)
                {
                    string idNetType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), idCol.TypeName);
                    //if (idNetType == "string" && idCol.Width == 36)//guid
                    if (tableInfo.IdColumnIsGuid)
                    {
                        strBuf.Append("\tif(I3DBUtil.IsValidPKey(").Append(tableInfo.EntityInstanceName).Append(".").Append(idCol.PropertyName).Append(")==false)\r\n\t{\r\n");
                        strBuf.Append("\t").Append(tableInfo.EntityInstanceName).Append("." + idCol.PropertyName + " = I3DBUtil.NewGuid;\r\n");
                        strBuf.Append("\treturn Save(").Append(tableInfo.EntityInstanceName).Append(",trans);");
                        strBuf.Append("\t}\r\nelse{\r\n");
                        strBuf.Append("\treturn Modify(").Append(tableInfo.EntityInstanceName).Append("." + idCol.PropertyName).Append(",").Append(tableInfo.EntityInstanceName).Append(",trans)>0;");
                        strBuf.Append("}\r\n}");
                    }
                    //else if (idNetType == "int")//自增型int
                    else if (tableInfo.IdColumnIsAutoInt)
                    {
                        strBuf.Append("\tif(I3DBUtil.IsValidPKey(").Append(tableInfo.EntityInstanceName).Append(".").Append(idCol.PropertyName).Append(")==false)\r\n\t{\r\n");
                        //strBuf.Append("\t").Append(tableInfo.EntityInstanceName).Append("." + idCol.PropertyName + " = I3DBUtil.NewGuid;\r\n");
                        strBuf.Append("\treturn Save(").Append(tableInfo.EntityInstanceName).Append(",trans);");
                        strBuf.Append("\t}\r\nelse{\r\n");
                        strBuf.Append("\treturn Modify(").Append(tableInfo.EntityInstanceName).Append("." + idCol.PropertyName).Append(",").Append(tableInfo.EntityInstanceName).Append(",trans)>0;");
                        strBuf.Append("}\r\n}");
                    }
                    else
                    {
                        strBuf.Append("\t//只支持char(36)、自增型int为主键,保存时请自行确认所有参数\r\n");
                        strBuf.Append("\treturn false;");
                        strBuf.Append("}");
                    }
                }
            }
            else
            {
                strBuf.Append("\t//未找到主键,保存时请自行确认所有参数\r\n");
                strBuf.Append("\treturn false;");
                strBuf.Append("}");
            }
            return strBuf.ToString();
        }

        //        /// <summary>
        //        /// 生成更新数据的函数
        //        /// </summary>
        //        /// <returns>源码</returns>
        private static string BuildBatchUpdate(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成　PrepareUpdate函数\r\n";
            }
            tableInfo.ResetToValueParams();
            tableInfo.SetParam("ID", true, true);
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\tpublic static void").Append(" PrepareUpdate(II3BatchUpdater updater,").Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(")\r\n\t{\r\n");
            strBuf.Append("\tconst string sSql = @\"").Append(BuildUpdateSql(tableInfo)).Append("\";\r\n");
            strBuf.Append("\tII3BatchCommand command = updater.UpdateCommand;\r\n");

            strBuf.Append("\tif(command.Sql==null){\r\n");
            strBuf.Append("\t command.TableName = \"").Append(tableInfo.TableName).Append("\";\r\n");
            strBuf.Append("\tcommand.Sql = sSql;\r\n");
            strBuf.Append(BuildBatchSqlPrams2(tableInfo, true, true));
            strBuf.Append("\t\r\n}\r\n");

            strBuf.Append("\r\n\tcommand.AddData(");
            int iIndex = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (columnInfo.IsValueParam)
                {
                    if (iIndex > 0)
                    {
                        strBuf.Append(",");
                    }
                    strBuf.Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName);
                    if (iIndex++ % 10 == 0)
                    {
                        strBuf.Append("\r\n");
                    }
                }
            }
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (columnInfo.IsCauseParam)
                {
                    strBuf.Append(",").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName);
                }
            }
            strBuf.Append(@");
        			}");
            return strBuf.ToString();
        }

        //        /// <summary>
        //        /// 生成插入数据的函数
        //        /// </summary>
        //        /// <returns>源码</returns>
        private static string BuildBatchDelete(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成　PrepareDelete函数\r\n";
            }
            tableInfo.ResetToValueParams();
            tableInfo.SetParam("ID", true, true);
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\tpublic static void").Append(" PrepareDelete(II3BatchUpdater updater,string id)\r\n\t{\r\n");
            strBuf.Append("\tconst string sSql = @\"").Append(BuildDeleteSql(tableInfo)).Append("\";\r\n");
            strBuf.Append("\tII3BatchCommand command = updater.DeleteCommand;\r\n");

            strBuf.Append("\tif(command.Sql==null){\r\n");
            strBuf.Append("\t command.TableName = \"").Append(tableInfo.TableName).Append("\";\r\n");
            strBuf.Append("\tcommand.Sql = sSql;\r\n");
            strBuf.Append(BuildBatchSqlPrams2(tableInfo, true, false));
            strBuf.Append("\t\r\n}\r\n");

            strBuf.Append("\r\n\tcommand.AddData(");
            int iIndex = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (columnInfo.IsCauseParam)
                {
                    if (iIndex > 0)
                    {
                        strBuf.Append(",");
                    }
                    strBuf.Append("id");
                    if (iIndex++ % 10 == 0)
                    {
                        strBuf.Append("\r\n");
                    }
                }
            }
            strBuf.Append(@");
        			}");
            return strBuf.ToString();
        }

        //        /// <summary>
        //        /// 生成插入数据的函数
        //        /// </summary>
        //        /// <returns>源码</returns>
        private static string BuildBatchSave(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            tableInfo.ResetToValueParams();
            tableInfo.SetParam("ID", true, true);
            strBuf.Append("\tpublic static void").Append(" PrepareSave(II3BatchUpdater updater,").Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(")\r\n\t{\r\n");
            strBuf.Append("\tconst string sSql = @\"").Append(BuildInsertSql(tableInfo)).Append("\";\r\n");
            strBuf.Append("\tII3BatchCommand command = updater.InsertCommand;\r\n");

            strBuf.Append("\tif(command.Sql==null){\r\n");
            strBuf.Append("\t command.TableName = \"").Append(tableInfo.TableName).Append("\";\r\n");
            strBuf.Append("\tcommand.Sql = sSql;\r\n");
            tableInfo.ResetToValueParams();
            strBuf.Append(BuildBatchSqlPrams2(tableInfo, false, true));
            strBuf.Append("\t\r\n}\r\n");

            strBuf.Append("\r\n\tcommand.AddData(");
            int iIndex = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (columnInfo.IsValueParam)
                {
                    if (iIndex > 0)
                    {
                        strBuf.Append(",");
                    }
                    strBuf.Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName).Append("");
                    //换行
                    if (iIndex++ % 10 == 0)
                    {
                        strBuf.Append("\r\n");
                    }
                }

            }
            strBuf.Append(@");
        			}");
            return strBuf.ToString();
        }


        //        /// <summary>
        //        /// 生成删除函数
        //        /// </summary>
        //        /// <returns></returns>
        private static string BuildDeleteBody(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成　Delete函数\r\n";
            }
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\t public static int ").Append(" Delete").Append("(").Append(GetFuncParams(tableInfo, true)).Append("IDbTransaction trans)\r\n\t{");
            strBuf.Append("\tconst string sSql = \"").Append(BuildDeleteSql(tableInfo)).Append("\";\r\n");
            strBuf.Append(BuildSqlPrams(tableInfo, true, false));
            strBuf.Append("\tif(trans==null){\r\n\t\r\n");
            strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(I3DBUtil.DBServerType, conStr,CommandType.Text,sSql,parms);\r\n");
            strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
            strBuf.Append("\t\t return I3DbHelper.ExecuteNonQuery(trans,CommandType.Text,sSql,parms);\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("}\r\n");
            return strBuf.ToString();
        }

        //        /// <summary>
        //        /// 生成删除函数
        //        /// </summary>
        //        /// <returns></returns>
        private static string BuildDeleteByParamBody(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\t public static int ").Append(" Delete").Append("(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",IDbTransaction trans)\r\n\t{");
            strBuf.Append("\tstring sSql = \"Delete From ").Append(tableInfo.TableName).Append(" Where 1=1 \";\r\n");
            strBuf.Append("\tIDbDataParameter[] parms = null;\r\n");
            strBuf.Append("\tsSql += BuildParams(null,").Append(tableInfo.ParamsInstanceName).Append(",out parms);\r\n");
            strBuf.Append("\tif(trans==null){\r\n\t\r\n");
            strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(I3DBUtil.DBServerType, conStr,CommandType.Text,sSql,parms);\r\n");
            strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
            strBuf.Append("\t\t return I3DbHelper.ExecuteNonQuery(trans,CommandType.Text,sSql,parms);\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("}\r\n");
            return strBuf.ToString();
        }

        /// <summary>
        /// 创建根据Params生成查询的函数
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        private static string BuildParams(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\r\n\t public static string BuildParams(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",out IDbDataParameter[] parms)\r\n\t{\r\n");
            strBuf.Append("\t return BuildParams(\"t\",").Append(tableInfo.ParamsInstanceName).Append(", out parms);\r\n}\r\n");

            strBuf.Append("\r\n\t public static string BuildParams(string tabPrfix,").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",out IDbDataParameter[] parms)\r\n\t{\r\n");
            strBuf.Append("\tStringBuilder strBuf = new StringBuilder();\r\n");
            strBuf.Append("\tArrayList paramList = new ArrayList();\r\n");
            strBuf.Append("\tIDbDataParameter param = null;\r\n");
            strBuf.Append(" if (").Append(tableInfo.ParamsInstanceName).Append(@" == null)
             {
                 parms = new IDbDataParameter[0];
                 return """";
             }
             tabPrfix += string.IsNullOrEmpty(tabPrfix)?"""":""."";
             ");
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string paramObj = tableInfo.ParamsInstanceName + "." + columnInfo.PropertyName;
                string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                string paramerType = DBTypeMapUtil.GetDBParamterType(columnInfo.Name, columnInfo.TypeName);
                switch (netType)
                {
                    case "int":
                        strBuf.Append("\tif(").Append(paramObj).Append("!= I3DBUtil.INVALID_ID)\r\n");
                        strBuf.Append("\t{\r\n");
                        strBuf.Append("\t\tstrBuf.Append(\" And \").Append(tabPrfix).Append(\"").Append(columnInfo.Name).Append(" = ").Append(columnInfo.WhereParamterName).Append("\");\r\n");
                        strBuf.Append("\t\tparam = I3DBUtil.PrepareParameter(\"").Append(columnInfo.WhereParamterName).Append("\"," + paramerType).Append(");\r\n");
                        strBuf.Append("\t\tparam.Value = ").Append(paramObj).Append(";\r\n");
                        strBuf.Append("\t\tparamList.Add(param);\r\n\t}\r\n");
                        break;
                    case "string":
                        strBuf.Append("\tif(!string.IsNullOrEmpty(").Append(paramObj).Append("))\r\n");
                        strBuf.Append("\t{\r\n");
                        strBuf.Append("\t\tstrBuf.Append(\" And \").Append(tabPrfix).Append(\"").Append(columnInfo.Name).Append(" = ").Append(columnInfo.WhereParamterName).Append("\");\r\n");
                        strBuf.Append("\t\tparam = I3DBUtil.PrepareParameter(\"").Append(columnInfo.WhereParamterName).Append("\"," + paramerType).Append(");\r\n");
                        strBuf.Append("\t\tparam.Value = ").Append(paramObj).Append(";\r\n");
                        strBuf.Append("\t\tparamList.Add(param);\r\n\t}\r\n");
                        break;
                    case "DateTime":
                        strBuf.Append("\tif(").Append(paramObj).Append("!= null)\r\n");
                        strBuf.Append("\t{\r\n");
                        strBuf.Append("\t\tIDbDataParameter[] spanParms = ").Append(paramObj).Append(".PrepareParams(\"").Append(columnInfo.Name).Append("\",\"").Append(columnInfo.Name).Append("\",strBuf);\r\n");
                        strBuf.Append("\t\tforeach(IDbDataParameter parm in spanParms)\r\n");
                        strBuf.Append("\t\t{\r\n");
                        strBuf.Append("\t\t\tparamList.Add(parm);\r\n");
                        strBuf.Append("\t\t}\r\n");
                        strBuf.Append("\t}\r\n");
                        break;
                    default:
                        break;
                }
            }
            strBuf.Append(@"
            string sOtherSql = ").Append(tableInfo.ParamsInstanceName).Append(@".ToSqlString(paramList);
            if (string.IsNullOrEmpty(sOtherSql) == false)
            {
                strBuf.Append((").Append(tableInfo.ParamsInstanceName).Append(@".ParamType == I3SqlParamType.GroupAnd ? "" And "" : "" Or "")).Append(""("").Append(sOtherSql).Append("")"");
            }
            ");
            strBuf.Append("\tparms =  new IDbDataParameter[paramList.Count];\r\n");
            strBuf.Append("\tparamList.CopyTo(parms);\r\n");
            strBuf.Append("\r return strBuf.ToString();\r\n");
            strBuf.Append("\r}\r\n");
            return strBuf.ToString();
        }

        //        /// <summary>
        //        /// 生成更新函数
        //        /// </summary>
        //        /// <returns></returns>
        private static string BuildUpdateBody(I3TableInfo tableInfo)
        {
            //if (!tableInfo.HasIdKey)
            //{
            //    return "\r\n//不能找到ID主键，不能生成Modify函数\r\n";
            //}

            StringBuilder strBuf = new StringBuilder();

            //有主键，用主键作为更新条件
            if (tableInfo.HasIdKey)
            {
                strBuf.Append("\tpublic static int ").Append(" Modify").Append("(").Append(GetFuncParams(tableInfo, true)).Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(",IDbTransaction trans)\r\n\t{\r\n");
                strBuf.Append("\tconst string sSql = @\"").Append(BuildUpdateSql(tableInfo)).Append("\";\r\n");
                strBuf.Append(BuildSqlPrams(tableInfo, true, true));
                strBuf.Append("\tif(trans==null)\r\n\t{\r\n");
                strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
                strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(I3DBUtil.DBServerType, conStr,CommandType.Text,sSql,parms);\r\n");
                strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
                strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(trans,CommandType.Text,sSql,parms);\r\n");
                strBuf.Append("\t}\r\n");
                strBuf.Append("}\r\n");
            }

            //自定义xxxxParams作为更新条件的方法
            {
                tableInfo.SetParam("ID", false, false);
                strBuf.Append("\tpublic static int ").Append(" Modify").Append("(").Append(GetFuncParams(tableInfo, true)).Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName)
                   .Append(", ").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",IDbTransaction trans)\r\n\t{\r\n");
                strBuf.Append("\tstring sSql = @\"").Append(BuildUpdateSql(tableInfo)).Append("\";\r\n");
                strBuf.Append(BuildSqlPrams(tableInfo, true, true));
                strBuf.Append("\r\n");
                strBuf.Append("\t//生成更新条件\r\n");
                strBuf.Append("\tsSql += \" where 1= 1\";\r\n");
                strBuf.Append("\tIDbDataParameter[] whereParams = null;\r\n");
                strBuf.Append("\tsSql += BuildParams(null,").Append(tableInfo.ParamsInstanceName).Append(",out whereParams);\r\n");
                strBuf.Append("\tList<IDbDataParameter> paramList = new List<IDbDataParameter>();\r\n");
                strBuf.Append("\tparamList.AddRange(parms);\r\n");
                strBuf.Append("\tparamList.AddRange(whereParams);\r\n");
                strBuf.Append("\tparms = paramList.ToArray();\r\n\r\n");
                strBuf.Append("\tif(trans==null)\r\n\t{\r\n");
                strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
                strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(I3DBUtil.DBServerType, conStr,CommandType.Text,sSql,parms);\r\n");
                strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
                strBuf.Append("\t\treturn I3DbHelper.ExecuteNonQuery(trans,CommandType.Text,sSql,parms);\r\n");
                strBuf.Append("\t}\r\n");
                strBuf.Append("}\r\n");
                tableInfo.SetParam("ID", true, true);
            }

            return strBuf.ToString();
        }

        /// <summary>
        /// 生成Entity[] Get方法和Find方法
        /// </summary>
        /// <returns></returns>
        private static string BuildQueryObjsAndFind(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            StringBuilder sqlBuf = new StringBuilder();
            StringBuilder sqlCountBuf = new StringBuilder();

            tableInfo.ResetToValueParams();
            sqlCountBuf.Append(BuildCountSql(tableInfo)).Append(" Where (1=1)");
            sqlBuf.Append(BuildSelectSql(tableInfo)).Append(" Where (1=1)");
            strBuf.Append("\tpublic static ").Append(tableInfo.EntityClassName).Append("[] Get(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",I3PageInfo pageInfo,I3OrderParams orderParams, IDbTransaction trans)\r\n\t{");
            strBuf.Append("\r\n\tconst string sBaseSql = @\"").Append(sqlBuf.ToString()).Append("\";\r\n");
            strBuf.Append("\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\tIDbDataParameter[] parms = null;\r\n");
            strBuf.Append("\tstring sCauseSql = BuildParams(").Append(tableInfo.ParamsInstanceName).Append(",out parms);");
            strBuf.Append("\tstring sSql = sBaseSql+sCauseSql;\r\n");
            strBuf.Append("\tif(pageInfo!=null)\r\n");
            strBuf.Append("\t{\r\n");
            strBuf.Append("\t\tstring sCountSql  = \"").Append(sqlCountBuf.ToString()).Append("\"+").Append("sCauseSql;").Append("\r\n");
            strBuf.Append("\t\tint iTotalCount = pageInfo.TotalCount;\r\n");
            strBuf.Append("\t\tif(pageInfo.TotalCount==0)\r\n");
            strBuf.Append("\t\t{\r\n");
            strBuf.Append("\t\t	iTotalCount = I3DBUtil.GetDbIntValue(I3DbHelper.ExecuteScalar(I3DBUtil.DBServerType, conStr,CommandType.Text,sCountSql").Append(",parms),0);\r\n");
            strBuf.Append("\t\t	pageInfo.TotalCount = iTotalCount;		\r\n");
            strBuf.Append("\t\t}\r\n");
            strBuf.Append("\t\t sSql = I3DBUtil.PrepareSql(pageInfo,orderParams,sSql);\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("\telse  if(orderParams!=null)\r\n");
            strBuf.Append("\t{\r\n");
            strBuf.Append("\t	  sSql += orderParams.ToString();\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("\tArrayList objArray = new ArrayList();\r\n");
            ArrayList updateParams = new ArrayList();
            strBuf.Append("\t").Append(tableInfo.EntityClassName).Append(" ").Append(tableInfo.EntityInstanceName).Append(" = null;\r\n");
            strBuf.Append("\tDbDataReader sqlReader;");
            strBuf.Append("\tif(trans==null){");
            strBuf.Append("\tsqlReader = I3DbHelper.ExecuteReader(I3DBUtil.DBServerType,conStr,CommandType.Text,sSql,parms);\r\n");
            strBuf.Append("\t}else{");
            strBuf.Append("\tsqlReader = I3DbHelper.ExecuteReader(trans,CommandType.Text,sSql,parms);\r\n");
            strBuf.Append("\t}");
            strBuf.Append("\t while(sqlReader.Read())\r\n\t{\r\n");
            strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(" = GetData(sqlReader); ");
            strBuf.Append("\t\tobjArray.Add(").Append(tableInfo.EntityInstanceName).Append(");\r\n\t}\r\n\tsqlReader.Close();\r\n");
            strBuf.Append("\t").Append(tableInfo.EntityClassName).Append("[] objs = new ").Append(tableInfo.EntityClassName).Append("[objArray.Count];\r\n");
            strBuf.Append("\tobjArray.CopyTo(0,objs,0,objs.Length);\r\n");
            strBuf.Append("\treturn objs;\r\n\t}\r\n");

            //Find方法
            strBuf.Append("\tpublic static ").Append(tableInfo.EntityClassName).Append(" Find(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",I3OrderParams orderParams, IDbTransaction trans)\r\n\t{");
            strBuf.Append("\t\r\nI3PageInfo pageInfo = new I3PageInfo(0, 1);");
            strBuf.Append("\t\r\npageInfo.TotalCount = 1;");
            strBuf.Append("\t\r\n").Append(tableInfo.EntityClassName).Append("[] objs = Get(").Append(tableInfo.ParamsInstanceName).Append(",pageInfo,orderParams,trans);");
            strBuf.Append("\tif(objs.Length>0) return objs[0];return null;\r\n\t}\r\n");
            return strBuf.ToString();
        }

        /// <summary>
        /// 生成Dictionary<string,Entity> GetDictionary
        /// </summary>
        /// <returns></returns>
        private static string BuildQueryObjsInHash(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成GetDictionary函数\r\n";
            }

            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\tpublic static Dictionary<");
            strBuf.Append(tableInfo.IdColumnIsInt ? "int" : "string");
            strBuf.Append(",").Append(tableInfo.EntityClassName).Append("> GetDictionary(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(",I3PageInfo pageInfo, IDbTransaction trans)\r\n\t{");
            strBuf.Append("\r\n\t").Append(tableInfo.EntityClassName).Append("[] objs = Get(").Append(tableInfo.ParamsInstanceName).Append(",pageInfo,null,trans);\r\n");
            strBuf.Append(@"Dictionary<");
            strBuf.Append(tableInfo.IdColumnIsInt ? "int" : "string");
            strBuf.Append(",").Append(tableInfo.EntityClassName).Append(@"> objHash = new Dictionary<");
            strBuf.Append(tableInfo.IdColumnIsInt ? "int" : "string");
            strBuf.Append(",").Append(tableInfo.EntityClassName).Append(@">();
                 foreach(").Append(tableInfo.EntityClassName).Append(@" obj in objs)
				 {
					objHash.Add(obj." + tableInfo.IdColumnInfo.PropertyName + @",obj);	
				 }
				 return objHash;
                 }");
            return strBuf.ToString();
        }

        /// <summary>
        /// 生成查询分页数据，返回DataSet
        /// </summary>
        /// <returns></returns>
        private static string BuildGetCountInfo(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            StringBuilder sqlBuf = new StringBuilder();
            StringBuilder sqlCountBuf = new StringBuilder();

            tableInfo.ResetToValueParams();
            sqlCountBuf.Append(BuildCountSql(tableInfo)).Append(" Where (1=1)");
            sqlBuf.Append(BuildSelectSql(tableInfo)).Append(" Where (1=1)");
            strBuf.Append("\r\n\r\n\tpublic static int GetCount(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(")\r\n\t{");
            strBuf.Append("\r\n\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\tIDbDataParameter[] parms = null;\r\n");
            strBuf.Append("\t\tstring sCauseSql = BuildParams(").Append(tableInfo.ParamsInstanceName).Append(",out parms);");
            strBuf.Append("\t\t\tstring sCountSql  = \"").Append(sqlCountBuf.ToString()).Append("\"+").Append("sCauseSql;").Append("\r\n");
            strBuf.Append("\t\t	int iTotalCount = I3DBUtil.GetDbIntValue(I3DbHelper.ExecuteScalar(I3DBUtil.DBServerType, conStr,CommandType.Text,sCountSql").Append(",parms), 0);\r\n");
            strBuf.Append("\t\t\treturn iTotalCount;\r\n\t}\r\n");


            strBuf.Append("\r\n\r\n\tpublic static int GetCount(").Append(tableInfo.ParamsClassName).Append(" ").Append(tableInfo.ParamsInstanceName).Append(", IDbTransaction trans)\r\n\t{");
            strBuf.Append("\r\n\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\tIDbDataParameter[] parms = null;\r\n");
            strBuf.Append("\t\tstring sCauseSql = BuildParams(").Append(tableInfo.ParamsInstanceName).Append(",out parms);");
            strBuf.Append("\t\t\tstring sCountSql  = \"").Append(sqlCountBuf.ToString()).Append("\"+").Append("sCauseSql;").Append("\r\n");
            strBuf.Append("\t\tif(trans == null)\r\n");
            strBuf.Append("\t\t{\r\n");
            strBuf.Append("\t\t	int iTotalCount = I3DBUtil.GetDbIntValue(I3DbHelper.ExecuteScalar(I3DBUtil.DBServerType, conStr,CommandType.Text,sCountSql").Append(",parms), 0);\r\n");
            strBuf.Append("\t\t\treturn iTotalCount;\r\n\t\t}\r\n");
            strBuf.Append("\t\telse\r\n");
            strBuf.Append("\t\t{\r\n");
            strBuf.Append("\t\t	int iTotalCount = I3DBUtil.GetDbIntValue(I3DbHelper.ExecuteScalar(trans,CommandType.Text,sCountSql").Append(",parms), 0);\r\n");
            strBuf.Append("\t\t\treturn iTotalCount;\r\n\t\t}\r\n");
            strBuf.Append("\t}\r\n\r\n");

            return strBuf.ToString();
        }

        public static string BuildIsExists(I3TableInfo tableInfo)
        {
            if (!tableInfo.HasIdKey)
            {
                return "\r\n//不能找到ID主键，不能生成IsExist函数\r\n";
            }

            tableInfo.ResetToValueParams();
            tableInfo.SetParam("ID", true, true);
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\t public static bool IsExist").Append("(").Append(GetFuncParams(tableInfo, false)).Append(",IDbTransaction trans)\r\n\t{\r\n");
            strBuf.Append("\tconst string sSelectSql = \"").Append(BuildCountSql(tableInfo)).Append("\";\r\n");
            strBuf.Append(BuildSqlPrams(tableInfo, true, false));
            strBuf.Append("\r\n\tif(trans==null)\r\n\t{\r\n");
            strBuf.Append("\t\tstring conStr=I3DBUtil.ConnectionString;\r\n");
            strBuf.Append("\t\treturn 1<= I3DBUtil.GetDbIntValue(I3DbHelper.ExecuteScalar(I3DBUtil.DBServerType, conStr,CommandType.Text,sSelectSql,parms),0);\r\n");
            strBuf.Append("\t}\r\n\telse\r\n\t{\r\n");
            strBuf.Append("\t\treturn 1<= I3DBUtil.GetDbIntValue(I3DbHelper.ExecuteScalar(trans,CommandType.Text,sSelectSql,parms),0);\r\n");
            strBuf.Append("\t}\r\n");
            strBuf.Append("\r\n}\r\n");
            return strBuf.ToString();
        }

        #region CodeUtil



        /// <summary>
        /// 将数据变换为数据对象的赋值方法
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public static string BuildData2Obj(I3TableInfo tableInfo)
        {
            StringBuilder strBuf = new StringBuilder();
            strBuf.Append("\t").Append(tableInfo.EntityInstanceName).Append(" = new ").Append(tableInfo.EntityClassName).Append("();\r\n");
            int avaible = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                avaible++;
                string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                switch (netType)
                {
                    case "int":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = (int)I3DBUtil.GetDbIntValue(sqlReader.GetValue(").Append(avaible - 1).Append("),I3DBUtil.INVALID_ID);\r\n");
                        break;
                    case "long":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = (long)I3DBUtil.GetDbLongValue(sqlReader.GetValue(").Append(avaible - 1).Append("),I3DBUtil.INVALID_ID);\r\n");
                        break;
                    case "decimal":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = (decimal)I3DBUtil.GetDbDecimalValue(sqlReader.GetValue(").Append(avaible - 1).Append("),I3DBUtil.INVALID_DECIMAL);\r\n");
                        break;
                    case "double":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = (double)I3DBUtil.GetDbDoubleValue(sqlReader.GetValue(").Append(avaible - 1).Append("),I3DBUtil.INVALID_DOUBLE);\r\n");
                        break;
                    case "float":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = (float)I3DBUtil.GetDbFloatValue(sqlReader.GetValue(").Append(avaible - 1).Append("),I3DBUtil.INVALID_FLOAT);\r\n");
                        break;
                    case "string":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = sqlReader.GetValue(").Append(avaible - 1).Append(").ToString();\r\n");
                        break;
                    case "bool":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = sqlReader.GetBoolean(").Append(avaible - 1).Append(");\r\n");
                        break;
                    case "DateTime":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                        .Append(" = sqlReader.GetDateTime(").Append(avaible - 1).Append(");\r\n");
                        break;
                    case "byte[]":
                        strBuf.Append("\tif(!sqlReader.IsDBNull(").Append(avaible - 1).Append("))\r\n\t{\t\r\n");
                        strBuf.Append("\t\t").Append(tableInfo.EntityInstanceName).Append(".").Append(columnInfo.PropertyName)
                            .Append(" = I3DBUtil.GetBytes(sqlReader,").Append(avaible - 1).Append(");\r\n");
                        strBuf.Append("\t}\r\n");
                        break;
                    default:
                        strBuf.Append(string.Format("//不支持类型{0}\r\n", columnInfo.TypeName));
                        break;
                }
            }

            return strBuf.ToString();
        }


        /// <summary>
        /// 构造函数的条件参数
        /// </summary>
        /// <param name="curTable"></param>
        /// <param name="bAddComma"></param>
        /// <returns></returns>
        public static string GetFuncParams(I3TableInfo tableInfo, bool bAddComma)
        {
            StringBuilder strBuf = new StringBuilder();
            int wCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsCauseParam)
                {
                    continue;
                }
                if (wCount > 0)
                {
                    strBuf.Append(",");
                }
                string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                strBuf.Append(netType).Append(" ").Append(columnInfo.ParamName);
                wCount++;
            }
            if (wCount > 0 && bAddComma)
            {
                strBuf.Append(",");
            }
            return strBuf.ToString();
        }

        /// <summary>
        /// 构建查询语句
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="bTempTable"></param>
        /// <returns></returns>
        public static string BuildSelectSql(I3TableInfo tableInfo)
        {
            StringBuilder sqlBuf = new StringBuilder();
            //创建选择语句
            sqlBuf.Append("Select ");
            int iCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsValueParam)
                {
                    continue;
                }
                iCount++;
                if (iCount % 20 == 0)
                {
                    sqlBuf.Append("\r\n");
                }
                sqlBuf.Append("t.").Append(columnInfo.Name).Append(",");
            }
            sqlBuf.Remove(sqlBuf.Length - 1, 1).Append(" From ").Append(tableInfo.TableName).Append(" t");
            int wCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsCauseParam)
                {
                    continue;
                }
                if (wCount > 0)
                {
                    sqlBuf.Append(" And ");
                }
                else
                {
                    sqlBuf.Append(" Where ");
                }
                sqlBuf.Append("t.").Append(columnInfo.Name).Append("=").Append(columnInfo.WhereParamterName);
                wCount++;
            }
            ///排序
            wCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (columnInfo.OrderBy == I3OrderByType.None)
                {
                    continue;
                }
                if (wCount > 0)
                {
                    sqlBuf.Append(", ");
                }
                else
                {
                    sqlBuf.Append(" Order By ");
                }
                sqlBuf.Append("t.").Append(columnInfo.Name);
                if (columnInfo.OrderBy == I3OrderByType.Desc)
                {
                    sqlBuf.Append(" Desc ");
                }
                wCount++;
            }
            return sqlBuf.ToString();

        }


        public static string BuildSqlPrams(I3TableInfo tableInfo, bool bCauseParam, bool bValueParam)
        {
            bool idColumnIsAutoInt = tableInfo.IdColumnIsAutoInt;

            StringBuilder paramBuf = new StringBuilder();
            int aviable = 0;
            paramBuf.Append("\tIDbDataParameter[] parms = new IDbDataParameter[]{");
            if (bValueParam)
            {
                ///值参数
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (idColumnIsAutoInt && columnInfo.Name.ToUpper().Equals("ID"))//自增型int ID不处理
                    {
                        continue;
                    }
                    if (columnInfo.IsValueParam)
                    {
                        if (aviable > 0)
                        {
                            paramBuf.Append(",");
                        }
                        aviable++;
                        paramBuf.Append(BuildSqlParam(columnInfo, true, tableInfo.EntityInstanceName));
                    }
                }
            }
            if (bCauseParam)
            {
                ///条件参数
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (columnInfo.IsCauseParam)
                    {
                        if (aviable > 0)
                            paramBuf.Append(",");

                        aviable++;
                        paramBuf.Append(BuildSqlParam(columnInfo, false, tableInfo.EntityInstanceName));
                    }
                }
            }
            return "" + paramBuf.ToString() + "\r\n\t\t\t};\r\n";
        }

        /// <summary>
        /// 构造查询参数
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <param name="bValueParam">是否为赋值参数，否为条件参数</param>
        /// <param name="dataInstanceName">对象名称</param>
        /// <param name="paramBuf"></param>
        /// <returns></returns>
        public static string BuildSqlParam(I3ColumnInfo columnInfo, bool bValueParam, string dataInstanceName)
        {
            StringBuilder paramBuf = new StringBuilder();
            string paramType = DBTypeMapUtil.GetDBParamterType(columnInfo.Name, columnInfo.TypeName);
            string paramterName = bValueParam ? columnInfo.ValueParamterName : columnInfo.WhereParamterName;
            if (!bValueParam)
            {
                paramBuf.Append("\r\n\t\t\t\tI3DBUtil.PrepareParameter(\"").Append(paramterName).Append("\"," + paramType).Append(",").Append(columnInfo.ParamName).Append(")");
            }
            else
            {
                paramBuf.Append("\r\n\t\t\t\tI3DBUtil.PrepareParameter(\"").Append(paramterName).Append("\"," + paramType).Append(",").Append(dataInstanceName).Append(".").Append(columnInfo.PropertyName).Append(")");
            }
            return paramBuf.ToString();
        }


        /// <summary>
        /// 根据当前选择项构造Insert SQL
        /// </summary>
        /// <param name="TblName">表名</param>
        /// <returns>SQL语句</returns>
        public static string BuildInsertSql(I3TableInfo tableInfo)
        {
            bool idColumnIsAutoInt = tableInfo.IdColumnIsAutoInt;

            StringBuilder sqlBuf = new StringBuilder();
            sqlBuf.Append("Insert Into ").Append(tableInfo.TableName).Append("(");
            int iCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsValueParam)
                {
                    continue;
                }
                if (idColumnIsAutoInt && columnInfo.Name.ToUpper().Equals("ID"))//自增型int ID不处理
                {
                    continue;
                }
                iCount++;
                if (iCount % 20 == 0)
                {
                    sqlBuf.Append("\r\n");
                }
                sqlBuf.Append(columnInfo.Name).Append(",");
            }
            sqlBuf.Remove(sqlBuf.Length - 1, 1).Append(")").Append("\r\n Values(");
            iCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsValueParam)
                {
                    continue;
                }
                if (idColumnIsAutoInt && columnInfo.Name.ToUpper().Equals("ID"))//自增型int ID不处理
                {
                    continue;
                }
                iCount++;
                if (iCount % 20 == 0)
                {
                    sqlBuf.Append("\r\n");
                }
                sqlBuf.Append(columnInfo.ValueParamterName).Append(",");
            }
            sqlBuf.Remove(sqlBuf.Length - 1, 1).Append(")");

            return sqlBuf.ToString();
        }


        /// <summary>
        /// 根据当前选择项构造Update SQL
        /// </summary>
        /// <param name="TblName">表名</param>
        /// <returns>SQL语句</returns>
        public static string BuildUpdateSql(I3TableInfo tableInfo)
        {
            bool idColumnIsAutoInt = tableInfo.IdColumnIsAutoInt;

            StringBuilder sqlBuf = new StringBuilder();
            sqlBuf.Append("Update ").Append(tableInfo.TableName).Append(" Set ");
            int iCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsValueParam)
                {
                    continue;
                }
                if (idColumnIsAutoInt && columnInfo.Name.ToUpper().Equals("ID"))//自增型int ID不处理
                {
                    continue;
                }
                iCount++;
                if (iCount % 20 == 0)
                {
                    sqlBuf.Append("\r\n");
                }
                sqlBuf.Append(columnInfo.Name).Append("=").Append(columnInfo.ValueParamterName).Append(",");
            }
            sqlBuf.Remove(sqlBuf.Length - 1, 1);
            if (tableInfo.HasCauseParam)
            {
                sqlBuf.Append(" Where ");
                int wCount = 0;
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (!columnInfo.IsCauseParam)
                    {
                        continue;
                    }
                    if (wCount > 0)
                    {
                        sqlBuf.Append(" And ");
                    }
                    sqlBuf.Append(columnInfo.Name).Append("=").Append(columnInfo.WhereParamterName);
                    wCount++;
                }
            }
            return sqlBuf.ToString();
        }


        /// <summary>
        /// 根据当前选择项构造Delete SQL
        /// </summary>
        /// <param name="TblName">表名</param>
        /// <returns>SQL语句</returns>
        public static string BuildDeleteSql(I3TableInfo tableInfo)
        {
            StringBuilder sqlBuf = new StringBuilder();
            sqlBuf.Append("Delete From  ").Append(tableInfo.TableName);
            if (tableInfo.HasCauseParam)
            {
                sqlBuf.Append(" Where ");
                int wCount = 0;
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (!columnInfo.IsCauseParam)
                    {
                        continue;
                    }
                    if (wCount > 0)
                    {
                        sqlBuf.Append(" And ");
                    }
                    sqlBuf.Append(columnInfo.Name).Append("=").Append(columnInfo.WhereParamterName);
                    wCount++;
                }
            }
            return sqlBuf.ToString();
        }

        public static string BuildBatchSqlPrams2(I3TableInfo tableInfo, bool bCauseParam, bool bValueParam)
        {
            StringBuilder paramBuf = new StringBuilder();
            if (bValueParam)
            {
                ///值参数
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (columnInfo.IsValueParam)
                    {
                        paramBuf.Append(BuildBatchSqlParam2(columnInfo, true));
                    }
                }
            }
            if (bCauseParam)
            {
                ///条件参数
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (columnInfo.IsCauseParam)
                    {
                        paramBuf.Append(BuildBatchSqlParam2(columnInfo, false));
                    }
                }
            }
            return paramBuf.ToString();
        }

        private static string BuildBatchSqlParam2(I3ColumnInfo columnInfo, bool bValueParam)
        {
            StringBuilder paramBuf = new StringBuilder();
            string paramType = DBTypeMapUtil.GetNetDBParamterType(columnInfo.Name, columnInfo.TypeName);
            string paramterName = bValueParam ? columnInfo.ValueParamterName : columnInfo.WhereParamterName;
            paramBuf.Append("\r\n\t command.AddParameter(\":").Append(paramterName).Append("\",").Append(paramType).Append(");");
            return paramBuf.ToString();
        }


        /// <summary>
        /// 获取求和的SQL语句
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public static string BuildCountSql(I3TableInfo tableInfo)
        {
            StringBuilder sqlBuf = new StringBuilder();
            //创建选择语句
            sqlBuf.Append("Select Count(").Append(tableInfo[0].Name).Append(") From ").Append(tableInfo.TableName).Append(" t");
            int wCount = 0;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (!columnInfo.IsCauseParam)
                {
                    continue;
                }
                if (wCount > 0)
                {
                    sqlBuf.Append(" And ");
                }
                else
                {
                    sqlBuf.Append(" Where ");
                }
                sqlBuf.Append("t.").Append(columnInfo.Name).Append("=").Append(columnInfo.WhereParamterName);
                wCount++;
            }
            return sqlBuf.ToString();
        }

        #endregion
    }
}
