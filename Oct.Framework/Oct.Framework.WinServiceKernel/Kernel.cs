using System;
using System.Collections.Generic;
using System.Configuration;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Log;
using Oct.Framework.WinServiceKernel.Common;
using Oct.Framework.WinServiceKernel.Core;
using Oct.Framework.WinServiceKernel.HeartBeat;
using Oct.Framework.WinServiceKernel.Util;

namespace Oct.Framework.WinServiceKernel
{
    /*
     核心业务
     */
    public class Kernel
    {
        private ListHeartBeat _trunkBeat;
        private LogicMgr _lgcs;
        public string Res;
        //初始化内核工具
        internal Kernel()
        {
            _trunkBeat = new ListHeartBeat(Cfg.Instance.Delay,this);
            Res = "../Res";
        }

        internal void Ctor(LogicMgr lgcs)
        {
            _lgcs = lgcs;
        }

        internal void Close()
        {
            _trunkBeat.Close();
        }

        public void Cmd(string cmd)
        {
            var msg = new CmdMessage(cmd);
            _trunkBeat.AddMsg(msg);
            
        }



        public int GetTimerCount()
        {
            return _trunkBeat.Count;
        }

        public int AddTimer(Action action, int delay, string lgcName = "")
        {
            return _trunkBeat.AddWork(action, delay, lgcName);
        }

        public int LgcTaskCount(string lgcName)
        {
            return _trunkBeat.LgcTaskCount(lgcName);
        }

        public long LgcTaskCostt(string lgcName)
        {
            return _trunkBeat.LgcTaskCost(lgcName);
        }

        public long LgcTaskCurCost(string lgcName)
        {
            return _trunkBeat.CurCost(lgcName);
        }

        public string CurrentTaskLgc
        {
            get { return _trunkBeat.CurrentRunLogic; }
        }

        public void Start()
        {
            _trunkBeat.Start();
            Csl.Wl("程序启动成功，心跳开始执行");
            LogsHelper.Info("程序启动成功，心跳开始执行");
        }

        public T GetLgc<T>(string name) where T : CoreLogic
        {
            if (_lgcs.Contains(name))
            {
                return (T)_lgcs.GetByName(name);
            }
            return null;
        }

        public IEnumerable<CoreLogic> GetLgcs()
        {
            return _lgcs.GetAllLgcs();
        }

    }
}
