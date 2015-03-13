using System.Collections.Generic;
using System.Text;

namespace Oct.Framework.DB.Linq.Commandbuilder
{
    public class SqlServerWherePartsCommandBuilder : SqlServerCommandBuilder
    {
        public StringBuilder WherePartsBuilder { get; private set; }

        public SqlServerWherePartsCommandBuilder() : base()
        {
            this.WherePartsBuilder = new StringBuilder();
        }

        public SqlServerWherePartsCommandBuilder( IDictionary<string,object> parameters ) : base( parameters )
        {
            this.WherePartsBuilder = new StringBuilder();
        }

        public override string ToString()
        {
            return this.WherePartsBuilder.ToString();
        }
    }
}

