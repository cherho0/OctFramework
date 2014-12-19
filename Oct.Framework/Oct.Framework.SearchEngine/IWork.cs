using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.SearchEngine
{
    public interface IWork
    {
        void DoWork();

        DoWorkStyle Style { get; }
        void OneDoWork();

        void UpdateUnitDoc(string key, object id);
    }
}
