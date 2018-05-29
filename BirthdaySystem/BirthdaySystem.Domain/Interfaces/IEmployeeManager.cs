using BirthdaySystem.Models.BindingModels.Employees;
using BirthdaySystem.Models.Models.Employees;

namespace BirthdaySystem.Domain.Interfaces
{
    public interface IEmployeeManager
    {
        bool CreateEmployee(EmployeeRegisterBindingModel employeeModel);

        EmployeeModel GetEmployee(EmployeeLoginBindingModel employeeModel);
    }
}