using System;
using Oct.Framework.Core;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    public abstract class DBContextBase : IDisposable
    {
        private readonly string _connstr = string.Empty;
        private SessionMgr _currentSessionMgr;

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
            get { return Bootstrapper.GetRepository<ISQLContext>(); }
        }

        public void Dispose()
        {
            if (CurrentSessionFactory.GetCurrentSession() != null)
            {
                CurrentSessionFactory.GetCurrentSession().Dispose();
                CurrentSessionFactory.Clear();
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
            return Bootstrapper.GetRepository<IDBContext<T>>();
        }
    }
}