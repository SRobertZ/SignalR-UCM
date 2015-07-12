using Comet;
using SignalR_UCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignalR_UCM.Controllers
{
    public class CometController : ApiController
    {
        class TestMessage
        {
            public string Text { get; set; }
        }
        
        [HttpGet]
        public void Push()
        {
            var pusher = new SignalRPush();
            pusher.Push<TestMessage>(Guid.Parse("39162f6d-a03b-425f-9952-56b83599f7ff"), new TestMessage { Text = "hello " + Statistic.GetTotal() });
        }
    }
}
