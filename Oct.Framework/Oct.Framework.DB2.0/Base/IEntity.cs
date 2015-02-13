using System.Collections.Generic;
using Oct.Framework.Core.Log;
using Oct.Framework.DB.Core;

namespace Oct.Framework.DB.Base
{
    public interface IEntity
    {
        /// <summary>
        /// 业务逻辑验证
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessRule> Validate();

        DataChangeLog GetChangeLogs();
    }
}
