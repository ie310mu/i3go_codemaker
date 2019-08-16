/*                     ieIni.ieIni
 * 
 * 
 *          类型: 控件
 * 
 *          说明: 利用DataSet与Xml文件的交换功能，简单实现类似用Delphi中TIniFile的功能
 * 
 *      使用方法: 1.从工具箱中加到窗口中
 *                  或者:自定义变量，手动生成与释放
 *                2.设置FileName属性
 *                  将Active = true;
 * 
 *      注意事项: 1.由于利用了DataSet与Xml文件的交换功能，而Xml文件有其特殊的格式，因此生成的ini文件不建议手动修改
 *                  如果使用记事本修改，需要保存成Unicode格式
 *                2.当读取失败时，会尝试建立文件，如果文件仍未能建立，则Actieve属性返回false,
 *                 此时任何ini操作实际上都是失败的，读取返回的是默认值，写入并没有写入。
 * 
 *          附注: 从这里可以学习如何为控件增加事件
 * 
 *      修改记录: 1.在Dispose(bool disposing)中，释放托管资源DataSet ini
 *                  2008-04-16  ie
 *  
 *                                                  Created by ie
 *                                                            2008-01-01      
 * 
 * */


/*
 * FileName
ini文件名

Active
得到与设置与ini文件的连接状态


UP
UP表示每次写入数据时，是否要即时更新到ini文件
默认为即时更新
UP=true,数据即时更新
Up=false,数据不即时更新，需要使用Updata()来更新


GetBool     SetBool
GetFloat    SetFloat
GetInt      SetInt
GetString   SetString
GetTime     SetTime


Updata
更新数据以保存到磁盘文件中，一般在UP=false时才需要使用，多用于大量数据

两个事件:
IECT_IniGetValueEvent,IECT_IniSetValueEvent
GetValueEvent可以改变e.Value，
SetValueEvent中可以改变e.Big,e.Small,e.Value
另有两个属性CanOnGetValue,CanOnSetValue,标志是否可以响应上面的两个事件



*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IE310.Core.Utils;

namespace IE310.Core.Components
{
    public partial class I3Ini : Component
    {
        private DataSet ini = new DataSet("ini");
        private bool FActive = false;
        /// <remarks>
        /// FUP表示每次写入数据时，是否要即时更新到ini文件
        /// 默认为即时更新
        /// </remarks>
        private bool FUP = true;
        /// <summary>
        /// 用于保存ini文件名，内部使用
        /// </summary>
        private string FFileName;
        private bool FCanOnGetValue = false;
        private bool FCanOnSetValue = false;

        public event I3IniGetValueEvent GetValueEvent;

        public event I3IniSetValueEvent SetValueEvent;


        public I3Ini()
        {
            InitializeComponent();
            //this.Visible = false;
        }

        /// <summary>
        /// UP表示每次写入数据时，是否要即时更新到ini文件
        /// 默认为即时更新
        /// UP=true,数据即时更新
        /// Up=false,数据不即时更新，需要使用Updata()来更新
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        public bool UP
        {
            get
            {
                return FUP;
            }
            set
            {
                FUP = value;
            }
        }

        /// <summary>
        /// ini文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return FFileName;
            }
            set
            {
                if (!Active)
                {
                    FFileName = value;
                }
                else
                {
                    throw new Exception("已连接到ini文件，无法设置文件名!");
                }
            }
        }

        /// <summary>
        /// 得到与设置与ini文件的连接状态
        /// 
        /// 错误处理：MessageBox
        /// 
        /// </summary>
        public bool Active
        {
            get
            {
                return FActive;
            }
            set
            {
                if (value)
                {
                    if (FActive)
                        return;

                    #region 连接ini文件
                    try
                    {
                        ini.ReadXml(FileName);
                        FActive = true;
                    }
                    catch (Exception)
                    {
                        FActive = false;
                    }


                    if (!(FActive))
                    {
                        try
                        {
                            string s = Path.GetDirectoryName(FileName);
                            if (!(Directory.Exists(s)))
                                Directory.CreateDirectory(s);
                            ini.WriteXml(FileName, XmlWriteMode.WriteSchema);
                            FActive = true;
                        }
                        catch (Exception ex)
                        {
                            I3MessageHelper.ShowError(ex.Message, ex);
                            FActive = false;
                        }
                    }
                    #endregion

                }
                else
                {
                    FActive = false;
                    try
                    {
                        if (!string.IsNullOrEmpty(FileName) && File.Exists(FileName))
                        {
                            ini.WriteXml(FileName, XmlWriteMode.WriteSchema);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    ini.Clear();
                }
            }
        }

        /// <summary>
        /// 是否可以响应GetStringEvent事件
        /// </summary>
        public bool CanOnGetValue
        {
            get
            {
                return FCanOnGetValue;
            }
            set
            {
                FCanOnGetValue = value;
            }
        }

        /// <summary>
        /// 是否可以响应SetStringEvent事件
        /// </summary>
        public bool CanOnSetValue
        {
            get
            {
                return FCanOnSetValue;
            }
            set
            {
                FCanOnSetValue = value;
            }
        }



        /// <summary>
        /// 读取属性
        /// Big:大项名称
        /// Small:小项名称
        /// Default:默认值，无法取到值时返回默认值
        /// </summary>
        public string GetString(string Big, string Small, string Default)
        {
            if (!(FActive)) return Default;

            string s;

            DataTable newTable = ini.Tables[Big];
            if (newTable == null)
            {
                s = Default;
                goto OnGetString;
            }

            DataColumn newColumn = newTable.Columns[Small];
            if (newColumn == null)
            {
                s = Default;
                goto OnGetString;
            }
            
            try
            {
                s = newTable.Rows[0][Small].ToString();
            }
            catch (Exception)
            {
                s = Default;
            }

            OnGetString:
            if (CanOnGetValue)
            {
                I3IntGetSetValueEventArgs e = new I3IntGetSetValueEventArgs();
                e.Big = Big;
                e.Small = Small;
                e.Value = s;
                OnGetValue(this, e);

                return e.Value;
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// 写入属性
        /// Big:大项名称
        /// Small:小项名称
        /// Value:要写入的值
        /// </summary>
        public void SetString(string Big, string Small, string Value)
        {
            if (!(FActive)) return;

            if (CanOnSetValue)
            {
                I3IntGetSetValueEventArgs e = new I3IntGetSetValueEventArgs();
                e.Big = Big;
                e.Small = Small;
                e.Value = Value;
                OnSetValue(this, e);
                Big = e.Big;
                Small = e.Small;
                Value = e.Value;
            }

            DataTable newTable = ini.Tables[Big];
            if (newTable == null)
            {
                newTable = new DataTable(Big);
                ini.Tables.Add(newTable);
            }

            DataColumn newColumn = newTable.Columns[Small];
            if (newColumn == null)
            {
                newColumn = new DataColumn(Small, typeof(string));
                newTable.Columns.Add(newColumn);
            }

            DataRow newRow;
            try
            {
                newRow = newTable.Rows[0];
            }
            catch (Exception)
            {
                newRow = newTable.NewRow();
                newTable.Rows.Add(newRow);
            }

            try
            {
                newRow[Small] = Value;
            }
            catch (Exception)
            {
            }

            if (UP)
                ini.WriteXml(FileName, XmlWriteMode.WriteSchema);
        }

        public int GetInt(string Big, string Small, int Default)
        {
            string s = GetString(Big, Small, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(s);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        public void SetInt(string Big, string Small, int Value)
        {
            SetString(Big, Small, Convert.ToString(Value));
        }

        public double GetFloat(string Big, string Small, double Default)
        {
            string s = GetString(Big, Small, Convert.ToString(Default));
            try
            {
                return Convert.ToDouble(s);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        public void SetFloat(string Big, string Small, double Value)
        {
            SetString(Big, Small, Convert.ToString(Value));
        }

        public bool GetBool(string Big, string Small, bool Default)
        {
            string s = GetString(Big, Small, Convert.ToString(Default));
            try
            {
                return Convert.ToBoolean(s);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        public void SetBool(string Big, string Small, bool Value)
        {
            SetString(Big, Small, Convert.ToString(Value));
        }

        public DateTime GetTime(string Big, string Small, DateTime Default)
        {
            string s = GetString(Big, Small, I3DateTimeUtil.ConvertDateTimeToDateTimeString(Default));
            try
            {
                return I3DateTimeUtil.ConvertStringToDateTime(s);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        public void SetTime(string Big, string Small, DateTime Value)
        {
            SetString(Big, Small, I3DateTimeUtil.ConvertDateTimeToDateTimeString(Value));
        }

        /// <summary>
        /// 更新数据以保存到磁盘文件中
        /// </summary>
        public void Updata()
        {
            if (!(FActive)) return;
            ini.WriteXml(FileName, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// 控件的GetStringEvent事件
        /// </summary>
        internal virtual void OnGetValue(Object Sender, I3IntGetSetValueEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (GetValueEvent != null)
                {
                    GetValueEvent(Sender, e);
                }
            }
        }

        /// <summary>
        /// 控件的SetStringEvent事件
        /// </summary>
        internal void OnSetValue(object Sender, I3IntGetSetValueEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (SetValueEvent != null)
                {
                    SetValueEvent(Sender, e);
                }
            }
        }
    }


    public class I3IntGetSetValueEventArgs : EventArgs
    {
        public string Big;
        public string Small;
        public string Value;
    }

    public delegate void I3IniSetValueEvent(object Sender, I3IntGetSetValueEventArgs e);
    public delegate void I3IniGetValueEvent(Object Sender, I3IntGetSetValueEventArgs e);
}