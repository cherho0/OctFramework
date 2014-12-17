using System.Threading;
using Oct.Framework.Core.Common;

namespace Oct.Framework.WinServiceKernel.HeartBeat
{
    public class Seed : SingleTon<Seed>
    {
        private int _i32;

        public int NewId()
        {
            return Interlocked.Increment(ref _i32);
        }
    }
}
