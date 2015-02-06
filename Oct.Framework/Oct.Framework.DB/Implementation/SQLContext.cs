using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Implementation
{
    public class SQLContext : ISQLContext
    {
        public ISession Session { get; private set; }

        public SQLContext(ISession session)
        {
            Session = session;
        }

        public T GetResult<T>(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                DataSet ds = ExecuteQuery(sql);

                if (ds.Tables.Count == 0)
                    return default(T);

                if (ds.Tables[0].Rows.Count == 0)
                    return default(T);

                object v = ds.Tables[0].Rows[0][0];

                return (T)Convert.ChangeType(v, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public T GetResult<T>(string sql, SqlParameter[] para)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                DataSet ds = ExecuteQuery(sql, para);

                if (ds.Tables.Count == 0)
                    return default(T);

                if (ds.Tables[0].Rows.Count == 0)
                    return default(T);

                object v = ds.Tables[0].Rows[0][0];

                return (T)Convert.ChangeType(v, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public object GetResult(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                DataSet ds = ExecuteQuery(sql);

                if (ds.Tables.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                object v = ds.Tables[0].Rows[0][0];

                return v;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\n" + sql);
            }
        }

        public object GetResult(string sql, SqlParameter[] paras)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                DataSet ds = ExecuteQuery(sql, paras);

                if (ds.Tables.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                object v = ds.Tables[0].Rows[0][0];

                return v;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\n" + sql);
            }
        }

        public IEnumerable<T> GetRowsResults<T>(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            DataSet ds = ExecuteQuery(sql);

            if (ds.Tables.Count == 0)
                yield return default(T);

            if (ds.Tables[0].Rows.Count == 0)
                yield return default(T);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                object v = r[0];
                yield return (T)Convert.ChangeType(v, typeof(T));
            }
        }

        public int ExecuteSQL(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            var cmd = new SqlCommand(sql);
            Session.AddCommands(cmd);
            int rows = Session.Commit();
            return rows;
        }

        public int ExecuteSQLs(List<string> sqls)
        {
            foreach (string sql in sqls)
            {
                var cmd = new SqlCommand(sql);
                Session.AddCommands(cmd);
            }
            int rows = Session.Commit();
            return rows;
        }

        public DataSet ExecuteQuery(string sql, params object[] paras)
        {
            if (paras != null && paras.Length > 0 && paras[0] is IDictionary<string, object>)
            {
                return ExecuteQueryDict(sql, (IDictionary<string, object>)paras[0]);
            }
            var scalarBuiler = WhereHelper.CreateScalarWhere(sql, paras);
            return ExecuteQuery(scalarBuiler.Where, scalarBuiler.SqlParameters.ToArray());
        }

        public DataSet ExecuteQueryScalar(string sql, params object[] paras)
        {
            if (paras != null && paras.Length > 0 && paras[0] is IDictionary<string, object>)
            {
                return ExecuteQueryDict(sql, (IDictionary<string, object>)paras[0]);
            }
            var scalarBuiler = WhereHelper.CreateScalarWhere(sql, paras);
            return ExecuteQuery(scalarBuiler.Where, scalarBuiler.SqlParameters.ToArray());
        }

        public DataSet ExecuteQueryDict(string sql, IDictionary<string, object> paras)
        {
            var parasList = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    parasList.Add(new SqlParameter(para.Key, para.Value));
                }
            }

            return ExecuteQuery(sql, parasList.ToArray());
        }

        public DataSet ExecuteQuery(string sql, IDictionary<string, object> paras)
        {
            var parasList = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    parasList.Add(new SqlParameter(para.Key, para.Value));
                }
            }

            return ExecuteQuery(sql, parasList.ToArray());
        }

        public PageResult ExecutePageQuery(string sql, IDictionary<string, object> paras, string order, int pageIndex, int pageSize)
        {
            var parasList = new List<SqlParameter>();
            var parasListData = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    parasList.Add(new SqlParameter(para.Key, para.Value));
                    parasListData.Add(new SqlParameter(para.Key, para.Value));
                }
            }
            var total = GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), parasList.ToArray());

            int start = (pageIndex - 1) * pageSize;
            var tempsql = "select *,ROW_NUMBER() OVER(ORDER BY " + order + ") rn from ({0}) a ";
            sql = string.Format(tempsql, sql);

            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";

            var ds = ExecuteQuery(sql, parasListData.ToArray());
            return new PageResult(ds, total);
        }

        public PageResult ExecutePageQuery(string sql, string order, int pageIndex, int pageSize, params object[] paras)
        {
            var scalarBuiler = WhereHelper.CreateScalarWhere(sql, paras);
            return ExecutePageQuery(scalarBuiler.Where, scalarBuiler.Paras, order, pageIndex, pageSize);
        }

        public DataSet ExecuteQuery(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                Session.Open();
                var ds = new DataSet();
                var cmd = new SqlCommand(sql);
                cmd.Connection = Session.Connection;
                var command = new SqlDataAdapter(cmd);

                command.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //session.Connection.Close();
            }
        }

        public DataSet ExecuteQuery(string sql, params SqlParameter[] cmdParms)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                Session.Open();
                var ds = new DataSet();
                var cmd = new SqlCommand();
                cmd = PrepareCommand(cmd, sql, cmdParms);
                cmd.Connection = Session.Connection;
                var command = new SqlDataAdapter(cmd);
                command.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "\r\n" + sql);
            }
            finally
            {
                //session.Connection.Close();
            }
        }

        public IDataReader ExecuteQueryReader(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                Session.Open();
                var cmd = new SqlCommand(sql);
                cmd.Connection = Session.Connection;
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "\r\n" + sql);
            }
        }

        public IDataReader ExecuteQueryReader(string sql, params SqlParameter[] cmdParms)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            try
            {
                Session.Open();
                var cmd = new SqlCommand();
                cmd = PrepareCommand(cmd, sql, cmdParms);
                cmd.Connection = Session.Connection;
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "\r\n" + sql);
            }

        }


        public IEnumerable<ExpandoObject> ExecuteExpandoObjects(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            DataSet ds = ExecuteQuery(string.Format(sql));
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                yield break;
            }

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    IDictionary<string, object> expando = new ExpandoObject();
                    foreach (DataColumn column in table.Columns)
                    {
                        expando.Add(column.Caption, row[column]);
                    }

                    yield return (ExpandoObject)expando;
                }
            }
        }

        public IEnumerable<ExpandoObject> ExecuteExpandoObjects(string sql, params object[] paras)
        {
            var scalarBuiler = WhereHelper.CreateScalarWhere(sql, paras);
            return ExecuteExpandoObjects(scalarBuiler.Where, scalarBuiler.SqlParameters);
        }

        public ExPageResult ExecutePageExpandoObjects(string sql, string order, int pageIndex, int pageSize, params object[] paras)
        {
            var scalarBuiler = WhereHelper.CreateScalarWhere(sql, paras);
            return ExecutePageExpandoObjects(scalarBuiler.Where, scalarBuiler.Paras, order, pageIndex, pageSize);
        }

        public IEnumerable<ExpandoObject> ExecuteExpandoObjects(string sql, params SqlParameter[] cmdParms)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            DataSet ds = ExecuteQuery(string.Format(sql), cmdParms);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                yield break;
            }

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    IDictionary<string, object> expando = new ExpandoObject();
                    foreach (DataColumn column in table.Columns)
                    {
                        expando.Add(column.Caption, row[column]);
                    }

                    yield return (ExpandoObject)expando;
                }
            }
        }

        public ExPageResult ExecutePageExpandoObjects(string sql, IDictionary<string, object> paras, string order, int pageIndex, int pageSize)
        {
            var parasList = new List<SqlParameter>();
            var parasListData = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    parasList.Add(new SqlParameter(para.Key, para.Value));
                    parasListData.Add(new SqlParameter(para.Key, para.Value));
                }
            }
            var total = GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), parasList.ToArray());

            int start = (pageIndex - 1) * pageSize;
            var tempsql = "select *,ROW_NUMBER() OVER(ORDER BY " + order + ") rn from ({0}) a ";
            sql = string.Format(tempsql, sql);

            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";

            var ds = ExecuteExpandoObjects(sql, parasListData.ToArray());
            return new ExPageResult(ds, total);
        }

        public ExpandoObject ExecuteExpandoObject(string sql)
        {
            if (SQLWordFilte.CheckSql(sql))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            DataSet ds = ExecuteQuery(string.Format(sql));
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    IDictionary<string, object> expando = new ExpandoObject();
                    foreach (DataColumn column in table.Columns)
                    {
                        expando.Add(column.Caption, row[column]);
                    }

                    return (ExpandoObject)expando;
                }
            }
            return null;
        }

        public ExpandoObject ExecuteExpandoObject(string sql, params object[] paras)
        {
            var scalarBuiler = WhereHelper.CreateScalarWhere(sql, paras);
            DataSet ds = ExecuteQuery(scalarBuiler.Where, paras);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    IDictionary<string, object> expando = new ExpandoObject();
                    foreach (DataColumn column in table.Columns)
                    {
                        expando.Add(column.Caption, row[column]);
                    }

                    return (ExpandoObject)expando;
                }
            }
            return null;
        }

        protected SqlCommand PrepareCommand(SqlCommand cmd, string cmdText, SqlParameter[] cmdParms)
        {
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput ||
                         parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }

            return cmd;
        }

        #region 存储过程操作

        /// <summary>
        ///     执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            try
            {
                Session.Open();
                SqlDataReader returnReader;
                SqlCommand command = BuildQueryCommand(Session.Connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return returnReader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //session.Connection.Close();
            }
        }

        /// <summary>
        ///     执行存储过程
        /// </summary>
        /// <param name="readType">数据读取方式</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            try
            {
                Session.Open();
                var dataSet = new DataSet();
                SqlCommand command = BuildQueryCommand(Session.Connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                var sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = command;
                sqlDA.Fill(dataSet, tableName);
                return dataSet;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //session.Connection.Close();
            }
        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int timeOut)
        {
            try
            {
                Session.Open();
                var dataSet = new DataSet();
                SqlCommand command = BuildQueryCommand(Session.Connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                var sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = command;
                sqlDA.SelectCommand.CommandTimeout = timeOut;
                sqlDA.Fill(dataSet, tableName);
                return dataSet;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //session.Connection.Close();
            }
        }

        /// <summary>
        ///     执行存储过程，返回影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            try
            {
                int result;
                Session.Open();
                SqlCommand command = BuildIntCommand(Session.Connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //session.Connection.Close();
            }
        }

        /// <summary>
        ///     构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName,
            IDataParameter[] parameters)
        {
            var command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput ||
                         parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        ///     创建 SqlCommand 对象实例(用来返回一个整数值)
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #endregion
    }
}