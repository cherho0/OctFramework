using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Log;
using Oct.Framework.WinServiceKernel.Interfaces;

namespace Oct.Framework.WinServiceKernel.Core
{
    public static class Composition
    {
        private static System.ComponentModel.Composition.Hosting.CompositionContainer container;

        static Composition()
        {
            var pluginName = ConfigSettingHelper.GetAppStr("PluginName");
            var dirCatalog = new DirectoryCatalog(".", pluginName + ".*.dll");
            container = new System.ComponentModel.Composition.Hosting.CompositionContainer(dirCatalog);
        }

        private static object syncRoot = new object();

        internal static void Initialize(Kernel knl, LogicMgr logicMgr)
        {
            lock (syncRoot)
            {
                var itasks = container.GetExportedValues<IServise>();
                foreach (var servise in itasks)
                {
                    var lgc = (CoreLogic)servise;
                    lgc.Ctor(knl);
                    logicMgr.Add(lgc);
                    LogHelper.Info(lgc.Name + " loading...");
                    Csl.Wl(lgc.Name + " loading...");
                }
                knl.Ctor(logicMgr);
                var batch = new CompositionBatch();
                foreach (var logic in knl.GetLgcs())
                {
                    batch.AddPart(logic);
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
