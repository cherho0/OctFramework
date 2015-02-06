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
            Bootstrapper.Initialise();
            Csl.Wl("开始构建内核...");
            _knl = new Kernel();
            _lgcs = new LogicMgr();
            Composition.Initialize(_knl, _lgcs);
            Csl.Wl("开始进行MEF注入，可能需要1-5秒。。。");
            Csl.Wl("开始加载模块...");
            LoadLgcs();
            Csl.Wl("模块加载完成。");
            Csl.Wl("内核构建完成。");
            LogHelper.Info("内核构建完成。");
            Csl.Wl(ConsoleColor.Yellow, "输入help查看相关command操作。");
            Csl.Wl(ConsoleColor.Yellow, "输入exit退出。");
        }

        private void LoadLgcs()
        {
            foreach (var lgc in _lgcs.GetAllLgcs())
            {
                lgc.Load();
                LogHelper.Info(lgc.Name + " loading...");
                Csl.Wl(lgc.Name + " loading...");
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
