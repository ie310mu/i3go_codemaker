using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Json
{
    /// <summary>
    /// NewtonJsonConverter
    /// </summary>
    public sealed class I3NewtonJsonConverter : II3JsonConverter
    {
        #region IJsonConverter 成员

        public string ToJson(object obj)
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, iso);
        }

        public object FromJson(string json, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
        }

        #endregion
    }
}
