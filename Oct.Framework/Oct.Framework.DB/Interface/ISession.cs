using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Oct.Framework.Core.Log;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Interface
{
    public interface ISession : IDisposable
    {
        string ConnString { get; }

        SqlConnection Connection { get; }

        SqlTransaction Transaction { get; }

        ISession CreateSession();

        void AddCommands(IDbCommand cmd, IEntity entity = null);

        void BeginTransaction();

        int Commit(bool transCommit = true);
        DataSet ExecCmd(IDbCommand cmd);
        void Open();
    }
}