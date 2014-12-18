using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using Oct.Framework.Core.Args;

namespace Oct.Framework.MQ
{
    /// <summary>
    /// MQ客户端
    /// </summary>
    public class OctMQClient
    {
        public event EventHandler<DataEventArgs<NetMQSocket, NetMQMessage>> OnReceive;

        protected virtual void OnOnReceive(DataEventArgs<NetMQSocket, NetMQMessage> e)
        {
            EventHandler<DataEventArgs<NetMQSocket, NetMQMessage>> handler = OnReceive;
            if (handler != null) handler(this, e);
        }

        private int _port;
        private NetMQSocket _clientSocket;
        private ClientType _type;
        private NetMQContext _context;
        private string _ip;
        public void Init(string ip, int port, ClientType type)
        {
            _type = type;
            _ip = ip;
            _port = port;
            _context = NetMQContext.Create();
            CreateClient();
        }

        void CreateClient()
        {
            switch (_type)
            {
                case ClientType.Request:
                    _clientSocket = _context.CreateRequestSocket(); break;
                case ClientType.Sub:
                    _clientSocket = _context.CreateSubscriberSocket(); break;
                case ClientType.Dealer:
                    _clientSocket = _context.CreateDealerSocket(); break;
                case ClientType.Stream:
                    _clientSocket = _context.CreateStreamSocket(); break;
                case ClientType.Pull:
                    _clientSocket = _context.CreatePullSocket(); break;
                case ClientType.XSub:
                    _clientSocket = _context.CreateXSubscriberSocket(); break;
                default:
                    _clientSocket = _context.CreateRequestSocket(); break;
            }
            _clientSocket.Connect("tcp://" + _ip + ":" + _port);
        }

        public void StartAsyncReceive()
        {
            Task.Factory.StartNew(() =>
         AsyncRead(_clientSocket), TaskCreationOptions.LongRunning);
        }

        private void AsyncRead(NetMQSocket cSocket)
        {
            while (true)
            {
                var msg = cSocket.ReceiveMessage();
                OnOnReceive(new DataEventArgs<NetMQSocket, NetMQMessage>(cSocket, msg));
            }
        }

        public NetMQSocket Client
        {
            get { return _clientSocket; }
        }

        public T GetClient<T>() where T : NetMQSocket
        {
            return (T)_clientSocket;
        }

        public void Send(NetMQMessage msg)
        {
            _clientSocket.SendMessage(msg);
        }

        public NetMQMessage CreateMessage()
        {
            return new NetMQMessage();
        }

        public NetMQMessage ReceiveMessage()
        {
            return _clientSocket.ReceiveMessage();
        }
    }
}
