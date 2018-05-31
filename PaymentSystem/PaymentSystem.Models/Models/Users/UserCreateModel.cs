using System;

namespace PaymentSystem.Models.Models.Users
{
    public class UserCreateModel
    {
        public UserCreateModel(string username, string name, string passwordHash, string passwordSalt)
        {
            this.Username = username;
            this.Name = name;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }

        public string Username { get; private set; }

        public string Name { get; private set; }

        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }
    }
}