using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;

namespace Oct.Framework.Core.OrderNoGenter
{
    public class NOGenter : SingleTon<NOGenter>
    {
        private static DateTime _seedDate;
        private static int _i32;

        public NOGenter()
        {
            _seedDate = DateTime.Now;
            _i32 = 0;
        }


        private int GetCurrSeed()
        {
            if (_seedDate.Day != DateTime.Now.Day )
            {
                _i32 = 0;
            }
            return Interlocked.Increment(ref _i32);
        }

        public string GenOrderNo(string perfix = "")
        {
            return string.Format("{0}{1}{2}",perfix,DateTime.Now.ToString("yyyyMMdd"), GetCurrSeed().ToString().PadLeft(5,'0'));
        }
    }
}
