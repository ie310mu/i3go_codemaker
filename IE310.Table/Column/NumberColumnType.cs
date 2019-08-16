using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Table.Column
{
    public enum NumberColumnType
    {
        SBYTE = 1,
        BYTE = 2,
        SHORT = 3,
        USHORT = 4,
        INT = 5, 
        UINT = 6,
        LONG = 7,
        ULONG = 8,
        FLOAT = 9,
        DOUBLE = 10,
        DECIMAL = 11,
    }

    public static class NumberColumnTypeHelper
    {
        public static NumberColumnType GetNumberColumnType(Type type)
        {
            if (type == typeof(sbyte))
            {
                return NumberColumnType.SBYTE;
            }
            else if (type == typeof(byte))
            {
                return NumberColumnType.BYTE;
            }
            else if (type == typeof(short))
            {
                return NumberColumnType.SHORT;
            }
            else if (type == typeof(ushort))
            {
                return NumberColumnType.USHORT;
            }
            else if (type == typeof(int))
            {
                return NumberColumnType.INT;
            }
            else if (type == typeof(uint))
            {
                return NumberColumnType.UINT;
            }
            else if (type == typeof(long))
            {
                return NumberColumnType.LONG;
            }
            else if (type == typeof(ulong))
            {
                return NumberColumnType.ULONG;
            }
            else if (type == typeof(float))
            {
                return NumberColumnType.FLOAT;
            }
            else if (type == typeof(double))
            {
                return NumberColumnType.DOUBLE;
            }
            else if (type == typeof(decimal))
            {
                return NumberColumnType.DECIMAL;
            }
            else
            {
                return (NumberColumnType)(-1);
            }
        }
    }
}
