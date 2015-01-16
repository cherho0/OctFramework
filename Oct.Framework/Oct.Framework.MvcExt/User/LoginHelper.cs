using System;
using System.Web.Security;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Session;

namespace Oct.Framework.MvcExt.User
{
    /// <summary>
    /// 请使用单例对象，用户登录维护管理
    /// </summary>
    public class LoginHelper : SingleTon<LoginHelper>
    {
        protected ISessionProvider SessionProvider { get; set; }

        public LoginHelper()
        {
            SessionProvider = Bootstrapper.GetRepository<ISessionProvider>();
        }

        /// <summary>
        /// 创建登录对象
        /// </summary>
        /// <param name="func"></param>
        public void Login(Func<UserBase> func)
        {
            var user = func();
            SessionProvider.AddSession(ConstArgs.UserSession, user);
            FormsAuthentication.SetAuthCookie(user.Account, false);
        }

        /// <summary>
        /// 缓存权限
        /// </summary>
        /// <param name="func"></param>
        public void CacheRoles(Func<UserRoles> func)
        {
            var roles = func();
            SessionProvider.AddSession(ConstArgs.UserRole, roles);
        }

        /// <summary>
        /// 缓存权限
        /// </summary>
        /// <param name="func"></param>
        public void CacheMeuns(Func<UserMenus> func)
        {
            var menus = func();
            SessionProvider.AddSession(ConstArgs.UserMenu, menus);
        }

        /// <summary>
        /// 获取登录对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetLoginUser<T>() where T : UserBase
        {
            return SessionProvider.GetSession<T>(ConstArgs.UserSession);
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        public UserRoles GetLoginUserRoles()
        {
            return SessionProvider.GetSession<UserRoles>(ConstArgs.UserRole);
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        public UserMenus GetLoginUserMenus()
        {
            return SessionProvider.GetSession<UserMenus>(ConstArgs.UserMenu);
        }

        /// <summary>
        /// 判断是否登录了
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            return SessionProvider.GetSession<UserBase>(ConstArgs.UserSession) != null;
        }

        public void LogOut()
        {
            SessionProvider.Clear(ConstArgs.UserSession);
            SessionProvider.Clear(ConstArgs.UserRole);
            SessionProvider.Clear(ConstArgs.UserMenu);
            SessionProvider.ClearAll();
            FormsAuthentication.SignOut();
        }
    }
}
