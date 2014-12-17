using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Oct.Tools.Plugin.CodeGenerator.Entity
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    [Serializable]
    public class DBInfo
    {
        #region 属性

        /// <summary>
        /// 标识符
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 表集合
        /// </summary>
        [XmlIgnore]
        public List<string> TableList
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public DBInfo()
        {
            this.TableList = new List<string>();
        }

        #endregion
    }
}
