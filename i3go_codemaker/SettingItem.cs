using System;
using System.Collections.Generic;
using System.Text;
using IE310.Core.DB;

namespace IE310.Tools.CodeMaker
{
    public class SettingItems : SortedList<string, SettingItem>
    {
    }

    public class SettingItem
    {

        /// <summary>
        /// 用于在检测到一次过期后，在配置文件（注：只是配置文件中一个方案）中标记过期，这样即使将时间改回去，仍然视为过期
        /// 注意，如果修改SourceBuildHelper中的过期时间，这里的属性名也要相应更改，不然已过期的配置文件将不能使用
        /// </summary>
        public bool Flag5
        {
            get;set;
        }



        public SettingItem(string schemaName)
        {
            this.schemaName = schemaName;
        }

        private string schemaName;
        /// <summary>
        /// 方案名称
        /// </summary>
        public string SchemaName
        {
            get
            {
                return schemaName;
            }
            set
            {
                schemaName = value;
            }
        }

        private DBServerType dbServerType;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBServerType DBServerType
        {
            get
            {
                return dbServerType;
            }
            set
            {
                dbServerType = value;
            }
        }

        private string server;
        /// <summary>
        /// 服务器
        /// </summary>
        public string Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
            }
        }

        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        private string password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private string database;
        /// <summary>
        /// 数据库
        /// </summary>
        public string Database
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
            }
        }

        private List<string> prefixList;
        /// <summary>
        /// 所有前缀
        /// </summary>
        public List<string> PrefixList
        {
            get
            {
                if (prefixList == null)
                {
                    prefixList = new List<string>();
                }
                return prefixList;
            }
            set
            {
                prefixList = value;
            }
        }


        private List<string> selectedPrefixList;
        /// <summary>
        /// 已选前缀
        /// </summary>
        public List<string> SelectedPrefixList
        {
            get
            {
                if (selectedPrefixList == null)
                {
                    selectedPrefixList = new List<string>();
                }
                return selectedPrefixList;
            }
            set
            {
                selectedPrefixList = value;
            }
        }

        private string namespaceNameData;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameData
        {
            get
            {
                return namespaceNameData;
            }
            set
            {
                namespaceNameData = value;
            }
        }

        private string namespaceNameDataAccess;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameDataAccess
        {
            get
            {
                return namespaceNameDataAccess;
            }
            set
            {
                namespaceNameDataAccess = value;
            }
        }

        private string namespaceNameBusiness;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameBusiness
        {
            get
            {
                return namespaceNameBusiness;
            }
            set
            {
                namespaceNameBusiness = value;
            }
        }

        private string namespaceNameServerService;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameServerService
        {
            get
            {
                return namespaceNameServerService;
            }
            set
            {
                namespaceNameServerService = value;
            }
        }

        private string namespaceNameClientService;
        public string NamespaceNameClientService
        {
            get
            {
                return namespaceNameClientService;
            }
            set
            {
                namespaceNameClientService = value;
            }
        }

        private string namespaceNameModel;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameModel
        {
            get
            {
                return namespaceNameModel;
            }
            set
            {
                namespaceNameModel = value;
            }
        }


        private string namespaceNameMapper;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameMapper
        {
            get
            {
                return namespaceNameMapper;
            }
            set
            {
                namespaceNameMapper = value;
            }
        }


        private string namespaceNameIService;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameIService
        {
            get
            {
                return namespaceNameIService;
            }
            set
            {
                namespaceNameIService = value;
            }
        }

        private string namespaceNameJsonService;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameJsonService
        {
            get
            {
                return namespaceNameJsonService;
            }
            set
            {
                namespaceNameJsonService = value;
            }
        }

        private string suffixJss;
        /// <summary>
        /// Service后缀
        /// </summary>
        public string SuffixJss
        {
            get
            {
                return suffixJss;
            }
            set
            {
                suffixJss = value;
            }
        }

        private string namespaceNameServiceImpl;
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceNameServiceImpl
        {
            get
            {
                return namespaceNameServiceImpl;
            }
            set
            {
                namespaceNameServiceImpl = value;
            }
        }

        private string outPathData;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathData
        {
            get
            {
                return outPathData;
            }
            set
            {
                outPathData = value;
            }
        }

        private string outPathDataAccess;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathDataAccess
        {
            get
            {
                return outPathDataAccess;
            }
            set
            {
                outPathDataAccess = value;
            }
        }

        private string outPathBusiness;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathBusiness
        {
            get
            {
                return outPathBusiness;
            }
            set
            {
                outPathBusiness = value;
            }
        }

        private string outPathServerService;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathServerService
        {
            get
            {
                return outPathServerService;
            }
            set
            {
                outPathServerService = value;
            }
        }

        private string outPathClientService;
        public string OutPathClientService
        {
            get
            {
                return outPathClientService;
            }
            set
            {
                outPathClientService = value;
            }
        }


        private string outPathModel;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathModel
        {
            get
            {
                return outPathModel;
            }
            set
            {
                outPathModel = value;
            }
        }

        private string outPathMapper;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathMapper
        {
            get
            {
                return outPathMapper;
            }
            set
            {
                outPathMapper = value;
            }
        }


        private string outPathIService;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathIService
        {
            get
            {
                return outPathIService;
            }
            set
            {
                outPathIService = value;
            }
        }


        private string outPathJsonService;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathJsonService
        {
            get
            {
                return outPathJsonService;
            }
            set
            {
                outPathJsonService = value;
            }
        }


        private string outPathJss;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathJss
        {
            get
            {
                return outPathJss;
            }
            set
            {
                outPathJss = value;
            }
        }

        private string outPathServiceImpl;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPathServiceImpl
        {
            get
            {
                return outPathServiceImpl;
            }
            set
            {
                outPathServiceImpl = value;
            }
        }

        private string providersPath;
        /// <summary>
        /// java服务提供者配置文件目录
        /// </summary>
        public string ProvidersPath
        {
            get
            {
                return providersPath;
            }
            set
            {
                providersPath = value;
            }
        }


        private string consumerFiles;
        /// <summary>
        /// java服务消费者文件路径
        /// </summary>
        public string ConsumerFiles
        {
            get
            {
                return consumerFiles;
            }
            set
            {
                consumerFiles = value;
            }
        }

        private string customConsumeFileSyn;
        /// <summary>
        /// 记录两个或多个自定义  消费者配置文件 的目录，将后面的同步为第一个的内容
        /// </summary>
        public string CustomConsumeFileSyn
        {
            get
            {
                return customConsumeFileSyn;
            }
            set
            {
                customConsumeFileSyn = value;
            }
        }


        private bool createData;
        /// <summary>
        /// 是否创建Data层
        /// </summary>
        public bool CreateData
        {
            get
            {
                return createData;
            }
            set
            {
                createData = value;
            }
        }

        private bool createDataAccess;
        /// <summary>
        /// 是否创建DAL层
        /// </summary>
        public bool CreateDataAccess
        {
            get
            {
                return createDataAccess;
            }
            set
            {
                createDataAccess = value;
            }
        }

        private bool createBusiness;
        /// <summary>
        /// 是否创建Business层
        /// </summary>
        public bool CreateBusiness
        {
            get
            {
                return createBusiness;
            }
            set
            {
                createBusiness = value;
            }
        }

        private bool createServerService;
        /// <summary>
        /// 是否创建ServiceService层
        /// </summary>
        public bool CreateServerService
        {
            get
            {
                return createServerService;
            }
            set
            {
                createServerService = value;
            }
        }

        private bool createClientService;
        /// <summary>
        /// 是否创建ClientService层
        /// </summary>
        public bool CreateClientService
        {
            get
            {
                return createClientService;
            }
            set
            {
                createClientService = value;
            }
        }


        private bool createModel;
        /// <summary>
        /// 是否创建Model层
        /// </summary>
        public bool CreateModel
        {
            get
            {
                return createModel;
            }
            set
            {
                createModel = value;
            }
        }

        private bool createMapper;
        /// <summary>
        /// 是否创建Mapper层
        /// </summary>
        public bool CreateMapper
        {
            get
            {
                return createMapper;
            }
            set
            {
                createMapper = value;
            }
        }

        private bool createIService;
        /// <summary>
        /// 是否创建IService层
        /// </summary>
        public bool CreateIService
        {
            get
            {
                return createIService;
            }
            set
            {
                createIService = value;
            }
        }


        private bool createJsonService;
        /// <summary>
        /// 是否创建JsonService层
        /// </summary>
        public bool CreateJsonService
        {
            get
            {
                return createJsonService;
            }
            set
            {
                createJsonService = value;
            }
        }

        private bool createJss;
        /// <summary>
        /// 是否创建Jss层
        /// </summary>
        public bool CreateJss
        {
            get
            {
                return createJss;
            }
            set
            {
                createJss = value;
            }
        }

        private bool createServiceImpl;
        /// <summary>
        /// 是否创建ServiceImpl层
        /// </summary>
        public bool CreateServiceImpl
        {
            get
            {
                return createServiceImpl;
            }
            set
            {
                createServiceImpl = value;
            }
        }

        private string refrenceNamespaceData;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceData
        {
            get
            {
                return refrenceNamespaceData;
            }
            set
            {
                refrenceNamespaceData = value;
            }
        }

        private string refrenceNamespaceDataAccess;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceDataAccess
        {
            get
            {
                return refrenceNamespaceDataAccess;
            }
            set
            {
                refrenceNamespaceDataAccess = value;
            }
        }

        private string refrenceNamespaceBusiness;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceBusiness
        {
            get
            {
                return refrenceNamespaceBusiness;
            }
            set
            {
                refrenceNamespaceBusiness = value;
            }
        }

        private string refrenceNamespaceServiceService;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceServerService
        {
            get
            {
                return refrenceNamespaceServiceService;
            }
            set
            {
                refrenceNamespaceServiceService = value;
            }
        }

        private string refrenceNamespaceClientService;
        public string RefrenceNamespaceClientService
        {
            get
            {
                return refrenceNamespaceClientService;
            }
            set
            {
                refrenceNamespaceClientService = value;
            }
        }

        private string refrenceNamespaceModel;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceModel
        {
            get
            {
                return refrenceNamespaceModel;
            }
            set
            {
                refrenceNamespaceModel = value;
            }
        }

        private string javaModelBaseClass;

        public string JavaModelBaseClass
        {
            get
            {
                return javaModelBaseClass;
            }
            set
            {
                javaModelBaseClass = value;
            }
        }


        private string refrenceNamespaceMapper;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceMapper
        {
            get
            {
                return refrenceNamespaceMapper;
            }
            set
            {
                refrenceNamespaceMapper = value;
            }
        }

        private string javaMapperBaseClass;

        public string JavaMapperBaseClass
        {
            get
            {
                return javaMapperBaseClass;
            }
            set
            {
                javaMapperBaseClass = value;
            }
        }

        private bool addMyBatisMapperAnn;
        /// <summary>
        /// 是否添加@MyBatisMapper注解
        /// </summary>
        public bool AddMyBatisMapperAnn
        {
            get
            {
                return addMyBatisMapperAnn;
            }
            set
            {
                addMyBatisMapperAnn = value;
            }
        }


        private string refrenceNamespaceIService;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceIService
        {
            get
            {
                return refrenceNamespaceIService;
            }
            set
            {
                refrenceNamespaceIService = value;
            }
        }

        private string refrenceNamespaceJsonService;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceJsonService
        {
            get
            {
                return refrenceNamespaceJsonService;
            }
            set
            {
                refrenceNamespaceJsonService = value;
            }
        }
         
        private string refrenceNamespaceJss;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceJss
        {
            get
            {
                return refrenceNamespaceJss;
            }
            set
            {
                refrenceNamespaceJss = value;
            }
        }

        private string refrenceNamespaceServiceImpl;
        /// <summary>
        /// 引用的命名空间，以分号相隔
        /// </summary>
        public string RefrenceNamespaceServiceImpl
        {
            get
            {
                return refrenceNamespaceServiceImpl;
            }
            set
            {
                refrenceNamespaceServiceImpl = value;
            }
        }

        private bool fieldNeedUnderline = false;

        public bool FieldNeedUnderline
        {
            get
            {
                return fieldNeedUnderline;
            }
            set
            {
                fieldNeedUnderline = value;
            }
        }

        private bool useDbNameWhenGetData;

        public bool UseDbNameWhenGetData
        {
            get
            {
                return useDbNameWhenGetData;
            }
            set
            {
                useDbNameWhenGetData = value;
            }
        }


        private bool tableNeedUnderline = false;

        public bool TableNeedUnderline
        {
            get
            {
                return tableNeedUnderline;
            }
            set
            {
                tableNeedUnderline = value;
            }
        }


        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return DBServerType + ":" + I3DBUtil.CreateConnectionString(DBServerType, Server, UserName, Password, Database);
        }

        public bool createGoModel;
        public bool createGoMapper;
        public bool createGoService;
        public string goModelRefrence { get; set; }
        public string goModelOutput { get; set; }
        public string goMapperRefrence { get; set; }
        public string goMapperOutput { get; set; }
        public string goServiceRefrence { get; set; }
        public string goServiceOutput { get; set; }
    }
}
