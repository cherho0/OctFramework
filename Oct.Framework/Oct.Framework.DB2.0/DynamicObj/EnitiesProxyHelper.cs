using System;
using System.Collections.Generic;
using System.Reflection;
using Oct.Framework.DB.Attrbuites;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Emit.Utils;

namespace Oct.Framework.DB.DynamicObj
{
    public class EnitiesProxyHelper
    {
        private static Dictionary<Type, IMemberAccessor> _dicts;

        private static Dictionary<Type, ClassProxyInfo> _proxyInfos;

        public static IMemberAccessor GetDynamicMethod<T>()
        {
            if (_dicts.ContainsKey(typeof(T)))
            {
                return _dicts[typeof(T)];
            }
            else
            {
                var m = new DynamicMethod(typeof(T));
                _dicts.Add(typeof(T), m);
                return m;
            }
        }

        public static ClassProxyInfo GetProxyInfo<T>()
        {
            if (_proxyInfos.ContainsKey(typeof(T)))
            {
                return _proxyInfos[typeof(T)];
            }
            else
            {
                CreateProxy(typeof(T));
                return _proxyInfos[typeof(T)];
            }
        }


        static EnitiesProxyHelper()
        {
            _dicts = new Dictionary<Type, IMemberAccessor>();
            _proxyInfos = new Dictionary<Type, ClassProxyInfo>();
        }

        public static void Register(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (_dicts.ContainsKey(type))
                {
                    continue;
                }
                var b = type.BaseType;
                if (b != null && b.FullName.Contains("BaseEntity"))
                {
                    var m = new DynamicMethod(type);
                    _dicts.Add(type, m);
                    CreateProxy(type);
                }
            }
        }

        private static void CreateProxy(Type type)
        {
            if (_proxyInfos.ContainsKey(type))
            {
                return;
            }
            var proxy = new ClassProxyInfo();
            var members = ReflectionUtils.GetPublicFieldsAndProperties(type);
            var attr = type.GetEntityAttribute();
            if (attr == null)
            {
                throw new Exception("当前实体对象无法进行注册：" + type.FullName);
            }
            proxy.IsCompositeQuery = attr.IsCompositeQuery;
            proxy.TableName = attr.TableName;
            proxy.CompositeSql = attr.Sql;
            foreach (var memberInfo in members)
            {
                if (memberInfo is PropertyInfo)
                {
                    var prop = ((PropertyInfo)memberInfo);
                    if (prop.IsPrimaryKey())
                    {
                        proxy.PrimaryKeys.Add(prop.Name);
                    }
                    if (prop.IsAutoIncrease())
                    {
                        proxy.IdentitesProp = prop.Name;
                    }
                }
            }
            _proxyInfos.Add(type, proxy);
        }

        public static void Register(Type type)
        {
            if (_dicts.ContainsKey(type))
            {
                return;
            }
            var m = new DynamicMethod(type);
            _dicts.Add(type, m);
        }
    }
}
