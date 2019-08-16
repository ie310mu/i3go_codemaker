using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;

namespace IE310.Core.DB
{
    /// <summary>
    /// SQL参数类型，一般项目，And、Or连接
    /// </summary>
    public enum I3SqlParamType
    {
        Item = 0,
        GroupAnd = 1,
        GroupOr = 2
    }
    /// <summary>
    /// SQL操作类型
    /// </summary>
    public enum I3SqlOperateType
    {
        等于 = 0,
        小于 = 1,
        小于等于 = 2,
        大于 = 3,
        大于等于 = 4,
        不等于 = 5,
        前匹配 = 6,
        全匹配 = 7,
        后匹配 = 8,
        不匹配 = 9,
        为空 = 10,
        不为空 = 11,
        复杂条件 = 12
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
                if (opType == I3SqlOperateType.前匹配)
                    oValue = oValue + "%";
                else if (opType == I3SqlOperateType.后匹配)
                    oValue = "%" + oValue;
                else if (opType == I3SqlOperateType.全匹配)
                    oValue = "%" + oValue + "%";
                oParam = I3DBUtil.PrepareParameter(sParamName, OracleType.NVarChar, 64, oValue);
            }
            return oParam;
        }
        /// <summary>
		/// 根据当前对象，返回组中的SQL及参数数组
		/// 注意：参数中不包含 Where 或 And 连接词。
		/// </summary>
		/// <param name="oParams">返回的SQL参数数组</param>
		/// <returns>返回的SQL字符串</returns>
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
		/// 内部处理方法，递归调用，生成SQL及SQL参数
		/// </summary>
		/// <param name="aryParams">SQL参数</param>
		/// <param name="sqlBuf">SQL语句</param>
        public abstract void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf);
	}
}
