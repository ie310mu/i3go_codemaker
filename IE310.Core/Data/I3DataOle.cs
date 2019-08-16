using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Collections;
using System.Data;
using System.Data.Common;
using IE310.Core.Utils;

namespace IE310.Core.Data
{

    class I3DataOleAda
    {
        private OleDbDataAdapter adapter = null;
        public OleDbDataAdapter Adapter
        {
            get
            {
                return adapter;
            }
        }
        private OleDbCommandBuilder commandBuilder = null;
        public OleDbCommandBuilder CommandBuilder
        {
            get
            {
                return commandBuilder;
            }
            set
            {
                commandBuilder = value;
            }
        }

        public I3DataOleAda(OleDbConnection con, string sqlStr, I3Command command)
        {
            adapter = new OleDbDataAdapter(sqlStr, con);
            //_com = new OleDbCommandBuilder(_ada);
            if (command == null)
            {
                commandBuilder = new OleDbCommandBuilder(adapter);
            }
            else
            {
                adapter.UpdateCommand = command == null || command.UpdateCommand == null ? null : (OleDbCommand)command.UpdateCommand;
                adapter.DeleteCommand = command == null || command.DeleteCommand == null ? null : (OleDbCommand)command.DeleteCommand;
                adapter.InsertCommand = command == null || command.InsertCommand == null ? null : (OleDbCommand)command.InsertCommand;
                if (adapter.UpdateCommand != null)
                {
                    adapter.UpdateCommand.Connection = con;
                }
                if (adapter.DeleteCommand != null)
                {
                    adapter.DeleteCommand.Connection = con;
                }
                if (adapter.InsertCommand != null)
                {
                    adapter.InsertCommand.Connection = con;
                }
            }
        }

        public OleDbCommand SelectCommand
        {
            get
            {
                return adapter.SelectCommand;
            }
        }

        public OleDbTransaction Transaction
        {
            set
            {
                adapter.SelectCommand.Transaction = value;

                if (commandBuilder != null)
                {
                    DbCommand command = commandBuilder.GetUpdateCommand();
                    command.Transaction = value;
                    //_com.GetUpdateCommand().Transaction = value;
                    commandBuilder.GetInsertCommand().Transaction = value;
                    commandBuilder.GetDeleteCommand().Transaction = value;
                }
                else
                {
                    if (adapter.UpdateCommand != null)
                    {
                        adapter.UpdateCommand.Transaction = value;
                    }
                    if (adapter.InsertCommand != null)
                    {
                        adapter.InsertCommand.Transaction = value;
                    }
                    if (adapter.DeleteCommand != null)
                    {
                        adapter.DeleteCommand.Transaction = value;
                    }
                }
            }
        }
    }

    class I3DataOle : I3Data
    {
        private OleDbConnection connection;
        private Hashtable _tableList = new Hashtable();

        public override DbConnection GetConnection()
        {
            return connection;
        }

        /// <summary>
        /// IEFS_DataOle构造函数
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="ShowMessage"></param>
        public I3DataOle(string conStr)
        {
            try
            {
                connection = new OleDbConnection(conStr);
                connection.Open();
                active = true;
            }
            catch (Exception ex)
            {
                this.lastErrorInfo = "数据库连接创建失败\r\n" + ex.Message;
                //throw new Exception("数据库连接创建失败", ex);
            }
        }


        public override I3MsgInfo Execute(string sqlText, DbParameter[] paramList, DbTransaction aTran)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "连接已断开，无法执行sql语句");
            }

                OleDbCommand com = new OleDbCommand(sqlText, connection);
                com.Parameters.Clear();
                if (paramList != null)
                {
                    for (int i = 0; i < paramList.Length; i++)
                    {
                        com.Parameters.Add(paramList[i]);
                    }
                }

                if (aTran == null)
                {
                    OleDbTransaction ot = connection.BeginTransaction();
                    try
                    {
                        com.Transaction = ot;
                        com.ExecuteNonQuery();
                        ot.Commit();
                        com.Parameters.Clear();
                        return I3MsgInfo.Default;
                    }
                    catch (Exception ex)
                    {
                        ot.Rollback();
                        return new I3MsgInfo(false, ex.Message, ex);
                    }
                    finally
                    {
                        ot.Dispose();
                        com.Dispose();
                    }
                }
                else
                {
                    com.Transaction = (OleDbTransaction)aTran;
                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                    return I3MsgInfo.Default;
                }
        }

        public override I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbParameter[] paramList, DbTransaction aTran, I3Command command)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "连接已断开，无法填充DataTable");
            }
            

            if (dataTable.TableName == "")
            {
                dataTable.TableName = Guid.NewGuid().ToString().ToUpper();
            }

            try
            {
                I3DataOleAda tmp = null;
                bool have = _tableList.Contains(dataTable);

                if (have)
                {
                    tmp = (I3DataOleAda)_tableList[dataTable];
                    if (clear)
                    {
                        dataTable.Clear();
                    }
                    tmp.SelectCommand.CommandText = sqlText;
                    tmp.SelectCommand.Parameters.Clear();
                    if (paramList != null)
                    {
                        for (int i = 0; i < paramList.Length; i++)
                        {
                            tmp.SelectCommand.Parameters.Add(paramList[i]);
                        }
                    }
                    tmp.Adapter.Fill(dataTable);
                    //tmp.SelectCommand.Parameters.Clear();
                }
                else
                {
                    tmp = new I3DataOleAda(connection, sqlText, command);
                    tmp.SelectCommand.Parameters.Clear();
                    if (paramList != null)
                    {
                        for (int i = 0; i < paramList.Length; i++)
                        {
                            tmp.SelectCommand.Parameters.Add(paramList[i]);
                        }
                    }
                    _tableList.Add(dataTable, tmp);

                    if (aTran != null)
                    {
                        tmp.SelectCommand.Transaction = (OleDbTransaction)aTran;
                    }
                    else
                    {
                        tmp.SelectCommand.Transaction = null;
                    }
                    tmp.Adapter.Fill(dataTable);
                    //tmp.SelectCommand.Parameters.Clear();
                }
                return I3MsgInfo.Default;
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
        }

        public override I3MsgInfo UpdataTable(DataTable dataTable, DbTransaction aTran)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "连接已断开，无法更新DataTable");
            }

            bool have = _tableList.Contains(dataTable);
            if (!have)
            {
                return new I3MsgInfo(false, "此表未被ieData.SqlData填充过,无法将其更新到数据源");
            }
            I3DataOleAda tmp = (I3DataOleAda)_tableList[dataTable];

            DataTable upTable = dataTable.GetChanges();
            if (upTable == null)
            {
                return I3MsgInfo.Default;
            }

            if (aTran == null)
            {
                OleDbTransaction ot = connection.BeginTransaction();
                try
                {
                    tmp.Transaction = ot;
                    tmp.Adapter.Update(upTable);
                    ot.Commit();
                    dataTable.AcceptChanges();
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    ot.Rollback();
                    return new I3MsgInfo(false, ex.Message, ex);
                }
                finally
                {
                    ot.Dispose();
                }
            }
            else
            {
                tmp.Transaction = (OleDbTransaction)aTran;
                tmp.Adapter.Update(upTable);
                return I3MsgInfo.Default;
            }
        }

        public override void DisposeDataTable(DataTable dataTable)
        {
            bool have = _tableList.Contains(dataTable);
            if (!have)
            {
                return;
            }

            I3DataOleAda tmp = (I3DataOleAda)_tableList[dataTable];
            tmp.Adapter.Dispose();
            if (tmp.CommandBuilder != null)
            {
                tmp.CommandBuilder.Dispose();
            }
            _tableList.Remove(dataTable);
        }


        public override void Close()
        {
            try
            {
                foreach (DictionaryEntry de in _tableList)
                {
                    I3DataOleAda tmp = (I3DataOleAda)de.Value;
                    tmp.Adapter.Dispose();
                    if (tmp.CommandBuilder != null)
                    {
                        tmp.CommandBuilder.Dispose();
                    }
                }
                _tableList.Clear();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("数据连接关闭失败", ex);
            }
        }
    }
}
