using System;

namespace Oct.Framework.Socket.Timer
{
    /// <summary>
    /// 支持一次或多次触发的计时器对象
    /// </summary>
    public class TickTimer : IDisposable, IUpdatable
    {
        #region Fields

        private readonly int _id;
        private readonly float _maxTicks;
        public Action<float> Action;
        public float Interval;
        private float _ticks;
        private float _totalTime;

        #endregion

        #region Properties

        /// <summary>
        /// 总时间
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
        /// 计时器是否在工作
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

        #region Construcotr

        public TickTimer()
        {
            _id = TimerHelper.Id;
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="ticks">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TickTimer(int interval, int ticks, Action<float> callback)
        {
            _totalTime = interval/1000.0f;
            Action = callback;
            Interval = interval;
            _maxTicks = ticks;
            _id = TimerHelper.Id;
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="ticks">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TickTimer(uint interval, int ticks, Action<float> callback)
        {
            _totalTime = interval/1000.0f;
            Action = callback;
            Interval = interval;
            _maxTicks = ticks;
            _id = TimerHelper.Id;
        }

        /// <summary>
        /// 计时器对象
        /// </summary>
        /// <param name="ticks">延迟触发时间</param>
        /// <param name="interval">触发间隔</param>
        /// <param name="callback">回调方法</param>
        public TickTimer(float interval, int ticks, Action<float> callback)
        {
            _totalTime = interval;
            Action = callback;
            Interval = interval;
            _maxTicks = ticks;
            _id = TimerHelper.Id;
        }

        #endregion

        #region Public methods

        #region IDisposable Members

        /// <summary>
        /// 停止并销毁计时器
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
            //计时器已经停止
            if (_totalTime == -1f)
                return;

            _totalTime += updateDelta;

            if (_totalTime >= Interval)
            {
                Action(_totalTime);
                if (_totalTime != -1)
                {
                    _totalTime -= Interval;
                    _ticks++;
                    if (_ticks == _maxTicks)
                    {
                        Stop();
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 计时器开始起作用
        /// </summary>
        public void Start()
        {
            _ticks = 0;
            _totalTime = 0.0f;
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop()
        {
            _totalTime = -1f;
        }

        public bool Equals(TimerEntry obj)
        {
            return obj.Interval == Interval && Equals(obj.Action, Action);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof (TimerEntry)) return false;
            return Equals((TimerEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = ((int) (Interval*397)) ^ (Action != null ? Action.GetHashCode() : 0);
                return result;
            }
        }

        #endregion
    }
}