using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;

namespace Oct.Framework.MvcExt.User
{
    /// <summary>
    /// 用户权限管理
    /// </summary>
    [Serializable]
    public class UserRoles
    {
        public Dictionary<string, OctRole> Roles { get; set; }

        public UserRoles()
        {
            Roles = new Dictionary<string, OctRole>();
        }

        /// <summary>
        /// 添加可访问地址
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="enName"></param>
        /// <param name="cnName"></param>
        public void AddRole(string controllerName, string actionName, string enName, string cnName)
        {
            var url = (controllerName + "/" + actionName).ToLower();

            if (!Roles.ContainsKey(url))
            {
                var role = new OctRole()
                {
                    ControllerName = controllerName,
                    ActionName = actionName,
                    EnName = enName,
                    CnName = cnName
                };
                Roles.Add(url, role);
            }
        }

        /// <summary>
        /// 添加可访问地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="enName"></param>
        /// <param name="cnName"></param>
        public void AddRole(string url, string enName, string cnName)
        {
            url = url.ToLower();
            if (!Roles.ContainsKey(url))
            {
                var strs = url.Split('/');
                var a = strs[strs.Length - 1];
                var c = strs[strs.Length - 2];
                var role = new OctRole()
                {
                    ControllerName = c,
                    ActionName = a,
                    EnName = enName,
                    CnName = cnName
                };
                Roles.Add(url, role);
            }
        }


        /// <summary>
        /// 判断某地址是否可访问
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool CheckRole(string controllerName, string actionName, string area = "")
        {
            var url = ("/" + controllerName + "/" + actionName);
            if (!area.IsNullOrEmpty())
            {
                url = "/" + area + url;
            }
            url = url.ToLower();
            return Roles.ContainsKey(url);
        }

        /// <summary>
        /// 判断某地址是否可访问
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckRole(string url)
        {
            url = url.ToLower();
            return Roles.ContainsKey(url);
        }

        public OctRole GetRole(string controllerName, string actionName, string area = "")
        {
            var url = ("/" + controllerName + "/" + actionName);
            if (!area.IsNullOrEmpty())
            {
                url = "/" + area + url;
            }
            url = url.ToLower();
            if (Roles.ContainsKey(url))
            {
                return Roles[url];
            }

            return null;
        }

        public OctRole GetRole(string url)
        {
            url = url.ToLower();
            if (Roles.ContainsKey(url))
            {
                return Roles[url];
            }

            return null;
        }
    }

    [Serializable]
    public class OctRole
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string EnName { get; set; }

        public string CnName { get; set; }


    }
}
