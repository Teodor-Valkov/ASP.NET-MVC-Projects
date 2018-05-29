using LocalPub.Domain.Interfaces;
using LocalPub.Domain.SqlServer;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Common;

namespace LocalPub.Domain.Managers
{
    public class ClientManager : IClientManager
    {
        private IClientRepository clientRepository;

        public ClientManager()
            : this(new SqlClientRepository())
        {
        }

        public ClientManager(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public ClientModel GetClient(ClientLoginBindingModel clientModel)
        {
            ClientWithPasswordModel clientWithPassword = this.clientRepository.GetClientByUsername(clientModel.Username);
            if (clientWithPassword == null)
            {
                return null;
            }

            string actualPasswordHash = PasswordUtilities.GeneratePasswordHash(clientModel.Password, clientWithPassword.PasswordSalt);

            if (actualPasswordHash != clientWithPassword.PasswordHash)
            {
                return null;
            }

            ClientModel client = new ClientModel(clientWithPassword.Id, clientWithPassword.Username, clientWithPassword.Role);
            return client;
        }
    }
}