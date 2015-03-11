using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MX.IM.WebApp.Models;
using Oct.Framework.SignalR2.Models;

namespace Oct.Framework.SignalR2.signalr
{
    [HubName("pushHub")]
    public class PushHub : Hub
    {
        public void Login(string acc, int platform)
        {
            var id = Context.ConnectionId;
            IMUserManager.Instance.Add(acc, id, (Platform)platform);
        }

        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void SysMsg(string title, string message)
        {
            Clients.All.sysMsg(title, message);
        }

        /// <summary>
        /// 私人发布
        /// </summary>
        /// <param name="from">from 可以是多人 使用，分隔</param>
        /// <param name="to"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public void Send2(string from, string to, string title, string msg)
        {
            var users = IMUserManager.Instance.GetUser(to);
            foreach (var imUser in users)
            {
                 Clients.Client(imUser.ConnectionId).sysMsg(title, msg);
            }
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            IMUserManager.Instance.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);

        }
    }
}