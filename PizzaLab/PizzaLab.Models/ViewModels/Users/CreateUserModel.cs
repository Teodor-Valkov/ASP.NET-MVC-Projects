namespace PizzaLab.Models.ViewModels.Users
{
    public class CreateUserModel
    {
        public CreateUserModel(string username, string address, string phone, string passwordHash, string passwordSalt)
        {
            this.Username = username;
            this.Address = address;
            this.Phone = phone;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }

        public string Username { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
