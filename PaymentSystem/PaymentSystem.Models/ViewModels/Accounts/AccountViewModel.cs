namespace PaymentSystem.Models.ViewModels.Accounts
{
    public class AccountViewModel
    {
        public AccountViewModel(string iban, decimal amount)
        {
            this.IBAN = iban;
            this.Amount = amount;
        }

        public string IBAN { get; set; }

        public decimal Amount { get; set; }
    }
}