using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetMQ.Sockets;
using Oct.Framework.Core.Common;
using Oct.Framework.MQ;

namespace Oct.Framework.MQClientDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Csl.Init();
        }

        /// <summary>
        /// mq客户端
        /// </summary>
        private OctMQClient _client;

        /// <summary>
        /// 订阅者模式连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, EventArgs e)
        {
            _client = new OctMQClient();
            _client.OnReceive += _client_OnReceive;

            _client.Init(txtip.Text, int.Parse(txtport.Text), ClientType.Sub);
            var sub = (SubscriberSocket)_client.Client;
            sub.Subscribe(txtTop.Text);
            _client.StartAsyncReceive();

        }

        /// <summary>
        /// 订阅者模式受到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _client_OnReceive(object sender, Core.Args.DataEventArgs<NetMQ.NetMQSocket, NetMQ.NetMQMessage> e)
        {
            var msg = e.Arg2;
            Csl.Wl("主题：" + msg.Pop().ConvertToString(Encoding.UTF8));
            Csl.Wl("内容：" + msg.Pop().ConvertToString(Encoding.UTF8));
        }

        /// <summary>
        /// 发送响应消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            using (_client = new OctMQClient())
            {
                _client.Init(txtip.Text, int.Parse(txtport.Text), ClientType.Request);
                var content = txtContent.Text;
                var msg = _client.CreateMessage();
                msg.Append(content, Encoding.UTF8);
                _client.Send(msg);
                var rmsg = _client.ReceiveMessage();
                var reqStr =  rmsg.Pop().ConvertToString(Encoding.UTF8);
                Csl.Wl(reqStr);
            }

        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (_client != null)
            {
                _client.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (_client = new OctMQClient())
            {
                _client.Init(txtip.Text, int.Parse(txtport.Text), ClientType.Request);
                var msg = _client.CreateMessage();
                msg.Append(int.Parse(txtSeat.Text));
                msg.Append(txtName.Text, Encoding.UTF8);
                _client.Send(msg);
                var rmsg = _client.ReceiveMessage();
                var ret = rmsg.Pop().ConvertToInt32();
                var reqStr = rmsg.Pop().ConvertToString(Encoding.UTF8);
                Csl.Wl(string.Format("判断值：{0}，结果：{1}", ret, reqStr));
            }
        }

        private void btnAsync_Click(object sender, EventArgs e)
        {
            var count = int.Parse(txtCount.Text);
            Csl.Wl("----------------------------------------------");
            Parallel.For(0, count, (x) =>
            {
                try
                {
                    using ( var sclient = new OctMQClient())
                    {
                        sclient.Init(txtip.Text, int.Parse(txtport.Text), ClientType.Request);
                        var msg = sclient.CreateMessage();
                        msg.Append(int.Parse(txtSeat.Text));
                        msg.Append(txtName.Text, Encoding.UTF8);
                        sclient.Send(msg);
                        var rmsg = sclient.ReceiveMessage();
                        var ret = rmsg.Pop().ConvertToInt32();
                        var reqStr = rmsg.Pop().ConvertToString(Encoding.UTF8);
                        Csl.Wl(string.Format("判断值：{0}，结果：{1},第几个进程:{2}", ret, reqStr,x));
                    }
                }
                catch (Exception)
                {
                }
                
            });
            Csl.Wl("----------------------------------------------");
        }
    }
}
