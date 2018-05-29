using BirthdaySystem.Common;
using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Domain.SqlServer;
using BirthdaySystem.Models.BindingModels.Employees;
using BirthdaySystem.Models.Models.Employees;

namespace BirthdaySystem.Domain.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeRepository employeeRepository;

        public EmployeeManager()
            : this(new SqlEmployeeRepository())
        {
        }

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public bool CreateEmployee(EmployeeRegisterBindingModel employeeModel)
        {
            using (this.employeeRepository)
            {
                bool isUserWithSameUsernameExisting = this.employeeRepository.IsUserWithSameUsernameExisting(employeeModel.Username);
                if (isUserWithSameUsernameExisting)
                {
                    return false;
                }

                string passwordSalt = PasswordUtilities.GeneratePasswordSalt();
                string passwordHash = PasswordUtilities.GeneratePasswordHash(employeeModel.Password, passwordSalt);
                EmployeeCreateModel employee = new EmployeeCreateModel(employeeModel.Username, employeeModel.Name, passwordHash, passwordSalt, employeeModel.BirthDate);

                bool createEmployeeResult = this.employeeRepository.CreateEmployee(employee);
                return createEmployeeResult;
            }
        }

        public EmployeeModel GetEmployee(EmployeeLoginBindingModel employeeModel)
        {
            using (this.employeeRepository)
            {
                EmployeeWithPasswordModel employeeWithPassword = this.employeeRepository.GetEmployeeWithPasswordByUsername(employeeModel.Username);
                if (employeeWithPassword == null)
                {
                    return null;
                }

                string actualPasswordHash = PasswordUtilities.GeneratePasswordHash(employeeModel.Password, employeeWithPassword.PasswordSalt);
                if (actualPasswordHash != employeeWithPassword.PasswordHash)
                {
                    return null;
                }

                EmployeeModel employee = new EmployeeModel(employeeWithPassword.Id, employeeWithPassword.Username);
                return employee;
            }
        }
    }
}