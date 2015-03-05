using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Oct.Framework.Core.ApiData;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.Core.Json;
using Oct.Framework.Core.Log;
using Oct.Framework.SSO.Entity;

namespace Oct.Framework.SSO.Client
{
    public class SsoClient : SingleTon<SsoClient>
    {
        public const string UserName = "UserName";
        public const string Token = "token";
        private const string api = "api/getuser";

        public SsoUser GetUser()
        {
            //sso逻辑
            var token = HttpContext.Current.Request.QueryString["token"];
            if (token.IsNullOrEmpty())
            {
                token = CookieHelper.GetCookieValue("token");
            }
            //根据token 获取用户信息，并保存token
            if (!token.IsNullOrEmpty())
            {
                try
                {
                    var ssoserver = ConfigSettingHelper.GetAppStr("ssoserver");
                    var userinfojson = ApiDataHelper.GetData(ssoserver + api + "?token=" + token);
                    var user = JsonHelper.DeserializeObject<SsoUser>(userinfojson);
                    return user;
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            return null;
        }
    }
}
