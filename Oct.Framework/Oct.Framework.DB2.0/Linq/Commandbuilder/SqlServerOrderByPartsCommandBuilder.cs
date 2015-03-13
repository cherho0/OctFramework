using System.Text;

namespace Oct.Framework.DB.Linq.Commandbuilder
{
    public class SqlServerOrderByPartsCommandBuilder : SqlServerCommandBuilder
    {
        public StringBuilder OrderByParts { get; private set; }

        public SqlServerOrderByPartsCommandBuilder() : base()
        {
            this.OrderByParts = new StringBuilder();
        }

        public override string ToString()
        {
            return this.OrderByParts.ToString();
        }
    }
}

