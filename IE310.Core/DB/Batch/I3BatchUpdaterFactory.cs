using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.DB
{
    /// <summary>
    /// 批量更新器工厂
    /// </summary>
    public static class I3BatchUpdaterFactory
    {
        /// <summary>
        /// 创建批量更新器
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
