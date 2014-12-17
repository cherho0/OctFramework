using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    internal class SessionMgr
    {
        private readonly string _connStr = string.Empty;

        public SessionMgr(string connstr)
        {
            _connStr = connstr;
        }

        public ISession GetCurrentSession()
        {
            ISession session = CurrentSessionFactory.GetCurrentSession();

            if (session == null)
            {
                session = CurrentSessionFactory.OpenSession(_connStr);
                CurrentSessionFactory.Bind(session);
            }

            return session;
        }

        public void CreateSession()
        {
            ISession session = CurrentSessionFactory.GetCurrentSession();

            if (session == null)
            {
                session = CurrentSessionFactory.OpenSession(_connStr);
                CurrentSessionFactory.Bind(session);
            }
        }
    }
}