using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Oct.Framework.Core.Mail
{
    public class MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailTo">发送给谁（邮件地址）</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public void SendMail(string emailTo, string subject, string body, string emailServer = "http://222.92.117.190:6002/api/Email/Post")
        {
            bool result = false;
            string requestJson = JsonConvert.SerializeObject(new
            {
                EmailTo = emailTo,
                Subject = subject,
                Body = body
            });

            HttpContent httpContent = new StringContent(requestJson);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            httpClient.PostAsync(emailServer, httpContent);
        }
    }
}
