namespace PaymentSystem.Models.ViewModels.Payments
{
    public class PaymentViewModel
    {
        public PaymentViewModel(int id, string accountIban, decimal accountAmount, string paymentIban, decimal paymentAmount, string paymentReason, string status)
        {
            this.Id = id;
            this.AccountIBAN = accountIban;
            this.AccountAmount = accountAmount;
            this.PaymentIBAN = paymentIban;
            this.PaymentAmount = paymentAmount;
            this.PaymentReason = paymentReason;
            this.Status = status;
        }

        public int Id { get; set; }

        public string AccountIBAN { get; set; }

        public decimal AccountAmount { get; set; }

        public string PaymentIBAN { get; set; }

        public decimal PaymentAmount { get; set; }

        public string PaymentReason { get; set; }

        public string Status { get; set; }
    }
}