using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 所有数据访问层类的基类，封装了一些数据库访问的保护方法
    /// </summary>
    /// 
    public class SQLHelper
    {

        private static string connectionString = Funs.ConnString;
        //private static SqlConnection Connection = new SqlConnection(connectionString);

        //public static SqlConnection GetConn()
        //{
        //    return Connection;
        //}

        /// <summary>
        ///被RunProcedure调用 构建 SqlCommand 对象.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure in the DB, eg. sp_DoTask</param>
        /// <param name="parameters">Array of IDataParameter objects containing parameters to the stored proc</param>
        /// <returns>Newly instantiated SqlCommand instance</returns>
        private static SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(storedProcName, parameters);
           
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int,
                4, /* Size */
                ParameterDirection.ReturnValue,
                false, /* is nullable */
                0, /* byte precision */
                0, /* byte scale */
                string.Empty,
                DataRowVersion.Default,
                null));

            return command;
        }


        /// <summary>
        /// 在BuildIntCommand中被调用的私有函数，用于构建 SqlCommand 对象.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of IDataParameter objects</param>
        /// <returns></returns>
        private static SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure//执行存储过程          
                    };
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    return command;
                }
                finally
                {
                    Connection.Close();
                }                
            }
        }

        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    SqlCommand command = BuildIntCommand(storedProcName, parameters);
                    command.CommandTimeout = 0;
                    rowsAffected = command.ExecuteNonQuery();//执行完存储过程后返回的结果
                    result = (int)command.Parameters["ReturnValue"].Value;
                    return result;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 通过存储过程获得新ID（存储过程有返回值有个output参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <returns></returns>
        public static string RunProcNewId(string storedProcName)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add(new SqlParameter("@returnVal", SqlDbType.VarChar, 50));
                    command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    str = command.Parameters["@returnVal"].Value.ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }
        }

        /// <summary>
        /// 通过存储过程名和表名以及列名获得新ID（存储过程有返回值有个output参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static string RunProcNewId(string storedProcName, string tableName, string columnName)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter[] values = new SqlParameter[]
                                            {
                                                new SqlParameter("@tableName", tableName),
                                                new SqlParameter("@columnName", columnName),
                                                new SqlParameter("@returnVal", SqlDbType.VarChar, 10)
                                            };
                    command.Parameters.AddRange(values);
                    command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    str = command.Parameters["@returnVal"].Value.ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }
        }

        /// <summary>
        ///  通过存储过程名和表名以及列名获得新ID（存储过程有返回值有个output参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="perfix">前缀</param>
        /// <returns>递增的新ID</returns>
        public static string RunProcNewId(string storedProcName, string tableName, string columnName, string projectId, string prefix)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter[] values = new SqlParameter[]
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@columnName", columnName),
                new SqlParameter("@projectId", projectId),
                new SqlParameter("@prefix", prefix),
                new SqlParameter("@returnVal", SqlDbType.VarChar, 30)
            };
                    command.Parameters.AddRange(values);
                    command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    str = command.Parameters["@returnVal"].Value.ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }
        }

        /// <summary>
        ///  通过存储过程名和表名以及列名获得新ID（存储过程有返回值有个output参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="perfix">前缀</param>
        /// <returns>递增的新ID</returns>
        public static string RunProcNewId(string storedProcName, string tableName, string columnName, string prefix)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                 
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter[] values = new SqlParameter[]
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@columnName", columnName),
                new SqlParameter("@prefix", prefix),
                new SqlParameter("@returnVal", SqlDbType.VarChar, 30)
            };
                    command.Parameters.AddRange(values);
                    command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    str = command.Parameters["@returnVal"].Value.ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }
        }

        /// <summary>
        /// 通过存储过程获得DataTable
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <returns></returns>
        public static DataTable GetDataTableRunProc(string storedProcName, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                  
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                finally
                {
                    Connection.Close();
                }
                return dataTable;
            }
        }


        /// <summary>
        /// 通过存储过程获得DataTable
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <returns></returns>
        public static DataTable GetDataTableRunText(string strSql, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                  
                    Connection.Open();
                    SqlCommand command = new SqlCommand(strSql, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.Text
                    };
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                finally
                {
                    Connection.Close();
                }
                return dataTable;
            }
        }

        /// <summary>
        /// Will run a stored procedure, can only be called by those classes deriving
        /// from this base. It returns a SqlDataReader containing the result of the stored
        /// procedure.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters to be passed to the procedure</param>
        /// <returns>A newly instantiated SqlDataReader object</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlDataReader returnReader;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                   
                    Connection.Open();
                    SqlCommand command = BuildQueryCommand(storedProcName, parameters);
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;
                    returnReader = command.ExecuteReader();
                }
                finally { Connection.Close(); }
                return returnReader;
            }
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>字符串</returns>
        public static string getStr(string sql)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                   
                    Connection.Open();
                    SqlCommand command = new SqlCommand(sql, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.Text
                    };
                    str = command.ExecuteScalar().ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }

        }

        public static int GetIntValue(string sql)
        {
            int i = 0;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                   
                    Connection.Open();
                    SqlCommand command = new SqlCommand(sql, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.Text
                    };
                    i = Convert.ToInt32(command.ExecuteScalar());
                }
                finally
                {
                    Connection.Close();
                }
                return i;
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        public static void ExecutSql(string sql)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlCommand command = new SqlCommand(sql, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.Text
                    };
                    command.ExecuteNonQuery();
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a DataSet by running the stored procedure and placing the results
        /// of the query/proc into the given tablename.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter
                    {
                        SelectCommand = BuildQueryCommand(storedProcName, parameters)
                    };
                    sqlDA.SelectCommand.CommandTimeout = 0;
                    sqlDA.Fill(dataSet, tableName);
                }
                finally
                {
                    Connection.Close();
                }
                return dataSet;
            }
        }

        /// <summary>
        /// Takes an -existing- dataset and fills the given table name with the results
        /// of the stored procedure.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter
                    {
                        SelectCommand = BuildIntCommand(storedProcName, parameters)
                    };
                    sqlDA.SelectCommand.CommandTimeout = 0;
                    sqlDA.Fill(dataSet, tableName);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        /// <summary>
        /// 用于简单的Sql查询，返回DataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet RunSqlString(string strSql, string tableName)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlCommand command = new SqlCommand(strSql, Connection)
                    {
                        CommandTimeout = 0
                    };
                    SqlDataAdapter sqlDA = new SqlDataAdapter
                    {
                        SelectCommand = command
                    };
                    sqlDA.Fill(dataSet, tableName);
                }
                finally
                {
                    Connection.Close();
                }

                return dataSet;
            }
        }
        /// <summary>
        /// 用于简单的Sql查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        public static void RunProcedure(string strSql, DataSet dataSet, string tableName)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlCommand command = new SqlCommand(strSql, Connection)
                    {
                        CommandTimeout = 0
                    };
                    SqlDataAdapter sqlDA = new SqlDataAdapter
                    {
                        SelectCommand = command
                    };
                    sqlDA.SelectCommand.CommandTimeout = 0;
                    sqlDA.Fill(dataSet, tableName);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        /// <summary>
        /// 用于简单的Sql执行，不返回值，用于数据操作类型的
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="result"></param>
        public static void RunSqlString(string strSql, out int result)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlCommand command = new SqlCommand(strSql, Connection)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.Text
                    };

                    result = command.ExecuteNonQuery();
                }
                finally
                {
                    Connection.Close();
                }
            }
        }


        /*	public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName )
            {
                DataSet dataSet = new DataSet();
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand( storedProcName, parameters );
                sqlDA.Fill( dataSet, tableName );
                Connection.Close();

                return dataSet;
            }*/

        /// 返回存储过程执行的结果集
        /// </summary>
        /// <param name="pur_name">存储过程名称</param>
        /// <param name="parmList">存储过程的{参数--值}的集合</param>
        //  返回存储过程执行的结果集。
        public static DataTable RunProcedureGetTable(string pur_name, Hashtable parmList)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                // 调用存储过程pur_name。
                SqlCommand sqlCmd = new SqlCommand(pur_name, Connection)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                // 为输入参数赋值。
                foreach (string parm in parmList.Keys)
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@" + parm, parmList[parm]));
                }
                // 打开连接。            
                sqlCmd.Connection.Open();
                SqlDataReader dreader = null;
                try
                {
                    dreader = sqlCmd.ExecuteReader();
                    for (int i = 0; i < dreader.FieldCount; i++)
                    {
                        DataColumn myDataColumn;
                        myDataColumn = new DataColumn
                        {
                            DataType = System.Type.GetType(dreader.GetFieldType(i).ToString()),
                            ColumnName = dreader.GetName(i),
                            Caption = dreader.GetName(i)
                        };
                        dt.Columns.Add(myDataColumn);
                    }
                    while (dreader.Read())
                    {
                        dr = dt.NewRow();
                        for (int i = 0; i < dreader.FieldCount; i++)
                        {
                            dr[i] = dreader[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    dreader.Close();
                }
                catch (Exception ex)
                {
                    string str = ex.Message.ToString();
                    // 不成功则给出提示。
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    // 关闭连接。
                    sqlCmd.Connection.Close();
                }

                return dt;
            }
        }

        /// </summary>
        /// <param name="strSql">sql串</param>
        /// <param name="parmList">sql{参数--值}的集合</param>
        public static DataTable RunSqlGetTable(string strSql)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                // 调用存储过程pur_name。
                SqlCommand sqlCmd = new SqlCommand(strSql, Connection)
                {
                    CommandTimeout = 0
                };
                // 打开连接。              
                sqlCmd.Connection.Open();
                SqlDataReader dreader = null;
                try
                {
                    dreader = sqlCmd.ExecuteReader();
                    for (int i = 0; i < dreader.FieldCount; i++)
                    {
                        DataColumn myDataColumn;
                        myDataColumn = new DataColumn
                        {
                            DataType = System.Type.GetType(dreader.GetFieldType(i).ToString()),
                            ColumnName = dreader.GetName(i),
                            Caption = dreader.GetName(i)
                        };
                        dt.Columns.Add(myDataColumn);
                    }
                    while (dreader.Read())
                    {
                        dr = dt.NewRow();
                        for (int i = 0; i < dreader.FieldCount; i++)
                        {
                            dr[i] = dreader[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    dreader.Close();
                }
                catch (Exception ex)
                {
                    // 不成功则给出提示。
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    // 关闭连接。
                    sqlCmd.Connection.Close();
                }

                return dt;
            }
        }


        /// 返回存储过程执行的结果集
        /// </summary>
        /// <param name="pur_name">存储过程名称</param>
        /// <param name="parmList">存储过程的{参数--值}的集合</param>
        public static void RunProcedure(string pur_name, Hashtable parmList)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                // 调用存储过程pur_name。
                SqlCommand sqlCmd = new SqlCommand(pur_name, Connection)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                // 为输入参数赋值。
                foreach (string parm in parmList.Keys)
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@" + parm, parmList[parm]));
                }
                // 打开连接。
                sqlCmd.Connection.Open();
                try
                {
                    sqlCmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    // 不成功则给出提示。
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    // 关闭连接。
                    sqlCmd.Connection.Close();
                }
            }
        }


        /// 返回存储过程执行的结果集
        /// </summary>
        /// <param name="pur_name">存储过程名称</param>
        /// <param name="parmList">存储过程的{参数--值}的集合</param>
        public static void RunProcedure(string pur_name, Hashtable parmList, ref  int returnValue)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                // 调用存储过程pur_name。
                SqlCommand sqlCmd = new SqlCommand(pur_name, Connection)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                // 为输入参数赋值。
                foreach (string parm in parmList.Keys)
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@" + parm, parmList[parm]));
                }
                // 打开连接。
                sqlCmd.Connection.Open();
                try
                {
                    returnValue = sqlCmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    // 不成功则给出提示。
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    // 关闭连接。
                    sqlCmd.Connection.Close();
                }
            }
        }

        private static void AddParameterToCommand(SqlCommand cmd, SqlParameter[] param)
        {
            foreach (SqlParameter p in param)
            {
                cmd.Parameters.Add(p);
            }
        }

        private static SqlCommand CreateCommand(CommandType commandType,
            string commandText, SqlParameter[] param)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandTimeout = 0,
                    CommandType = commandType,
                    Connection = Connection
                };
                if (param != null)
                {
                    AddParameterToCommand(cmd, param);
                }
                cmd.CommandText = commandText;
                return cmd;
            }
        }


        public static int ExecuteCommand(CommandType commandType,
            string commandText, SqlParameter[] param)
        {
            SqlCommand cmd = CreateCommand(commandType, commandText, param);
            cmd.CommandTimeout = 0;
            SqlTransaction trans = null;

            try
            {                
                cmd.Connection.Open();
                trans = cmd.Connection.BeginTransaction();
                cmd.Transaction = trans;
                int i = cmd.ExecuteNonQuery();
                trans.Commit();

                return i;
            }
            catch (SqlException se)
            {
                trans.Rollback();
                throw se;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        ///  通过存储过程名和表名以及列名获得新ID（存储过程有返回值有个output参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="projectId">项目号</param>
        /// <returns>递增的新版本号</returns>
        public static string RunProcNewId2(string storedProcName, string tableName, string columnName, string projectId)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                  
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter[] values = new SqlParameter[]
                {
                  new SqlParameter("@tableName", tableName),
                  new SqlParameter("@columnName", columnName),
                  new SqlParameter("@projectId", projectId),
                  new SqlParameter("@returnVal", SqlDbType.VarChar, 30)
                };
                    command.Parameters.AddRange(values);
                    command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    str = command.Parameters["@returnVal"].Value.ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }
        }

        /// <summary>
        ///  通过存储过程名和表名以及列名获得新ID（存储过程有返回值有个output参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="perfix">前缀</param>
        /// <returns>递增的新ID</returns>
        public static string RunProcNewIdByProjectId(string storedProcName, string tableName, string columnName, string projectId, string prefix)
        {
            string str = "";
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                try
                {                    
                    Connection.Open();
                    SqlCommand command = new SqlCommand(storedProcName, Connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter[] values = new SqlParameter[]
                 {
                   new SqlParameter("@tableName", tableName),
                   new SqlParameter("@columnName", columnName),
                   new SqlParameter("@projectId", projectId),
                   new SqlParameter("@prefix", prefix),
                   new SqlParameter("@returnVal", SqlDbType.VarChar, 50)
                 };
                    command.Parameters.AddRange(values);
                    command.Parameters["@returnVal"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    str = command.Parameters["@returnVal"].Value.ToString();
                }
                finally
                {
                    Connection.Close();
                }
                return str;
            }
        }

        /// <summary>
        /// 创建一个对象锁
        /// </summary>
        private static object newIdLocker = new object();

        /// <summary>
        /// 生成一个新的用于指定表的主键。
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetNewID(Type table)
        {
            lock (newIdLocker)
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// 生成一个新的用于指定表的主键。
        /// </summary>
        /// <returns></returns>
        public static string GetNewID()
        {
            lock (newIdLocker)
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// 表数据中的主键最大值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="keyColumnName">主键名</param>
        /// <returns>主键最大值</returns>
        public static int GetMaxId(string tableName, string ColumnName)
        {
            int maxId = 0;
            string str = "SELECT (ISNULL(MAX(" + ColumnName + "),0)+1) from " + tableName + "";
            maxId = GetIntValue(str);
            return maxId;
        }
    }
}
