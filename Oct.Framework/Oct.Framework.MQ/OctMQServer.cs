using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
/*
 *基于ZeroMq实现的MQ，无需部署其他服务器程序，并且性能最高,需要自己实现消息队列持久化
 */
using Oct.Framework.Core.Args;

namespace Oct.Framework.MQ
{
    /// <summary>
    /// Mq服务端
    /// </summary>
    public class OctMQServer : IDisposable
    {
        public event EventHandler<DataEventArgs<NetMQSocket, NetMQMessage>> OnReceive;

        protected virtual void OnOnReceive(DataEventArgs<NetMQSocket, NetMQMessage> e)
        {
            EventHandler<DataEventArgs<NetMQSocket, NetMQMessage>> handler = OnReceive;
            if (handler != null) handler(this, e);
        }

        private int _port;
        private NetMQSocket _serverSocket;
        private ServerType _type;
        private NetMQContext _context;

        public void Init(int port, ServerType type)
        {
            _type = type;
            _port = port;
            _context = NetMQContext.Create();
            CreateServer();
        }

        void CreateServer()
        {
            switch (_type)
            {
                case ServerType.Response:
                    _serverSocket = _context.CreateResponseSocket(); break;
                case ServerType.Pub:
                    _serverSocket = _context.CreatePushSocket(); break;
                case ServerType.Router:
                    _serverSocket = _context.CreateRouterSocket(); break;
                case ServerType.Stream:
                    _serverSocket = _context.CreateStreamSocket(); break;
                case ServerType.Push:
                    _serverSocket = _context.CreatePushSocket(); break;
                case ServerType.XPub:
                    _serverSocket = _context.CreateXPublisherSocket(); break;
                default:
                    _serverSocket = _context.CreateResponseSocket(); break;
            }
            _serverSocket.Bind("tcp://*:" + _port);
            Task.Factory.StartNew(() =>
            AsyncRead(_serverSocket), TaskCreationOptions.LongRunning);
        }

        private void AsyncRead(NetMQSocket serverSocket)
        {
            while (true)
            {
                var msg = serverSocket.ReceiveMessage();
                OnOnReceive(new DataEventArgs<NetMQSocket, NetMQMessage>(serverSocket, msg));
            }
        }


        public NetMQSocket Server
        {
            get { return _serverSocket; }
        }

        public void Dispose()
        {
            _serverSocket.Dispose();
            _context.Dispose();
        }

        public void Send(NetMQMessage msg)
        {
            _serverSocket.SendMessage(msg);
        }

        public NetMQMessage CreateMessage()
        {
            return new NetMQMessage();
        }
    }
}
