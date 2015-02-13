using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Oct.Framework.Core.Common;

namespace Oct.Framework.Core.Session
{
    public class CacheSessionProvider : ISessionProvider
    {
        public bool AddSession(string key, object value)
        {
            return CacheSessionHelper.Instance.Add(key, value);
        }

        public bool AddSession(string key, object value, int tomeOut)
        {
            return CacheSessionHelper.Instance.Add(key, value, tomeOut);
        }

        public object GetSession(string key)
        {
            return CacheSessionHelper.Instance.ReleaseObj(key);
        }

        public T GetSession<T>(string key)
        {
            return CacheSessionHelper.Instance.Get<T>(key);
        }

        public void Clear(string key)
        {
            if (HttpContext.Current != null)
            {
                CacheSessionHelper.Instance.Remove(key);
            }
        }

        public void ClearAll()
        {
            if (HttpContext.Current != null)
            {
                CacheSessionHelper.Instance.ClearAll();

                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }
    }
}
