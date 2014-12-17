using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Oct.Framework.Core.Log
{
       /// <summary>
    /// 操作数据变更日志
    /// </summary>
    [Serializable]
    public class DataChangeLog : BaseLog
    {
        /// <summary>
        /// 操作模块名称
        /// </summary>
        public string OperObjName { get; set; }

        /// <summary>
        /// 数据变更日志
        /// </summary>
        public List<ValueLog> ValueLogs { get; set; }

        /// <summary>
        /// 模块主键
        /// </summary>
        public string OperID { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperUser { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime OperDate { get; set; }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        public void Add(ValueLog log)
        {
            if (ValueLogs == null)
            {
                ValueLogs = new List<ValueLog>();
            }
            ValueLogs.Add(log);
        }
    }

    [Serializable]
    public class ValueLog 
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string CNName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
