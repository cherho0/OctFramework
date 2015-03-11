using System;
using System.Collections.Generic;
using System.Configuration;
using Oct.Framework.Core.Common;
using ServiceStack.Redis;

namespace Oct.Framework.Core.Cache
{
    public class RedisHelper : ICacheHelper
    {
        #region << 静态属性 >>

        /// <summary>
        /// 读数据Redis服务IP地址端口号
        /// </summary>
        private static string[] _ReadClientHosts;

        /// <summary>
        /// 读数据Redis服务IP地址端口号
        /// </summary>
        private static string[] ReadClientHosts
        {
            get
            {
                if (null == _ReadClientHosts)
                {
                    string readClientHostsString = ConfigurationManager.AppSettings[ConstArgs.RedisReadServer];
                    _ReadClientHosts = readClientHostsString.Split(',');
                }
                return _ReadClientHosts;
            }
        }

        /// <summary>
        /// 写数据Redis服务IP地址端口号
        /// </summary>
        private static string[] _WriteClientHosts;

        /// <summary>
        /// 写数据Redis服务IP地址端口号
        /// </summary>
        private static string[] WriteClientHosts
        {
            get
            {
                if (null == _WriteClientHosts)
                {
                    string writeClientHostsString = ConfigurationManager.AppSettings[ConstArgs.RedisWriteServer];
                    _WriteClientHosts = writeClientHostsString.Split(',');
                }
                return _WriteClientHosts;
            }
        }

        /// <summary>
        /// RedisClient链接池管理
        /// </summary>
        private static PooledRedisClientManager _RedisClientManager;

        /// <summary>
        /// RedisClient链接池管理
        /// </summary>
        private PooledRedisClientManager RedisClientManager
        {
            get
            {
                
                if (null == _RedisClientManager)
                {
                    _RedisClientManager = CreateManager(WriteClientHosts, ReadClientHosts);
                }
                return _RedisClientManager;
            }
        }

        /// <summary>
        /// 创建缓存客户端链接池管理
        /// </summary>
        /// <param name="readWriteHosts">读写服务器Host</param>
        /// <param name="readOnlyHosts">只读数据库Host</param>
        /// <returns></returns>
        private PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //支持读写分离，均衡负载
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                //“写”链接池链接数
                MaxWritePoolSize = 5000,
                //“读”链接池链接数
                MaxReadPoolSize = 5000,
                AutoStart = true,
            });

        }

        /// <summary>
        /// 得到用于读取的客户端对象
        /// </summary>
        /// <returns>只读客户端对象</returns>
        public IRedisClient GetReadClient()
        {
            return RedisClientManager.GetReadOnlyClient();
        }

        /// <summary>
        /// 得到用于写入的客户端对象
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetWriteClient()
        {
            return RedisClientManager.GetClient();
        }

        #endregion

        public bool Exists(string key)
        {
            using (IRedisClient client = GetReadClient())
            {
                return client.ContainsKey(key);
            }
        }

        public T Get<T>(string key)
        {
            using (IRedisClient client = GetReadClient())
            {
                return client.Get<T>(key);
            }
        }

        public object Get(string key)
        {
            using (IRedisClient client = GetReadClient())
            {
                return client.Get<object>(key);
            }
        }

        public List<string> GetAllKey()
        {
            using (IRedisClient client = GetReadClient())
            {
                return client.GetAllKeys();
            }
        }

        public List<string> GetAllKey(string cacheKeyPrefix)
        {
            using (IRedisClient client = GetReadClient())
            {
               return client.SearchKeys("*" + cacheKeyPrefix + "*");
            }
        }

        public List<T> GetAll<T>(string cacheKeyPrefix)
        {
            using (IRedisClient client = GetReadClient())
            {
                var keys = client.SearchKeys("*" + cacheKeyPrefix + "*");
                return client.GetValues<T>(keys);
            }
        }

        public List<T> GetAll<T>(List<string> keys)
        {
            using (IRedisClient client = GetReadClient())
            {
                return client.GetValues<T>(keys);
            }
        }

        public bool Set<T>(string key, T value)
        {
            using (IRedisClient client = GetWriteClient())
            {
                return client.Set<T>(key, value);
            }
        }

        public bool Set<T>(string key, T value, int minutes)
        {
            using (IRedisClient client = GetWriteClient())
            {
                return client.Set<T>(key, value, DateTime.Now.AddMinutes(minutes));
            }
        }

        public bool Set<T>(string key, T value, TimeSpan ts)
        {
            using (IRedisClient client = GetWriteClient())
            {
                return client.Set<T>(key, value, ts);
            }
        }

        public bool Remove(string key)
        {
            using (IRedisClient client = GetWriteClient())
            {
                return client.Remove(key);
            }
        }

        public void RemoveAll(string cacheKeyPrefix)
        {
            using (IRedisClient client = GetWriteClient())
            {
                var keys = client.SearchKeys("*" + cacheKeyPrefix + "*");
                foreach (var key in keys)
                {
                    client.Remove(key);
                }
            }
        }

        public void RemoveAll(List<string> keys)
        {
            using (IRedisClient client = GetWriteClient())
            {
                foreach (var key in keys)
                {
                    client.Remove(key);
                }
            }
        }

        public void FlushAll()
        {
            using (IRedisClient client = GetWriteClient())
            {
                client.FlushAll();
            }
        }

        public void Save()
        {
            using (IRedisClient client = GetWriteClient())
            {
                client.Save();
            }
        }
    }
}
