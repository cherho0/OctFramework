using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Composite
{
    public class CompositeQuery : ICompositeQuery
    {


        private List<string> _tblNames = new List<string>();
        private StringBuilder _joinBuilder = new StringBuilder();
        private bool _firstJoin = false;

        List<SqlParameter> _parameters = new List<SqlParameter>();
        private ISQLContext _context;

        public CompositeQuery(ISQLContext context)
        {
            _context = context;
        }

        public ICompositeQuery Fetch<T>() where T : BaseEntity<T>, new()
        {
            _tblNames.Add(EntitiesProxyHelper.GetProxyInfo<T>().TableName);
            return this;
        }

        public ICompositeQuery On<T1, T2, TP1, TP2>(Expression<Func<T1, TP1>> left, Expression<Func<T2, TP2>> right)
            where T1 : BaseEntity<T1>, new()
            where T2 : BaseEntity<T2>, new()
        {
            var tbl1 = EntitiesProxyHelper.GetProxyInfo<T1>().TableName;
            var prop1 = ExpressionHelper.GetProps(left)[0];
            var tbl2 = EntitiesProxyHelper.GetProxyInfo<T2>().TableName;
            var prop2 = ExpressionHelper.GetProps(right)[0];

            if (_firstJoin)
            {
                _joinBuilder.AppendFormat(" inner join {2} on {0}.{1} = {2}.{3} ", tbl1, prop1, tbl2, prop2);
            }
            else
            {
                _firstJoin = true;
                _joinBuilder.AppendFormat(" from {0} inner join {2} on {0}.{1} = {2}.{3} ", tbl1, prop1, tbl2, prop2);
            }

            return this;
        }

        public ICompositeQuery OnWhere<T>(Expression<Func<T, bool>> where) where T : BaseEntity<T>, new()
        {
            var tblName = EntitiesProxyHelper.GetProxyInfo<T>().TableName;
            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(where, tblName);
            var sqlwhere = h.SqlWhere;
            var paras = h.GetParameters();
            _parameters.AddRange(paras);
            _joinBuilder.Append(" and " + sqlwhere);
            return this;
        }

        public DataSet Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            int idx = 0;
            foreach (var tblName in _tblNames)
            {
                if (idx == _tblNames.Count - 1)
                {
                    sb.AppendFormat(" {0}.* ", tblName);
                }
                else
                {
                    sb.AppendFormat(" {0}.*, ", tblName);
                }
                idx++;
            }

            sb.Append(_joinBuilder.ToString());

            var ds = _context.ExecuteQuery(sb.ToString(), _parameters.ToArray());
            return ds;
        }
    }
}
