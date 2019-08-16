using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;

namespace IE310.Core.DB
{
    class I3Oracle10gBatchCommand : II3BatchCommand
    {
        private List<IDbDataParameter> parameters;
        private ArrayList parametersVals;
        private string sql;

        public I3Oracle10gBatchCommand()
            : this(null)
        {
        }

        /// <summary>
        /// ����Oracle92����ִ�������һ��ʵ����
        /// </summary>
        /// <param name="sql">SQL���</param>
        public I3Oracle10gBatchCommand(string sql)
        {
            this.sql = sql;
            this.parameters = new List<IDbDataParameter>();
            this.parametersVals = new ArrayList(50);
        }
        
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public List<IDbDataParameter> Parameters
        {
            get { return this.parameters; }
        }

        /// <summary>
        /// ����ֵ����
        /// </summary>
        public ArrayList ParametersVals
        {
            get { return this.parametersVals; }
        }

        #region IBatchCommand ��Ա

        public string Sql
        {
            get { return this.sql; }
            set { this.sql = value; }
        }

        public void AddParameter(string name, DbType type)
        {
            OracleType otp = OracleType.VarChar;
            if (type == DbType.Date || type == DbType.DateTime || type == DbType.Time)
            {
                otp = OracleType.DateTime;
            }
            else if (type == DbType.Decimal || type == DbType.Double || type == DbType.Int32 || type == DbType.Currency || type == DbType.VarNumeric)
            {
                otp = OracleType.Number;
            }
            else if (type == DbType.Binary)
            {
                otp = OracleType.Blob;
            }
            else if (type == DbType.Single)
            {
                otp = OracleType.Float;
            }
            else if (type == DbType.Int64)
            {
                otp = OracleType.Int32;
            }
            OracleParameter p = new OracleParameter(":" + name, otp);
            this.parameters.Add(p);
        }

        public void AddData(params object[] data)
        {
            this.parametersVals.Add(new ArrayList(data));
        }

        public int Execute(string connection)
        {
            int count = this.parametersVals.Count;
            if (count > 0)
            {
                IDbConnection sqlCon = Activator.CreateInstance(I3Oracle10gBatchUpdater.ConnectionType, connection) as IDbConnection;
                using (sqlCon)
                {
                    sqlCon.Open();
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(string)��ʼִ��,Sql���Ϊ:", sql));
                    return this.Execute(sqlCon,null,this.sql);
                }
            }
            return 0;
        }

        public int Execute(IDbTransaction trans)
        {
            int count = this.parametersVals.Count;
            if (count > 0)
            {
                //if (LogHelper.IsInfoEnable)
                //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(OracleTransaction)��ʼִ��,Sql���Ϊ:", sql));
                return this.Execute(trans.Connection,trans, this.sql);
            }
            return 0;
        }

        //int IBatchCommand.Execute(IDbTransaction trans)
        //{
        //    //OracleTransaction ot = trans as OracleTransaction;
        //    if (trans == null)
        //    {
        //        //if (LogHelper.IsInfoEnable)
        //        //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(IDbTransaction)ִ��ʧ��,�������Ϊnull"));
        //        throw new Exception("trans is null or is not a instance of Oracle.DataAccess.Client.OracleTransaction");
        //    }
        //    return this.Execute(trans);
        //}

        #endregion

        /// <summary>
        /// ִ�в���
        /// </summary>
        /// <param name="sqlCon">��������</param>
        /// <param name="sql">SQL���</param>
        /// <returns>Ӱ������</returns>
        private int Execute(IDbConnection sqlCon,IDbTransaction sqlTrans, string sql)
        {
            
            IDbCommand sqlCom = sqlCon.CreateCommand();// new OracleCommand(sql, sqlCon);
            sqlCom.Transaction = sqlTrans;
            sqlCom.CommandText = sql;
            sqlCom.GetType().GetProperty("ArrayBindCount").SetValue(sqlCom, this.parametersVals.Count, null);
            //sqlCom.ArrayBindCount = this.parametersVals.Count;
            //��������
            foreach (IDbDataParameter p in this.parameters)
            {
                IDbDataParameter newP = sqlCom.CreateParameter();
                newP.ParameterName =p.ParameterName;
                newP.Size = p.Size;
                newP.DbType = p.DbType;
                sqlCom.Parameters.Add(newP);
            }
            //�󶨲���ֵ
            BindParametersValue(sqlCom.Parameters);
            //if (LogHelper.IsInfoEnable)
            //    LogHelper.Info(string.Concat("Oracle10gBatchCommand.Execute(OracleConnection,string)��ʼִ��,Sql���Ϊ:", sql));
            //ִ������
            return sqlCom.ExecuteNonQuery();
        }

        /// <summary>
        /// �󶨲���ֵ
        /// </summary>
        /// <param name="parameters">��������</param>
        private void BindParametersValue(IDataParameterCollection parameters)
        {
            int count = this.parametersVals.Count;
            //��������
            for (int i = 0; i < parameters.Count; i++)
            {
                //����ֵ����
                object[] ary = new object[count];
                for (int j = 0; j < this.parametersVals.Count; j++)
                {
                    object obj = (this.parametersVals[j] as ArrayList)[i];
                    if (obj != null)
                    {
                        if ((obj is int && ((int)obj) == I3DBUtil.INVALID_ID)
                            || (obj is float && float.IsNaN((float)obj)) ||
                            (obj is decimal && (decimal)obj == I3DBUtil.INVALID_DECIMAL) ||
                            (obj is DateTime && ((DateTime)obj) == I3DBUtil.INVALID_DATE) ||
                            obj == DBNull.Value)
                        {
                            obj = null;
                        }
                    }
                    ary[j] = obj;
                }
                (parameters[i] as IDbDataParameter).Value = ary;
            }
            //�������
            this.parametersVals.Clear();
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
    }
}
