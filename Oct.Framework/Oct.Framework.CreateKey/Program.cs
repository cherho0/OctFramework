using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oct.Framework.CreateKey
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string decryptionKey = CreateMachineKey(48);
            string validationKey = CreateMachineKey(128);

             

            Console.WriteLine(string.Format("decryptionKey:{0}", decryptionKey));
            Console.WriteLine(string.Format("validationKey:{0}", validationKey));
            var mc = string.Format("<machineKey validationKey=\"{0}\" decryptionKey=\"{1}\" validation=\"SHA1\" decryption=\"AES\" />",
                validationKey, decryptionKey);
            Console.WriteLine(mc);
            Clipboard.SetDataObject(mc,true);
            Console.WriteLine("已复制到粘贴板");
            Console.ReadLine();
        }

        /// <summary>
        /// 使用加密服务提供程序实现加密生成随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns>16进制格式字符串</returns>
        public static string CreateMachineKey(int length)
        {
            // 要返回的字符格式为16进制,byte最大值255
            // 需要2个16进制数保存1个byte,因此除2
            byte[] random = new byte[length / 2];

            // 使用加密服务提供程序 (CSP) 提供的实现来实现加密随机数生成器 (RNG)
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // 用经过加密的强随机值序列填充字节数组
            rng.GetBytes(random);

            StringBuilder machineKey = new StringBuilder(length);
            for (int i = 0; i < random.Length; i++)
            {
                machineKey.Append(string.Format("{0:X2}", random[i]));
            }
            return machineKey.ToString();
        }

        static String CreateKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];

            rng.GetBytes(buff);
            return BytesToHexString(buff);
        }

        static String BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }
    }
}
