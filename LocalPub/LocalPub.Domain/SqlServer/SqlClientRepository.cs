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