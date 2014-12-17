using System.Data;
using System.Data.SqlClient;

namespace Oct.Tools.Core.Unity
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DBHelper
    {
        #region 方法

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TextConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                return true;
            }
        }

        /// <summary>
        /// 根据Sql获取DataTable
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string connectionString, string sql)
        {
            var conn = new SqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var comm = conn.CreateCommand();
                comm.CommandText = sql;

                var da = new SqlDataAdapter(comm);
                var dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }

        /// <summary>
        /// 批量执行Sql（带事务）
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static bool ExecuteSqls(string connectionString, string[] sqls)
        {
            var conn = new SqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var comm = conn.CreateCommand();
                var tran = conn.BeginTransaction();

                comm.Transaction = tran;

                try
                {
                    foreach (var sql in sqls)
                    {
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();

                    throw;
                }

                return true;
            }
        }

        #endregion
    }
}
