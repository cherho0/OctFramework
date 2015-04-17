using System;
using System.Diagnostics;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Log;
using Oct.Framework.WinServiceKernel.Util;

namespace Oct.Framework.WinServiceKernel.HeartBeat
{
    public class HeartWork
    {
        private int _id;
        private Action _action;
        private int _baseTime;
        private DateTime dealtime;
        private string _logicName;
        private long _lastCostTime;
        private Stopwatch _sw;
        private bool _firstDo;

        //最后一次消耗时间
        public long LastCostTime
        {
            get { return _lastCostTime; }
        }

        //最后一次消耗时间
        public long CurCost
        {
            get { return _sw.ElapsedMilliseconds; }
        }

        public HeartWork(Action action, int dotime, string logicName = "", bool firstDo = false)
        {
            _firstDo = firstDo;
            _id = Seed.Instance.NewId();
            _action = action;
            _baseTime = dotime;
            dealtime = DateTime.MinValue;
            _logicName = logicName;
            _sw = new Stopwatch();
        }

        public int Id { get { return _id; } }
        public string LgcName { get { return _logicName; } }

        public void Do()
        {
            try
            {
                if (dealtime == DateTime.MinValue)
                {
                    dealtime = DateTime.Now;
                    if (_firstDo)
                    {
                        _sw.Restart();
                        _action();
                        _sw.Stop();
                        _lastCostTime = _sw.ElapsedMilliseconds;
                        _sw.Reset();
                    }
                    return;
                }
                if ((DateTime.Now - dealtime).TotalMilliseconds >= _baseTime)
                {
                    dealtime = DateTime.Now;
                    _sw.Restart();
                    _action();
                    _sw.Stop();
                    _lastCostTime = _sw.ElapsedMilliseconds;
                    _sw.Reset();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                Csl.WlEx(ex);
            }
        }
    }
}
