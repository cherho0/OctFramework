using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;

namespace Oct.Framework.MQDemo.Base
{
    public abstract class BaseCommand
    {

        public abstract NetMQMessage Exec(NetMQMessage inMsg);
    }
}
