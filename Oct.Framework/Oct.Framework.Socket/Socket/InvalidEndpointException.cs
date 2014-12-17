using System;
using System.Net;

namespace Oct.Framework.Socket.Socket
{
    [Serializable]
    public class InvalidEndpointException : Exception
    {
        private readonly IPEndPoint _endpoint;

        public InvalidEndpointException(IPEndPoint ep)
        {
            _endpoint = ep;
        }

        public InvalidEndpointException(IPEndPoint ep, string message)
            : base(message)
        {
            _endpoint = ep;
        }

        public IPEndPoint Endpoint
        {
            get { return _endpoint; }
        }
    }
}