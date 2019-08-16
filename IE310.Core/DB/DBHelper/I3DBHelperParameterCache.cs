using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections;
using System.Data.OracleClient;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;


namespace IE310.Core.DB
{
    public sealed class I3DBHelperParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new System.Collections.Hashtable());

        private I3DBHelperParameterCache()
        {

        }

        public static void CacheParameterset(string connectionString, string commandText, params IDbDataParameter[] commandParameters)
        {
            string str = connectionString + ":" + commandText;
            paramCache[str] = commandParameters;
        }

        private static IDbDataParameter[] CloneParameters(IDbDataParameter[] originalParameters)
        {
            IDbDataParameter[] parameterArray = new IDbDataParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (IDbDataParameter)originalParameters[index];//.co.Clone();
                index++;
            }
            return parameterArray;
        }

        private static IDbDataParameter[] DiscoverSpParameterset(string connectionString, string spName, bool includeReturnValueParameter)
        {
            DBServerType dbServerType = I3DBUtil.AnalysisConnectionString(ref connectionString);
            DbConnection connection = null;
            switch (dbServerType)
            {
                case DBServerType.SqlServer:
                    connection = new SqlConnection(connectionString);
                    break;
                case DBServerType.Oracle:
                    connection = new OracleConnection(connectionString);
                    break;
                default:
                    connection = new MySqlConnection(connectionString);
                    break;
            }

            IDbDataParameter[] parameterArray2;
            try
            {
                DbCommand command = null;
                switch (dbServerType)
                {
                    case DBServerType.SqlServer:
                        command = new SqlCommand(spName, connection as SqlConnection);
                        break;
                    case DBServerType.Oracle:
                        command = new OracleCommand(spName, connection as OracleConnection);
                        break;
                    default:
                        command = new MySqlCommand(spName, connection as MySqlConnection);
                        break;
                }

                try
                {
                    connection.Open();
                    command.CommandType = (CommandType.StoredProcedure);
                    switch (dbServerType)
                    {
                        case DBServerType.SqlServer:
                            SqlCommandBuilder.DeriveParameters(command as SqlCommand);
                            //if ((command as SqlCommand).Parameters[0].SqlDbType == SqlDbType.Cursor)
                            //{
                            //    (command as SqlCommand).Parameters[0].Direction = (ParameterDirection.Output);
                            //}
                            break;
                        case DBServerType.Oracle:
                            OracleCommandBuilder.DeriveParameters(command as OracleCommand);
                            if ((command as OracleCommand).Parameters[0].OracleType == OracleType.Cursor)
                            {
                                (command as OracleCommand).Parameters[0].Direction = (ParameterDirection.Output);
                            }
                            break;
                        default:
                            MySqlCommandBuilder.DeriveParameters(command as MySqlCommand);
                            //if ((command as MySqlCommand).Parameters[0].SqlDbType == MySqlDbType.Cursor)
                            //{
                            //    (command as MySqlCommand).Parameters[0].Direction = (ParameterDirection.Output);
                            //}
                            break;
                    }
                    IDbDataParameter[] parameterArray = new IDbDataParameter[command.Parameters.Count];
                    command.Parameters.CopyTo(parameterArray, 0);
                    parameterArray2 = parameterArray;
                }
                finally
                {
                    command.Dispose();
                }
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return parameterArray2;
        }

        public static IDbDataParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string str = connectionString + ":" + commandText;
            IDbDataParameter[] originalParameters = (IDbDataParameter[])paramCache[str];
            if (originalParameters == null)
            {
                return null;
            }
            return CloneParameters(originalParameters);
        }

        public static IDbDataParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        public static IDbDataParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string str = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            IDbDataParameter[] originalParameters = (IDbDataParameter[])paramCache[str];
            if (originalParameters == null)
            {
                object obj2;
                paramCache[str] = obj2 = DiscoverSpParameterset(connectionString, spName, includeReturnValueParameter);
                originalParameters = (IDbDataParameter[])obj2;
            }
            return CloneParameters(originalParameters);
        }
    }
}