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
        }

        private OctMQClient _client;

        private void btnConn_Click(object sender, EventArgs e)
        {
            _client = new OctMQClient();
            _client.OnReceive += _client_OnReceive;

            _client.Init(txtip.Text,int.Parse(txtport.Text),ClientType.Sub);
            var sub = (SubscriberSocket) _client.Client;
            sub.Subscribe("ww");
            _client.StartAsyncReceive();
            Csl.Init();
        }

        void _client_OnReceive(object sender, Core.Args.DataEventArgs<NetMQ.NetMQSocket, NetMQ.NetMQMessage> e)
        {
            Csl.Wl("sss");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_client == null)
            {
                MessageBox.Show("please connect");return;
            }
            var content = txtContent.Text;
            var msg = _client.CreateMessage();
            msg.Append(content);
            _client.Send(msg);
            var rmsg = _client.ReceiveMessage();
            var call = rmsg.Pop().ConvertToString();
            Csl.Wl(call);
        }
    }
}
