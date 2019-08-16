using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace IE310.Core.DB
{
    public class I3SqlServerBatchCommand : II3BatchCommand
    {
        private List<SqlParameter> parameters = new List<SqlParameter>();
        // private ArrayList parametersVals = new ArrayList();
        private string sql;
        private DataTable commandTable = new DataTable();

        public DataTable CommandTable
        {
            get
            {
                return commandTable;
            }
            // set { commandTable = value; }
        }
        private string tableName;
        private I3SqlServerBatchUpdater batchUpdater = null;

        public I3SqlServerBatchUpdater BatchUpdater
        {
            get
            {
                return batchUpdater;
            }
            set
            {
                batchUpdater = value;
            }
        }
        public I3SqlServerBatchCommand()
            : this("")
        {
            DataTable dnTable = new DataTable();
            DataRow newRow = dnTable.NewRow();
            dnTable.Rows.Add(newRow);
            //newRow.RowState = DataRowState.Detached;
        }
        public I3SqlServerBatchCommand(I3SqlServerBatchUpdater updater)
        {
            batchUpdater = updater;
        }
        /// <summary>
        /// 创建Oracle92批量执行命令的一个实例。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        public I3SqlServerBatchCommand(string sql)
        {
            this.sql = sql;
            this.parameters = new List<SqlParameter>();
            //  this.parametersVals = new ArrayList(50);
        }
        /// <summary>
        /// 获取参数集合
        /// </summary>
        public List<SqlParameter> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        /// <summary>
        /// 参数值集合
        /// </summary>
        public ArrayList ParametersVals
        {
            get
            {
                return null;
            }// this.parametersVals; 
        }

        #region IBatchCommand 成员

        public string Sql
        {
            get
            {
                return this.sql;
            }
            set
            {
                this.sql = value;
                if (string.IsNullOrEmpty(sql) == false)
                {
                    sql = sql.Replace(':', '@');
                }
            }
        }

        public void AddParameter(string name, DbType type)
        {
            SqlDbType otp = SqlDbType.VarChar;
            Type colType = typeof(string);
            if (type == DbType.Date || type == DbType.DateTime || type == DbType.Time)
            {
                otp = SqlDbType.DateTime;
                colType = typeof(DateTime);
            }
            else if (type == DbType.Decimal || type == DbType.Double || type == DbType.Int32 || type == DbType.Currency || type == DbType.VarNumeric)
            {
                otp = SqlDbType.Decimal;
                colType = typeof(Decimal);
            }
            else if (type == DbType.Binary)
            {
                otp = SqlDbType.Binary;
                colType = typeof(Byte[]);
            }
            else if (type == DbType.Single)
            {
                otp = SqlDbType.Float;
                colType = typeof(float);
            }
            else if (type == DbType.Int64)
            {
                otp = SqlDbType.BigInt;
                colType = typeof(long);
            }
            else if (type == DbType.Boolean)
            {
                otp = SqlDbType.Bit;
                colType = typeof(bool);
            }
            name = name.Replace(':', '@');
            if (name[0] != '@')
                name = '@' + name;
            SqlParameter p = new SqlParameter(name, otp);
            this.parameters.Add(p);
            string sColName = name.Substring(1);
            if (sColName.StartsWith("V_"))
            {
                sColName = sColName.Substring(2);
            }
            p.SourceColumn = sColName;
            commandTable.Columns.Add(sColName, colType);
        }

        public void AddData(params object[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = I3DBUtil.InsureDBParam(data[i]);
            }
            commandTable.Rows.Add(data);
            // this.parametersVals.Add(new ArrayList(data));
        }

        public int Execute(string connection)
        {
            //int count = this.parametersVals.Count;
            //if (count > 0)
            //{
            //    using (SqlConnection sqlCon = new SqlConnection(connection))
            //    {
            //        sqlCon.Open();
            //        //if (LogHelper.IsInfoEnable)
            //        //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(string)开始执行,Sql语句为:", sql));
            //        return this.Execute(sqlCon, this.sql);
            //    }
            //}
            //return 0;
            throw new OperationCanceledException("不能直接调用该方法");
        }

        public int Execute(SqlTransaction trans)
        {
            //int count = this.parametersVals.Count;
            //if (count > 0)
            //{
            //    //if (LogHelper.IsInfoEnable)
            //    //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(OracleTransaction)开始执行,Sql语句为:", sql));
            //    return this.Execute(trans.Connection, this.sql);
            //}
            //return 0;
            throw new OperationCanceledException("不能直接调用该方法");
        }

        int II3BatchCommand.Execute(IDbTransaction trans)
        {
            //SqlTransaction ot = trans as SqlTransaction;
            //if (ot == null)
            //{
            //    //if (LogHelper.IsInfoEnable)
            //    //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(IDbTransaction)执行失败,事务对象为null"));
            //    throw new Exception("trans is null or is not a instance of Oracle.DataAccess.Client.OracleTransaction");
            //}
            //return this.Execute(ot);
            throw new OperationCanceledException("不能直接调用该方法");
        }

        #endregion

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="sqlCon">数据连接</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响行数</returns>
        private int Execute(SqlConnection sqlCon, string sql)
        {
            throw new OperationCanceledException("不能直接调用该方法");
        }



        #region IBatchCommand 成员

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

        #endregion
    }
}
