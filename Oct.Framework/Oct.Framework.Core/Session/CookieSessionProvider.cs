using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace Oct.Framework.Core.Session
{
    public class CookieSessionProvider : ISessionProvider
    {
        public string SessionId
        {
            get { return HttpContext.Current.Session.SessionID; }
        }

        public bool AddSession(string key, object value)
        {
            var timeOut = HttpContext.Current.Session.Timeout;
            return AddSession(key, value, timeOut);

        }

        public bool AddSession(string key, object value, int timeOut)
        {
            var data = JsonConvert.SerializeObject(value);

            //创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
            var ticket = new FormsAuthenticationTicket(2,
               key, DateTime.Now, DateTime.Now.AddMinutes(timeOut), true, data);

            //加密Ticket，变成一个加密的字符串。
            var cookieValue = FormsAuthentication.Encrypt(ticket);
            if (cookieValue != null && cookieValue.Length > 4000)
            {
                throw new Exception("您要保存的数据超过cookie的最大存储空间");
            }
            //根据加密结果创建登录Cookie
            var cookie = new HttpCookie(key, cookieValue);

            var context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();
            try
            {
                //写登录Cookie
                context.Response.Cookies.Remove(cookie.Name);
                context.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public object GetSession(string key)
        {
            try
            {
                var context = HttpContext.Current;
                var cookie = context.Request.Cookies[key];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var obj = JsonConvert.DeserializeObject(ticket.UserData);
                var timeOut = HttpContext.Current.Session.Timeout;
                AddSession(key, obj, timeOut);
                return obj;
            }
            catch
            {
                return null;
            }
        }

        public T GetSession<T>(string key)
        {
            try
            {
                var context = HttpContext.Current;
                var cookie = context.Request.Cookies[key];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var obj = JsonConvert.DeserializeObject<T>(ticket.UserData);
                var timeOut = HttpContext.Current.Session.Timeout;
                AddSession(key, obj, timeOut);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        public void Clear(string key)
        {
            var context = HttpContext.Current;
            var cookie = context.Request.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public void ClearAll()
        {
            HttpContext.Current.Response.Cookies.Clear();
        }
    }
}
