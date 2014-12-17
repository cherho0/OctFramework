using System.Web;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    internal class CurrentSessionFactory
    {
        private const string OCTSESSION = "OCTSESSION";
        private static ISession _session;

        public static void Bind(ISession session)
        {
            HttpContext context = GetCurrentRequert();
            if (context == null)
            {
                _session = session;
            }
            else
            {
                context.Session[OCTSESSION] = session;
            }
        }

        public static ISession GetCurrentSession()
        {
            HttpContext context = GetCurrentRequert();
            if (context == null)
            {
                return _session;
            }
            return (ISession) context.Session[OCTSESSION];
        }

        public static ISession OpenSession(string connstr)
        {
            var session = new DBSession(connstr);
            session.CreateSession();
            return session;
        }

        private static HttpContext GetCurrentRequert()
        {
            return HttpContext.Current;
        }

        public static void Clear()
        {
            _session = null;
            HttpContext context = GetCurrentRequert();
            if (context != null)
            {
                context.Session[OCTSESSION] = null;
            }
        }
    }
}