using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.Core.Session
{
    [Serializable]
    internal class CacheSession
    {
        public string SessionId { get; set; }

        public string Key { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime Expires { get; set; }

        public string ApplicationName { get; set; }

        public DateTime LockDate { get; set; }

        public int Timeout { get; set; }

        public bool Locked { get; set; }

        public object SessionItem { get; set; }
    }
}
