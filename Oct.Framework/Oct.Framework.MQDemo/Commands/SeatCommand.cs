using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;

namespace Oct.Framework.MQDemo.Commands
{
    public class SeatCommand
    {
        public SeatCommand()
        {
        }

        public NetMQMessage Exec(NetMQMessage inmsg)
        {
           var msg = Program.queue.InQueue(inmsg);
            return msg;
        }
    }
}
