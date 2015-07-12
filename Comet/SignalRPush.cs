using System;
using System.Collections.Generic;
using System.Linq;
using Comet.Contracts;
using Microsoft.AspNet.SignalR;
using Owin;

namespace Comet
{
    public class SignalRPush : ICometPush
    {
        private static IHubContext _hubContext;

        public static void InitializeInWebApp(string host, int port, IAppBuilder appBuilder, IUserIdResolver userIdResolver)
        {
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UserIdProvider(userIdResolver));
            InitializeInBackgroundApp(host, port);
            appBuilder.MapSignalR();
        }

        public static void InitializeInBackgroundApp(string host, int port)
        {
            GlobalHost.DependencyResolver.UseRedis(host, port, "", "SignalR");
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<EmptyHub>();
        }

        private IHubContext GetContext()
        {
            if (_hubContext == null)
            {
                throw new Exception("Сначала необходимо вызвать метод Initialize");
            }
            return _hubContext;
        }

        public void Push<T>(T data) where T : class
        {
            GetContext().Clients.All.notification(typeof(T).Name, data);
        }

        public void Push<T>(Guid reciever, T data) where T : class
        {
            GetContext().Clients.User(reciever.ToString()).notification(typeof(T).Name, data);
        }

        public void Push<T>(List<Guid> recievers, T data) where T : class
        {
            GetContext().Clients.Users(recievers.Select(x => x.ToString()).ToList()).notification(typeof(T).Name, data);
        }

        public void Push<T>(string notificationName, T data) where T : class
        {
            GetContext().Clients.All.notification(notificationName, data);
        }

        public void Push<T>(Guid reciever, string notificationName, T data) where T : class
        {
            GetContext().Clients.User(reciever.ToString()).notification(notificationName, data);
        }

        public void Push<T>(List<Guid> recievers, string notificationName, T data) where T : class
        {
            GetContext().Clients.Users(recievers.Select(x => x.ToString()).ToList()).notification(notificationName, data);
        }
    }
}
