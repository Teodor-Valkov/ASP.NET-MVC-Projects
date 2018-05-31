﻿using System.Collections.Generic;
using System.Data.SqlClient;
using PizzaLab.Models.ViewModels.Users;
using PizzaLab.Services.Interfaces;

namespace PizzaLab.Services.Repositories
{
    public class UserRepository : DbRepository, IUserRepository
    {
        public UserRepository()
        {
        }

        public UserRepository(string connectionString)
            : base(connectionString)
        {
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

        public bool CreateUser(CreateUserModel user)
        {
            int recordsInserted = this.ExecuteNonQuery(
                  @"INSERT INTO Users (Username, Address, Phone, PasswordHash, PasswordSalt)
                         VALUES (@username, @address, @phone, @passwordHash, @passwordSalt)",
                            new Dictionary<string, object>()
                            {
                                    { "@username", user.Username },
                                    { "@address", user.Address },
                                    { "@phone", user.Phone },
                                    { "@passwordHash", user.PasswordHash },
                                    { "@passwordSalt", user.PasswordSalt }
                            });

            return recordsInserted > 0;
        }

        public UserModel GetUserByUsername(string username)
        {
            UserModel user = null;

            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id, Username, PasswordHash, PasswordSalt
                     FROM Users
                    WHERE Username = @username",
                 new Dictionary<string, object>
                 {
                     { "@username", username }
                 });

            using (reader)
            {
                for (int usersCount = 0; reader.Read() && usersCount <= 1; usersCount++)
                {
                    int id = reader.GetInt32(0);
                    string usernameFromDatabase = reader.GetString(1);
                    string passwordHash = reader.GetString(2);
                    string passwordSalt = reader.GetString(3);

                    user = new UserModel(id, usernameFromDatabase, passwordHash, passwordSalt);
                }
            }

            return user;
        }
    }
}