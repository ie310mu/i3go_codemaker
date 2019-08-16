using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.IO;
using IE310.Core.Json;
using IE310.Core.Utils;

namespace IE310.Core.LocalSetting
{
    public partial class LocalSettingManager : Component
    {
        public LocalSettingManager()
        {
            InitializeComponent();
        }

        public LocalSettingManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private string fileName;
        private Type type;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        public void Init(string fileName, Type type)
        {
            this.fileName = fileName;
            I3MsgInfo msg = I3DirectoryUtil.CreateDirectoryByFileName(fileName);
            if (!msg.State)
            {
                throw msg.ExpMsg == null ? new Exception(msg.Message) : msg.ExpMsg;
            }

            this.type = type;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        public void Save(object item)
        {
            Save(item, fileName);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        public void Save(object item, string file)
        {
            string json = I3JsonConvert.ToJson(item);
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(json);
                fs.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        public object Read()
        {
            return Read(fileName);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        public object Read(string file)
        {
            if (!File.Exists(file))
            {
                return Activator.CreateInstance(type);
            }

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                string json = Encoding.UTF8.GetString(data);
                try
                {
                    return I3JsonConvert.FromJson(json, type);
                }
                catch
                {
                    return Activator.CreateInstance(type);
                }
            }
        }
    }
}
