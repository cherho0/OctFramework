using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.Core.Log
{
    public interface IDbLog
    {
        /// <summary>
        /// 自定义日志添加，自定义日志必须继承baselog ，并且在系统启动时对类型进行注册 ： BsonClassMap.RegisterClassMap<UserLog>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="log"></param>
        void Add<T>(T log) where T : BaseLog, new();

        /// <summary>
        /// 添加数据变更日志
        /// </summary>
        /// <param name="log"></param>
        void AddDataLog(DataChangeLog log);

        /// <summary>
        /// 添加浏览日志
        /// </summary>
        /// <param name="log"></param>
        void AddViewLog(ViewLog log);

        /// <summary>
        /// 添加性能监测日志
        /// </summary>
        /// <param name="log"></param>
        void AddPerformanceLog(PerformanceLog log);
    }
}
