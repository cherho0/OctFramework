using System;
using System.Diagnostics;
using System.Threading;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Log;
using Oct.Framework.WinServiceKernel;
using Oct.Framework.WinServiceKernel.Util;
using Win32 = Oct.Framework.WinServiceKernel.Common.Win32;

namespace Oct.Framework.WinConsole
{
    class Program
    {
        private static FrameworkServer Server;
        static void Main(string[] args)
        {
            Win32.RemoveMenu(Win32.GetSystemMenu(Win32.FindWindow(null, System.Console.Title), 0), 0xf060, Convert.ToInt32(false));
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            Server = new FrameworkServer();
            Server.Start();

            Cmd();
        }

        private static void Cmd()
        {
            var key = System.Console.ReadLine();

            var cmds = key.ToLower().Split(' ');

            if (cmds.Length == 0)
            {
                return;
            }

            var cmd = cmds[0];

            switch (cmd)
            {
                case "exit":
                    Csl.Wl("正在关闭应用程序。。。等待最后一个心跳执行完成。。。");
                    Server.Close();
                    Csl.Wl("2秒后关闭应用程序。。。");
                    Thread.Sleep(2000);
                    Process.GetCurrentProcess().CloseMainWindow();
                    break;
                case "start":
                    Server.Start();
                    Cmd();
                    break;
                case "gc":
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        GC.Collect();
                        sw.Stop();
                        Cmd();
                        break;
                    }
                default:
                    {
                        Server.Cmd(cmd);
                        Cmd();
                        break;
                    }
                    return;
            }
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            if (ex != null)
            {

                LogsHelper.Error(ex);
                Csl.WlEx(ex);
            }
        }
    }
}
