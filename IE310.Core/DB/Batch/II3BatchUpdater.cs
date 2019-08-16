using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IE310.Core.Utils;

namespace IE310.Core.DB
{
    /// <summary>
    /// ��������ӿ�
    /// </summary>
    public interface II3BatchCommand
    {
        string TableName { get;set;}
        /// <summary>
        /// ��ȡ������SQL
        /// </summary>
        string Sql { get; set; }
        /// <summary>
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">��������</param>
        /// <param name="size">��С</param>
        void AddParameter(string name, DbType type);
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="data">����</param>
        void AddData(params object[] data);
        /// <summary>
        /// ִ������
        /// </summary>
        /// <param name="connection">���Ӵ�</param>
        /// <param name="sql">SQL</param>
        /// <returns>������</returns>
        int Execute(string connection);
        /// <summary>
        /// ִ������
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        /// <param name="sql">SQL</param>
        /// <returns>������</returns>
        int Execute(IDbTransaction trans);
    }
    /// <summary>
    /// �������½ӿ�
    /// </summary>
    public interface II3BatchUpdater
    {
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        II3BatchCommand InsertCommand { get; }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        II3BatchCommand UpdateCommand { get; }
        /// <summary>
        /// ��ȡɾ������
        /// </summary>
        II3BatchCommand DeleteCommand { get; }
        /// <summary>
        /// ����ִ��
        /// </summary>
        /// <returns>ִ�н��</returns>
        I3MsgInfo BatchExecute(string connection);

        /// <summary>
        /// ����ִ��
        /// </summary>
        /// <returns>ִ�н��</returns>
        I3MsgInfo BatchExecute(IDbTransaction sqlTrans);
        /// <summary>
        /// ��ȡ�ող����IDֵ�б�
        /// </summary>
        string[] InsertIDList { get; }

    }
}
