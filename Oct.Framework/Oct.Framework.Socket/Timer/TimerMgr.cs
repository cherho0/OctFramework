using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Oct.Framework.Socket.Common;

namespace Oct.Framework.Socket.Timer
{
    public class TimerMgr
    {
        #region Fields
        private readonly Stopwatch _queueTimer;
        private readonly List<IUpdatable> _updatables;
        private readonly int _updateFrequency;
        private readonly WaitHandle _waitHandle;
        private int _currentThreadId;
        private long _lastUpdate;
        private List<IUpdatable> _tempUpdateTables;
        #endregion

        #region Properties
        public List<IUpdatable> UpdateTables
        {
            get { return _updatables; }
        }
        #endregion

        #region Constructor

        public TimerMgr()
        {
            _updateFrequency = CfgMgr.BasicCfg.UpdateFrequency;
            _waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            _updatables = new List<IUpdatable>();
            _tempUpdateTables = new List<IUpdatable>();
            _queueTimer = Stopwatch.StartNew();
            ThreadPool.RegisterWaitForSingleObject(_waitHandle, QueueUpdateCallback, null, _updateFrequency, true);
        }

        #endregion

        #region Methods

        protected void QueueUpdateCallback(object state, bool timedOut)
        {
            _currentThreadId = Thread.CurrentThread.ManagedThreadId;

            long timerStart = _queueTimer.ElapsedMilliseconds;
            float updateDt = (timerStart - _lastUpdate)/1000.0f;

            //Monitor.Enter(_updatables);
            // 运行计时器
            Parallel.ForEach(_updatables, (u, p) =>
                                              {
                                                  u.Update(updateDt);
                                              }
                );
            //删除已停止的计时器
            _updatables.RemoveAll(x => !x.IsRunning);
            if (_tempUpdateTables.Count > 0)
            {
                _updatables.AddRange(_tempUpdateTables);
                _tempUpdateTables.Clear();
            }
            //Monitor.Exit(_updatables);

            _lastUpdate = _queueTimer.ElapsedMilliseconds;

            long timerStop = _queueTimer.ElapsedMilliseconds;
            bool updateLagged = timerStop - timerStart > _updateFrequency;
            long callbackTimeout = updateLagged ? 0 : ((timerStart + _updateFrequency) - timerStop);

            _currentThreadId = 0;

            ThreadPool.RegisterWaitForSingleObject(_waitHandle, QueueUpdateCallback, null, callbackTimeout, true);
        }

        /// <summary>
        /// 注册更新对象
        /// </summary>
        /// <param name="updatable"></param>
        public void RegisterUpdatable(IUpdatable updatable)
        {
            //Monitor.Enter(_tempUpdateTables);
            _tempUpdateTables.Add(updatable);
            //Monitor.Exit(_tempUpdateTables);
        }

        #endregion

        #region Timers

        /// <summary>
        /// 注册心跳
        /// </summary>
        /// <param name="func"></param>
        /// <param name="intervalMS"></param>
        /// <param name="count"></param>
        /// <param name="tag"></param>
        /// <param name="rsv"></param>
        /// <returns></returns>
        public int Reg(Action<float> func, int delay, int intervalMS,
                        int count = -1,
                       object tag = null,
                        object rsv = null)
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

            //Monitor.Enter(_tempUpdateTables);
            _tempUpdateTables.Add(timer);
            //Monitor.Exit(_tempUpdateTables);
            return tId;
        }

        /// <summary>
        /// 取消心跳
        /// </summary>
        /// <param name="timer"></param>
        public void Unreg(int timer)
        {
            var update = _updatables.Find(x => x.Id == timer);

            if (update != null)
            {
                var t = (TimerEntry)update;
                t.Stop();
            }
        }

        /// <summary>
        /// 是否包含计时器
        /// </summary>
        /// <param name="timer"></param>
        /// <returns></returns>
        public bool ContainsTimer(int timer)
        {
            return _updatables.Find(x => x.Id == timer) != null;
        }

        public float GetIntervalMS(int timer)
        {
            var update = (TimerEntry)_updatables.Find(x => x.Id == timer);

            if (update != null)
            {
                return update.Interval;
            }

            return 0f;
        }
        #endregion
    }
}