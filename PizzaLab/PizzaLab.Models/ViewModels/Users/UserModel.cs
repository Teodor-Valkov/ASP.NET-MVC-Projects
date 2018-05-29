using System.Security.Principal;

namespace PizzaLab.Models.ViewModels.Users
{
    public class UserModel : IdentifiedObject, IPrincipal
    {
        public UserModel(int id, string username, string passwordHash, string passwordSalt)
            : base(id)
        {
            this.Username = username;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.Identity = new GenericIdentity(username);
        }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            // There are currently no roles defined
            return false;
        }
    }
}
