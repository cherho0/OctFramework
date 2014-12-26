using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;

namespace Oct.Framework.DB.Interface
{
    public interface ISQLContext
    {
        ISession Session { get; }

        /// <summary>
        ///     获取单个结果值，查询数量，查询某个字段值等等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T GetResult<T>(string sql);

        /// <summary>
        ///     获取单个结果值，查询数量，查询某个字段值等等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T GetResult<T>(string sql, SqlParameter[] para);

        /// <summary>
        ///     获取单个结果值，查询数量，查询某个字段值等等
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object GetResult(string sql);

        /// <summary>
        ///     获取单个结果值，查询数量，查询某个字段值等等
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object GetResult(string sql, SqlParameter[] para);

        /// <summary>
        ///     返回多行的第一个结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> GetRowsResults<T>(string sql);

        /// <summary>
        ///     执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSQL(string sql);

        /// <summary>
        ///     按事务执行批量SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        int ExecuteSQLs(List<string> sql);

        /// <summary>
        ///     执行查询，返回dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        DataSet ExecuteQuery(string sql, params SqlParameter[] cmdParms);

        /// <summary>
        ///     执行查询，返回dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet ExecuteQuery(string sql);

        /// <summary>
        /// 查询reader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IDataReader ExecuteQueryReader(string sql);

        /// <summary>
        /// 查询reader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        IDataReader ExecuteQueryReader(string sql, params SqlParameter[] cmdParms);

        /// <summary>
        ///     查询一组数据的动态结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        IEnumerable<ExpandoObject> ExecuteExpandoObjects(string sql);

        /// <summary>
        ///     查询一组数据的动态结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        ExpandoObject ExecuteExpandoObject(string sql);

        /// <summary>
        ///     执行存储过程返回reader
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters);

        /// <summary>
        ///     执行存储过程返回dataset
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName);

        /// <summary>
        ///     执行存储过程返回dataset
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int timeOut);

        /// <summary>
        ///     执行存储过程返回行数
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="rowsAffected"></param>
        /// <returns></returns>
        int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected);
    }
}