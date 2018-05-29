namespace PaymentSystem.Models.ViewModels.Accounts
{
    public class AccountViewModel
    {
        public AccountViewModel(int id, string iban, decimal amount)
        {
            this.Id = id;
            this.IBAN = iban;
            this.Amount = amount;
        }

        public int Id { get; set; }

        public string IBAN { get; set; }

        public decimal Amount { get; set; }
    }
}
