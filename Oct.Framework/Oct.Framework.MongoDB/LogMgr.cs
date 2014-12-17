using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.Core.Log;

namespace Oct.Framework.MongoDB
{
    public class LogMgr : IDbLog
    {
        private static string _dbName;

        public static string DBName
        {
            get
            {
                if (string.IsNullOrEmpty(_dbName))
                {
                    _dbName = ConfigSettingHelper.GetAppStr(ConstArgs.MongoDBName);
                }
                return _dbName;
            }
        }

        public void Add<T>(T log) where T : BaseLog, new()
        {
            using (var c = new MongoDbRepository<T>(DBName))
            {
                c.Add(log);
            }

        }

        public void AddDataLog(DataChangeLog log)
        {
            using (var c = new MongoDbRepository<DataChangeLog>(DBName))
            {
                c.Add(log);
            }
        }

        public void AddViewLog(ViewLog log)
        {
            using (var c = new MongoDbRepository<ViewLog>(DBName))
            {
                c.Add(log);
            }
        }

        public void AddPerformanceLog(PerformanceLog log)
        {
            using (var c = new MongoDbRepository<PerformanceLog>(DBName))
            {
                c.Add(log);
            }
        }
    }
}
