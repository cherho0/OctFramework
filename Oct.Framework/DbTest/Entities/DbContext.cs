using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;

namespace DbTest.Entities
{
    public class DbContext : DBContextBase
    {
        public DbContext()
            : base()
        {
        }

        public DbContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbContext(DBOperationType dbType)
            : base(dbType)
        {

        }

        public IDBContext<TestTs> TestTsContext
        {
            get { return new EntityContext<TestTs>(Session); }
        }
    }
}
