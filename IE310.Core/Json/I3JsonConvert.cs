using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Json
{
    /// <summary>
    /// Jsonת��
    /// </summary>
    public static class I3JsonConvert
    {
        private static II3JsonConverter jsonConverter = new I3NewtonJsonConverter();

        /// <summary>
        /// ��ȡ������ת����
        /// </summary>
        public static II3JsonConverter JsonConverter
        {
            get { return I3JsonConvert.jsonConverter; }
            set { I3JsonConvert.jsonConverter = value; }
        }
        
        /// <summary>
        /// ������ת��ΪJson
        /// </summary>
        /// <param name="obj">����</param>
        /// <returns>Json</returns>
        public static string ToJson(object obj)
        {
            return jsonConverter.ToJson(obj);
        }
        /// <summary>
        /// ͨ��Json��������
        /// </summary>
        /// <param name="json">Json</param>
        /// <param name="type">����</param>
        /// <returns>����</returns>
        public static object FromJson(string json,Type type)
        {
            return jsonConverter.FromJson(json, type);
        }
        /// <summary>
        /// ͨ��Json��������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="json">Json</param>
        /// <returns>����</returns>
        public static T FromJson<T>(string json)
        {
            return (T)jsonConverter.FromJson(json, typeof(T));
        }
    }
}
