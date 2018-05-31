using System;

namespace PaymentSystem.Models.ViewModels.Payments
{
    public class PaymentViewModel
    {
        public PaymentViewModel(int id, string accountIban, string paymentIban, decimal paymentAmount, string paymentReason, DateTime paymentDate, string status)
        {
            this.Id = id;
            this.AccountIBAN = accountIban;
            this.PaymentIBAN = paymentIban;
            this.PaymentAmount = paymentAmount;
            this.PaymentReason = paymentReason;
            this.PaymentDate = paymentDate;
            this.Status = status;
        }

        public int Id { get; set; }

        public string AccountIBAN { get; set; }

        public string PaymentIBAN { get; set; }

        public decimal PaymentAmount { get; set; }

        public string PaymentReason { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Status { get; set; }
    }
}