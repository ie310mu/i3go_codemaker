using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace IE310.Core.DB
{
    /// <summary>
    /// ���ӵ�����
    /// </summary>
    [Serializable]
    public class I3SqlComplexParam : I3SqlParam
    {
        private string complexCause;
        private object[] multiValues;
        /// <summary>
        /// ���������������ĸ��ӵĲ������
        /// </summary>
        /// <param name="complexCause">�ӣѣ��������������ʹ��{0},{1}</param>
        /// <param name="oValues">����ֵ���飬�����������������еĲ�����������һ��</param>
        public I3SqlComplexParam(string complexCause, params object[] oValues)
        {
            this.complexCause = complexCause;
            opType = I3SqlOperateType.��������;
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
