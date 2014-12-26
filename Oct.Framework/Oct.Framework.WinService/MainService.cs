using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Log;
using Oct.Framework.WinServiceKernel;
using Oct.Framework.WinServiceKernel.Util;

namespace Oct.Framework.WinService
{
    public partial class MainService : ServiceBase
    {
        private FrameworkServer _server;
        public MainService()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            if (ex != null)
            {
                LogHelper.Error(ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            _server = new FrameworkServer();
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Close();
        }
    }
}
