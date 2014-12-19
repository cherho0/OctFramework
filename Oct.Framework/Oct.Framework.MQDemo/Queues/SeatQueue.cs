using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using Oct.Framework.Core.Common;

namespace Oct.Framework.MQDemo.Queues
{
    public class SeatQueue
    {
        private int _count;

        private Dictionary<int, string> _queue;

        public NetMQMessage InQueue(NetMQMessage msg)
        {
            var outMsg = new NetMQMessage();

            if (msg.FrameCount != 2)
            {
                outMsg.Append(0);
                outMsg.Append("占座失败", Encoding.UTF8);
                return outMsg;
            }
            var seat = msg.Pop().ConvertToInt32();
            var name = msg.Pop().ConvertToString(Encoding.UTF8);
            if (!_queue.ContainsKey(seat) || !_queue[seat].IsNullOrEmpty())
            {
                outMsg.Append(0);
                outMsg.Append("占座失败", Encoding.UTF8);
                return outMsg;
            }
            _queue[seat] = name;

            outMsg.Append(1);
            outMsg.Append("占座成功", Encoding.UTF8);
            ShowQueue();
            return outMsg;
        }

        /// <summary>
        /// 初始化队列
        /// </summary>
        /// <param name="count"></param>
        public void Init(int count)
        {
            _count = count;
            _queue = new Dictionary<int, string>();

            for (int i = 0; i < _count; i++)
            {
                _queue.Add(i + 1, string.Empty);
            }
            ShowQueue();
        }

        private void ShowQueue()
        {
            Console.CursorTop = 5;
            Csl.Wl("---------------------------------------------------");
            for (int i = 0; i < _count; i++)
            {
                var idx = i + 1;
                var name = string.IsNullOrEmpty(_queue[idx]) ? "____" : _queue[idx];
                Csl.WlInLine(ConsoleColor.Green, idx + "." + name + "\t");
                if (idx % 4 == 0 && idx != 0)
                {
                    Csl.WlInLine("\n");
                }
            }

            Csl.Wl("---------------------------------------------------");
        }
    }
}
