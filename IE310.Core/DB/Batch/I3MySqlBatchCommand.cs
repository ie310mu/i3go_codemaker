using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

namespace IE310.Core.DB
{
    public class I3MySqlBatchCommand : II3BatchCommand
    {
        private List<MySqlParameter> parameters = new List<MySqlParameter>();
        private string sql;
        private DataTable commandTable = new DataTable();

        public DataTable CommandTable
        {
            get
            {
                return commandTable;
            }
        }
        private string tableName;
        private I3MySqlBatchUpdater batchUpdater = null;

        public I3MySqlBatchUpdater BatchUpdater
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
        public I3MySqlBatchCommand()
            : this("")
        {
            DataTable dnTable = new DataTable();
            DataRow newRow = dnTable.NewRow();
            dnTable.Rows.Add(newRow);
        }
        public I3MySqlBatchCommand(I3MySqlBatchUpdater updater)
        {
            batchUpdater = updater;
        }
        /// <summary>
        /// ����Oracle92����ִ�������һ��ʵ����
        /// </summary>
        /// <param name="sql">SQL���</param>
        public I3MySqlBatchCommand(string sql)
        {
            this.sql = sql;
            this.parameters = new List<MySqlParameter>();
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public List<MySqlParameter> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        /// <summary>
        /// ����ֵ����
        /// </summary>
        public ArrayList ParametersVals
        {
            get
            {
                return null;
            }// this.parametersVals; 
        }

        #region IBatchCommand ��Ա

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
            MySqlDbType otp = MySqlDbType.VarChar;
            Type colType = typeof(string);
            if (type == DbType.Date || type == DbType.DateTime || type == DbType.Time)
            {
                otp = MySqlDbType.DateTime;
                colType = typeof(DateTime);
            }
            else if (type == DbType.Decimal || type == DbType.Currency || type == DbType.VarNumeric)
            {
                otp = MySqlDbType.Decimal;
                colType = typeof(Decimal);
            }
            else if (type == DbType.Double )
            {
                otp = MySqlDbType.Double;
                colType = typeof(double);
            }
            else if (type == DbType.Int32)
            {
                otp = MySqlDbType.Int32;
                colType = typeof(int);
            }
            else if (type == DbType.Binary)
            {
                otp = MySqlDbType.Binary;
                colType = typeof(Byte[]);
            }
            else if (type == DbType.Single)
            {
                otp = MySqlDbType.Float;
                colType = typeof(float);
            }
            else if (type == DbType.Int64)
            {
                otp = MySqlDbType.Int64;
                colType = typeof(long);
            }
            else if (type == DbType.Boolean)
            {
                otp = MySqlDbType.Bit;
                colType = typeof(bool);
            }
            name = name.Replace(':', '@');
            if (name[0] != '@')
                name = '@' + name;
            MySqlParameter p = new MySqlParameter(name, otp);
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
            //        //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(string)��ʼִ��,Sql���Ϊ:", sql));
            //        return this.Execute(sqlCon, this.sql);
            //    }
            //}
            //return 0;
            throw new OperationCanceledException("����ֱ�ӵ��ø÷���");
        }

        public int Execute(MySqlTransaction trans)
        {
            //int count = this.parametersVals.Count;
            //if (count > 0)
            //{
            //    //if (LogHelper.IsInfoEnable)
            //    //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(OracleTransaction)��ʼִ��,Sql���Ϊ:", sql));
            //    return this.Execute(trans.Connection, this.sql);
            //}
            //return 0;
            throw new OperationCanceledException("����ֱ�ӵ��ø÷���");
        }

        int II3BatchCommand.Execute(IDbTransaction trans)
        {
            //SqlTransaction ot = trans as SqlTransaction;
            //if (ot == null)
            //{
            //    //if (LogHelper.IsInfoEnable)
            //    //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(IDbTransaction)ִ��ʧ��,�������Ϊnull"));
            //    throw new Exception("trans is null or is not a instance of Oracle.DataAccess.Client.OracleTransaction");
            //}
            //return this.Execute(ot);
            throw new OperationCanceledException("����ֱ�ӵ��ø÷���");
        }

        #endregion

        /// <summary>
        /// ִ�в���
        /// </summary>
        /// <param name="sqlCon">��������</param>
        /// <param name="sql">SQL���</param>
        /// <returns>Ӱ������</returns>
        private int Execute(MySqlConnection sqlCon, string sql)
        {
            throw new OperationCanceledException("����ֱ�ӵ��ø÷���");
        }



        #region IBatchCommand ��Ա

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
