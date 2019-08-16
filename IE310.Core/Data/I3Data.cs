/*                     ieData.IEFS_Data
 * 
 * 
 *          ����: ���
 * 
 *          ˵��: �ṩ��Sql.OLE.XML֮������ݲ�������
 * 
 *      ʹ�÷���: 1.�Զ������
 *                2.����CreateDataSql.CreateDataOle.CreateDataXml���ɶ���
 *                3.��FillTable(System.IEFS_Data.DataTable dataTable, Boolean clear, String sqlText)���������
 *                4.��UpdataTable(System.IEFS_Data.DataTable dataTable)����������
 *                  �������Ҫʹ��FillTable�������ݲſ���
 *                5.ʹ��Execute��ִ��SQL���
 *                6.ʹ�ô������DataTable�����ͷ�DataTable֮ǰ����Ҫ����DisposeDataTable���ͷ���ص���Դ
 *                  ��Ȼ�����Data�������ͷţ�����Ҫ������
 *                7.Ϊ�˷�ֹsqlע�룬Excute��FillTable�������µ����غ�����������ArrayList�������ǲ����б�
 * 
 * 
 * 
 * 
 *      ע������: 1.����Դ��XML�ļ�ʱ��Excute������Ч
 *                    2.����Դ��XML�ļ�ʱ��FillTable����ȡ����������ݣ�����Ҫ����SQL���
 *                    3.����Դ��XML�ļ�ʱ��UpdataTable���������б�dataTable����null����  
 *                    4.ʹ����Ϻ󣬵���Close()������
 * 
 * 
 * 
 *      �޸ļ�¼: 1.2008-06-24
 *                ������һ�������ص����⣺��ǰ����DataTableʱ���Ǹ���DataTable.TableName�õ�����TableNames�е�������
 * ���������������� ��ͨ����������õ�Adapter�����еĶ�ӦAdapter����ǰ������DataTableʱ�͸���һ��DataName�����������ݱ������
 * ���������������� �����Ļ�������Ⱥ󴴽���һ�����ݱ���������ݼ����󣬾ͻ�������´���(ǰ�����ݼ��޸ĵ�sql����ڸ��º������ݼ�ʱ������)������޸�Ϊ��һ��Guid��ΪTableName
 *                  a.��ǰ�ķ���Ҳ��Ȼ�����ã���������Ǵ���DataTableʱ����ָ��TableName
 *                  b.��������ǰ�ֹ�ָ��TableName���������Զ�ָ��TableName������Ҫע�����ͬһ�ű�ʱ�����ݸ������⣬���������ݿ��̱�������⣬����ע�⣩
 *                  c.ע�⣺�����xml����Դ����Ȼ����ָ��DataTable.TableNameΪ���ݱ���  ��Ϊ��ģʽ��ͬ��ʵ����ֻ��ȡ��һ�η��뵽��һ��DataSet��
 * 
 *                2.2008-06-24
 *                  ��TableName��Ada��CommandBuilder�Ĺ���ʽ������ArrayList��Ϊһ��HashTable
 * 
 *                3.2008-11-21
 *                  1.��Excute��FillTable�������µ����غ�����������ArrayList����
 *                    ʹ��ʱ��ArrayList�е�Ԫ�ر���ΪSqlParameter����OleDbParameter���˹�����Ϊ�˷�ֹsqlע�����ӵ�
 *                    
 *                4.2011-03-09
 *                  1.���Ӻ���GetConnection���Է�����������
 *                  2.UpdateTable Execute ���������� �����Ӳ���DbTransaction aTran��ָʾ�Ƿ���Ҫʹ�������ύģʽ
 *                    ��Ϊnullʱ����һ�����ύ��Σ���Ҫ�Լ�д���񣬲��������� commit���Լ������ݼ�AcceptChanges()
 *                  2011-04-12
 *                  ������ʹ������ķ���
 *                  
 *                5.2011-04-12
 *                  ����ϣ��ļ�ֵ��TableName��Ϊֱ�Ӵ�DataTable������ˣ�����DataTable����ָ����������ݱ�����
 *                  ������ʹ���Զ����ɵ�Guid��Ϊ���ݱ���
 *                  
 *                6.2011-08-09
 *                  ʹ��select��ѯʱ��֮ǰʹ����CommandBuilder������UpdateCommand��InsertCommand��DeleteCommand
 *                  �����ͬһ�����ݼ��Ⱥ�����ʹ�ò�ͬ��select��䲢�������£��ڶ��ε�select����ֶν��٣���ڶ��θ���ʱ���Զ����ɵ�Command
 *                  ��UpdateCommand���˶���Ĳ�����ֵ��null�����µ����ݿ��������󣺸�����Ԥ��1�������е�0����
 *                  *****�����ԣ�ʹ��CommandBuilderʱ����Ҫ��ͬһ�����ݼ�ʹ�ò�ͬ��select���
 *                  
 *                  ���⣬����ʹ��CommandBuilder�Զ����ɵ��������Ч�ʲ��ã������FillTable���������˽ӿڣ�ʹ��ʾ����
 *                      string sqlStr = " select id,ET_Sort from GK_ExcelTemplate order by ET_Sort asc ";
                        OleDbCommand update = new OleDbCommand("UPDATE GK_ExcelTemplate SET ET_Sort = ? WHERE (id = ?)");
                        update.Parameters.Add("@sort", OleDbType.Integer, 4, "ET_Sort");
                        update.Parameters.Add("@id", OleDbType.VarChar, 50, "id");
                        IEFS_Command command = new IEFS_Command(update, null, null);
                        fcon.FillTable(tmp, true, sqlStr, null, command);
                    ע�⣬���ʹ��OleDb��ʽ���ӵ����ݿ⣬sql�����д����ʱ��Ҫ��?����Ӳ�����˳����(�����@ET_Sort�ᱨ��Ҫ���ñ���ET_Sort)��
 *                  ��ʹ��sql����ʱ��Ҫ��@ET_Sort����Ӳ�������Ҫ��˳��
 *                  
 *                  ���⣬UpdateCommand�У�ID��������ʱ��Ҫ����������������
 *                      SqlCommand command = new SqlCommand(
                            "UPDATE Customers SET CustomerID = @CustomerID, CompanyName = @CompanyName " +
                            "WHERE CustomerID = @oldCustomerID", connection);
                        command.Parameters.Add("@CustomerID", SqlDbType.NChar, 5, "CustomerID");
                        command.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40, "CompanyName");
                        SqlParameter parameter = command.Parameters.Add(
                            "@oldCustomerID", SqlDbType.NChar, 5, "CustomerID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = command;
 *                  ��@oldCustomerID���������Ҫָ��ֵ��ԴΪԭʼֵ����ָ��ʱĬ��Ϊ��ǰֵ��

 *  
 *                                                  Created by ie
 *                                                            2008-01-01      
 * 
 * */



using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;
using IE310.Core.Utils;

namespace IE310.Core.Data
{
    public class I3Command
    {

        protected DbCommand updateCommand;
        protected DbCommand deleteCommand;
        protected DbCommand _insertCommand;


        public I3Command(DbCommand updateCommand, DbCommand deleteCommand, DbCommand insertCommand)
        {
            updateCommand = updateCommand;
            deleteCommand = deleteCommand;
            _insertCommand = insertCommand;
        }

        public DbCommand UpdateCommand
        {
            get
            {
                return updateCommand;
            }
        }
        public DbCommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
        }
        public DbCommand InsertCommand
        {
            get
            {
                return _insertCommand;
            }
        }
    }

    public abstract class I3Data /*: IDisposable*/
    {
        protected string lastErrorInfo = "";
        public string LastErrorInfo
        {
            get
            {
                return this.lastErrorInfo;
            }
            set
            {
                this.lastErrorInfo = value;
            }
        }

        protected bool active;
        /// <summary>
        /// �����Ƿ��Ѿ����ӵ����ݿ�
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
        }



        public abstract DbConnection GetConnection();

      
        /// <summary>
        /// ������������ʵ�֣�����������ݼ�,clearָʾ�Ƿ����dataTable�е���������
        /// ���Զ�ε����������DataTable
        /// ������ͬһ�����ڶ����䵽��ͬ�����ݼ���ʱ����ע��������ݻ��ҡ�
        /// 
        /// ����Xml����Դ��ֻ�ܶ�ȡһ�Σ����ǽ�Data��Ķ����ͷź����¶�ȡ
        /// 
        /// ���⣬����Xml����Դ���봫��""��ΪsqlText
        /// 
        /// 
        /// �ڶ�ĳ��DataTable������䣬�����������޸ģ������ʱʹ��DataTable.Clear()
        /// ��SqlCommandBuilder�����ɵ���Ӧsql���Ҳ�ᱻ�Զ��ͷ�
        /// 
        /// ArrayList�еĶ��������SqlParameter����OleDbParameter
        /// 
        /// �������Զ���UpdateCommand��InsertCommand��DeleteCommand֧�֣�˵�����޸ļ�¼6
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="clear"></param>
        public abstract I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbParameter[] paramList, DbTransaction aTran, I3Command command);
        public I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbTransaction aTran, I3Command command)
        {
            return FillTable(dataTable, clear, sqlText, null, aTran, command);
        }

        /// <summary>
        /// ����ִ��û�з��ؽ����sql����     
        /// ����Xml����Դ������ִ�д˺���     
        /// 
        /// UpdateTable Execute ���������� �����Ӳ���DbTransaction aTran��ָʾ�Ƿ���Ҫʹ�������ύģʽ
        /// ��Ϊnullʱ����һ�����ύ��Σ���Ҫ�Լ�д���񣬲��������� commit���Լ������ݼ�AcceptChanges()
        /// 
        /// </summary>
        /// <param name="sqlText"></param>
        public abstract I3MsgInfo Execute(string sqlText, DbParameter[] paramList, DbTransaction aTran);
        public I3MsgInfo Execute(string sqlText, DbTransaction aTran)
        {
            return Execute(sqlText, null, aTran);
        }

        /// <summary>
        /// ���ڸ������ݵ�����Դ
        /// ÿ��ֻ��ѡ�����һ����
        /// ����Xml����Դ���봫��null��ΪdataTable,��Ϊÿ�θ��¶�����������dataset
        /// 
        /// UpdateTable Execute ���������� �����Ӳ���DbTransaction aTran��ָʾ�Ƿ���Ҫʹ�������ύģʽ
        /// ��Ϊnullʱ����һ�����ύ��Σ���Ҫ�Լ�д���񣬲��������� commit���Լ������ݼ�AcceptChanges()
        /// 
        /// </summary>
        public abstract I3MsgInfo UpdataTable(DataTable dataTable, DbTransaction aTran);
        public abstract void DisposeDataTable(DataTable dataTable);
        public abstract void Close();



        /// <summary>
        /// ��������sql��Data����
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public static I3Data CreateDataSql(string conStr)
        {
            I3DataSql data = new I3DataSql(conStr);
            return (I3Data)data;
        }

        /// <summary>
        /// ��������OleDb��Data����
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public static I3Data CreateDataOle(string conStr)
        {
            I3DataOle data = new I3DataOle(conStr);
            return (I3Data)data;
        }


        /// <summary>
        /// 
        /// ��������Xml�ļ���Data����
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public static I3Data CreateDataXml(string conStr)
        {
            I3DataXml data = new I3DataXml(conStr);
            return (I3Data)data;
        }

        #region �ͷ���Դ       �����Ǹ�ʾ����ʵ���ͷŷŵ�Close������
        /*protected bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                //�ͷ��й���Դ
            }
            //�ͷŷ��й���Դ

            isDisposed = true;
        }
        ~IEFS_Data()
        {
            Dispose(false);
        }*/
        
        #region ����������д
        /* �������У�������������дDispose(bool)����
         * 
         * if (isDisposed)
         *     return;
         * 
         * if (disposing)
         * {
         *     //�ͷ��й���Դ
         * }
         * //�ͷŷ��й���Դ
         * 
         * base.Dispose(disposing);
         * 
         */
        #endregion

        #endregion
    }
}
