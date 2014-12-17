using System.Collections.Concurrent;

namespace Oct.Framework.Socket.Protocal
{
    public class VarListPool
    {
        private readonly ConcurrentQueue<VarList> _msgPools = new ConcurrentQueue<VarList>();

        public VarListPool(int count)
        {
            Init(count);
        }
    
        public int Count
        {
            get { return _msgPools.Count; }
        }

        private void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _msgPools.Enqueue(Create());
            }
        }

        private VarList Create()
        {
            return new VarList();
        }

        public VarList Acquire()
        {
            VarList msg;
            return _msgPools.TryDequeue(out msg) ? msg : Create();
        }

        public void Release(VarList msg)
        {
            msg.Clear();
            _msgPools.Enqueue(msg);
        }
    }
}
