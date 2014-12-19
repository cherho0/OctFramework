using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Oct.Framework.DB.Base;

namespace Oct.Framework.SearchEngine
{
    /// <summary>
    /// 搜索引擎任务列表，创建索引
    /// </summary>
    public class SearchEngineTasks
    {
        private ConcurrentDictionary<string, IWork> _works;
        private Thread _thread;

        public SearchEngineTasks()
        {
            _works = new ConcurrentDictionary<string, IWork>();
        }

        public void AddWord<T>(string key, string indexSavePath, DoWorkStyle style, Action<T, Document> addDocAction) where T : BaseEntity<T>, new()
        {
            IWork work = new CreateIndexTask<T>(indexSavePath, style, addDocAction);
            _works.TryAdd(key, work);
        }

        public void Start()
        {
            _thread = new Thread(DoSomeing);
            _thread.Start();
        }

        private void DoSomeing()
        {
            while (true)
            {
                try
                {

                }
                finally
                {
                    foreach (var hw in _works)
                    {
                        hw.Value.DoWork();
                    }
                }
                Thread.Sleep(1000 * 60);
            }
        }

        public void Do(string key)
        {
            Task.Factory.StartNew(() =>
            {
                if (_works.ContainsKey(key))
                {
                    _works[key].OneDoWork();
                }
            });

        }

        public void DoUnitUpdate(string key, string termkey, string termvalue)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                if (_works.ContainsKey(key))
                {
                    _works[key].UpdateUnitDoc(termkey, termvalue);
                }
            });
        }
    }
}
