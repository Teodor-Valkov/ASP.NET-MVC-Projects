﻿namespace BirthdaySystem.Models.Models.Employees
{
    public class EmployeeWithPasswordModel
    {
        public EmployeeWithPasswordModel(int id, string username, string passwordHash, string passwordSalt)
        {
            this.Id = id;
            this.Username = username;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }
    }
}