using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comet.Contracts
{
    public interface ICometPush
    {
        void Push<T>(T data) where T : class;
        void Push<T>(Guid reciever, T data) where T : class;
        void Push<T>(List<Guid> recievers, T data) where T : class;
        void Push<T>(string notificationName, T data) where T : class;
        void Push<T>(Guid reciever, string notificationName, T data) where T : class;
        void Push<T>(List<Guid> recievers, string notificationName, T data) where T : class;
    }
}
