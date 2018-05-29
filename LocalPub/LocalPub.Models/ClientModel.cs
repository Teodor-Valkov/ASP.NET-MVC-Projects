using LocalPub.Common;
using System.Security.Principal;

namespace LocalPub.Models
{
    public class ClientModel : IPrincipal
    {
        public ClientModel(int id, string username, string role)
        {
            this.Id = id;
            this.Username = username;
            this.Role = role;
            this.Identity = new GenericIdentity(this.Username, AuthConstants.CookieAuthType);
        }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public string Role { get; private set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return this.Role == role;
        }
    }
}