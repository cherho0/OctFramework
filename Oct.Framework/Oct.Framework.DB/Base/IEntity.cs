using Oct.Framework.Core.Log;
using Oct.Framework.DB.Core;
using System.Collections.Generic;

namespace Oct.Framework.DB.Base
{
    public interface IEntity
    {
        /// <summary>
        /// 实现获取数据变更日志
        /// </summary>
        /// <returns></returns>
        DataChangeLog GetLog();

        /// <summary>
        /// 业务逻辑验证
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessRule> Validate();
    }
}
