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
        public AccountService _serice = new AccountService();
        public MessageService _m = new MessageService();
        public QuestionService _q = new QuestionService();
        public static List<UserInfo> UserList = new List<UserInfo>();

        public override Task OnConnected()
        {
            var user = _GetUser(Context.ConnectionId);  // 查询用户。

            if (user == null)   //判断用户是否存在,否则添加进集合
            {
                user = new UserInfo()
                {
                    ContextID = Context.ConnectionId
                };
                UserList.Add(user);
            }
            _GetUserList();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stop)
        {
            var user = _GetUser(Context.ConnectionId);

            //判断用户是否存在,存在则删除
            if (user != null)
            {
                //删除用户
                UserList.Remove(user);

            }
            //更新所有用户的列表
            _GetUserList();
            return base.OnDisconnected(true);
        }

        private UserInfo _GetUser(string id)
        {
            return UserList.FirstOrDefault(u => u.ContextID == id);
        }

        private void _setName(string name)
        {
            var user = _GetUser(Context.ConnectionId);  // 查询用户。
            if (user != null)
            {
                user.Name = name;
            }

            // 回傳Id給User
            var id = _serice.Login(name);
            Clients.Client(Context.ConnectionId).sendIdToUser(id);

            // 發第一題給user
            var q = _q.Get(id);
            Clients.Client(Context.ConnectionId).sendQuestionToUser(q);
        }

        private void _GetUserList()
        {
            var list = from a in UserList
                       select new { a.Name, a.ContextID };
            //string jsondata = JsonConvert.SerializeObject(itme.ToList());
            Clients.All.getUserlist(list);
        }

        public void setUserName(string name)
        {
            _setName(name);

            _GetUserList();
        }

        public void refreshTargetPage(string userId)
        {
            Clients.Client(userId).broadcastRefresh();
        }

        public void sendTargetUserSurvey(string name)
        {
            var user = UserList.FirstOrDefault(u => u.Name == name);
            if (user != null)
            {
            }
        }

        public void Send(string name, string message)
        {
            _m.MessageLog(name, message);

            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void Answer(int id, int questionId, string answer)
        {
            _q.Answer(id, questionId, answer);

            var q = _q.Get(id);
            Clients.Client(Context.ConnectionId).sendQuestionToUser(q);
        }
    }
}
