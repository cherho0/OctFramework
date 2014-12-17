using System;
using System.Collections.Generic;
using Oct.Framework.Socket.Common;
using Oct.Framework.Socket.Protocal;
using Oct.Framework.Socket.Timer;

namespace Oct.Framework.Socket.Socket
{
    public delegate void ReceivedHandler(IClient client,VarList msg);
    public sealed class TcpServer : ServerApp<TcpServer>
    {
        #region Properties

        public override string Host
        {
            get { return CfgMgr.BasicCfg.IP; }
        }

        public override int Port
        {
            get { return CfgMgr.BasicCfg.Port; }
        }

        public long TotalReceiveMsgCount
        {
            get { return _totolReceiveMsgCount; }
        }

        public long TotalSendMsgCount
        {
            get { return _totalSendMsgCount; }
        }

        public long TotalBytesSend
        {
            get { return ClientBase.TotalBytesSent; }
        }

        public long TotoalBytesReceive
        {
            get { return ClientBase.TotalBytesReceived; }
        }

        public long TotalMsgCount
        {
            get { return MessageQueue.Count; }
        }

        public long TotalHeartBeats
        {
            get { return Updatables.Count; }
        }

        public long HeartInternal
        {
            get { return UpdateFrequency; }
        }

        public long TotalBufferCount
        {
            get { return ClientBase.TotalReceBufferCount; }
        }
        #endregion

        #region Events

        public event ReceivedHandler Received;

        #endregion

        #region Methods

        public override void Start()
        {
            base.Start();

            if (_running)
            {
                //TODO后续操作
            }
        }

        public override void Stop()
        {
            base.Stop();
        }

        protected override IClient CreateClient()
        {
            return new IMClient(this);
        }

        /// <summary>
        /// 客户端接入
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected override bool OnClientConnected(IClient client)
        {
            base.OnClientConnected(client);
            return true;
        }

        /// <summary>
        /// 激发收到消息事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="msg"></param>
        public void RaiseReceiveHandler(IClient client, VarList msg)
        {
            if (Received != null)
            {
                Received(client, msg);
            }
        }

        /// <summary>
        /// 剔除指定玩家
        /// </summary>
        /// <param name="client"></param>
        public void Kick(IClient client)
        {
            DisconnectClient(client, true);
        }

        /// <summary>
        /// 指定客户端发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="msg"></param>
        public void Send(IClient client, VarList msg)
        {
            AddSendMsgCount();
            client.Send(msg);
        }

        /// <summary>
        /// 指定客户端发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="msg"></param>
        public void Send(IEnumerable<IClient> clients, VarList msg)
        {
            foreach (IClient client in clients)
            {
                AddSendMsgCount();
                client.Send(msg);
            }
        }

        public void SendAll(VarList msg)
        {
            foreach (IClient client in _clients)
            {
                AddSendMsgCount();
                client.Send(msg);
            }
        }

        private new void AddSendMsgCount()
        {
            unchecked
            {
                _totalSendMsgCount++;
            }
        }

        #endregion

        #region Timers

        /// <summary>
        /// 注册心跳
        /// </summary>
        /// <param name="func"></param>
        /// <param name="delay"> </param>
        /// <param name="intervalMS"></param>
        /// <param name="count"></param>
        /// <param name="tag"></param>
        /// <param name="rsv"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
        public int Reg(Action<float> func, int delay, int intervalMS, int count = -1, object tag = null, object rsv = null, TimerType type = TimerType.Important)
        {
            TimerEntry timer;
            int tId;
            if (count == -1)
            {
                timer = new TimerEntry(delay, intervalMS, func);
                timer.Start();
                
                tId = timer.Id;
            }
            else
            {
                timer = new TimerEntry(delay, intervalMS, func, count);
                timer.Start();
                tId = timer.Id;
            }

            RegisterUpdatable(timer);
            return tId;
        }

        /// <summary>
        /// 取消心跳
        /// </summary>
        /// <param name="timer"></param>
        public void Unreg(int timer)
        {
            var update = Updatables.Find(x => x.Id == timer);
            if (update != null)
            {
                UnregisterUpdatable(update);
            }
        }

        /// <summary>
        /// 是否包含计时器
        /// </summary>
        /// <param name="timer"></param>
        /// <returns></returns>
        public bool ContainsTimer(int timer)
        {
            return Updatables.Find(x => x.Id == timer) != null;
        }

        public float GetIntervalMS(int timer)
        {
            //IUpdatable te;

            //Updatables.TryGetValue(timer, out te);

            //if (te != null)
            //{
            //    var t = te as TimerEntry;
            //    return t.Interval;
            //}

            var update = (TimerEntry)Updatables.Find(x => x.Id == timer);

            if (update != null)
            {
                return update.Interval;
            }

            return 0f;
        }
        #endregion

        public  int GetMsgCount()
        {
            return MessageQueue.Count;
        }
    }
}