using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oct.Framework.Core.Session
{
    internal class SysSessionProvider : ISessionProvider
    {
        public bool AddSession(string key, object value)
        {
            HttpContext.Current.Session.Add(key, value);
            return true;
        }

        public bool AddSession(string key, object value, int timeOut)
        {
            HttpContext.Current.Session.Timeout = timeOut;
            HttpContext.Current.Session.Add(key, value);
            return true;
        }

        public object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public T GetSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public void Clear(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public void ClearAll()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}
