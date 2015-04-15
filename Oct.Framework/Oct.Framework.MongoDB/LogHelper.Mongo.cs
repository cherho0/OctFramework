using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.MongoDB
{
    public class LogHelperMongo
    {

        private static MongoDbRepository<LogEntity> Instance
        {
            get
            {
                return new MongoDbRepository<LogEntity>();
            }
        }
        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = msg,
                Type = "Info"
            });
            Instance.Dispose();
        }

        public static void Info(Exception ex)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = ex.Message + "\r\n" + ex.StackTrace,
                Type = "Info"
            });
            Instance.Dispose();
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = msg,
                Type = "Error"
            });
            Instance.Dispose();
        }

        public static void Error(string msg, Exception ex)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = msg + "\r\n" + ex.Message + "\r\n" + ex.StackTrace,
                Type = "Error"
            });
            Instance.Dispose();
        }

        public static void Error(Exception ex)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = ex.Message + "\r\n" + ex.StackTrace,
                Type = "Error"
            });
            Instance.Dispose();
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = msg,
                Type = "Debug"
            });
            Instance.Dispose();
        }

        public static void Debug(Exception ex)
        {
            Instance.Add(new LogEntity()
            {
                CreateTime = DateTime.Now,
                Message = ex.Message + "\r\n" + ex.StackTrace,
                Type = "Debug"
            });
            Instance.Dispose();
        }
    }
}
