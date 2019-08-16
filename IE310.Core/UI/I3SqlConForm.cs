using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using IE310.Core.Utils;

namespace IE310.Core.UI
{
    internal partial class I3SqlConForm : Form
    {
        private bool ok = false;
        private string FConnctionString = "null" ;

        private I3SqlConForm()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = instance.GetDataSources();

            DisplayData(table);

            
        }

        private void DisplayData(System.Data.DataTable table)
        {
            cbDataSource.Items.Clear();
            foreach (System.Data.DataRow row in table.Rows)
            {
                cbDataSource.Items.Add(row["ServerName"].ToString() + @"\" + row["InstanceName"]);
            }
        }

        private string ConnectionString(string DataBaseName)
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.DataSource = cbDataSource.Text;
            con.InitialCatalog = DataBaseName;

            if (rbWindows.Checked)
            {
                con.IntegratedSecurity = true;
            }
            else
            {
                con.IntegratedSecurity = false;
                con.UserID = tbUserName.Text;
                con.Password = tbPassWord.Text;
            }

            return con.ConnectionString;
        }

        private void rbWindows_Click(object sender, EventArgs e)
        {
            tbUserName.Enabled = rbSql.Checked;
            tbPassWord.Enabled = rbSql.Checked;
        }

        private void cbDataBase_DropDown(object sender, EventArgs e)
        {
            cbDataBase.Items.Clear();

            SqlConnection con = new SqlConnection(ConnectionString("master"));

            try
            {
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = @"select name from sysdatabases";
                SqlDataReader read = com.ExecuteReader();

                while (read.Read())
                {
                    cbDataBase.Items.Add(read["name"]);
                }

                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                I3MessageHelper.ShowError("无法以指定的信息连接到数据库，错误信息：\r\n" + ex.Message, ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString(cbDataBase.Text));

            try
            {
                con.Open();
                I3MessageHelper.ShowInfo("连接成功");
            }
            catch (Exception ex)
            {
                I3MessageHelper.ShowError(ex.Message, ex);
            }
        }


        /// <summary>
        /// 弹出窗口以获取一个SQL连接字符串，获取失败返回false
        /// aConnectionString：老的连接字符串，aNewConnectionString:新的连接字符串
        /// 
        /// 错误处理：屏蔽
        /// 
        /// </summary>
        /// <param name="aConnectionString"></param>
        /// <param name="aNewConnectionString"></param>
        /// <returns></returns>
        static public bool Excute(string aConnectionString, out string aNewConnectionString)
        {
            using (I3SqlConForm main = new I3SqlConForm())
            {
                try
                {
                    SqlConnectionStringBuilder con = new SqlConnectionStringBuilder(aConnectionString);
                    main.cbDataSource.Text = con.DataSource;
                    if (con.IntegratedSecurity)
                    {
                        main.rbWindows.Checked = true;
                    }
                    else
                    {
                        if (main.cbDataSource.Text != "")
                        {
                            main.rbSql.Checked = true;
                            main.tbUserName.Text = con.UserID;
                            main.tbPassWord.Text = con.Password;
                            main.tbPassWord.Enabled = true;
                            main.tbUserName.Enabled = true;
                        }
                    }
                    main.cbDataBase.Text = con.InitialCatalog;
                }
                catch (Exception)
                {
                }

                main.ShowDialog();
                aNewConnectionString = main.FConnctionString;
                return main.ok;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            FConnctionString = ConnectionString(cbDataBase.Text);
            ok = true;
            this.Close();
        }

        private void cbDataSource_DropDown(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = instance.GetDataSources();

            DisplayData(table);

            cbDataSource.DroppedDown = true;
        }

    }

}