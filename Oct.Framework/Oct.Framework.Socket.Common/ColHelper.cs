using System.Collections.Generic;
using System.Linq;

namespace Oct.Framework.Socket.Common
{
    public static class ColHelper
    {
        public static HashSet<T> Ban<T>(this HashSet<T> objs, IEnumerable<T> x)
        {
            var y = new HashSet<T>(objs);
            y.ExceptWith(x);
            return y;
        }

        public static HashSet<T> BanWith<T>(this HashSet<T> objs, IEnumerable<T> x)
        {
            objs.ExceptWith(x);
            return objs;
        }

        public static bool IsEqual(IEnumerable<byte[]> x, IEnumerable<byte[]> y)
        {
            if ((x != null) && (y != null))
            {
                int xlen = x.Count();
                int ylen = y.Count();
                if (xlen != ylen)
                {
                    return false;
                }
                for (int i = 0; i < xlen; i++)
                {
                    byte[] l = x.ElementAt(i);
                    byte[] r = y.ElementAt(i);
                }
            }
            return true;
        }

        public static bool IsEqual<T>(IEnumerable<T> x, IEnumerable<T> y)
        {
            if ((x != null) && (y != null))
            {
                int xlen = x.Count();
                int ylen = y.Count();
                if (xlen != ylen)
                {
                    return false;
                }
                for (int i = 0; i < xlen; i++)
                {
                    T l = x.ElementAt(i);
                    T r = y.ElementAt(i);
                    if (!l.Equals(r))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static HashSet<T> ToSet<T>(this IEnumerable<T> objs)
        {
            var x = new HashSet<T>();
            foreach (T obj in objs)
            {
                x.Add(obj);
            }
            return x;
        }
    }
}