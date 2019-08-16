/*                     FileTypeReg.IEFS_FileTypeRegister
 * 
 * 
 *          类型: 类
 * 
 *          说明: 注册文件类型
 * 
 *      使用方法: 1.生成FileTypeRegInfo的对象，然后调用FileTypeRegister中的静态方法RegisterFileType
 *                  DelFileTypeReg  GetFileTypeRegInfo  UpdateFileTypeRegInfo  FileTypeRegistered
 *                2.文件类型表示为   类似于 .xlsm ，大小写都可以
 * 
 *      注意事项: 
 * 
 *      修改记录: 1.
 *  
 *                                                  Created by ie
 *                                                            2008-03-28      
 * 
 * */




using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace IE310.Core.Utils
{
    public class I3FileTypeRegInfo
    {
        /**/
        /// <summary>
        /// 目标类型文件的扩展名  类似于".xcf"
        /// </summary>
        public string ExtendName;

        /**/
        /// <summary>
        /// 目标文件类型说明  类似于"XCodeFactory项目文件"
        /// </summary>
        public string Description;

        /**/
        /// <summary>
        /// 目标类型文件关联的图标
        /// 可为ico文件的完整路径，也可类似于：  "D:\test\aa.exe 1"
        /// </summary>
        public string IcoPath;

        /**/
        /// <summary>
        /// 打开目标类型文件的应用程序的完整路径
        /// </summary>
        public string ExePath;

        public I3FileTypeRegInfo()
        {
        }

        public I3FileTypeRegInfo(string extendName)
        {
            this.ExtendName = extendName;
        }
    }





    public static class I3FileTypeRegisterUtil
    {
        #region RegisterFileType
        /// <summary>
        /// 注册文件类型
        /// 
        /// 错误处理：无
        /// 
        /// </summary>        
        public static void RegisterFileType(I3FileTypeRegInfo regInfo)
        {
            if (FileTypeRegistered(regInfo.ExtendName))
            {
                return;
            }

            //xcf_FileType            
            string relationName = I3StringUtil.SubString(regInfo.ExtendName, 1, regInfo.ExtendName.Length - 1).ToUpper() + "_FileType";

            //指定.xcf文件的关联信息在 xcf_FileType中
            RegistryKey fileTypeKey = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);//创建项.xcf
            fileTypeKey.SetValue("", relationName);//在.xcf 中增加一个默认值为  xcf_FileType
            fileTypeKey.Close();

            RegistryKey relationKey = Registry.ClassesRoot.CreateSubKey(relationName);//创建项xcf_FileType
            relationKey.SetValue("", regInfo.Description);//写入默认值 文件类型说明

            RegistryKey iconKey = relationKey.CreateSubKey("DefaultIcon");//添加项，图标路径
            iconKey.SetValue("", regInfo.IcoPath);

            RegistryKey shellKey = relationKey.CreateSubKey("Shell");
            RegistryKey openKey = shellKey.CreateSubKey("Open");
            RegistryKey commandKey = openKey.CreateSubKey("Command");
            commandKey.SetValue("", regInfo.ExePath + " \"%1\"");   //让应用程序知道打开了哪个文件     

            relationKey.Close();
        }

        /// <summary>
        /// 删除文件类型的注册 
        /// 
        /// 错误处理：无
        /// 
        /// </summary>
        /// <param name="extendName"></param>
        public static void DelFileTypeReg(string extendName)
        {
            if (!FileTypeRegistered(extendName))
            {
                return;
            }

            RegistryKey extendKey = Registry.ClassesRoot.OpenSubKey(extendName);
            string relationName = extendKey.GetValue("").ToString();

            RegistryKey relationKey = Registry.ClassesRoot;
            relationKey.DeleteSubKeyTree(extendName);
            relationKey.DeleteSubKeyTree(relationName);
        }


        /// <summary>
        /// 获取指定文件类型关联信息
        /// 类似于 .xlsm ，大小写都可以
        /// 错误处理：无
        /// 注意：返回的ExePath是两头带"的，用File.Exists来判断时会找不到
        /// </summary>        
        public static I3FileTypeRegInfo GetFileTypeRegInfo(string extendName)
        {
            if (!FileTypeRegistered(extendName))
            {
                return null;
            }

            I3FileTypeRegInfo regInfo = new I3FileTypeRegInfo(extendName);
            RegistryKey extendKey = Registry.ClassesRoot.OpenSubKey(extendName);
            string relationName = extendKey.GetValue("").ToString();

            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName);
            regInfo.Description = relationKey.GetValue("").ToString();

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon");
            regInfo.IcoPath = iconKey.GetValue("").ToString();

            RegistryKey shellKey = relationKey.OpenSubKey("Shell");
            RegistryKey openKey = shellKey.OpenSubKey("Open");
            RegistryKey commandKey = openKey.OpenSubKey("Command");
            string temp = commandKey.GetValue("").ToString();
            regInfo.ExePath = I3StringUtil.SubString(temp, 0, temp.Length - 3);

            return regInfo;
        }


        /// <summary>
        /// 更新指定文件类型关联信息
        /// 
        /// 错误处理：无
        /// 
        /// </summary>    
        public static bool UpdateFileTypeRegInfo(I3FileTypeRegInfo regInfo)
        {
            if (!FileTypeRegistered(regInfo.ExtendName))
            {
                return false;
            }


            string extendName = regInfo.ExtendName;
            RegistryKey extendKey = Registry.ClassesRoot.OpenSubKey(extendName);
            string relationName = extendKey.GetValue("").ToString();

            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName, true);
            relationKey.SetValue("", regInfo.Description);

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon", true);
            iconKey.SetValue("", regInfo.IcoPath);

            RegistryKey shellKey = relationKey.OpenSubKey("Shell");
            RegistryKey openKey = shellKey.OpenSubKey("Open");
            RegistryKey commandKey = openKey.OpenSubKey("Command", true);
            commandKey.SetValue("", regInfo.ExePath + " \"%1\"");

            relationKey.Close();

            return true;
        }

        
        /// <summary>
        /// 获取指定文件类型是否已经注册
        /// 
        /// 错误处理：无
        /// 
        /// </summary>        
        public static bool FileTypeRegistered(string extendName)
        {
            RegistryKey softwareKey = Registry.ClassesRoot.OpenSubKey(extendName);
            if (softwareKey != null)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
