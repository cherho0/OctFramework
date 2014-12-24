using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;

namespace Oct.Framework.Core.Session
{
    internal class CacheSessionHelper : SingleTon<CacheSessionHelper>
    {
        public string SessionId
        {
            get { return HttpContext.Current.Session.SessionID; }
        }

        public object this[string key]
        {
            get
            {
                return ReleaseObj(key);
            }
            set
            {
                Add(key, value);
            }
        }

        public ICacheHelper CacheHelper
        {
            get { return Bootstrapper.GetRepository<ICacheHelper>(); }
        }

        /// <summary>
        /// 添加过期20
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeOut"></param>
        public bool Add(string key, object value, int timeOut = -1)
        {   
            if (timeOut == -1)
            {
                timeOut = HttpContext.Current.Session.Timeout;
            }
            return CacheHelper.Set(SessionId + "_" + key, value, timeOut);

        }

        /// <summary>
        /// 处理session对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object ReleaseObj(string key)
        {
            var timeOut = HttpContext.Current.Session.Timeout;
            var session = CacheHelper.Get(SessionId + "_" + key);
            if (session != null)
            {
                //CacheHelper.Set(SessionId + "_" + key, session, timeOut);
            }
            return session;
        }

        public void Remove(string key)
        {
            CacheHelper.Remove(SessionId + "_" + key);
        }


        public void ClearAll()
        {
            CacheHelper.RemoveAll(SessionId);
        }

        public T Get<T>(string key)
        {
            var timeOut = HttpContext.Current.Session.Timeout;
            var session = CacheHelper.Get<T>(SessionId + "_" + key);
            if (session != null)
            {
                CacheHelper.Set(SessionId + "_" + key, session, timeOut);
            }
            return session;
        }
    }
}
