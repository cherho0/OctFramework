using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    internal class DBSession : ISession
    {
        public string SessionId;
        private bool _openConn;

        private readonly List<IDbCommand> _cmds;

        private readonly string _connStr = string.Empty;

        private SqlConnection _conn;
        private SqlTransaction _trans;

        public DBSession(string connstr)
        {
            SessionId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            _connStr = connstr;
            _cmds = new List<IDbCommand>();
        }

        public string ConnString
        {
            get { return _connStr; }
        }

        public SqlConnection Connection
        {
            get { return _conn; }
        }

        public SqlTransaction Transaction
        {
            get { return _trans; }
        }

        public ISession CreateSession()
        {
            _conn = new SqlConnection(_connStr);
            return this;
        }

        public void BeginTransaction()
        {
            if (_trans == null)
            {
                _trans = _conn.BeginTransaction();
            }
        }

        public int Commit()
        {
            int execCount = 0;
            try
            {
                if (_cmds != null)
                {
                    if (!_openConn)
                    {
                        _conn.Open();
                        _openConn = true;
                    }


                    if (_trans == null)
                    {
                        _trans = _conn.BeginTransaction();
                    }
                    foreach (IDbCommand dbCommand in _cmds)
                    {
                        dbCommand.Connection = _conn;
                        dbCommand.Transaction = _trans;
                        int rows = dbCommand.ExecuteNonQuery();
                        execCount += rows;
                    }
                    _trans.Commit();
                }
            }
            catch (SqlException ex)
            {
                if (_trans != null) _trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                if (_trans != null) _trans.Rollback();
                throw ex;
            }
            finally
            {
                _conn.Close();
                _openConn = false;
            }
            return execCount;
        }

        public DataSet ExecCmd(IDbCommand cmd)
        {
            var ds = new DataSet();
            if (!_openConn)
            {
                _conn.Open();
                _openConn = true;
            }
            if (_trans == null)
            {
                _trans = _conn.BeginTransaction();
            }
            cmd.Connection = _conn;
            cmd.Transaction = _trans;
            var command = new SqlDataAdapter((SqlCommand)cmd);
            command.Fill(ds);
            return ds;
        }

        public void Open()
        {
            if (!_openConn)
            {
                _conn.Open();
                _openConn = true;
            }
        }

        public void AddCommands(IDbCommand cmd)
        {
            if (cmd.CommandText.IsNullOrEmpty())
            {
                return;
            }
            _cmds.Add(cmd);
        }

        public void Dispose()
        {
            if (_trans != null)
            {
                _trans.Dispose();
                _trans = null;
            }
            if (_conn != null)
            {
                _openConn = false;
                _conn.Close();
                _conn.Dispose();
                _conn = null;
            }
        }
    }
}