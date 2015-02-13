using System;

namespace Oct.Framework.DB.Attrbuites
{
    /// <summary>
    /// 标记属性对应的数据库字段是否为主键
    /// </summary>
    [AttributeUsage( AttributeTargets.Property, Inherited = true )]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}
