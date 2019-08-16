using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using IE310.Table.Column;

namespace IE310.Table.Design
{
    public class NumberConvert : DoubleConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            object result = base.ConvertFrom(context, culture, value);
            if (result is double)
            {
                if (context.Instance == null || context.Instance.GetType() != context.PropertyDescriptor.ComponentType)
                {
                    throw new Exception("the NumberConvert is only work for single object");
                }
                object source = context.Instance;
                PropertyInfo property = context.PropertyDescriptor.ComponentType.GetProperty(context.PropertyDescriptor.Name);
                if (property != null)
                {
                    object propertyValue = property.GetValue(source, null);
                    if (propertyValue != null)
                    {
                        Type propertyType = propertyValue.GetType();
                        NumberColumnType numberColumnType = NumberColumnTypeHelper.GetNumberColumnType(propertyType);
                        switch (numberColumnType)
                        {
                            case NumberColumnType.SBYTE:
                                return Convert.ToSByte((double)result);
                            case NumberColumnType.BYTE:
                                return Convert.ToByte((double)result);
                            case NumberColumnType.SHORT:
                                return Convert.ToInt16((double)result);
                            case NumberColumnType.USHORT:
                                return Convert.ToUInt16((double)result);
                            case NumberColumnType.INT:
                                return Convert.ToInt32((double)result);
                            case NumberColumnType.UINT:
                                return Convert.ToUInt32((double)result);
                            case NumberColumnType.LONG:
                                return Convert.ToInt64((double)result);
                            case NumberColumnType.ULONG:
                                return Convert.ToUInt64((double)result);
                            case NumberColumnType.FLOAT:
                                return Convert.ToSingle((double)result);
                            case NumberColumnType.DOUBLE:
                                return (double)result;
                            case NumberColumnType.DECIMAL:
                                return Convert.ToDecimal((double)result);
                            default:
                                return Convert.ToDecimal((double)result);
                        }
                    }
                }
            }
            return result;
        }
    }
}
