 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using IE310.Core.Utils;
using IE310.Core.IO.Compression;

namespace IE310.Core.Components
{
    public partial class I3Rar : Component
    {
        /// <summary>
        /// Compress.exe的全路径，必须与主程序在同一个目录！
        /// </summary>
        private string exeFileName = "";


        public I3Rar()
        {
            InitializeComponent();
            exeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Compress.exe");
        }

        private string passWord = "";
        /// <summary>
        /// 密码  由于使用命令行参数形式，密码不能包含特殊符号
        /// </summary>
        public string PassWord
        {
            get
            {
                return passWord;
            }
            set
            {
                passWord = value;
            }
        }

        /// <summary>
        /// 根据错误码返回错误消息
        /// </summary>
        /// <returns></returns>
        private string GetErrInfoByCode(int aCode)
        {
            switch (aCode)
            {
                case 1:
                    return "警告。发生非致命错误。";
                case 2:
                    return "发生致命错误。";
                case 3:
                    return "解压时发生 CRC 错误。";
                case 4:
                    return "尝试修改一个 锁定的压缩文件。";
                case 5:
                    return "写错误。";
                case 6:
                    return "文件打开错误。";
                case 7:
                    return "错误命令行选项。";
                case 8:
                    return "内存不足。";
                case 9:
                    return "文件创建错误。";
                case 256:
                    return "用户中断。";
                default:
                    return "未知错误信息。";
            }
        }

        /// <summary>
        /// 运行Compress.exe，传入aCmdLine作为命令行参数
        /// 错误处理：IEFS_Error.LastErrorMessage
        /// </summary>
        /// <param name="aCmdLine"></param>
        /// <returns></returns>
        private I3MsgInfo RunCmdLine(string aCmdLine)
        {
            using (Process rar = new Process())
            {
                rar.StartInfo.FileName = exeFileName;
                rar.StartInfo.Arguments = aCmdLine;
                rar.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                rar.Start();
                rar.WaitForExit();

                bool result = rar.ExitCode == 0;
                if (!result)
                {
                    string error = GetErrInfoByCode(rar.ExitCode);
                    return new I3MsgInfo(false, error);
                }

                rar.Close();

                return I3MsgInfo.Default;
            }
        }

        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        public I3MsgInfo CompressASingleFile(string sourceFileName, string destFileName)
        {
            #region 先删除目的文件
            if (!I3FileUtil.CheckFileNotExists(destFileName))
            {
                return new I3MsgInfo(false, "目标文件删除失败");
            }
            #endregion

            #region 生成命令行参数
            string cmdLine;
            cmdLine = " A -ep -Y ";
            if (!string.IsNullOrEmpty(passWord))
            {
                cmdLine = cmdLine + " -p" + I3StringUtil.AppendDoubleQuotes(passWord) + " ";
            }
            cmdLine = cmdLine + I3StringUtil.AppendDoubleQuotes(destFileName) + " " + I3StringUtil.AppendDoubleQuotes(sourceFileName);
            #endregion

            return RunCmdLine(cmdLine);
        }

        /// <summary>
        /// 压缩一个数据集到文件
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="destFileName"></param>
        /// <param name="cry"></param>
        /// <param name="cryKey"></param>
        /// <returns></returns>
        public I3MsgInfo CompressADataSet(DataSet dataSet, string destFileName, bool cry, string cryKey)
        {
            #region 初始化临时文件变量
            string tmpDir = I3DirectoryUtil.GetAppTmpTmpDir();
            string tmpDataSetFile = Path.Combine(tmpDir, "DataSet.tmp");
            string tmpRarFile = Path.Combine(tmpDir, I3DateTimeUtil.ConvertDateTimeToLongString(DateTime.Now) + ".tmp");
            if ((!I3DirectoryUtil.CreateDirctory(tmpDir).State) || (!I3FileUtil.CheckFileNotExists(tmpDataSetFile)) || (!I3FileUtil.CheckFileNotExists(tmpRarFile)))
            {
                return new I3MsgInfo(false, "");
            }
            #endregion

            try
            {
                #region 数据集保存到临时文件
                try
                {
                    //保存时要写入架构信息，否则字段类型有可能会更改，如Int32更改为String
                    dataSet.WriteXml(tmpDataSetFile, XmlWriteMode.WriteSchema);
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, "数据集保存到临时文件失败！文件名：" + tmpDataSetFile + "错误信息：" + ex.Message);
                }
                if (!File.Exists(tmpDataSetFile))
                {
                    return new I3MsgInfo(false, "数据集保存到临时文件失败！文件名：" + tmpDataSetFile);
                }
                #endregion

                #region 数据集临时文件压缩到临时压缩包
                I3MsgInfo msg=this.CompressASingleFile(tmpDataSetFile, tmpRarFile);
                if (!msg.State)
                {
                    return msg;
                }
                if (!File.Exists(tmpRarFile))
                {
                    return new I3MsgInfo(false, "数据集临时文件压缩失败！文件名：" + tmpDataSetFile);
                }
                #endregion

                #region 临时压缩包加密到目标文件
                if (cry)
                {
                    return I3RijnDaelCry.CryFile(tmpRarFile, destFileName, cryKey);
                }
                else
                {
                    return I3FileUtil.MoveFile(tmpRarFile, destFileName, true);
                }
                #endregion
            }
            finally
            {
                #region 删除临时文件
                I3DirectoryUtil.DeleteDirctory(tmpDir);
                #endregion
            }
        }

        /// <summary>
        /// 从一个被压缩的数据集文件中加载数据集
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="dataSet"></param>
        /// <param name="unCry"></param>
        /// <param name="unCryKey"></param>
        /// <returns></returns>
        public I3MsgInfo UnCompressADataSet(string sourceFileName, DataSet dataSet, bool unCry, string unCryKey)
        {
            #region 初始化临时文件变量
            string tmpDir = I3DirectoryUtil.GetAppTmpTmpDir();
            string tmpDataSetFile = Path.Combine(tmpDir, "DataSet.tmp");
            string tmpRarFile = Path.Combine(tmpDir, I3DateTimeUtil.ConvertDateTimeToLongString(DateTime.Now) + ".tmp");
            if ((!I3DirectoryUtil.CreateDirctory(tmpDir).State) || (!I3FileUtil.CheckFileNotExists(tmpDataSetFile)) || (!I3FileUtil.CheckFileNotExists(tmpRarFile)))
            {
                return new I3MsgInfo(false, "");
            }
            #endregion

            I3MsgInfo msg;
            try
            {
                #region 解密到临时压缩文件
                if (unCry)
                {
                    msg=I3RijnDaelCry.UnCryFile(sourceFileName, tmpRarFile, unCryKey);
                    if (!msg.State)
                    {
                        return msg;
                    }
                }
                else
                {
                    msg=I3FileUtil.MoveFile(sourceFileName, tmpRarFile, false);
                    if (!msg.State)
                    {
                        return msg;
                    }
                }
                if (!File.Exists(tmpRarFile))
                {
                    return new I3MsgInfo(false, "文件解密到临时压缩文件失败！文件名：" + tmpRarFile);
                }
                #endregion

                #region 解压缩到临时数据集文件
                msg = this.UnCompressASingleFile(tmpRarFile, "DataSet.tmp", tmpDataSetFile);
                if (!msg.State)
                {
                    return msg;
                }
                if (!File.Exists(tmpDataSetFile))
                {
                    return new I3MsgInfo(false, "临时压缩文件解压到临时数据集文件失败！文件名：" + tmpDataSetFile);
                }
                #endregion
                
                #region 从临时数据集文件加载
                try
                {
                    dataSet.ReadXml(tmpDataSetFile);
                    return I3MsgInfo.Default;
                }
                catch (Exception ex)
                {
                    return new I3MsgInfo(false, "从临时数据集文件加载数据集失败！错误信息：" + ex.Message, ex);
                }
                #endregion
            }
            finally
            {
                #region 删除临时文件
                I3DirectoryUtil.DeleteDirctory(tmpDir);
                #endregion
            }
        }


        /// <summary>
        /// 解压单个文件 到 指定文件
        /// 注意：单个文件的文件名，要指定在rar文件中的相对路径
        /// </summary>
        /// <param name="sourceRarFileName"></param>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        public I3MsgInfo UnCompressASingleFile(string sourceRarFileName, string sourceFileName, string destFileName)
        {
            #region 先删除目的文件
            if (!I3FileUtil.CheckFileNotExists(destFileName))
            {
                return new I3MsgInfo(false, "");
            }
            #endregion

            #region 获取临时目录与临时文件名
            string tmpDir = I3DirectoryUtil.GetAppTmpTmpDir();
            string tmpFileName = Path.Combine(tmpDir, Path.GetFileName(sourceFileName));
            #endregion

            I3MsgInfo msg=I3DirectoryUtil.CreateDirectoryByFileName(tmpFileName);
            #region 创建临时目录
            if (!msg.State)
            {
                return msg;
            }
            #endregion

            try
            {
                #region 生成命令行参数
                string cmdLine;
                cmdLine = " E -Y ";
                if (!string.IsNullOrEmpty(passWord))
                {
                    cmdLine = cmdLine + " -p" + I3StringUtil.AppendDoubleQuotes(passWord) + " ";
                }
                cmdLine = cmdLine + I3StringUtil.AppendDoubleQuotes(sourceRarFileName) + " "
                        + I3StringUtil.AppendDoubleQuotes(sourceFileName) + " "
                        + I3StringUtil.AppendDoubleQuotes(tmpDir);
                #endregion

                //执行
                msg=RunCmdLine(cmdLine);
                if (!msg.State)
                {
                    return msg;
                }

                //检查临时文件
                if (!File.Exists(tmpFileName))
                {
                    return new I3MsgInfo(false, "未知错误，临时文件未生成！文件名：" + tmpFileName);
                }

                //移动
                return I3FileUtil.MoveFile(tmpFileName, destFileName, true);
            }
            finally
            {
                I3DirectoryUtil.DeleteDirctory(tmpDir);
            }
        }

        /// <summary>
        /// 压缩整个目录，包含子目录和文件
        /// </summary>
        /// <param name="aDir"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        public I3MsgInfo CompressADir(string aDir, string destFileName)
        {
            #region 先删除目的文件
            if (!I3FileUtil.CheckFileNotExists(destFileName))
            {
                return new I3MsgInfo(false,"");
            }
            #endregion

            if (!string.IsNullOrEmpty(aDir))
            {
                aDir = I3DirectoryUtil.CheckDirctoryLastChar(aDir);
                aDir = aDir + "*";
            }
            string cmdLine = " a -ep1 -r -Y ";
            if (!string.IsNullOrEmpty(passWord))
            {
                cmdLine = cmdLine + " -p" + I3StringUtil.AppendDoubleQuotes(passWord) + " ";
            }
            cmdLine = cmdLine + I3StringUtil.AppendDoubleQuotes(destFileName) + " " + I3StringUtil.AppendDoubleQuotes(aDir);

            return RunCmdLine(cmdLine);
        }

        public I3MsgInfo UnAllCompressToADir(string sourceRarFileName, string destDir)
        {
            I3MsgInfo msg=I3DirectoryUtil.CreateDirctory(destDir);
            if (!msg.State)
            {
                return msg;
            }

            string cmdLine = " X -Y -C- ";
            if (!string.IsNullOrEmpty(passWord))
            {
                cmdLine = cmdLine + " -p" + I3StringUtil.AppendDoubleQuotes(passWord) + " ";
            }
            cmdLine = cmdLine + I3StringUtil.AppendDoubleQuotes(sourceRarFileName) + " " + I3StringUtil.AppendDoubleQuotes(destDir);

            return RunCmdLine(cmdLine);
        }
    }
}
