using System.Net.Sockets;
using Oct.Framework.Socket.Buffer;
using Oct.Framework.Socket.Common;

namespace Oct.Framework.Socket.Socket
{
    public static class SocketHelpers
    {
        static SocketHelpers()
        {
            if (!ObjectPoolMgr.ContainsType<SocketAsyncEventArgs>())
            {
                ObjectPoolMgr.RegisterType(CreateSocketArg);
                ObjectPoolMgr.SetMinimumSize<SocketAsyncEventArgs>(CfgMgr.BasicCfg.MaxNumberOfConnections); 
            }
        }


        private static SocketAsyncEventArgs CreateSocketArg()
        {
            var arg = new SocketAsyncEventArgs();
            return arg;
        }

        private static void CleanSocketArg(SocketAsyncEventArgs arg)
        {
        }

        public static SocketAsyncEventArgs AcquireSocketArg()
        {
            var args = ObjectPoolMgr.ObtainObject<SocketAsyncEventArgs>();
            CleanSocketArg(args);
            return args;
        }

        public static void ReleaseSocketArg(SocketAsyncEventArgs arg)
        {
            ObjectPoolMgr.ReleaseObject(arg);
        }


        public static void SetListenSocketOptions(System.Net.Sockets.Socket socket)
        {
            socket.NoDelay = true;
        }
    }
}