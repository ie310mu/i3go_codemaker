using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Common;
using IE310.Core.Utils;

namespace IE310.Core.Data
{
    class I3DataXml : I3Data
    {
        private DataSet dataset;
        private ArrayList tablenames = new ArrayList();
        private String filename;

        public override DbConnection GetConnection()
        {
            return null;
        }

        public I3DataXml(string conStr)
        {
            try
            {
                filename = conStr;
                dataset = new DataSet();
                dataset.ReadXml(conStr);
                active = true;
            }
            catch (Exception ex)
            {
                this.lastErrorInfo = "xml�ļ�����ʧ��\r\n" + ex.Message;
                //throw new Exception("xml�ļ�����ʧ��", ex);
            }
        }
    
        public override void Close()
        {
            dataset.Dispose();
        }

        public override I3MsgInfo Execute(string sqlText, DbParameter[] paramList, DbTransaction aTran)
        {
            return new I3MsgInfo(false, "������ԴΪXml�ļ�ʱ,�޷�ִ��sql���!");
        }

        public override I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbParameter[] paramList, DbTransaction aTran, I3Command command)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "δ��ȡXml�ļ����޷����DataTable");
            }

            if (dataTable.TableName == "")
            {
                return new I3MsgInfo(false, "δָ��TableName!");
            }

            //if (clear) dataTable.Clear();//���������XML����Դʱʼ��Ϊȫ������

            if (sqlText == "")
            {
                try
                {
                    int count = dataset.Tables.Count;
                    int j = -1;
                    for (int i = 0; i < count; i++ )
                    {
                        if (dataset.Tables[i].TableName == dataTable.TableName)
                        {
                            j = i;
                            break;
                        }
                    }
                    if (j == -1)
                    {
                        return new I3MsgInfo(false, "��Xml����Դ��û�б�" + dataTable.TableName + "�Ĵ���");
                    }
                    count = tablenames.Count; j = -1;
                    for (int i = 0; i < count; i++)
                    {
                        if (tablenames[i].ToString() == dataTable.TableName)
                        {
                            j = i;
                            break;
                        }
                    }
                    if (j != -1)
                    {
                        return new I3MsgInfo(false, "��Xml����Դ�б�" + dataTable.TableName + "�ѱ���ȡ");
                    }

                    dataTable = dataset.Tables[dataTable.TableName];
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }
            }
            else
            {
                return new I3MsgInfo(false, "������ԴΪXml�ļ�ʱ,���DataTable����Ҫsql���!");
            }
        }

        public override I3MsgInfo UpdataTable(DataTable dataTable, DbTransaction aTran)
        {
            if (dataTable == null)
            {
                try
                {
                    dataset.WriteXml(filename);
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, ex.Message, ex);
                }
            }
            else
            {
                return new I3MsgInfo(false, "������ԴΪXml�ļ�ʱ,���½���������DataTable����˲���Ҫ����DataTable��Ϊ����!");
            }
        }

        public override void DisposeDataTable(DataTable dataTable)
        {
        }
    }
}
