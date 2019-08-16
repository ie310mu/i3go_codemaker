using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// Json方法特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class JsonMethodAttribute : Attribute
    {
        private string name;
        private string description;

        /// <summary>
        /// 初始化一个JsonMethodAttribute的新实例。
        /// </summary>
        public JsonMethodAttribute()
        {
        }
        /// <summary>
        /// 初始化一个JsonMethodAttribute的新实例。
        /// </summary>
        /// <param name="description">方法描述</param>
        public JsonMethodAttribute(string description)
        {
            this.description = description;
        }
        /// <summary>
        /// 获取或设置方法名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// 获取或设置方法描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
