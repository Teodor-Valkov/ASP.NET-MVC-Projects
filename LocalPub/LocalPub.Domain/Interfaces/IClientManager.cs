using LocalPub.Models;
using LocalPub.Models.BindingModels;

namespace LocalPub.Domain.Interfaces
{
    public interface IClientManager
    {
        ClientModel GetClient(ClientLoginBindingModel clientModel);
    }
}