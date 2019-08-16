using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using IE310.Core.Utils;

namespace IE310.Core.DB
{
    public class I3SqlServerBatchUpdater : II3BatchUpdater
    {
        #region IBatchUpdater 成员

        private I3SqlServerBatchCommand insertCommand = null;
        private I3SqlServerBatchCommand updateCommand = null;
        private I3SqlServerBatchCommand deleteCommand = null;
        private ArrayList insertIDList;

        public I3SqlServerBatchUpdater()
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
                    insertCommand = new I3SqlServerBatchCommand(this);
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
                    updateCommand = new I3SqlServerBatchCommand(this);
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
                    deleteCommand = new I3SqlServerBatchCommand(this);
                }
                return deleteCommand;
            }
        }

        public I3MsgInfo BatchExecute(string connection)
        {
            using (SqlConnection sqlCon =I3DBUtil.CreateAndOpenDbConnection(connection) as SqlConnection)
            {
                using (SqlTransaction sqlTrans = sqlCon.BeginTransaction())
                {
                    I3MsgInfo msgInfo = BatchExecute(sqlTrans);
                    if (msgInfo.State)
                    {
                        sqlTrans.Commit();
                    }
                    else
                    {
                        sqlTrans.Rollback();
                    }
                    return msgInfo;
                }
            }
        }

        public I3MsgInfo BatchExecute(IDbTransaction sqlTrans)
        {
            try
            {
                IDbConnection sqlCon = sqlTrans.Connection;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateBatchSize = 100;
                newIds = new List<string>();
                if (insertCommand != null)
                {
                    I3SqlServerBatchCommand insert = insertCommand as I3SqlServerBatchCommand;
                    DataTable insertTbl = insert.CommandTable;
                    insertTbl.TableName = insertCommand.TableName;
                    adapter.TableMappings.Add(insertTbl.TableName, insertTbl.TableName);
                    // result.Tables.Add(insertTbl);
                    if (insertTbl.Columns.Contains("ID"))
                    {
                        if (insertTbl.Columns["ID"].DataType == typeof(string))
                        {
                            foreach (DataRow curRow in insertTbl.Rows)
                            {
                                if (string.IsNullOrEmpty(curRow["ID"].ToString()))
                                {
                                    string sNewId = Guid.NewGuid().ToString();
                                    curRow["ID"] = sNewId;
                                    newIds.Add(sNewId);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("ID只能为string类型");
                        }
                    }
                    adapter.InsertCommand = sqlCon.CreateCommand() as SqlCommand;
                    adapter.InsertCommand.CommandText = insertCommand.Sql;
                    adapter.InsertCommand.Transaction = sqlTrans as SqlTransaction;// new SqlCommand(insert.Sql, sqlCon as SqlConnection, sqlTrans as SqlTransaction);
                    adapter.InsertCommand.Parameters.AddRange(insert.Parameters.ToArray());
                    adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.AcceptChangesDuringUpdate = true;
                    adapter.Update(insertTbl);
                }
                if (updateCommand != null)
                {
                    I3SqlServerBatchCommand update = updateCommand as I3SqlServerBatchCommand;
                    DataTable updateTbl = update.CommandTable;
                    updateTbl.TableName = "ForUpdate";
                    // result.Tables.Add(updateTbl);
                    updateTbl.AcceptChanges();
                    foreach (DataRow curRow in updateTbl.Rows)
                    {
                        curRow.BeginEdit();
                        curRow.EndEdit();
                    }
                    adapter.UpdateCommand = sqlCon.CreateCommand() as SqlCommand;
                    adapter.UpdateCommand.CommandText = updateCommand.Sql;
                    adapter.UpdateCommand.Transaction = sqlTrans as SqlTransaction;//new SqlCommand(update.Sql, sqlCon, sqlTrans);
                    adapter.UpdateCommand.Parameters.AddRange(update.Parameters.ToArray());
                    adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.AcceptChangesDuringUpdate = true;
                    adapter.Update(updateTbl);
                }
                if (deleteCommand != null)
                {
                    I3SqlServerBatchCommand delete = deleteCommand as I3SqlServerBatchCommand;

                    DataTable deleteTbl = delete.CommandTable;
                    deleteTbl.TableName = "ForDelete";
                    //  result.Tables.Add(deleteTbl);
                    deleteTbl.AcceptChanges();
                    foreach (DataRow curRow in deleteTbl.Rows)
                    {
                        curRow.Delete();
                    }
                    adapter.DeleteCommand = sqlCon.CreateCommand() as SqlCommand;
                    adapter.DeleteCommand.CommandText = deleteCommand.Sql;
                    adapter.DeleteCommand.Transaction = sqlTrans as SqlTransaction;//new SqlCommand(delete.Sql, sqlCon, sqlTrans);
                    adapter.DeleteCommand.Parameters.AddRange(delete.Parameters.ToArray());
                    adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.AcceptChangesDuringUpdate = true;
                    adapter.Update(deleteTbl);
                }

                return I3MsgInfo.Default;
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
        }
        List<string> newIds = new List<string>();
        public string[] InsertIDList
        {
            get { return newIds.ToArray(); }
        }

        #endregion
    }
}
