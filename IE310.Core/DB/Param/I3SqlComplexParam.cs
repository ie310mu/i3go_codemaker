using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace IE310.Core.DB
{
    /// <summary>
    /// 复杂的条件
    /// </summary>
    [Serializable]
    public class I3SqlComplexParam : I3SqlParam
    {
        private string complexCause;
        private object[] multiValues;
        /// <summary>
        /// 构造包含多个条件的复杂的参数语句
        /// </summary>
        /// <param name="complexCause">ＳＱＬ条件，多个参数使用{0},{1}</param>
        /// <param name="oValues">参数值数组，个数必须与条件的中的参数个数保持一致</param>
        public I3SqlComplexParam(string complexCause, params object[] oValues)
        {
            this.complexCause = complexCause;
            opType = I3SqlOperateType.复杂条件;
            paramType = I3SqlParamType.Item;
            //this.sTableAlias = sTableAlias;
            this.multiValues = oValues;
            if (this.multiValues == null)
                this.multiValues = new object[0];
        }
        public override void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf)
        {
            object[] parmNames = new object[multiValues.Length];
            for (int i = 0; i < parmNames.Length; i++)
            {
                parmNames[i] = ":W_MULTI_PARAM_" + sqlBuf.Length + "_" + i;
            }
            sqlBuf.Append(string.Format(complexCause, parmNames));
            for (int i = 0; i < parmNames.Length; i++)
            {
                IDbDataParameter oParam = BuildParam((string)parmNames[i], multiValues[i]);
                aryParams.Add(oParam);
            }
        }
    }
}
