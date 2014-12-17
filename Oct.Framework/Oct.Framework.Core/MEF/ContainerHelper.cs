using System.ComponentModel.Composition.Hosting;

namespace Oct.Framework.Core.MEF
{
    public class ContainerHelper
    {
        /// <summary>
        /// 返回具有指定的协定名称的导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchPattern">搜索字符串</param>
        /// <param name="contractName">协定名称</param>
        /// <returns></returns>
        public static T GetExport<T>(string searchPattern, string contractName)
        {
            var dirCatalog = new DirectoryCatalog(".", searchPattern);
            var container = new CompositionContainer(dirCatalog);

            return container.GetExport<T>(contractName).Value;
        }
    }
}
