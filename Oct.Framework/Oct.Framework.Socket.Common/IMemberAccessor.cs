namespace NyMQ.Common
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
}
