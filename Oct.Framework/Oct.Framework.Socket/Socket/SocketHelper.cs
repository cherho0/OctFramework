using Oct.Framework.Socket.Common;
using Oct.Framework.Socket.Protocal;

namespace Oct.Framework.Socket.Socket
{
    public class SocketHelper
    {
        #region Fields

        /// <summary>
        /// 发送缓冲区
        /// </summary>
        private static DataBufferPool _sendBufferPool;
        private static DataBufferPool _sendBigBufferPool;

        /// <summary>
        /// 接收发消息SocketAsyncEventArgs池
        /// </summary>
        private static SocketAsyncEventArgsPool _poolOfRecSendEventArgs;

        private static SocketAsyncEventArgsPool _poolOfAcceptEventArgs;

        private static VarListPool _varListPool;
        private static PackageHeadPool _packageHeadPool;


        public const ushort H1 = 250;
        public const ushort H2 = 251;
        public const ushort H3 = 252;
        public const ushort H4 = 253;
        #endregion

        #region Properties

        public static DataBufferPool SendBufferPool
        {
            get { return _sendBufferPool; }
        }

        public static DataBufferPool SendBigBufferPool
        {
            get { return _sendBigBufferPool; }
        }

        public static SocketAsyncEventArgsPool RecSendEventArgs
        {
            get { return _poolOfRecSendEventArgs; }
        }

        public static SocketAsyncEventArgsPool AcceptEventArgs
        {
            get { return _poolOfAcceptEventArgs; }
        }

        public static PackageHeadPool PackageHeads
        {
            get { return _packageHeadPool; }
        }

        //public static VarListPool MsgPool
        //{
        //    get { return _varListPool ?? new VarListPool(CfgMgr.BasicCfg.MaxNumberOfConnections); }
        //}

        public static VarList Msg
        {
            get { return _varListPool.Acquire() ?? new VarListPool(CfgMgr.BasicCfg.MaxNumberOfConnections).Acquire(); }
        }

        #endregion

        #region Public Methods

        //static SocketHelper()
        //{
        //    _varListPool = new VarListPool(CfgMgr.BasicCfg.MaxNumberOfConnections);
        //}

        /// <summary>
        /// 服务端初始化
        /// </summary>
        public static void Init()
        {
            _sendBufferPool = new DataBufferPool(CfgMgr.BasicCfg.MaxNumberOfConnections + CfgMgr.BasicCfg.ExcessSaeaObjectsInPool,CfgMgr.BasicCfg.BufferSize);
            _sendBigBufferPool = new DataBufferPool(CfgMgr.BasicCfg.BigBufferCount,CfgMgr.BasicCfg.BufferSize * 10);
            _packageHeadPool = new PackageHeadPool(CfgMgr.BasicCfg.MaxNumberOfConnections);
            //_recvBufferPool = new DataBufferPool(CfgMgr.BasicCfg.MaxNumberOfConnections + CfgMgr.BasicCfg.ExcessSaeaObjectsInPool,CfgMgr.BasicCfg.BufferSize);
            _poolOfRecSendEventArgs = new SocketAsyncEventArgsPool();
            _poolOfAcceptEventArgs = new SocketAsyncEventArgsPool();
           
        }

        /// <summary>
        /// 客户端初始化
        /// </summary>
        /// <param name="poolCount"></param>
        /// <param name="bufferSize"></param>
        public static void Init(int poolCount, int bufferSize)
        {
            _varListPool = new VarListPool(poolCount);
            _sendBufferPool = new DataBufferPool(poolCount, bufferSize);
            _sendBigBufferPool = new DataBufferPool(100, bufferSize * 10);
            _packageHeadPool = new PackageHeadPool(poolCount);
            //_recvBufferPool = new DataBufferPool(poolCount, bufferSize);
            _poolOfRecSendEventArgs = new SocketAsyncEventArgsPool();
        }
        #endregion



    }
}