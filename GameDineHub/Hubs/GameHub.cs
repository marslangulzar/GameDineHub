﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GameDineHub.Hubs
{
    [HubName("game")]
    public class GameHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}