using System.Collections.Generic;
using System.Data;
using System.Dynamic;
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

    public class PageResult
    {
        public DataSet Models { get; private set; }
        public int TotalCount { get; private set; }

        public PageResult(DataSet models, int total)
        {
            Models = models;
            TotalCount = total;
        }
    }

    public class ExPageResult
    {
        public IEnumerable<ExpandoObject> Models { get; private set; }
        public int TotalCount { get; private set; }

        public ExPageResult(IEnumerable<ExpandoObject> models, int total)
        {
            Models = models;
            TotalCount = total;
        }
    }
}
