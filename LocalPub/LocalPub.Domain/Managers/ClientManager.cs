using LocalPub.Domain.Interfaces;
using LocalPub.Domain.SqlServer;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Common;
using System.Collections.Generic;

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

        public ICollection<ClientTypeDescription> GetAllClientTypes()
        {
            using (this.clientRepository)
            {
                ICollection<ClientTypeDescription> clientTypes = this.clientRepository.GetAllClientTypes();
                return clientTypes;
            }
        }

        public bool CreateClient(ClientCreateModel client)
        {
            using (this.clientRepository)
            {
                bool isClientWithSameUsernameExisting = this.clientRepository.IsClientWithSameUsernameExisting(client.Username);
                if (isClientWithSameUsernameExisting)
                {
                    return false;
                }

                string passwordSalt = PasswordUtilities.GeneratePasswordSalt();
                string passwordHash = PasswordUtilities.GeneratePasswordHash(client.Password, passwordSalt);
                client.PasswordHash = passwordHash;
                client.PasswordSalt = passwordSalt;

                bool createClientResult = this.clientRepository.CreateClient(client);
                return createClientResult;
            }
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