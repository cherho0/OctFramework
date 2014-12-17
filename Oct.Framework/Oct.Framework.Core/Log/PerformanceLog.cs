using System;
using MongoDB.Bson;

namespace Oct.Framework.Core.Log
{
    [Serializable]
    public class PerformanceLog : BaseLog
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public long MillionSeconds { get; set; }

        public string IP { get; set; }
    }
}
