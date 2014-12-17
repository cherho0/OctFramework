using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Oct.Framework.Socket.Common
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class Log
    {
        private static ILog log;


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

        private static Log logHelper;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private static ILog Instance
        {
            get
            {
                if (logHelper == null)
                {
                    logHelper = new Log(null);
                }

                return log;
            }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configPath"></param>
        private Log(string configPath)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "../Res/Log4Net.config"));
            if (!string.IsNullOrEmpty(configPath))
            {
                log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                XmlConfigurator.Configure(new FileInfo(configPath));
            }
            else
            {
                log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

    }
}