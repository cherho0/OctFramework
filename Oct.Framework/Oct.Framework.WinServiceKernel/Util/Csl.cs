using System;

namespace Oct.Framework.WinServiceKernel.Util
{
    public class Csl
    {
        public static void Init()
        {
            Console.TreatControlCAsInput = true;
            Console.SetBufferSize(120, 0x800);
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

        public static void Wl(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.StackTrace);
        }
    }
}
