using Comet.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_UCM.Models
{
    public class UserIdResolver : IUserIdResolver
    {
        


        public UserIdResolver()
        {

        }

        public Guid? GetUserId(string token, bool onlyRegisterCompletedUsers, bool useMobileToken)
        {
            return Guid.Parse("39162f6d-a03b-425f-9952-56b83599f7ff");
        }



    }
}