using Oct.Framework.DB.Base;
using System.Linq;

namespace Oct.Framework.Services
{
    public class BaseService
    {
        protected void Validate(IEntity entity)
        {
            var rules = entity.Validate();

            if (rules != null && rules.Any())
            {
                //只显示第一个验证失败的信息
                throw new ServiceException(rules.ElementAt(0).Rule);
            }
        }
    }
}
