using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Net.Sockets;

namespace Oct.Framework.Socket.Socket
{
    public class SocketAsyncEventArgsPool
    {
        private readonly ObjectPool<SocketAsyncEventArgs> _objectPool =
            new ObjectPool<SocketAsyncEventArgs>(() => new SocketAsyncEventArgs());

        public SocketAsyncEventArgs Acquire()
        {
            SocketAsyncEventArgs obj = _objectPool.GetObject();
            Contract.Assume(obj != null);
            return obj;
        }

        public void Release(SocketAsyncEventArgs arg)
        {
            if (_objectPool.Count > 10000)
            {
                arg.Dispose();
                return;
            }
            arg.AcceptSocket = null;
            arg.SetBuffer(null, 0, 0);
            arg.BufferList = null;
            arg.DisconnectReuseSocket = false;
            arg.RemoteEndPoint = null;
            arg.SendPacketsElements = null;
            arg.SendPacketsFlags = 0;
            arg.SendPacketsSendSize = 0;
            arg.SocketError = 0;
            arg.SocketFlags = 0;
            arg.UserToken = null;

            _objectPool.PutObject(arg);
        }
    }
}