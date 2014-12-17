using System.Threading;

namespace Oct.Framework.Socket.BaseDef
{
    public class I32Gen
    {
        private int _i32;

        public int NewId()
        {
            return Interlocked.Increment(ref _i32);
        }
    }
}