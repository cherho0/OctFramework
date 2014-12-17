using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.MvcExt.User
{
    /// <summary>
    /// 用户信息基类
    /// </summary>
    [Serializable]
    public class  UserBase
    {
        public string Account { get; set; }

        public string UserName { get; set; }

        public int Sex { get; set; } 
       
        public string Avatar { get; set; }

        public string IpAddress { get; set; }


        public UserBase()
        {
        }

    }
}
