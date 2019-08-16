using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using IE310.Core.Utils;
using MySql.Data.MySqlClient;

namespace IE310.Core.DB
{
    public class I3MySqlBatchUpdater : II3BatchUpdater
    {
        #region IBatchUpdater ��Ա

        private I3MySqlBatchCommand insertCommand = null;
        private I3MySqlBatchCommand updateCommand = null;
        private I3MySqlBatchCommand deleteCommand = null;
        private ArrayList insertIDList;

        public I3MySqlBatchUpdater()
        {
            this.insertIDList = new ArrayList();
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public II3BatchCommand InsertCommand
        {
            get
            {
                if (insertCommand == null)
                {
                    insertCommand = new I3MySqlBatchCommand(this);
                }
                return insertCommand;
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public II3BatchCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new I3MySqlBatchCommand(this);
                }
                return updateCommand;
            }
        }

        /// <summary>
        /// ��ȡɾ������
        /// </summary>
        public II3BatchCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new I3MySqlBatchCommand(this);
                }
                return deleteCommand;
            }
        }

        public I3MsgInfo BatchExecute(string connection)
        {
            using (MySqlConnection con = I3DBUtil.CreateAndOpenDbConnection(connection) as MySqlConnection)
            {
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    I3MsgInfo msgInfo = BatchExecute(trans);
                    if (msgInfo.State)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
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
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.UpdateBatchSize = 100;
                newIds = new List<string>();
                if (insertCommand != null)
                {
                    I3MySqlBatchCommand insert = insertCommand as I3MySqlBatchCommand;
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
                            throw new Exception("IDֻ��Ϊstring����");
                        }
                    }
                    adapter.InsertCommand = sqlCon.CreateCommand() as MySqlCommand;
                    adapter.InsertCommand.CommandText = insertCommand.Sql;
                    adapter.InsertCommand.Transaction = sqlTrans as MySqlTransaction;// new SqlCommand(insert.Sql, sqlCon as SqlConnection, sqlTrans as SqlTransaction);
                    adapter.InsertCommand.Parameters.AddRange(insert.Parameters.ToArray());
                    adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.AcceptChangesDuringUpdate = true;
                    adapter.Update(insertTbl);
                }
                if (updateCommand != null)
                {
                    I3MySqlBatchCommand update = updateCommand as I3MySqlBatchCommand;
                    DataTable updateTbl = update.CommandTable;
                    updateTbl.TableName = "ForUpdate";
                    // result.Tables.Add(updateTbl);
                    updateTbl.AcceptChanges();
                    foreach (DataRow curRow in updateTbl.Rows)
                    {
                        curRow.BeginEdit();
                        curRow.EndEdit();
                    }
                    adapter.UpdateCommand = sqlCon.CreateCommand() as MySqlCommand;
                    adapter.UpdateCommand.CommandText = updateCommand.Sql;
                    adapter.UpdateCommand.Transaction = sqlTrans as MySqlTransaction;//new SqlCommand(update.Sql, sqlCon, sqlTrans);
                    adapter.UpdateCommand.Parameters.AddRange(update.Parameters.ToArray());
                    adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.AcceptChangesDuringUpdate = true;
                    adapter.Update(updateTbl);
                }
                if (deleteCommand != null)
                {
                    I3MySqlBatchCommand delete = deleteCommand as I3MySqlBatchCommand;

                    DataTable deleteTbl = delete.CommandTable;
                    deleteTbl.TableName = "ForDelete";
                    //  result.Tables.Add(deleteTbl);
                    deleteTbl.AcceptChanges();
                    foreach (DataRow curRow in deleteTbl.Rows)
                    {
                        curRow.Delete();
                    }
                    adapter.DeleteCommand = sqlCon.CreateCommand() as MySqlCommand;
                    adapter.DeleteCommand.CommandText = deleteCommand.Sql;
                    adapter.DeleteCommand.Transaction = sqlTrans as MySqlTransaction;//new SqlCommand(delete.Sql, sqlCon, sqlTrans);
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
