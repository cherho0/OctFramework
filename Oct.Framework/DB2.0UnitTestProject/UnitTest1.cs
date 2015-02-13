using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oct.Framework.DB2Dot0;
using Oct.Framework.Entities;

namespace DB2._0UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            EntityContext context = new EntityContext();
            var tss = context.Find<TestTs>(1);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            tss = context.Find<TestTs>(1);
            sw.Stop();

            sw.Restart();
            DbContext dbContext = new DbContext("User id=Octopus_Framework;Password=JSJQH8819!(K;Server=192.168.2.20;database=Oct_Framework;");
            var cc = dbContext.TestTsContext.Query("");
            sw.Stop();

            var count1 = cc.Count;
            var count = tss.Count;
        }
    }
}
