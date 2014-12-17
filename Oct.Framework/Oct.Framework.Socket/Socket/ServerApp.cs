using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NyMQ.Core.Socket;
using Oct.Framework.Socket.Common;
using Oct.Framework.Socket.Timer;

namespace Oct.Framework.Socket.Socket
{
    public abstract class ServerApp<T> : ServerBase, IContextHandler
        where T : ServerBase, new()
    {
        #region Fields
        public static readonly DateTime StartTime;
        protected int CurrentThreadId;
        protected long LastUpdate;
        public ConcurrentQueue<IMessage> MessageQueue;
        private ConcurrentQueue<IMessage> LogicQueue; 
        protected Stopwatch QueueTimer;
        protected List<IUpdatable> Updatables;
        protected int UpdateFrequency;
        protected WaitHandle WaitHandle;
        protected long _totalSendMsgCount;
        protected long _totolReceiveMsgCount;
        protected TimerEntry m_shutdownTimer;
        //private ConcurrentQueue<IMessage> _switchQueue;
        //protected ConcurrentQueue<IMessage> LogicQueue; 
        //public event EventHandler<DataEventArgs<Exception>> OnError;
        protected Task _updateTask;
        //private int flag;
        #endregion

        #region Properties

        public static TimeSpan RunTime
        {
            get { return DateTime.Now - StartTime; }
        }

        /// <summary>
        /// 获取当前Server单例
        /// </summary>
        public static T Instance
        {
            get { return SingletonHolder<T>.Instance; }
        }

        public static bool IsShuttingDown { get; private set; }

        public static bool IsPreparingShutdown { get; private set; }

        public abstract string Host { get; }

        public abstract int Port { get; }

        public override bool IsRunning
        {
            get { return _running; }
            set
            {
                _running = value;

                if (value)
                {
                    // start message loop
                    //_updateTask = Task.Factory.StartNewDelayed(UpdateFrequency, QueueUpdateCallback, this);
                    ThreadPool.RegisterWaitForSingleObject(WaitHandle, QueueUpdateCallback, null, UpdateFrequency, true);
                }
                //Task.Factory.StartNew(() => QueueUpdateCallback(null, true));
                //ThreadPool.RegisterWaitForSingleObject(WaitHandle, Heartbeat, null, UpdateFrequency, true);
                //ThreadPool.RegisterWaitForSingleObject(WaitHandle, QueueUpdateCallback, null, UpdateFrequency, true);
            }
        }

        #endregion

        #region Initialize

        static ServerApp()
        {
            StartTime = DateTime.Now;
        }

        protected ServerApp()
        {
            WaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            Updatables = new List<IUpdatable>();
            MessageQueue = new ConcurrentQueue<IMessage>();
            LogicQueue = new ConcurrentQueue<IMessage>();
            QueueTimer = Stopwatch.StartNew();
            UpdateFrequency = 50;
            LastUpdate = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取当前程序集版本号
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                Assembly asm = Assembly.GetEntryAssembly();
                if (asm != null)
                {
                    Version ver = asm.GetName().Version;
                    return string.Format("{0}.{1} ({2}#{3})", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                return string.Format("[Cannot get AssemblyVersion]");
            }
        }

        #endregion

        #region Timers

        /// <summary>
        /// 注册更新对象
        /// </summary>
        /// <param name="updatable"></param>
        public void RegisterUpdatable(IUpdatable updatable)
        {
            AddMessage(() => Updatables.Add(updatable));
        }

        /// <summary>
        /// 反注册更新对象
        /// </summary>
        /// <param name="updatable"></param>
        public void UnregisterUpdatable(IUpdatable updatable)
        {
            updatable.IsRunning = false;
            AddMessage(() => Updatables.Remove(updatable));
        }

        #endregion

        #region Task Pool

        /// <summary>
        /// 将一个任务加入执行队列
        /// </summary>
        public void AddMessage(Action action)
        {
            //if (flag == 0)
            //{
            //    LogicQueue.Enqueue(new Message(action));
            //}
            //else
            //{
                MessageQueue.Enqueue(new Message(action));
            //}
            
        }

        public bool ExecuteInContext(Action action)
        {
            if (!IsInContext)
            {
                AddMessage(new Message(action));
                return false;
            }

            action();
            return true;
        }

        public void EnsureContext()
        {
            if (Thread.CurrentThread.ManagedThreadId != CurrentThreadId)
            {
                throw new InvalidOperationException("Not in context");
            }
        }

        /// <summary>
        /// 判断当前线程是否是处理消息队列的线程
        /// </summary>
        public bool IsInContext
        {
            get { return Thread.CurrentThread.ManagedThreadId == CurrentThreadId; }
        }

        /// <summary>
        /// 加入消息队列
        /// </summary>
        /// <param name="msg"></param>
        public void AddMessage(IMessage msg)
        {
            //if (flag == 0)
            //{
            //    LogicQueue.Enqueue(msg);
            //}
            //else
            //{
                MessageQueue.Enqueue(msg);
            //}
           
        }
        //Stopwatch sw = new Stopwatch();
        protected void QueueUpdateCallback(object state,bool timeOut)
        {
            if (!_running)
            {
                return;
            }

            CurrentThreadId = Thread.CurrentThread.ManagedThreadId;
            var timerStart = QueueTimer.ElapsedMilliseconds;
            var updateDt = (timerStart - LastUpdate) / 1000.0f;
            //Parallel.ForEach(Updatables, (x, y) => x.Update(updateDt));

            //sw.Restart();
            var gcupdates = Updatables.Where(p => !p.IsRunning).ToList();
            foreach (var updatable in gcupdates)
            {
                Updatables.Remove(updatable);
            }

            foreach (var updatable in Updatables)
            {
                try
                {
                    updatable.Update(updateDt);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    Csl.WlEx(e);
                }
            }

            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 100)
            //{
            //    Csl.Wl("Heart:" + sw.ElapsedMilliseconds.ToString());
            //}


            LastUpdate = QueueTimer.ElapsedMilliseconds;
            //sw.Restart();
            IMessage msg;

            //if (flag ==1)
            //{
                while (LogicQueue.TryDequeue(out msg))
                {
                    try
                    {
                        msg.Execute();
                        if (!_running)
                        {
                            return;
                        }
                    }
                    
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        Csl.WlEx(ex);
                    }
                }

            //    Interlocked.Exchange(ref flag, 0);
            //}
            //else
            //{
            //    while (MessageQueue.TryDequeue(out msg))
            //    {
            //        try
            //        {
            //            msg.Execute();
            //            if (!_running)
            //            {
            //                return;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Log.Error(ex);
            //            Csl.WlEx(ex);
            //        }
            //    }

            //    Interlocked.Exchange(ref flag, 1);
            //}
            


            //sw.Stop();

            //if (sw.ElapsedMilliseconds > 100)
            //{
            //    Csl.Wl("Msg: " + sw.ElapsedMilliseconds.ToString());
            //}
            long timerStop = QueueTimer.ElapsedMilliseconds;
            bool updateLagged = timerStop - timerStart > UpdateFrequency;
            long callbackTimeout = updateLagged ? 0 : ((timerStart + UpdateFrequency) - timerStop);

            CurrentThreadId = 0;

            //if (_running)
            //{

            //    // re-register the Update-callback
            //    _updateTask = Task.Factory.StartNewDelayed((int)callbackTimeout, QueueUpdateCallback, this);
            //}

            if (_running)
            {
                //Csl.Wl(ConsoleColor.Blue, string.Format("LogicQueue: {0},MessageQueue: {1}", LogicQueue.Count, MessageQueue.Count));
                MessageQueue = Interlocked.Exchange(ref LogicQueue, MessageQueue);
                //Swap(ref LogicQueue, ref MessageQueue);
                //Csl.Wl(ConsoleColor.DarkRed, string.Format("LogicQueue: {0},MessageQueue: {1}",LogicQueue.Count,MessageQueue.Count));
                if (callbackTimeout < 0)
                {
                    callbackTimeout = 0;
                }
                ThreadPool.RegisterWaitForSingleObject(WaitHandle, QueueUpdateCallback, null, callbackTimeout, true);
            }

        }

        /// <summary>
        /// 交换消息队列
        /// </summary>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        private void Swap(ref ConcurrentQueue<IMessage> q1,ref ConcurrentQueue<IMessage> q2)
        {
            var temp = q1;
            q1 = q2;
            q2 = temp;
        }
        #endregion

        #region Start
        /// <summary>
        /// Starts the server and performs and needed initialization.
        /// </summary>
        public override void Start()
        {
            base.Start();
            if (!(_running = _tcpEnabled))
            {
                Stop();
                return;
            }

            //GC.Collect(2, GCCollectionMode.Optimized);
        }

        #endregion

        #region Shutdown

        /// <summary>
        /// 服务器关闭时触发该事件
        /// </summary>
        public static event Action Shutdown;

        /// <summary>
        /// 延迟指定时间后关闭
        /// </summary>
        /// <param name="delayMillis"></param>
        public virtual void ShutdownIn(uint delayMillis)
        {
            IsShuttingDown = true;
            m_shutdownTimer = new TimerEntry(delayMillis / 1000f, 0f, upd =>
            {
                //AppUtil.UnhookAll();
                if (IsRunning)
                {
                    _OnShutdown();
                }
            });

            m_shutdownTimer.Start();
            if (!IsPreparingShutdown)
            {
                RegisterUpdatable(m_shutdownTimer);
                IsPreparingShutdown = true;
            }
        }

        public virtual void CancelShutdown()
        {
            //if (IsPreparingShutdown)
            //{
            //    UnregisterUpdatable(m_shutdownTimer);
            //    m_shutdownTimer.Stop();
            //    IsPreparingShutdown = false;
            //}
        }

        private void _OnShutdown()
        {
            IsShuttingDown = true;

            Action evt = Shutdown;
            if (evt != null)
            {
                evt();
            }

            OnShutdown();
            Stop();
            Thread.Sleep(1000);

            Process.GetCurrentProcess().CloseMainWindow();
        }

        protected virtual void OnShutdown()
        {
        }

        #endregion

        #region Private methods

        public void AddReceivemMsgCount()
        {
            unchecked
            {
                _totolReceiveMsgCount++;
            }
        }

        public void AddSendMsgCount()
        {
            unchecked
            {
                _totalSendMsgCount++;
            }
        }
        #endregion
    }

    internal static class SingletonHolder<TSingle> where TSingle : new()
    {
        internal static readonly TSingle Instance = new TSingle();
    }
}