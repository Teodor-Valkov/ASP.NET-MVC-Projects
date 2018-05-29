using System.Security.Principal;
using static BirthdaySystem.Common.AuthConstants;

namespace BirthdaySystem.Models.Models.Employees
{
    public class EmployeeModel : IPrincipal
    {
        public EmployeeModel(int id, string username)
        {
            this.Id = id;
            this.Username = username;
            this.Identity = new GenericIdentity(this.Username, CookieAuthType);
        }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            // The application does not work with roles for the moment
            return false;
        }
    }
}