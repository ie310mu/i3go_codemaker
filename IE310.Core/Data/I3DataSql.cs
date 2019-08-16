using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Drawing;
using IE310.Core.Utils;

//1.��������
//2.������ArrayListΪһ��Hashtable
//3.FillTable���ж�have
//4.���
//5.����
//6.Update
//7.Close
//8.DisposeDataTable
//9.Execute finally

namespace IE310.Core.Data
{

    class I3DataSqlAda
    {
        private SqlDataAdapter adapter = null;
        public SqlDataAdapter Adapter
        {
            get
            {
                return adapter;
            }
        }
        private SqlCommandBuilder commandBuilder = null;
        public SqlCommandBuilder CommandBuilder
        {
            get
            {
                return commandBuilder;
            }
        }

        public I3DataSqlAda(SqlConnection con, string sqlStr, I3Command command)
        {
            adapter = new SqlDataAdapter(sqlStr, con);
            if (command == null)
            {
                commandBuilder = new SqlCommandBuilder(adapter);
            }
            else
            {
                adapter.UpdateCommand = command == null || command.UpdateCommand == null ? null : (SqlCommand)command.UpdateCommand;
                adapter.DeleteCommand = command == null || command.DeleteCommand == null ? null : (SqlCommand)command.DeleteCommand;
                adapter.InsertCommand = command == null || command.InsertCommand == null ? null : (SqlCommand)command.InsertCommand;
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

        public SqlCommand SelectCommand
        {
            get
            {
                return adapter.SelectCommand;
            }
        }

        public SqlTransaction Transaction
        {
            set
            {
                adapter.SelectCommand.Transaction = value;

                if (commandBuilder != null)
                {
                    commandBuilder.GetUpdateCommand().Transaction = value;
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

    class I3DataSql : I3Data
    {
        private SqlConnection connection;
        private Hashtable _tableList = new Hashtable();

        public override DbConnection GetConnection()
        {
            return connection;
        }




        public I3DataSql(string conStr)
        {
            try
            {
                connection = new SqlConnection(conStr);
                connection.Open();
                active = true;
            }
            catch (Exception ex)
            {
                this.lastErrorInfo = "���ݿ����Ӵ���ʧ��\r\n" + ex.Message;
                //throw new Exception("���ݿ����Ӵ���ʧ��", ex);
            }
        }

        public override I3MsgInfo Execute(string sqlText, DbParameter[] paramList, DbTransaction aTran)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "�����ѶϿ����޷�ִ��sql���");
            }

            SqlCommand command = new SqlCommand(sqlText, connection);
            command.Parameters.Clear();
            if (paramList != null)
            {
                for (int i = 0; i < paramList.Length; i++)
                {
                    command.Parameters.Add(paramList[i]);
                }
            }

            if (aTran == null)
            {
                //2012.11.01 �����жϣ��е�SQL��䲻��ʹ������ 
                if (sqlText.ToUpper().IndexOf("DROP DATABASE") >= 0 || sqlText.ToUpper().IndexOf("RESTORE DATABASE") >= 0)
                {
                    command.Transaction = null;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    return I3MsgInfo.Default;
                }
                else
                {
                    SqlTransaction st = connection.BeginTransaction();
                    try
                    {
                        command.Transaction = st;
                        command.ExecuteNonQuery();
                        st.Commit();
                        command.Parameters.Clear();
                        return I3MsgInfo.Default;
                    }
                    catch (Exception ex)
                    {
                        st.Rollback();
                        return new I3MsgInfo(false, ex.Message, ex);
                    }
                    finally
                    {
                        st.Dispose();
                        command.Dispose();
                    }
                }
            }
            else
            {
                command.Transaction = (SqlTransaction)aTran;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return I3MsgInfo.Default;
            }
        }

        public override I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbParameter[] paramList, DbTransaction aTran, I3Command command)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "�����ѶϿ����޷�ִ��sql���");
            }

            if (dataTable.TableName == "")
            {
                dataTable.TableName = Guid.NewGuid().ToString().ToUpper();
            }

            try
            {
                I3DataSqlAda tmp = null;
                bool have = _tableList.Contains(dataTable);

                if (have)
                {
                    tmp = (I3DataSqlAda)_tableList[dataTable];
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
                            tmp.Adapter.SelectCommand.Parameters.Add(paramList[i]);
                        }
                    }
                    tmp.Adapter.Fill(dataTable);
                    //tmp.SelectCommand.Parameters.Clear();
                }
                else
                {
                    tmp = new I3DataSqlAda(connection, sqlText, command);
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
                        tmp.SelectCommand.Transaction = (SqlTransaction)aTran;
                    }
                    else
                    {
                        tmp.SelectCommand.Transaction = null;
                    }
                    tmp.Adapter.Fill(dataTable);
                    //tmp.SelectCommand.Parameters.Clear();  //�������ɾ�������������ݺ�Upadateʱ�ᱨ���������������������������ע��
                    //���ǣ�֮ǰ��ʲôҪ������䣿   
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
                return new I3MsgInfo(false, "�����ѶϿ����޷�ִ��sql���");
            }

            bool have = _tableList.Contains(dataTable);
            if (!have)
            {
                return new I3MsgInfo(false, "�˱�δ��ieData.SqlData����,�޷�������µ�����Դ");
            }
            I3DataSqlAda tmp = (I3DataSqlAda)_tableList[dataTable];

            DataTable upTable = dataTable.GetChanges();
            if (upTable == null)
            {
                return I3MsgInfo.Default;
            }

            if (aTran == null)
            {
                SqlTransaction st = connection.BeginTransaction();
                try
                {
                    tmp.Transaction = st;
                    tmp.Adapter.Update(upTable);
                    st.Commit();
                    dataTable.AcceptChanges();
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    st.Rollback();
                    return new I3MsgInfo(false, ex.Message, ex);
                }
                finally
                {
                    st.Dispose();
                }
            }
            else
            {
                tmp.Transaction = (SqlTransaction)aTran;
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

            I3DataSqlAda tmp = (I3DataSqlAda)_tableList[dataTable];
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
                    I3DataSqlAda tmp = (I3DataSqlAda)de.Value;
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
                throw new Exception("�������ӹر�ʧ��", ex);
            }
        }
    }
}



/*

ʹ�����ַ��������ݼ���
1.ʹ�������sql��䣬ʹ���˲���
  string sqlstr = " select * from RoleUsePage where RoleName = @RoleName";
2.ʹ�������sql��䣬��ʹ�ò���
  string sqlstr = " select * from RoleUsePage where RoleName = '����Ա' ";

�ڴ����ݼ����Ƚ���������գ�
for (int i = 0; i < dataTable.Rows.Count; i++)
{
  dataTable.Rows[i].Delete();
}

Ȼ������һЩ���ݣ�
DataRow row = dataTable.NewRow();
row.BeginEdit();
row["RoleName"] = "����Ա";
row["PageID"] = "1";
row.EndEdit();
dataTable.Rows.Add(row);

������SqlDataAdapter.Updata()�����������ݣ�����SqlDataAdapter��sql��䣬ʹ����SqlCommandBuilder�Զ����ɡ�

���ڣ���������ˣ�
ʹ�õ�һ�ַ�����select����а�������ʱ������ʱ���ִ���
���������������� "@RoleName"��
���ڶ��ַ�����û��������֡�

�ν⣿



*/