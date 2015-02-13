using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Composite
{
    public class CompositeQuery:ICompositeQuery
    {
        public ICompositeQuery Fetch<T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, Expression<Func<T1, T2, bool>> whereExpression)
            where T1 : BaseEntity<T1>, new()
            where T2 : BaseEntity<T2>, new()
        {
            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(joinExpression);
            var where = h.SqlWhere;


            return this;
        }
    }
}
