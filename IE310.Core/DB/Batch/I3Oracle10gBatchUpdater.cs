using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Reflection;
using IE310.Core.Utils;

namespace IE310.Core.DB
{
    public class I3Oracle10gBatchUpdater : II3BatchUpdater
    {
        static Assembly assembly;
         static I3Oracle10gBatchUpdater()
        {
            assembly = Assembly.Load("Oracle.DataAccess.dll");
        }
       // private Type connectionType;
        public static Type ConnectionType
        {
            get
            {
                Type conType = assembly.GetType("Oracle.DataAccess.OracleConnection");
                return conType;
            }
        }
        private I3Oracle10gBatchCommand insertCommand = null;
        private I3Oracle10gBatchCommand updateCommand = null;
        private I3Oracle10gBatchCommand deleteCommand = null;
        private ArrayList insertIDList;

        public I3Oracle10gBatchUpdater()
        {
            this.insertIDList = new ArrayList();
        }

        /// <summary>
        /// 获取插入命令
        /// </summary>
        public II3BatchCommand InsertCommand
        {
            get
            {
                if (insertCommand == null)
                {
                    insertCommand = new I3Oracle10gBatchCommand();
                }
                return insertCommand;
            }
        }

        /// <summary>
        /// 获取更新命令
        /// </summary>
        public II3BatchCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new I3Oracle10gBatchCommand();
                }
                return updateCommand;
            }
        }

        /// <summary>
        /// 获取删除命令
        /// </summary>
        public II3BatchCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new I3Oracle10gBatchCommand();
                }
                return deleteCommand;
            }
        }

        private int ExecuteInsertCommand(IDbTransaction sqlTrans)
        {
            //插入命令的参数
            List<IDbDataParameter> ps = this.insertCommand.Parameters;
            //插入命令的参数值
            ArrayList pvs = this.insertCommand.ParametersVals;

            //获取ID的索引
            int iIdIndex = -1;
            bool bFindId = false;
            foreach (IDbDataParameter p in ps)
            {
                iIdIndex++;
                if (p.ParameterName.Equals(":V_ID"))
                {
                    bFindId = true;
                    break;
                }
            }
            //存在ID，则存在新增序列的可能
            if (bFindId)
            {
                foreach (ArrayList al in pvs)
                {
                    if (string.IsNullOrEmpty((string)al[iIdIndex]))
                    {  ///没有Guid,则对Guid赋值
                        al[iIdIndex] = Guid.NewGuid().ToString();
                    }
                    ///将所有的Guid值记录到列表中
                    this.insertIDList.Add(al[iIdIndex]);
                }
            }
            return this.insertCommand.Execute(sqlTrans);
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="connection">数据连接串</param>
        /// <returns></returns>
        public I3MsgInfo BatchExecute(string connection)
        {
            this.insertIDList.Clear();
            Type conType = assembly.GetType("Oracle.DataAccess.OracleConnection");
            IDbConnection sqlCon = Activator.CreateInstance(conType, connection) as IDbConnection;
            sqlCon.Open();
            using (IDbTransaction sqlTrans = sqlCon.BeginTransaction())
            {
                try
                {
                    I3MsgInfo msgInfo = BatchExecute(sqlTrans);
                    sqlTrans.Commit();
                    return msgInfo;
                }
                catch (Exception ex)
                {
                    //回滚数据库事务
                    sqlTrans.Rollback();
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute执行失败:", ex.Message));
                    return new I3MsgInfo(false, ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        public I3MsgInfo BatchExecute(IDbTransaction sqlTrans)
        {
                int iDel = 0, iUpdate = 0, iInsert = 0;
                //删除命令
                if (this.deleteCommand != null)
                {
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute开始执行删除"));
                    iDel = this.deleteCommand.Execute(sqlTrans);
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute完成执行删除,成功的条数为:", iDel.ToString()));
                }
                //更新命令
                if (this.updateCommand != null)
                {
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute开始执行更新"));
                    iUpdate = this.updateCommand.Execute(sqlTrans);
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute完成执行更新,成功的条数为:", iUpdate.ToString()));
                }
                //插入命令
                if (this.insertCommand != null)
                {
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute开始执行插入"));
                    iInsert = this.ExecuteInsertCommand(sqlTrans);
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute完成执行插入,成功的条数为:", iInsert.ToString()));
                }
                //if (LogHelper.IsInfoEnable)
                //    LogHelper.Info(string.Concat("BatchExecute开始提交事务"));
                //提交数据事务
                //sqlTrans.Commit();
                //if (LogHelper.IsInfoEnable)
                //    LogHelper.Info(string.Concat("BatchExecute完成提交事务"));
                return new I3MsgInfo(true, "删除:" + iDel + ";修改:" + iUpdate + ";插入" + iInsert);
            
        }
       ///// <summary>
        ///// 获取插入SQL中数据表名称
        ///// </summary>
        ///// <param name="sql">SQL</param>
        ///// <returns></returns>
        //private string GetInsertTableName(string sql)
        //{
        //    int iIntoIndex = sql.ToUpper().IndexOf(" INTO ");
        //    int iEndIndex = sql.IndexOf("(", iIntoIndex + 1);
        //    return sql.Substring(iIntoIndex + 6, iEndIndex - iIntoIndex - 6);
        //}

        #region IBatchUpdater 成员

        II3BatchCommand II3BatchUpdater.InsertCommand
        {
            get { return this.InsertCommand; }
        }

        II3BatchCommand II3BatchUpdater.UpdateCommand
        {
            get { return this.UpdateCommand; }
        }

        II3BatchCommand II3BatchUpdater.DeleteCommand
        {
            get { return this.DeleteCommand; }
        }

        I3MsgInfo II3BatchUpdater.BatchExecute(string connection)
        {
            return this.BatchExecute(connection);
        }

        string[] II3BatchUpdater.InsertIDList
        {
            get
            {
                return (string[])this.insertIDList.ToArray(typeof(string));
            }
        }

        #endregion

    }
}
