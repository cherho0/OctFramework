using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Oct.Framework.Core.Security
{
    public partial class Encypt
    {
        #region MD5函数
        /// <summary>
        /// MD5函数,需引用：using System.Security.Cryptography;
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            //微软md5方法参考return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="input">明文</param>
        /// <param name="isLower">密文是否小写</param>
        /// <returns></returns>
        public static string MD5By32Bit(string input, bool isLower = true)
        {
            //var cl = input;
            //var pwd = string.Empty;

            ////实例化一个md5对像
            //var md5 = System.Security.Cryptography.MD5.Create();

            ////加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            //var s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));

            ////通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            //for (int i = 0; i < s.Length; i++)
            //{
            //    //将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            //    pwd = pwd + s[i].ToString(isLower ? "x" : "X");
            //}

            //return pwd;

            var pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");

            return isLower ? pwd.ToLower() : pwd;
        }

        #endregion
    }
}
