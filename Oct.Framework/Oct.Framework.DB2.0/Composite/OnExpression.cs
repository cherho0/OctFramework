using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.DynamicObj;

namespace Oct.Framework.DB.Composite
{
    public class OnExpression<T1, T2, TP1, TP2>
    {
        public OnExpression(Expression<Func<T1, TP1>> prop1, Expression<Func<T2, TP2>> prop2, Expression<Func<T2, bool>> func = null)
        {
            var t1TableName = EntitiesProxyHelper.GetProxyInfo<T1>().TableName;
            var t2TableName = EntitiesProxyHelper.GetProxyInfo<T2>().TableName;

            var t1Prop = ExpressionHelper.GetProps(prop1)[0];
            var t2Prop = ExpressionHelper.GetProps(prop2)[0];
        }
    }
}
