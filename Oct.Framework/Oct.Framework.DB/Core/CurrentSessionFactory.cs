using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Core
{
    /// <summary>
    /// session 工厂类
    /// </summary>
    internal class CurrentSessionFactory
    {
        private static ConcurrentDictionary<int, ISession> _sessions
            = new ConcurrentDictionary<int, ISession>();

        private static ISession _session;

        [Obsolete("已弃用")]
        public static void Bind(ISession session)
        {
            _sessions.TryAdd(CurThId, session);
        }

        private static int CurThId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        private static ISession GetCurrentSession()
        {
            if (_sessions == null || !_sessions.ContainsKey(CurThId))
            {
                return null;
            }
            return _sessions[CurThId];
        }

        public static ISession OpenSession(string connstr)
        {
            var session = new DBSession(connstr);
            return session.CreateSession();
        }

        private static HttpContext GetCurrentRequert()
        {
            return HttpContext.Current;
        }

        private static void Clear()
        {
            ISession s;
            _sessions.TryRemove(CurThId, out s);

        }
    }
}