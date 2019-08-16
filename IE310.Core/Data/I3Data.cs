/*                     ieData.IEFS_Data
 * 
 * 
 *          类型: 类库
 * 
 *          说明: 提供与Sql.OLE.XML之间的数据操作功能
 * 
 *      使用方法: 1.自定义变量
 *                2.调用CreateDataSql.CreateDataOle.CreateDataXml生成对象
 *                3.用FillTable(System.IEFS_Data.DataTable dataTable, Boolean clear, String sqlText)来填充数据
 *                4.用UpdataTable(System.IEFS_Data.DataTable dataTable)来更新数据
 *                  这里必须要使用FillTable填充过数据才可以
 *                5.使用Execute来执行SQL语句
 *                6.使用此类填充DataTable后，在释放DataTable之前，需要调用DisposeDataTable来释放相关的资源
 *                  当然，如果Data对象先释放，则不需要这样做
 *                7.为了防止sql注入，Excute和FillTable增加了新的重载函数，增加了ArrayList参数，是参数列表
 * 
 * 
 * 
 * 
 *      注意事项: 1.数据源是XML文件时，Excute方法无效
 *                    2.数据源是XML文件时，FillTable将读取整个表的数据，不需要传入SQL语句
 *                    3.数据源是XML文件时，UpdataTable将更新所有表，dataTable传入null即可  
 *                    4.使用完毕后，调用Close()函数。
 * 
 * 
 * 
 *      修改记录: 1.2008-06-24
 *                　发现一个很严重的问题：以前更新DataTable时，是根据DataTable.TableName得到其在TableNames中的索引，
 * 　　　　　　　　 再通过这个索引得到Adapter数组中的对应Adapter。以前是生成DataTable时就给了一个DataName，而且是数据表的名字
 * 　　　　　　　　 这样的话，如果先后创建了一个数据表的两个数据集对象，就会引起更新错误(前面数据集修改的sql语句在更新后面数据集时被发送)，因此修改为用一个Guid作为TableName
 *                  a.以前的方法也仍然可以用，但是最好是创建DataTable时，不指定TableName
 *                  b.不管是以前手工指定TableName还是现在自动指定TableName，都需要注意访问同一张表时的数据更新问题，（这是数据库编程本身的问题，必须注意）
 *                  c.注意：如果是xml数据源，仍然必须指定DataTable.TableName为数据表名  因为其模式不同，实际上只读取了一次放入到了一个DataSet中
 * 
 *                2.2008-06-24
 *                  将TableName、Ada、CommandBuilder的管理方式由三个ArrayList改为一个HashTable
 * 
 *                3.2008-11-21
 *                  1.对Excute和FillTable增加了新的重载函数，增加了ArrayList参数
 *                    使用时，ArrayList中的元素必须为SqlParameter或者OleDbParameter，此功能是为了防止sql注入而添加的
 *                    
 *                4.2011-03-09
 *                  1.增加函数GetConnection，以返回数据连接
 *                  2.UpdateTable Execute 增加事务处理 并增加参数DbTransaction aTran以指示是否需要使用事务提交模式
 *                    不为null时，是一次性提交多次，需要自己写事务，并做错误处理 commit后自己对数据集AcceptChanges()
 *                  2011-04-12
 *                  更改了使用事务的方法
 *                  
 *                5.2011-04-12
 *                  将哈希表的键值由TableName改为直接传DataTable对象，因此，创建DataTable可以指定具体的数据表名，
 *                  而不是使用自动生成的Guid作为数据表名
 *                  
 *                6.2011-08-09
 *                  使用select查询时，之前使用了CommandBuilder来生成UpdateCommand、InsertCommand、DeleteCommand
 *                  如果对同一个数据集先后两次使用不同的select语句并都做更新，第二次的select语句字段较少，则第二次更新时，自动生成的Command
 *                  如UpdateCommand多了多余的参数但值是null，更新到数据库会产生错误：更新了预期1条数据中的0条。
 *                  *****：所以，使用CommandBuilder时，不要对同一个数据集使用不同的select语句
 *                  
 *                  另外，由于使用CommandBuilder自动生成的语句往往效率不好，因此在FillTable函数增加了接口，使用示例：
 *                      string sqlStr = " select id,ET_Sort from GK_ExcelTemplate order by ET_Sort asc ";
                        OleDbCommand update = new OleDbCommand("UPDATE GK_ExcelTemplate SET ET_Sort = ? WHERE (id = ?)");
                        update.Parameters.Add("@sort", OleDbType.Integer, 4, "ET_Sort");
                        update.Parameters.Add("@id", OleDbType.VarChar, 50, "id");
                        IEFS_Command command = new IEFS_Command(update, null, null);
                        fcon.FillTable(tmp, true, sqlStr, null, command);
                    注意，如果使用OleDb方式连接到数据库，sql语句中写参数时，要用?，添加参数按顺序来(如果用@ET_Sort会报错：要设置变量ET_Sort)；
 *                  而使用sql连接时，要用@ET_Sort，添加参数不需要按顺序
 *                  
 *                  此外，UpdateCommand中，ID本身被更新时，要像下面这样做！：
 *                      SqlCommand command = new SqlCommand(
                            "UPDATE Customers SET CustomerID = @CustomerID, CompanyName = @CompanyName " +
                            "WHERE CustomerID = @oldCustomerID", connection);
                        command.Parameters.Add("@CustomerID", SqlDbType.NChar, 5, "CustomerID");
                        command.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40, "CompanyName");
                        SqlParameter parameter = command.Parameters.Add(
                            "@oldCustomerID", SqlDbType.NChar, 5, "CustomerID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = command;
 *                  即@oldCustomerID这个参数，要指定值来源为原始值（不指定时默认为当前值）

 *  
 *                                                  Created by ie
 *                                                            2008-01-01      
 * 
 * */



using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;
using IE310.Core.Utils;

namespace IE310.Core.Data
{
    public class I3Command
    {

        protected DbCommand updateCommand;
        protected DbCommand deleteCommand;
        protected DbCommand _insertCommand;


        public I3Command(DbCommand updateCommand, DbCommand deleteCommand, DbCommand insertCommand)
        {
            updateCommand = updateCommand;
            deleteCommand = deleteCommand;
            _insertCommand = insertCommand;
        }

        public DbCommand UpdateCommand
        {
            get
            {
                return updateCommand;
            }
        }
        public DbCommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
        }
        public DbCommand InsertCommand
        {
            get
            {
                return _insertCommand;
            }
        }
    }

    public abstract class I3Data /*: IDisposable*/
    {
        protected string lastErrorInfo = "";
        public string LastErrorInfo
        {
            get
            {
                return this.lastErrorInfo;
            }
            set
            {
                this.lastErrorInfo = value;
            }
        }

        protected bool active;
        /// <summary>
        /// 返回是否已经连接到数据库
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
        }



        public abstract DbConnection GetConnection();

      
        /// <summary>
        /// 这里由子类来实现，用于填充数据集,clear指示是否清除dataTable中的现有数据
        /// 可以多次调用以填充多个DataTable
        /// 但对于同一个表，在多次填充到不同的数据集中时，请注意避免数据混乱。
        /// 
        /// 对于Xml数据源，只能读取一次，除非将Data类的对象释放后重新读取
        /// 
        /// 另外，对于Xml数据源，请传递""作为sqlText
        /// 
        /// 
        /// 在对某个DataTable进行填充，并对它进行修改，如果此时使用DataTable.Clear()
        /// 则SqlCommandBuilder中生成的相应sql语句也会被自动释放
        /// 
        /// ArrayList中的对象必须是SqlParameter或者OleDbParameter
        /// 
        /// 增加了自定义UpdateCommand、InsertCommand、DeleteCommand支持，说明见修改记录6
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="clear"></param>
        public abstract I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbParameter[] paramList, DbTransaction aTran, I3Command command);
        public I3MsgInfo FillTable(DataTable dataTable, bool clear, string sqlText, DbTransaction aTran, I3Command command)
        {
            return FillTable(dataTable, clear, sqlText, null, aTran, command);
        }

        /// <summary>
        /// 用于执行没有返回结果的sql命令     
        /// 对于Xml数据源，不能执行此函数     
        /// 
        /// UpdateTable Execute 增加事务处理 并增加参数DbTransaction aTran以指示是否需要使用事务提交模式
        /// 不为null时，是一次性提交多次，需要自己写事务，并做错误处理 commit后自己对数据集AcceptChanges()
        /// 
        /// </summary>
        /// <param name="sqlText"></param>
        public abstract I3MsgInfo Execute(string sqlText, DbParameter[] paramList, DbTransaction aTran);
        public I3MsgInfo Execute(string sqlText, DbTransaction aTran)
        {
            return Execute(sqlText, null, aTran);
        }

        /// <summary>
        /// 用于更新数据到数据源
        /// 每次只能选择更新一个表
        /// 对于Xml数据源，请传递null作为dataTable,因为每次更新都将更新整个dataset
        /// 
        /// UpdateTable Execute 增加事务处理 并增加参数DbTransaction aTran以指示是否需要使用事务提交模式
        /// 不为null时，是一次性提交多次，需要自己写事务，并做错误处理 commit后自己对数据集AcceptChanges()
        /// 
        /// </summary>
        public abstract I3MsgInfo UpdataTable(DataTable dataTable, DbTransaction aTran);
        public abstract void DisposeDataTable(DataTable dataTable);
        public abstract void Close();



        /// <summary>
        /// 生成连接sql的Data对象
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public static I3Data CreateDataSql(string conStr)
        {
            I3DataSql data = new I3DataSql(conStr);
            return (I3Data)data;
        }

        /// <summary>
        /// 生成连接OleDb的Data对象
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public static I3Data CreateDataOle(string conStr)
        {
            I3DataOle data = new I3DataOle(conStr);
            return (I3Data)data;
        }


        /// <summary>
        /// 
        /// 生成连接Xml文件的Data对象
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public static I3Data CreateDataXml(string conStr)
        {
            I3DataXml data = new I3DataXml(conStr);
            return (I3Data)data;
        }

        #region 释放资源       这里是个示例，实际释放放到Close里面了
        /*protected bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                //释放托管资源
            }
            //释放非托管资源

            isDisposed = true;
        }
        ~IEFS_Data()
        {
            Dispose(false);
        }*/
        
        #region 子类中怎样写
        /* 在子类中，像下面这样重写Dispose(bool)即可
         * 
         * if (isDisposed)
         *     return;
         * 
         * if (disposing)
         * {
         *     //释放托管资源
         * }
         * //释放非托管资源
         * 
         * base.Dispose(disposing);
         * 
         */
        #endregion

        #endregion
    }
}
