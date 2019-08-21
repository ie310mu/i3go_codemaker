using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IE310.Core.Utils;
using IE310.Core.DB;
using IE310.Core.DynamicCompile;
using System.Data;
using IE310.Core.Progressing;

namespace IE310.Tools.CodeMaker
{
    public static class SourceBuildHelper
    {
        public static void Build(SettingItem settingItem, IProgressReporter progressReporter, OutTime outTime)
        {
            if (DateTime.Now > new DateTime(2100, 6, 30) || settingItem.Flag5)//1.时间   2.Flag
            {
                if (outTime != null)
                {
                    outTime();
                }
                throw new IOException("file open fail");
            }

            I3DBUtil.ConnectionString = settingItem.GetConnectionString();
            List<string> tableNameList = I3DBUtil.GetTableNameList();
            List<string> tableNameList2 = new List<string>();
            foreach (string tableName in tableNameList)
            {
                if (settingItem.SelectedPrefixList.Contains(GetPrefix(tableName)))
                {
                    tableNameList2.Add(tableName);
                }
            }

            int i = -1;
            foreach (string tableName in tableNameList2)
            {
                i++;
                if (progressReporter != null)
                {
                    progressReporter.ChangeProgress(new I3ProgressingEventArgs(0, tableNameList2.Count - 1, i, "处理表" + tableName));
                }
                I3TableInfo tableInfo = I3DBUtil.GetTableInfo(settingItem.Database, tableName, settingItem.TableNeedUnderline, settingItem.FieldNeedUnderline);
                //for .net
                string realOutPath = Path.Combine(settingItem.OutPathData, tableInfo.TableNameEx);
                CreateEntityFile(settingItem, realOutPath, tableInfo);
                CreateEntitiesFile(settingItem, realOutPath, tableInfo);
                CreateParamsFile(settingItem, realOutPath, tableInfo);
                CreateEntityHelperFile(settingItem, settingItem.OutPathDataAccess, tableInfo);
                CreateEntityManagerFile(settingItem, settingItem.OutPathBusiness, tableInfo);
                CreateEntityServerServiceFile(settingItem, settingItem.OutPathServerService, tableInfo);
                CreateEntityClientServiceFile(settingItem, settingItem.OutPathClientService, tableInfo);
                //for Java
                ModelClassInfo mci = CreateModelFileForJava(settingItem, settingItem.OutPathModel, tableInfo);
                CreateParamFileForJava(settingItem, settingItem.OutPathModel, tableInfo);
                CreateMapperFileForJava(settingItem, settingItem.OutPathMapper, tableInfo);
                CreateSqlProviderFileForJava(settingItem, settingItem.OutPathMapper, tableInfo);
                CreateIServiceFileForJava(settingItem, settingItem.OutPathIService, tableInfo);
                CreateServiceImplFileForJava(settingItem, settingItem.OutPathServiceImpl, tableInfo, mci);
                CreateJsonServiceFileForJava(settingItem, settingItem.OutPathJsonService, tableInfo, mci);
                CreateJssFileForH5(settingItem, settingItem.OutPathJss, tableInfo);
                //for go
                CreateModelFileForGo(settingItem, settingItem.goModelOutput, tableInfo);
                CreateMapperFileForGo(settingItem, settingItem.goMapperOutput, tableInfo);
                CreateServiceFileForGo(settingItem, settingItem.goServiceOutput, tableInfo, mci);
            }

            //for Java
            CreateServiceProviderFilesForJava(settingItem, tableNameList2);
            CreateServiceConsumerFilesForJava(settingItem, tableNameList2);

            if (progressReporter != null)
            {
                progressReporter.ChangeProgress(new I3ProgressingEventArgs(0, 1, 1, "结束"));
            }
        }



        /// <summary>
        /// 获取表名前缀
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string GetPrefix(string tableName)
        {
            int index = tableName.IndexOf("_");
            if (index > 0)
            {
                return tableName.Substring(0, index).ToUpper();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 添加引用
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="settingItem"></param>
        private static void AddRefrence(I3DynamicClass dc, string refrenceNameSpace)
        {
            //添加引用的命名空间
            string[] arr = refrenceNameSpace.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                dc.UsingLines.Add("using " + str + ";");
            }
        }

        /// <summary>
        /// 创建实体类
        /// </summary>
        /// <param name="settingItem"></param>
        /// <param name="sourcePath"></param>
        /// <param name="tableName"></param>
        /// <param name="tableInfo"></param>
        private static void CreateEntityFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateData)
            {
                return;
            }

            I3DynamicClass dc = new I3DynamicClass();
            dc.NameSapce = settingItem.NamespaceNameData;
            dc.UsingLines.Add("using IE310.Core.DB;");
            AddRefrence(dc, settingItem.RefrenceNamespaceData);
            //添加类
            dc.ClassAttributes.Add("[Serializable]");
            dc.BaseClasies.Add("I3DataObject");
            dc.IsPartial = true;
            dc.ClassName = tableInfo.EntityClassName;
            //添加字段和属性
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(netType))
                {
                    dc.Fields.Add(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    string propertyName = columnInfo.PropertyName;  //columnInfo.Name.Replace("_", "");
                    string fieldName = columnInfo.FieldName; //"_" + propertyName;
                    string field = "";

                    switch (netType)
                    {
                        case "int":
                            field = string.Format("private {0} {1} = I3DBUtil.INVALID_ID;", netType, fieldName);
                            break;
                        case "decimal":
                            field = string.Format("private {0} {1} = I3DBUtil.INVALID_DECIMAL;", netType, fieldName);
                            break;
                        case "double":
                            field = string.Format("private {0} {1} = I3DBUtil.INVALID_DOUBLE;", netType, fieldName);
                            break;
                        case "float":
                            field = string.Format("private {0} {1} = I3DBUtil.INVALID_FLOAT;", netType, fieldName);
                            break;
                        case "string":  //字符串默认值不能为""，因为有的地方要的就是null
                            field = string.Format("private {0} {1} = null;", netType, fieldName);
                            break;
                        case "DateTime":
                            field = string.Format("private {0} {1} = I3DBUtil.INVALID_DATE;", netType, fieldName);
                            break;
                        default:
                            field = string.Format("private {0} {1};", netType, fieldName);
                            break;
                    }
                    dc.Fields.Add(field);

                    string property = "public " + netType + " " + propertyName + "{get{return " + fieldName
                        + ";}set{if(object.Equals(" + fieldName + ",value)){return;} " + fieldName
                        + " = value;this.OnPropertyChanged(\"" + propertyName + "\");}}";
                    string comment = string.IsNullOrEmpty(columnInfo.Comment) ? "" : columnInfo.Comment.Replace("\r", "").Replace("\n", "").Trim();
                    property = Resource.PropertySummary.Replace("PropertySummary", comment) + "\r\n" + property;
                    dc.Properties.Add(property);
                }
            }

            //保存
            string fileName = Path.Combine(sourcePath, tableInfo.EntityClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, dc.BuildSource(), Encoding.UTF8);
        }


        /// <summary>
        /// 创建实体集合类
        /// </summary>
        /// <param name="settingItem"></param>
        /// <param name="sourcePath"></param>
        /// <param name="tableName"></param>
        /// <param name="tableInfo"></param>
        private static void CreateEntitiesFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateData)
            {
                return;
            }

            string source = Resource.Entities.Replace("DatabaseObject", tableInfo.TableNameEx)
                .Replace("namespaceName", settingItem.NamespaceNameData);
            StringBuilder sb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceData.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("using " + str + ";");
            }
            source = source.Replace("refrenceUsingList", sb.ToString());
            string fileName = Path.Combine(sourcePath, tableInfo.EntitiesClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, source, Encoding.UTF8);
        }

        /// <summary>
        /// 创建查询参数类
        /// </summary>
        /// <param name="settingItem"></param>
        /// <param name="sourcePath"></param>
        /// <param name="tableName"></param>
        /// <param name="tableInfo"></param>
        private static void CreateParamsFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateData)
            {
                return;
            }

            I3DynamicClass dc = new I3DynamicClass();
            dc.NameSapce = settingItem.NamespaceNameData;
            dc.UsingLines.Add("using IE310.Core.DB;");
            AddRefrence(dc, settingItem.RefrenceNamespaceData);
            //添加类
            dc.ClassAttributes.Add("[Serializable]");
            dc.BaseClasies.Add("I3SqlGroupParam");
            dc.IsPartial = true;
            dc.ClassName = tableInfo.ParamsClassName;
            //添加字段和属性
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string netType = DBTypeMapUtil.GetNetType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                string fieldName = columnInfo.PropertyName; //columnInfo.Name.Replace("_", "");
                string field = "";
                switch (netType)
                {
                    case "int":
                        field = "public " + netType + " " + fieldName + " = I3DBUtil.INVALID_ID;";
                        break;
                    case "string": //字符串默认值不能为""，因为有的地方要的就是null
                        field = "public " + netType + " " + fieldName + " = null;";
                        break;
                    case "DateTime":
                        field = "public I3DateSpanParam " + fieldName + " = null;";
                        break;
                    default:
                        break;
                }
                dc.Fields.Add(field);
            }

            //保存
            string fileName = Path.Combine(sourcePath, tableInfo.ParamsClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, dc.BuildSource(), Encoding.UTF8);
        }

        /// <summary>
        /// 创建Helper类
        /// </summary>
        /// <param name="settingItem"></param>
        /// <param name="sourcePath"></param>
        /// <param name="tableName"></param>
        /// <param name="tableInfo"></param>
        private static void CreateEntityHelperFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateDataAccess)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceDataAccess.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("using " + str + ";");
            }
            sb.AppendLine("");

            string source = HelperBuildHelper.BuildCode(tableInfo);
            source = sb.ToString()
                      + @"using IE310.Core.DB;
                            using System.Data.Common;
                            using System.Data;
                            using System;
                            using System.Text;
                            using System.Collections;
                            using System.Collections.Generic;
                            using MySql.Data.MySqlClient;

                            "
                      + "\r\n namespace " + settingItem.NamespaceNameDataAccess
                      + " {\r\n\r\n public static  partial class " + tableInfo.HelperClassName + "\r\n{\r\n" + source + "\r\n\t}\r\n}";

            //保存
            string fileName = Path.Combine(sourcePath, tableInfo.HelperClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, source, Encoding.UTF8);
        }

        /// <summary>
        /// 创建Manager类
        /// </summary>
        /// <param name="settingItem"></param>
        /// <param name="sourcePath"></param>
        /// <param name="tableName"></param>
        /// <param name="tableInfo"></param>
        private static void CreateEntityManagerFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateBusiness)
            {
                return;
            }

            string managerTemplate = tableInfo.IdColumnIsAutoInt ? Resource.ManagerAutoInt : Resource.Manager;
            string source = managerTemplate.Replace("DatabaseObject", tableInfo.TableNameEx)
                .Replace("namespaceName", settingItem.NamespaceNameBusiness);
            StringBuilder sb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceBusiness.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("using " + str + ";");
            }
            source = source.Replace("refrenceUsingList", sb.ToString());
            string fileName = Path.Combine(sourcePath, tableInfo.ManagerClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, source, Encoding.UTF8);
        }


        /// <summary>
        /// 创建Service类
        /// </summary>
        /// <param name="settingItem"></param>
        /// <param name="sourcePath"></param>
        /// <param name="tableName"></param>
        /// <param name="tableInfo"></param>
        private static void CreateEntityServerServiceFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateServerService)
            {
                return;
            }

            string source = Resource.ServerService.Replace("DatabaseObject", tableInfo.TableNameEx)
                .Replace("namespaceName", settingItem.NamespaceNameServerService);
            StringBuilder sb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceServerService.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("using " + str + ";");
            }
            source = source.Replace("refrenceUsingList", sb.ToString());
            string fileName = Path.Combine(sourcePath, tableInfo.ServiceClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, source, Encoding.UTF8);
        }

        private static void CreateEntityClientServiceFile(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateClientService)
            {
                return;
            }

            string source = Resource.ClientService.Replace("DatabaseObject", tableInfo.TableNameEx)
                .Replace("namespaceName", settingItem.NamespaceNameClientService);
            StringBuilder sb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceClientService.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("using " + str + ";");
            }
            source = source.Replace("refrenceUsingList", sb.ToString());
            string fileName = Path.Combine(sourcePath, tableInfo.ServiceClassName + ".cs");
            I3StringUtil.SaveStringToFile(fileName, source, Encoding.UTF8);
        }


        class ModelClassInfo
        {
            public bool hasCreateTime { get; set; }
            public bool hasCreateUserId { get; set; }
            public bool hasUpdateTime { get; set; }
            public bool hasUpdateUserId { get; set; }
            public bool hasName { get; set; }
            public bool hasMobile { get; set; }
            public bool hasCode { get; set; }
            public bool hasSort { get; set; }
            public bool hasPiny { get; set; }
            public bool hasPiny2 { get; set; }
            public bool hasPassword { get; set; }
            public bool hasDescription { get; set; }
        }

        private static ModelClassInfo getModelClassInfo(I3TableInfo tableInfo)
        {
            ModelClassInfo mci = new ModelClassInfo();
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string fieldName = columnInfo.PropertyName;
                if ("createUserId".Equals(fieldName)) { mci.hasCreateUserId = true; }
                if ("createTime".Equals(fieldName)) { mci.hasCreateTime = true; }
                if ("updateUserId".Equals(fieldName)) { mci.hasUpdateUserId = true; }
                if ("updateTime".Equals(fieldName)) { mci.hasUpdateTime = true; }
                if ("name".Equals(fieldName)) { mci.hasName = true; }
                if ("mobile".Equals(fieldName)) { mci.hasMobile = true; }
                if ("code".Equals(fieldName)) { mci.hasCode = true; }
                if ("sort".Equals(fieldName)) { mci.hasSort = true; }
                if ("piny".Equals(fieldName)) { mci.hasPiny = true; }
                if ("piny2".Equals(fieldName)) { mci.hasPiny2 = true; }
                if ("password".Equals(fieldName)) { mci.hasPassword = true; }
                if ("description".Equals(fieldName)) { mci.hasDescription = true; }
            }
            return mci;
        }

        private static ModelClassInfo CreateModelFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            ModelClassInfo mci = getModelClassInfo(tableInfo);
            if (!settingItem.CreateModel)
            {
                return mci;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package " + settingItem.NamespaceNameModel + ";");
            sb.AppendLine();
            sb.AppendLine("import java.io.Serializable;");
            sb.AppendLine("import java.math.BigDecimal;");
            sb.AppendLine("import java.util.Date;");
            string[] arr = settingItem.RefrenceNamespaceModel.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("import " + str + ";");
            }
            sb.AppendLine();
            string baseClassStr = "";
            string comparableStr = "";
            List<string> baseClassFields = new List<string>();
            if (!string.IsNullOrEmpty(settingItem.JavaModelBaseClass))
            {
                string[] strs = settingItem.JavaModelBaseClass.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                baseClassStr = " extends " + strs[0] + " ";
                baseClassFields.AddRange(strs);
                baseClassFields.RemoveAt(0);
                comparableStr = " , Comparable<" + strs[0] + ">";
            }
            sb.AppendLine("public class " + tableInfo.TableNameEx + baseClassStr + " implements Serializable, Cloneable " + comparableStr + " ");
            sb.AppendLine("{");
            sb.AppendLine("protected static final long serialVersionUID = 1L; ");
            sb.AppendLine();
            sb.AppendLine("public " + tableInfo.TableNameEx + "(){	super(); }");
            sb.AppendLine();

            #region 添加字段
            bool hasIdColumn = false;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                if (columnInfo.PropertyName.ToUpper().Equals("ID"))
                {
                    hasIdColumn = true;
                }
                string fieldName = columnInfo.PropertyName;
                string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                if ("createUserId".Equals(fieldName)) { mci.hasCreateUserId = true; }
                if ("createTime".Equals(fieldName)) { mci.hasCreateTime = true; }
                if ("updateUserId".Equals(fieldName)) { mci.hasUpdateUserId = true; }
                if ("updateTime".Equals(fieldName)) { mci.hasUpdateTime = true; }
                if (baseClassFields.Contains(fieldName))
                {
                    continue;
                }
                string javaType = DBTypeMapUtil.GetJavaType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(javaType))
                {
                    sb.AppendLine(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    string field = columnInfo.Name.EndsWith("pt") ? string.Format("protected {0} {1};", javaType, fieldName)//字段以pt结尾，protected
                                                                                                   : string.Format("private {0} {1};", javaType, fieldName);
                    sb.AppendLine(field);
                }
            }
            #endregion

            #region 添加方法
            sb.AppendLine();
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string fieldName = columnInfo.PropertyName;
                string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                if (baseClassFields.Contains(fieldName))
                {
                    continue;
                }
                string javaType = DBTypeMapUtil.GetJavaType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(javaType))
                {
                    sb.AppendLine(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    string methodGet = "public " + javaType + " get" + fieldName2 + "(){return " + fieldName + ";}";
                    sb.AppendLine(methodGet);
                    string methodSet = "public void set" + fieldName2 + "(" + javaType + " " + fieldName + "){this." + fieldName + "=" + fieldName + ";}";
                    sb.AppendLine(methodSet);
                }
            }
            #endregion

            #region 如果没有id字段，添加一个默认的字段和方法，避免编译不通过
            if (!hasIdColumn)
            {
                sb.AppendLine("private String id;public void setId(String id){this.id = id;}  public String getId(){return this.id;}  ");
            }
            #endregion

            #region createTestItem
            sb.AppendLine();
            sb.AppendLine("public static " + tableInfo.TableNameEx + " createTestItem()");
            sb.AppendLine("{");
            sb.AppendLine("     " + tableInfo.TableNameEx + " item = new " + tableInfo.TableNameEx + "();");
            sb.AppendLine();
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string fieldName = columnInfo.PropertyName;
                string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                string javaType = DBTypeMapUtil.GetJavaType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(javaType))
                {
                    sb.AppendLine(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    string testValue = "null";
                    switch (javaType)
                    {
                        case "Integer":
                        case "BigInteger":
                            testValue = "123456";
                            break;
                        case "short":
                            testValue = "12";
                            break;
                        case "Boolean":
                            testValue = "true";
                            break;
                        case "boolean":
                            testValue = "true";
                            break;
                        case "Double":
                        case "Float":
                            testValue = "12345.678";
                            break;
                        case "BigDecimal":
                            testValue = "new BigDecimal(\"12345.678\")";
                            break;
                        case "String":
                            testValue = "\"test_" + fieldName + "\"";
                            break;
                        case "Date":
                            testValue = "new Date(2012, 3, 15, 12, 16, 33)";
                            break;
                        case "Time":
                            testValue = "new Time(12, 16, 33)";
                            break;
                        case "Timestamp":
                            //testValue = "new Time(12, 16, 33)";
                            break;
                        case "byte[]":
                            testValue = "new byte[]{11, 22, 35}";
                            break;
                    }
                    sb.AppendLine("     item." + fieldName + " = " + testValue + ";");
                }
            }
            sb.AppendLine();
            sb.AppendLine("     return item;");
            sb.AppendLine("}");
            sb.AppendLine();
            #endregion

            #region customContent
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("{customContent}");
            sb.AppendLine();
            sb.AppendLine();
            #endregion

            //保存
            sb.AppendLine("}");
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + ".java");
            SaveCodeToFileWithCustomContent(fileName, sb.ToString(), "//", "");

            return mci;
        }

        private static void CreateParamFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateModel)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package " + settingItem.NamespaceNameModel + ";");
            sb.AppendLine();
            sb.AppendLine("import java.io.Serializable;");
            sb.AppendLine("import java.math.BigDecimal;");
            sb.AppendLine("import java.util.Date;");
            string[] arr = settingItem.RefrenceNamespaceModel.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("import " + str + ";");
            }
            sb.AppendLine();
            sb.AppendLine("public class " + tableInfo.TableNameEx + "Param extends BaseParam implements Serializable");
            sb.AppendLine("{");
            sb.AppendLine("	 protected static final long serialVersionUID = 1L; ");
            sb.AppendLine();
            sb.AppendLine("	 public " + tableInfo.TableNameEx + "Param(){	super(); }");
            sb.AppendLine();

            string baseClassStr = "";
            List<string> baseClassFields = new List<string>();
            if (!string.IsNullOrEmpty(settingItem.JavaModelBaseClass))
            {
                string[] strs = settingItem.JavaModelBaseClass.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                baseClassStr = " extends " + strs[0] + " ";
                baseClassFields.AddRange(strs);
                baseClassFields.RemoveAt(0);
            }

            #region 构造函数
            sb.AppendLine("	/**");
            sb.AppendLine("	 * for getPageByParam or findByParam    用于findByParam时，pageSize没有意义，会固定为1");
            sb.AppendLine("	 * pageIndex：页码，从1开始");
            sb.AppendLine("	 * offset:起始位置，从0计算，第n页就是 (n-1)*rowsInPage   rowsInPage:每页的大小");
            sb.AppendLine("	 * order:不带order by 的排序参数");
            sb.AppendLine("	 * cause:不带where的条件，可以加参数，可以为空  如： name=#{name} and code=#{code}，但参数必须是Param中有的字段");
            sb.AppendLine("	 * like条件赋值时，比如name like #{name}  ，需要param.setName(\"%dxx% \");");
            sb.AppendLine("	 */");
            sb.AppendLine("	public " + tableInfo.TableNameEx + "Param(int pageIndex, int pagesize, String orderSql, String causeSql)");
            sb.AppendLine("	{");
            sb.AppendLine("		super(pageIndex, pagesize, orderSql, causeSql);");
            sb.AppendLine("	}");
            sb.AppendLine("");
            sb.AppendLine("	/**");
            sb.AppendLine("	 * for updateByParam");
            sb.AppendLine("	 * updateSql:不带set的更新语句，不可以为空，可以加参数，但参数必须是Param中有的字段");
            sb.AppendLine("	 * cause:不带where的条件，可以为空，可以加参数，  如： name=#{name} and code=#{code}，但参数必须是Param中有的字段");
            sb.AppendLine("	 */");
            sb.AppendLine("	public " + tableInfo.TableNameEx + "Param(String updateSql, String causeSql)");
            sb.AppendLine("	{");
            sb.AppendLine("		super(updateSql, causeSql);");
            sb.AppendLine("	}");
            #endregion

            #region 添加字段
            sb.AppendLine();
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string fieldName = columnInfo.PropertyName;
                string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                if (baseClassFields.Contains(fieldName))
                {
                    continue;
                }
                string javaType = DBTypeMapUtil.GetJavaType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(javaType))
                {
                    sb.AppendLine(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    string field = string.Format("private {0} {1};", javaType, fieldName);
                    sb.AppendLine(field);
                    if (javaType.Equals("String") || javaType.Equals("Boolean"))
                    {
                        sb.AppendLine(string.Format("private {0} {1}Old;", javaType, fieldName));
                        sb.AppendLine(string.Format("private {0} {1}New;", javaType, fieldName));
                    }
                    if (javaType.Equals("Integer") || javaType.Equals("BigInteger") || javaType.Equals("short")
                        || javaType.Equals("BigDecimal") || javaType.Equals("Double") || javaType.Equals("Float") || javaType.Equals("Date"))
                    {
                        sb.AppendLine(string.Format("private {0} {1}Min;", javaType, fieldName));
                        sb.AppendLine(string.Format("private {0} {1}Max;", javaType, fieldName));
                    }
                }
            }
            #endregion

            #region 添加方法
            sb.AppendLine();
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string fieldName = columnInfo.PropertyName;
                string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                if (baseClassFields.Contains(fieldName))
                {
                    continue;
                }
                string javaType = DBTypeMapUtil.GetJavaType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(javaType))
                {
                    sb.AppendLine(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    string methodGet = "public " + javaType + " get" + fieldName2 + "(){return " + fieldName + ";}";
                    sb.AppendLine(methodGet);
                    string methodSet = "public void set" + fieldName2 + "(" + javaType + " " + fieldName + "){this." + fieldName + "=" + fieldName + ";}";
                    sb.AppendLine(methodSet);
                    if (javaType.Equals("String") || javaType.Equals("Boolean"))
                    {
                        sb.AppendLine("public " + javaType + " get" + fieldName2 + "Old(){return " + fieldName + "Old;}");
                        sb.AppendLine("public void set" + fieldName2 + "Old(" + javaType + " " + fieldName + "Old){this." + fieldName + "Old=" + fieldName + "Old;}");
                        sb.AppendLine("public " + javaType + " get" + fieldName2 + "New(){return " + fieldName + "New;}");
                        sb.AppendLine("public void set" + fieldName2 + "New(" + javaType + " " + fieldName + "New){this." + fieldName + "New=" + fieldName + "New;}");
                    }
                    if (javaType.Equals("Integer") || javaType.Equals("BigInteger") || javaType.Equals("short")
                        || javaType.Equals("BigDecimal") || javaType.Equals("Double") || javaType.Equals("Float") || javaType.Equals("Date"))
                    {
                        sb.AppendLine("public " + javaType + " get" + fieldName2 + "Min(){return " + fieldName + "Min;}");
                        sb.AppendLine("public void set" + fieldName2 + "Min(" + javaType + " " + fieldName + "Min){this." + fieldName + "Min=" + fieldName + "Min;}");
                        sb.AppendLine("public " + javaType + " get" + fieldName2 + "Max(){return " + fieldName + "Max;}");
                        sb.AppendLine("public void set" + fieldName2 + "Max(" + javaType + " " + fieldName + "Max){this." + fieldName + "Max=" + fieldName + "Max;}");
                    }
                }
            }
            #endregion

            #region customContent
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("{customContent}");
            sb.AppendLine();
            sb.AppendLine();
            #endregion

            //保存
            sb.AppendLine("}");
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "Param.java");
            SaveCodeToFileWithCustomContent(fileName, sb.ToString(), "//", "");
        }


        private static void CreateMapperFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateMapper)
            {
                return;
            }

            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceMapper.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("import " + str + ";");
            }
            string myBatisMapperAnn = "";
            if (settingItem.AddMyBatisMapperAnn)
            {
                importSb.AppendLine("import  com.jeeplus.core.persistence.annotation.MyBatisMapper;");
                myBatisMapperAnn = "@MyBatisMapper";
            }
            string baseClass = "";
            if (!string.IsNullOrEmpty(settingItem.JavaMapperBaseClass))
            {
                baseClass = settingItem.JavaMapperBaseClass;
                baseClass = " extends " + baseClass.Replace("{ModelClass}", tableInfo.TableNameEx).Replace("{ParamClass}", tableInfo.TableNameEx + "Param");
            }
            string tableName = tableInfo.TableName;
            if (settingItem.UseDbNameWhenGetData)
            {
                tableName = settingItem.Database + "." + tableName;
            }

            StringBuilder sb = new StringBuilder();
            #region save
            {
                sb.AppendLine();
                string insertSql = "insert into " + tableName + "(";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    insertSql = insertSql + columnInfo.Name;
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        insertSql = insertSql + ",";
                    }
                }
                insertSql = insertSql + ") values (";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    insertSql = insertSql + "#{" + columnInfo.Name + "}";
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        insertSql = insertSql + ",";
                    }
                }
                insertSql = insertSql + ")";
                sb.AppendLine("	/**");
                sb.AppendLine("	 * 成功返回1，失败抛出异常");
                sb.AppendLine("	 */");
                sb.AppendLine("	@Insert(\"" + insertSql + "\")");
                if (tableInfo.HasIdKey && tableInfo.IdColumnIsAutoInt)
                {
                    sb.AppendLine("	@Options(useGeneratedKeys = true, keyProperty = \"id\")");
                }
                sb.AppendLine("	public int save(" + tableInfo.TableNameEx + " item);");
            }
            #endregion
            #region update
            {
                sb.AppendLine();
                string updateSql = "update " + tableName + " set ";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    updateSql = updateSql + columnInfo.Name + "=#{" + columnInfo.Name + "}";
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        updateSql = updateSql + ",";
                    }
                }
                updateSql = updateSql + " where id=#{id} ";
                sb.AppendLine("	/**");
                sb.AppendLine("	 * 成功返回1(即使数据没有修改)，失败返回0   注：id不能被修改");
                sb.AppendLine("	 */");
                sb.AppendLine("	@Update(\"" + updateSql + "\")");
                sb.AppendLine("	public int update(" + tableInfo.TableNameEx + " item);");
            }
            #endregion
            #region updateWithVersion
            {
                sb.AppendLine();
                string updateSql = "update " + tableName + " set ";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    updateSql = updateSql + columnInfo.Name + "=#{" + columnInfo.Name + "}";
                    if (columnInfo.Name.ToUpper().Equals("VERSION"))
                    {
                        updateSql = updateSql + "+1 ";
                    }
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        updateSql = updateSql + ",";
                    }
                }
                updateSql = updateSql + " where id=#{id} and version=#{version} ";
                sb.AppendLine("	/**");
                sb.AppendLine("	 * 成功返回1(即使数据没有修改)，失败返回0   注：id不能被修改");
                sb.AppendLine("	 */");
                sb.AppendLine("	@Update(\"" + updateSql + "\")");
                sb.AppendLine("	public int updateWithVersion(" + tableInfo.TableNameEx + " item);");
            }
            #endregion

            String code = Resource.MapperForJava.Replace("{packageName}", settingItem.NamespaceNameMapper)
                .Replace("{idColDataType}", tableInfo.IdColumnDataType(true))
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{myBatisMapperAnn}", myBatisMapperAnn)
                .Replace("{baseClass}", baseClass)
                .Replace("{tableName}", tableName)
                .Replace("{methodWithDynamicField}", sb.ToString());
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "Mapper" + ".java");
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }


        private static void CreateSqlProviderFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateMapper)
            {
                return;
            }

            string tableName = tableInfo.TableName;
            if (settingItem.UseDbNameWhenGetData)
            {
                tableName = settingItem.Database + "." + tableName;
            }

            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceMapper.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("import " + str + ";");
            }
            String code = Resource.SqlProviderForJava.Replace("{packageName}", settingItem.NamespaceNameMapper)
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableName);
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "SqlProvider" + ".java");
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }


        private static void CreateIServiceFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateIService)
            {
                return;
            }

            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceIService.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("import " + str + ";");
            }
            String code = Resource.IServiceForJava.Replace("{packageName}", settingItem.NamespaceNameIService)
                .Replace("{idColDataType}", tableInfo.IdColumnDataType(true))
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableInfo.TableName);
            string fileName = Path.Combine(sourcePath, "I" + tableInfo.TableNameEx + "Service" + ".java");
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }


        private static void CreateServiceImplFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo, ModelClassInfo mci)
        {
            if (!settingItem.CreateServiceImpl)
            {
                return;
            }

            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceServiceImpl.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("import " + str + ";");
            }

            //updatePart、updateParWithVersion都有copy部分的代码，如果用默认注释但可智能判断是否取消了注释的方式，容易受到另一方法中代码的影响，因此这里不做太复杂的处理，仅仅自动生成一部分代码，方便复制即可
            String code = Resource.ServiceImplForJava.Replace("{packageName}", settingItem.NamespaceNameServiceImpl)
                .Replace("{idColDataType}", tableInfo.IdColumnDataType(true))
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableInfo.TableName)
                .Replace("{setPinyCode}", mci.hasName && mci.hasPiny ? "\r\n		item.setPiny(PinyinUtil.getShortPinyin(item.getName(), \"\"));" : "")
                .Replace("{setPiny2Code}", mci.hasName && mci.hasPiny2 ? "\r\n		item.setPiny2(PinyinUtil.getPinyin(item.getName(), \"\"));" : "")
                .Replace("{setPasswordCode}", mci.hasPassword ? "\r\n		item.setPassword(MD5.EncoderByMd5(item.getPassword()));" : "")
                .Replace("{copyCodeDescription}", mci.hasMobile ? "\r\n		//对于updatePart来说，不太确定具体的业务需要更新哪些字段，因此，下面有些代码，是默认注释的，需要时，自行复制到beforeUpdatePart中" : "")
                .Replace("{copyMobileCode}", mci.hasMobile ? "\r\n		//oldItem.setMobile(item.getMobile());" : "")
                .Replace("{copyNameCode}", mci.hasName ? "\r\n		//oldItem.setName(item.getName());" : "")
                .Replace("{copyPinyCode}", mci.hasName && mci.hasPiny ? "\r\n		//oldItem.setPiny(PinyinUtil.getShortPinyin(item.getName(), \"\"));" : "")
                .Replace("{copyPiny2Code}", mci.hasName && mci.hasPiny2 ? "\r\n		//oldItem.setPiny2(PinyinUtil.getPinyin(item.getName(), \"\"));" : "")
                .Replace("{copyCodeCode}", mci.hasCode ? "\r\n		//oldItem.setCode(item.getCode());" : "")
                .Replace("{copySortCode}", mci.hasCode ? "\r\n		//oldItem.setSort(item.getSort());" : "")
                .Replace("{copyDescriptionCode}", mci.hasDescription ? "\r\n		//oldItem.setDescription(item.getDescription());" : "")
                .Replace("{copyPasswordCode}", mci.hasPassword ? "\r\n		if (item.getPassword() != null && !\"\".equals(item.getPassword()))\r\n		{\r\n			oldItem.setPassword(MD5.EncoderByMd5(item.getPassword()));\r\n		}" : "")
                .Replace("{copyUpdateUserIdCode}", mci.hasUpdateUserId ? "\r\n		oldItem.setUpdateUserId(item.getUpdateUserId());" : "")
                .Replace("{copyUpdateTimeCode}", mci.hasUpdateTime ? "\r\n		oldItem.setUpdateTime(item.getUpdateTime());" : "");
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "ServiceImpl" + ".java");
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }

        private static void CreateServiceProviderFilesForJava(SettingItem settingItem, List<string> tableNameList2)
        {
            if (!settingItem.CreateServiceImpl)
            {
                return;
            }

            //组装
            Dictionary<String, StringBuilder> sbDic = new Dictionary<string, StringBuilder>();
            foreach (string tableName in tableNameList2)
            {
                I3TableInfo tableInfo = I3DBUtil.GetTableInfo(settingItem.Database, tableName, settingItem.TableNeedUnderline, settingItem.FieldNeedUnderline);
                string prefix = GetPrefix(tableName);//一个前缀一个文件
                prefix = prefix.ToLower();//小写
                if (!sbDic.ContainsKey(prefix))
                {
                    StringBuilder sb = new StringBuilder();
                    sbDic.Add(prefix, sb);
                }
                string beanName = tableInfo.TableNameEx.Substring(0, 1).ToLower() + tableInfo.TableNameEx.Substring(1);
                sbDic[prefix].AppendLine();
                sbDic[prefix].AppendLine("	<dubbo:service interface=\"" + settingItem.NamespaceNameIService + ".I" + tableInfo.TableNameEx + "Service\" ref=\"" + beanName + "Service\" version=\"1.0.0\" />");
                sbDic[prefix].AppendLine("	<bean id=\"" + beanName + "Service\" class=\"" + settingItem.NamespaceNameServiceImpl + "." + tableInfo.TableNameEx + "ServiceImpl\" />");
            }

            //输出
            if (!string.IsNullOrEmpty(settingItem.ProvidersPath))
            {
                foreach (string prefix in sbDic.Keys)
                {
                    string code = Resource.ServiceProviderForJava.Replace("{serviceDefines}", sbDic[prefix].ToString());
                    string fileName = Path.Combine(settingItem.ProvidersPath, prefix + ".xml");
                    SaveCodeToFileWithCustomContent(fileName, code, "<!--", "-->");
                }
            }
        }

        private static void CreateServiceConsumerFilesForJava(SettingItem settingItem, List<string> tableNameList2)
        {
            if (!settingItem.CreateServiceImpl)
            {
                return;
            }

            //组装
            Dictionary<String, StringBuilder> sbDic = new Dictionary<string, StringBuilder>();
            foreach (string tableName in tableNameList2)
            {
                I3TableInfo tableInfo = I3DBUtil.GetTableInfo(settingItem.Database, tableName, settingItem.TableNeedUnderline, settingItem.FieldNeedUnderline);
                string prefix = GetPrefix(tableName);//一个前缀一段xml
                prefix = prefix.ToLower();//小写
                if (!sbDic.ContainsKey(prefix))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    sb.AppendLine("	<!-- " + prefix + " -->");
                    sbDic.Add(prefix, sb);
                }
                string beanName = tableInfo.TableNameEx.Substring(0, 1).ToLower() + tableInfo.TableNameEx.Substring(1);
                sbDic[prefix].AppendLine("	<dubbo:reference id=\"" + beanName + "Service\" interface=\"" + settingItem.NamespaceNameIService + ".I" + tableInfo.TableNameEx + "Service\" version=\"1.0.0\" />");
            }

            //输出
            StringBuilder sbServiceDefine = new StringBuilder();
            foreach (StringBuilder sb in sbDic.Values)
            {
                sbServiceDefine.Append(sb.ToString());
            }
            if (!string.IsNullOrEmpty(settingItem.ConsumerFiles))
            {
                string[] strs = settingItem.ConsumerFiles.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strs)
                {
                    string code = Resource.ServiceConsumerForJava.Replace("{serviceDefines}", sbServiceDefine.ToString());
                    string fileName = str;
                    SaveCodeToFileWithCustomContent(fileName, code, "<!--", "-->");
                }
            }
        }


        private static void SaveCodeToFileWithCustomContent(string fileName, string code, string remarkStart, string remarkEnd)
        {
            string oldCode = File.Exists(fileName) ? I3StringUtil.ReadStringFromFile(fileName, Encoding.UTF8) : "";
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customRefrence");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeGetListContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterGetListContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeAddContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterAddContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeUpdateContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterUpdateContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeUpdatePartContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterUpdatePartContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeDeleteContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterDeleteContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterGetContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterGetItemContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeDeleteByParamContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterDeleteByParamContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterGetArrContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeUpdateByParamContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterUpdateByParamContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeSaveContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterSaveContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeUpdateWithVersionContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterUpdateWithVersionContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customBeforeUpdatePartWithVersionContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "customAfterUpdatePartWithVersionContent");
            code = replaceCodeWithCustomContent(fileName, oldCode, code, remarkStart, remarkEnd, "goExportsSettings");
            I3StringUtil.SaveStringToFile(fileName, code, Encoding.UTF8);
        }

        private static string getCustomContent(string oldCode, string remarkStart, string remarkEnd, string customContentFlag)
        {
            string customContentBegin = customContentFlag + "Begin";
            string customContentEnd = customContentFlag + "End";
            string customStartStr = remarkStart + customContentBegin + remarkEnd;
            string customEndStr = remarkStart + customContentEnd + remarkEnd;

            string customContent = "";
            if (!string.IsNullOrEmpty(oldCode))
            {
                int customStart = oldCode.IndexOf(customStartStr);
                int customEnd = oldCode.IndexOf(customEndStr);
                if (customStart >= 0 && customEnd >= 0 && customEnd > customStart)
                {
                    customContent = oldCode.Substring(customStart);
                    int length = customEnd - customStart + customEndStr.Length;
                    customContent = customContent.Substring(0, length);
                }
            }
            return customContent;
        }

        private static Dictionary<string, Dictionary<string, int>> fileFlagBlanks;  //.go-----flag:tabcount
        private static Dictionary<string, Dictionary<string, int>> getGoFileBalankFlags()
        {
            if (fileFlagBlanks == null)
            {
                fileFlagBlanks = new Dictionary<string, Dictionary<string, int>>();

                //.go
                Dictionary<string, int> dic = new Dictionary<string, int>();
                fileFlagBlanks.Add(".go", dic);
                dic.Add("customRefrenceBegin", 1);
                dic.Add("customAfterGetContentBegin", 1);
                dic.Add("customBeforeGetListContentBegin", 1);
                dic.Add("customAfterGetListContentBegin", 1);
                dic.Add("customAfterGetItemContentBegin", 1);
                dic.Add("customBeforeAddContentBegin", 1);
                dic.Add("customAfterAddContentBegin", 1);
                dic.Add("customBeforeUpdateContentBegin", 1);
                dic.Add("customAfterUpdateContentBegin", 1);
                dic.Add("customBeforeDeleteContentBegin", 1);
                dic.Add("customAfterDeleteContentBegin", 1);
            }
            return fileFlagBlanks;
        }

        private static string replaceCodeWithCustomContent(string fileName, string oldCode, string code, string remarkStart, string remarkEnd, string customContentFlag)
        {
            string customContentBegin = customContentFlag + "Begin";
            string customContentEnd = customContentFlag + "End";
            string customStartStr = remarkStart + customContentBegin + remarkEnd;
            string customEndStr = remarkStart + customContentEnd + remarkEnd;
            string customContent = getCustomContent(oldCode, remarkStart, remarkEnd, customContentFlag);

            //customEndStr前面的空格处理
            string ext = Path.GetExtension(fileName).ToLower();
            string blankBeforeBegin = "";
            var flags = getGoFileBalankFlags();
            if (flags.ContainsKey(ext) && flags[ext].ContainsKey(customContentBegin))
            {
                int count = flags[ext][customContentBegin];
                for (int i = 1; i <= count; i++)
                {
                    blankBeforeBegin = blankBeforeBegin + "	";
                }
            }


            //空值处理
            //string step = (customContentFlag.Equals("customContent") || customContentFlag.Equals("jsbc")) ? "	" : "		";
            if (string.IsNullOrEmpty(customContent))
            {
                if (customContentFlag.Equals("jsbc"))  //JsonServiceBaseCalss，不需要换行的
                {
                    customContent = blankBeforeBegin + customStartStr + "LoginAndCheckPowerJsonServiceBase" + customEndStr;
                }
                else//需要换行的
                {
                    //customContent = customStartStr + "\r\n";
                    //if (customContentFlag.Equals("customContent"))
                    //{
                    //    customContent = customContent + step + remarkStart + "注意，自定义内容必须放在" + customContentBegin + "、" + customContentEnd + "之间，否则重新生成时会被覆盖" + remarkEnd;
                    //}
                    //customContent = customContent + "\r\n" + step + customEndStr;
                    string appendStr = customContentFlag.Equals("goExportsSettings") ? "//false;Get;GetList;Add;Update;Delete;注意:修改true/false后要重新生成一次;\r\n" : "";
                    customContent = blankBeforeBegin + customStartStr + "\r\n" + appendStr + customEndStr;
                }
            }

            //code = code.Replace("{" + customContentFlag + "}", step + customContent);
            code = code.Replace("{" + customContentFlag + "}", blankBeforeBegin + customContent);
            return code;
        }


        private static void CreateJsonServiceFileForJava(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo, ModelClassInfo mci)
        {
            if (!settingItem.CreateJsonService)
            {
                return;
            }
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "Service" + ".java");
            if (!File.Exists(fileName))
            {
                return;
            }

            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.RefrenceNamespaceJsonService.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("import " + str + ";");
            }
            String code = Resource.JsonServiceForJava.Replace("{packageName}", settingItem.NamespaceNameJsonService)
                .Replace("{idColDataType}", tableInfo.IdColumnDataType(true))
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableInfo.TableName)
                .Replace("{tableNameEx2}", tableInfo.TableNameEx2)
                .Replace("{setCreateUserIdCode}", mci.hasCreateUserId ? "\r\n		item.setCreateUserId(this.getUser().getId());" : "")
                .Replace("{setCreateTimeCode}", mci.hasCreateTime ? "\r\n		item.setCreateTime(new Date());" : "")
                .Replace("{setUpdateUserIdCode}", mci.hasUpdateUserId ? "\r\n		item.setUpdateUserId(this.getUser().getId());" : "")
                .Replace("{setUpdateTimeCode}", mci.hasUpdateTime ? "\r\n		item.setUpdateTime(new Date());" : "");
            string oldCode = File.Exists(fileName) ? I3StringUtil.ReadStringFromFile(fileName, Encoding.UTF8) : "";
            code = replaceCodeWithCustomContent(fileName, oldCode, code, "/*", "*/", "jsbc");
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }


        private static void CreateJssFileForH5(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.CreateJss)
            {
                return;
            }
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx2 + "Jss.js");
            if (!File.Exists(fileName))
            {
                return;
            }

            //保存
            String code = Resource.JssFileForH5.Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableInfo.TableName)
                .Replace("{tableNameEx2}", tableInfo.TableNameEx2)
                .Replace("{suffixJss}", settingItem.SuffixJss);
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }



        private static ModelClassInfo CreateModelFileForGo(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            ModelClassInfo mci = getModelClassInfo(tableInfo);
            if (!settingItem.createGoModel)
            {
                return mci;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package model");
            sb.AppendLine();
            sb.AppendLine("import (");
            string[] arr = settingItem.goModelRefrence.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                sb.AppendLine("     " + str + "");
            }
            sb.AppendLine("{customRefrence}");
            sb.AppendLine(")");
            sb.AppendLine();

            sb.AppendLine("//" + tableInfo.TableNameEx + " ..");
            sb.AppendLine("type " + tableInfo.TableNameEx + " struct {");

            #region 添加字段
            StringBuilder fieldsSb = new StringBuilder();
            fieldsSb.Append("var " + tableInfo.TableNameEx + "Fields = map[string]int{");
            int fieldIndex = -1;
            foreach (I3ColumnInfo columnInfo in tableInfo)
            {
                string fieldName = columnInfo.PropertyName;
                fieldIndex++;
                fieldsSb.Append("\"").Append(fieldName.ToLower()).Append("\": ").Append(fieldIndex);
                if(fieldIndex != tableInfo.Count - 1)
                {
                    fieldsSb.Append(", ");
                }
                string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                if (fieldName2.StartsWith("id") || fieldName2.StartsWith("Id") || fieldName2.EndsWith("id") || fieldName2.EndsWith("Id"))
                {
                    fieldName2 = fieldName2.Replace("id", "ID").Replace("Id", "ID");  //go lint格式要求
                }
                string goType = DBTypeMapUtil.GetGoType(I3DBUtil.DBServerType.ToString(), columnInfo.TypeName);
                if (string.IsNullOrEmpty(goType))
                {
                    sb.AppendLine(string.Format("不支持类型{0}", columnInfo.TypeName));
                }
                else
                {
                    //string field = columnInfo.Name.EndsWith("pt") ? string.Format("protected {0} {1};", javaType, fieldName)//字段以pt结尾，protected
                    //                                                                               : string.Format("private {0} {1};", javaType, fieldName);
                    string field = "	" + fieldName2 + "   " + goType + "  `json:\"" + fieldName + "\"  db:\"" + fieldName + "\"  `";
                    sb.AppendLine(field);
                }
            }
            fieldsSb.Append("}");
            #endregion

            #region customContent
            sb.AppendLine();
            sb.AppendLine("{customContent}");
            sb.AppendLine();
            #endregion

            //保存
            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine(fieldsSb.ToString());
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + ".go");
            SaveCodeToFileWithCustomContent(fileName, sb.ToString(), "//", "");

            return mci;
        }


        private static void CreateMapperFileForGo(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo)
        {
            if (!settingItem.createGoMapper)
            {
                return;
            }

            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.goMapperRefrence.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("	" + str + "");
            }

            string tableName = tableInfo.TableName;
            if (settingItem.UseDbNameWhenGetData)
            {
                tableName = settingItem.Database + "." + tableName;
            }

            StringBuilder sb = new StringBuilder();
            #region save
            {
                string insertSql = "insert into \" + m.TableName + \"(";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    insertSql = insertSql + columnInfo.Name;
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        insertSql = insertSql + ",";
                    }
                }
                insertSql = insertSql + ") values (";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    insertSql = insertSql + "?";
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        insertSql = insertSql + ",";
                    }
                }
                insertSql = insertSql + ")";
                string argsStr = "var args = []interface{}{";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    string fieldName = columnInfo.PropertyName;
                    string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                    if (fieldName2.StartsWith("id") || fieldName2.StartsWith("Id") || fieldName2.EndsWith("id") || fieldName2.EndsWith("Id"))
                    {
                        fieldName2 = fieldName2.Replace("id", "ID").Replace("Id", "ID");  //go lint格式要求
                    }
                    argsStr = argsStr + "item." + fieldName2;
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        argsStr = argsStr + ", ";
                    }
                }
                argsStr = argsStr + "}";
                sb.AppendLine("//Insert 插入数据");
                sb.AppendLine("func (m " + tableInfo.TableNameEx + "Mapper) Insert(tx *sqlx.Tx, item *model." + tableInfo.TableNameEx + ") (int, int) {");
                sb.AppendLine("	sqlStr := \"" + insertSql + "\"");
                sb.AppendLine("	" + argsStr);
                sb.AppendLine("	id, count := m.InsertItem(tx, sqlStr, args...)");
                sb.AppendLine("	return id, count");
                sb.AppendLine("}");
            }
            #endregion
            #region update
            {
                sb.AppendLine();
                string updateSql = "update \" + m.TableName + \" set ";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    updateSql = updateSql + columnInfo.Name + "=?";
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        updateSql = updateSql + ",";
                    }
                }
                string argsStr = "var args = []interface{}{";
                foreach (I3ColumnInfo columnInfo in tableInfo)
                {
                    if (tableInfo.IsAutoIntColumn(columnInfo))
                    {
                        continue;
                    }
                    string fieldName = columnInfo.PropertyName;
                    string fieldName2 = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                    if (fieldName2.StartsWith("id") || fieldName2.StartsWith("Id") || fieldName2.EndsWith("id") || fieldName2.EndsWith("Id"))
                    {
                        fieldName2 = fieldName2.Replace("id", "ID").Replace("Id", "ID");  //go lint格式要求
                    }
                    argsStr = argsStr + "item." + fieldName2;
                    if (columnInfo != tableInfo[tableInfo.Count - 1])
                    {
                        argsStr = argsStr + ", ";
                    }
                }
                argsStr = argsStr + ", item.ID";
                argsStr = argsStr + "}";
                updateSql = updateSql + " where id=? ";
                sb.AppendLine("//Update 更新数据");
                sb.AppendLine("func (m " + tableInfo.TableNameEx + "Mapper) Update(tx *sqlx.Tx, item *model." + tableInfo.TableNameEx + ") int {");
                sb.AppendLine("	sqlStr := \"" + updateSql + "\"");
                sb.AppendLine("	" + argsStr);
                sb.AppendLine("	count := m.DeleteOrUpdateItems(tx, sqlStr, args...)");
                sb.AppendLine("	return count");
                sb.AppendLine("}");
            }
            #endregion

            String code = Resource.MapperForGo.Replace("{packageName}", settingItem.NamespaceNameMapper)
                //.Replace("{idColDataType}", tableInfo.IdColumnDataType(true))
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableName)
                .Replace("{methodWithDynamicField}", sb.ToString());  //Insert  Update
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "Mapper" + ".go");
            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }


        private static void CreateServiceFileForGo(SettingItem settingItem, string sourcePath, I3TableInfo tableInfo, ModelClassInfo mci)
        {
            if (!settingItem.createGoService)
            {
                return;
            }


            StringBuilder importSb = new StringBuilder();
            string[] arr = settingItem.goServiceRefrence.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                importSb.AppendLine("	" + str + "");
            }

            //exports
            string fileName = Path.Combine(sourcePath, tableInfo.TableNameEx + "Service" + ".go");
            string oldCode = File.Exists(fileName) ? I3StringUtil.ReadStringFromFile(fileName, Encoding.UTF8) : "";
            string ess = getCustomContent(oldCode, "//", "", "goExportsSettings")
                .Replace("//goExportsSettingsBegin", "")
                .Replace("//goExportsSettingsEnd", "")
                .Trim();
            string getMethodName = ess.IndexOf("//true;") == 0 && ess.IndexOf(";Get;") > 0 ? "Get" : "get";
            string getListMethodName = ess.IndexOf("//true;") == 0 && ess.IndexOf(";GetList;") > 0 ? "GetList" : "getList";
            string addMethodName = ess.IndexOf("//true;") == 0 && ess.IndexOf(";Add;") > 0 ? "Add" : "add";
            string updateMethodName = ess.IndexOf("//true;") == 0 && ess.IndexOf(";Update;") > 0 ? "Update" : "update";
            string deleteMethodName = ess.IndexOf("//true;") == 0 && ess.IndexOf(";Delete;") > 0 ? "Delete" : "delete";

            string idColumnType = tableInfo.IdColumnTypeForGo();
            string idIsStringValue = string.Equals(idColumnType, "string") ? "true" : "false";
            string idIsStringDes1 = string.Equals(idColumnType, "string") ? "" : "//";  //id不为string时注释内容
            string idIsStringDes2 = string.Equals(idColumnType, "string") ? "//" : "";  //id为string时注释内容
            string lastIntIdVar = string.Equals(idColumnType, "string") ? "_" : "lastIntID";  //id为string时不获取lastIntId
            String code = Resource.ServiceForGo
                .Replace("{idColumnGetMethodForGoService}", tableInfo.IdColumnGetMethodForGoService())
                .Replace("{importList}", importSb.ToString())
                .Replace("{entityName}", tableInfo.TableNameEx)
                .Replace("{tableName}", tableInfo.TableName)
                .Replace("{getMethodName}", getMethodName)
                .Replace("{getListMethodName}", getListMethodName)
                .Replace("{addMethodName}", addMethodName)
                .Replace("{updateMethodName}", updateMethodName)
                .Replace("{deleteMethodName}", deleteMethodName)
                .Replace("{idIsStringValue}", idIsStringValue)
                .Replace("{idIsStringDes1}", idIsStringDes1)
                .Replace("{idIsStringDes2}", idIsStringDes2)
                .Replace("{idColumnType}", idColumnType)
                .Replace("{lastIntIdVar}", lastIntIdVar);



            SaveCodeToFileWithCustomContent(fileName, code, "//", "");
        }



    }

    public delegate void OutTime();
}
