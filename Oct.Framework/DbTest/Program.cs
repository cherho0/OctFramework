﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbTest.Entities;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.OrderNoGenter;
using Oct.Framework.DB.Composite;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Extisions;
using Oct.Framework.DB.GenTable;
using Oct.Framework.DB.Linq;

namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parallel.For(0, 10, x => Csl.Wl(NOGenter.Instance.GenOrderNo("BZY")));


            TestSku();

            Stopwatch sw = new Stopwatch();
            sw.Start();

           /* DbTest.Oct_FrameworkEntities e = new Oct_FrameworkEntities();

            var list = e.TestTs.ToList();

            sw.Stop();
            e.Dispose();*/
            /* EntityContext context = new EntityContext();
              var tss = context.Find<TestTs>(1);
            var ssssss=  ActiveRecordLinq.AsQueryable<TestTs>().First();
          
              var ss1 = context.Find    One<TestTs>(1);
              sw.Stop();
              Console.WriteLine("Nhibernate" + sw.Elapsed);*/
            EntitiesProxyHelper.Register(Assembly.GetExecutingAssembly());
            /*

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
            var dd = new TestTs()
            {
                DD = "ss"
            };
            dbContext.TestTsContext.Add(dd);
            var ddd = dd;
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
            // var m = dbContext.TestTsContext.GetModel(1);
            /*sw.Restart();
            var count111 = dbContext.TestTsContext.Query();
            sw.Stop();
            var lis21 = dbContext.GetContext<UserAction>().AsLinqQueryable().ToList();
            var count = dbContext.TestTsContext.AsLinqQueryable().Count();
            // var a = dbContext.GetContext<TestTs>().AsLinqQueryable().FirstOrDefault();
            sw.Restart();
            var lis = dbContext.GetContext<TestTs>().AsLinqQueryable().Select(p => new TestTs() { DD = p.DD }).ToList();
            sw.Stop();
            var ss = sw.Elapsed;
            var a = dbContext.GetContext<TestTs>().AsLinqQueryable().FirstOrDefault();
            sw.Restart();
            var lis1 = dbContext.GetContext<TestTs>().AsLinqQueryable().Select(p => new TestTs() { DD = p.DD }).ToList();
            var stes = dbContext.GetContext<TestTs>().AsLinqQueryable().Select(p => new { p.DD, p.Id }).ToList();
            sw.Stop();
            var qq = sw.Elapsed;
            var order = dbContext.TestTsContext.AsLinqQueryable().Where(p => p.DD.Contains("ss")).ToList();
            var part = dbContext.TestTsContext.AsLinqQueryable().Where(p => p.DD.Contains("ss")).ToList();

            var ret = (from dd in dbContext.GetContext<TestTs>().AsLinqQueryable() where dd.Id == 12 select new TestTs() { Id = dd.Id }).ToList();

            var rets = dbContext.TestTsContext.AsLinqQueryable().OrderBy(p => p.Id).Skip(10).Take(10).ToList();

            new TestTs() { DD = "ss" }.Insert();
            new TestTs() { DD = "ss", Id = 1 }.Update();
            new TestTs() { Id = 1 }.Delete();
            dbContext.SaveChanges();
            // Console.WriteLine("Oct.DB :" + sql);
            */
            Console.ReadLine();

        }


        private static void TestSku()
        {
            var sku1 = new string[] { "1", "2", "3", "4", "5" };
            var sku2 = new string[] { "a", "b", "c", "d" };
            var sku3 = new string[] { "8", "16", "32", "64" };

            var skuList = new List<string[]>
            {
                sku1,sku2,sku3
            };

            var ret = TestGet(skuList.ToArray(), 0);

        }

        private static List<string> TestGet(string[][] arr, int from)
        {
            if (from == arr.Length - 1)
            {
                List<string> strs = new List<string>();
                for (int k = 0; k < arr[from].Length; k++)
                {
                    strs.Add(arr[from][k]);
                }
                return strs;
            }
            List<string> sList = new List<string>();
            List<string> strs1 = TestGet(arr, from + 1);
            for (int i = 0; i < strs1.Count; i++)
            {
                var temparr = arr[from];
                for (int j = 0; j < temparr.Length; j++)
                {
                    sList.Add(strs1[i] + "," + temparr[j]);
                }
            }
            return sList;
        }
    }
}
