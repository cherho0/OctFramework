using System;
using System.Data;
using System.Data.SqlClient;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Interface
{
    public interface ISession : IDisposable
    {
        string ConnString { get; }

        SqlConnection Connection { get; }

        SqlTransaction Transaction { get; }

        ISession CreateSession();

        void AddCommands(IDbCommand cmd);

        void BeginTransaction();

        int Commit();
        DataSet ExecCmd(IDbCommand cmd);
        void Open();
    }
}