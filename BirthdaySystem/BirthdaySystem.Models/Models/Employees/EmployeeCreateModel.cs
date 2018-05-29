using System;

namespace BirthdaySystem.Models.Models.Employees
{
    public class EmployeeCreateModel
    {
        public EmployeeCreateModel(string username, string name, string passwordHash, string passwordSalt, DateTime birthDate)
        {
            this.Username = username;
            this.Name = name;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.BirthDate = birthDate;
        }

        public string Username { get; private set; }

        public string Name { get; private set; }

        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
