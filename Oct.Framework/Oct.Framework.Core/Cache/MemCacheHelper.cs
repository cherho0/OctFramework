using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace Oct.Framework.Core.Cache
{
    internal class MemCacheHelper : ICacheHelper
    {
        private MemcachedClient mc = null;
        MemcachedClientSection DefaultSettings = (ConfigurationManager.GetSection("enyim.com/memcached") as MemcachedClientSection);
        public MemCacheHelper()
        {
            try
            {

               
                mc = new MemcachedClient();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Exists(string key)
        {
            return true;
            //return mc.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (mc == null)
            {
                return default(T);
            }
            if (key == null)
                return default(T);
            return mc.Get<T>(key);
        }

        public object Get(string key)
        {
            if (mc == null)
            {
                return null;
            }
            if (key == null)
                return null;
            return mc.Get(key);
        }

        public List<string> GetAllKey()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllKey(string cacheKeyPrefix)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll<T>(string cacheKeyPrefix)
        {
            return null;
        }

        public List<T> GetAll<T>(List<string> keys)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value)
        {
            try
            {
                if (mc == null)
                {
                    return false;
                }
                return mc.Store(StoreMode.Set, key, value);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        public bool Set<T>(string key, T value, int minutes)
        {
            if (mc == null)
            {
                return false;
            }
            return mc.Store(StoreMode.Set, key, value, DateTime.Now.AddMinutes(minutes));
        }

        public bool Set<T>(string key, T value, TimeSpan ts)
        {
            if (mc == null)
            {
                return false;
            }
            return mc.Store(StoreMode.Set, key, value, ts);

        }

        public bool Remove(string key)
        {
            return mc.Remove(key);
        }

        public void RemoveAll(string cacheKeyPrefix)
        {
            var s = mc.Stats();
        }

        public void RemoveAll(List<string> keys)
        {
            throw new NotImplementedException();
        }

        public void FlushAll()
        {
            mc.FlushAll();
        }


        public void Save()
        {

        }
    }
}
