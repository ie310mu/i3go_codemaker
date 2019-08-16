using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IE310.Core.Utils;

namespace IE310.Core.DB
{
    /// <summary>
    /// 批量命令接口
    /// </summary>
    public interface II3BatchCommand
    {
        string TableName { get;set;}
        /// <summary>
        /// 获取或设置SQL
        /// </summary>
        string Sql { get; set; }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">大小</param>
        void AddParameter(string name, DbType type);
        /// <summary>
        /// 加入参数数据
        /// </summary>
        /// <param name="data">数据</param>
        void AddData(params object[] data);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="sql">SQL</param>
        /// <returns>数据量</returns>
        int Execute(string connection);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="sql">SQL</param>
        /// <returns>数据量</returns>
        int Execute(IDbTransaction trans);
    }
    /// <summary>
    /// 批量更新接口
    /// </summary>
    public interface II3BatchUpdater
    {
        /// <summary>
        /// 获取插入命令
        /// </summary>
        II3BatchCommand InsertCommand { get; }
        /// <summary>
        /// 获取更新命令
        /// </summary>
        II3BatchCommand UpdateCommand { get; }
        /// <summary>
        /// 获取删除命令
        /// </summary>
        II3BatchCommand DeleteCommand { get; }
        /// <summary>
        /// 批量执行
        /// </summary>
        /// <returns>执行结果</returns>
        I3MsgInfo BatchExecute(string connection);

        /// <summary>
        /// 批量执行
        /// </summary>
        /// <returns>执行结果</returns>
        I3MsgInfo BatchExecute(IDbTransaction sqlTrans);
        /// <summary>
        /// 获取刚刚插入的ID值列表
        /// </summary>
        string[] InsertIDList { get; }

    }
}
