using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MX.IM.WebApp.Models
{
    public class IMUser
    {
        public string ConnectionId { get; set; }
        public string Guid { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public Platform Platform { get; set; }
    }

    public enum Platform
    {
        OMP = 1,
        OSP = 2,
        OBP = 3
    }
}