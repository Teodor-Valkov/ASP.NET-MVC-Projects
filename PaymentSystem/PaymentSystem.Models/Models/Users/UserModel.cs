using System.Security.Principal;
using PaymentSystem.Common;

namespace PaymentSystem.Models.Models.Users
{
    public class UserModel : IPrincipal
    {
        public UserModel(int id, string username)
        {
            this.Id = id;
            this.Username = username;
            this.Identity = new GenericIdentity(this.Username, AuthConstants.CookieAuthType);
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
