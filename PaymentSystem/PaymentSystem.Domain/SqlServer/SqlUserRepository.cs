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

            using (reader)
            {
                if (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string userUsername = reader.GetString(1);
                    string passwordHash = reader.GetString(2);
                    string passwordSalt = reader.GetString(3);

                    UserWithPasswordModel userWithPassword = new UserWithPasswordModel(userId, userUsername, passwordHash, passwordSalt);
                    return userWithPassword;
                }

                return null;
            }
        }

        public bool IsUserWithSameUsernameExisting(string username)
        {
            int usersCount = (int)this.ExecuteScalar(
                    @"SELECT COUNT(*)
                          FROM Users
                         WHERE Username = @username",
                         new Dictionary<string, object>()
                         {
                                { "@username", username }
                         });

            return usersCount > 0;
        }

        public bool CreateUser(UserCreateModel user)
        {
            int recordsInserted = this.ExecuteNonQuery(
                       @"INSERT INTO Users (Username, Name, PasswordHash, PasswordSalt)
                         VALUES (@username, @name, @passwordHash, @passwordSalt)",
                            new Dictionary<string, object>()
                            {
                                { "@username", user.Username },
                                { "@name", user.Name },
                                { "@passwordHash", user.PasswordHash },
                                { "@passwordSalt", user.PasswordSalt }
                            });

            return recordsInserted > 0;
        }
    }
}