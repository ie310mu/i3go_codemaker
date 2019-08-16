using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;

namespace IE310.Core.DB
{
    [Serializable]
    public class I3SqlItemParam:I3SqlParam
    {
       	
		private string sColName;
		private string sTableAlias;
		private object oValue;
		/// <summary>
		/// ����Sql��Ŀ,�������Ĭ��Ϊ t,ƥ�䷽ʽΪ "="
		/// </summary>
		/// <param name="sColName">�ֶ�����</param>
		/// <param name="oValue">�Ƚ�ֵ�����ݱȽ�ֵ�������������������ͣ��磺ʱ�����ΪDateTime���ͣ���ֵ����ΪInt��decimal����
		/// �����Ҫ��ʾΪ�գ���ʹ��I3SqlOperateType.Ϊ�� ����ʾ</param>
		public I3SqlItemParam(string sColName,object oValue):
			this("t",sColName,I3SqlOperateType.����,oValue)
		{
		}
		/// <summary>
		/// ����Sql��Ŀ,�������Ĭ��Ϊ t
		/// </summary>
		/// <param name="sColName">�ֶ�����</param>
		/// <param name="type">�Ƚ�����</param>
		/// <param name="oValue">�Ƚ�ֵ�����ݱȽ�ֵ�������������������ͣ��磺ʱ�����ΪDateTime���ͣ���ֵ����ΪInt��decimal����
		/// �����Ҫ��ʾΪ�գ���ʹ��I3SqlOperateType.Ϊ�� ����ʾ</param>
		public I3SqlItemParam(string sColName,I3SqlOperateType type,object oValue):
			this("t",sColName,type,oValue)
		{
		}
		/// <summary>
		/// ����Sql��Ŀ������
		/// </summary>
		/// <param name="tableAlias">�����</param>
		/// <param name="sColName">�ֶ�����</param>
		/// <param name="type">�Ƚ�����</param>
		/// <param name="oValue">�Ƚ�ֵ�����ݱȽ�ֵ�������������������ͣ��磺ʱ�����ΪDateTime���ͣ���ֵ����ΪInt��decimal����
		/// �����Ҫ��ʾΪ�գ���ʹ��I3SqlOperateType.Ϊ�� ����ʾ</param>
        public I3SqlItemParam(string tableAlias, string sColName, I3SqlOperateType type, object oValue)
		{
			this.sTableAlias = tableAlias;
			this.sColName = sColName;
			this.oValue = oValue;
			opType = type;
			paramType = I3SqlParamType.Item;
			this.sTableAlias = sTableAlias;
		}
       
		public string ColumnName
		{
			get
			{
				return sColName;
			}
		}
		
		public I3SqlOperateType OperateType
		{
			get
			{
				return opType;
			}
		}
		public object Value
		{
			get
			{
				return oValue;
			}
		}		
		private static string Oper2Sql(I3SqlOperateType type)
		{
			switch(type)
			{
				case I3SqlOperateType.����:return " = ";
				case I3SqlOperateType.С��:return " < ";
				case I3SqlOperateType.С�ڵ���:return " <= ";
				case I3SqlOperateType.����:return " > ";
				case I3SqlOperateType.���ڵ���:return " >= ";
				case I3SqlOperateType.������:return " <> ";
				case I3SqlOperateType.ǰƥ�� :return " Like ";
				case I3SqlOperateType.ȫƥ��:return " Like ";
				case I3SqlOperateType.��ƥ�� :return " Like ";
				case I3SqlOperateType.��ƥ��:return " Not Like ";
				case I3SqlOperateType.Ϊ��:return " Is Null";
				case I3SqlOperateType.��Ϊ��:return " Is Not Null";
				default:
					return "";
			}
		}
	 

		/// <summary>
		/// �ڲ����������ݹ���ã�����SQL��SQL����
		/// </summary>
		/// <param name="aryParams">SQL����</param>
		/// <param name="sqlBuf">SQL���</param>
        public override void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf)
        {
           if (this.opType != I3SqlOperateType.Ϊ�� &&
                this.opType != I3SqlOperateType.��Ϊ��)
            {
                string sParamName = ":W_" + sColName + sqlBuf.Length;
                //sqlBuf.Append(this.sTableAlias).Append(".").Append(this.sColName).Append(Oper2Sql(opType));
                sqlBuf.Append(!string.IsNullOrEmpty(this.sTableAlias) ? this.sTableAlias + "." : " ").Append(this.sColName).Append(Oper2Sql(opType));
                sqlBuf.Append(sParamName);
                IDbDataParameter oParam = BuildParam(sParamName, oValue);
                aryParams.Add(oParam);
                //return sqlBuf.ToString();
            }
            else
            {
                sqlBuf.Append(!string.IsNullOrEmpty(this.sTableAlias) ? this.sTableAlias + "." : " ").Append(this.sColName).Append(this.opType == I3SqlOperateType.Ϊ�� ? " Is Null" : " Is Not Null");
            }
        }
        
    }
}
