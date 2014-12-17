using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Oct.Framework.WinServiceKernel.Common;

namespace Oct.Framework.WinServiceKernel.HeartBeat
{
    public class ListHeartBeat
    {
        private Kernel _knl;
        private int _delay = 0;
        private List<HeartWork> _works;
        private ConcurrentQueue<CmdMessage> _msgQueue;
        private Thread _thread;
        private object _o;

        private string _CurrentRunLogic;

        public string CurrentRunLogic
        {
            get { return _CurrentRunLogic; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="delay">延迟多久后持续执行</param>
        public ListHeartBeat(int delay, Kernel knl)
        {
            _knl = knl;
            _o = new object();
            _delay = delay;
            _works = new List<HeartWork>();
            _msgQueue = new ConcurrentQueue<CmdMessage>();
            _thread = new Thread(DoSomeing);
        }

        public int Count
        {
            get { return _works.Count; }
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Close()
        {
            _thread.Abort();
        }

        public int AddWork(Action action, int delay, string lgcName)
        {
            lock (_o)
            {
                HeartWork hw = new HeartWork(action, delay, lgcName);
                _works.Add(hw);
                return hw.Id;
            }
        }

        public void DelWork(int hwid)
        {
            lock (_o)
            {
                var hw = _works.FirstOrDefault(p => p.Id == hwid);
                if (hw != null)
                {
                    _works.Remove(hw);
                }
            }
        }

        private void DoSomeing()
        {
            while (true)
            {
                lock (_o)
                {
                    try
                    {

                    }
                    finally
                    {
                        foreach (var hw in _works)
                        {
                            _CurrentRunLogic = hw.LgcName;
                            hw.Do();
                            _CurrentRunLogic = "";
                        }

                        if (_msgQueue.Count > 0)
                        {
                            CmdMessage msg;
                            _msgQueue.TryDequeue(out msg);
                            if (msg != null)
                            {
                                foreach (var lgc in _knl.GetLgcs())
                                {
                                    lgc.Cmd(msg.Msg);
                                }
                            }
                        }
                    }
                }

                Thread.Sleep(_delay);
            }
        }

        public int LgcTaskCount(string lgcName)
        {
            return _works.Where(p => p.LgcName == lgcName).Count();
        }

        public long LgcTaskCost(string lgcName)
        {
            return _works.Where(p => p.LgcName == lgcName).Sum(p => p.LastCostTime);
        }

        public long CurCost(string lgcName)
        {
            return _works.Where(p => p.LgcName == lgcName).Sum(p => p.CurCost);
        }

        public void AddMsg(CmdMessage msg)
        {
            _msgQueue.Enqueue(msg);
        }
    }
}
