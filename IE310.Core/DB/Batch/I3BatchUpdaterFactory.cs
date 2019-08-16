using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.DB
{
    /// <summary>
    /// ��������������
    /// </summary>
    public static class I3BatchUpdaterFactory
    {
        /// <summary>
        /// ��������������
        /// </summary>
        /// <returns></returns>
        public static II3BatchUpdater CreateBatchUpdater()
        {
            switch (I3DBUtil.DBServerType)
            {
                case DBServerType.SqlServer:
                    return new I3SqlServerBatchUpdater();
                case DBServerType.Oracle:
                    return new I3Oracle10gBatchUpdater();
                default:
                    return new I3MySqlBatchUpdater();
            }
        }
    }
}
