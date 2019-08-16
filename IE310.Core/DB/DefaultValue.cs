using System;
using System.Collections.Generic;

using System.Text;

namespace IE310.Core.DB
{
    public class DefaultValue
    {
        public DefaultValue()
        {
        }

        public DefaultValue(string defaultString)
        {
            this.defaultString = defaultString;
        }

        private string defaultString = "";

        public string DefaultString
        {
            get
            {
                return defaultString;
            }
            set
            {
                defaultString = value;
            }
        }

        private int defaultInt = 0;

        public int DefaultInt
        {
            get
            {
                return defaultInt;
            }
            set
            {
                defaultInt = value;
            }
        }

        private float defaultFloat = 0;

        public float DefaultFloat
        {
            get
            {
                return defaultFloat;
            }
            set
            {
                defaultFloat = value;
            }
        }

        private double defaultDouble = 0;

        public double DefaultDouble
        {
            get
            {
                return defaultDouble;
            }
            set
            {
                defaultDouble = value;
            }
        }

        public object CheckValue(object value, Type type)
        {
            if (type == typeof(string))//空字符串替换
            {
                if (value == DBNull.Value || value == null || /*string.IsNullOrEmpty(value.ToString().Trim())*/  string.IsNullOrEmpty(value.ToString())
                    || "NaN.undefined".Equals(value.ToString()) || "NaN".Equals(value.ToString()) || "undefined".Equals(value.ToString()))
                {
                    value = this.defaultString;
                }
            }
            else if (type == typeof(int))
            {
                if (value == DBNull.Value || value == null)
                {
                    value = this.defaultInt;
                }
            }
            else if (type == typeof(float))
            {
                if (value == DBNull.Value || value == null)
                {
                    value = this.defaultFloat;
                }
            }
            else if (type == typeof(double))
            {
                if (value == DBNull.Value || value == null)
                {
                    value = this.defaultDouble;
                }
            }
            else if (type == typeof(DateTime))
            {
                if (value == DBNull.Value || value == null || (DateTime)value == DateTime.MinValue || (DateTime)value == DateTime.MaxValue)
                {
                    value = DBNull.Value;
                }
            }


            return value;
        }
    }
}
