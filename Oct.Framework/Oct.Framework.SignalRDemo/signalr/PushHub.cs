using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Oct.Framework.Core.Cookie;
using Oct.Framework.Core.Json;
using Oct.Framework.SignalRDemo.Common;
using Oct.Framework.SignalRDemo.Models;

namespace Oct.Framework.SignalRDemo.signalr
{
    [HubName("pushHub")]
    public class PushHub : Hub
    {
        

        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="message"></param>
        public void SendAll(string message)
        {
            Clients.All.showMessage("系统管理员", message);
        }

        /// <summary>
        /// 全局对谁发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            Clients.All.addNewMessage(name,message);
        }

        /// <summary>
        /// 发送私人广播
        /// </summary>
        public void SendPrivateBroadcast(string account , string msg)
        {
            var ids = SessionMgr.AllAcount.Where(p => p.Value == account);
            var user = CookieHelper.GetCookieValue("user");
            foreach (var id in ids)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
                context.Clients.Client(id.Key).sendMessage(user, msg);
            }
            
        }

        

        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            var user = CookieHelper.GetCookieValue("user");
            SessionMgr.AllAcount.Add(id, user);
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var id = Context.ConnectionId;
            
            SessionMgr.AllAcount.Remove(id);
            return base.OnDisconnected();
        }

    }
}