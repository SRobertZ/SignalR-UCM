using Comet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        class TestMessage
        {
            public string Text { get; set; }
        }

        static void Main(string[] args)
        {
            var cString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
            var host = cString.Split(':')[0];
            var port = int.Parse(cString.Split(':')[1]);
            SignalRPush.InitializeInBackgroundApp(host, port/*, new Comet.UserIdResolver()*/);

            var pusher = new SignalRPush();
            int total = 1;
            while (true)
            {
                Console.ReadKey();
                pusher.Push<TestMessage>(new TestMessage { Text = "hello " + total.ToString() });
                total++;
            }
           
        }
    }
}
