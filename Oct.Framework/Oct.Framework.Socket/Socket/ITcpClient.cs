using NyMQ.Core.Socket;

namespace Oct.Framework.Socket.Socket
{
    internal interface ITcpClient : IClient
    {
        /// <summary>
        /// The server this client is connected to.
        /// </summary>
        new TcpServer Server { get; }
    }
}