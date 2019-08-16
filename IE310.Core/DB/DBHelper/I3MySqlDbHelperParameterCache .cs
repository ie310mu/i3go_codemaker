
using System;
using System.Collections;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace IE310.Core.DB
{
    public sealed class I3MySqlHelperParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        private I3MySqlHelperParameterCache()
        {
        }

        public static void CacheParameterset(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            string str = connectionString + ":" + commandText;
            paramCache[str] = commandParameters;
        }

        private static MySqlParameter[] CloneParameters(MySqlParameter[] originalParameters)
        {
            MySqlParameter[] parameterArray = new MySqlParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (MySqlParameter)originalParameters[index];//.Clone();
                index++;
            }
            return parameterArray;
        }

        private static MySqlParameter[] DiscoverSpParameterset(string connectionString, string spName, bool includeReturnValueParameter)
        {
            MySqlParameter[] parameterArray2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(spName, connection))
                {
                    connection.Open();
                    command.CommandType = (CommandType.StoredProcedure);
                    MySqlCommandBuilder.DeriveParameters(command);
                    //if (command.Parameters[0].DbType == DbType.cu)
                    //{
                    //    command.Parameters[0].Direction  = (ParameterDirection.Output);
                    //}
                    MySqlParameter[] parameterArray = new MySqlParameter[command.Parameters.Count];
                    command.Parameters.CopyTo(parameterArray, 0);
                    parameterArray2 = parameterArray;
                }
            }
            return parameterArray2;
        }

        public static MySqlParameter[] GetCachedParameterset(string connectionString, string commandText)
        {
            string str = connectionString + ":" + commandText;
            MySqlParameter[] originalParameters = (MySqlParameter[])paramCache[str];
            if (originalParameters == null)
            {
                return null;
            }
            return CloneParameters(originalParameters);
        }

        public static MySqlParameter[] GetSpParameterset(string connectionString, string spName)
        {
            return GetSpParameterset(connectionString, spName, false);
        }

        public static MySqlParameter[] GetSpParameterset(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string str = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            MySqlParameter[] originalParameters = (MySqlParameter[])paramCache[str];
            if (originalParameters == null)
            {
                object obj2;
                paramCache[str] = obj2 = DiscoverSpParameterset(connectionString, spName, includeReturnValueParameter);
                originalParameters = (MySqlParameter[])obj2;
            }
            return CloneParameters(originalParameters);
        }
    }
}

