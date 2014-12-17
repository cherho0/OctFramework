using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Oct.Framework.DB.Interface
{
    public interface IOctDbCommand
    {
        string SqlText { get; }

        IDictionary<string, object> Paras { get; }

        DbCommand GetDbCommand(Func<string, string> createSql, Func<DbCommand> createCmd,
            Func<IDictionary<string, object>, IDbDataParameter[]> createPara);
    }
}