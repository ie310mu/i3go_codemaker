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
                this.lastErrorInfo = "xml文件加载失败\r\n" + ex.Message;
                //throw new Exception("xml文件加载失败", ex);
            }
        }
    
        public override void Close()
        {
            dataset.Dispose();
        }

        public override I3MsgInfo Execute(string sqlText, DbParameter[] paramList, DbTransaction aTran)
        {
            return new I3MsgInfo(false, "当数据源为Xml文件时,无法执行sql语句!");
        }

        public override I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbParameter[] paramList, DbTransaction aTran, I3Command command)
        {
            if (!active)
            {
                return new I3MsgInfo(false, "未读取Xml文件，无法填充DataTable");
            }

            if (dataTable.TableName == "")
            {
                return new I3MsgInfo(false, "未指定TableName!");
            }

            //if (clear) dataTable.Clear();//无需清除，XML数据源时始终为全部数据

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
                        return new I3MsgInfo(false, "此Xml数据源中没有表" + dataTable.TableName + "的存在");
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
                        return new I3MsgInfo(false, "此Xml数据源中表" + dataTable.TableName + "已被读取");
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
                return new I3MsgInfo(false, "当数据源为Xml文件时,填充DataTable不需要sql语句!");
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
                return new I3MsgInfo(false, "当数据源为Xml文件时,更新将更新所有DataTable，因此不需要传递DataTable作为参数!");
            }
        }

        public override void DisposeDataTable(DataTable dataTable)
        {
        }
    }
}
