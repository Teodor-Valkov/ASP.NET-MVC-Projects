namespace LocalPub.Models
{
    public class ClientCreateModel
    {
        public ClientCreateModel(string username, string name, string password, int clientTypeId)
        {
            this.Username = username;
            this.Name = name;
            this.Password = password;
            this.ClientTypeId = clientTypeId;
        }

        public string Username { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public int ClientTypeId { get; private set; }
    }
}