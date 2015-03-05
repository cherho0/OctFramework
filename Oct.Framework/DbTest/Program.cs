using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbTest.Entities;
using Oct.Framework.DB.Composite;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Extisions;
using Oct.Framework.DB.GenTable;

namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            /* EntityContext context = new EntityContext();
              var tss = context.Find<TestTs>(1);
            var ssssss=  ActiveRecordLinq.AsQueryable<TestTs>().First();
          
              var ss1 = context.Find    One<TestTs>(1);
              sw.Stop();
              Console.WriteLine("Nhibernate" + sw.Elapsed);*/
            /*EntitiesProxyHelper.Register(Assembly.GetExecutingAssembly());

            TestTs t = new TestTs();


            var m = dbContext.TestTsContext.GetModel(1);
            m.EntryUpdateStack();
            m.DD = "ss1";

            var id = EntitiesProxyHelper.GetDynamicMethod<TestTs>().GetValue(m, "Id");
             
            dbContext.TestTsContext.Update(m);

            var ts = new TestTs();
            ts.DD = "new";
            dbContext.TestTsContext.Add(ts);
            dbContext.SaveChanges();

            var models = dbContext.GetContext<UserAction>().Query(" MenuName='我'");

            /*sw.Restart();
            var list = dbContext.TestTsContext.Query("");
          
            sw.Stop();

            Console.WriteLine("Oct.DB" + sw.Elapsed);
            //   var count1 = cc.Count;

            //compoise

            /* dbContext.SQLContext.AsCompositeQuery()
                 .Fetch<TestTs, TestTs>((x, y) => x.Id == y.Id,(x,y)=>x.DD.Contains("的"));*/
            // Console.ReadLine();

            /* sw.Restart();
             var list1 = dbContext.TestTsContext.Query("");
             sw.Stop();*/

            DbContext dbContext = new DbContext("User id=Octopus_Framework;Password=JSJQH8819!(K;Server=192.168.2.20;database=Oct_Framework;");

            /*  var ds = dbContext.SQLContext.AsCompositeQuery()
                  .Fetch<TestTs>()
                  //第一个
                  .Fetch<DemoTable>()
                  .On<TestTs, DemoTable, int, int>(p => p.Id, p => p.Id)
                  .OnWhere<DemoTable>(p => p.Name == "chenzhiyin")
                  //第二个
                   .Fetch<DemoTable>()
                    .On<TestTs, DemoTable, int, int>(p => p.Id, p => p.Id)
                    //返回数据
                  .Query();*/


            // GenDb.Gen(Assembly.GetAssembly(typeof(DbContext)),dbContext.SQLContext);
            //var sql = GenTbl.Gen<TestTs>(dbContext.SQLContext);
            var m = dbContext.TestTsContext.GetModel(1);

            // Console.WriteLine("Oct.DB :" + sql);

            Console.ReadLine();

        }
    }
}
