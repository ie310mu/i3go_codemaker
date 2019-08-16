
using System;
using System.Collections;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;

namespace IE310.Core.DB
{
    public sealed class I3SqlHelperParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        private I3SqlHelperParameterCache()
        {
        }

        public static void CacheParameterset(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            string str = connectionString + ":" + commandText;
            paramCache[str] = commandParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] parameterArray = new SqlParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (SqlParameter)originalParameters[index];//.Clone();
                index++;
            }
            return parameterArray;
        }

        private static SqlParameter[] DiscoverSpParameterset(string connectionString, string spName, bool includeReturnValueParameter)
        {
            SqlParameter[] parameterArray2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(spName, connection))
                {
                    connection.Open();
                    command.CommandType = (CommandType.StoredProcedure);
                    SqlCommandBuilder.DeriveParameters(command);
                    //if (command.Parameters[0].DbType == DbType.cu)
                    //{
                    //    command.Parameters[0].Direction  = (ParameterDirection.Output);
                    //}
                    SqlParameter[] parameterArray = new SqlParameter[command.Parameters.Count];
                    command.Parameters.CopyTo(parameterArray, 0);
                    parameterArray2 = parameterArray;
                }
            }
            return parameterArray2;
        }

        public static SqlParameter[] GetCachedParameterset(string connectionString, string commandText)
        {
            string str = connectionString + ":" + commandText;
            SqlParameter[] originalParameters = (SqlParameter[])paramCache[str];
            if (originalParameters == null)
            {
                return null;
            }
            return CloneParameters(originalParameters);
        }

        public static SqlParameter[] GetSpParameterset(string connectionString, string spName)
        {
            return GetSpParameterset(connectionString, spName, false);
        }

        public static SqlParameter[] GetSpParameterset(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string str = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            SqlParameter[] originalParameters = (SqlParameter[])paramCache[str];
            if (originalParameters == null)
            {
                object obj2;
                paramCache[str] = obj2 = DiscoverSpParameterset(connectionString, spName, includeReturnValueParameter);
                originalParameters = (SqlParameter[])obj2;
            }
            return CloneParameters(originalParameters);
        }
    }
}

