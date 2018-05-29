using System.Collections.Generic;
using System.Data.SqlClient;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Domain.SqlServer
{
    public class SqlUserRepository : DbRepository, IUserRepository
    {
        public SqlUserRepository()
        {
        }

        public SqlUserRepository(string connectionString)
            : base(connectionString)
        {
        }

        public UserWithPasswordModel GetUserWithPasswordByUsername(string username)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id,
                          Username,
                          PasswordHash,
                          PasswordSalt
                     FROM Users
                    WHERE Username = @username",
                      new Dictionary<string, object>
                      {
                          { "@username", username }
                      });

            UserWithPasswordModel userWithPassword = null;

            using (reader)
            {
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string userUsername = reader.GetString(1);
                    string passwordHash = reader.GetString(2);
                    string passwordSalt = reader.GetString(3);

                    userWithPassword = new UserWithPasswordModel(userId, userUsername, passwordHash, passwordSalt);
                }
            }

            return userWithPassword;
        }
    }
}
