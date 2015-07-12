using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



using System.Configuration;
using Comet;
using Comet.Contracts;

using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System;
using SignalR_UCM;
using SignalR_UCM.Models;

[assembly: OwinStartup(typeof(SignalR_UCM.App_Start.Startup))]
namespace SignalR_UCM.App_Start
{
   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var cString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
            var host = cString.Split(':')[0];
            var port = int.Parse(cString.Split(':')[1]);
            SignalRPush.InitializeInWebApp(host, port, app, new UserIdResolver());
           
            
        }
    }
}