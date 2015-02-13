using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.DB.Attrbuites
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class EntityAttribute : Attribute
    {
        public string TableName { get; private set; }

        public string Sql { get; private set; }

        public bool IsCompositeQuery { get; private set; }

        public EntityAttribute(string tableName)
        {
            TableName = tableName;
        }

        public EntityAttribute(string sql, bool compositeQuery)
        {
            IsCompositeQuery = compositeQuery;
            Sql = sql;
        }
    }
}
