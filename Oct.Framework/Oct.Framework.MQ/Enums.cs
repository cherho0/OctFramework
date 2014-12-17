using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.MQ
{
    public enum ServerType
    {
        Response,
        Pub,
        Router,
        Stream,
        XPub,
        Push
    }

    public enum ClientType
    {
        Request,
        Sub,
        Dealer,
        Stream,
        XSub,
        Pull
    }
}
