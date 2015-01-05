using System;
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
        public List<DynamicResult> Result { get; private set; }

        public List<SortField> SortFields { get; private set; }

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

        public void Search(string keyword, int start = 0, int count = 100)
        {
            sw.Restart();
            KeyWord = keyword;
            Result = new List<DynamicResult>();
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
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);

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
                var ret = new DynamicResult();
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

        public void SearchOrder(string keyword, int start = 0, int count = 100)
        {
            sw.Restart();
            KeyWord = keyword;
            Result = new List<DynamicResult>();
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
            var topdocs = searcher.Search(query, null, 1000, sort);
            Total = topdocs.totalHits;

            ScoreDoc[] docs = topdocs.scoreDocs;
            sw.Stop();
            Cost = sw.Elapsed;
            for (int i = start; i < start + count; i++)
            {
                //取得文档的编号(主键)是Lucene.Net提供的
                if (i>Total-1)
                {
                    break;
                }
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                var fileds = doc.GetFields();
                var ret = new DynamicResult();
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
