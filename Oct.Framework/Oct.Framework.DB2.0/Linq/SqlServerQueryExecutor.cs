using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Emit.Utils;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;
using Oct.Framework.DB.LightDataAccess;
using Remotion.Linq;

namespace Oct.Framework.DB.Linq
{
    public class SqlServerQueryExecutor : IQueryExecutor
    {
        protected ISQLContext provider;

        public SqlServerQueryExecutor(ISQLContext provider)
        {
            this.provider = provider;
        }

        #region ExecuteCollection<T>

        /// <summary>
        ///  执行查询，并返回 IEnumerable<T> 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            SqlServerQueryModelVisitor queryModelVisitor = new SqlServerQueryModelVisitor();

            var command = queryModelVisitor.Translate(queryModel);
            using (IDataReader reader = this.provider.ExecuteQueryReader(command.ToString(), command.PrepareParameter().ToArray()))
            {
                while (reader.Read())
                {
                    yield return ((DbDataReader)reader).ToObject<T>();
                }
              
            }
        }

        #endregion

        #region ExecuteScalar<T>

        /// <summary>
        /// 执行查询，并返回第一行第一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            SqlServerQueryModelVisitor queryModelVisitor = new SqlServerQueryModelVisitor();

            var command = queryModelVisitor.Translate(queryModel);
            return provider.GetResult<T>(command.ToString(),command.PrepareParameter().ToArray());
        }

        #endregion

        #region ExecuteSingle<T>

        /// <summary>
        /// 执行查询，并返回单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryModel"></param>
        /// <param name="returnDefaultWhenEmpty"></param>
        /// <returns></returns>
        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty = true)
        {
            SqlServerQueryModelVisitor queryModelVisitor = new SqlServerQueryModelVisitor();

            var command = queryModelVisitor.Translate(queryModel);


            using (IDataReader reader = this.provider.ExecuteQueryReader(command.ToString(), command.PrepareParameter().ToArray()))
            {
                bool isExists = reader.Read();

                // 如果不存在查询记录
                if (isExists == false)
                {
                    // 如果可以默认
                    if (returnDefaultWhenEmpty)
                    {
                        return default(T);
                    }
                    else
                    {
                        throw new InvalidOperationException("未查询出满足条件的任何记录。");
                    }
                }
                return reader.ToObject<T>();
            }
        }

        #endregion
    }
}
