using System;
using System.Collections.Generic;

namespace Oct.Tools.Plugin.CodeGenerator.Entity
{
    [Serializable]
    public class CodeBaseInfo
    {
        #region 属性

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace
        {
            get;
            set;
        }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName
        {
            get;
            set;
        }

        /// <summary>
        /// 类名扩展
        /// </summary>
        public string ClassNameExtend
        {
            get;
            set;
        }

        /// <summary>
        /// Sql语句
        /// </summary>
        public string Sql
        {
            get;
            set;
        }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<FiledInfo> FiledList
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public CodeBaseInfo()
        {
            this.Sql = string.Empty;
            this.FiledList = new List<FiledInfo>();
        }

        #endregion
    }

    [Serializable]
    public class FiledInfo
    {
        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int No
        {
            get;
            set;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// CSharp类型
        /// </summary>
        public string CSharpType
        {
            get;
            set;
        }

        /// <summary>
        /// 通用类型
        /// </summary>
        public Type CommonType
        {
            get;
            set;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get;
            set;
        }

        /// <summary>
        /// 小数位
        /// </summary>
        public int DecimalPlace
        {
            get;
            set;
        }

        /// <summary>
        /// 是否标识列
        /// </summary>
        public bool IsIdentify
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPk
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool IsAllowNull
        {
            get;
            set;
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取显示名称
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            return string.IsNullOrEmpty(this.Description) ? this.Name : this.Description;
        }

        #endregion
    }
}
