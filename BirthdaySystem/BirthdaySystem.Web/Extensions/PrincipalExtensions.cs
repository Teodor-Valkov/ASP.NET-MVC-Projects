using BirthdaySystem.Models.Models.Employees;
using System.Security.Principal;

public static class PrincipalExtensions
{
    public static int GetUserId(this IPrincipal principal)
    {
        EmployeeModel employee = principal as EmployeeModel;

        if (employee != null)
        {
            return employee.Id;
        }

        return 0;
    }
}