using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Oct.Framework.Core.Common;

namespace Oct.Framework.MongoDB
{
    public class MongoDbRepository<T> : IDisposable where T : new()
    {
        private string _dbName = "OctFramework";
        private MongoServer _server;
        //private 
        private MongoDatabase _db;

        public MongoDbRepository(string dbName = "OctFramework")
        {
            _dbName = dbName;
            if (string.IsNullOrEmpty(_dbName))
            {
                _dbName = "OctFramework";
            }
        }

        private MongoClient Client
        {
            get
            {
                if (_client == null)
                {
                    var settings = new MongoClientSettings();
                    var servers = new List<MongoServerAddress>();
                    var adds = ConfigSettingHelper.GetAppStr(ConstArgs.MongoConnStr).Split(',');
                    foreach (var add in adds)
                    {
                        var strs = add.Split(':');
                        var ip = strs[0];
                        var port = strs[1];
                        servers.Add(new MongoServerAddress(ip, int.Parse(port)));

                    }
                    settings.Servers = servers;
                    _client = new MongoClient(settings);
                }
                return _client;
            }
        }

        private static MongoClient _client;

        private MongoCollection<T> GetCollection()
        {
            _server = Client.GetServer();
            _db = _server.GetDatabase(_dbName);
            return _db.GetCollection<T>(typeof(T).Name);
        }

        public MongoCursor<T> FindAll()
        {
            return GetCollection().FindAll();
        }

        public IQueryable<T> GetAll()
        {
            return GetCollection().FindAll().AsQueryable();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> fun)
        {
            return GetCollection().FindAll().AsQueryable().Where(fun);
        }

        public void Add(T item)
        {
            GetCollection().Save(item);

        }

        public void Update(T item)
        {
            GetCollection().Save(item);

        }

        public void Delete(ObjectId id)
        {
            var query = Query.EQ("_id", id);
            GetCollection().Remove(query);
        }

        public T GetSingle(Expression<Func<T, bool>> fun)
        {
            return GetCollection().AsQueryable().FirstOrDefault(fun);


        }

        public IQueryable<T> GetPageData<TKey>(Expression<Func<T, bool>> fun, Expression<Func<T, TKey>> @orderby, out int total, int pageIndex = 0, bool asc = true,
            int pageSize = 20)
        {
            var collection = GetCollection();
            var q = collection.AsQueryable().Where(fun);
            total = q.Count();
            if (asc)
            {
                q = q.OrderBy(@orderby);
            }
            else
            {
                q = q.OrderByDescending(@orderby);
            }

            return q.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        }

        public void Dispose()
        {
            if (_server != null)
            {
                _server.Disconnect();
                _db = null;
                _server = null;
                //_client = null;
            }

        }

        public void Update(IMongoQuery query, UpdateBuilder update)
        {
            var collection = GetCollection();
            collection.Update(query, update);
        }

        public void Delete(IMongoQuery query)
        {
            var collection = GetCollection();
            collection.Remove(query);
        }

        public int GetCount(Expression<Func<T, bool>> fun)
        {
            return GetCollection().AsQueryable().Count(fun);
        }

        public IList<TR> QueryResult<TR>(Expression<Func<T, TR>> fun)
        {
            return GetCollection().AsQueryable().Select(fun).ToList();
        }
    }
}
