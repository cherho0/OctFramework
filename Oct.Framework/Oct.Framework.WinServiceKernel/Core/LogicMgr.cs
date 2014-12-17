using System.Collections.Generic;

namespace Oct.Framework.WinServiceKernel.Core
{
    public class LogicMgr
    {
        private readonly Dictionary<string, CoreLogic> _lgcs = new Dictionary<string, CoreLogic>();


        public int Count
        {
            get { return _lgcs.Count; }
        }

        public void Add(CoreLogic lgc)
        {
            _lgcs.Add(lgc.Name, lgc);
        }

        public bool Contains(string name)
        {
            return _lgcs.ContainsKey(name);
        }

        public CoreLogic GetByName(string name)
        {
            CoreLogic lgc;
            _lgcs.TryGetValue(name, out lgc);
            return lgc;
        }

        public bool Remove(string name)
        {
            if (Contains(name))
            {
                _lgcs.Remove(name);
                return true;
            }

            return false;
        }

        public IEnumerable<CoreLogic> GetAllLgcs()
        {
            return _lgcs.Values;
        }

    }
}
