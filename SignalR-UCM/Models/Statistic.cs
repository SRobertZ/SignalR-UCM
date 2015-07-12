using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_UCM.Models
{
    public static class Statistic
    {
        private static int TotalSentMessages { get; set; }

        public static string GetTotal()
        {
            return (++TotalSentMessages).ToString();
        }


    }
}