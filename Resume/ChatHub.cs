using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Resume
{
    public class ChatHub : Hub
    {
        public void Send(int count)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastCount(++count);
        }
    }
}