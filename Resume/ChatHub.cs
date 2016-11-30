using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Resume.Service;
using Resume.Models;

namespace Resume
{
    public class UserInfo
    {
        [Key]
        public string ContextID { get; set; }

        public string Name { get; set; }
    }
    public class ChatHub : Hub
    {
        public static List<UserInfo> UserList = new List<UserInfo>();

        private DeviceService _device = new DeviceService();
        private MemberService _member = new MemberService();

        public override Task OnConnected()
        {
            // 查詢用戶
            var user = _GetUser(Context.ConnectionId);

            // 判斷用戶是否存在，否則新增
            if (user == null)
            {
                user = new UserInfo()
                {
                    Name = "",
                    ContextID = Context.ConnectionId
                };
                UserList.Add(user);
            }

            return base.OnConnected();
        }

        private UserInfo _GetUser(string id)
        {
            return UserList.SingleOrDefault(p => p.ContextID == id);
        }

        public void GetName(int id)
        {
            // 查詢用戶
            var user = _GetUser(Context.ConnectionId);
            if (user != null)
            {
                user.Name = id.ToString();
                Clients.Client(Context.ConnectionId).showID(Context.ConnectionId);
                
                _device.AddDevice(new Device
                {
                    memberid = id,
                    sessionid = Context.ConnectionId
                });
            }

            GetUserList();
        }

        public void GetUserList()
        {
            var item = from a in UserList
                       select new { a.Name, a.ContextID };
            string jsondata = JsonConvert.SerializeObject(item.ToList());
            Clients.All.getUserlist(jsondata);
        }

        public override Task OnDisconnected(bool stop)
        {
            var user = _GetUser(Context.ConnectionId);
            if (user != null)
            {
                // 刪除
                UserList.Remove(user);
            }

            // 更新用戶列表
            GetUserList();
            return base.OnDisconnected(stop);
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void Toggle()
        {
            Clients.All.toggle();
        }

        public void Hide()
        {
            Clients.All.hide();
        }

        public void Refresh()
        {
            Clients.All.refresh();
        }
    }
}