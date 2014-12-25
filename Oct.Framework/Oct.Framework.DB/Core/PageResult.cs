using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Core
{
    public class PageResult<T> where T : BaseEntity<T>, new()
    {
        public List<T> Models { get; private set; }
        public int TotalCount { get; private set; }

        public PageResult(List<T> models, int total)
        {
            Models = models;
            TotalCount = total;
        }
    }
}
