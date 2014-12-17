using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.Core.ApiData
{
    public class ApiDataHelper
    {
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostData(string url, string data)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            return wc.UploadString(url, "post", data);
        }

        /// <summary>
        /// 获取string 数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(string url)
        {
            WebClient wc = new WebClient();
            return wc.DownloadString(url);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        public static void DownLoadFile(string url, string savePath)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(new Uri(url), savePath);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        public static void UpLoadFile(string url, string filePath)
        {
            WebClient wc = new WebClient();

            wc.UploadFile(url, filePath);
        }
    }
}
