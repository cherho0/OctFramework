using System;
using System.Data;
using System.Linq.Expressions;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Composite
{
    public interface ICompositeQuery
    {
        ICompositeQuery Fetch<T>()
            where T : BaseEntity<T>, new();

        ICompositeQuery On<T1, T2, TP1, TP2>(Expression<Func<T1, TP1>> left, Expression<Func<T2, TP2>> right)
            where T1 : BaseEntity<T1>, new()
            where T2 : BaseEntity<T2>, new()
            ;

        ICompositeQuery OnWhere<T>(Expression<Func<T, bool>> where)
           where T : BaseEntity<T>, new();

        DataSet Query();
    }
}
