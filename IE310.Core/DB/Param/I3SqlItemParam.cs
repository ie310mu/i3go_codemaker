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
		/// 构造Sql项目,表别名，默认为 t,匹配方式为 "="
		/// </summary>
		/// <param name="sColName">字段名称</param>
		/// <param name="oValue">比较值，根据比较值的类型来决定数据类型，如：时间必须为DateTime类型，数值必须为Int或decimal类型
		/// ，如果要表示为空，请使用I3SqlOperateType.为空 来表示</param>
		public I3SqlItemParam(string sColName,object oValue):
			this("t",sColName,I3SqlOperateType.等于,oValue)
		{
		}
		/// <summary>
		/// 构造Sql项目,表别名，默认为 t
		/// </summary>
		/// <param name="sColName">字段名称</param>
		/// <param name="type">比较类型</param>
		/// <param name="oValue">比较值，根据比较值的类型来决定数据类型，如：时间必须为DateTime类型，数值必须为Int或decimal类型
		/// ，如果要表示为空，请使用I3SqlOperateType.为空 来表示</param>
		public I3SqlItemParam(string sColName,I3SqlOperateType type,object oValue):
			this("t",sColName,type,oValue)
		{
		}
		/// <summary>
		/// 构造Sql项目参数，
		/// </summary>
		/// <param name="tableAlias">表别名</param>
		/// <param name="sColName">字段名称</param>
		/// <param name="type">比较类型</param>
		/// <param name="oValue">比较值，根据比较值的类型来决定数据类型，如：时间必须为DateTime类型，数值必须为Int或decimal类型
		/// ，如果要表示为空，请使用I3SqlOperateType.为空 来表示</param>
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
				case I3SqlOperateType.等于:return " = ";
				case I3SqlOperateType.小于:return " < ";
				case I3SqlOperateType.小于等于:return " <= ";
				case I3SqlOperateType.大于:return " > ";
				case I3SqlOperateType.大于等于:return " >= ";
				case I3SqlOperateType.不等于:return " <> ";
				case I3SqlOperateType.前匹配 :return " Like ";
				case I3SqlOperateType.全匹配:return " Like ";
				case I3SqlOperateType.后匹配 :return " Like ";
				case I3SqlOperateType.不匹配:return " Not Like ";
				case I3SqlOperateType.为空:return " Is Null";
				case I3SqlOperateType.不为空:return " Is Not Null";
				default:
					return "";
			}
		}
	 

		/// <summary>
		/// 内部处理方法，递归调用，生成SQL及SQL参数
		/// </summary>
		/// <param name="aryParams">SQL参数</param>
		/// <param name="sqlBuf">SQL语句</param>
        public override void ToSqlString(ArrayList aryParams, StringBuilder sqlBuf)
        {
           if (this.opType != I3SqlOperateType.为空 &&
                this.opType != I3SqlOperateType.不为空)
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
                sqlBuf.Append(!string.IsNullOrEmpty(this.sTableAlias) ? this.sTableAlias + "." : " ").Append(this.sColName).Append(this.opType == I3SqlOperateType.为空 ? " Is Null" : " Is Not Null");
            }
        }
        
    }
}
