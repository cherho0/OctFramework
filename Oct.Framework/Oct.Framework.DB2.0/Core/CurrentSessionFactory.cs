using System;
using System.Collections.Concurrent;
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
        private const string DbSessionKey = "DbSession";
        private static object o = new object();
        private static ConcurrentDictionary<int, ISession> _sessions
            = new ConcurrentDictionary<int, ISession>();
        private static int CtId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        private static string SessionKey
        {
            get { return DbSessionKey + HttpContext.Current.Session.SessionID + CtId; }
        }

        private static ISession _session;

        private static void Bind(ISession session)
        {
            if (HttpContext.Current == null)
            {
                if (_sessions == null)
                {
                    lock (o)
                    {
                        if (_sessions == null)
                        {
                            _sessions = new ConcurrentDictionary<int, ISession>();
                        }
                    }
                }
                _sessions.TryAdd(CtId, session);
            }
            else
            {
                HttpContext.Current.Cache.Insert(SessionKey, session);
            }
        }

        private static int CurThId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        public static ISession GetCurrentSession()
        {
            if (HttpContext.Current == null)
            {
                if (_sessions == null || !_sessions.ContainsKey(CtId))
                {
                    //Console.WriteLine(CtID);
                    throw new Exception("请先创建Session,创建数据关系上下文");
                }
                //Console.WriteLine(CtID);
                return _sessions[CtId];
            }
            else
            {
                var session = HttpContext.Current.Cache[SessionKey];
                //var session = HttpContext.Current.Session[DbSessionKey];
                if (session == null)
                {
                    throw new Exception("请先创建Session,创建数据关系上下文");
                }

                return (ISession)session;
            }

        }

        public static ISession OpenSession(string connstr)
        {
            var session = new DBSession(connstr);
            session.CreateSession();
            Bind(session);
            return session;
        }


        public static void Clear()
        {
            if (_sessions != null)
            {
                ISession s;
                _sessions.TryRemove(CurThId, out s);
            }

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Remove(SessionKey);
            }
        }
    }
}