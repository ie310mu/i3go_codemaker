
using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace IE310.Core.DB
{
    public sealed class I3MySqlHelper
    {
        private I3MySqlHelper()
        {
        }

        private static void AssignParameterValues(MySqlParameter[] commandParameters, object[] parameterValues)
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

        private static void AttachParameters(MySqlCommand command, MySqlParameter[] commandParameters)
        {
            foreach (MySqlParameter parameter in commandParameters)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                {
                    parameter.Value = (DBNull.Value);
                }
                command.Parameters.Add(parameter);
            }
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(MySqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
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
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand command = new MySqlCommand();
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            command.Parameters.Clear();
            return dataSet;
        }

        public static DataSet ExecuteDataset(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand command = new MySqlCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            command.Parameters.Clear();
            return dataSet;
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static int ExecuteNonQuery(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(MySqlConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(MySqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
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
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand command = new MySqlCommand();
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand command = new MySqlCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, null);
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public static MySqlDataReader ExecuteReader(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        public static MySqlDataReader ExecuteReader(MySqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, null);
        }

        public static MySqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, commandParameters, I3DbConnectionOwnership.External);
        }

        public static MySqlDataReader ExecuteReader(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, I3DbConnectionOwnership.External);
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlDataReader reader;
            MySqlConnection connection = new MySqlConnection(connectionString);
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

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, CommandType commandType, string commandText, MySqlParameter[] commandParameters, I3DbConnectionOwnership connectionOwnership)
        {
            MySqlDataReader reader;
            MySqlCommand command = new MySqlCommand();
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

        public static object ExecuteScalar(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        public static object ExecuteScalar(MySqlConnection connection, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connection.ConnectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        public static object ExecuteScalar(MySqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(transaction.Connection.ConnectionString, spName);
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
                MySqlParameter[] spParameterset = I3MySqlHelperParameterCache.GetSpParameterset(connectionString, spName);
                AssignParameterValues(spParameterset, parameterValues);
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterset);
            }
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand command = new MySqlCommand();
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand command = new MySqlCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        private static void PrepareCommand(MySqlCommand command, MySqlConnection connection, MySqlTransaction transaction, CommandType commandType, string commandText, MySqlParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            command.Connection = (connection);
            StringBuilder strBuf = new StringBuilder(commandText);//.ToUpper());
            commandText = strBuf.Replace(':', '@').Replace("||", "+").Replace("SUBSTR(", "dbo.SUBSTR(").Replace("substr(", "dbo.SUBSTR(").Replace("Substr(", "dbo.SUBSTR(").Replace("length(", "len(").Replace("Length(", "len(").Replace("LENGTH(", "len(").Replace("NVL(", "dbo.NVL(").Replace("Nvl(", "dbo.NVL(").ToString();//.Replace("SUBSTR(", "SUBSTRING(").ToString();
            int iIndex = commandText.IndexOf("DECODE", StringComparison.OrdinalIgnoreCase);
            while (iIndex > 0)
            {
                StringBuilder newBuf = new StringBuilder();
                int iLastIndex = commandText.IndexOf(")", iIndex);
                string sInDecode = commandText.Substring(iIndex + 7, iLastIndex - iIndex - 7);
                string[] sSplit = sInDecode.Split(',');
                newBuf.Append(" (Case ").Append(sSplit[0]);
                for (int i = 1; i < sSplit.Length - 1; i += 2)
                {
                    newBuf.Append(" When ").Append(sSplit[i]).Append(" Then ").Append(sSplit[i + 1]);
                }
                newBuf.Append(" Else ").Append(sSplit[sSplit.Length - 1]).Append(" End) ");
                strBuf.Replace(commandText.Substring(iIndex, iLastIndex - iIndex + 1), newBuf.ToString());
                commandText = strBuf.ToString();
                iIndex = commandText.IndexOf("DECODE", StringComparison.OrdinalIgnoreCase);
            }
            command.CommandText = commandText;
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
    }
}

