using System.Collections.Concurrent;

namespace Oct.Framework.Socket.Socket
{
    public class PackageHeadPool
    {
        private readonly ConcurrentQueue<PackageHead> _headPools = new ConcurrentQueue<PackageHead>();
        private int _totalCount = 0;

        public PackageHeadPool(int count)
        {
            Init(count);
        }

        public int Count
        {
            get { return _headPools.Count; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
        }

        private void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _headPools.Enqueue(Create());
            }
        }

        private PackageHead Create()
        {
            return new PackageHead() { Pool = this }; ;
        }

        public PackageHead Pop()
        {
            PackageHead ph;
            return _headPools.TryDequeue(out ph) ? ph : Create();
        }

        public void Push(PackageHead item)
        {
            _headPools.Enqueue(item);
        }
    }
}
