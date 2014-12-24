using System;
using Oct.Framework.Core;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    public abstract class DBContextBase : IDisposable
    {
        private readonly string _connstr = string.Empty;
        private SessionMgr _currentSessionMgr;

        protected ISession Session
        {
            get { return _currentSessionMgr.GetCurrentSession(); }
        }

        protected DBContextBase()
        {
            _connstr = ConfigSettingHelper.GetConnectionStr(ConstArgs.ConnStrName);
            Init();
        }

        protected DBContextBase(string connectionString)
        {
            _connstr = connectionString;
            Init();
        }

        protected DBContextBase(DBOperationType dbType)
        {
            if (dbType == DBOperationType.Read)
            {
                _connstr = ConfigSettingHelper.GetConnectionStr(ConstArgs.ReadConnStrName);
                Init();
            }

            if (dbType == DBOperationType.Write)
            {
                _connstr = ConfigSettingHelper.GetConnectionStr(ConstArgs.WriteConnStrName);
                Init();
            }
        }

        public ISQLContext SQLContext
        {
            get
            {
                ISQLContext sqlContext = new SQLContext(_currentSessionMgr.GetCurrentSession());
                return sqlContext;
            }
        }

        public void Dispose()
        {
            if (_currentSessionMgr.GetCurrentSession() != null)
            {
                _currentSessionMgr.GetCurrentSession().Dispose();
            }
            _currentSessionMgr = null;
        }

        private void Init()
        {
            _currentSessionMgr = new SessionMgr(_connstr);
            _currentSessionMgr.CreateSession();
        }

        public int SaveChanges()
        {
            return _currentSessionMgr.GetCurrentSession().Commit();
        }

        public IDBContext<T> GetContext<T>() where T : BaseEntity<T>, new()
        {
            IDBContext<T> context = new SQLDBContext<T>(_currentSessionMgr.GetCurrentSession());
            return context;
        }
    }
}