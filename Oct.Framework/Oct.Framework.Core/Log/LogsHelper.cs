using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.Core.Log
{
    public class LogsHelper 
    {
        public static void Init()
        {
            log4net.Config.XmlConfigurator.Configure();  
        }

        private static log4net.ILog _log;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private static log4net.ILog Instance
        {
            get
            {
                if (_log == null)
                {
                    _log = log4net.LogManager.GetLogger("Oct.Framework");
                }

                return _log;
            }
        }

        public static void Info(string msg)
        {
            Instance.Info(msg);
        }

        public static void Info(Exception ex)
        {
            Instance.Info(ex.Message, ex);
        }

        public static void Error(string msg)
        {
            Instance.Error(msg);
        }

        public static void Error(string msg, Exception ex)
        {
            Instance.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            Instance.Error(ex.Message, ex);
        }

        public static void Debug(string msg)
        {
            Instance.Debug(msg);
        }

        public static void Debug(Exception ex)
        {
            Instance.Debug(ex.Message, ex);
        }
    }
}
