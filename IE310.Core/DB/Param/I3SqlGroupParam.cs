using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace IE310.Core.DB
{
    [Serializable]
    public class I3SqlGroupParam:I3SqlParam
    {
        private List<I3SqlParam> subList = null;
        public I3SqlGroupParam():this(true)
        {

        }
        /// <summary>
		/// ������ϲ�������ʾ ��  �� ����
		/// ֻ����ϲ������������������Ŀ
		/// </summary>
		/// <param name="bGroupAnd">��ϲ�������</param>
        public I3SqlGroupParam(bool bGroupAnd)
		{
			paramType = bGroupAnd?I3SqlParamType.GroupAnd:I3SqlParamType.GroupOr;
		}
        public I3SqlParam[] SubParams
        {
            get
            {
                if (subList == null)
                    return new I3SqlParam[0];
                return subList.ToArray();
            }
        }
        /// <summary>
        /// ��������Ŀ��
        /// ֻ����ϲ���������������Ŀ
        /// </summary>
        /// <param name="sColName"></param>
        /// <param name="type"></param>
        /// <param name="oValue"></param>
        /// <returns></returns>
        public I3SqlGroupParam AppendChild(string sColName, I3SqlOperateType type, object oValue)
        {
            return AppendChild(new I3SqlItemParam(sColName, type, oValue));
        }
        /// <summary>
        /// ��������Ŀ��
        /// ֻ����ϲ���������������Ŀ
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <param name="sColName"></param>
        /// <param name="type"></param>
        /// <param name="oValue"></param>
        /// <returns></returns>
        public I3SqlGroupParam AppendChild(string tableAlias, string sColName, I3SqlOperateType type, object oValue)
        {
            return AppendChild(new I3SqlItemParam(tableAlias, sColName, type, oValue));
        }
        /// <summary>
        /// ��������Ŀ��
        /// ֻ����ϲ���������������Ŀ
        /// </summary>
        /// <param name="child">����Ŀ</param>
        /// <returns>����SQL��������</returns>
        public I3SqlGroupParam AppendChild(params I3SqlParam[] children)
        {
            if (subList == null)
                subList = new List<I3SqlParam>();
            subList.AddRange(children);
            return this;
        }

        /// <summary>
        /// �ڲ����������ݹ���ã�����SQL��SQL����
        /// </summary>
        /// <param name="aryParams">SQL����</param>
        /// <param name="sqlBuf">SQL���</param>
        public override void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf)
        {
            ///�����Ŀ����ÿһ����ϸ��Ŀ�������
            if (subList != null)
            {
                if (subList.Count > 1)
                {
                    sqlBuf.Append("(");
                }
                int iIndex = 0;
                foreach (I3SqlParam param in subList)
                {
                    if (iIndex > 0)
                    {
                        sqlBuf.Append(paramType == I3SqlParamType.GroupAnd ? " And " : " Or ");
                    }
                    param.ToSqlString(aryParams, sqlBuf);
                    iIndex++;
                }
                if (subList.Count > 1)
                {
                    sqlBuf.Append(")");
                }
            }
        }
    }
}
