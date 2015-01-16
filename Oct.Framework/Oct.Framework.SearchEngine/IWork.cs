using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.SearchEngine
{
    public interface IWork
    {
        /// <summary>
        /// 按照设定好的时间执行任务
        /// </summary>
        void DoWork();

        /// <summary>
        /// 执行类型
        /// </summary>
        DoWorkStyle Style { get; }

        /// <summary>
        /// 直接执行一次任务
        /// </summary>
        void OneDoWork();

        /// <summary>
        /// 更新一个单独对象索引，关键字，值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="id">值</param>
        void UpdateUnitDoc(string key, object id);

        /// <summary>
        /// 删除一个索引
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="id">值</param>
        void DeleteDoc(string key, object id);
    }
}
