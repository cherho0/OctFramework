using Newtonsoft.Json;
using Oct.Framework.Core.Log;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace Oct.Framework.Core.Common
{
    public static class ExtFunc
    {
        /// <summary>
        /// obj 对象日志记录
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dirpath">日志存放文件夹</param>
        /// <param name="name">对象名称</param>
        /// <param name="ext">日志文件后缀名</param>
        /// <param name="key">对象主键 可为空</param>
        /// <param name="appKey">appsetting里面获取是否记录的键，为空则记录,为0 则不记录</param>
        public static void SaveToLocalPath
            (this object obj, string dirpath, string name, string ext, object key = null, string appKey = "ObjLog")
        {
            try
            {
                var islog = ConfigurationManager.AppSettings[appKey];
                if (!string.IsNullOrEmpty(islog) && islog == "0")
                {
                    return;
                }
                var json = JsonConvert.SerializeObject(obj);
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dirpath);
                var bakName = name + "_" + DateTime.Now.ToString("yyyy_MM_dd") + "." + ext;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filePath = Path.Combine(path, bakName);
                var logsb = new StringBuilder();
                logsb.AppendLine("--------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----------");
                if (key != null)
                {
                    logsb.AppendLine(string.Format("-------- IndexKey:{0}----------", key));
                }
                logsb.AppendLine(json);
                File.AppendAllText(filePath, logsb.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 获取异常的描述消息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;

            Exception innerEx = ex.InnerException;

            while (innerEx != null)
            {
                if (innerEx.InnerException == null)
                    break;

                innerEx = innerEx.InnerException;
            }

            return innerEx.Message;
        }
    }
}
