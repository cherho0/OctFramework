using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Oct.Framework.Core.Log
{
    /// <summary>
    /// 页面浏览日志
    /// </summary>
    public class ViewLog : BaseLog
    {
        /// <summary>
        /// 浏览控制
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// action
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 浏览url
        /// </summary>
        public string Para { get; set; }
        /// <summary>
        /// 浏览名称
        /// </summary>
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string OpertingUser { get; set; }
        /// <summary>
        /// 执行的操作
        /// </summary>
        public string Operation { get; set; }

        public string FromIp { get; set; }

        public string ViewType { get; set; }
    }
}
