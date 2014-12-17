using System;
using System.Collections.Generic;
using System.Net;
using NyMQ.Core.Socket;
using Oct.Framework.Socket.Common;
using Oct.Framework.Socket.Protocal;

namespace Oct.Framework.Socket.Socket
{
    public delegate void ClientReceivedHandler(VarList msg);
    public interface IClient : IDisposable
    {
        ServerBase Server { get; }

        /// <summary>
        /// 是否已经验证通过
        /// </summary>
        bool IsAutherized { get; set; }

        /// <summary>
        /// IP Address
        /// </summary>
        IPAddress ClientAddress { get; }

        /// <summary>
        /// 获取当前端口
        /// </summary>
        int Port { get; }

        /// <summary>
        /// 获取当前Socket
        /// </summary>
        System.Net.Sockets.Socket TcpSocket { get; set; }

        bool IsConnected { get; }

        event EventHandler ClientConnected;
        event EventHandler<DataEventArgs<IClient>> Closed;
        //event EventHandler<DataEventArgs<Exception>> Error;
        event ClientReceivedHandler Received;

        /// <summary>
        /// 异步接收消息
        /// </summary>
        void BeginReceive();

        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="packet"></param>
        void Send(byte[] packet);

        /// <summary>
        /// 异步发送复制消息包
        /// </summary>
        /// <param name="packet"></param>
        void SendCopy(byte[] packet);

        /// <summary>
        /// 发送消息包指定位置开始指定长度的字节
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="length"></param>
        /// <param name="offset"></param>
        void Send(byte[] packet, int offset, int length);

        /// <summary>
        /// 发送指定消息号
        /// </summary>
        /// <param name="msg"></param>
        void Send(VarList msg);

        /// <summary>
        /// 延迟拼包发送
        /// </summary>
        /// <param name="msgs"></param>
        void SendDelay(IEnumerable<VarList> msgs);

        /// <summary>
        /// 批量发送
        /// </summary>
        /// <param name="content"></param>
        void Send(IList<ArraySegment<byte>> content);

        /// <summary>
        /// 同步连接
        /// </summary>
        /// <remarks></remarks>
        /// <param name="host"></param>
        /// <param name="port"></param>
        void Connect(string host, int port);

        /// <summary>
        /// 同步连接
        /// </summary>
        /// <remarks></remarks>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        void Connect(IPAddress addr, int port);

        /// <summary>
        /// 异步连接
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        void ConnectAsync(string host, int port);

        /// <summary>
        /// 异步连接
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        void ConnectAsync(IPAddress addr, int port);
    }
}