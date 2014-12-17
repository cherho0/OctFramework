using System;

namespace Oct.Framework.Socket.Timer
{
    /// <summary>
    /// 支持一次或多次触发的计时器对象
    /// </summary>
    public class TimerEntry : IDisposable, IUpdatable
    {
        #region Fields
        private readonly int _id;
        public Action<float> Action;
        public float Interval;
        public float RemainingInitialDelay;
        private float _totalTime;
        private int _count = -1;
        #endregion

        #region Properties

        /// <summary>
        /// 计时器运行总时间
        /// </summary>
        public float TotalTime
        {
            get { return _totalTime; }
        }

        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// 计时器是否还在运行
        /// </summary>
        public bool IsRunning
        {
            get { return _totalTime >= 0; }
            set
            {
                if (!value)
                {
                    _totalTime = -1f;
                }
            }
        }

        #endregion

        #region Constructor

        public TimerEntry()
        {
            _id = TimerHelper.Id;
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="delay">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TimerEntry(float delay, float interval, Action<float> callback)
        {
            _totalTime = -1.0f;
            Action = callback;
            RemainingInitialDelay = delay;
            Interval = interval;
            _id = TimerHelper.Id;
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="delay">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TimerEntry(float delay, float interval, Action<float> callback, int count)
        {
            _totalTime = -1.0f;
            Action = callback;
            RemainingInitialDelay = delay;
            Interval = interval;
            _id = TimerHelper.Id;
            _count = count;
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="delay">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TimerEntry(int delay, int interval, Action<float> callback)
            : this(delay / 1000.0f, interval / 1000.0f, callback)
        {
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="delay">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TimerEntry(int delay, int interval, Action<float> callback, int count)
            : this(delay / 1000.0f, interval / 1000.0f, callback, count)
        {
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="delay">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TimerEntry(uint delay, uint interval, Action<float> callback)
            : this(delay / 1000.0f, interval / 1000.0f, callback)
        {
        }

        public TimerEntry(Action<float> callback)
            : this(0, 0, callback)
        {
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// 停止并且清楚计时器
        /// </summary>
        public void Dispose()
        {
            Stop();
            Action = null;
        }

        #endregion

        #region IUpdatable Members

        /// <summary>
        /// 计时器更新
        /// </summary>
        /// <param name="updateDelta"></param>
        public void Update(float updateDelta)
        {
            if (_totalTime == -1f)
                return;

            if (RemainingInitialDelay > 0.0f)
            {
                RemainingInitialDelay -= updateDelta;

                if (RemainingInitialDelay <= 0.0f)
                {
                    if (Interval == 0.0f)
                    {
                        Stop();
                    }

                    Action(_totalTime);
                    if (_totalTime != -1)
                    {
                        _totalTime = 0;
                    }
                }
            }
            else
            {
                _totalTime += updateDelta;
                if (_totalTime >= Interval)
                {
                    Action(_totalTime);
                    if (_count != -1)
                    {
                        _count--;
                    }
                    if (_count == 0)
                    {
                        Dispose();
                    }
                    if (_totalTime != -1)
                    {
                        _totalTime -= Interval;
                    }
                }
            }

        }

        #endregion

        /// <summary>
        /// 开启计时器
        /// </summary>
        public void Start()
        {
            _totalTime = 0.0f;
        }

        /// <summary>
        /// 指定延迟时间
        /// </summary>
        public void Start(float initialDelay)
        {
            RemainingInitialDelay = initialDelay;
            _totalTime = 0.0f;
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        /// <param name="initialDelay">延迟启动时间</param>
        /// <param name="interval">时间间隔</param>
        public void Start(float initialDelay, float interval)
        {
            RemainingInitialDelay = initialDelay;
            Interval = interval;
            _totalTime = 0.0f;
        }

        /// <summary>
        /// 延迟启动
        /// </summary>
        public void Start(int initialDelay)
        {
            Start(initialDelay / 1000.0f);
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        /// <param name="initialDelay">延迟启动时间</param>
        /// <param name="interval">时间间隔</param>
        public void Start(int initialDelay, int interval)
        {
            Start(initialDelay / 1000.0f, interval / 1000.0f);
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        /// <param name="initialDelay">延迟启动时间</param>
        /// <param name="interval">时间间隔</param>
        /// <param name="callback">回调方法 </param>
        public void Start(uint initialDelay, uint interval, Action<float> callback)
        {
            Action = callback;

            Start(initialDelay / 1000.0f, interval / 1000.0f);
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        public void Stop()
        {
            _totalTime = -1f;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(TimerEntry)) return false;
            return Equals((TimerEntry)obj);
        }

        public bool Equals(TimerEntry obj)
        {
            return obj.Interval == Interval && Equals(obj.Action, Action);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = ((int)(Interval * 397)) ^ (Action != null ? Action.GetHashCode() : 0);
                return result;
            }
        }
    }
}