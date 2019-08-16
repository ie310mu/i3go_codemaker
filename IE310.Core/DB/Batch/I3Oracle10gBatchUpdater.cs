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
        /// ��ȡ��������
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
        /// ��ȡ��������
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
        /// ��ȡɾ������
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
            //��������Ĳ���
            List<IDbDataParameter> ps = this.insertCommand.Parameters;
            //��������Ĳ���ֵ
            ArrayList pvs = this.insertCommand.ParametersVals;

            //��ȡID������
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
            //����ID��������������еĿ���
            if (bFindId)
            {
                foreach (ArrayList al in pvs)
                {
                    if (string.IsNullOrEmpty((string)al[iIdIndex]))
                    {  ///û��Guid,���Guid��ֵ
                        al[iIdIndex] = Guid.NewGuid().ToString();
                    }
                    ///�����е�Guidֵ��¼���б���
                    this.insertIDList.Add(al[iIdIndex]);
                }
            }
            return this.insertCommand.Execute(sqlTrans);
        }
        /// <summary>
        /// ִ����������
        /// </summary>
        /// <param name="connection">�������Ӵ�</param>
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
                    //�ع����ݿ�����
                    sqlTrans.Rollback();
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecuteִ��ʧ��:", ex.Message));
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
                //ɾ������
                if (this.deleteCommand != null)
                {
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute��ʼִ��ɾ��"));
                    iDel = this.deleteCommand.Execute(sqlTrans);
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute���ִ��ɾ��,�ɹ�������Ϊ:", iDel.ToString()));
                }
                //��������
                if (this.updateCommand != null)
                {
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute��ʼִ�и���"));
                    iUpdate = this.updateCommand.Execute(sqlTrans);
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute���ִ�и���,�ɹ�������Ϊ:", iUpdate.ToString()));
                }
                //��������
                if (this.insertCommand != null)
                {
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute��ʼִ�в���"));
                    iInsert = this.ExecuteInsertCommand(sqlTrans);
                    //if (LogHelper.IsInfoEnable)
                    //    LogHelper.Info(string.Concat("BatchExecute���ִ�в���,�ɹ�������Ϊ:", iInsert.ToString()));
                }
                //if (LogHelper.IsInfoEnable)
                //    LogHelper.Info(string.Concat("BatchExecute��ʼ�ύ����"));
                //�ύ��������
                //sqlTrans.Commit();
                //if (LogHelper.IsInfoEnable)
                //    LogHelper.Info(string.Concat("BatchExecute����ύ����"));
                return new I3MsgInfo(true, "ɾ��:" + iDel + ";�޸�:" + iUpdate + ";����" + iInsert);
            
        }
       ///// <summary>
        ///// ��ȡ����SQL�����ݱ�����
        ///// </summary>
        ///// <param name="sql">SQL</param>
        ///// <returns></returns>
        //private string GetInsertTableName(string sql)
        //{
        //    int iIntoIndex = sql.ToUpper().IndexOf(" INTO ");
        //    int iEndIndex = sql.IndexOf("(", iIntoIndex + 1);
        //    return sql.Substring(iIntoIndex + 6, iEndIndex - iIntoIndex - 6);
        //}

        #region IBatchUpdater ��Ա

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
