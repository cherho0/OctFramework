using System;

namespace Oct.Framework.DB.Attrbuites
{
    /// <summary>
    /// 标记属性对应的数据库字段是否为自增长
    /// </summary>
    [AttributeUsage( AttributeTargets.Property, Inherited = true )]
    public class IdentityAttribute : Attribute
    {
    }
}
