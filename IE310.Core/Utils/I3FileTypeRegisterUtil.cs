/*                     FileTypeReg.IEFS_FileTypeRegister
 * 
 * 
 *          ����: ��
 * 
 *          ˵��: ע���ļ�����
 * 
 *      ʹ�÷���: 1.����FileTypeRegInfo�Ķ���Ȼ�����FileTypeRegister�еľ�̬����RegisterFileType
 *                  DelFileTypeReg  GetFileTypeRegInfo  UpdateFileTypeRegInfo  FileTypeRegistered
 *                2.�ļ����ͱ�ʾΪ   ������ .xlsm ����Сд������
 * 
 *      ע������: 
 * 
 *      �޸ļ�¼: 1.
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
        /// Ŀ�������ļ�����չ��  ������".xcf"
        /// </summary>
        public string ExtendName;

        /**/
        /// <summary>
        /// Ŀ���ļ�����˵��  ������"XCodeFactory��Ŀ�ļ�"
        /// </summary>
        public string Description;

        /**/
        /// <summary>
        /// Ŀ�������ļ�������ͼ��
        /// ��Ϊico�ļ�������·����Ҳ�������ڣ�  "D:\test\aa.exe 1"
        /// </summary>
        public string IcoPath;

        /**/
        /// <summary>
        /// ��Ŀ�������ļ���Ӧ�ó��������·��
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
        /// ע���ļ�����
        /// 
        /// ��������
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

            //ָ��.xcf�ļ��Ĺ�����Ϣ�� xcf_FileType��
            RegistryKey fileTypeKey = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);//������.xcf
            fileTypeKey.SetValue("", relationName);//��.xcf ������һ��Ĭ��ֵΪ  xcf_FileType
            fileTypeKey.Close();

            RegistryKey relationKey = Registry.ClassesRoot.CreateSubKey(relationName);//������xcf_FileType
            relationKey.SetValue("", regInfo.Description);//д��Ĭ��ֵ �ļ�����˵��

            RegistryKey iconKey = relationKey.CreateSubKey("DefaultIcon");//����ͼ��·��
            iconKey.SetValue("", regInfo.IcoPath);

            RegistryKey shellKey = relationKey.CreateSubKey("Shell");
            RegistryKey openKey = shellKey.CreateSubKey("Open");
            RegistryKey commandKey = openKey.CreateSubKey("Command");
            commandKey.SetValue("", regInfo.ExePath + " \"%1\"");   //��Ӧ�ó���֪�������ĸ��ļ�     

            relationKey.Close();
        }

        /// <summary>
        /// ɾ���ļ����͵�ע�� 
        /// 
        /// ��������
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
        /// ��ȡָ���ļ����͹�����Ϣ
        /// ������ .xlsm ����Сд������
        /// ��������
        /// ע�⣺���ص�ExePath����ͷ��"�ģ���File.Exists���ж�ʱ���Ҳ���
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
        /// ����ָ���ļ����͹�����Ϣ
        /// 
        /// ��������
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
        /// ��ȡָ���ļ������Ƿ��Ѿ�ע��
        /// 
        /// ��������
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
