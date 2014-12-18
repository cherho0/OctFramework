using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NyMQ.Core.Socket;
using Oct.Framework.Core.Common;
using Oct.Framework.Socket.Common;
using Oct.Framework.Socket.Protocal;

namespace Oct.Framework.Socket.Socket
{
    public abstract class ClientBase : IClient
    {
        #region Fields

        private int BufferSize = CfgMgr.BasicCfg.BufferSize;
        /// <summary>
        /// 缓冲区开辟个数
        /// </summary>
        private static long _receBufferCount;

        /// 所有客户端接收消息字节数
        private static long _totalBytesReceived;

        /// 所有客户端接收消息字节数
        protected static long _totalBytesSent;

        /// 此客户端接收消息字节数
        private uint _bytesReceived;

        /// 此客户端接收消息字节数
        protected uint _bytesSent;

        /// <summary>
        /// 偏移量
        /// </summary>
        protected int _offset;

        /// <summary>
        /// 剩余长度
        /// </summary>
        protected int _remainingLength;

        protected ServerBase _server;

        protected DataBuffer recvDataBuffer;
        protected System.Net.Sockets.Socket _tcpSock = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      //接收消息缓冲

        #endregion

        #region Public properties

        public static long TotalBytesSent
        {
            get { return _totalBytesSent; }
        }


        public static long TotalBytesReceived
        {
            get { return _totalBytesReceived; }
        }

        public uint ReceivedBytes
        {
            get { return _bytesReceived; }
        }

        public static long TotalReceBufferCount
        {
            get { return _receBufferCount; }
        }

        public uint SentBytes
        {
            get { return _bytesSent; }
        }

        public ServerBase Server
        {
            get { return _server; }
        }

        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool IsAutherized { get; set; }

        public IPAddress ClientAddress
        {
            get
            {
                return (_tcpSock != null && _tcpSock.RemoteEndPoint != null)
                           ? ((IPEndPoint) _tcpSock.RemoteEndPoint).Address
                           : null;
            }
        }

        public int Port
        {
            get
            {
                return (_tcpSock != null && _tcpSock.RemoteEndPoint != null)
                           ? ((IPEndPoint) _tcpSock.RemoteEndPoint).Port
                           : -1;
            }
        }

        public System.Net.Sockets.Socket TcpSocket
        {
            get { return _tcpSock; }
            set
            {
                if (_tcpSock != null && _tcpSock.Connected)
                {
                    _tcpSock.Shutdown(SocketShutdown.Both);
                    _tcpSock.Close();
                }

                if (value != null)
                {
                    _tcpSock = value;
                }
            }
        }

        public bool IsConnected
        {
            get { return _tcpSock != null && _tcpSock.Connected; }
        }

        #endregion

        #region Events

        public event EventHandler ClientConnected;
        public event EventHandler<DataEventArgs<IClient>> Closed;
        public event EventHandler<DataEventArgs<Exception>> Error;
        public event ClientReceivedHandler Received;

        #endregion

        #region Constructor

        protected ClientBase(ServerBase server)
        {
            _server = server;
            recvDataBuffer = new DataBuffer(CfgMgr.BasicCfg.BufferSize);
            _receBufferCount++;
        }

        /// <summary>
        /// 此构造函数方便测试
        /// </summary>
        protected ClientBase()
        {
            recvDataBuffer = new DataBuffer(CfgMgr.BasicCfg.BufferSize);
            _receBufferCount++;
        }

        #endregion

        #region Connect
        public void Connect(string host, int port)
        {
            IPAddress addr = IPAddress.Parse(host);
            Connect(addr, port);
        }

        public void ConnectHost(string host, int port)
        {
            _tcpSock.Connect(host,port);

            if (_tcpSock.Connected)
            {
                if (ClientConnected != null)
                {
                    ClientConnected(this, null);
                }
            }
        }

        public void Connect(IPAddress addr, int port)
        {
            _tcpSock.Connect(new IPEndPoint(addr, port));
             
            if (_tcpSock.Connected)
            {
                if (ClientConnected != null)
                {
                    ClientConnected(this, null);
                }
            }
        }

        public void ConnectAsync(string host, int port)
        {
            IPAddress addr = IPAddress.Parse(host);
            ConnectAsync(addr, port);
        }

        public void ConnectAsync(IPAddress addr, int port)
        {
            _tcpSock.BeginConnect(new IPEndPoint(addr, port), EndConnect, _tcpSock);
        }

        private void EndConnect(IAsyncResult ar)
        {
            try
            {
                var s = (System.Net.Sockets.Socket) ar.AsyncState;

                s.EndConnect(ar);

                if (s.Connected)
                {
                    if (ClientConnected != null)
                    {
                        ClientConnected(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                {
                    Error(null, new DataEventArgs<Exception>(ex));
                }
                Csl.Wl(ex.StackTrace);
            }
        }

        #endregion

        #region Receive

        public void BeginReceive()
        {
            ResumeReceive();
        }

        private void ResumeReceive()
        {
            if (_tcpSock != null && _tcpSock.Connected)
            {
                SocketAsyncEventArgs socketArgs = SocketHelpers.AcquireSocketArg();
                //int offset = _offset + _remainingLength;

                socketArgs.SetBuffer(recvDataBuffer.Data, recvDataBuffer.Postion, BufferSize - _remainingLength);
                socketArgs.UserToken = this;
                socketArgs.Completed += ReceiveAsyncComplete;

                bool willRaiseEvent = _tcpSock.ReceiveAsync(socketArgs);
                if (!willRaiseEvent)
                {
                    ProcessRecieve(socketArgs);
                }
            }
        }

        private void ProcessRecieve(SocketAsyncEventArgs args)
        {
            try
            {
                int bytesReceived = args.BytesTransferred;
                recvDataBuffer.SetCount(recvDataBuffer.Count + bytesReceived);

                if (bytesReceived == 0)
                {
                    // no bytes means the client disconnected, so clean up!
                    _server.DisconnectClient(this, true);
                }
                else
                {
                    // increment our counters
                    //unchecked
                    //{
                    //    _bytesReceived += (uint)bytesReceived;
                    //}

                    //Interlocked.Add(ref _totalBytesReceived, bytesReceived);

                    _remainingLength += bytesReceived;

                    if (OnReceive(recvDataBuffer))
                    {
                        _offset = 0;
                        recvDataBuffer.Clear();
                    }
                    else
                    {
                        //var newBuffer = new DataBuffer(BufferSize);
                       // Array.Copy(recvDataBuffer.Data, _offset, newBuffer.Data, 0, _remainingLength);

                        Array.Copy(recvDataBuffer.Data, _offset, recvDataBuffer.Data, 0, _remainingLength);
                        recvDataBuffer.SetPostion(_remainingLength);
                        recvDataBuffer.SetCount(_remainingLength);
                        //recvDataBuffer = newBuffer;

                        _offset = 0;
                        //_receBufferCount++;
                    }

                    ResumeReceive();
                }
            }
            catch (ObjectDisposedException)
            {
                if (_server != null)
                {
                    _server.DisconnectClient(this, true);
                }

            }
            catch (Exception e)
            {
                if (_server != null)
                {
                    //_server.Warning(this, e);
                   
                    _server.DisconnectClient(this, true);
                    Csl.Wl(e);
                }

            }
            finally
            {
                args.Completed -= ReceiveAsyncComplete;
                SocketHelpers.ReleaseSocketArg(args);
            }
        }

        private void ReceiveAsyncComplete(object sender, SocketAsyncEventArgs args)
        {
            ProcessRecieve(args);
        }

        /// <summary>
        /// 处理接收到的消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        protected abstract bool OnReceive(DataBuffer buffer);
        public abstract void Send(VarList msg);
        //public abstract void SendDelay(VarList msg);
        public abstract void SendDelay(IEnumerable<VarList> msgs);

        /// <summary>
        /// 批量发送
        /// </summary>
        public void Send(IList<ArraySegment<byte>> content)
        {
            if (_tcpSock != null && _tcpSock.Connected)
            {
                SocketAsyncEventArgs args = SocketHelpers.AcquireSocketArg();
                if (args != null)
                {
                    args.Completed += SendAsyncComplete;
                    args.BufferList = content;
                    args.UserToken = this;
                    _tcpSock.SendAsync(args);

                    //unchecked
                    //{
                    //    _bytesSent += (uint)length;
                    //}

                    //Interlocked.Add(ref _totalBytesSent, length);
                }
                else
                {
                    Csl.Wl(string.Format("Client {0}'s SocketArgs are null", this));
                }
            }
        }

        protected virtual void RaiseReceiveHandler(VarList msg)
        {
            if (Received != null)
            {
                Received(msg);
            }
        }

        #endregion

        #region Send

        public void Send(byte[] packet)
        {
            Send(packet, 0, packet.Length);
        }

        public void SendCopy(byte[] packet)
        {
            var copy = new byte[packet.Length];
            Array.Copy(packet, copy, packet.Length);
            Send(copy, 0, copy.Length);
        }

        public void Send(byte[] packet, int offset, int length)
        {
            if (_tcpSock != null && _tcpSock.Connected)
            {
                SocketAsyncEventArgs args = SocketHelpers.AcquireSocketArg();
                if (args != null)
                {
                    args.Completed += SendAsyncComplete;

                    args.SetBuffer(packet, offset, length);
                    args.UserToken = this;
                    _tcpSock.SendAsync(args);

                    //unchecked
                    //{
                    //    _bytesSent += (uint)length;
                    //}

                    //Interlocked.Add(ref _totalBytesSent, length);
                }
                else
                {
                    Csl.Wl(string.Format("Client {0}'s SocketArgs are null",this));
                }
            }
        }

        private static void SendAsyncComplete(object sender, SocketAsyncEventArgs args)
        {
            args.Completed -= SendAsyncComplete;
            SocketHelpers.ReleaseSocketArg(args);
        }

        public override string ToString()
        {
            return
                (TcpSocket == null || !TcpSocket.Connected
                     ? "<disconnected client>"
                     : (TcpSocket.RemoteEndPoint ?? (object)"<unknown client>")).ToString();
        }
        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ClientBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_tcpSock != null && _tcpSock.Connected)
            {
                try
                {
                    if (Closed != null)
                    {
                        Closed(this, new DataEventArgs<IClient>(this));
                    }

                    _tcpSock.Shutdown(SocketShutdown.Both);
                    _tcpSock.Close();
                    _tcpSock = null;
                }
                catch (SocketException ex)
                {
                    //Csl.Wl(ex);
                    Log.Error(string.Format("Dispose error {0}", ClientAddress), ex);
                }
            }
        }

        #endregion
    }
}