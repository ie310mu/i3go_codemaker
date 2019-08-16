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
		/// 构造组合参数，表示 且  或 参数
		/// 只有组合参数才能增加组合子项目
		/// </summary>
		/// <param name="bGroupAnd">组合参数类型</param>
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
        /// 增加子项目，
        /// 只有组合参数才能增加子项目
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
        /// 增加子项目，
        /// 只有组合参数才能增加子项目
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
        /// 增加子项目，
        /// 只有组合参数才能增加子项目
        /// </summary>
        /// <param name="child">子项目</param>
        /// <returns>返回SQL参数本身</returns>
        public I3SqlGroupParam AppendChild(params I3SqlParam[] children)
        {
            if (subList == null)
                subList = new List<I3SqlParam>();
            subList.AddRange(children);
            return this;
        }

        /// <summary>
        /// 内部处理方法，递归调用，生成SQL及SQL参数
        /// </summary>
        /// <param name="aryParams">SQL参数</param>
        /// <param name="sqlBuf">SQL语句</param>
        public override void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf)
        {
            ///组合项目，将每一个明细项目进行组合
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
