using System.Security.Principal;

namespace PizzaLab.Models.Models.Users
{
    public class UserModel : IdentifiedObject, IPrincipal
    {
        public UserModel(int id, string username)
            : base(id)
        {
            this.Username = username;
            this.Identity = new GenericIdentity(username);
        }

        public string Username { get; set; }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            // There are currently no roles defined
            return false;
        }
    }
}