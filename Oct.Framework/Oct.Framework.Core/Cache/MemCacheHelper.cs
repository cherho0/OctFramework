using System;
using System.Collections.Generic;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace Oct.Framework.Core.Cache
{
    internal class MemCacheHelper : ICacheHelper
    {
        private MemcachedClient mc = null;

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

        public List<T> GetAll<T>(string cacheKeyPrefix)
        {
            return null;
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

        }

        public void Save()
        {

        }
    }
}
