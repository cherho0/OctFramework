using System;

namespace Oct.Framework.Core.Common
{
    public class Csl
    {
        public static void Init()
        {
            Win32.AllocConsole();
            Console.TreatControlCAsInput = true;
            Console.SetBufferSize(120, 0x800);
            SetClosable(false);
        }

        private static void SetClosable(bool val)
        {
            try
            {
                Win32.RemoveMenu(Win32.GetSystemMenu(Win32.FindWindow(null, Console.Title), 0), 0xf060, Convert.ToInt32(val));
            }
            catch
            {
            }
        }

        public static void Destory()
        {
            Win32.FreeConsole();
        }

        public static void Wl(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
        }
        public static void Wl(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
        }

        public static void WlEx(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
        }

        public static void WlEx(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.StackTrace);
        }

        public static void Wl(ConsoleColor red, string msg)
        {
            Console.ForegroundColor = red;
            Console.WriteLine(msg);
        }

        public static void WlInLine(ConsoleColor red, string msg)
        {
            Console.ForegroundColor = red;
            Console.Write(msg);
        }
        public static void WlInLine(string msg)
        {
            Console.Write(msg);
        }

        public static void Wl(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.StackTrace);
        }
    }
}
