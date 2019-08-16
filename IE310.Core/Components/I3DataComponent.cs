using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using IE310.Core.Data;

namespace IE310.Core.Components
{
    public partial class I3DataComponent : Component
    {
        public I3DataComponent()
        {
            InitializeComponent();
        }

        private I3DataSourceType dataSourceType = I3DataSourceType.dstOLE;
        /// <summary>
        /// 数据连接的类型
        /// </summary>
        public I3DataSourceType DataSourceType
        {
            get
            {
                return dataSourceType;
            }
            set
            {
                dataSourceType = value;
            }
        }

        private string connectionString = "";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

       


        private I3Data data = null;
        /// <summary>
        /// 返回数据连接对象  
        /// 第一次调用时才实际生成数据连接对象
        /// </summary>
        public I3Data Data
        {
            get
            {
                if (data == null)
                {
                    switch (dataSourceType)
                    {
                        case I3DataSourceType.dstSQL:
                            {
                                data = I3Data.CreateDataSql(connectionString);
                                break;
                            }
                        case I3DataSourceType.dstOLE:
                            {
                                data = I3Data.CreateDataOle(connectionString);
                                break;
                            }
                        case I3DataSourceType.dstXML:
                            {
                                data = I3Data.CreateDataXml(connectionString);
                                break;
                            }
                    }
                }

                return data;
            }
        }

        /// <summary>
        /// 返回是否连接到数据库
        /// </summary>
        public bool Active
        {
            get
            {
                return Data.Active;
            }
        }
    }

    public enum I3DataSourceType
    {
        dstSQL = 0,
        dstOLE = 1,
        dstXML = 2
    }
}
