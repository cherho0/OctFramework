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
using Oct.Framework.Core.Reflection;
using PanGu;

namespace Oct.Framework.SearchEngine
{
    public enum DataType
    {
        dynamic,
        dictionary
    }

    public class Rows
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SearchRet
    {
        public List<Rows> Values { get; private set; }

        public SearchRet()
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

        public string this[string key]
        {
            get { return Values.First(p => p.Name == key).Value; }
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

        /// <summary>
        /// 请使用List<dynamic> 获取
        /// </summary>
        public List<SearchRet> Result { get; private set; }

        public List<SortField> SortFields { get; private set; }

        public DataType DataType { get; set; }

        /// <summary>
        /// 最大搜索深度
        /// </summary>
        public int MaxHits = 100000;

        /// <summary>
        /// 集合总量
        /// </summary>
        public int Total { get; private set; }

        public TimeSpan Cost { get; private set; }

        public string PkName { get; set; }

        public string IndexPath { get; private set; }

        public string KeyWord { get; private set; }

        public List<string> Keys { get; private set; }

        public List<string> PreviewKeys { get; private set; }

        private Stopwatch sw;

        public SearchEngineWapper(string indexPath)
        {
            DataType = DataType.dynamic;
            IndexPath = indexPath;
            Keys = new List<string>();
            PreviewKeys = new List<string>();
            sw = new Stopwatch();
        }

        public void ClearKey()
        {
            Keys.Clear();
        }

        public void AddKey(string key)
        {
            Keys.Add(key);
        }

        public void AddPreviewKey(string key)
        {
            PreviewKeys.Add(key);
        }

        public void AddSort(string key, int fieldType, bool reverse)
        {
            if (SortFields == null)
            {
                SortFields = new List<SortField>();
            }
            SortFields.Add(new SortField(key, fieldType, reverse));
        }

        /// <summary>
        /// 条件查询， 可参考 http://blog.csdn.net/weizengxun/article/details/8101097
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void SearchCondition(Func<Query> fun, int start = 0, int count = 100)
        {

            sw.Restart();
            Result = new List<SearchRet>();
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NoLockFactory());

            IndexReader reader = IndexReader.Open(directory, true);

            //IndexSearcher需要传递一个IndexReader对象

            IndexSearcher searcher = new IndexSearcher(reader);

            //PhraseQuery用来进行多个关键词的检索，调用Add方法添加关键词
            var query = fun();
            // query.Add(query8, BooleanClause.Occur.SHOULD);
            //PhraseQuery. SetSlop(int slop)用来设置关键词之间的最大距离，默认是0，设置了Slop以后哪怕文档中两个关键词之间没有紧挨着也能找到。
            TopScoreDocCollector collector = TopScoreDocCollector.create(MaxHits, true);
            searcher.Search(query, null, collector);
            Total = collector.GetTotalHits();

            ScoreDoc[] docs = collector.TopDocs(start, count).scoreDocs;
            sw.Stop();
            Cost = sw.Elapsed;
            for (int i = 0; i < docs.Length; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new SearchRet();
                foreach (Field filed in fileds)
                {
                    var name = filed.Name();
                    var val = filed.StringValue();
                    if (PreviewKeys.Contains(name))
                    {
                        val = Preview(val, KeyWord);
                    }
                    ret.Add(name, val);
                }
                Result.Add(ret);
            }

        }

        /// <summary>
        /// 全词查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void SearchAll(string keyword, int start = 0, int count = 100)
        {
            sw.Restart();
            KeyWord = keyword;
            Result = new List<SearchRet>();
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NoLockFactory());

            IndexReader reader = IndexReader.Open(directory, true);

            //IndexSearcher需要传递一个IndexReader对象

            IndexSearcher searcher = new IndexSearcher(reader);

            //PhraseQuery用来进行多个关键词的检索，调用Add方法添加关键词
            BooleanQuery query = new BooleanQuery();

            foreach (var key in Keys)
            {
                PhraseQuery query1 = new PhraseQuery();
                query1.Add(new Term(key, keyword));
                query1.SetSlop(100);
                query.Add(query1, BooleanClause.Occur.SHOULD);
            }

            // query.Add(query8, BooleanClause.Occur.SHOULD);
            //PhraseQuery. SetSlop(int slop)用来设置关键词之间的最大距离，默认是0，设置了Slop以后哪怕文档中两个关键词之间没有紧挨着也能找到。
            TopScoreDocCollector collector = TopScoreDocCollector.create(MaxHits, true);
            searcher.Search(query, null, collector);
            Total = collector.GetTotalHits();

            ScoreDoc[] docs = collector.TopDocs(start, count).scoreDocs;
            sw.Stop();
            Cost = sw.Elapsed;
            for (int i = 0; i < docs.Length; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new SearchRet();
                foreach (Field filed in fileds)
                {
                    var name = filed.Name();
                    var val = filed.StringValue();
                    if (PreviewKeys.Contains(name))
                    {
                        val = Preview(val, KeyWord);
                    }
                    ret.Add(name, val);
                }
                Result.Add(ret);
            }

        }

        /// <summary>
        /// 分词查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void Search(string keyword, int start = 0, int count = 100)
        {
            sw.Restart();
            KeyWord = keyword;
            Result = new List<SearchRet>();
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NoLockFactory());

            IndexReader reader = IndexReader.Open(directory, true);

            //IndexSearcher需要传递一个IndexReader对象

            IndexSearcher searcher = new IndexSearcher(reader);

            //PhraseQuery用来进行多个关键词的检索，调用Add方法添加关键词
            BooleanQuery query = new BooleanQuery();

            var words = keyword.Segment();

            foreach (string word in words)
            {
                foreach (var key in Keys)
                {
                    PhraseQuery query1 = new PhraseQuery();
                    query1.Add(new Term(key, word));
                    query1.SetSlop(100);
                    query.Add(query1, BooleanClause.Occur.SHOULD);
                }
            }

            // query.Add(query8, BooleanClause.Occur.SHOULD);
            //PhraseQuery. SetSlop(int slop)用来设置关键词之间的最大距离，默认是0，设置了Slop以后哪怕文档中两个关键词之间没有紧挨着也能找到。
            TopScoreDocCollector collector = TopScoreDocCollector.create(MaxHits, true);
            searcher.Search(query, null, collector);
            Total = collector.GetTotalHits();

            ScoreDoc[] docs = collector.TopDocs(start, count).scoreDocs;
            sw.Stop();
            Cost = sw.Elapsed;
            for (int i = 0; i < docs.Length; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new SearchRet();
                foreach (Field filed in fileds)
                {

                    var name = filed.Name();
                    var val = filed.StringValue();
                    if (PreviewKeys.Contains(name))
                    {
                        val = Preview(val, KeyWord);
                    }
                    ret.Add(name, val);
                }
                Result.Add(ret);
            }

        }

        /// <summary>
        /// 排序分词查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void SearchOrder(string keyword, int start = 0, int count = 100)
        {
            sw.Restart();
            KeyWord = keyword;
            Result = new List<SearchRet>();
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NoLockFactory());

            IndexReader reader = IndexReader.Open(directory, true);

            //IndexSearcher需要传递一个IndexReader对象

            IndexSearcher searcher = new IndexSearcher(reader);

            //PhraseQuery用来进行多个关键词的检索，调用Add方法添加关键词
            BooleanQuery query = new BooleanQuery();

            var words = keyword.Segment();

            foreach (string word in words)
            {
                foreach (var key in Keys)
                {
                    PhraseQuery query1 = new PhraseQuery();
                    query1.Add(new Term(key, word));
                    query1.SetSlop(100);
                    query.Add(query1, BooleanClause.Occur.SHOULD);
                }
            }

            // query.Add(query8, BooleanClause.Occur.SHOULD);
            //PhraseQuery. SetSlop(int slop)用来设置关键词之间的最大距离，默认是0，设置了Slop以后哪怕文档中两个关键词之间没有紧挨着也能找到。
            // TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            var sort = new Sort();
            if (SortFields != null)
            {
                sort.SetSort(SortFields.ToArray());
            }
            var topdocs = searcher.Search(query, null, MaxHits, sort);
            Total = topdocs.totalHits;

            ScoreDoc[] docs = topdocs.scoreDocs;
            sw.Stop();
            Cost = sw.Elapsed;
            for (int i = start; i < start + count; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                if (i > Total - 1)
                {
                    break;
                }
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new SearchRet();
                foreach (Field filed in fileds)
                {
                    var name = filed.Name();
                    var val = filed.StringValue();
                    if (PreviewKeys.Contains(name))
                    {
                        val = Preview(val, KeyWord);
                    }
                    ret.Add(name, val);
                }
                Result.Add(ret);
            }

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
