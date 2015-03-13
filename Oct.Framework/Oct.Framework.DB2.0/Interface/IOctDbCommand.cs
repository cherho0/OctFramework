using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Oct.Framework.DB.Interface
{
    public interface IOctDbCommand
    {
        IList<string> FromParts { get; }

        IList<string> WhereParts { get; }

        IList<string> OrderByParts { get; }

        string SqlText { get; }

        IDictionary<string, object> Paras { get; }

        DbCommand GetDbCommand(Func<string, string> createSql, Func<DbCommand> createCmd,
            Func<IDictionary<string, object>, IDbDataParameter[]> createPara);
    }
}