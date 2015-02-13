using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Oct.Framework.DB.DynamicObj
{
    /// <summary>
    /// 运行时访问对象成员
    /// </summary>
    public interface IMemberAccessor
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="instance">对象</param>
        /// <param name="memberName">属性名称</param>
        /// <returns>属性值</returns>
        object GetValue(object instance, string memberName);

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="instance">对象</param>
        /// <param name="memberName">属性名称</param>
        /// <param name="newValue">属性值</param>
        void SetValue(object instance, string memberName, object newValue);
    }

    public class DynamicMethod : IMemberAccessor
    {
        internal Func<object, string, object> GetValueDelegate;
        internal Action<object, string, object> SetValueDelegate;
        private Type _t;
        public DynamicMethod(Type type)
        {
            _t = type;
            GetValueDelegate = GenerateGetValue();
            SetValueDelegate = GenerateSetValue();
        }

        public object GetValue(object instance, string memberName)
        {
            return GetValueDelegate(instance, memberName);
        }

        public void SetValue(object instance, string memberName, object newValue)
        {
            SetValueDelegate(instance, memberName, newValue);
        }

        private Func<object, string, object> GenerateGetValue()
        {
            var type = _t;
            var instance = Expression.Parameter(typeof(object), "instance");
            var memberName = Expression.Parameter(typeof(string), "memberName");
            var nameHash = Expression.Variable(typeof(int), "nameHash");
            var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var property = Expression.Property(Expression.Convert(instance, type), propertyInfo.Name);
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
            }
            var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Func<object, string, object>>(methodBody, instance, memberName).Compile();
        }

        private Action<object, string, object> GenerateSetValue()
        {
            var type = _t;
            var instance = Expression.Parameter(typeof(object), "instance");
            var memberName = Expression.Parameter(typeof(string), "memberName");
            var newValue = Expression.Parameter(typeof(object), "newValue");
            var nameHash = Expression.Variable(typeof(int), "nameHash");
            var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var property = Expression.Property(Expression.Convert(instance, type), propertyInfo.Name);
                var setValue = Expression.Assign(property, Expression.Convert(newValue, propertyInfo.PropertyType));
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(setValue, typeof(object)), propertyHash));
            }
            var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Action<object, string, object>>(methodBody, instance, memberName, newValue).Compile();
        }
    }
}
