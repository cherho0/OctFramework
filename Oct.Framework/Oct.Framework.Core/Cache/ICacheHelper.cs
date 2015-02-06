using System;
using System.Collections.Generic;

namespace Oct.Framework.Core.Cache
{
    public interface ICacheHelper
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        ///获取全部key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKeyPrefix"></param>
        /// <returns></returns>
        List<string> GetAllKey();

        /// <summary>
        ///获取这个前缀的全部key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKeyPrefix"></param>
        /// <returns></returns>
        List<string> GetAllKey(string cacheKeyPrefix);

        /// <summary>
        /// 获取一系列对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKeyPrefix"></param>
        /// <returns></returns>
        List<T> GetAll<T>(string cacheKeyPrefix);
        List<T> GetAll<T>(List<string> keys);
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value);

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, int minutes);
        bool Set<T>(string key, T value, TimeSpan ts);

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
        void RemoveAll(string cacheKeyPrefix);
        void RemoveAll(List<string> keys);
        void FlushAll();
        /// <summary>
        /// 仅redis支持
        /// </summary>
        void Save();
    }
}
