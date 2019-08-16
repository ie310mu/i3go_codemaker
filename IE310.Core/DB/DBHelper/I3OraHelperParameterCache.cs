
using System;
using System.Collections;
using System.Data.OracleClient;
using System.Data;

namespace IE310.Core.DB
{
    public sealed class I3OracleHelperParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        private I3OracleHelperParameterCache()
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
            IDbDataParameter[] parameterArray2;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(spName, connection))
                {
                    connection.Open();
                    command.CommandType = (CommandType.StoredProcedure);
                    OracleCommandBuilder.DeriveParameters(command);
                    if (command.Parameters[0].OracleType == OracleType.Cursor)
                    {
                        command.Parameters[0].Direction = (ParameterDirection.Output);
                    }
                    IDbDataParameter[] parameterArray = new IDbDataParameter[command.Parameters.Count];
                    command.Parameters.CopyTo(parameterArray, 0);
                    parameterArray2 = parameterArray;
                }
            }
            return parameterArray2;
        }

        public static IDbDataParameter[] GetCachedParameterset(string connectionString, string commandText)
        {
            string str = connectionString + ":" + commandText;
            IDbDataParameter[] originalParameters = (IDbDataParameter[])paramCache[str];
            if (originalParameters == null)
            {
                return null;
            }
            return CloneParameters(originalParameters);
        }

        public static IDbDataParameter[] GetSpParameterset(string connectionString, string spName)
        {
            return GetSpParameterset(connectionString, spName, false);
        }

        public static IDbDataParameter[] GetSpParameterset(string connectionString, string spName, bool includeReturnValueParameter)
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

