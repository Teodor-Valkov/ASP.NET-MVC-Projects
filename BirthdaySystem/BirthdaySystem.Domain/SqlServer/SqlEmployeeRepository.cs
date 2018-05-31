using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Models.Models.Employees;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BirthdaySystem.Domain.SqlServer
{
    public class SqlEmployeeRepository : DbRepository, IEmployeeRepository
    {
        public SqlEmployeeRepository()
        {
        }

        public SqlEmployeeRepository(string connectionString)
            : base(connectionString)
        {
        }

        public bool IsUserWithSameUsernameExisting(string username)
        {
            int employeesCount = (int)this.ExecuteScalar(
                      @"SELECT COUNT(*)
                          FROM Employees
                         WHERE Username = @username",
                           new Dictionary<string, object>()
                           {
                                { "@username", username }
                           });

            return employeesCount > 0;
        }

        public bool CreateEmployee(EmployeeCreateModel employee)
        {
            int recordsInserted = this.ExecuteNonQuery(
                  @"INSERT INTO Employees (Username, Name, BirthDate, PasswordHash, PasswordSalt)
                         VALUES (@username, @name, @birthDate, @passwordHash, @passwordSalt)",
                            new Dictionary<string, object>()
                            {
                                      { "@username", employee.Username },
                                      { "@name", employee.Name },
                                      { "@birthDate", employee.BirthDate },
                                      { "@passwordHash", employee.PasswordHash },
                                      { "@passwordSalt", employee.PasswordSalt }
                            });

            return recordsInserted > 0;
        }

        public EmployeeWithPasswordModel GetEmployeeWithPasswordByUsername(string username)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id,
                          Username,
                          PasswordHash,
                          PasswordSalt
                     FROM Employees
                    WHERE Username = @username",
                      new Dictionary<string, object>
                      {
                          { "@username", username }
                      });

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string usernameFromDatabase = reader.GetString(1);
                    string passwordHash = reader.GetString(2);
                    string passwordSalt = reader.GetString(3);

                    EmployeeWithPasswordModel employeeWithPassword = new EmployeeWithPasswordModel(id, usernameFromDatabase, passwordHash, passwordSalt);
                    return employeeWithPassword;
                }
            }

            return null;
        }

        public ICollection<EmployeeDescription> GetAllEmployeesExceptForCurrentUser(string username)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id,
                          Username,
                          Name
                     FROM Employees
                    WHERE Username != @username",
                      new Dictionary<string, object>
                      {
                          { "@username", username }
                      });

            IList<EmployeeDescription> employees = new List<EmployeeDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string userUsername = reader.GetString(1);
                    string userName = reader.GetString(2);
                    string userNameRepresentation = this.CombineUsernameAndName(userUsername, userName);

                    EmployeeDescription employee = new EmployeeDescription(userId, userNameRepresentation);
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public DateTime GetEmployeeBirthDate(int employeeId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT BirthDate
                     FROM Employees
                    WHERE Id = @employeeId",
                    new Dictionary<string, object>
                    {
                          { "@employeeId", employeeId }
                    });

            using (reader)
            {
                DateTime birthDate = default(DateTime);

                if (reader.Read())
                {
                    birthDate = reader.GetDateTime(0);
                }

                return birthDate;
            }
        }

        public bool IsEmployeeExisting(int employeeId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id
                     FROM Employees
                    WHERE Id = @employeeId",
                      new Dictionary<string, object>()
                      {
                          { "@employeeId", employeeId }
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    return true;
                }

                return false;
            }
        }

        private string CombineUsernameAndName(string username, string name)
        {
            string resultName = string.Format("{0} ({1})", string.IsNullOrEmpty(username) ? "[no name]" : name, username);
            return resultName;
        }
    }
}