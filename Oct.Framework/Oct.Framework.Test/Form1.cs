using System.Threading.Tasks;
using Oct.Framework.Core.Log;
using Oct.Framework.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using Oct.Framework.Entities.Entities;

namespace Oct.Framework.Test
{
    //1、查询条件，参数化
    //2、不同数据库适配
    //3、构造函数，可以适应多个数据库日
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LogHelper.Info("ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ICacheHelper cache = Bootstrapper.GetRepository<ICacheHelper>();
            //cache.Set("11", "11");
            //var sss = cache.Get<string>("11");

            /* string sss = "";
            Stopwatch sw = new Stopwatch();
            var umsg1 = new UcUserMsg()
            {
                Id = Guid.NewGuid(),
                UserId = "admin",
                Title = "我我哦我我我我1212",
                Message = "我我哦我我我我1212",
                CreateDate = DateTime.Now,
                IsRead = false,
                FromUser = "admin"
            };
            sw.Start();
            using (DbContext context = new DbContext())
            {
               
                
                context.UserMsgContext.Add(umsg1);

               
               
                context.SaveChanges();
               
            }
            sw.Stop();

            sss += "一个连接耗时 ：" + sw.ElapsedMilliseconds;
            /* sw.Restart();

            for (int i = 0; i < 10000; i++)
            {
                var umsg = new UcUserMsg()
                {
                    Id = Guid.NewGuid(),
                    UserId = "admin",
                    Title = "我我哦我我我我1212",
                    Message = "我我哦我我我我1212",
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    FromUser = "admin"
                };

                using (DbContext context = new DbContext())
                {
                    context.UserMsgContext.Add(umsg);
                    context.SaveChanges();
                }
                
            }
            sw.Stop();
            sss += "每次一个连接耗时 ：" + sw.ElapsedMilliseconds;
            sw.Restart();
            using (DbContext context = new DbContext())
            {
                for (int i = 0; i < 10000; i++)
                {
                    var umsg = new UcUserMsg()
                    {
                        Id = Guid.NewGuid(),
                        UserId = "admin",
                        Title = "我我哦我我我我1212",
                        Message = "我我哦我我我我1212",
                        CreateDate = DateTime.Now,
                        IsRead = false,
                        FromUser = "admin"
                    };

                    context.UserMsgContext.Add(umsg);
                }
                context.SaveChanges();
            }
            sw.Stop();
            sss += "10000个插入同时执行 ：" + sw.ElapsedMilliseconds;

            */
            //实体操作


            /*using (DbContext context = new DbContext())
            {
                // context.UserMsgContext.Add(umsg);
                //context.UserMsgContext.Update(umsg);
                var model = new UcUserMsg();
                model.Id = Guid.Parse("C155FF8A-90CE-4E26-89CF-D79048CB37DB");
                model.Message = "修改qweqweqweqweq";
                context.UserMsgContext.Update(model);
                // context.UserMsgContext.Delete("26E57601-DAFA-4206-A72E-CD1B3E7EB364");
                // context.UserMsgContext.Query("1=1");
                int total;
                //context.UserMsgContext.QueryPage("1=1", "id", 1, 20, out total);

                //所有操作执行完成，调用一个保存，全部工作单元保存
                // context.SaveChanges();
                //var models = context.UserMsgContext.QueryPage("", "id", 1, 5,out total );
                //dataGridView1.DataSource = models;

                //context.UserMsgContext.Add(umsg);
                //context.UserMsgContext.Add(umsg);

                context.SaveChanges();
            }*/
            // Text = sss;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var context = new DbContext())
            {
                var strs = new string[] { "cw", "syf", "czy", "xzj" };
                int total = 0;
               
            }

            //    //语句操作
            //    using (DbContext context = new DbContext())
            //    {
            //        var ds = context.SQLContext.ExecuteQuery("select * from uc_user where account=@account", new SqlParameter[] { new SqlParameter("@account", "chenzhiyin") });
            //        // var ds1 = context.SQLContext.ExecuteQuery("select * from uc_user");
            //        dataGridView1.DataSource = ds.Tables[0];
            //        //int total;
            //        // var models = context.UserMsgContext.QueryPage("title like @title and isread = @isread", new Dictionary<string, object>()
            //        // {
            //        //     {"@title","%我%"},{"@isread",0}
            //        // }, "id", 1, 5, out total);
            //        //  dataGridView1.DataSource = models;

            //        dynamic model = context.SQLContext.ExecuteExpandoObject("  select * from UC_UserMsg where id='61FD1A84-1083-4286-A6FA-3BD2EFD09053'");

            //        //dataGridView1.DataSource = model;
            //    }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Parallel.For(0,500, x =>
           // {
                try
                {
                    using (DbContext context = new DbContext())
                    {
                        context.TestTsContext.Add(new TestTs() { DD = "第几个" });
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    
                }
                
           // });
        }
    }
}
