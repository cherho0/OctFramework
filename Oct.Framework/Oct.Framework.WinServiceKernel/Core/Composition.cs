using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using Oct.Framework.Core.Common;

namespace Oct.Framework.WinServiceKernel.Core
{
    public static class Composition
    {
        private static System.ComponentModel.Composition.Hosting.CompositionContainer container;

        static Composition()
        {
            var pluginName = ConfigSettingHelper.GetAppStr("PluginName");
            var dirCatalog = new DirectoryCatalog(".", pluginName+".*.dll");
            container = new System.ComponentModel.Composition.Hosting.CompositionContainer(dirCatalog);
        }

        private static object syncRoot = new object();

        internal static void Initialize(Kernel knl, IUnityContainer boot)
        {
            lock (syncRoot)
            {
                var batch = new CompositionBatch();
                foreach (var logic in knl.GetLgcs())
                {
                    batch.AddPart(logic);
                }
                foreach (var containerRegistration in boot.Registrations)
                {
                    batch.AddPart(containerRegistration);
                }

                try
                {
                    container.Compose(batch);
                }
                catch (CompositionException compositionException)
                {
                    Debug.WriteLine(compositionException.ToString());
                    throw;
                }
            }
        }

        public static void ComposeParts(params object[] attributedParts)
        {
            lock (syncRoot)
            {
                try
                {
                    container.ComposeParts(attributedParts);
                }
                catch (CompositionException compositionException)
                {
                    Debug.WriteLine(compositionException.ToString());
                    throw;
                }
            }
        }

    }
}
