using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oct.Framework.Core.Common;
using Oct.Framework.SignalRDemo.Models;

namespace Oct.Framework.SignalRDemo.Common
{
    public class SessionMgr : SingleTon<SessionMgr>
    {
        public static Dictionary<string, string> AllAcount = new Dictionary<string, string>();

        public const string UserKey = "singler";

        public T GetSession<T>(string key)
        {
            var id = HttpContext.Current.Session.SessionID;
            if (HttpContext.Current.Session[id+key] == null)
            {
                return default(T);
            }
            return (T)HttpContext.Current.Session[id+key];

        }

        public void SetSession(string key, object model)
        {
            var id = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session.Add(id+key, model);
            //HttpContext.Current.Session.Timeout = 5;//  5分钟超时
            //if (!AllAcount.ContainsKey(id))
            //{
            //    AllAcount.Add(id,model);
            //}
        }

        public void Remove(string key)
        {
            var id = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session.Remove(id+key);
            AllAcount.Remove(id);
        }

        //public List<T> GetAllSession<T>() where T : new() 
        //{

        //}
    }
}