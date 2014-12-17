using System;

namespace Oct.Framework.Socket.Timer
{
    /// <summary>
    /// ֧��һ�λ��δ����ļ�ʱ������
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
        /// ��ʱ��������ʱ��
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
        /// ��ʱ���Ƿ�������
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
        /// ��ʱ������
        /// </summary>
        /// <param name="delay">�ӳٴ���ʱ��</param>
        /// <param name="interval">�������</param>
        /// <param name="callback">�ص�����</param>
        public TimerEntry(float delay, float interval, Action<float> callback)
        {
            _totalTime = -1.0f;
            Action = callback;
            RemainingInitialDelay = delay;
            Interval = interval;
            _id = TimerHelper.Id;
        }

        /// <summary>
        /// ��ʱ������
        /// </summary>
        /// <param name="delay">�ӳٴ���ʱ��</param>
        /// <param name="interval">�������</param>
        /// <param name="callback">�ص�����</param>
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
        /// ��ʱ������
        /// </summary>
        /// <param name="delay">�ӳٴ���ʱ��</param>
        /// <param name="interval">�������</param>
        /// <param name="callback">�ص�����</param>
        public TimerEntry(int delay, int interval, Action<float> callback)
            : this(delay / 1000.0f, interval / 1000.0f, callback)
        {
        }

        /// <summary>
        /// ��ʱ������
        /// </summary>
        /// <param name="delay">�ӳٴ���ʱ��</param>
        /// <param name="interval">�������</param>
        /// <param name="callback">�ص�����</param>
        public TimerEntry(int delay, int interval, Action<float> callback, int count)
            : this(delay / 1000.0f, interval / 1000.0f, callback, count)
        {
        }

        /// <summary>
        /// ��ʱ������
        /// </summary>
        /// <param name="delay">�ӳٴ���ʱ��</param>
        /// <param name="interval">�������</param>
        /// <param name="callback">�ص�����</param>
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
        /// ֹͣ���������ʱ��
        /// </summary>
        public void Dispose()
        {
            Stop();
            Action = null;
        }

        #endregion

        #region IUpdatable Members

        /// <summary>
        /// ��ʱ������
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
        /// ������ʱ��
        /// </summary>
        public void Start()
        {
            _totalTime = 0.0f;
        }

        /// <summary>
        /// ָ���ӳ�ʱ��
        /// </summary>
        public void Start(float initialDelay)
        {
            RemainingInitialDelay = initialDelay;
            _totalTime = 0.0f;
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        /// <param name="initialDelay">�ӳ�����ʱ��</param>
        /// <param name="interval">ʱ����</param>
        public void Start(float initialDelay, float interval)
        {
            RemainingInitialDelay = initialDelay;
            Interval = interval;
            _totalTime = 0.0f;
        }

        /// <summary>
        /// �ӳ�����
        /// </summary>
        public void Start(int initialDelay)
        {
            Start(initialDelay / 1000.0f);
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        /// <param name="initialDelay">�ӳ�����ʱ��</param>
        /// <param name="interval">ʱ����</param>
        public void Start(int initialDelay, int interval)
        {
            Start(initialDelay / 1000.0f, interval / 1000.0f);
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        /// <param name="initialDelay">�ӳ�����ʱ��</param>
        /// <param name="interval">ʱ����</param>
        /// <param name="callback">�ص����� </param>
        public void Start(uint initialDelay, uint interval, Action<float> callback)
        {
            Action = callback;

            Start(initialDelay / 1000.0f, interval / 1000.0f);
        }

        /// <summary>
        /// ֹͣ��ʱ��
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