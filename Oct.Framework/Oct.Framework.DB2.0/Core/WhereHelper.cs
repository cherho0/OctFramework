using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Oct.Framework.DB.Core
{
    public class WhereHelper
    {
        public static ScalarBuiler CreateScalarWhere(string where, object[] paras)
        {
            string[] parts = where.Split('?');
            StringBuilder sb = new StringBuilder();
            ScalarBuiler builder = ScalarBuiler.CreateBuilder();
            for (int i = 0; i < parts.Length; i++)
            {
                sb.Append(parts[i]);

                if (i < parts.Length - 1)
                {
                    object p = null;
                    if (i > paras.Length - 1)
                    {
                        p = paras[paras.Length - 1];
                    }
                    else
                    {
                        p = paras[i];
                    }

                    sb.Append(builder.AddParameter(p));
                }
            }
            builder.Where = sb.ToString();
            return builder;
        }

    }

    public class ScalarBuiler
    {
        public string Where { get; set; }

        public IDictionary<string, object> Paras { get; set; }

        public List<SqlParameter>  SqlParameters { get; set; }

        public static ScalarBuiler CreateBuilder()
        {
            var builder = new ScalarBuiler();
            builder.SqlParameters = new List<SqlParameter>();
            return builder;
        }

        public string AddParameter(object value)
        {
            if (Paras == null)
            {
                Paras = new Dictionary<string, object>();
            }
            string parameterName = string.Format("@P{0}", Paras.Count);

            Paras.Add(parameterName, value);
            SqlParameters.Add(new SqlParameter(parameterName, value));
            return parameterName;
        }
    }

}
