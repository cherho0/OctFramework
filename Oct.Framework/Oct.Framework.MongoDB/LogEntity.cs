using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Oct.Framework.MongoDB
{
    public class LogEntity
    {
        public ObjectId Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }
}
