using System;
using System.IO;
using System.Reflection;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.WinServiceKernel.Core;
using Oct.Framework.WinServiceKernel.Util;

namespace Oct.Framework.WinServiceKernel
{
    public class FrameworkServer
    {
        private Kernel _knl;
        private LogicMgr _lgcs;
        public FrameworkServer()
        {
            var boot = Bootstrapper.Initialise();
            log4net.Config.XmlConfigurator.Configure();
            Csl.Wl("开始构建内核...");
            _knl = new Kernel();
            LoadLgc();
            _knl.Ctor(_lgcs);
            Csl.Wl("开始进行MEF注入，可能需要1-5秒。。。");
            Composition.Initialize(_knl, boot);
            Csl.Wl("开始加载模块...");
            LoadLgcs();
            Csl.Wl("模块加载完成。");
            Csl.Wl("内核构建完成。");
            LogsHelper.Info("内核构建完成。");
            Csl.Wl(ConsoleColor.Yellow, "输入help查看相关command操作。");
            Csl.Wl(ConsoleColor.Yellow, "输入exit退出。");
        }

        private void LoadLgcs()
        {
            foreach (var lgc in _lgcs.GetAllLgcs())
            {
                lgc.Load();
            }
        }

        private void LoadLgc()
        {
            Csl.Wl("开始加载逻辑模块...");
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            LogsHelper.Info("开始加载逻辑模块..." + assemblyDirPath);
            _lgcs = new LogicMgr();
            var pluginName = ConfigSettingHelper.GetAppStr("PluginName");
            foreach (string file in Directory.GetFiles(assemblyDirPath))
            {
                var fi = new FileInfo(file);

                if (fi.Extension == ".dll" && fi.FullName.Contains(pluginName+"."))
                {
                    var asm = Assembly.LoadFile(fi.FullName);
                    LoadLgc(asm);
                }
            }
        }

        private void LoadLgc(Assembly asm)
        {
            foreach (Type i in asm.GetExportedTypes())
            {
                if (!i.IsAbstract && (i.IsSubclassOf(typeof(CoreLogic)) && (i.GetConstructor(Type.EmptyTypes) != null)))
                {
                    var lgc = (CoreLogic)asm.CreateInstance(i.FullName);
                    lgc.Ctor(_knl);

                    if (!_lgcs.Contains(lgc.Name))
                    {
                        _lgcs.Add(lgc);
                    }
                    LogsHelper.Info(lgc.Name + " loading...");
                    Csl.Wl(lgc.Name + " loading...");
                }
            }
        }

        public void Close()
        {
            foreach (var lgc in _lgcs.GetAllLgcs())
            {
                lgc.Close();
            }
            _knl.Close();
           
        }

        public void Start()
        {
            _knl.Start();
        }

        public void Cmd(string cmd)
        {
            _knl.Cmd(cmd);
        }
    }
}
