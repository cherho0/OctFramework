using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.zmq;
using Oct.Framework.Core.Args;
using Oct.Framework.Core.Common;
using Oct.Framework.MQ;

namespace Oct.Framework.MQDemo
{
    class Program
    {
        private static OctMQServer server;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            server = new OctMQServer();
            server.OnReceive += server_OnReceive;
            server.Init(5555, ServerType.Pub);
            Cmd();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Csl.WlEx((Exception)e.ExceptionObject);
        }

        private static void Cmd()
        {
            var cmd = System.Console.ReadLine();


            switch (cmd)
            {
                case "exit":
                    Csl.Wl("正在关闭应用程序。。。等待最后一个心跳执行完成。。。");
                    server.Dispose();

                    break;

                default:
                    {
                        var str = cmd.Split(' ');
                        var msg = server.CreateMessage();
                        msg.Append(str[0]);
                        msg.Append(str[1]);
                        server.Send(msg);
                        Cmd();
                        break;
                    }
                    return;
            }
        }

        static void server_OnReceive(object sender, DataEventArgs<NetMQ.NetMQSocket, NetMQ.NetMQMessage> e)
        {
            var msg = e.Arg2;
            var server = e.Arg1;
            Csl.Wl(msg.Pop().ConvertToString());
            server.Send("你好");
        }
    }
}
