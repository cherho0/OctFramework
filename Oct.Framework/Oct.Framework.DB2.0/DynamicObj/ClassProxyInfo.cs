using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.DB.DynamicObj
{
    public class ClassProxyInfo
    {
        public ClassProxyInfo()
        {
            PrimaryKeys = new List<string>();
        }

        public bool IsCompositeQuery { get; set; }

        public string TableName { get; set; }

        public string CompositeSql { get; set; }

        public List<string> PrimaryKeys { get; set; }

        public string IdentitesProp { get; set; }
    }
}
