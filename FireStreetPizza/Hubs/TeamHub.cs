using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FireStreetPizza.Hubs
{
  
    public class TeamHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}