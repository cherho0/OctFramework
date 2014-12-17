using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Oct.Framework.Core.Log
{
    [Serializable]
    public class BaseLog
    {
        public ObjectId _id { get; set; }

    }
}
