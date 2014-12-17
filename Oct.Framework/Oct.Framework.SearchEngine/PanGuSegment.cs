using System.Collections.Generic;
using System.Linq;
using PanGu;

namespace Oct.Framework.SearchEngine
{
    public static class PanGuSegment
    {
        public static bool Inited = false;
        /// <summary>
        /// 分词扩展方法
        /// </summary>
        /// <param name="keyWord">搜索关键字</param>
        /// <returns></returns>
        public static List<string> Segment(this string keyWord)
        {
            if (!Inited)
            {
                PanGu.Segment.Init();
            }
            var segment = new Segment();
            var words = segment.DoSegment(keyWord);
            return words.Select(str => str.Word.ToLower()).ToList();
        }
    }
}
