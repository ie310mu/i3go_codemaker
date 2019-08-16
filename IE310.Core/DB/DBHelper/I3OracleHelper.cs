
using System;
using System.Data.OracleClient;
using System.Data;
using System.Text;

namespace IE310.Core.DB
{
    public sealed class I3OracleHelper
    {
        private I3OracleHelper()
        {
        }

        private static void AssignParameterValues(IDbDataParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters != null) && (parameterValues != null))
            {
                if (commandParameters.Length != (parameterValues.Length + 1))
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
                int index = 0;
                int num2 = commandParameters.Length - 1;
                while (index < num2)
                {
                    commandParameters[index + 1].Value = (parameterValues[index]);
                    index++;
                }
            }
        }

        private static void AttachParameters(OracleCommand command, IDbDataParameter[] commandParameters)
        {
            foreach (IDbDataParameter parameter in commandParameters)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                {
                    parameter.Value = (DBNull.Value);
                }
                command.Parameters.Add(parameter);
            }
        }

        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            command.Parameters.Clear();
            return dataSet;
        }

        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            command.Parameters.Clear();
            return dataSet;
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, null);
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, null);
        }

        public static OracleDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, commandParameters, I3DbConnectionOwnership.External);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, I3DbConnectionOwnership.External);
        }

        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleDataReader reader;
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            try
            {
                reader = ExecuteReader(connection, null, commandType, commandText, commandParameters, I3DbConnectionOwnership.Internal);
            }
            catch
            {
                connection.Close();
                throw;
            }
            return reader;
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, I3DbConnectionOwnership connectionOwnership)
        {
            OracleDataReader reader;
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            if (connectionOwnership == I3DbConnectionOwnership.External)
            {
                reader = command.ExecuteReader();
            }
            else
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            command.Parameters.Clear();
            return reader;
        }

        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        public static object ExecuteScalar(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        public static object ExecuteScalar(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connectionString, commandType, commandText, null);
        }

        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3OracleHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            command.Connection = (connection);
            command.CommandText = (commandText);
            if (transaction != null)
            {
                command.Transaction = (transaction);
            }
            command.CommandType = (commandType);
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        /// <summary>
        /// 连续增加序列计数，返回最后一个值
        /// 快速处理序列的方法
        /// </summary>
        /// <param name="sSeqName">序列名称</param>
        /// <param name="iSeqCount">系列计数</param>
        /// <returns>返回最后一个值</returns>
        public static int BatchGetSequence(string sSeqName, int iSeqCount, string connection)
        {
            if (iSeqCount == 1)
            {
                try
                {
                    return I3DBUtil.ToInt(I3OracleHelper.ExecuteScalar(connection, CommandType.Text, "Select " + sSeqName + ".NextVal From Dual"));
                }
                catch
                {
                    I3OracleHelper.ExecuteScalar(connection, CommandType.Text, "create sequence " + sSeqName);
                    return I3DBUtil.ToInt(I3OracleHelper.ExecuteScalar(connection, CommandType.Text, "Select " + sSeqName + ".NextVal From Dual"));
                }
            }
            //通过存储过程增加序列值
            string sSql = "select BatchUpdateSequence(:W_SEQ_NAME,:W_SEQ_COUNT) from dual";
            OracleConnection sqlCon = I3DBUtil.CreateAndOpenDbConnection(connection) as OracleConnection;
            sqlCon.Open();
            try
            {
                OracleCommand sqlCom = new OracleCommand(sSql, sqlCon);
                sqlCom.Parameters.Add(":W_SEQ_NAME", OracleType.VarChar);
                sqlCom.Parameters.Add(":W_SEQ_COUNT", OracleType.Int32);
                sqlCom.Parameters[0].Value = sSeqName;
                sqlCom.Parameters[1].Value = iSeqCount;
                int iLastSeq = -1;
                try
                {
                    //选择序列值
                    iLastSeq = I3DBUtil.ToInt(sqlCom.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    //添加存储过程
                    StringBuilder sDDLSql = new StringBuilder();
                    sDDLSql.Append("create or replace function  BatchUpdateSequence(seqName in varchar2, iCount in integer) return integer is ").
                        Append("Result1 integer; ").
                        Append("iIndex integer; ").
                        Append("sSql varchar2(200); ").
                        Append("begin ").
                        Append("iIndex := 0; ").
                        Append("LOOP ").
                        Append("sSql := 'Select '||seqName||'.NextVal From Dual' ; ").
                        Append("execute immediate sSql ").
                        Append("	into Result1; ").
                        Append("iIndex := iIndex+1; ").
                        Append("EXIT When iIndex=iCount; ").
                        Append("End Loop; ").
                        Append("return (Result1); ").
                        Append("end BatchUpdateSequence; ");
                    OracleCommand sqlCom1 = new OracleCommand(sDDLSql.ToString(), sqlCon);
                    sqlCom1.ExecuteNonQuery();
                    iLastSeq = I3DBUtil.ToInt(sqlCom.ExecuteScalar());
                }
                return iLastSeq - iSeqCount + 1;
            }
            finally
            {
                sqlCon.Close();
            }
        }

    }
}

