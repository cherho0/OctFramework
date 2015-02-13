using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    internal class SessionMgr
    {
        private readonly string _connStr = string.Empty;

        public ISession Session { get; private set; }

        public SessionMgr(string connstr)
        {
            _connStr = connstr;
        }

        public ISession GetCurrentSession()
        {
            if (Session == null)
            {
                Session = CurrentSessionFactory.OpenSession(_connStr);
            }

            return Session;
        }

        public void CreateSession()
        {

            Session = CurrentSessionFactory.OpenSession(_connStr);
        }
    }
}