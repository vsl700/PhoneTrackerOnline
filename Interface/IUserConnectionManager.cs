using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTrackerOnline.Interface
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string userId);
    }
}
