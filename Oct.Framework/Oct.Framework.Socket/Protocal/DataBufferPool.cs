using System.Collections.Concurrent;
using Oct.Framework.Socket.Common;

namespace Oct.Framework.Socket.Protocal
{
    public class DataBufferPool
    {
        private readonly int _bufferLength = CfgMgr.BasicCfg.BufferSize;
        private readonly ConcurrentQueue<DataBuffer> _bufferPools = new ConcurrentQueue<DataBuffer>();
        private int _totalCount;
        public DataBufferPool(int count)
        {
            Init(count);
        }

        public DataBufferPool(int count, int bufferlength)
        {
            _bufferLength = bufferlength;
            Init(count);
        }

        public int Count
        {
            get { return _bufferPools.Count; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
        }

        private void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _bufferPools.Enqueue(Create());
            }
        }

        private DataBuffer Create()
        {
            _totalCount++;
            return new DataBuffer(_bufferLength) {Pool = this};;
        }

        public DataBuffer Pop()
        {
            DataBuffer buffer;
            return _bufferPools.TryDequeue(out buffer) ? buffer : Create();
        }

        public void Push(DataBuffer item)
        {
            //item.Clear();
            _bufferPools.Enqueue(item);
        }
    }
}