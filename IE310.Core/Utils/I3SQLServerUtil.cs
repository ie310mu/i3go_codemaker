using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Threading;
using IE310.Core.Utils;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IE310.Core.Utils
{
    public static class I3SQLServerUtil
    {
        /// <summary>
        /// 获取服务器上的时间
        /// 
        /// 错误处理：MessageBox.Show()
        /// 
        /// </summary>
        public static DateTime GetSQLServerDateTime(I3Data data)
        {
            using (DataTable dataTable = new DataTable("NowTime"))
            {
                try
                {
                    string sqlStr = "  select getdate() as snow ";
                    data.FillTable(dataTable, true, sqlStr, null, null);

                    return (DateTime)dataTable.Rows[0]["snow"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DateTime.Now;
                }
                finally
                {
                    data.DisposeDataTable(dataTable);
                }
            }
        }


        /// <summary>
        /// 根据UDL文件中的连接信息和备份文件名，备份数据库
        /// </summary>
        /// <param name="udlFileName"></param>
        /// <param name="bakFileName"></param>
        /// <param name="restoreInfo"></param>
        /// <returns></returns>
        public static bool BakupSQLServerDataBase(string connectionString, string bakFileName, I3SQLServerRestoreInfo restoreInfo)
        {
            #region 从udl文件获取连接字符串
            //restoreInfo("一、连接字符串：" + connectionString, false, MessageBoxIcon.Warning);
            restoreInfo("一、获取连接字符串", false, MessageBoxIcon.Warning);
            //MessageBox.Show(connectionString);
            #endregion


            #region 获取服务器名，登录方式，数据库名等信息
            restoreInfo("", false, MessageBoxIcon.Warning);
            restoreInfo("二、解析服务器名，数据库名", false, MessageBoxIcon.Warning);
            //OleDbConnectionStringBuilder ob;
            SqlConnectionStringBuilder sb;
            try
            {
                sb = new SqlConnectionStringBuilder(connectionString);
            }
            catch (Exception ex)
            {
                restoreInfo("数据库连接字符串解析失败，错误消息：\r\n" + ex.Message, true, MessageBoxIcon.Error);
                return false;
            }
            string serverName = sb.DataSource.ToUpper();
            string dataBasename = sb["Initial Catalog"].ToString();
            restoreInfo("即将在服务器" + serverName + "上备份数据库" + dataBasename, false, MessageBoxIcon.Warning);
            #endregion
            
                I3Data masterCon=null;
            try
            {
                #region 登录到master数据库，并检查数据库是否已经存在
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("三、连接数据库引擎", false, MessageBoxIcon.Warning);
                sb["Initial Catalog"] = "master";
                masterCon = I3Data.CreateDataSql(sb.ConnectionString);
                if (!masterCon.Active)
                {
                    restoreInfo("无法启动数据库引擎，错误消息：\r\n" + masterCon.LastErrorInfo, true, MessageBoxIcon.Error);
                    return false;

                    #region 检查是否本机，如果不是本机，则直接返回false
                    //if (!I3PCUtil.CheckServerNameIsLocal(serverName))
                    //{
                    //    restoreInfo("连接数据库引擎失败，但连接字符串中指定的服务器" + serverName + "不是本机，因此无法启动数据库引擎！", true, MessageBoxIcon.Error);
                    //    return false;
                    //}
                    #endregion

                    #region 启动数据库引擎 并重新连接
                    //if (MessageBox.Show("数据库引擎连接失败，需要启动数据库引擎吗？", "连接数据库引擎", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //{
                    //    //IEFS_PC.RunService("MSSQLServer");
                    //    //为了有提示界面，通过调用SCM.exe来启动服务
                    //    I3MsgInfo msg = I3PCUtil.CreateAndWaitProcess("SCM.exe", " -action 1 -service MSSQLServer -SvcStartType 2 ", false);
                    //    if (msg.State)
                    //    {
                    //        Thread.Sleep(5000);
                    //        masterCon = I3Data.CreateDataOle(sb.ConnectionString);
                    //        if (!masterCon.Active)
                    //        {
                    //            restoreInfo("启动了数据库引擎，但仍然连接失败！请联系技术人员进行检查。", true, MessageBoxIcon.Error);
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        restoreInfo("无法启动数据库引擎，错误消息：\r\n" + msg.Message, true, MessageBoxIcon.Error);
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    restoreInfo("数据库引擎连接失败，无法创建默认数据库", true, MessageBoxIcon.Error);
                    //    return false;
                    //}
                    #endregion
                }
                restoreInfo("数据库引擎连接OK", false, MessageBoxIcon.Warning);
                #endregion


                #region 检查备份文件
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("四、检查数据库备份文件", false, MessageBoxIcon.Warning);
                if (File.Exists(bakFileName))
                {
                    if (MessageBox.Show("数据库备份文件已经存在，是否覆盖？", "覆盖备份文件", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (!I3FileUtil.CheckFileNotExists(bakFileName))
                        {
                            restoreInfo("数据库备份文件已经存在，且删除失败，备份操作中止！", true, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        restoreInfo("数据库备份文件已经存在，用户放弃覆盖，备份操作中止！", true, MessageBoxIcon.Error);
                        return false;
                    }
                }
                restoreInfo("数据库备份文件检查成功", false, MessageBoxIcon.Warning);
                #endregion


                #region 备份
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("五、开始备份数据库，请耐心等待", false, MessageBoxIcon.Warning);
                string restoreStr = " BACKUP DATABASE " + dataBasename.Trim()
                                  + " To Disk = " + I3DBUtil.QuotedStr(bakFileName);
                //MessageBox.Show(restoreStr);
                SqlCommand com = new SqlCommand(restoreStr, (SqlConnection)masterCon.GetConnection());
                try
                {
                    com.CommandTimeout = 600;
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    restoreInfo("数据库" + dataBasename + "备份不成功！错误消息：\r\n" + ex.Message, true, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    com.Dispose();
                }


                restoreInfo("数据库备份成功！备份文件已经保存到：\r\n" + bakFileName, true, MessageBoxIcon.Warning);
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("", false, MessageBoxIcon.Warning);
                #endregion

                return true;
            }
            finally
            {
                if (masterCon != null && masterCon.Active)
                {
                    masterCon.Close();
                }
            }
        }

        /// <summary>
        /// 根据UDL文件中的连接信息和备份文件名，创建或者恢复数据库
        /// runMode:创建，恢复 
        /// </summary>
        /// <param name="udlFileName"></param>
        /// <returns></returns>
        public static bool RestoreSQLServerDataBase(string connectionString, string bakFileName, I3SQLServerRestoreMode restoreMode, I3SQLServerRestoreInfo restoreInfo, string dataFilePath)
        {
            #region 操作关键字
            string runMode = "";
            if (restoreMode == I3SQLServerRestoreMode.rmCreate)
            {
                runMode = "创建";
            }
            else
            {
                runMode = "恢复";
            }
            #endregion


            #region 从udl文件获取连接字符串
            //restoreInfo("一、连接字符串：" + connectionString, false, MessageBoxIcon.Warning);
            restoreInfo("一、获取连接字符串", false, MessageBoxIcon.Warning);
            //MessageBox.Show(connectionString);
            #endregion


            #region 获取服务器名，登录方式，数据库名等信息
            restoreInfo("", false, MessageBoxIcon.Warning);
            restoreInfo("二、解析服务器名，数据库名", false, MessageBoxIcon.Warning);
            //OleDbConnectionStringBuilder ob;
            SqlConnectionStringBuilder sb;
            try
            {
                sb = new SqlConnectionStringBuilder(connectionString);
            }
            catch (Exception ex)
            {
                restoreInfo("数据库连接字符串解析失败，错误消息：\r\n" + ex.Message, true, MessageBoxIcon.Error);
                return false;
            }
            string serverName = sb.DataSource.ToUpper();
            string dataBasename = sb["Initial Catalog"].ToString();
            restoreInfo("即将在服务器" + serverName + "上" + runMode + "数据库" + dataBasename, false, MessageBoxIcon.Warning);
            #endregion


            I3Data masterCon = null;
            try
            {
                #region 登录到master数据库，并检查数据库是否已经存在
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("三、连接数据库引擎", false, MessageBoxIcon.Warning);
                sb["Initial Catalog"] = "master";
                masterCon = I3Data.CreateDataSql(sb.ConnectionString);
                if (!masterCon.Active)
                {
                    restoreInfo("无法启动数据库引擎，错误消息：\r\n" + masterCon.LastErrorInfo, true, MessageBoxIcon.Error);
                    return false;

                    #region 检查是否本机，如果不是本机，则直接返回false
                    //if (!I3PCUtil.CheckServerNameIsLocal(serverName))
                    //{
                    //    restoreInfo("连接数据库引擎失败，但连接字符串中指定的服务器" + serverName + "不是本机，因此无法启动数据库引擎！", true, MessageBoxIcon.Error);
                    //    return false;
                    //}
                    #endregion

                    #region 启动数据库引擎 并重新连接
                    //if (MessageBox.Show("数据库引擎连接失败，需要启动数据库引擎吗？", "连接数据库引擎", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //{
                    //    //IEFS_PC.RunService("MSSQLServer");
                    //    //为了有提示界面，通过调用SCM.exe来启动服务
                    //    I3MsgInfo msg = I3PCUtil.CreateAndWaitProcess("SCM.exe", " -action 1 -service MSSQLServer -SvcStartType 2 ", false);
                    //    if (msg.State)
                    //    {
                    //        Thread.Sleep(5000);
                    //        masterCon = I3Data.CreateDataOle(sb.ConnectionString);
                    //        if (!masterCon.Active)
                    //        {
                    //            restoreInfo("启动了数据库引擎，但仍然连接失败！请联系技术人员进行检查。", true, MessageBoxIcon.Error);
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        restoreInfo("无法启动数据库引擎，错误消息：\r\n" + msg.Message, true, MessageBoxIcon.Error);
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    restoreInfo("数据库引擎连接失败，无法创建默认数据库", true, MessageBoxIcon.Error);
                    //    return false;
                    //}
                    #endregion
                }
                restoreInfo("数据库引擎连接OK", false, MessageBoxIcon.Warning);
                #endregion


                #region 获取系统目录信息
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("四、获取目录信息", false, MessageBoxIcon.Warning);
                string dataRootPath = "";
                if (!string.IsNullOrEmpty(dataFilePath))
                {
                    dataRootPath = dataFilePath;
                    I3DirectoryUtil.CreateDirctory(dataRootPath);
                }
                else
                {
                    using (DataTable tmp = new DataTable("sysdatabases"))
                    {
                        string sqlStr = "select * from sysdatabases where name = 'master'";
                        I3MsgInfo msg = masterCon.FillTable(tmp, true, sqlStr, null, null);
                        if (!msg.State)
                        {
                            restoreInfo("取不到系统目录，" + runMode + "操作中止！错误消息：\r\n" + msg.Message, true, MessageBoxIcon.Error);
                            return false;
                        }
                        try
                        {
                            if (tmp.Rows.Count == 0)
                            {
                                restoreInfo("取不到系统目录，" + runMode + "操作中止！", true, MessageBoxIcon.Error);
                                return false;
                            }
                            dataRootPath = Path.GetDirectoryName(tmp.Rows[0]["filename"].ToString());
                        }
                        finally
                        {
                            masterCon.DisposeDataTable(tmp);
                        }
                        restoreInfo("系统目录获取成功：" + dataRootPath, false, MessageBoxIcon.Warning);
                    }
                }
                #endregion


                #region 校验备份文件
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("五、校验数据库备份文件", false, MessageBoxIcon.Warning);
                string moveStr = "";
                if (!File.Exists(bakFileName))
                {
                    restoreInfo("数据库备份文件不存在！\r\n文件路径：\r\n" + bakFileName, true, MessageBoxIcon.Error);
                    return false;
                }
                List<string> toDeleteFile = new List<string>();
                using (DataTable tmp = new DataTable("FILELISTONLY"))
                {
                    string sqlStr = " RESTORE FILELISTONLY from disk=" + I3StringUtil.QuotedStr(bakFileName);
                    I3MsgInfo msg = masterCon.FillTable(tmp, true, sqlStr, null, null);
                    if (!msg.State)
                    {
                        restoreInfo("该备份文件不是数据库备份文件!\r\n文件路径：\r\n" + bakFileName + "\r\n错误消息：\r\n" + msg.Message, true, MessageBoxIcon.Error);
                        return false;
                    }
                    try
                    {
                        foreach (DataRow row in tmp.Rows)
                        {
                            if (row["Type"].ToString() == "D")
                            {
                                string fileName = Path.Combine(dataRootPath, dataBasename.Trim() + "_Dat.MDF");
                                toDeleteFile.Add(fileName);
                                moveStr = moveStr + " Move " + I3DBUtil.QuotedStr(row["logicalname"].ToString())
                                        + " To " + I3DBUtil.QuotedStr(fileName) + ",";
                            }
                            else
                            {
                                string fileName = Path.Combine(dataRootPath, dataBasename.Trim() + "_Log.LDF");
                                toDeleteFile.Add(fileName);
                                moveStr = moveStr + " Move " + I3DBUtil.QuotedStr(row["logicalname"].ToString())
                                        + " To " + I3DBUtil.QuotedStr(fileName) + ",";
                            }
                        }
                        moveStr = I3StringUtil.SubString(moveStr, 0, moveStr.Length - 1);
                        //MessageBox.Show(moveStr);
                    }
                    finally
                    {
                        masterCon.DisposeDataTable(tmp);
                    }
                }

                restoreInfo("数据库备份文件校验成功", false, MessageBoxIcon.Warning);
                #endregion

                #region 删除现有数据库
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("六、检查数据库" + dataBasename + "是否已经存在", false, MessageBoxIcon.Warning);
                bool has;
                #region 获取数据库是否已经存在
                using (DataTable tmp = new DataTable("sysdatabases"))
                {
                    string sqlStr = " select * from sysdatabases where name = " + I3DBUtil.QuotedStr(dataBasename);
                    I3MsgInfo msg = masterCon.FillTable(tmp, true, sqlStr, null, null);
                    if (!msg.State)
                    {
                        restoreInfo("出现错误，操作中止！错误消息：\r\n" + msg.Message, true, MessageBoxIcon.Error);
                        return false;
                    }
                    try
                    {
                        has = tmp.Rows.Count > 0;
                    }
                    finally
                    {
                        masterCon.DisposeDataTable(tmp);
                    }
                }
                #endregion
                if (has)
                {
                    if (restoreMode == I3SQLServerRestoreMode.rmCreate)
                    {
                        restoreInfo("数据库" + dataBasename + "已经存在，不能创建数据库！", true, MessageBoxIcon.Error);
                        return false;
                    }

                    #region 删除数据库
                    if (MessageBox.Show("数据库" + dataBasename + "已经存在，是否删除？\r\n警告：进行删除操作前，请确认之前已经备份数据库，否则所有数据将丢失！", "删除数据库", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        restoreInfo("开始删除数据库" + dataBasename, false, MessageBoxIcon.Warning);
                        string sqlStr = " DROP DATABASE " + dataBasename;
                        I3MsgInfo msg = masterCon.Execute(sqlStr, null);
                        if (msg.State)
                        {
                            restoreInfo("数据库" + dataBasename + "删除成功", false, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            restoreInfo("数据库" + dataBasename + "删除失败，错误消息：" + msg.Message, true, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        restoreInfo("数据库" + dataBasename + "已经存在，用户放弃删除，不能" + runMode + "数据库！", true, MessageBoxIcon.Error);
                        return false;
                    }
                    #endregion

                }
                #region 再次删除数据库文件 注：确保清空文件
                foreach (string s in toDeleteFile)
                {
                    I3FileUtil.CheckFileNotExists(s);
                }
                #endregion
                restoreInfo("现有数据库删除成功，可以进行下一步操作", false, MessageBoxIcon.Warning);
                #endregion
                
                #region 恢复
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("七、开始" + runMode + "数据库，请耐心等待", false, MessageBoxIcon.Warning);
                string restoreStr = " RESTORE DATABASE " + dataBasename.Trim()
                                  + " From Disk = " + I3DBUtil.QuotedStr(bakFileName)
                                  + " with " + moveStr;
                //MessageBox.Show(restoreStr);
                SqlCommand com = new SqlCommand(restoreStr, (SqlConnection)masterCon.GetConnection());
                try
                {
                    com.CommandTimeout = 600;
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    restoreInfo("数据库" + dataBasename + runMode + "不成功！错误消息：\r\n" + ex.Message, true, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    com.Dispose();
                }


                restoreInfo("数据库" + runMode + "成功！", true, MessageBoxIcon.Warning);
                restoreInfo("", false, MessageBoxIcon.Warning);
                restoreInfo("", false, MessageBoxIcon.Warning);
                #endregion


                return true;
            }
            finally
            {
                if (masterCon != null && masterCon.Active)
                {
                    masterCon.Close();
                }
            }
        }

    }


    public enum I3SQLServerRestoreMode
    {
        rmCreate = 1,
        rmRestore = 2,
    }


    public delegate void I3SQLServerRestoreInfo(string aInfo, bool show, MessageBoxIcon icon);
}
