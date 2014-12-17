using System;

namespace Oct.Framework.Socket.Socket
{
    [Serializable]
    public class NoAvailableAdaptersException : Exception
    {
        public NoAvailableAdaptersException()
        {
        }

        public NoAvailableAdaptersException(string message)
            : base(message)
        {
        }
    }
}