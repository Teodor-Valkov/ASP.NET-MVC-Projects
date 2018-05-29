namespace LocalPub.Models
{
    public class ClientWithPasswordModel : ClientModel
    {
        public ClientWithPasswordModel(int id, string username, string role, string passwordHash, string passwordSalt)
            : base(id, username, role)
        {
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }

        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }
    }
}