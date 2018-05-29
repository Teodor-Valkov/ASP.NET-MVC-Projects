namespace PaymentSystem.Models.ViewModels.Accounts
{
    public class AccountDescriptionViewModel
    {
        public AccountDescriptionViewModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
