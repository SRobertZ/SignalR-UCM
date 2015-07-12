using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Comet.Contracts
{
    public interface IUserIdResolver
    {
        Guid? GetUserId(string token, bool onlyRegisterCompletedUsers, bool useMobileToken);
        
    }
}
