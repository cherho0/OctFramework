using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Oct.Framework.Core.IOC
{
    public static class Bootstrapper
    {
        public static IUnityContainer Container;

        public static IUnityContainer CreateContainer()
        {
            Container =   new UnityContainer();
            return Container;
        }

        /// <summary>
        /// 依赖注入启动
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer Initialise()
        {
            Container = BuildUnityContainer();

            return Container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            Container = new UnityContainer();
            ConfigurationManager.GetSection("unity");
            UnityConfigurationSection.CurrentSection.Configure(Container);
            return Container;
        }

        /// <summary>
        /// 获取注入的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRepository<T>()
        {
            return Container.Resolve<T>();
        }
    }
}