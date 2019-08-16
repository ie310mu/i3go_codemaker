using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;

namespace IE310.Core.DB
{
    /// <summary>
    /// SQL�������ͣ�һ����Ŀ��And��Or����
    /// </summary>
    public enum I3SqlParamType
    {
        Item = 0,
        GroupAnd = 1,
        GroupOr = 2
    }
    /// <summary>
    /// SQL��������
    /// </summary>
    public enum I3SqlOperateType
    {
        ���� = 0,
        С�� = 1,
        С�ڵ��� = 2,
        ���� = 3,
        ���ڵ��� = 4,
        ������ = 5,
        ǰƥ�� = 6,
        ȫƥ�� = 7,
        ��ƥ�� = 8,
        ��ƥ�� = 9,
        Ϊ�� = 10,
        ��Ϊ�� = 11,
        �������� = 12
    }
    [Serializable]
    public  abstract class I3SqlParam
    {
       protected I3SqlOperateType opType;
       protected I3SqlParamType paramType;
        public I3SqlParamType ParamType
        {
            get
            {
                return paramType;
            }
        }
        protected IDbDataParameter BuildParam(string sParamName, object oValue)
        {
            IDbDataParameter oParam;
            if (oValue is int
                || oValue is decimal
                || oValue is float
                || oValue is double)
            {
                oParam = I3DBUtil.PrepareParameter(sParamName, OracleType.Number, 22, oValue);
            }
            else if (oValue is DateTime)
            {
                oParam = I3DBUtil.PrepareParameter(sParamName, OracleType.DateTime, oValue);
            }
            else
            {
                if (opType == I3SqlOperateType.ǰƥ��)
                    oValue = oValue + "%";
                else if (opType == I3SqlOperateType.��ƥ��)
                    oValue = "%" + oValue;
                else if (opType == I3SqlOperateType.ȫƥ��)
                    oValue = "%" + oValue + "%";
                oParam = I3DBUtil.PrepareParameter(sParamName, OracleType.NVarChar, 64, oValue);
            }
            return oParam;
        }
        /// <summary>
		/// ���ݵ�ǰ���󣬷������е�SQL����������
		/// ע�⣺�����в����� Where �� And ���Ӵʡ�
		/// </summary>
		/// <param name="oParams">���ص�SQL��������</param>
		/// <returns>���ص�SQL�ַ���</returns>
		public string ToSqlString(out IDbDataParameter[]  oParams)
		{
			StringBuilder sqlBuf = new StringBuilder();
			ArrayList list = new ArrayList();
			ToSqlString(list,sqlBuf);
            oParams = (IDbDataParameter[])list.ToArray(typeof(IDbDataParameter));
			return sqlBuf.ToString();
		}
        public string ToSqlString(ArrayList aryParams)
        {
            StringBuilder sqlBuf = new StringBuilder();
            ToSqlString(aryParams, sqlBuf);
            return sqlBuf.ToString();
        }
		/// <summary>
		/// �ڲ����������ݹ���ã�����SQL��SQL����
		/// </summary>
		/// <param name="aryParams">SQL����</param>
		/// <param name="sqlBuf">SQL���</param>
        public abstract void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf);
	}
}
