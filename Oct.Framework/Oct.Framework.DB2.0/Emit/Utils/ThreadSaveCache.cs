using System;
using System.Collections.Generic;

namespace Oct.Framework.DB.Emit.Utils
{
    class ThreadSaveCache
    {
        Dictionary<string, object> _cache = new Dictionary<string,object>();

        public T Get<T>(string key, Func<object> getter)
        {
            lock(_cache)
            {
                object value;
                if(!_cache.TryGetValue(key, out value))
                {
                    value = getter();
                    _cache[key] = value;
                }
                return (T)value;
            }
        }
    }
}
