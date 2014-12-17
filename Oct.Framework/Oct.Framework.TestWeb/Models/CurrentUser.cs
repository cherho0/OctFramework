using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.TestWeb.Models
{
    public class CurrentUser:UserBase
    {
        public string Address { get; set; }
    }
}