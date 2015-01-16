using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NetMQ;
using NetMQ.Sockets;
using NetMQ.zmq;
using Oct.Framework.Core.Args;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.MQ;
using Oct.Framework.MQDemo.Base;
using Oct.Framework.MQDemo.Commands;
using Oct.Framework.MQDemo.Queues;

namespace Oct.Framework.MQDemo
{
   public  class Program
    {
        private static OctMQServer _server;
        static ServerType _type;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            CreateCmd();

        }

        /// <summary>
        /// 创建mq对象
        /// </summary>
        static void Create()
        {
            _server = new OctMQServer();
            _server.OnReceive += server_OnReceive;
            _server.Init(5555, _type);
            // _server.Server.Options.
        }

        /// <summary>
        /// 选择类型
        /// </summary>
        private static void CreateCmd()
        {
            Csl.Wl(ConsoleColor.Red, "请选择您要创建的MQ服务端类型");
            Csl.Wl(ConsoleColor.Yellow, "1.PUB   2.REP");
            var key = System.Console.ReadLine();
            switch (key)
            {
                case "1":
                    {
                        _type = ServerType.XPub;
                        Create();
                        Cmd();
                    }

                    break;
                case "2":
                    _type = ServerType.Response;
                    Create();
                    InitQueue();
                    Cmd();

                    break;
                default:
                    {
                        CreateCmd();

                    }
                    break;
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Csl.WlEx((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private static void Cmd()
        {
            if (_type == ServerType.Pub)
            {
                Csl.Wl(ConsoleColor.Red, "请输入您要发个订阅者的信息主题与信息用空格分开");
            }
            else
            {
                Csl.Wl(ConsoleColor.Red, "等待消息");
            }
            var cmd = System.Console.ReadLine();

            switch (cmd)
            {
                case "exit":
                    Csl.Wl("正在关闭应用程序。。。等待最后一个心跳执行完成。。。");
                    _server.Dispose();
                    break;

                default:
                    {
                        var str = cmd.Split(' ');
                        var msg = _server.CreateMessage();
                        msg.Append(str[0], Encoding.UTF8);
                        msg.Append(str[1], Encoding.UTF8);
                        _server.Send(msg);
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
            if (msg.FrameCount != 2)
            {
                Csl.Wl(msg.Pop().ConvertToString(Encoding.UTF8));
                server.Send("你好,您的请求已处理，并返回消息及处理结果", Encoding.UTF8);

                return;
            }
            SeatCommand cmd = new SeatCommand();
            var back = cmd.Exec(msg);
            server.SendMessage(back);
        }

        static void InitCmd()
        {
            var container = Bootstrapper.CreateContainer();

        }

        public static SeatQueue queue;
        static void InitQueue()
        {
            queue = new SeatQueue();
            queue.Init(20);
            Csl.Wl("初始化队列完成");
        }
    }
}
