using LocalPub.Domain.Interfaces;
using LocalPub.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LocalPub.Domain.SqlServer
{
    public class SqlClientRepository : DbRepository, IClientRepository
    {
        public SqlClientRepository()
        {
        }

        public SqlClientRepository(string connectionString)
            : base(connectionString)
        {
        }

        public ICollection<ClientTypeDescription> GetAllClientTypes()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id, Name
                     FROM ClientTypes");

            ICollection<ClientTypeDescription> clientTypes = new List<ClientTypeDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int clientTypeId = reader.GetInt32(0);
                    string clientTypeName = reader.GetString(1);
                    ClientTypeDescription clientType = new ClientTypeDescription(clientTypeId, clientTypeName);

                    clientTypes.Add(clientType);
                }
            }

            return clientTypes;
        }

        public bool IsClientWithSameUsernameExisting(string username)
        {
            int clientsCount = (int)this.ExecuteScalar(
                  @"SELECT COUNT(*)
                      FROM Clients
                     WHERE Username = @username",
                       new Dictionary<string, object>()
                       {
                            { "@username", username }
                       });

            return clientsCount > 0;
        }

        public bool CreateClient(ClientCreateModel user)
        {
            int recordsInserted = this.ExecuteNonQuery(
                  @"INSERT INTO Clients (Username, Name, PasswordHash, PasswordSalt, ClientTypeId)
                         VALUES (@username, @name, @passwordHash, @passwordSalt, @clientTypeId)",
                            new Dictionary<string, object>()
                            {
                                    { "@username", user.Username },
                                    { "@name", user.Name },
                                    { "@passwordHash", user.PasswordHash },
                                    { "@passwordSalt", user.PasswordSalt },
                                    { "@clientTypeId", user.ClientTypeId }
                            });

            return recordsInserted > 0;
        }

        public ClientWithPasswordModel GetClientByUsername(string username)
        {
            SqlDataReader reader = this.ExecuteReader(
                    @"SELECT c.Id,
                             c.Username,
                             c.PasswordHash,
                             c.PasswordSalt,
                             ct.Name AS RoleName
                        FROM Clients AS c
                        JOIN ClientTypes AS ct
                          ON c.ClientTypeId = ct.Id
                       WHERE Username = @username",
                         new Dictionary<string, object>
                         {
                             { "@username", username }
                         });

            ClientWithPasswordModel clientWithPassword = null;

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string usernameFromDatabase = reader.GetString(1);
                    string passwordHash = reader.GetString(2);
                    string passwordSalt = reader.GetString(3);
                    string roleName = reader.GetString(4);

                    clientWithPassword = new ClientWithPasswordModel(id, usernameFromDatabase, roleName, passwordHash, passwordSalt);
                }
            }

            return clientWithPassword;
        }
    }
}