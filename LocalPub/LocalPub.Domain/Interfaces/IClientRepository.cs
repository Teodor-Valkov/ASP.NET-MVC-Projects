using LocalPub.Models;

namespace LocalPub.Domain.Interfaces
{
    public interface IClientRepository : IDbRepository
    {
        ClientWithPasswordModel GetClientByUsername(string username);
    }
}