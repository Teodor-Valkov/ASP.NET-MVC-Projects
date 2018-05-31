using LocalPub.Models;
using LocalPub.Models.BindingModels;
using System.Collections.Generic;

namespace LocalPub.Domain.Interfaces
{
    public interface IClientManager
    {
        ICollection<ClientTypeDescription> GetAllClientTypes();

        bool CreateClient(ClientCreateModel client);

        ClientModel GetClient(ClientLoginBindingModel clientModel);
    }
}