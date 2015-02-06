using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Oct.Framework.Core.Security
{
    public partial class Encypt
    {
        #region MD5����
        /// <summary>
        /// MD5����,�����ã�using System.Security.Cryptography;
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
        public static string MD5(string str)
        {
            //΢��md5�����ο�return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        /// <summary>
        /// MD5 32λ����
        /// </summary>
        /// <param name="input">����</param>
        /// <param name="isLower">�����Ƿ�Сд</param>
        /// <returns></returns>
        public static string MD5By32Bit(string input, bool isLower = true)
        {
            //var cl = input;
            //var pwd = string.Empty;

            ////ʵ����һ��md5����
            //var md5 = System.Security.Cryptography.MD5.Create();

            ////���ܺ���һ���ֽ����͵����飬����Ҫע�����UTF8/Unicode�ȵ�ѡ��
            //var s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));

            ////ͨ��ʹ��ѭ�������ֽ����͵�����ת��Ϊ�ַ��������ַ����ǳ����ַ���ʽ������
            //for (int i = 0; i < s.Length; i++)
            //{
            //    //���õ����ַ���ʹ��ʮ���������͸�ʽ����ʽ����ַ���Сд����ĸ�����ʹ�ô�д��X�����ʽ����ַ��Ǵ�д�ַ� 
            //    pwd = pwd + s[i].ToString(isLower ? "x" : "X");
            //}

            //return pwd;

            var pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");

            return isLower ? pwd.ToLower() : pwd;
        }

        #endregion
    }
}
