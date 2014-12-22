using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.Core.Session
{
    public interface ISessionProvider
    {
        /// <summary>
        /// 新增session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool AddSession(string key, object value);

        /// <summary>
        /// 添加session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("目前默认使用系统session时间")]
        bool AddSession(string key, object value, int timeOut);

        /// <summary>
        /// 获取session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetSession<T>(string key);

        void Clear(string key);

        void ClearAll();
    }
}
