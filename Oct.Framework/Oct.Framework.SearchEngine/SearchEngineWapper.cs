using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Reflection;
using PanGu;

namespace Oct.Framework.SearchEngine
{
    public enum DataType
    {
        dynamic,
        dictionary
    }


    public class SearchRet
    {
        public int Total { get; set; }
        public double Cost { get; set; }
        public List<SearchData> SearchDatas { get; set; }
    }

    public class Rows
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SearchData
    {
        public List<Rows> Values { get; private set; }

        public SearchData()
        {
            Values = new List<Rows>();
        }

        public void Add(string key, string val)
        {
            Values.Add(new Rows()
            {
                Name = key,
                Value = val
            });
        }

        public void Set(string key, string val)
        {
            var prop = Values.FirstOrDefault(p => p.Name == key.Trim());
            if (prop == null)
            {
                prop = new Rows();
                prop.Name = key;
                Values.Add(prop);
            }
            prop.Value = val;

        }

        public string this[string key]
        {
            get { return Values.First(p => p.Name == key.Trim()).Value; }
        }
    }

    /// <summary>
    /// 搜索引擎封装，用来查询结果集
    /// </summary>
    public class SearchEngineWapper
    {

        public const int AUTO = 2;
        public const int BYTE = 10;
        public const int CUSTOM = 9;
        public const int DOC = 1;
        public const int DOUBLE = 7;
        public const int FLOAT = 5;
        public const int INT = 4;
        public const int LONG = 6;
        public const int SHORT = 8;
        public const int STRING = 3;
        public const int STRING_VAL = 11;
        public static string Path = "";


        /// <summary>
        /// 最大搜索深度
        /// </summary>
        public const int MaxHits = 100000;
        public static void Init()
        {
            Path = ConfigSettingHelper.GetAppStr("indexpath");
            // FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NoLockFactory());
            //_reader = IndexReader.Open(directory, true);
        }

        public SearchEngineWapper()
        {

        }

        public static IndexReader _reader;

        /// <summary>
        /// 条件查询， 可参考 http://blog.csdn.net/weizengxun/article/details/8101097
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public static SearchRet SearchCondition(Func<Query> fun, int start = 0, int count = 100)
        {
            SearchRet searchRet = new SearchRet();
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            //IndexSearcher需要传递一个IndexReader对象
            var reader = LuceneFactory.GetIndexReader(Path);
            IndexSearcher searcher = LuceneFactory.getIndexSearcher(reader); // new IndexSearcher(SearchEngineWapper._reader);

            //PhraseQuery用来进行多个关键词的检索，调用Add方法添加关键词
            var query = fun();

            // query.Add(query8, BooleanClause.Occur.SHOULD);
            //PhraseQuery. SetSlop(int slop)用来设置关键词之间的最大距离，默认是0，设置了Slop以后哪怕文档中两个关键词之间没有紧挨着也能找到。
            TopScoreDocCollector collector = TopScoreDocCollector.create(MaxHits, true);
            searcher.Search(query, null, collector);
            searchRet.Total = collector.GetTotalHits();
            searchRet.SearchDatas = new List<SearchData>();
            ScoreDoc[] docs = collector.TopDocs(start, count).scoreDocs;
            sw.Stop();
            searchRet.Cost = sw.Elapsed.TotalMilliseconds;
            for (int i = 0; i < docs.Length; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new SearchData();
                foreach (Field filed in fileds)
                {
                    var name = filed.Name();
                    var val = filed.StringValue();

                    ret.Add(name, val);
                }
                searchRet.SearchDatas.Add(ret);
            }
            return searchRet;
        }


        /// <summary>
        /// 排序分词查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="reverse">倒序</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public static SearchRet SearchOrder(Func<Query> query, SortField[] orders, int start = 0, int count = 100)
        {
            SearchRet searchRet = new SearchRet();
            Stopwatch sw = new Stopwatch();
            sw.Restart();


            //IndexSearcher需要传递一个IndexReader对象

            var reader = LuceneFactory.GetIndexReader(Path);
            IndexSearcher searcher = LuceneFactory.getIndexSearcher(reader);

            //PhraseQuery用来进行多个关键词的检索，调用Add方法添加关键词
            var sort = new Sort();
            sort.SetSort(orders);

            var topdocs = searcher.Search(query(), null, MaxHits, sort);
            searchRet.Total = topdocs.totalHits;

            ScoreDoc[] docs = topdocs.scoreDocs;
            sw.Stop();
            searchRet.Cost = sw.Elapsed.TotalMilliseconds;
            searchRet.SearchDatas = new List<SearchData>();
            for (int i = start; i < start + count; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                if (i > searchRet.Total - 1)
                {
                    break;
                }
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new SearchData();
                foreach (Field filed in fileds)
                {
                    var name = filed.Name();
                    var val = filed.StringValue();

                    ret.Add(name, val);
                }
                searchRet.SearchDatas.Add(ret);
            }
            return searchRet;
        }

        private string Preview(string body, string keyword)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"Red\">", "</font>");
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 1000;
            string bodyPreview = highlighter.GetBestFragment(keyword, body);
            if (string.IsNullOrEmpty(bodyPreview))
            {
                return body;
            }
            return bodyPreview;
        }
    }
}
