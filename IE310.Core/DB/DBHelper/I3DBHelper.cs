
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace IE310.Core.DB
{
    public static class I3DbHelper
    {
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

        private static void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
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

        public static DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(DBServerType type, string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(type, connectionString, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(DBServerType type, string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(type, connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(type, connectionString, CommandType.StoredProcedure, spName);
        }
        public static T[] ToDBParamArray<T>(params IDbDataParameter[] parms)
        {
            if (parms == null || parms.Length == 0)
                return null;
            Array array = Array.CreateInstance(typeof(T), parms.Length);
            if (typeof(T) == typeof(OracleParameter))
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    array.SetValue(Convert2OracleParameter(parms[i]), i);
                }
            }
            else if (typeof(T) == typeof(SqlParameter))
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    array.SetValue(Convert2SqlParameter(parms[i]), i);
                }
            }
            else
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    array.SetValue(Convert2MySqlParameter(parms[i]), i);
                }
            }
            return array as T[];

        }
        public static OracleParameter Convert2OracleParameter(IDbDataParameter parameter)
        {
            string sName = parameter.ParameterName;
            if (sName[0] == '@')
                sName = ":" + sName.Substring(1);
            else if (sName[0] != ':')
            {
                sName = ":" + sName;
            }
            OracleParameter newParam = new OracleParameter();
            newParam.ParameterName = sName;
            if (parameter is SqlParameter)
            {
                newParam.OracleType = I3DBUtil.Convert2OracleType((parameter as SqlParameter).SqlDbType);
            }
            else if (parameter is MySqlParameter)
            {
                newParam.OracleType = I3DBUtil.Convert2OracleType((parameter as MySqlParameter).MySqlDbType);
            }
            else if (parameter is OracleParameter)
            {
                newParam.DbType = parameter.DbType;
            }
            else
            {
                newParam.OracleType = I3DBUtil.Convert2OracleType(parameter.DbType);
            }
            newParam.Value = parameter.Value;
            newParam.Size = parameter.Size;
            return newParam;
        }
        public static SqlParameter Convert2SqlParameter(IDbDataParameter parameter)
        {
            string sName = parameter.ParameterName;
            if (sName[0] == ':')
                sName = "@" + sName.Substring(1);
            else if (sName[0] != '@')
            {
                sName = "@" + sName;
            }
            SqlParameter newParam = new SqlParameter();
            newParam.ParameterName = sName;
            if (parameter is OracleParameter)
            {
                newParam.SqlDbType = I3DBUtil.Convert2SQLType((parameter as OracleParameter).OracleType);
            }
            else if (parameter is MySqlParameter)
            {
                newParam.SqlDbType = I3DBUtil.Convert2SQLType((parameter as MySqlParameter).MySqlDbType);
            }
            else if (parameter is SqlParameter)
            {
                newParam.DbType = parameter.DbType;
            }
            else
            {
                newParam.SqlDbType = I3DBUtil.Convert2SQLType(parameter.DbType);
            }
            newParam.Value = parameter.Value;
            newParam.Size = parameter.Size;
            return newParam;
        }
        public static MySqlParameter Convert2MySqlParameter(IDbDataParameter parameter)
        {
            string sName = parameter.ParameterName;
            if (sName[0] == ':')
                sName = "@" + sName.Substring(1);
            else if (sName[0] != '@')
            {
                sName = "@" + sName;
            }
            MySqlParameter newParam = new MySqlParameter();
            newParam.ParameterName = sName;
            if (parameter is OracleParameter)
            {
                newParam.MySqlDbType = I3DBUtil.Convert2MySqlType((parameter as OracleParameter).OracleType);
            }
            else if (parameter is SqlParameter)
            {
                newParam.MySqlDbType = I3DBUtil.Convert2MySqlType((parameter as SqlParameter).SqlDbType);
            }
            else if (parameter is MySqlParameter)
            {
                newParam.DbType = parameter.DbType;
            }
            else
            {
                newParam.MySqlDbType = I3DBUtil.Convert2MySqlType(parameter.DbType);
            }
            newParam.Value = parameter.Value;
            newParam.Size = parameter.Size;
            return newParam;
        }

        public static DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            LogSql("ExecuteDataset", commandType, commandText, commandParameters);

            DataSet dataSet = null;
            if (connection is SqlConnection)
            {
                dataSet = I3SqlHelper.ExecuteDataset(connection as SqlConnection, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters));
            }
            else if (connection is OracleConnection)
            {
                dataSet = I3OracleHelper.ExecuteDataset(connection as OracleConnection, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters));
            }
            else
            {
                dataSet = I3MySqlHelper.ExecuteDataset(connection as MySqlConnection, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters));
            }

            LogSqlResult(dataSet == null || dataSet.Tables.Count == 0 ? "0" : dataSet.Tables[0].Rows.Count.ToString());
            return dataSet;
        }

        public static DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            LogSql("ExecuteDataset", commandType, commandText, commandParameters);

            DataSet dataSet = null;
            if (transaction is SqlTransaction)
            {
                dataSet = I3SqlHelper.ExecuteDataset(transaction as SqlTransaction, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters));
            }
            else if (transaction is OracleTransaction)
            {
                dataSet = I3OracleHelper.ExecuteDataset(transaction as OracleTransaction, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters));
            }
            else
            {
                dataSet = I3MySqlHelper.ExecuteDataset(transaction as MySqlTransaction, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters));
            }

            LogSqlResult(dataSet == null || dataSet.Tables.Count == 0 ? "0" : dataSet.Tables[0].Rows.Count.ToString());
            return dataSet;
        }

        public static DataSet ExecuteDataset(DBServerType type, string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (IDbConnection connection = I3DBUtil.CreateConnection(type, connectionString))
            {
                connection.Open();
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(DBServerType type, string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(type, connectionString, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(DBServerType type, string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(type, connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(type, connectionString, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            LogSql("ExecuteNonQuery", commandType, commandText, commandParameters);

            int result = I3DBUtil.INVALID_ID;
            if (connection is SqlConnection)
            {
                result = I3SqlHelper.ExecuteNonQuery(connection as SqlConnection, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters));
            }
            else if (connection is OracleConnection)
            {
                result = I3OracleHelper.ExecuteNonQuery(connection as OracleConnection, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters));
            }
            else
            {
                result = I3MySqlHelper.ExecuteNonQuery(connection as MySqlConnection, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters));
            }

            LogSqlResult(result.ToString());
            return result;
        }

        public static int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            LogSql("ExecuteNonQuery", commandType, commandText, commandParameters);

            int result = I3DBUtil.INVALID_ID;
            if (transaction is OracleTransaction)
            {
                result = I3SqlHelper.ExecuteNonQuery(transaction as SqlTransaction, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters));
            }
            else if (transaction is OracleTransaction)
            {
                result = I3OracleHelper.ExecuteNonQuery(transaction as OracleTransaction, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters));
            }
            else
            {
                result = I3MySqlHelper.ExecuteNonQuery(transaction as MySqlTransaction, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters));
            }

            LogSqlResult(result.ToString());
            return result;
        }

        public static int ExecuteNonQuery(DBServerType type, string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (IDbConnection connection = I3DBUtil.CreateConnection(type, connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static DbDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, null);
        }

        public static DbDataReader ExecuteReader(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public static DbDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        public static DbDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static DbDataReader ExecuteReader(DBServerType type, string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(type, connectionString, commandType, commandText, null);
        }

        public static DbDataReader ExecuteReader(DBServerType type, string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(type, connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(type, connectionString, CommandType.StoredProcedure, spName);
        }

        public static DbDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, commandParameters, I3DbConnectionOwnership.External);
        }

        public static DbDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, I3DbConnectionOwnership.External);
        }

        public static DbDataReader ExecuteReader(DBServerType type, string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            DbDataReader reader;
            IDbConnection connection = I3DBUtil.CreateConnection(type, connectionString);
            connection.Open();
            try
            {
                reader = ExecuteReader(connection, null, commandType, commandText, commandParameters, I3DbConnectionOwnership.Internal);
            }
            catch (Exception ex)
            {
                connection.Close();
                throw;
            }
            return reader;
        }

        private static DbDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, I3DbConnectionOwnership connectionOwnership)
        {
            LogSql("ExecuteReader", commandType, commandText, commandParameters);

            DbDataReader reader = null;
            if (connection is SqlConnection)
            {
                reader = I3SqlHelper.ExecuteReader(connection as SqlConnection, transaction as SqlTransaction, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters), connectionOwnership);
            }
            else if (connection is OracleConnection)
            {
                reader = I3OracleHelper.ExecuteReader(connection as OracleConnection, transaction as OracleTransaction, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters), connectionOwnership);
            }
            else
            {
                reader = I3MySqlHelper.ExecuteReader(connection as MySqlConnection, transaction as MySqlTransaction, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters), connectionOwnership);
            }

            LogSqlResult(reader.HasRows.ToString());
            return reader;
        }

        public static object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        public static object ExecuteScalar(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        public static object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(DBServerType type, string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar(type, connectionString, commandType, commandText, null);
        }

        public static object ExecuteScalar(DBServerType type, string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDbDataParameter[] spParameterset = I3DBHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(type, connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(type, connectionString, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            LogSql("ExecuteScalar", commandType, commandText, commandParameters);

            object result = null;
            if (connection is SqlConnection)
            {
                result = I3SqlHelper.ExecuteScalar(connection as SqlConnection, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters));
            }
            else if (connection is OracleConnection)
            {
                result = I3OracleHelper.ExecuteScalar(connection as OracleConnection, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters));
            }
            else
            {
                result = I3MySqlHelper.ExecuteScalar(connection as MySqlConnection, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters));
            }

            LogSqlResult(result == null ? "null" : result.ToString());
            return result;
        }

        public static object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            LogSql("ExecuteScalar", commandType, commandText, commandParameters);

            object result = null;
            if (transaction is SqlTransaction)
            {
                result = I3SqlHelper.ExecuteScalar(transaction as SqlTransaction, commandType, commandText, ToDBParamArray<SqlParameter>(commandParameters));
            }
            else if (transaction is OracleTransaction)
            {
                result = I3OracleHelper.ExecuteScalar(transaction as OracleTransaction, commandType, commandText, ToDBParamArray<OracleParameter>(commandParameters));
            }
            else
            {
                result = I3MySqlHelper.ExecuteScalar(transaction as MySqlTransaction, commandType, commandText, ToDBParamArray<MySqlParameter>(commandParameters));
            }

            LogSqlResult(result == null ? "null" : result.ToString());
            return result;
        }

        public static object ExecuteScalar(DBServerType type, string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (IDbConnection connection = I3DBUtil.CreateConnection(type, connectionString))
            {
                connection.Open();
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        private static void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
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

        public static event LogSqlEvent LogSqlEvent;

        /// <summary>
        /// 在日志中记录Sql执行结果
        /// </summary>
        /// <param name="result"></param>
        private static void LogSqlResult(string result)
        {
            if (LogSqlEvent == null)
            {
                return;
            }
              
            try
            {
                LogSqlEvent("Result：" + result);
            }
            catch
            {
            }
        }

        private static void LogSql(string type, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (LogSqlEvent == null)
            {
                return;
            }

            try
            {
                string sql = string.Format("{0}： commandType---{1}，commandText---{2}", type, commandType, commandText);
                LogSqlEvent(sql);

                if (commandParameters != null && commandParameters.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (IDbDataParameter par in commandParameters)
                    {
                        sb.Append(string.Format("{0}({1})---{2}", par.ParameterName, par.DbType, par.Value));
                        sb.Append(";");
                    }
                    LogSqlEvent("pars:" + sb.ToString());
                }
            }
            catch
            {
            }
        }
    }

    public delegate void LogSqlEvent(string info);
}

