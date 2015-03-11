using System.Collections.Generic;
using MX.IM.WebApp.Models;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;

namespace Oct.Framework.SignalR2.Models
{
    public class IMUserManager : SingleTon<IMUserManager>
    {
        private const string IMUserList = "OCTIMUserList";

        readonly ICacheHelper _cacheHelper = new RedisHelper();

        public List<IMUser> GetAllUsers(Platform plat)
        {
            var key = IMUserList + ((int)plat);
            return _cacheHelper.GetAll<IMUser>(key);
        }

        private string GetUserKey(IMUser user)
        {
            return IMUserList + ((int)user.Platform) + user.Account + user.ConnectionId;
        }

        private void CacheUser(IMUser user)
        {
            _cacheHelper.Set(GetUserKey(user), user);
        }

        public List<IMUser> GetUser(string guid)
        {
            return _cacheHelper.GetAll<IMUser>(guid);
        }

        public void Add(string acc,string connid, Platform platform)
        {
            var user = new IMUser()
            {
                Account = acc,
                ConnectionId = connid,
                Platform = platform,
            };
            CacheUser(user);
        }

        public void Remove(string connid)
        {
            _cacheHelper.RemoveAll(connid);
        }

    }
}