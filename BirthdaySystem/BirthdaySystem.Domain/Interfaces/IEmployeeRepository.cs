using BirthdaySystem.Models.BindingModels.Employees;
using BirthdaySystem.Models.Models.Employees;
using System;
using System.Collections.Generic;

namespace BirthdaySystem.Domain.Interfaces
{
    public interface IEmployeeRepository : IDbRepository
    {
        bool IsUserWithSameUsernameExisting(string username);

        bool CreateEmployee(EmployeeCreateModel employee);

        EmployeeWithPasswordModel GetEmployeeWithPasswordByUsername(string username);

        ICollection<EmployeeDescription> GetAllEmployeesExceptForCurrentUser(string username);

        DateTime GetEmployeeBirthDate(int employeeId);

        bool IsEmployeeExisting(int receiverId);
    }
}