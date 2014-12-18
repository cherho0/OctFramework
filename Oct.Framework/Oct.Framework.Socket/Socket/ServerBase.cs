using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using NyMQ.Core.Socket;
using Oct.Framework.Core.Common;
using Oct.Framework.Socket.Common;

namespace Oct.Framework.Socket.Socket
{
    /// <summary>
    /// 建立连接时间
    /// </summary>
    /// <param name="client">连接进来的客户端</param>
    public delegate void ClientConnectedHandler(IClient client);

    /// <summary>
    /// 断开事件
    /// </summary>
    /// <param name="client">断开的客户端</param>
    /// <param name="forced">是否强制断开</param>
    public delegate void ClientDisconnectedHandler(IClient client, bool forced);

    public abstract class ServerBase : IDisposable
    {
        #region Fields

        //protected readonly ConcurrentDictionary<int, IClient> _clients;
        private readonly Semaphore _theMaxConnectionsEnforcer;
        protected HashSet<IClient> _clients = new HashSet<IClient>();
        private int _maxPendingCon;

        /// <summary>
        /// 是否在运行
        /// </summary>
        protected bool _running;

        /// <summary>
        /// 是否可用
        /// </summary>
        protected bool _tcpEnabled;

        /// <summary>
        /// 地址
        /// </summary>
        protected IPEndPoint _tcpEndpoint;

        /// <summary>
        /// 监听Socket
        /// </summary>
        protected System.Net.Sockets.Socket _tcpListen;
        #endregion

        #region Properties

        public virtual bool IsRunning
        {
            get { return _running; }
            set { _running = value; }
        }

        public virtual int MaximumPendingConnections
        {
            get { return _maxPendingCon; }
            set
            {
                if (value > 0)
                {
                    _maxPendingCon = value;
                }
            }
        }

        public virtual int TcpPort
        {
            get { return _tcpEndpoint.Port; }
            set { _tcpEndpoint.Port = value; }
        }

        public virtual IPAddress TcpIP
        {
            get { return _tcpEndpoint.Address; }
            set { _tcpEndpoint.Address = value; }
        }

        public virtual IPEndPoint TcpEndPoint
        {
            get { return _tcpEndpoint; }
            set { _tcpEndpoint = value; }
        }

        public int NumberOfClients
        {
            get { return _clients.Count; }
        }

        public string RootPath
        {
            get { return Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName; }
        }

        public bool EnableTCP
        {
            get { return _tcpEnabled; }
            set
            {
                if (_running && _tcpEnabled != value)
                {
                    if (value)
                    {
                        StartTCP();
                    }
                    else
                    {
                        try
                        {
                            _tcpListen.Close();
                        }
                        catch (Exception ex)
                        {
                           Csl.Wl("Exception occured while trying to close the TCP Connection");
                        }

                        _tcpListen = null;
                        _tcpEnabled = false;
                    }
                }
            }
        }

        #endregion

        #region Events

        #region Public Events
        public event ClientConnectedHandler ClientConnected;
        public event ClientDisconnectedHandler ClientDisconnected;

        //public event EventHandler<DataEventArgs<string>> ClientConnected;
        //public event EventHandler<DataEventArgs<string, int>> DisConnected;
        #endregion

        #endregion

        #region Initialize

        protected ServerBase()
        {
            _theMaxConnectionsEnforcer = new Semaphore(CfgMgr.BasicCfg.MaxNumberOfConnections,
                                                       CfgMgr.BasicCfg.MaxNumberOfConnections);
        }

        #endregion

        #region State Management

        /// <summary>
        /// 开启服务器，开始接受连接
        /// <seealso cref="ServerBase.Stop"/>
        /// </summary>
        public virtual void Start()
        {
            try
            {
                if (!_running)
                {
                    Log.Info("ServerBase started");
                    IsRunning = true;

                    if (_tcpEndpoint == null)
                    {
                        _tcpEndpoint = new IPEndPoint(GetDefaultExternalIpAddress(), 0);
                    }

                    StartTCP();

                    Log.Info("ServerBase ready for connections");
                }
            }
            catch (InvalidEndpointException ex)
            {
                Csl.WlEx(ex);
                Log.Error(string.Format("InvalidEndpoint,{0}", ex.Endpoint));
                Stop();
            }
            catch (NoAvailableAdaptersException ex)
            {
                Csl.WlEx(ex);
                Log.Error("NoNetworkAdapters");
                Stop();
            }
        }



        /// <summary>
        /// 停止服务器并清除所有连接客户端
        /// </summary>
        public virtual void Stop()
        {
            Log.Info("ServerBase Stopped");

            if (_running)
            {
                IsRunning = false;

                RemoveAllClients();
                if (_tcpListen != null)
                {
                    _tcpListen.Close(60);
                }
            }
        }

        //private Thread _thThread;
        /// <summary>
        /// 开启TCP服务
        /// </summary>
        protected void StartTCP()
        {
            if (!_tcpEnabled && _running)
            {
                VerifyEndpointAddress(TcpEndPoint);

                _tcpListen = new System.Net.Sockets.Socket(TcpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    _tcpListen.Bind(TcpEndPoint);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Could not bind to Address {0}: {1}", TcpEndPoint, ex));
                    Csl.Wl(string.Format("Could not bind to Address {0}: {1}", TcpEndPoint, ex));
                    return;
                }

                _tcpListen.Listen(CfgMgr.BasicCfg.MaxSimultaneousAcceptOps);
                SocketHelpers.SetListenSocketOptions(_tcpListen);
                _tcpEnabled = true;
                StartAccept(null);

                Log.Info(string.Format("Endpoint {0} start listening", TcpEndPoint));
            }
        }

        /// <summary>
        /// 检查当前端口是否可用
        /// </summary>
        /// <param name="endPoint"></param>
        public static void VerifyEndpointAddress(IPEndPoint endPoint)
        {
            if (!endPoint.Address.Equals(IPAddress.Any) &&
                !endPoint.Address.Equals(IPAddress.Loopback))
            {
                IPAddress endpointAddr = endPoint.Address;
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                if (interfaces.Length > 0)
                {
                    foreach (NetworkInterface iface in interfaces)
                    {
                        UnicastIPAddressInformationCollection uniAddresses = iface.GetIPProperties().UnicastAddresses;

                        if (uniAddresses.Where(ipInfo => ipInfo.Address.Equals(endpointAddr)).Any())
                        {
                            return;
                        }
                    }

                    throw new InvalidEndpointException(endPoint);
                }

                throw new NoAvailableAdaptersException();
            }
        }

        /// <summary>
        /// 获取服务端默认地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetDefaultExternalIpAddress()
        {
            return IPAddress.Loopback;
        }

        #endregion

        #region Listen

        /// <summary>
        /// 开始监听
        /// </summary>
        protected void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += AcceptEventCompleted;
            }
            else
            {
                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
            }

            bool willRaiseEvent = _tcpListen.AcceptAsync(acceptEventArg);
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArg);
            }
        }

        private void AcceptEventCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs args)
        {
            try
            {
                if (!_running)
                {
                    Csl.Wl("Server closed...");
                    return;
                }

                IClient client = CreateClient();
                client.TcpSocket = args.AcceptSocket;
                client.BeginReceive();

                StartAccept(args);

                if (OnClientConnected(client))
                {
                    lock (_clients)
                    {
                        _clients.Add(client);
                    }
                }
                else
                {
                    client.TcpSocket.Shutdown(SocketShutdown.Both);
                    client.TcpSocket.Close();
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (SocketException e)
            {
                Csl.Wl(e.StackTrace);
            }
            catch (Exception e)
            {
                Csl.Wl(e.StackTrace);
            }
        }

        #endregion

        #region Client management

        /// <summary>
        /// 创建客户端
        /// <seealso cref="ServerBase.Start"/>
        /// </summary>
        /// <returns></returns>
        protected abstract IClient CreateClient();

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="client"></param>
        public void RemoveClient(IClient client)
        {
            lock (_clients)
            {
                _clients.Remove(client);
            }
        }

        /// <summary>
        /// 转移特殊客户端
        /// </summary>
        /// <param name="client"></param>
        protected void RemoveSpecialClient(IClient client)
        {
            lock (_clients)
            {
                _clients.Remove(client);
            }
        }

        /// <summary>
        /// 移除所有客户端
        /// </summary>
        public void RemoveAllClients()
        {
            lock (_clients)
            {
                foreach (IClient client in _clients)
                {
                    try
                    {
                        OnClientDisconnected(client, true);
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                    catch (Exception e)
                    {
                        Csl.WlEx(e);
                        Log.Error(e.ToString());
                    }
                }

                _clients.Clear();
            }
        }

        /// <summary>
        /// 断开客户端
        /// </summary>
        /// <param name="client"></param>
        /// <param name="forced"></param>
        public void DisconnectClient(IClient client, bool forced)
        {
            RemoveClient(client);

            try
            {

                //if (_theMaxConnectionsEnforcer.Release(1) == CfgMgr.BasicCfg.MaxNumberOfConnections - 1)
                //{
                //    Csl.Wl("计数器已达上限");
                //}
                if (client != null && client.TcpSocket != null)
                {
                    OnClientDisconnected(client, forced);
                    //client.Dispose();
                }
            }
            catch (ObjectDisposedException e)
            {
                Csl.WlEx(e);
                Log.Error("Could not disconnect client", e);
                // Connection was already closed (probably by the remote side)
            }
            catch (Exception e)
            {
                Csl.WlEx(e);
                Log.Error("Could not disconnect client", e);
            }
        }

        /// <summary>
        /// 连接处理
        /// </summary>
        protected virtual bool OnClientConnected(IClient client)
        {
            ClientConnectedHandler handler = ClientConnected;
            if (handler != null)
            {
                handler(client);
            }

            //if (ClientConnected != null)
            //{
            //    ClientConnected(this, new DataEventArgs<string>(client.ClientAddress.ToString()));
            //}

            return true;
        }

        /// <summary>
        /// 断开处理
        /// </summary>
        /// <param name="client"></param>
        /// <param name="forced"></param>
        protected virtual void OnClientDisconnected(IClient client, bool forced)
        {
            ClientDisconnectedHandler handler = ClientDisconnected;
            if (handler != null)
                handler(client, forced);

            //if (DisConnected != null && client.TcpSocket != null)
            //{
            //    DisConnected(this, new DataEventArgs<string, int>(string.Empty, client.TcpSocket.Handle.ToInt32()));
            //}

            client.Dispose();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Don't call this method outside of the Context that manages the server.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServerBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_running)
            {
                Stop();
            }
        }

        #endregion

    }
}