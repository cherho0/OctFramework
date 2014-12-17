using System;
using System.Collections.Generic;
using System.IO;
using NyMQ.Core.Socket;
using Oct.Framework.Socket.BaseDef;
using Oct.Framework.Socket.Common;
using Oct.Framework.Socket.Protocal;
using ProtoBuf;

namespace Oct.Framework.Socket.Socket
{
    public sealed class IMClient : ClientBase, IEquatable<IMClient>
    {
        #region Fields
        #endregion

        #region Properties

        /// <summary>
        /// 服务器
        /// </summary>
        public new TcpServer Server
        {
            get { return (TcpServer)_server; }
        }

        #endregion

        public IMClient(ServerBase server)
            : base(server)
        {

        }

        public IMClient()
        {
        }

        #region IEquatable<GameClient> Members

        public bool Equals(IMClient other)
        {
            return other != null
                   && other.ClientAddress == ClientAddress
                   && other.Port == Port;
        }

        #endregion

        #region ITcpClient Members
        //Stopwatch sw = new Stopwatch();
        public override void Send(VarList msg)
        {
            //sw.Restart();
            if (_tcpSock == null || !_tcpSock.Connected)
            {
                return;
            }

            try
            {
                using (var ms = new MemoryStream())
                {
                    //sw.Stop();

                    //if (sw.ElapsedMilliseconds > 50)
                    //{
                    //    Csl.Wl("memory msg " + sw.ElapsedMilliseconds);
                    //}
                    //sw.Restart();
                    //包头定义
                    //包头定义
                    //using (var ph = new PackageHead())
                    //{
                    //sw.Stop();

                    //if (sw.ElapsedMilliseconds > 50)
                    //{
                    //    Csl.Wl("head msg " + sw.ElapsedMilliseconds);
                    //}
                    //sw.Restart();
                    ms.WriteByte((byte)SocketHelper.H1);
                    ms.WriteByte((byte)SocketHelper.H2);
                    ms.WriteByte((byte)SocketHelper.H3);
                    ms.WriteByte((byte)SocketHelper.H4);
                    //ph.GetHeadBytes(ms);
                    ms.Position = 8;

                    //消息序列化
                    Serializer.Serialize(ms, msg);
                    int count = Convert.ToInt32(ms.Position - 8);

                    //设置包体长度
                    ms.Position = 4;
                    ms.Write(BitConverter.GetBytes(count), 0, 4);
                    //ph.Write(ms, count);

                    //sw.Stop();

                    //if (sw.ElapsedMilliseconds > 50)
                    //{
                    //    msg.ToString();
                    //    Csl.Wl("Serialize msg " + sw.ElapsedMilliseconds);
                    //}
                    //sw.Restart();
                    Send(ms.GetBuffer(), 0, count + 8);
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                Csl.Wl(ConsoleColor.Red, ex.StackTrace);
            }

            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 50)
            //{
            //    Csl.Wl("Send msg " + sw.ElapsedMilliseconds);
            //}
        }

        //Stopwatch sw1 = new Stopwatch();
        /// <summary>
        /// 延迟拼包发送
        /// </summary>
        /// <param name="msg"></param>
        public override void SendDelay(IEnumerable<VarList> msgs)
        {
            //sw1.Restart();
            if (_tcpSock == null || !_tcpSock.Connected)
            {
                return;
            }
            try
            {
                using (var ms = new MemoryStream())
                {
                    long lastPosition = 0;
                    foreach (var msg in msgs)
                    {
                        //包头定义
                        var ph = new PackageHead();
                        ms.Position = lastPosition;
                        ph.GetHeadBytes(ms);
                        ms.Position = lastPosition + ph.HEADSIZE + ph.BODYLENGTH;
                        Serializer.Serialize(ms, msg);

                        var count = Convert.ToInt32(ms.Position - ph.HEADSIZE - ph.BODYLENGTH - lastPosition);
                        lastPosition = ms.Position;
                        ms.Position = lastPosition - count - ph.BODYLENGTH;
                        ph.Write(ms, count);
                        ph.Dispose();
                    }

                    Send(ms.GetBuffer(), 0, (int)lastPosition);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                Csl.Wl(ConsoleColor.Red, ex.StackTrace);
            }

            //sw1.Stop();

            //if (sw1.ElapsedMilliseconds > 50)
            //{
            //    Csl.Wl(ConsoleColor.Red, "Send msgs " + sw1.ElapsedMilliseconds);
            //}
        }
        #endregion

        //Tencent 验证
        protected override bool OnReceive(DataBuffer buffer)
        {
            using (var ms = new MemoryStream(buffer.Data))
            {
                //using (var ph = new PackageHead())
                //{
                do
                {
                    if (ms.Position + 8 >= buffer.Count)
                    {
                        return false;
                    }

                    //var validate = ph.CheckPackageHead(ms);

                    //if (!validate)
                    //{
                    //    return false;
                    //}


                    int length = 0;
                    //包头验证
                    if (buffer.Data[ms.Position] == SocketHelper.H1 && buffer.Data[ms.Position + 1] == SocketHelper.H2 && buffer.Data[ms.Position + 2] == SocketHelper.H3 && buffer.Data[ms.Position + 3] == SocketHelper.H4)
                    {
                        length = buffer.Data[ms.Position + 4] + buffer.Data[ms.Position + 5] * 256;
                        if (length > 0)
                        {
                            if (ms.Position + 8 + length > buffer.Count)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (ms.Position == buffer.Count)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                    ms.Position = ms.Position + 8;
                    using (var msgMemory = new MemoryStream(buffer.Data, Convert.ToInt32(ms.Position), length))
                    {
                        var msg = Serializer.Deserialize<VarList>(msgMemory);
                        //流位移
                        ms.Position = ms.Position + length;
                        _offset = (int)ms.Position;
                        _remainingLength = (int)(buffer.Count - ms.Position);
                        var message = new Message2<IClient, VarList>(this, msg) { Callback = MessageHandler };

                        if (Server != null)
                        {
                            //Server.AddReceivemMsgCount();

                            if (Server.MessageQueue.Count < 500)
                            {
                                Server.AddMessage(message);
                            }
                            else
                            {
                                if (msg.Type(0) == VarType.Int32 && (msg.Int32(0) != 8001 && msg.Int32(0) != 8000 && msg.Int32(0) != 5000))
                                {
                                    Server.AddMessage(message);
                                }
                            }
                        }
                        else
                        {
                            RaiseReceiveHandler(msg);
                        }
                    }

                } while (_remainingLength > 0);

                return true;
                //}
            }
        }

        private void MessageHandler(IClient client, VarList msg)
        {
            if (IsAutherized)
            {
                RaiseReceiveHandler(msg);
            }
            else
            {
                Server.RaiseReceiveHandler(client, msg);
            }
        }

        public override string ToString()
        {
            return string.Format("IP:{0},Port:{1}", ClientAddress, Port);
        }
    }
}