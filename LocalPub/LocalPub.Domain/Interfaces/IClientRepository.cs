using System.Collections.Generic;
using LocalPub.Models;

namespace LocalPub.Domain.Interfaces
{
    public interface IClientRepository : IDbRepository
    {
        ICollection<ClientTypeDescription> GetAllClientTypes();

        bool IsClientWithSameUsernameExisting(string username);

        bool CreateClient(ClientCreateModel client);

        ClientWithPasswordModel GetClientByUsername(string username);
    }
}