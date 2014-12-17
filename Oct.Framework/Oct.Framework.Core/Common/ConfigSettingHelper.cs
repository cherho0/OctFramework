using System;
using System.Configuration;

namespace Oct.Framework.Core.Common
{
    public class ConfigSettingHelper
    {
        /// <summary>
        /// 获取app
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAppStr(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        /// <summary>
        /// 获取app
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetAppStr<T>(string name)
        {
            return (T)Convert.ChangeType(GetAppStr(name), typeof(T));
        }

        /// <summary>
        /// 获取connectionstr
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConnectionStr(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
