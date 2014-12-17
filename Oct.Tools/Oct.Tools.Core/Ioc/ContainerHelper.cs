using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace Oct.Tools.Core.Ioc
{
    public class ContainerHelper
    {
        /// <summary>
        /// 获取具有从指定的类型参数派生的协定名称的所有已导出对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">要扫描要添加目录的程序集的目录路径</param>
        /// <returns></returns>
        public static IEnumerable<T> GetExport<T>(string path)
        {
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);

            return container.GetExportedValues<T>();
        }
    }
}
