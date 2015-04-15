using System;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    public class DBContextBase : IDisposable
    {
        private readonly string _connstr = string.Empty;
        private SessionMgr _currentSessionMgr;

        protected ISession Session
        {
            get { return _currentSessionMgr.GetCurrentSession(); }
        }

        public DBContextBase()
        {
            _connstr = ConfigSettingHelper.GetConnectionStr(ConstArgs.ConnStrName);
            Init();
        }

        public DBContextBase(string connectionString)
        {
            _connstr = connectionString;
            Init();
        }

        public DBContextBase(DBOperationType dbType)
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
            CurrentSessionFactory.Clear();
        }

        private void Init()
        {
            _currentSessionMgr = new SessionMgr(_connstr);
            _currentSessionMgr.CreateSession();
        }

        public int SaveChanges()
        {
            var cout = _currentSessionMgr.GetCurrentSession().Commit();
            return cout;
        }

        public IDBContext<T> GetContext<T>() where T : BaseEntity<T>, new()
        {
            IDBContext<T> context = new EntityContext<T>(_currentSessionMgr.GetCurrentSession());
            return context;
        }
    }
}