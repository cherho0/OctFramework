using Microsoft.VisualStudio.TextTemplating;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace Oct.Tools.Core.T4
{
    public class TemplateMgr
    {
        #region 方法

        /// <summary>
        /// 执行模板
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="temp"></param>
        /// <param name="nameSpaces"></param>
        /// <returns></returns>
        public static string ProcessTemplate(string key, object value, FileInfo temp, string[] nameSpaces)
        {
            var input = File.ReadAllText(temp.FullName);
            var output = new StringBuilder();
            var host = new CustomTextTemplatingEngineHost(nameSpaces);

            host.templateFileValue = temp.Name;

            host.Session = new TextTemplatingSession();
            host.Session.Add(key, value);

            var result = new Engine().ProcessTemplate(input, host);

            if (host.Errors.Count == 0)
                output.Append(result);
            else
            {
                output.Append("error：\r\n");

                foreach (CompilerError error in host.Errors)
                {
                    output.AppendFormat("{0}：{1}\r\n", error.Line, error.ErrorText);
                }
            }

            return output.ToString();
        }

        #endregion
    }
}
