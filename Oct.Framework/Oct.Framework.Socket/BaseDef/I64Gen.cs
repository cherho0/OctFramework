using System.Threading;

namespace Oct.Framework.Socket.BaseDef
{
    public class I64Gen
    {
        private long _i64;

        public long NewId()
        {
            return Interlocked.Increment(ref _i64);
        }
    }
}