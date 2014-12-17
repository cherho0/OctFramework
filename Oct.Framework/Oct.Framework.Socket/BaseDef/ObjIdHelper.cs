using System;

namespace Oct.Framework.Socket.BaseDef
{
    internal static class ObjIdHelper
    {
        public static ObjId FromBag(string s)
        {
            string[] s2 = s.Split(new[] { ',' });
            return new ObjId(Convert.ToInt32(s2[0]), Convert.ToInt32(s2[1]), Convert.ToInt32(s2[2]));
        }

        public static string ToBag(this ObjId obj)
        {
            return string.Join(",", new[] { obj.Cls.ToString(), obj.Owner.ToString(), obj.Id.ToString() });
        }

        public static bool CheckInequals(this ObjId obj, ObjId obj2)
        {
            if (obj.Equals(obj2))
            {
                return false;
            }
            return true;
        }

        public static bool CheckValid(this ObjId obj)
        {
            if (obj.Equals(ObjId.Empty))
            {
                //string s = string.Format("OBJ id:\"{0}\" EMPTY\r\n{1}\r\n{2}", obj, new StackTrace(1, true).ToText(4), StrHelper.Splitter);
                //Csl.Wl(ConsoleColor.Red, s);
                //Log.Error(s);
                return false;
            }
            return true;
        }

        public static ObjId Create(int cls, int srvId, long id)
        {
            return new ObjId(cls, srvId, (int)id);
        }
    }
}
